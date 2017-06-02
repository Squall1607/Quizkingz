using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QK03_3_MatchStyle_Controller : BasePageController {

	//public BlockUIController blockUI;
	public CameraController cam;
    //public SwichLoadingController switchLoading;
    public GameObject shield, sword;
	public GameObject quest, battle;
	public GameObject UIMain;
	public List<GameObject> lstActive;
	//public GameObject bgClicked;
	public GameObject bgClickedPopup;

	public GameObject navbarPopup;
    public GameObject blockUI, PanelBlur;

	public override void SceneSwitch(){

		if(gameObject.activeSelf){
            //cam.turnBlurOff ();
            PanelBlur.SetActive(false);
            UIMain.GetComponent<Button> ().enabled = false;
			quest.GetComponent<Button> ().enabled = false;
			battle.GetComponent<Button> ().enabled = false;

			iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(0, -349, 0), "islocal", true, "time", 1.0f,
				"oncomplete", "downCompleted","oncompletetarget",gameObject));
			blockUI.SetActive (false);

		}else{
            //cam.turnBlurOn ();
            PanelBlur.SetActive(true);
            gameObject.SetActive(true);
			quest.GetComponent<Button> ().enabled = false;
			battle.GetComponent<Button> ().enabled = false;
			UIMain.GetComponent<Button> ().enabled = false;

			iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(0, 7, 0), "islocal", true, "time", 1.0f, 
				"oncomplete", "upCompleted","oncompletetarget",gameObject));
			blockUI.SetActive (true);
		}
	}

	void OnDisable(){
		blockUI.SetActive (false);
		gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x,-349,gameObject.transform.localPosition.z);

	}

    public void down()
    {
        //cam.turnBlurOff ();
        PanelBlur.SetActive(false);
        UIMain.GetComponent<Button>().enabled = false;
        quest.GetComponent<Button>().enabled = false;
        battle.GetComponent<Button>().enabled = false;

        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(0, -349, 0), "islocal", true, "time", 1.0f,
            "oncomplete", "downCompleted", "oncompletetarget", gameObject));
        blockUI.SetActive(false);
    }

    public void up()
    {
        PanelBlur.SetActive(true);
        gameObject.SetActive(true);
        quest.GetComponent<Button>().enabled = false;
        battle.GetComponent<Button>().enabled = false;
        UIMain.GetComponent<Button>().enabled = false;

        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(0, 7, 0), "islocal", true, "time", 1.0f,
            "oncomplete", "upCompleted", "oncompletetarget", gameObject));
        blockUI.SetActive(true);
    }

    private void downCompleted(){
		gameObject.SetActive(false);
		UIMain.GetComponent<Button> ().enabled = true;
		battle.GetComponent<Button> ().enabled = true;
		quest.GetComponent<Button> ().enabled = true;

		//for (int i = 0; i < lstActive.Count; i++) {
		//	if (lstActive[i].activeSelf) {

  //              //if (navbar.activeSelf) {
  //              //                //navbar.GetComponent<NavigationBarController>().clickM();
  //              //                navbar.GetComponent<NavigationBarController> ().clicked2 (i,bgClicked);

  //              //} else if (navbarPopup.activeSelf) {
  //              //                //Debug.Log (i);
  //              //                //navbarPopup.GetComponent<NavigationBarController>().clickM();   
  //              //                navbarPopup.GetComponent<NavigationBarController> ().clicked2 (i,bgClickedPopup);

  //              //}
  //              navbarPopup.GetComponent<NavigationBarController>().clicked2(i, bgClickedPopup);
  //          }
		//}

	}

	private void upCompleted(){
		//gameObject.SetActive(false);
		UIMain.GetComponent<Button> ().enabled = true;
		battle.GetComponent<Button> ().enabled = true;
		quest.GetComponent<Button> ().enabled = true;
		//blockUI.turnOffBlockUI();
	}

	public void switchShield(){
		navbarPopup.SetActive (false);
        //navbar.SetActive (true);
        PanelBlur.SetActive(false);
        shield.SetActive (true);
		gameObject.SetActive(false);

		BaseService.Instance.gameService.SendTips ();
	}

	public void switchSword(){
		navbarPopup.SetActive (false);
        //navbar.SetActive (true);
        PanelBlur.SetActive(false);
        sword.SetActive (true);
		gameObject.SetActive(false);


	}


}
