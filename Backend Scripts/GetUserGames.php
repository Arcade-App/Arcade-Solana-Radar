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

// Prepare the SQL statement to fetch game data for the specified user
$stmt = $conn->prepare("
    SELECT gameId, templateId, faceId, backgroundId, jumpAudioId, backgroundAudioId, gameOverAudioId, gameName, playCount
    FROM games
    WHERE userId = ?");

if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->bind_param("i", $userId);
$stmt->execute();
$result = $stmt->get_result();

$games = array();  // Initialize an empty array for games

// Check if the user has any games
if ($result->num_rows > 0) {
    // Fetch all the user's games
    while ($row = $result->fetch_assoc()) {
        $games[] = array(
            "gameId" => $row['gameId'],
            "templateId" => $row['templateId'],
            "faceId" => $row['faceId'],
            "backgroundId" => $row['backgroundId'],
            "jumpAudioId" => $row['jumpAudioId'],
            "backgroundAudioId" => $row['backgroundAudioId'],
            "gameOverAudioId" => $row['gameOverAudioId'],
            "gameName" => $row['gameName'],
            "playCount" => $row['playCount']
        );
    }
}

// Always return success with the games array, even if it's empty
echo json_encode(array("status" => "success", "games" => $games));

$stmt->close();
$conn->close();
?>
