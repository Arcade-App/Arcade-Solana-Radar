<?php
// Include the database connection settings
require 'ConnectionSettings.php';

// Check if the connection is successful
if ($conn->connect_error) {
    echo json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error));
    exit();
}

// Variables submitted by users, sanitized
$username = filter_input(INPUT_POST, 'username', FILTER_SANITIZE_STRING);
$email = filter_input(INPUT_POST, 'email', FILTER_VALIDATE_EMAIL);
$password = filter_input(INPUT_POST, 'password', FILTER_SANITIZE_STRING);

// Validate required fields
if (!$username || !$email || !$password) {
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide valid user details."));
    exit();
}

// Check if the username is already taken using a prepared statement
$stmt = $conn->prepare("SELECT username FROM users WHERE username = ?");
$stmt->bind_param("s", $username);
$stmt->execute();
$stmt->store_result();

if ($stmt->num_rows > 0) {
    // Username is already taken
    echo json_encode(array("status" => "error", "message" => "Username is already taken"));
    $stmt->close();
    exit();
}

// Close the check statement
$stmt->close();

// Hash the password securely
$hashedPassword = password_hash($password, PASSWORD_BCRYPT);

// Prepare the SQL statement to insert a new user
$stmt = $conn->prepare("INSERT INTO users (username, email, password, createdAt) VALUES (?, ?, ?, UNIX_TIMESTAMP())");
$stmt->bind_param("sss", $username, $email, $hashedPassword);

if ($stmt->execute()) {
    // Fetch the last inserted userId
    $userId = $conn->insert_id;

    // Respond with JSON including the new userId
    echo json_encode(array(
        "status" => "success",
        "message" => "New user created successfully",
        "userId" => $userId
    ));
} else {
    // Respond with an error message if the insert fails
    echo json_encode(array("status" => "error", "message" => "Error creating user: " . $stmt->error));
}

// Close the statement and connection
$stmt->close();
$conn->close();
?>

