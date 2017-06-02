using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Facebook.Unity;
using GameDelegates;
using System;
using System.Threading;




public class QK13_3_FindOpponent_Controller : BasePageController {

	public GameObject inviteTab;
	public GameObject revengeTab;
	public GameObject btnRandom;
	public GameObject playerToInvite;
	public GameObject sprBgInvite;
	public GameObject btnBack;
	public GameObject inputField;

	public GameObject playerPrefab;
	public GameObject queuePlayerPrefab;

	public GameObject lobbyScroll;
	public GameObject findingScroll;
	public GameObject lobbyContainer;
	public GameObject findingContainer;

	public GameObject queuePanel;
	public GameObject queueScroll;
	public GameObject queueContainer;

    public GameObject newInvitePopup, QK13_4_InvitationSent;
	public GameObject scrollPanel;
	public GameObject tempPlayer;
	public GameObject tempQueue;
	public GameObject loadingSearch1;
	public InputField newName;

	public ScrollRect inviteScroll;

	public Text inputName;
	public float timer = 0.3f;
	public InputField Name;

	public static new string name = "";
	public static string battleTag = "";
	public static int userID = 0;
	public static List<int> lstUSER = new List<int>(2);

	public QK14_2_Fee QK14_2_Fee;
	public GameObject DuelInviRandomNoti;
	public GameObject Notification;

	static List<PlayerData> foundList = new List<PlayerData>();
	static List<PlayerData> lobbyList = new List<PlayerData>();
	static List<PlayerData> queueList = new List<PlayerData>();
	static int num = 0;
	static bool isRun = false;
	static bool isRandom = false;
    public CommonScript cs;
    public GameObject QK13PreGame, fakeMain;
    public static bool isAccept = false;




    #region display information of player when scene is enable 
    void OnEnable(){
        fakeMain.SetActive(true);
        BaseService.Instance.gameService.responseStartGameDelegate += startGame;

        //Get list player lobby
        InvokeRepeating ("refresh", 0, 4);
		/*---------------------------------------*/

		findingScroll.SetActive (false);
		playerToInvite.SetActive (false);
		displayTab (inviteTab, revengeTab);

        BaseService.Instance.gameService.AnswerInvitationDelegate += check;
    }
		
    void check(bool isaAccept)
    {
        isAccept = isaAccept;

        if (!isaAccept)
        {
           
        }
    }

    private void startGame()
    {
        cs.turnOffAll(QK13PreGame);
    }

    void refresh(){
		for (int i = 0; i < lobbyContainer.transform.childCount; i++) {
			GameObject p = lobbyContainer.transform.GetChild (i).gameObject;
			Destroy(p);
		}
		BaseService.Instance.gameService.SearchPlayer ("");
		BaseService.Instance.gameService.GetListPlayerLobby += displayLobby;
	}

	void OnDisable(){
		CancelInvoke ();
        fakeMain.SetActive(false);

    }

	public void onClickInviteTab(){
		displayTab (inviteTab, revengeTab);
		lobbyScroll.SetActive (true);
	}

	public void onClickRevengeTab(){
		displayTab (revengeTab, inviteTab);
		lobbyScroll.SetActive (false);
	}

	private void displayTab(GameObject obj1, GameObject obj2){
		obj1.SetActive (true);
		obj2.SetActive (false);
	}

