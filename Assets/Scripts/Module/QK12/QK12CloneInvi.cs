using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class QK12CloneInvi : BasePageController
{

    public GameObject prefab;
	public GameObject prefab1;
	public GameObject Notification;
    public GameObject grid12, grid13;
     
    public CommonScript cs;
    public GameObject QK13PreGame;

    public  List<GameObject> lstDuelInvi = new List<GameObject>();
    public  List<string> lstName = new List<string>();
    public  List<string> lstRoomName = new List<string>();
    public  List<int> lstCoin = new List<int>();

	public  List<GameObject> lstNotiDuelInvi = new List<GameObject>();
	public  List<string> lstNotiName = new List<string>();
	public  List<string> lstNotiRoomName = new List<string>();
	public  List<int> lstNotiCoin = new List<int>();

    public  List<GameObject> lstDuelInvi13 = new List<GameObject>();
    public  List<string> lstName13 = new List<string>();
    public  List<string> lstRoomName13 = new List<string>();
    public  List<int> lstCoin13 = new List<int>();

    //public QK12_Challengemain_Cotroller QK12_Challengemain;
    //public QK12_Challengemain_Cotroller QK12_Challengemain;
    public qk13Find qk13;

    public void OnEnable() {

    }
    // Use this for initialization
    void Start()
    {
        BaseService.Instance.gameService.SendStartGameMultiDelegate += clone12;
        BaseService.Instance.gameService.SendStartGameMultiDelegate += clone13;
		BaseService.Instance.gameService.SendStartGameMultiDelegate += cloneNotiInviDuel;

        BaseService.Instance.playerService.ReceiveInviteDelegate += deleteInvi;
        BaseService.Instance.gameService.GetListPlayerLobby += createLobby;
        //myGO = Resources.Load("PlayerInformation") as GameObject;
    }

    public void clone13()
    {
        if (DuelData.receiverTime > 0)
        {
            GameObject go = Instantiate(prefab) as GameObject;
            //13
            go.transform.parent = grid13.transform;
            //
            go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
            string name = DuelData.ownerName;
            int coin = DuelData.coinFee;
            string rName = DuelData.rName;

            lstDuelInvi13.Add(go);
            lstRoomName13.Add(rName);
            lstName13.Add(name);
            lstCoin13.Add(coin);

            QK13_Duel_Controller qk13 = new QK13_Duel_Controller();
            //this.CloneIvitationed += qk13.CloneData;
            qk13.CloneData(this);
            StartCoroutine(countDownInvi13(go, name, rName, coin, 20));
           
        }
    }

    public void cloneNotiInviDuel(){

		if (DuelData.receiverTime > 0)
		{
			GameObject go1 = Instantiate(prefab1) as GameObject;
			go1.transform.parent = Notification.transform;
			go1.GetComponent<RectTransform> ().localPosition = new Vector3 (0f, 0f, 0f); 
			go1.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
			//
			string name = DuelData.ownerName;
			int coin = DuelData.coinFee;
			string rName = DuelData.rName;
			GameData.playerData.NotiDuelInvi.Add (go1);
			GameData.playerData.NotiName.Add (name);
			GameData.playerData.NotiRoomName.Add (rName);
			GameData.playerData.NotiCoin.Add (coin);
			if (GameData.playerData.NotiDuelInvi.Count > 0) {
				for (int i=0; i < GameData.playerData.NotiDuelInvi.Count; i++) {
					go1.GetComponentsInChildren<Text> (true) [0].text = GameData.playerData.NotiName [i];
					if (GameData.playerData.NotiCoin [i] == 0) {
						go1.GetComponentsInChildren<Text> (true) [4].text = "FREE";
					} else {
						go1.GetComponentsInChildren<Text> (true) [4].text = GameData.playerData.NotiCoin [i].ToString ();
					}
				}
			}
			StartCoroutine(countDownNotiInviDuel(go1, name, rName, coin, 4));
		}
	}

    public void clone12()
    {
        if (DuelData.receiverTime > 0)
        {
            GameObject go = Instantiate(prefab) as GameObject;
            //12
            go.transform.parent = grid12.transform;
            go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
            string name = DuelData.ownerName;
            int coin = DuelData.coinFee;
            string rName = DuelData.rName;

            lstDuelInvi.Add(go);
            lstRoomName.Add(rName);
            lstName.Add(name);
            lstCoin.Add(coin);

            QK12_Challengemain_Cotroller qk12 = new QK12_Challengemain_Cotroller();
            qk12.CloneData(this);
            StartCoroutine(countDownInvi(go, name, rName, coin, 20));
        }

    }


	IEnumerator countDownNotiInviDuel(GameObject go1, string name, string rName, int coin, float delay)
	{
		yield return new WaitForSeconds(delay);
		Debug.Log("remove Notification Invite Duel");
		lstNotiDuelInvi.Remove(go1);
		Destroy(go1);
		lstNotiName.Remove(name);
		lstNotiCoin.Remove(coin);
		lstNotiRoomName.Remove(rName);
	}

    IEnumerator countDownInvi(GameObject go, string name, string rName, int coin, float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("removeeeeeeeeeeeeeeeee");
        lstDuelInvi.Remove(go);
        Destroy(go);
        lstName.Remove(name);
        lstCoin.Remove(coin);
        lstRoomName.Remove(rName);
    }

    IEnumerator countDownInvi13(GameObject go, string name, string rName, int coin, float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("removeeeeeeeeeeeeeeeee13");
        lstDuelInvi13.Remove(go);
        Destroy(go);
        lstName13.Remove(name);
        lstCoin13.Remove(coin);
        lstRoomName13.Remove(rName);
    }

    public void GameStart()
    {
        //QK13PreGame.SetActive(true);
        //this.SceneSwitch();
        cs.turnOffAll(QK13PreGame);
    }

    public void createLobby(List<PlayerData> list)
    {

        qk13.displayLobby(list);
    }

    public void deleteInvi(string masterName)
    {
        //Debug.Log(GameData.gameRoom);

        if (grid12.transform.childCount > 0)
        {
            for (int i = 0; i < grid12.transform.childCount; i++)
            {

                GameObject p = grid12.transform.GetChild(i).gameObject;
                //Debug.Log(p.GetComponentInChildren<DuelInvitation>().rName);
                //Debug.Log(GameData.room);

                if (p.GetComponentInChildren<DuelInvitation>().rName.ToUpper() == GameData.room.ToUpper())
                {
                    Destroy(p);
                }
            }
        }

        if (grid13.transform.childCount > 0)
        {
            for (int i = 0; i < grid13.transform.childCount; i++)
            {

                GameObject p = grid13.transform.GetChild(i).gameObject;
                //Debug.Log(p.GetComponentInChildren<DuelInvitation>().rName);
                //Debug.Log(GameData.room);

                if (p.GetComponentInChildren<DuelInvitation>().rName.ToUpper() == GameData.room.ToUpper())
                {
                    Destroy(p);
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
