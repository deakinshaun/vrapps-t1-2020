<!doctype html>
<html>
<head>
<meta charset="utf-8">
<title>Swing Viewer</title>
<style type="text/css">
#btn strong {
    font-family: Lucida Grande, Lucida Sans Unicode, Lucida Sans, DejaVu Sans, Verdana, sans-serif;
}
</style>
</head>
	<meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
<body>
	
<div class="jumbotron text-center"><h1>View My Swing
	</h1></div>
	<?php if (isset($_GET["VID"])) :?>
	<button class="btn-primary" style="width: 250px; height: 75px" id="btn" onclick="myFunction()"><strong>Click to View!</strong></button>

<script>
		function myFunction() {
		  location.replace("golfswingandroid://data?VID=<?php echo($_GET["VID"]) ?>")
		}
	</script>	
	<?php endif; ?>
</body>
</html>
