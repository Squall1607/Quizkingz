using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

public class CameraController : MonoBehaviour {

	public bool isEnabled = false;
	//public GameObject navbar;
	//public GameObject navbarPopup;
	//public List<GameObject> lstActive;
	//public GameObject bgClicked;
	//public GameObject bgClickedPopup;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CameraBlur_Switch(){
		if(gameObject.GetComponent<Blur>().enabled){
			gameObject.GetComponent<Blur>().enabled = false;
			isEnabled = false;
		}else {
			gameObject.GetComponent<Blur>().enabled = true;
			isEnabled = true;
		}
	}

	//public void turnBlurOn(){
	//	gameObject.GetComponent<Blur>().enabled = true;
	//	//isEnabled = true;

	//}

	//public void turnBlurOff(){
	//	gameObject.GetComponent<Blur>().enabled = false;
	//	//isEnabled = false;
	//}
}
