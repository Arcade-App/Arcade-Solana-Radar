<?php
// Include the database connection settings
require 'ConnectionSettings.php';

// Check if the connection is successful
if ($conn->connect_error) {
    echo json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error));
    exit();
}

// Sanitize and validate the inputs
$userId = filter_input(INPUT_POST, 'userId', FILTER_VALIDATE_INT);
$gameId = filter_input(INPUT_POST, 'gameId', FILTER_VALIDATE_INT);
$newScore = filter_input(INPUT_POST, 'score', FILTER_VALIDATE_INT);

// Validate required fields
if (!$userId || !$gameId || $newScore === null) {  // Check if score is explicitly provided
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide valid userId, gameId, and score."));
    exit();
}

// Prepare the SQL statement to check if an entry for the userId and gameId exists
$stmt = $conn->prepare("SELECT score FROM gamescores WHERE userId = ? AND gameId = ?");
if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->bind_param("ii", $userId, $gameId);
$stmt->execute();
$stmt->bind_result($currentScore);

// Check if an entry exists
if ($stmt->fetch()) {
    // If entry exists, update the score with the new score
    $stmt->close();
    $updateStmt = $conn->prepare("UPDATE gamescores SET score = ? WHERE userId = ? AND gameId = ?");
    if (!$updateStmt) {
        echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement for update."));
        exit();
    }

    $updateStmt->bind_param("iii", $newScore, $userId, $gameId);
    if ($updateStmt->execute()) {
        echo json_encode(array("status" => "success", "message" => "Score updated successfully.", "newScore" => $newScore));
    } else {
        echo json_encode(array("status" => "error", "message" => "Failed to update the score."));
    }
    $updateStmt->close();
} else {
    // If no entry exists, create a new one with the given score
    $stmt->close();
    $insertStmt = $conn->prepare("INSERT INTO gamescores (userId, gameId, score) VALUES (?, ?, ?)");
    if (!$insertStmt) {
        echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement for insert."));
        exit();
    }

    $insertStmt->bind_param("iii", $userId, $gameId, $newScore);
    if ($insertStmt->execute()) {
        echo json_encode(array("status" => "success", "message" => "Score added successfully.", "newScore" => $newScore));
    } else {
        echo json_encode(array("status" => "error", "message" => "Failed to add the new score."));
    }
    $insertStmt->close();
}

$conn->close();
?>
