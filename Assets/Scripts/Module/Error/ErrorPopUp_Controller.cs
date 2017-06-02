using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ErrorPopUp_Controller : MonoBehaviour {
	//public CameraController blur;


	public GameObject lblHeader;
	public Text lblContent;
	public GameObject lblBtnOk, PanelBlurPopUp;

	public List<GameObject> listPopup;
	public static bool isDisconnect = false;

	public void turnOn(string msg){

		//blur.turnBlurOn();
        PanelBlurPopUp.SetActive(true);
        lblContent.text = msg;
		gameObject.SetActive(true);
	}

	public void turnOff(){
        //Debug.Log ("aaaaaaaaaaaaaaaaaaaaaaaaa");
        PanelBlurPopUp.SetActive(false);
        //blur.turnBlurOff();
		gameObject.SetActive(false);
		if (lblContent.text == "Disconnected from server") {
			
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);

		}
	}

	public void turnOnDisconnect(){
		//blur.turnBlurOn();
        PanelBlurPopUp.SetActive(true);
        lblContent.text = "Disconnected from server";
		gameObject.SetActive(true);
	}

	public void turnOnLogOut(){
        //blur.turnBlurOn ();
        PanelBlurPopUp.SetActive(true);
        lblContent.text = "You've already logged out!";
		gameObject.SetActive (true);
	}
}
