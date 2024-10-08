
<?php
// Include the database connection settings
require 'ConnectionSettings.php';

// Check if the connection is successful
if ($conn->connect_error) {
    echo json_encode(array("status" => "error", "message" => "Database connection failed: " . $conn->connect_error));
    exit();
}

// Sanitize and validate inputs
$tournamentId = filter_input(INPUT_POST, 'tournamentId', FILTER_VALIDATE_INT);

// Validate required fields
if (!$tournamentId) {
    echo json_encode(array("status" => "error", "message" => "Invalid input. Please provide a valid tournamentId."));
    exit();
}

// Prepare the SQL statement to fetch current userCount, prizePool, and playerJoiningFee for the specified tournamentId
$stmt = $conn->prepare("SELECT userCount, prizePool, playerJoiningFee FROM tournaments WHERE tournamentId = ?");
if (!$stmt) {
    echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement."));
    exit();
}

$stmt->bind_param("i", $tournamentId);
$stmt->execute();
$stmt->bind_result($userCount, $prizePool, $playerJoiningFee);

// Check if the tournament entry exists
if ($stmt->fetch()) {
    // Increment userCount and increase prizePool by playerJoiningFee
    $newUserCount = $userCount + 1;
    $newPrizePool = $prizePool + $playerJoiningFee;

    // Close the previous statement
    $stmt->close();

    // Prepare the SQL statement to update userCount and prizePool
    $updateStmt = $conn->prepare("UPDATE tournaments SET userCount = ?, prizePool = ? WHERE tournamentId = ?");
    if (!$updateStmt) {
        echo json_encode(array("status" => "error", "message" => "Failed to prepare the SQL statement for update."));
        exit();
    }

    $updateStmt->bind_param("idi", $newUserCount, $newPrizePool, $tournamentId);

    if ($updateStmt->execute()) {
        // Respond with success message and the updated userCount and prizePool
        echo json_encode(array(
            "status" => "success",
            "message" => "User count and prize pool updated successfully.",
            "newUserCount" => $newUserCount,
            "newPrizePool" => $newPrizePool
        ));
    } else {
        echo json_encode(array("status" => "error", "message" => "Failed to update user count and prize pool."));
    }

    $updateStmt->close();
} else {
    echo json_encode(array("status" => "error", "message" => "Tournament not found for the provided tournamentId."));
}

$conn->close();
?>
