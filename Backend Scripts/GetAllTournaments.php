<?php
// Include the database connection settings
require 'ConnectionSettings.php';

// Check if the connection is successful
if ($conn->connect_error) {
    echo json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error));
    exit();
}

// Prepare the SQL statement to fetch all tournaments, including userId
$stmt = $conn->prepare("
    SELECT userId, tournamentId, gameId, tournamentName, tournamentHostName, socialLink, playerJoiningFee, startDate, startTime, endDate, endTime, prizePool, status, playCount, userCount, createdAt, winnerId, runnerUpId, secondRunnerUpId
    FROM tournaments");

if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->execute();
$result = $stmt->get_result();

$tournaments = array();  // Initialize an empty array for tournaments

// Check if there are any tournaments
if ($result->num_rows > 0) {
    // Fetch all tournaments
    while ($row = $result->fetch_assoc()) {
        $tournaments[] = array(
            "userId" => $row['userId'],
            "tournamentId" => $row['tournamentId'],
            "gameId" => $row['gameId'],
            "tournamentName" => $row['tournamentName'],
            "tournamentHostName" => $row['tournamentHostName'],
            "socialLink" => $row['socialLink'],
            "playerJoiningFee" => $row['playerJoiningFee'],
            "startDate" => $row['startDate'],
            "startTime" => $row['startTime'],
            "endDate" => $row['endDate'],
            "endTime" => $row['endTime'],
            "prizePool" => $row['prizePool'],
            "status" => $row['status'],
            "playCount" => $row['playCount'],
            "userCount" => $row['userCount'],
            "createdAt" => $row['createdAt'],
            "winnerId" => $row['winnerId'],
            "runnerUpId" => $row['runnerUpId'],
            "secondRunnerUpId" => $row['secondRunnerUpId']
        );
    }
}

// Always return success with the tournaments array, even if it's empty
echo json_encode(array("status" => "success", "tournaments" => $tournaments));

$stmt->close();
$conn->close();
?>
