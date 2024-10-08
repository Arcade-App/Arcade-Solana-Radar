<?php

// Include the database connection settings and encryption key
require 'ConnectionSettings.php';

// Define encryption method
$encryption_method = "AES-256-CBC";  // AES encryption
$iv_length = openssl_cipher_iv_length($encryption_method);

// Check connection
if ($conn->connect_error) {
    echo json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error));
    exit();
}

// Variables submitted by users, sanitized
$userId = filter_input(INPUT_POST, 'userId', FILTER_VALIDATE_INT);
$walletAddress = filter_input(INPUT_POST, 'walletAddress', FILTER_SANITIZE_STRING);
$walletBalance = filter_input(INPUT_POST, 'walletBalance', FILTER_VALIDATE_INT);
$walletMnemonics = filter_input(INPUT_POST, 'walletMnemonics', FILTER_SANITIZE_STRING);

// Validate required fields
if (!$userId || !$walletAddress || $walletBalance === false || !$walletMnemonics) {
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide valid user details."));
    exit();
}

// Encrypt the wallet mnemonics using the encryption key from ConnectionSettings.php
$iv = openssl_random_pseudo_bytes($iv_length);
$encryptedMnemonics = openssl_encrypt($walletMnemonics, $encryption_method, $encryption_key, 0, $iv);
$encryptedMnemonics = base64_encode($encryptedMnemonics . '::' . $iv);  // Encode to store easily

// Prepare the SQL statement to update the user's wallet information
$stmt = $conn->prepare("UPDATE users SET walletAddress = ?, walletBalance = ?, walletMnemonics = ? WHERE userId = ?");
$stmt->bind_param("siss", $walletAddress, $walletBalance, $encryptedMnemonics, $userId);

if ($stmt->execute()) {
    // Respond with a success message
    echo json_encode(array("status" => "success", "message" => "User wallet information updated successfully."));
} else {
    // Respond with an error message if the update fails
    echo json_encode(array("status" => "error", "message" => "Error updating user wallet information: " . $stmt->error));
}

// Close the statement and connection
$stmt->close();
$conn->close();

?>
