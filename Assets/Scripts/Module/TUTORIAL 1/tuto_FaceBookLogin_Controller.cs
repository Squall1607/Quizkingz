using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.UI;
using SimpleJSON;

public class tuto_FaceBookLogin_Controller : BasePageController
{

	public Text txtName;
	public GameObject qK02_WelcomeBackAfterLogin;
	public GameObject qk11_4_1;
	public CameraController blur;
	public GameObject fb_logout;
	public GameObject btnLogin;
	public GameObject btnConnect;

	public GameObject loading;

	public void onClickLoginFacebook ()
	{
		Login ();
	}

	void OnEnable ()
	{
		if (PlayerPrefs.HasKey ("email")) {
			btnLogin.SetActive (false);
			btnConnect.SetActive (true);
		} else {
			btnLogin.SetActive (true);
			btnConnect.SetActive (false);
		}
	}

	void Awake ()
	{
		if (!FB.IsInitialized) {
			FB.Init (InitCallBack);
		}
	}

	void InitCallBack ()
	{
		Debug.Log ("FB has been initiased.");
		ShowUI ();
	}

	void DisplayInformation (bool success)
	{
		if (success) {
			ShowUI ();
			loading.SetActive (true);

		} else {
//			this.SceneSwitch ();
		}
	}

	public void Login ()
	{
		if (!FB.IsLoggedIn) {
			FB.LogInWithReadPermissions (new List<string>{ "user_friends" }, LoginCallBack);
		}
	}

	void LoginCallBack (ILoginResult result)
	{

		if (result.Error == null) {
			Debug.Log ("FB has logged in.");
			BaseService.Instance.playerService.SendTokenDelegate += DisplayInformation;
			BaseService.Instance.playerService.SendToken (AccessToken.CurrentAccessToken.TokenString);
		} else {
			Debug.Log ("Error during login: " + result.Error);
		}
	}

	void ShowUI ()
	{
		if (FB.IsLoggedIn) {
			FB.API ("me/picture?width=100&height=100", HttpMethod.GET, PictureCallBack);
			FB.API ("me?fields=first_name", HttpMethod.GET, NameCallBack);
			//			FB.API ("me/friends", HttpMethod.GET, FriendCallBack);
			//			QK03.SceneSwitch ();

		} else {

		}
	}

	void PictureCallBack (IGraphResult result)
	{ 
		//		sprAvatar.gameObject.SetActive (true);

		Texture2D image = result.Texture;
		GameData.playerData.Avartar = Sprite.Create (image, new Rect (0, 0, image.width, image.height), new Vector2 (0.5f, 0.5f));
		byte[] img = image.EncodeToJPG ();
		StartCoroutine (Utils.UploadImage (img));

		qk11_4_1.SetActive (false);

		qK02_WelcomeBackAfterLogin.SetActive (true);
		loading.SetActive (false);
	}

	void NameCallBack (IGraphResult result)
	{
		IDictionary<string, object> profile = result.ResultDictionary;
		string id = "" + profile ["id"];
		string name = "" + profile ["first_name"];

	}
}
