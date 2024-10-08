<?php
// Include the database connection settings
require 'ConnectionSettings.php';

// Check if the connection is successful
if ($conn->connect_error) {
    echo json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error));
    exit();
}

// Sanitize and validate the input
$userId = filter_input(INPUT_POST, 'userId', FILTER_VALIDATE_INT);
$tournamentId = filter_input(INPUT_POST, 'tournamentId', FILTER_VALIDATE_INT);

// Validate required fields
if (!$userId || !$tournamentId) {
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide valid userId and tournamentId."));
    exit();
}

// Prepare the SQL statement to check if an entry for the userId and tournamentId exists
$stmt = $conn->prepare("SELECT score FROM tournamentscores WHERE userId = ? AND tournamentId = ?");
if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->bind_param("ii", $userId, $tournamentId);
$stmt->execute();
$stmt->bind_result($score);

// Check if an entry was found
if ($stmt->fetch()) {
    // Entry found, return the existing score
    echo json_encode(array("status" => "success", "score" => $score));
    $stmt->close();
} else {
    // No entry found, create one with score = 0
    $stmt->close();  // Close the original statement

    $insertStmt = $conn->prepare("INSERT INTO tournamentscores (userId, tournamentId, score) VALUES (?, ?, 0)");
    if (!$insertStmt) {
        echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement for insert."));
        exit();
    }

    $insertStmt->bind_param("ii", $userId, $tournamentId);
    if ($insertStmt->execute()) {
        // Return the newly inserted score of 0
        echo json_encode(array("status" => "success", "score" => 0));
    } else {
        echo json_encode(array("status" => "error", "message" => "Error creating a new score entry."));
    }
    $insertStmt->close();
}

$conn->close();
?>
