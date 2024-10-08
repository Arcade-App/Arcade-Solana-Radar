<?php
// Include the database connection settings
require 'ConnectionSettings.php';

// Check if the connection is successful
if ($conn->connect_error) {
    echo json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error));
    exit();
}

// We are looking for tournamentManagerId = 1
$tournamentManagerId = 1;

// Prepare the SQL statement to fetch tournament manager data
$stmt = $conn->prepare("SELECT tournamentManagerAddress, contractName FROM tournamentmanager WHERE tournamentManagerId = ?");
if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

// Bind the tournamentManagerId
$stmt->bind_param("i", $tournamentManagerId);
$stmt->execute();
$stmt->store_result();

// Check if data was found
if ($stmt->num_rows > 0) {
    $stmt->bind_result($tournamentManagerAddress, $contractName);
    $stmt->fetch();

    // Return the result in JSON format
    echo json_encode(array(
        "status" => "success",
        "tournamentManagerAddress" => $tournamentManagerAddress,
        "contractName" => $contractName
    ));
} else {
    echo json_encode(array("status" => "error", "message" => "No data found for tournamentManagerId = 1"));
}

// Close the statement and connection
$stmt->close();
$conn->close();
?>
