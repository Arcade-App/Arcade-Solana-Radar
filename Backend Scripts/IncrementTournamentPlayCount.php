<?php
// Include the database connection settings
require 'ConnectionSettings.php';

// Check if the connection is successful
if ($conn->connect_error) {
    echo json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error));
    exit();
}

// Sanitize and validate the input
$tournamentId = filter_input(INPUT_POST, 'tournamentId', FILTER_VALIDATE_INT);

// Validate required fields
if (!$tournamentId) {
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide a valid tournamentId."));
    exit();
}

// Prepare the SQL statement to fetch the current playCount
$stmt = $conn->prepare("SELECT playCount FROM tournaments WHERE tournamentId = ?");
if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->bind_param("i", $tournamentId);
$stmt->execute();
$stmt->bind_result($playCount);

// Check if the record was found
if ($stmt->fetch()) {
    $stmt->close();
    
    // Increment the playCount by 1
    $newPlayCount = $playCount + 1;
    
    // Update the playCount in the database
    $updateStmt = $conn->prepare("UPDATE tournaments SET playCount = ? WHERE tournamentId = ?");
    if (!$updateStmt) {
        echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement for update."));
        exit();
    }
    
    $updateStmt->bind_param("ii", $newPlayCount, $tournamentId);
    if ($updateStmt->execute()) {
        echo json_encode(array("status" => "success", "message" => "Play count updated successfully.", "newPlayCount" => $newPlayCount));
    } else {
        echo json_encode(array("status" => "error", "message" => "Failed to update play count."));
    }
    
    $updateStmt->close();
} else {
    echo json_encode(array("status" => "error", "message" => "No tournament found for the provided tournamentId."));
    $stmt->close();
}

$conn->close();
?>
