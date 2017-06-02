using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using SimpleJSON;

public class tuto_Account_Controller : BasePageController
{
	[DllImport ("__Internal")]
	private static extern void OpenVideoPicker (string game_object_name, string function_name);

	public Text txtName;
	public Text email;
	public Text status;
	public GameObject ErrorPopup;
	public Button btnLogOut;
	public GameObject btnLogOutAccount;
	public GameObject btnLogInAccount;
	public GameObject btnAddEmail;
	public GameObject btnComfirmEmail;
	public GameObject btnPrev;
	public GameObject btnPrevAccount;
	//public GameObject Navbar;
	//public GameObject NavbarPopup;
	public GameObject buttonLogin;
	public GameObject sprAvatar;

	// Use this for initialization
	void Update ()
	{
		sprAvatar.GetComponent<Image> ().sprite = GameData.playerData.Avartar;
	}

	void OnEnable ()
	{
		
		btnLogOut.interactable = true;
		ChangeInfor ();
		if (PlayerPrefs.HasKey ("facebook") && !PlayerPrefs.HasKey ("email")) {
			btnAddEmail.SetActive (true);
			btnLogInAccount.SetActive (false);
			btnLogOutAccount.SetActive (false);
		} else if (PlayerPrefs.HasKey ("facebook") && PlayerPrefs.HasKey ("email")) {
			btnLogOutAccount.SetActive (true);
			btnLogInAccount.SetActive (false);
			btnAddEmail.SetActive (false);
		} else if (PlayerPrefs.HasKey ("email")) {
			btnLogOutAccount.SetActive (true);
			btnLogInAccount.SetActive (false);
			btnAddEmail.SetActive (false);
		} else {
			btnLogOutAccount.SetActive (false);
			btnLogInAccount.SetActive (true);
			btnAddEmail.SetActive (false);
		}
		if (PlayerPrefs.HasKey ("email")) {
			btnComfirmEmail.SetActive (true);
		} else {
			btnComfirmEmail.SetActive (false);
		}

		BaseService.Instance.gameService.RefreshDataDelegate += ChangeInfor;

			
	}

	public void onClickLogIn ()
	{
		//Navbar.SetActive (false);
		//NavbarPopup.SetActive (false);
		btnPrev.SetActive (false);
		btnPrevAccount.SetActive (true);
	}

	public void onClickLogout ()
	{
		PlayerPrefs.DeleteKey ("email");
		PlayerPrefs.DeleteKey ("facebook");
		buttonLogin.GetComponent<Button> ().enabled = true;
	}

	public void onClickYes ()
	{
		//Navbar.SetActive (false);
		///NavbarPopup.SetActive (false);
		btnPrev.SetActive (false);
		btnPrevAccount.SetActive (true);
		btnLogOutAccount.SetActive (false);
		btnLogInAccount.SetActive (true);
		btnLogOut.interactable = false;
		PlayerPrefs.DeleteKey ("email");
		PlayerPrefs.DeleteKey ("facebook");
		BaseService.Instance.gameService.SendLogOutEmail ();
	}

	public void OnClickChangeImage ()
	{
		OpenVideoPicker ("QK11.3 - Account", "DisplayImage");
	}

	void DisplayImage (string path)
	{
		StartCoroutine (Utils.LoadImage (path, value => GameData.playerData.Avartar = value));
		byte[] img = GameData.playerData.Avartar.texture.EncodeToJPG ();
		StartCoroutine (Utils.UploadImage (img));
	}

	void ChangeInfor ()
	{
		email.text = GameData.playerData.Email;
		txtName.text = GameData.playerData.Display;
		sprAvatar.GetComponent<Image> ().sprite = GameData.playerData.Avartar;
	}
	//	void Update(){
	//		if (loading.activeSelf) {
	//			time -= Time.deltaTime;
	//			if (time < 0) {
	//				loading.SetActive (false);
	//				email.text = "none";
	//			}
	//		}
	//	}



}
