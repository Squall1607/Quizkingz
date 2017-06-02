using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QK02_ChangeName_Controller : BasePageController {

	public InputField newName;
	public GameObject ErrorPopup;
	public Text lblContent;
	public GameObject btnContinue;
	//public CameraController blur;
	public GameObject Qk02_WelcomeToTheGame, PanelBlurPopUp;
	public Text txtName_Home;

	void Update(){
		//blur.turnBlurOn();
        //PanelBlurPopUp.SetActive(true);
    }
	void Start () {
		iOSKeyboardPatch.Apply ();
	}

    void OnEnable()
    {

        PanelBlurPopUp.SetActive(true);
    }

    void OnDisable()
    {

        PanelBlurPopUp.SetActive(false);
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
		txtName_Home.text = newName.text;
		this.DisplayScene (false);
		Qk02_WelcomeToTheGame.SetActive (true);
	}
}
