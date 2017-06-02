using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tuto_feedBack_Controller : BasePageController {

	public InputField IFmessage;
	public InputField txtName;
	public InputField email;
	public GameObject buttonSubmit;
	public GameObject tutorial;
	public Text lblContent;
	//public CameraController blur;
	public GameObject ErrorPopup, PanelBlurPopUp;

	void Start () {
		iOSKeyboardPatch.Apply ();
	}

	void OnEnable()
	{
		txtName.text = GameData.playerData.Display;
		email.text = GameData.playerData.Email;
		buttonSubmit.GetComponent<Button> ().enabled = true;
	}
	public void btnSubmit()
	{
		string message = IFmessage.text;
		if (message.Length > 1 && !message.Equals ("tap here to input...")) {
			BaseService.Instance.playerService.sendFeedBack (message);
			BaseService.Instance.playerService.FeedBackDelegate += checkResponseFeedBack;
		} else {
            //blur.turnBlurOn();
            PanelBlurPopUp.SetActive(true);

            ErrorPopup.SetActive(true);
			lblContent.text = "Please, check feedback";
		}
	}
	void checkResponseFeedBack(bool success)
	{
		if (success = true) {
			Debug.Log ("true");
			buttonSubmit.GetComponent<Button> ().enabled = false;
			this.DisplayScene (false);
			tutorial.SetActive (true);
		} else {
			Debug.Log ("false");
		}
	}
}
