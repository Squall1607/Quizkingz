﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QK02A2_2_SignInComplete_Controller : BasePageController {

	public InputField newName;
	public GameObject ErrorPopup;
	public Text lblContent;
	public GameObject btnContinue;
	public CameraController blur;
	public GameObject QK02_WelcomeBack;
	public GameObject QK03_Home, PanelBlurPopUp;
	//public GameObject NavBar;


	void Start () {
		iOSKeyboardPatch.Apply ();
	}

	public void ChangeName(){
		string name = newName.text;
		if (name.Length > 5 && !name.Equals("tap here to input...")){
			BaseService.Instance.playerService.sendChangeName (name);
			BaseService.Instance.playerService.ChangeNameDelegate += checkChangeNameResponse;
			btnContinue.GetComponent<Button> ().enabled = false;
		}
		else{
			//blur.turnBlurOn();
            PanelBlurPopUp.SetActive(true);
            ErrorPopup.SetActive(true);
			lblContent.text = "Username too short. Username should contain at least 6 character(s)";
		}
	}
	void checkChangeNameResponse(bool status, string newname){
		if (status) {
			GameData.playerData.Display = newname;
		}
		//SceneSwitch ();
		//blur.turnBlurOn();
        PanelBlurPopUp.SetActive(true);
        QK02_WelcomeBack.SetActive (true);
		QK03_Home.SetActive (true);
		//NavBar.SetActive (true);
	}
	public void btnSKIP()
	{
        //		SceneSwitch ();
        //blur.turnBlurOn();
        PanelBlurPopUp.SetActive(true);
        PanelBlurPopUp.SetActive(true);
        QK02_WelcomeBack.SetActive (true);
		QK03_Home.SetActive (true);
		//NavBar.SetActive (true);
	}
}
