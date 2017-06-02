using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QK02_ChangeNow_Controller : BasePageController {

	public InputField newName;
	public GameObject ErrorPopup;
	public Text lblContent;
	public GameObject btnDone;
	//public CameraController blur;
	public GameObject Qk02_WelcomeBack, PanelBlurPopUp;
	public Text txtName_Home;
	public Text txtNameWelcomeBack;

	void Update(){
		//blur.turnBlurOn();
	}
	void Start () {
		iOSKeyboardPatch.Apply ();
	}
	void OnEnable (){
        PanelBlurPopUp.SetActive(true);
        btnDone.GetComponent<Button> ().enabled = true;

	}
	public void ChangeName(){
		string name = newName.text;
		if (name.Length > 5 && !name.Equals("tap here to input...")){
			BaseService.Instance.playerService.sendChangeName (name);
			BaseService.Instance.playerService.ChangeNameDelegate += checkChangeNameResponse;
			btnDone.GetComponent<Button> ().enabled = false;
		}
		else{
            PanelBlurPopUp.SetActive(true);
            //blur.turnBlurOn();
			ErrorPopup.SetActive(true);
			lblContent.text = "Username too short. Username should contain at least 6 character(s)";
		}
	}
	void checkChangeNameResponse(bool status, string newname){
		if (status) {
			GameData.playerData.Display = newname;
		}
		txtName_Home.text = newName.text;
		txtNameWelcomeBack.text = newName.text;
		this.DisplayScene (false);
		Qk02_WelcomeBack.SetActive (true);
	}

    void OnDisable()
    {

        PanelBlurPopUp.SetActive(false);
    }
}
