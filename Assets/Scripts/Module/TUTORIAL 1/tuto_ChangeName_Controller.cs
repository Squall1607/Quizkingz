using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tuto_ChangeName_Controller : BasePageController {

	public InputField newName;
	public GameObject ErrorPopup;
	public Text lblContent;
	public GameObject btnSave;
	public CameraController blur;
	public GameObject tuto_Account, PanelBlurPopUp;
	public Text txtName_Home;
	public Text txtNameAccount;

	void Start () {
		iOSKeyboardPatch.Apply ();
	}
	public void ChangeName(){
		string name = newName.text;
		if (name.Length > 5 && !name.Equals("tap here to input...")){
			BaseService.Instance.playerService.sendChangeName (name);
			BaseService.Instance.playerService.ChangeNameDelegate += checkChangeNameResponse;
			btnSave.GetComponent<Button> ().enabled = false;
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
		txtNameAccount.text = newName.text;
		this.DisplayScene (false);
		tuto_Account.SetActive (true);
	}
}
