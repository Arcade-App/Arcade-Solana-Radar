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

// Validate required fields
if (!$userId || !$gameId) {
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide valid userId and gameId."));
    exit();
}

// Check if a score entry exists for the given userId and gameId
$stmt = $conn->prepare("SELECT score FROM gamescores WHERE userId = ? AND gameId = ?");
if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->bind_param("ii", $userId, $gameId);
$stmt->execute();
$stmt->bind_result($score);

// Check if an entry was found
if ($stmt->fetch()) {
    // Entry found, return the existing score
    echo json_encode(array("status" => "success", "score" => $score));
    $stmt->close();  // Close the statement
} else {
    // No entry found, close the current statement and create a new entry
    $stmt->close();  // Close the original statement before inserting

    $insertStmt = $conn->prepare("INSERT INTO gamescores (userId, gameId, score) VALUES (?, ?, 0)");
    if (!$insertStmt) {
        echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement for insert."));
        exit();
    }

    $insertStmt->bind_param("ii", $userId, $gameId);
    if ($insertStmt->execute()) {
        // Return the newly inserted score of 0
        echo json_encode(array("status" => "success", "score" => 0));
    } else {
        echo json_encode(array("status" => "error", "message" => "Error creating a new score entry."));
    }
    $insertStmt->close();  // Close the insert statement
}

$conn->close();  // Close the connection
?>
