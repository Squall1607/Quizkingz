using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tuto_AddEmail_Controller : BasePageController {

	public InputField IFemail;
	public InputField IFpassword;
	public InputField IFconfirmPassword;
	public GameObject Tuto_Account;
	public GameObject ErrorPopup;
	public Text lblContent;
	//public CameraController blur;
	private string email;
	public GameObject buttonAddEmail;
	public GameObject btnAddEmail_Account;
	public GameObject btnComfirmEmail_Account, PanelBlurPopUp;

	void Start () {
		iOSKeyboardPatch.Apply ();
	}

	public void btnAddEmail ()
	{
		email = IFemail.text;
		string password = IFpassword.text;
		string confirmpassword = IFconfirmPassword.text;
		if (email.Length > 7 && !email.Equals ("tap here to input...") && password.Length > 6
			&& !password.Equals ("tap here to input...") && confirmpassword == password) {
			BaseService.Instance.playerService.sendSignUpSuccess (email, password);
	//		BaseService.Instance.playerService.SignUpDelegate += checkReponseSignUp;
		} else {
            //blur.turnBlurOn ();
            PanelBlurPopUp.SetActive(true);

            ErrorPopup.SetActive (true);
			lblContent.text = "Please, check email,password";
		}
	}
	void OnEnable ()
	{
		BaseService.Instance.playerService.SignUpDelegate += checkReponseSignUp;
		buttonAddEmail.GetComponent<Button> ().enabled = true;
	}

	void OnDisable() {
		BaseService.Instance.playerService.SignUpDelegate += null;
	}

	public void checkReponseSignUp (bool success)
	{
		if (success = true) {
			buttonAddEmail.GetComponent<Button> ().enabled = false;
			btnAddEmail_Account.SetActive (false);
			btnComfirmEmail_Account.SetActive (true);
			this.DisplayScene (false);
			Tuto_Account.SetActive (true);
		}
	}
}
