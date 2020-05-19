<?php
	require_once("MyDB.php");
	main($db);


	function main($db)
	{

		if (isset($_GET["VID"]) && isset($_GET["UID"]))
		{
			header("content-type: application/jsonS");
			echo Connection($db, $_GET["VID"]);
		}
		return 0;
	}

	function Connection($db, $VID)
	{
		$sql = "SELECT DATA FROM sharedata WHERE VID = '$VID'";
				if ($db->query($sql) === FALSE) {
					echo("Error: " .  $db->error);
				}
			$result = $db -> query($sql);
			
		if ($result->num_rows > 0) return result;
		else return "false";
		// clear connected devices 
	}
?>