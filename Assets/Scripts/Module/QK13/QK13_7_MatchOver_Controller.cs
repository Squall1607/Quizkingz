using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class QK13_7_MatchOver_Controller : BasePageController {

    public Text txtName, txtTAG, txtYOU, txtOPP, txtTotoalScore, txtCorrectScore;
    public static bool isWin;
    public GameObject UIYouWin, UIYouLose;
    public qk13Find QK13_Find;
    public GameObject fakeMain;
    public GameObject blockNav, blockNavPopup;
    public Button rematchBtn;
    public static List<int> lstUSER = new List<int>();
	public static bool isRematchClick = false;
	public CameraController blur;
    public CommonScript cs;
    public GameObject QK13PreGame;
	int userID;
	public GameObject loading;
	public GameObject prefabNotiRematch;
	public GameObject Notification;
	public  List<GameObject> lstRNotiDuelInvi = new List<GameObject>();
	public  List<string> lstRNotiName = new List<string>();
	public  List<string> lstRNotiRoomName = new List<string>();
	public  List<int> lstRNotiCoin = new List<int>();

    public QK07Duel_Controller QK7_2_Duel;

    void OnEnable()
    {
        BaseService.Instance.gameService.SearchPlayerDelegate += activeWhenFind;
        BaseService.Instance.gameService.responseStartGameDelegate += startGame;

        QK7_2_Duel.lose = 0;
        QK7_2_Duel.win = 0;

        //navbar.SetActive(false);
        fakeMain.SetActive(false);
		//navbarPopup.SetActive(true);
        blockNav.SetActive(false);
		rematchBtn.interactable = true;

        if (!String.IsNullOrEmpty(DuelData.ownerName))
        {
            //neu la nguoi dc moi
            txtName.text = DuelData.ownerName.ToUpper();
            txtTAG.text = "TAG #" + DuelData.ownerBattleTAG.ToUpper();
            BaseService.Instance.gameService.SearchPlayer(DuelData.ownerBattleTAG);

        }
        else if (!String.IsNullOrEmpty(QK13_Find.nameFind))
        {
            //neu la chu phong
            txtName.text = QK13_Find.nameFind.ToUpper();
            txtTAG.text = "TAG #" + QK13_Find.batterTAG.ToUpper();
            BaseService.Instance.gameService.SearchPlayer(QK13_Find.batterTAG);
        }
        //else if (!String.IsNullOrEmpty(QK13_3_FindOpponent_Controller.name))
        //{
        //    //neu la chu phong
        //    txtName.text = QK13_3_FindOpponent_Controller.name.ToUpper();
        //    txtTAG.text = "TAG #" + QK13_3_FindOpponent_Controller.battleTag.ToUpper();
        //    BaseService.Instance.gameService.SearchPlayer(QK13_3_FindOpponent_Controller.battleTag);
        //}



        txtTotoalScore.text = WinnerData.winnerPointTotal.ToString();
        txtCorrectScore.text = WinnerData.winnerPointCorrect.ToString();



        if (WinnerData.winnerTAG == GameData.playerData.BattleTag)
        {
            if (WinnerData.win == 2)
            {
                txtYOU.text = "2";
                txtOPP.text = "0";
                UIYouWin.SetActive(true);
                UIYouLose.SetActive(false);
            }
            else if (WinnerData.win == 1)
            {
                if (GameData.currentQuestion <= 5)
                {
                    txtYOU.text = "1";
                    txtOPP.text = "0";
                    UIYouWin.SetActive(true);
                    UIYouLose.SetActive(false);
                }
                else
                {
                    txtYOU.text = "2";
                    txtOPP.text = "1";
                    UIYouWin.SetActive(true);
                    UIYouLose.SetActive(false);
                }

            }
        }
        else
        {
            if (WinnerData.win == 2)
            {
                txtYOU.text = "0";
                txtOPP.text = "2";
                UIYouWin.SetActive(false);
                UIYouLose.SetActive(true);
            }
            else if (WinnerData.win == 1)
            {
                if (GameData.currentQuestion <= 5)
                {
                    txtYOU.text = "0";
                    txtOPP.text = "1";
                    UIYouWin.SetActive(false);
                    UIYouLose.SetActive(true);
                }
                else
                {
                    txtYOU.text = "1";
                    txtOPP.text = "2";
                    UIYouWin.SetActive(false);
                    UIYouLose.SetActive(true);
                }

            }
        }
    }

    void startGame()
    {
        cs.turnOffAll(QK13PreGame);
		loading.SetActive (false);
		blockNavPopup.SetActive (false);
    }

    public void remactchClick()
    {
		BaseService.Instance.gameService.SendStartGameMultiDelegate += cloneRematchNotiInviDuel;
        BaseService.Instance.gameService.sendStartGameMulti(1, lstUSER, 0, QK14_2_Fee.fee);
		loading.SetActive (true);
		rematchBtn.interactable = false;
		blockNavPopup.SetActive (true);
		isRematchClick = true;

    }
	private void cloneRematchNotiInviDuel(){

		if (DuelData.receiverTime > 0)
		{
			GameObject go = Instantiate(prefabNotiRematch) as GameObject;
			go.transform.parent = Notification.transform;
			go.GetComponent<RectTransform> ().localPosition = new Vector3 (0f, 0f, 0f); 
			go.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
			//
			string name = DuelData.ownerName;
			int coin = DuelData.coinFee;
			string rName = DuelData.rName;

			GameData.playerData.RNotiDuelInvi.Add (go);
			GameData.playerData.RNotiName.Add (name);
			GameData.playerData.RNotiRoomName.Add (rName);
			GameData.playerData.RNotiCoin.Add (coin);
			if (GameData.playerData.RNotiDuelInvi.Count > 0) {
				for (int i = 0; i < GameData.playerData.RNotiDuelInvi.Count; i++) {
					go.GetComponentsInChildren<Text> (true) [0].text = GameData.playerData.RNotiName [0];
				}
			}
			StartCoroutine (countDownRNotiInviDuel (go, name, rName, coin, 4));
		}
	}
	IEnumerator countDownRNotiInviDuel(GameObject go, string name, string rName, int coin, float delay)
	{
		yield return new WaitForSeconds(delay);
		Debug.Log("remove Notification Rematch");
		lstRNotiDuelInvi.Remove(go);
		Destroy(go);
		lstRNotiName.Remove(name);
		lstRNotiCoin.Remove(coin);
		lstRNotiRoomName.Remove(rName);
	}

	public void turnOffLoading(bool isAccept){
		Debug.Log ("bool: "+ isAccept);
		if (!isAccept) {
			loading.SetActive (false);
			rematchBtn.interactable = true;
			blockNavPopup.SetActive (false);
		}
	}

    void activeWhenFind(List<PlayerData> list)
    {
		if (list[0].IsOnline)
        {
            rematchBtn.interactable = true;
			int userID = (int)list[0].Id;
            lstUSER.Add(userID);

        }
        else
        {
            rematchBtn.interactable = false;
        }
    }
}
