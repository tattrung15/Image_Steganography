<?php if ($data == null): ?>
	 <script type="text/javascript">
  alert("Không có kết quả trả về");
  location.replace("home.php");
</script>
<?php else:  ?>
<!DOCTYPE html>
<html>
<head>
	<title>Kết quả</title>
</head>
<body>
<div style="margin-bottom: 17px; margin-top: 20px;"><b style="color: black; font-size: 15px; text-decoration: underline;">Kết quả trả về </b> : 
	<b style="color: green; font-size: 17px;"> <?php echo $data; ?></b></div>
	<a href="home.php">Quay về trang chủ</a>
</body>
</html>
<?php endif; ?>
