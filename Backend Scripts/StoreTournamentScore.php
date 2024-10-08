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
$score = filter_input(INPUT_POST, 'score', FILTER_VALIDATE_INT);

// Validate required fields
if ($userId === null || $tournamentId === null || $score === null) {
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide valid userId, tournamentId, and score."));
    exit();
}

// Check if an entry with the same userId and tournamentId exists
$stmt = $conn->prepare("SELECT score FROM tournamentscores WHERE userId = ? AND tournamentId = ?");
$stmt->bind_param("ii", $userId, $tournamentId);
$stmt->execute();
$stmt->store_result();

// If the entry exists, update the score
if ($stmt->num_rows > 0) {
    $stmt->bind_result($existingScore);
    $stmt->fetch();
    
    // Add the new score to the existing score
    $newScore = $existingScore + $score;

    // Update the score in the database
    $updateStmt = $conn->prepare("UPDATE tournamentscores SET score = ? WHERE userId = ? AND tournamentId = ?");
    $updateStmt->bind_param("iii", $newScore, $userId, $tournamentId);

    if ($updateStmt->execute()) {
        echo json_encode(array("status" => "success", "message" => "Score updated successfully."));
    } else {
        echo json_encode(array("status" => "error", "message" => "Failed to update the score."));
    }

    $updateStmt->close();
} else {
    // If the entry does not exist, insert a new record
    $insertStmt = $conn->prepare("INSERT INTO tournamentscores (userId, tournamentId, score) VALUES (?, ?, ?)");
    $insertStmt->bind_param("iii", $userId, $tournamentId, $score);

    if ($insertStmt->execute()) {
        echo json_encode(array("status" => "success", "message" => "Score added successfully."));
    } else {
        echo json_encode(array("status" => "error", "message" => "Failed to insert the score."));
    }

    $insertStmt->close();
}

$stmt->close();
$conn->close();
?>
