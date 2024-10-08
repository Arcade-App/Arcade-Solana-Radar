<?php
// Include the database connection settings
require 'ConnectionSettings.php';

// Check if the connection is successful
if ($conn->connect_error) {
    echo json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error));
    exit();
}

// Sanitize and validate inputs
$userId = filter_input(INPUT_POST, 'userId', FILTER_VALIDATE_INT);
$gameId = filter_input(INPUT_POST, 'gameId', FILTER_VALIDATE_INT);
$tournamentName = filter_input(INPUT_POST, 'tournamentName', FILTER_SANITIZE_STRING);
$tournamentHostName = filter_input(INPUT_POST, 'tournamentHostName', FILTER_SANITIZE_STRING);
$socialLink = filter_input(INPUT_POST, 'socialLink', FILTER_SANITIZE_STRING);
$playerJoiningFee = filter_var($_POST['playerJoiningFee'], FILTER_VALIDATE_FLOAT);  // Validate float directly
$startDate = filter_input(INPUT_POST, 'startDate', FILTER_VALIDATE_INT);
$startTime = filter_input(INPUT_POST, 'startTime', FILTER_VALIDATE_INT);
$endDate = filter_input(INPUT_POST, 'endDate', FILTER_VALIDATE_INT);
$endTime = filter_input(INPUT_POST, 'endTime', FILTER_VALIDATE_INT);
$prizePool = filter_var($_POST['prizePool'], FILTER_VALIDATE_FLOAT);  // Validate float directly
$status = filter_input(INPUT_POST, 'status', FILTER_VALIDATE_INT);
$playCount = filter_input(INPUT_POST, 'playCount', FILTER_VALIDATE_INT);
$userCount = filter_input(INPUT_POST, 'userCount', FILTER_VALIDATE_INT);

// Validate required fields
if ($userId === null || $gameId === null || empty($tournamentName) || empty($tournamentHostName) || empty($socialLink) || $playerJoiningFee === false || $startDate === null || $startTime === null || $endDate === null || $endTime === null || $prizePool === false || $status === null || $playCount === null || $userCount === null) {
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide valid tournament details."));
    exit();
}

// Prepare the SQL statement to insert the tournament data
$stmt = $conn->prepare("
    INSERT INTO tournaments 
    (userId, gameId, tournamentName, tournamentHostName, socialLink, playerJoiningFee, startDate, startTime, endDate, endTime, prizePool, status, playCount, userCount, createdAt) 
    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, UNIX_TIMESTAMP())");

if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

// Bind the parameters
$stmt->bind_param("iisssdiiiidiii", $userId, $gameId, $tournamentName, $tournamentHostName, $socialLink, $playerJoiningFee, $startDate, $startTime, $endDate, $endTime, $prizePool, $status, $playCount, $userCount);

if ($stmt->execute()) {
    // Respond with success message and the newly created tournament ID
    echo json_encode(array(
        "status" => "success",
        "message" => "Tournament data stored successfully",
        "tournamentId" => $conn->insert_id
    ));
} else {
    // Respond with an error message if the insert fails
    echo json_encode(array("status" => "error", "message" => "Error storing tournament data: " . $stmt->error));
}

// Close the statement and connection
$stmt->close();
$conn->close();
?>
