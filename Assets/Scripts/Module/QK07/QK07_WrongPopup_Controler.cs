using UnityEngine;
using System.Collections;

public class QK07_WrongPopup_Controler : BasePageController {
    //public CameraController cam;
    public GameObject PanelBlurPopUp;

    void Start () {
	
	}

	void OnEnable(){
		//cam.turnBlurOn();
        PanelBlurPopUp.SetActive(true);

    }
	void OnDisable(){
		//cam.turnBlurOff();
        PanelBlurPopUp.SetActive(false);
    }

	void Update () {	
    }

}