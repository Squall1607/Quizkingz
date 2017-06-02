using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Facebook.Unity;

public class QK14_3_StartGame_Controller : BasePageController {

	// Use this for initialization

	public GameObject QK14_3;
	public QK14_2_Fee QK14_2_Fee;
	public GameObject gInvite;
	public GameObject btnInvite;
	public GameObject btnStart;
	public GameObject btnStartInvite;
//	public GameObject playerToInvite;
	public GameObject playerPrefab;
	public GameObject lobbyScroll;
	public GameObject tempPlayer;
	public GameObject lobbyContainer;
	public GameObject playerToInvite;
	public GameObject findingScroll;
	public GameObject findingContainer;
	public GameObject INVITE;
	public GameObject scrollPanel;
	public GameObject inputField;
	public ScrollRect inviteScroll;
	public GameObject ScrollPlayerToInvite;
	public GameObject playertoinviteContainer;
//	public ScrollRect PlayerInviteScrollRect;
	public Text inputName;
	public InputField Name;
	public new string name = "";
	public string battleTag = "";
	public int userID = 0;
	public int inviteID = 0;
	public List<int> lstUSER = new List<int>();
	static List<PlayerData> myList = new List<PlayerData>();
	static List<PlayerData> lobbyList = new List<PlayerData>();
	static List<PlayerData> lstPlayerInvite = new List<PlayerData>();

	void OnEnable(){
	//	PlayerInviteScrollRect.horizontalNormalizedPosition = 1f;
		GetListPlayerLobby();
		playerToInvite.SetActive (false);
	}

	void OnDisable(){
		CancelInvoke ();
	}


	public void onClickInputField(){
		lobbyScroll.SetActive (false);
		requestPlayer ();
		string name = Name.text;
		if (name.Length ==0 || name.Equals ("Enter #BattleTag of Name")) {
			lobbyScroll.SetActive (true	);
			findingScroll.SetActive (false);
		//	iTween.MoveTo (inputField, iTween.Hash ("y", -467, "islocal", true, "time", 0.5f, "easetype", iTween.EaseType.easeInOutQuad));
		}
	}

	public void requestPlayer(){	
		for(int i = 0; i < findingContainer.transform.childCount; i++) {
			GameObject p = findingContainer.transform.GetChild (i).gameObject;
			Destroy(p);
		}
		BaseService.Instance.gameService.SearchPlayer (inputName.text);
		BaseService.Instance.gameService.SearchPlayerDelegate += displayWhenFind;
		findingScroll.SetActive (true);
	}

	public void displayWhenFind(List<PlayerData> listPlayer){
		myList = listPlayer;
		for (int i = 0; i < myList.Count; i++) {
			GameObject p = Instantiate (playerPrefab);
			Transform parent = findingScroll.transform.FindChild ("Container");
			p.transform.SetParent (parent);
			p.GetComponent<RectTransform> ().localPosition = new Vector3 (0f,0f,0f); 
			p.GetComponent<RectTransform> ().localScale = new Vector3 (1f,1f,1f);
			p.GetComponentsInChildren<Text> (true) [0].text = "" + myList [i].Id;
			p.GetComponentsInChildren<Text> (true) [1].text = myList [i].Display.ToUpper ();
			userID = (int)myList [i].Id;
			lstUSER.Add (userID);
		}	

	}
	public void displayInformation(List<PlayerData> list, string id){
		lstPlayerInvite = list;
		playerToInvite.SetActive (true);
		for (int i = 0; i < lstPlayerInvite.Count; i++) {
			if (list [i].Id == int.Parse (id)) {
				playerToInvite.SetActive (true);
				GameObject p = Instantiate (playerToInvite);
				Transform parent = ScrollPlayerToInvite.transform.FindChild ("Container");
				p.transform.SetParent (parent);
				p.GetComponent<RectTransform> ().localPosition = new Vector3 (0f,0f,0f); 
				p.GetComponent<RectTransform> ().localScale = new Vector3 (0.6f,0.6f,0.6f);
				playerToInvite.GetComponentsInChildren<Text> (true) [0].text = lstPlayerInvite [i].Display.ToUpper ();
				playerToInvite.GetComponentsInChildren<Text> (true) [1].text = "TAG #" + lstPlayerInvite [i].BattleTag.ToUpper ();
				playerToInvite.GetComponentsInChildren<Text> (true) [2].text = "LVL. " + lstPlayerInvite [i].Level.ToString ();
		//		inviteID = (int)lstPlayerInvite [i].Id;
				userID = (int)lstPlayerInvite [i].Id;
				lstUSER.Add (userID);
				Debug.Log ("id"+userID);
				Debug.Log ("lstUser"+lstUSER[userID].ToString());
			}
		}
	//	lstUSER.Add (userID);
	}
	public void displayInvite(string id){
		
			tempPlayer.SetActive (true);
			iTween.ScaleTo (tempPlayer, iTween.Hash ("x", 1.5f, "y", 1.5f, "islocal", true, "time",1));
			iTween.MoveTo (tempPlayer, iTween.Hash ("position", new Vector3(-284, -620, 0),"islocal", true, "time", 1,
				"oncomplete", "invitePlayer", "oncompletetarget", gameObject, "oncompleteparams", id));
			BaseService.Instance.gameService.sendStartGameMulti (1, lstUSER, 0, QK14_2_Fee.fee);
			displayInformation (lobbyList,id);
	}

