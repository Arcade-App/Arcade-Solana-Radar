<?php
// Include the database connection settings
require 'ConnectionSettings.php';

// Check if the connection is successful
if ($conn->connect_error) {
    echo json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error));
    exit();
}

// Variables submitted by users, sanitized
$userId = filter_input(INPUT_POST, 'userId', FILTER_VALIDATE_INT);
$templateId = filter_input(INPUT_POST, 'templateId', FILTER_VALIDATE_INT);
$faceId = filter_input(INPUT_POST, 'faceId', FILTER_VALIDATE_INT);
$backgroundId = filter_input(INPUT_POST, 'backgroundId', FILTER_VALIDATE_INT);
$jumpAudioId = filter_input(INPUT_POST, 'jumpAudioId', FILTER_VALIDATE_INT);
$backgroundAudioId = filter_input(INPUT_POST, 'backgroundAudioId', FILTER_VALIDATE_INT);
$gameOverAudioId = filter_input(INPUT_POST, 'gameOverAudioId', FILTER_VALIDATE_INT);
$gameName = filter_input(INPUT_POST, 'gameName', FILTER_SANITIZE_STRING);
$playCount = filter_input(INPUT_POST, 'playCount', FILTER_VALIDATE_INT);

// Validate required fields
if ($userId === null || $templateId === null  || $faceId === null  || $backgroundId === null  || $jumpAudioId === null  || $backgroundAudioId === null  || $gameOverAudioId === null  || empty($gameName) || $playCount === false) {
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide valid game details."));
    exit();
}

// Prepare the SQL statement to insert a new game record (gameId is auto_increment, so it's not included here)
$stmt = $conn->prepare("
    INSERT INTO games 
    (userId, templateId, faceId, backgroundId, jumpAudioId, backgroundAudioId, gameOverAudioId, gameName, playCount, createdAt)
    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, UNIX_TIMESTAMP())");

if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

// Bind the parameters
$stmt->bind_param("iiiiiiisi", $userId, $templateId, $faceId, $backgroundId, $jumpAudioId, $backgroundAudioId, $gameOverAudioId, $gameName, $playCount);

if ($stmt->execute()) {
    // Respond with success message and the newly created gameId
    echo json_encode(array(
        "status" => "success",
        "message" => "Game data stored successfully",
        "gameId" => $conn->insert_id
    ));
} else {
    // Respond with an error message if the insert fails
    echo json_encode(array("status" => "error", "message" => "Error storing game data: " . $stmt->error));
}

// Close the statement and connection
$stmt->close();
$conn->close();
?>
