using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QK02A1_1_SignUp_Controller : BasePageController
{

	public InputField IFemail;
	public InputField IFpassword;
	public InputField IFconfirmPassword;
	public GameObject QK_SignUpDone;
	public GameObject ErrorPopup;
	public Text lblContent;
	public CameraController blur;
	private string email;
	public GameObject buttonSignUp;
	public GameObject belowSignup, PanelBlurPopUp;

	void Start () {
		iOSKeyboardPatch.Apply ();
	}

	public void btnSignUp ()
	{
		email = IFemail.text;
		string password = IFpassword.text;
		string confirmpassword = IFconfirmPassword.text;
		if (email.Length > 7 && !email.Equals ("tap here to input...") && password.Length > 6
		    && !password.Equals ("tap here to input...") && confirmpassword == password) {
			BaseService.Instance.playerService.sendSignUpSuccess (email, password);

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
		buttonSignUp.GetComponent<Button> ().enabled = true;
		if (PlayerPrefs.HasKey ("facebook")) {
			belowSignup.SetActive (false);
		} else {
			belowSignup.SetActive (true);
		}
	}

	void OnDisable() {
		BaseService.Instance.playerService.SignUpDelegate += null;
        PanelBlurPopUp.SetActive(false);
    }

	public void checkReponseSignUp (bool success)
	{
		if (success = true) {
			buttonSignUp.GetComponent<Button> ().enabled = false;
			this.DisplayScene (false);
			QK_SignUpDone.SetActive (true);
		}
	}
}
