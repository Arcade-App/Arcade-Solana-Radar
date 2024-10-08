<?php
// Include the database connection settings
require 'ConnectionSettings.php';

// Check if the connection is successful
if ($conn->connect_error) {
    echo json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error));
    exit();
}

// Prepare the SQL statement to fetch the top 30 games, ordered by playCount
$stmt = $conn->prepare("
    SELECT gameId, userId, templateId, faceId, backgroundId, jumpAudioId, backgroundAudioId, gameOverAudioId, gameName, playCount
    FROM games
    ORDER BY playCount DESC
    LIMIT 30");

if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->execute();
$result = $stmt->get_result();

$games = array();  // Initialize an empty array for games

// Check if there are any games
if ($result->num_rows > 0) {
    // Fetch all games
    while ($row = $result->fetch_assoc()) {
        $games[] = array(
            "gameId" => $row['gameId'],
            "userId" => $row['userId'],
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
