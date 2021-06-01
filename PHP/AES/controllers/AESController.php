<?php 
	include "models/AESModel.php";
	class AESController extends Controller{
		use AESModel;
		//ham mac dinh
		public function index(){
			//load view
			$this->loadView("MergeView.php");
		}

		public function encrypt(){
			$data = $this->modelEncrypt();
			header("location:home.php");
		}

		public function extract(){
			//load view
			$this->loadView("ExtractView.php");
		}

		public function decrypt(){
			$data = $this->modelDecrypt();
			$this->loadView("ResultView.php", ['data' => $data]);
		}
	}
 ?>