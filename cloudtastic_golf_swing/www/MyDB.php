<?php

	$host='localhost';   			// Host name of MySQL server
	$dbUser='newcarav_sit383';    			// User name for MySQL
	$dbPass='ThisIsThePassword4';     // Password for user
	$dbName='newcarav_sit383'; 		  		// Database name

	$db = new mysqli($host, $dbUser, $dbPass, $dbName);
	
	if($db->connect_error){
		die("Connection Failed: " . $db->connect_error);
	}
?>