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

// Prepare the SQL statement to fetch userId, username, and score for the given tournamentId, ordered by score in descending order
$stmt = $conn->prepare("
    SELECT ts.userId, u.username, ts.score
    FROM tournamentscores ts
    JOIN users u ON ts.userId = u.userId
    WHERE ts.tournamentId = ?
    ORDER BY ts.score DESC");  // Ordering by score in descending order

if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->bind_param("i", $tournamentId);
$stmt->execute();
$result = $stmt->get_result();

$tournamentScores = array();  // Initialize an empty array for tournament scores

// Check if there are any tournament scores
if ($result->num_rows > 0) {
    // Fetch all userId, username, and score for the given tournament
    while ($row = $result->fetch_assoc()) {
        $tournamentScores[] = array(
            "userId" => $row['userId'],
            "username" => $row['username'],
            "score" => $row['score']
        );
    }
}

// Always return success with the tournamentScores array, even if it's empty
echo json_encode(array("status" => "success", "tournamentScores" => $tournamentScores));

$stmt->close();
$conn->close();
?>
