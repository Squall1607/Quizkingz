using UnityEngine;
using System.Collections;

public class EmailConfirmation_Controller : BasePageController {

	public GameObject confirmation;
	public GameObject sendEmail;

	public GameObject btnSendNow;
	public GameObject btnReturn, PanelBlurPopUp;

	void OnEnable(){
        PanelBlurPopUp.SetActive(true);
        displayContent (sendEmail,confirmation);
		displayBtn (btnReturn, btnSendNow);
	}
    void OnDisable()
    {
       
        PanelBlurPopUp.SetActive(false);
    }

    public void OnClickSendNow(){
		displayContent (confirmation, sendEmail);
		displayBtn (btnSendNow,btnReturn);
	}

	void displayContent(GameObject obj1, GameObject obj2){
		obj1.SetActive (false);
		obj2.SetActive (true);
	}

	void displayBtn(GameObject obj1, GameObject obj2){
		obj1.SetActive (false);
		obj2.SetActive (true);
	}
}
