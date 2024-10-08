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

// Validate required fields
if (!$userId) {
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide a valid userId."));
    exit();
}

// Prepare the SQL statement to fetch tournament details from both tournaments and tournamentscores tables
$stmt = $conn->prepare("
    SELECT t.tournamentId, t.gameId, t.tournamentName, t.tournamentHostName, t.socialLink, t.playerJoiningFee, t.startDate, t.startTime, t.endDate, t.endTime, t.prizePool, t.status, t.playCount, t.userCount, t.winnerId, t.runnerUpId, t.secondRunnerUpId, ts.score
    FROM tournaments t
    JOIN tournamentscores ts ON t.tournamentId = ts.tournamentId
    WHERE ts.userId = ?");

if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->bind_param("i", $userId);
$stmt->execute();
$result = $stmt->get_result();

$tournaments = array();  // Initialize an empty array for tournaments

// Check if the user has any tournament data
if ($result->num_rows > 0) {
    // Fetch all tournaments with score details for the user
    while ($row = $result->fetch_assoc()) {
        $tournaments[] = array(
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
            "winnerId" => $row['winnerId'],
            "runnerUpId" => $row['runnerUpId'],
            "secondRunnerUpId" => $row['secondRunnerUpId'],
            "score" => $row['score']
        );
    }
}

// Always return success with the tournaments array, even if it's empty
echo json_encode(array("status" => "success", "tournaments" => $tournaments));

$stmt->close();
$conn->close();
?>
