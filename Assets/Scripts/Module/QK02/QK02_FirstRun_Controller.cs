using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QK02_FirstRun_Controller : BasePageController {
	
	public GameObject uc02_2;
	public InputField newName;
	public GameObject  Top,Body,Logo;
	public GameObject loadingNew;
	public GameObject btnContinue;
	public GameObject ErrorPopup, PanelBlurPopUp;
	public Text lblContent;
	//public CameraController blur;

	void Start () {
		iOSKeyboardPatch.Apply ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void ChangeName(){
		string name = newName.text;
		if (name.Length > 5 && !name.Equals("tap here to input...")){
			BaseService.Instance.playerService.sendChangeName (name);
			BaseService.Instance.playerService.ChangeNameDelegate += checkChangeNameResponse;
			//uiblock.turnOnBlockUI ();
			btnContinue.GetComponent<Button> ().enabled = false;
		}
//		if(name.Length < 6){
//			blur.turnBlurOn();
//			ErrorPopup.SetActive(true);
//			lblContent.text = "Username too short. Username should contain at least 6 character(s)";
//		}
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
		this.DisplayScene (false);
		uc02_2.SetActive (true);
	}

	void OnEnable(){

		float time = 1.5f;
		iTween.MoveTo (Top, iTween.Hash ("position", new Vector3 (Top.transform.localPosition.x, 644, 0),"islocal",true, "time", time,"oncomplete", "none","oncompletetarget",gameObject));
		iTween.MoveTo (Body, iTween.Hash ("position", new Vector3 (Body.transform.localPosition.x, 0, 0),"islocal",true, "time", time,"oncomplete", "turnOn","oncompletetarget",gameObject));
		//iTween.MoveTo (Logo, iTween.Hash ("position", new Vector3 (0, 242.5f, 0),"islocal",true, "time", time,"oncomplete", "none","oncompletetarget",gameObject));

		iTween.ScaleTo (Top, iTween.Hash ("scale", new Vector3 (1f, 1f, 1f),"islocal",true, "time", time,"oncomplete", "none","oncompletetarget",gameObject));
		iTween.ScaleTo (Body, iTween.Hash ("scale", new Vector3 (1f, 1f, 1f),"islocal",true, "time", time,"oncomplete", "none","oncompletetarget",gameObject));
		btnContinue.GetComponent<Button> ().enabled = true;
	}

    void OnDisable() {
        loadingNew.SetActive(false);
    }

	void turnOn(){
        loadingNew.SetActive(false);
        Logo.SetActive (true);
	}
}
