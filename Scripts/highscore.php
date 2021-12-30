<?php

//Insert the domain your game is hosted on into the array
$cors_whitelist = [
    'https://uploads.ungrounded.net'
];

if (in_array($_SERVER['HTTP_ORIGIN'], $cors_whitelist)) 
{
    header('Access-Control-Allow-Origin: ' . $_SERVER['HTTP_ORIGIN']);
}

// Define the database to connect to, $$$ in place of actuals
define("DBHOST", "$$$");
define("DBNAME", "$$$$");
define("DBUSER", "$$$$$");
define("DBPASS", "$$$$");

// Connect to the database
$connection = new mysqli(DBHOST, DBUSER, DBPASS, DBNAME);

// Other code goes here
// Check if the request is to retrieve or post a score to the leaderboard
if (isset($_POST['retrieve_leaderboard']))
{
    // Create the query string
    $sql = "SELECT * FROM HIGHSCORE ORDER BY score DESC limit 10";

    // Execute the query
    $result = $connection->query($sql);
    $num_results = $result->num_rows;

    // Loop through the results and print them out, using "\n" as a delimiter
    for ($i = 0; $i < $num_results; $i++) 
    {
        if (!($row = $result->fetch_assoc()))
            break;
        echo $row["name"];
        echo "\n";
        echo $row["score"];
        echo "\n";
    }

    $result->free_result();
}
elseif (isset($_POST['post_leaderboard']))
{
    // Get the user's name and store it
    $name = mysqli_escape_string($connection, $_POST['name']);
    $name = filter_var($name, FILTER_SANITIZE_STRING, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);
    // Get the user's score and store it
    $score = $_POST['score'];

    // Create prepared statement
    $statement = $connection->prepare("INSERT INTO HIGHSCORE (name, score) VALUES (?, ?)");
    $statement->bind_param("si", $name, $score);

    $statement->execute();
    $statement->close();
}

// Close the database connection
$connection->close();

?>
