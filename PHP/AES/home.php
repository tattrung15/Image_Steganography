<?php 
	//start session
	session_start();
	//load file Controller.php
	include "../app/Controller.php";
 ?>
 

 <?php 
 	//lay bien controller truyen tu url
 	//index.php?controller=PhongBan&action=listPhongBan
 	$controller = isset($_GET["controller"]) ? $_GET["controller"] : "AES";
 		//-> $controller = PhongBan
 	//ghep chuoi de thanh ten class
 	$classController = $controller."Controller";
 		//->$classController = PhongBanController
 	//ghep chuoi de thanh ten file
 	$fileController = "controllers/".$controller."Controller.php";
 		//-> $fileController =  controllers/PhongBanController.php		
 	$action = isset($_GET["action"]) ? $_GET["action"] : "index";
 		//$action = listPhongBan 
 	//---
 	//include file controller
 	if(file_exists($fileController)){
 		include $fileController;
 		$obj = new $classController();
 		$obj->$action();
 	}
 	//---

  ?>