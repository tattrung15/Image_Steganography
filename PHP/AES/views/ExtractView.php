<!DOCTYPE html>
<html>
<head>
  <title>An toàn bảo mât thông tin - AES</title>
</head>
<body>
	<div style=" margin: auto; border: 2px solid green;width: 510px; margin-top: 50px; padding: 40px;">
	<div style="font-size: 18px; text-decoration: underline;font-weight: bold; margin-left:180px; padding-top: 20px; padding-bottom: 40px;">TÁCH TIN BẰNG AES</div>
<form method='post' action="home.php?controller=AES&action=decrypt" onsubmit="return notify();" >
  <div style="margin-bottom: 15px;">Khóa chuỗi mã hóa : <input type="text" name="password" required placeholder="Khóa chuỗi mã hóa"></div>
  <div style="color: red; font-size: 12px; margin-left: 15px;">* Định dạng file png</div>
  <div style="margin-bottom: 15px;"><input type="file" name="photo" id="photo" required required onchange="loadFile(event)"></div>
<img style="width: 320px;"id="output"/>
<script>
  var loadFile = function(event) {
    var output = document.getElementById('output');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function() {
      URL.revokeObjectURL(output.src) // free memory
    }
  };
</script>
  <input type="submit" value="TÁCH TIN" style="margin-left: 70px; margin-top: 15px; margin-bottom: 15px;">
</form>
<div style="margin-left: 150px;margin-top: 20px;"><a href="home.php">GIẤU TIN </a> | <a href="home.php?controller=AES&action=extract"> TÁCH TIN </a></div>
</div>
<script type="text/javascript">
	function notify() {
     var photo = document.getElementById('photo').value;
      var position = photo.indexOf('.png');
      if(position < 0 )
             {
                alert('File image phải có định dạng .png');
             }
      else
              {
                alert('Chờ tách tin')
              }
             
  }
</script>
<style type="text/css">
	input{padding: 7px 12px; width: 320px;}
	a{ font-weight: bold; color: black; text-decoration: none; font-size: 14px;}
	a:hover{text-decoration: underline; color: green;}
</style>
</body>
</html>