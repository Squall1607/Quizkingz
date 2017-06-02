using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using SimpleJSON;

public class QK02_LoginFacebook_Controller : BasePageController
{
	public Text lblName;
	public Button[] btns;
	public Panel03Controller QK03;
	public GameObject QK02;
	public static bool isFirst = true;
	public GameObject QK02A1;
	public GameObject loading;

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

	public void Login ()
	{
		if (!FB.IsLoggedIn) {
			FB.LogInWithReadPermissions (new List<string>{ "user_friends" }, LoginCallBack);
		}
	}

	void DisplayInformation (bool success)
	{
		if (success) {
			loading.SetActive (true);
			ShowUI ();
		} else {
			QK02.SetActive (true);
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
		QK02.SetActive (false);
		QK02A1.SetActive (true);
		loading.SetActive (false);
	}

	void NameCallBack (IGraphResult result)
	{
		IDictionary<string, object> profile = result.ResultDictionary;
		string id = "" + profile ["id"];
		GameData.playerData.FbName = "" + profile ["first_name"];
//		if (GameData.playerData.FacebookID == "-1") {
//			lblName.text = GameData.playerData.Display;
//		} else {
//			lblName.text = GameData.playerData.FbName;
//		}
		lblName.text = GameData.playerData.Display;
		string name = "" + profile ["first_name"];

	}
		


}
