<?php
	require_once("MyDB.php");
	main($db);


	function main($db)
	{

		if (isset($_GET["VID"]) && isset($_GET["UID"]))
		{
			header("content-type: application/json");
			echo getData($db, $_GET["VID"]);
		}
		if (isset($_GET["DATA"]))
		{
			if (!isset($_GET["TYPE"])) $_GET["TYPE"] = "PLAYER";
			
			setData($db,  $_GET["DATA"], $_GET["TYPE"]);
		}
		return 0;
	}

	function getData($db, $VID)
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
	function setData($db, $data, $type)
	{
		$str=rand(); 
		$id = md5($str); 
		$sql = "INSERT INTO sharedata (VID, UID, TYPE, DATA) VALUES ('$id', '11ute4', '$data'";


				if ($db->query($sql) === FALSE) {
					echo("Error: " .  $db->error);
				}
		// clear connected devices 
	}
?>