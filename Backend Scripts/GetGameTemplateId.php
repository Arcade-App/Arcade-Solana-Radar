<?php
// Include the database connection settings
require 'ConnectionSettings.php';

// Check if the connection is successful
if ($conn->connect_error) {
    echo json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error));
    exit();
}

// Sanitize and validate the input
$gameId = filter_input(INPUT_POST, 'gameId', FILTER_VALIDATE_INT);

// Validate required fields
if ($gameId  === null) {
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide a valid gameId."));
    exit();
}

// Prepare the SQL statement to fetch the templateId for the specified gameId
$stmt = $conn->prepare("SELECT templateId FROM games WHERE gameId = ?");

if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->bind_param("i", $gameId);
$stmt->execute();
$stmt->bind_result($templateId);

// Check if a record was found
if ($stmt->fetch()) {
    echo json_encode(array("status" => "success", "templateId" => $templateId));
} else {
    echo json_encode(array("status" => "error", "message" => "No game found for the provided gameId."));
}

$stmt->close();
$conn->close();
?>
