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

// Prepare the SQL statement to fetch the current playCount
$stmt = $conn->prepare("SELECT playCount FROM games WHERE gameId = ?");
if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->bind_param("i", $gameId);
$stmt->execute();
$stmt->bind_result($playCount);

// Check if a game entry was found
if ($stmt->fetch()) {
    $stmt->close();
    
    // Increment the playCount by 1
    $newPlayCount = $playCount + 1;
    
    // Update the playCount in the database
    $updateStmt = $conn->prepare("UPDATE games SET playCount = ? WHERE gameId = ?");
    if (!$updateStmt) {
        echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement for update."));
        exit();
    }
    
    $updateStmt->bind_param("ii", $newPlayCount, $gameId);
    if ($updateStmt->execute()) {
        echo json_encode(array("status" => "success", "message" => "Play count updated successfully.", "newPlayCount" => $newPlayCount));
    } else {
        echo json_encode(array("status" => "error", "message" => "Failed to update play count."));
    }
    
    $updateStmt->close();
} else {
    echo json_encode(array("status" => "error", "message" => "Game not found for the provided gameId."));
    $stmt->close();
}

$conn->close();
?>
