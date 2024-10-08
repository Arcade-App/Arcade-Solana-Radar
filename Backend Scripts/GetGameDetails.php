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
if (!$gameId) {
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide a valid gameId."));
    exit();
}

// Prepare the SQL statement to fetch the required fields for the specified gameId
$stmt = $conn->prepare("
    SELECT templateId, faceId, backgroundId, jumpAudioId, backgroundAudioId, gameOverAudioId, gameName
    FROM games
    WHERE gameId = ?
");

if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->bind_param("i", $gameId);
$stmt->execute();
$stmt->bind_result($templateId, $faceId, $backgroundId, $jumpAudioId, $backgroundAudioId, $gameOverAudioId, $gameName);

// Check if a record was found
if ($stmt->fetch()) {
    // Return the game details as a JSON response
    echo json_encode(array(
        "status" => "success",
        "templateId" => $templateId,
        "faceId" => $faceId,
        "backgroundId" => $backgroundId,
        "jumpAudioId" => $jumpAudioId,
        "backgroundAudioId" => $backgroundAudioId,
        "gameOverAudioId" => $gameOverAudioId,
        "gameName" => $gameName
    ));
} else {
    echo json_encode(array("status" => "error", "message" => "No game found for the provided gameId."));
}

$stmt->close();
$conn->close();
?>
