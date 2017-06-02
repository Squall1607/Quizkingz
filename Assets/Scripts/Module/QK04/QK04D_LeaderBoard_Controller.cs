using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Facebook.Unity;
using System.Collections.Generic;
using Tacticsoft;

public class QK04D_LeaderBoard_Controller : BasePageController , ITableViewDataSource
{

	public GameObject globalActive;
	public GameObject facebookActive;
	public GameObject PopUp;
	public GameObject gift;
	public ScrollRect myScrollRect;

	public GameObject unassignedFb;
	public GameObject loginFb;

	public VisibleCounterCell prefabGlobal;
	public GameObject prefabFacebook;
	public GameObject fbScrollview;
	public GameObject fbContainer;
	public GameObject btnLogin;

	public Text username;
	public static Text score;
	public static Text serial;

	public Text fbName;
	public Text fbScore;
	public Text fbSerial;

	public GameObject sprDiamondCrown;
	public GameObject sprGoldCrown;
	public GameObject sprSilverCrown;
	public GameObject sprBronzeCrown;
	public Image sprAvatar;
	public Image fbAvatar;

	private List<PlayerData> cmd21Leaderboard = new List<PlayerData> ();
	private List<PlayerData> fbList = new List<PlayerData> ();

	public TableView m_tableView;
	int m_numRows = 10;
	private int m_numInstancesCreated = 0;
	//float catchTime = 0.25f;

	void OnEnable ()
	{
		username.text = GameData.playerData.Display.ToUpper ();
		sprAvatar.sprite = GameData.playerData.Avartar;
		fbAvatar.sprite = GameData.playerData.Avartar;
		myScrollRect.verticalNormalizedPosition = 1f;
		globalActive.SetActive (true);
		facebookActive.SetActive (false);

		BaseService.Instance.gameService.sendLeaderBoard ();
		BaseService.Instance.gameService.LeaderBoardDelegate += displayListLeaderBoard;
		//Sinh list leaderboard
		m_tableView.dataSource = this;
	}

	void displayListLeaderBoard (List<PlayerData> list)
	{
		if (list != null) {
			cmd21Leaderboard = list;
			m_numRows = cmd21Leaderboard.Count;
			m_tableView.ReloadData ();
			for (int i = 0; i < list.Count; i++) {
				if (GameData.playerData.Id == list [i].Id) {
					score.text = list [i].Score.ToString ();
					serial.text = (i + 1).ToString () + ".";
					if ((i + 1) <= 10) {
						displayCrown (sprDiamondCrown, sprGoldCrown, sprSilverCrown, sprBronzeCrown);
					} else if ((i + 1) > 10 && (i + 1) <= 50) {
						displayCrown (sprGoldCrown, sprDiamondCrown, sprSilverCrown, sprBronzeCrown);
					} else if ((i + 1) > 50 && (i + 1) <= 100) {
						displayCrown (sprSilverCrown, sprDiamondCrown, sprGoldCrown, sprBronzeCrown);
					} else {
						displayCrown (sprBronzeCrown, sprDiamondCrown, sprGoldCrown, sprSilverCrown);
					}
				}
			}
		}
	}

	void displayCrown (GameObject obj1, GameObject obj2, GameObject obj3, GameObject obj4)
	{
		obj1.SetActive (true);
		obj2.SetActive (false);
		obj3.SetActive (false);
		obj4.SetActive (false);
	}

	public void onClickGift ()
	{
		PopUp.SetActive (true);
		BaseService.Instance.gameService.sendLPriceL ();
	}

	public void onClickGlobal ()
	{
		onClickTab (globalActive, facebookActive);
		gift.SetActive (true);	
	}



	public void onClickFacebook ()
	{
		onClickTab (facebookActive, globalActive);
		gift.SetActive (false);
		fbName.text = GameData.playerData.Display.ToUpper ();
		fbScore = score;
		fbSerial = serial;
		fbAvatar.sprite = GameData.playerData.Avartar;
		if (PlayerPrefs.HasKey ("facebook")) {
			Debug.Log ("here!");
			activeMode (loginFb, unassignedFb);
			for (int i = 0; i < cmd21Leaderboard.Count; i++) {
				for (int j = 0; j < GameData.friendList.Count; j++) {
					if (GameData.friendList [j].BattleTag == cmd21Leaderboard [i].BattleTag) {
						fbList.Add (GameData.friendList [j]);
					}

				}
			}
			if (fbList.Count > 0) {
				for (int k = 0; k < fbList.Count; k++) {
					GameObject p = Instantiate (prefabFacebook);
					Transform parent = fbScrollview.transform.FindChild ("Container");
					p.transform.SetParent (parent);
					p.GetComponent<RectTransform> ().localPosition = new Vector3 (0f, 0f, 0f); 
					p.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
					p.GetComponentsInChildren<Text> (true) [4].text = (k + 1) + ".";
				}
			}
		} else {
			Debug.Log ("Vao day");
			activeMode (unassignedFb, loginFb);

		}
	}

	private void activeMode (GameObject obj1, GameObject obj2)
	{
		obj1.SetActive (true);
		obj2.SetActive (false);
	}

	public void onClickLoginFacebook ()
	{
		Login ();

	}

	void onClickTab (GameObject obj1, GameObject obj2)
	{
		obj1.SetActive (true);
		obj2.SetActive (false);
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
			activeMode (loginFb, unassignedFb);
		} else {
			Debug.Log ("Error during login: " + result.Error);
		}
	}

	void DisplayInformation (bool success)
	{
		if (success) {
			ShowUI ();
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
		Texture2D image = result.Texture;
		GameData.playerData.Avartar = Sprite.Create (image, new Rect (0, 0, image.width, image.height), new Vector2 (0.5f, 0.5f));
		byte[] img = image.EncodeToJPG ();
		sprAvatar.sprite = GameData.playerData.Avartar;
		fbAvatar.sprite = GameData.playerData.Avartar;
		StartCoroutine (Utils.UploadImage (img));
	}

	void NameCallBack (IGraphResult result)
	{

		IDictionary<string, object> profile = result.ResultDictionary;
		string id = "" + profile ["id"];

		GameData.playerData.Display = "" + profile ["first_name"];

		username.text = GameData.playerData.Display;
		fbName.text = GameData.playerData.Display;
//		string name = "" + profile ["first_name"];

	}

	#region tableview

	public int GetNumberOfRowsForTableView (TableView tableView)
	{
		return m_numRows;
	}

	public float GetHeightForRowInTableView (TableView tableView, int row)
	{
		return (prefabGlobal.transform as RectTransform).rect.height;
	}

	public TableViewCell GetCellForRowInTableView (TableView tableView, int row)
	{
		VisibleCounterCell cell = tableView.GetReusableCell (prefabGlobal.reuseIdentifier) as VisibleCounterCell;
		if (cell == null) {
			cell = (VisibleCounterCell)GameObject.Instantiate (prefabGlobal);
			cell.name = "VisibleCounterCellInstance_" + (++m_numInstancesCreated).ToString ();
		}
		if (cmd21Leaderboard.Count > 0) {
			cell.SetRowData (cmd21Leaderboard [row], row + 1);
			StartCoroutine (Utils.LoadImage (cmd21Leaderboard [row].AvatarURL, value => cell._avatar.sprite = value));
		}
		return cell;
	}

	#endregion
}