	/*Hien thi danh sach nguoi choi trong lobby*/
	public void displayLobby(List<PlayerData> list){
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

	/*Xu li event hien thi khi nguoi choi click tim kiem*/
	public void onClickInputField(){

		string name = Name.text;
		if (name.Length != 0 || name.Equals ("Enter #BattleTag of Name")) {
			loadingSearch1.SetActive (true);
			timer = 10000f;
		} else {
			loadingSearch1.SetActive (false);
		}

		inviteTab.SetActive (false);
		revengeTab.SetActive (false);
		btnRandom.SetActive (false);
		sprBgInvite.SetActive (true);
		lobbyScroll.SetActive (false);
		iTween.MoveTo (inputField, iTween.Hash("y", 34, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
		iTween.MoveTo (sprBgInvite, iTween.Hash("y", -290, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
	}

	#endregion	

	#region Finding player and invite another player
	//Hien thi playerToInvite
	public void displayInvite(string id){
		//Debug.Log("dddddddddddddddddddddddddddddddddddddddddddddd         "+id);
		if (lobbyScroll.activeSelf) {//Click player trong lobby
			//Di chuyen sang mode find player
			inviteTab.SetActive (false);
			revengeTab.SetActive (false);
			btnRandom.SetActive (false);
			sprBgInvite.SetActive (true);
			findingScroll.SetActive (false);

			lobbyScroll.GetComponent<RectTransform> ().sizeDelta = new Vector2 (810, 507);

			iTween.MoveTo (lobbyScroll, iTween.Hash ("y", 493, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
			iTween.MoveTo (inputField, iTween.Hash ("y", 34, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
			iTween.MoveTo (sprBgInvite, iTween.Hash ("y", -290, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad,
				"oncomplete", "InvitePlayer", "oncompletetarget", gameObject, "oncompleteparams", id));
			//Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
			//BaseService.Instance.gameService.sendStartGameMulti (1, lstUSER, 0, QK14_2_Fee.fee);
		} else {
			//Find player
			tempPlayer.SetActive (true);
			iTween.ScaleTo (tempPlayer, iTween.Hash ("x", 2.3f, "y", 2.3f, "islocal", true, "time", 1));
			iTween.MoveTo (tempPlayer, iTween.Hash ("position", new Vector3 (-139, -136, 0), "islocal", true, "time", 1,
				"oncomplete", "InvitePlayer", "oncompletetarget", gameObject, "oncompleteparams", id));
			//Debug.Log("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");
			//BaseService.Instance.gameService.sendStartGameMulti (1, lstUSER, 0, QK14_2_Fee.fee);
		}
}
	void InvitePlayer(string id){
        lstUSER.Add(int.Parse(id));
        tempPlayer.SetActive (false);
		tempPlayer.transform.localScale = new Vector3(1f,1f,1f);
		tempPlayer.transform.localPosition = new Vector3 (-335, 523, 0);
		for (int i = 0; i < foundList.Count; i++) {
			if (foundList [i].Id == int.Parse (id)) {
				queueList.Add (foundList[i]);

				playerToInvite.GetComponentsInChildren<Text> (true) [0].text = foundList [i].Display.ToUpper ();
				playerToInvite.GetComponentsInChildren<Text> (true) [1].text = "TAG #" + foundList [i].BattleTag.ToUpper ();
				playerToInvite.GetComponentsInChildren<Text> (true) [2].text = "LVL. " + foundList [i].Level.ToString ();
			}

		}
		playerToInvite.SetActive (true);

		Debug.Log ("queue List: "+ queueList.Count);
        //nhieu player
        if (queueList.Count > 1) {
			//Di chuyen avatar, disable thong tin
			iTween.MoveTo (playerToInvite, iTween.Hash ("x", -44, "islocal", true, "time", 1));
			playerToInvite.GetComponentsInChildren<Text> (true) [0].enabled = false;
			playerToInvite.GetComponentsInChildren<Text> (true) [1].enabled = false;
			playerToInvite.GetComponentsInChildren<Text> (true) [2].enabled = false;
			playerToInvite.GetComponentsInChildren<Text> (true) [4].enabled = false;

			queuePanel.SetActive (true);
			GameObject p = Instantiate (queuePlayerPrefab);
			Transform parent = queueScroll.transform.FindChild ("Container");
			p.transform.SetParent (parent);
			p.GetComponent<RectTransform> ().localPosition = new Vector3 (0f, 0f, 0f); 
			p.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);

            //Hien thi avatar
            //			p.GetComponentsInChildren<Image>(true)[1].sprite
            //BaseService.Instance.gameService.sendStartGameMulti(1, lstUSER, 0, QK14_2_Fee.fee);
            //1 player
        }
        else {
			Debug.Log ("vao day roi");
			iTween.MoveTo (playerToInvite, iTween.Hash ("x", 57, "islocal", true, "time", 1));
			playerToInvite.GetComponentsInChildren<Text> (true) [0].enabled = true;
			playerToInvite.GetComponentsInChildren<Text> (true) [1].enabled = true;
			playerToInvite.GetComponentsInChildren<Text> (true) [2].enabled = true;
			playerToInvite.GetComponentsInChildren<Text> (true) [4].enabled = true;
			queuePanel.SetActive (false);

            BaseService.Instance.gameService.sendStartGameMulti(1, lstUSER, 0, QK14_2_Fee.fee);
        }

	}
	public void btnInviteRandom(){
		Debug.Log ("ClickInviteRandom");
		BaseService.Instance.gameService.sendStartGameMultiRandom (1, true, 0, QK14_2_Fee.fee);
		DuelInviRandomNoti.SetActive (true);
		//cloneNotiInviRandomDuel ();
	}
//	public void cloneNotiInviRandomDuel(){
//			GameObject go = Instantiate(DuelInviRandomNoti) as GameObject;
//			go.transform.parent = Notification.transform;
//			go.GetComponent<RectTransform> ().localPosition = new Vector3 (0f, 0f, 0f); 
//			go.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
//	}
	public void btnCancelInviteRandom(){
		BaseService.Instance.gameService.sendCancelInviteRandom (true);
		DuelInviRandomNoti.SetActive (false);
	}
	public void onClickFind(){	
		

		for (int i = 0; i < findingContainer.transform.childCount; i++) {
			GameObject p = findingContainer.transform.GetChild (i).gameObject;
			Destroy (p);
		}
		BaseService.Instance.gameService.SearchPlayer (inputName.text);
		BaseService.Instance.gameService.SearchPlayerDelegate += displayWhenFind;
		findingScroll.SetActive (true);

	}
	/*Hien thi list nguoi choi tim thay*/
	public void displayWhenFind(List<PlayerData> listPlayer){
		foundList = listPlayer;
		for (int i = 0; i < foundList.Count; i++) {
			GameObject p = Instantiate (playerPrefab);
			Transform parent = findingScroll.transform.FindChild ("Container");
			p.transform.SetParent (parent);
			p.GetComponent<RectTransform> ().localPosition = new Vector3 (0f,0f,0f); 
			p.GetComponent<RectTransform> ().localScale = new Vector3 (1f,1f,1f);
			p.GetComponentsInChildren<Text> (true) [0].text = "" + foundList [i].Id;
			p.GetComponentsInChildren<Text> (true) [1].text = foundList [i].Display.ToUpper ();
			if(!foundList[i].IsOnline || foundList[i].IsGame)
            {
				p.GetComponentsInChildren<Image> (true) [0].color = Color.gray;
				p.GetComponentsInChildren<Image> (true) [3].color = Color.gray;

                //p.GetComponentInChildren<Button>(true).enabled = false;
            }
			//userID = (int)foundList [i].Id;
			//lstUSER.Add (userID);
		}	
		int ccf = findingContainer.transform.childCount;
		if (ccf == 0) {
			loadingSearch1.SetActive (true);
			timer = 10000f;
		}
		else {
			loadingSearch1.SetActive (true);
			timer = 0.5f;
		}
		//FB.API(p.FacebookID + "/picture?width=100&height=100", HttpMethod.GET, delegate (IGraphResult result)
		//{
		//    playerToInvite.GetComponentsInChildren<Image>(true)[1].sprite = Sprite.Create(result.Texture, new Rect(0, 0, 120, 120), new Vector2(0.5f, 0.5f));
		//});
	}
		
	/*Xu ly Queue List*/

	void Update(){
        //for (int i = 0; i < lstUSER.Count; i++) {
        //	Debug.Log ("lstUSER: "+ lstUSER[i]);
        //}
		timer -= Time.deltaTime;
		if (timer < 0) {
			timer -= Time.deltaTime;
			if (timer < 0) {
				loadingSearch1.SetActive (false);
			}
		}



        if (queueList.Count == 1)
        {
            iTween.MoveTo(playerToInvite, iTween.Hash("x", 57, "islocal", true, "time", 1));
            playerToInvite.GetComponentsInChildren<Text>(true)[0].enabled = true;
            playerToInvite.GetComponentsInChildren<Text>(true)[1].enabled = true;
            playerToInvite.GetComponentsInChildren<Text>(true)[2].enabled = true;
            playerToInvite.GetComponentsInChildren<Text>(true)[4].enabled = true;
            queuePanel.SetActive(false);
        }

		if (Timer.isDone) {
            if (queueList.Count == 1)
            {
                invisible();
                lstUSER.RemoveAt(0);
                //lstUSER.Clear();

            }
            else if (queueList.Count > 1)
            {
                invisible();
                lstUSER.RemoveAt(0);
                //InvitePlayer(lstUSER[0].ToString());
                BaseService.Instance.gameService.sendStartGameMulti(1, lstUSER, 0, QK14_2_Fee.fee);
            }
			
		}

        if (QK13_4_Duel_InvitationSend_Controller.isAccept)
        {
            invisible();
            lstUSER.Clear();
            //BaseService.Instance.gameService.sendStartGameMulti (1, lstUSER, 0, QK14_2_Fee.fee);
        }
    }

    public void avtBACK()
    {

    }

    void invisible(){
		playerToInvite.SetActive(false);
		Timer.isDone = false;
		tempQueue.SetActive (true);
//			iTween.ScaleTo (tempQueue, iTween.Hash ("x", 1.6, "y", 1.6, "islocal", true, "time", 1));te
//			iTween.MoveTo (tempQueue, iTween.Hash ("position", new Vector3 (-236, -135, 0), "islocal", true, "time", 1,
//				"oncomplete", "onComplete", "oncompletetarget", gameObject));
		tempQueue.SetActive (false);
		if (queueContainer.transform.childCount > 0) {
			playerToInvite.SetActive (true);
		} else {
			queuePanel.SetActive (false);
		}
		//Delete a gameobject in queue list
		if (queueList.Count > 1) {
			Debug.Log (queueContainer.transform.GetChild(0));
			GameObject p = queueContainer.transform.GetChild (0).gameObject;

			Destroy (p);
			queueList.RemoveAt (0);
//			lstUSER.Add (p1);
//			BaseService.Instance.gameService.sendStartGameMulti (1, lstUSER, 0, QK14_2_Fee.fee);
		}

	}

	void onComplete(){
		tempQueue.SetActive (false);
		if (queueContainer.transform.childCount>0) {
			playerToInvite.SetActive (true);
		}
	}

	#endregion
}