	public void displayPlayerLobby(List<PlayerData> list){
		lobbyList = list;
		for (int i = 0; i < list.Count; i++) {
			if (i < 20) {
				GameObject p = Instantiate (playerPrefab);
				Transform parent = lobbyScroll.transform.FindChild ("Container");
				p.transform.SetParent (parent);
				p.GetComponent<RectTransform> ().localPosition = new Vector3 (0f, 0f, 0f); 
				p.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);

				p.GetComponentsInChildren<Text> (true) [1].text = list [i].Display.ToUpper ();
				p.GetComponentsInChildren<Text> (true) [0].text = "" + list [i].Id;

			}
		}
	}
	public void GetListPlayerLobby(){
		InvokeRepeating ("refresh", 0, 4);
	}
	private void refresh(){
		for (int i = 0; i < lobbyContainer.transform.childCount; i++) {
			GameObject p = lobbyContainer.transform.GetChild (i).gameObject;
			Destroy(p);
		}
		BaseService.Instance.gameService.SearchPlayer ("");
		BaseService.Instance.gameService.GetListPlayerLobby += displayPlayerLobby;
	}
	void invitePlayer(string id){
		tempPlayer.SetActive (false);
		tempPlayer.transform.localScale = new Vector3(1f,1f,1f);
		tempPlayer.transform.localPosition = new Vector3 (589, -1165, 0); //-106*userID

		for (int i = 0; i < myList.Count; i++) {
			if (myList [i].Id == int.Parse (id)) {
				playerToInvite.GetComponentsInChildren<Text> (true) [0].text = myList [i].Display.ToUpper ();
				playerToInvite.GetComponentsInChildren<Text> (true) [1].text = "TAG #" + myList [i].BattleTag.ToUpper ();
				playerToInvite.GetComponentsInChildren<Text> (true) [2].text = "LVL. " + myList [i].Level.ToString ();
			}
		}
	}

	public void showInvite()
	{
		displayPlayerLobby (lobbyList);
		btnInvite.SetActive (false);
		btnStart.SetActive (false);
		btnStartInvite.SetActive (true);
		gInvite.SetActive (true);
		iTween.MoveTo(QK14_3, iTween.Hash("position", new Vector3(0, 957, 0), "islocal", true, "time", 1f,"oncomplete", "none"));
	}
	public void hideInvite()
	{
		btnInvite.SetActive (true);
		btnStart.SetActive (true);
		btnStartInvite.SetActive (false);
		gInvite.SetActive (false);
		lobbyScroll.SetActive (true);
		iTween.MoveTo(QK14_3, iTween.Hash("position", new Vector3(0, 0, 0), "islocal", true, "time", 1f,"oncomplete", "none"));
		iTween.MoveTo (inputField, iTween.Hash("y", -467, "islocal", true, "time", 0.1, "easetype", iTween.EaseType.easeInOutQuad));
	}

}
