<?php

$servername = "localhost";
$username = "u779996992_kshitij_solana";
$password = "0mNQ0l!aJ4]";
$dbname = "u779996992_arcade_solana";

// $servername = "localhost";
// $username = "root";
// $password = "";
// $dbname = "aptoshackbackend";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Encryption key for AES-256-CBC encryption
$encryption_key = "d7a6bf51a8de77193b8ff9edb0c7174f0ad742edfa5cb9cf52f15a6d722c67e3"; // 256-bit key

?>