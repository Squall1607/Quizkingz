using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QK02A2_1_SignIn_Controller : BasePageController {

	public InputField IFemail;
	public InputField IFpassword;
	public GameObject ErrorPopup;
	public GameObject QK_LoginDone;
	public Text lblContent;
	//public CameraController blur;
	private string email;
	public GameObject btnLogOutAccount;
	public GameObject btnLogInAccount;
	public GameObject btnPrev;
	public GameObject btnPrevAccount;
	public GameObject NavBar;
	public GameObject tuto_Account;
	public GameObject buttonLogin;
	public GameObject QK03_Home;
	public GameObject QK02_WelcomeBack, PanelBlurPopUp;
	public Text name;

	void Start () {
		iOSKeyboardPatch.Apply ();
	}
	void OnEnable ()
	{
		buttonLogin.GetComponent<Button> ().enabled = true;
	}

	public void btnLogin()
	{
		email = IFemail.text;
		string password = IFpassword.text;
		if (email.Length > 7 && !email.Equals ("tap here to input...") && password.Length > 6
			&& !password.Equals ("tap here to input...") ) 
		{
			BaseService.Instance.playerService.sendSignInSuccess (email, password);
			BaseService.Instance.playerService.SignInDelegate += checkReponseSignIn;
		}
		else {
			//blur.turnBlurOn();
            PanelBlurPopUp.SetActive(true);
            ErrorPopup.SetActive(true);
			lblContent.text = "Please, check email,password";
		}
	}

	public void checkReponseSignIn(bool success)
	{
		if (success=true) {
			buttonLogin.GetComponent<Button> ().enabled = false;
			btnLogOutAccount.SetActive (true);
			btnLogInAccount.SetActive (false);
			GameData.playerData.IEmail = email;
			PlayerPrefs.SetString ("email", email);
			PlayerPrefs.Save ();
			if (btnPrev.activeSelf == true) {
				this.DisplayScene (false);
				//blur.turnBlurOn();
                PanelBlurPopUp.SetActive(true);
                QK02_WelcomeBack.SetActive (true);
				QK03_Home.SetActive (true);
				NavBar.SetActive (true);
			//	QK_LoginDone.SetActive (true);

			} else {
				this.DisplayScene (false);
				tuto_Account.SetActive (true);
				NavBar.SetActive (true);
			}
		}
	}
}
