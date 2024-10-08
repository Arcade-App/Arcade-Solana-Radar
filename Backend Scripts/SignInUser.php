<?php
// Database connection settings and encryption key
require 'ConnectionSettings.php';

// Encryption key and method
$encryption_method = "AES-256-CBC";  // AES encryption
$iv_length = openssl_cipher_iv_length($encryption_method);

// Check if the connection is successful
if ($conn->connect_error) {
    die(json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error)));
}

// Sanitize and validate the inputs
$username = filter_input(INPUT_POST, 'username', FILTER_SANITIZE_STRING);
$password = filter_input(INPUT_POST, 'password', FILTER_SANITIZE_STRING);

// Check if the required fields are provided
if (!$username || !$password) {
    die(json_encode(array("status" => "error", "message" => "Invalid input. Please provide valid user details.")));
}

// Prepare the SQL statement to fetch the user based on the username
$stmt = $conn->prepare("SELECT userId, password, walletAddress, walletMnemonics, email, username FROM users WHERE username = ?");

if (!$stmt) {
    die(json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement.")));
}

$stmt->bind_param("s", $username);
$stmt->execute();
$stmt->store_result();

if ($stmt->num_rows > 0) {
    // User found, fetch details
    $stmt->bind_result($userId, $hashedPassword, $walletAddress, $encryptedMnemonics, $email, $fetchedUsername);
    $stmt->fetch();

    // Verify the password
    if (password_verify($password, $hashedPassword)) {
        // Decrypt the wallet mnemonics
        if (!empty($encryptedMnemonics)) {
            list($encrypted_data, $iv) = explode('::', base64_decode($encryptedMnemonics), 2);
            $decryptedMnemonics = openssl_decrypt($encrypted_data, $encryption_method, $encryption_key, 0, $iv);
        } else {
            $decryptedMnemonics = null;  // No mnemonics stored
        }

        // Return userId, username, email, walletAddress, and decrypted walletMnemonics in JSON format
        $response = array(
            "status" => "success",
            "userId" => $userId,
            "username" => $fetchedUsername,  // Return the fetched username
            "email" => $email,               // Return the fetched email
            "walletAddress" => $walletAddress,
            "walletMnemonics" => $decryptedMnemonics  // Decrypted wallet mnemonics
        );
        echo json_encode($response);
    } else {
        echo json_encode(array("status" => "error", "message" => "Invalid password"));
    }
} else {
    echo json_encode(array("status" => "error", "message" => "User not found"));
}

// Close the statement and connection
$stmt->close();
$conn->close();
?>
