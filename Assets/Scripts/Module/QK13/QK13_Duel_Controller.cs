using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class QK13_Duel_Controller : BasePageController {

    public GameObject fakeMain, DuelTab, revengeTab;
	public Text name;
	public Text tag;
	public Text level;
	public Text coin;
	public Text matchTotal;
	public Text wDuel;
	public Text lDuel;
	public static int ptlose;
	public static int ptwin;
	public Image tabLose;
	public Image tabWin;
	public GameObject sprAvatar;
    
    public List<PlayerData> RevengeList = new List<PlayerData>();

    public GameObject RevengeContainer, RevengeScroll, playerPrefab;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable()
	{
        fakeMain.SetActive(true);

		name.text = GameData.playerData.Display;
		tag.text = "TAG #"+GameData.playerData.BattleTag.ToUpper();
		level.text = "LVL. "+GameData.playerData.Level;
		coin.text = GameData.playerData.Coin.ToString();
		matchTotal.text = GameData.playerData.MatchTotalDuel.ToString ();
		sprAvatar.GetComponent<Image>().sprite = GameData.playerData.Avartar;
		ptwin = GameData.playerData.WinDuel;
		ptlose = 100 - ptwin;
		int matchTOTAL = GameData.playerData.MatchTotalDuel;
		if (matchTOTAL == 0) {
			wDuel.text = "0%";
			lDuel.text = "0%";
			tabLose.fillAmount = 0;
			tabWin.fillAmount = 0;
		} else {
			wDuel.text = ptwin.ToString () + "%";
			lDuel.text = ptlose.ToString () + "%";
			float PTWIN = (float)ptwin;
			float PTLOSE = (float)ptlose;
			tabLose.fillAmount = PTLOSE / 100;
			tabWin.fillAmount = PTWIN / 100;
		}

        BaseService.Instance.gameService.RevengeListDelegate += displayRevenge;
    }

    //void displayRevenge(List<PlayerData> list)
    //{
    //    if (list.Count > 0)
    //    {
    //        if (RevengeList.Count == 0)
    //        {
    //            RevengeList = list;
    //            //Debug.Log(lobbyList.Count);
    //            for (int i = 0; i < RevengeList.Count; i++)
    //            {
    //                //if (i < 20)
    //                //{

    //                //Debug.Log(p.GetComponentsInChildren<Text>(true)[2].text);
    //                //print(GameData.playerData.BattleTag);

    //                if (RevengeList[i].BattleTag.ToUpper() == GameData.playerData.BattleTag.ToUpper())
    //                {

    //                }
    //                else
    //                {
    //                    GameObject p = Instantiate(playerPrefab);
    //                    Transform parent = RevengeScroll.transform.FindChild("Container");
    //                    p.transform.SetParent(parent);
    //                    p.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
    //                    p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

    //                    p.GetComponentsInChildren<Text>(true)[2].text = RevengeList[i].BattleTag.ToUpper();
    //                    p.GetComponentsInChildren<Text>(true)[1].text = RevengeList[i].Display.ToUpper();
    //                    p.GetComponentsInChildren<Text>(true)[0].text = "" + RevengeList[i].Id;

    //                    //if (!RevengeList[i].IsOnline || RevengeList[i].IsGame)
    //                    //{
    //                    //    p.GetComponentsInChildren<Image>(true)[0].color = Color.gray;
    //                    //    p.GetComponentsInChildren<Image>(true)[3].color = Color.gray;
    //                    //    p.GetComponentInChildren<Button>().enabled = false;
    //                    //}
    //                }  
    //            }
    //        }
    //        else
    //        {

    //            for (int i = 0; i < list.Count; i++)
    //            {
    //                if (!RevengeList.Contains(list[i]))
    //                {

    //                    if (RevengeList[i].BattleTag.ToUpper() == GameData.playerData.BattleTag.ToUpper())
    //                    {

    //                    }
    //                    else
    //                    {
    //                        GameObject p = Instantiate(playerPrefab);
    //                        Transform parent = RevengeScroll.transform.FindChild("Container");
    //                        p.transform.SetParent(parent);
    //                        p.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
    //                        p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

    //                        p.GetComponentsInChildren<Text>(true)[1].text = list[i].Display.ToUpper();
    //                        p.GetComponentsInChildren<Text>(true)[0].text = "" + list[i].Id;

    //                        //if (!RevengeList[i].IsOnline || RevengeList[i].IsGame)
    //                        //{
    //                        //    p.GetComponentsInChildren<Image>(true)[0].color = Color.gray;
    //                        //    p.GetComponentsInChildren<Image>(true)[3].color = Color.gray;
    //                        //    p.GetComponentInChildren<Button>().enabled = false;
    //                        //}
    //                    }

    //                }
    //            }

    //            for (int i = 0; i < RevengeList.Count; i++)
    //            {
    //                if (!list.Contains(RevengeList[i]) && int.Parse(RevengeContainer.transform.GetChild(i).GetComponentsInChildren<Text>(true)[0].text) == (int)RevengeList[i].Id)
    //                {
    //                    Debug.Log("DESTROY");
    //                    GameObject p = RevengeContainer.transform.GetChild(i).gameObject;
    //                    Destroy(p);
    //                }
    //            }
    //        }
    //    }

    //}
    void displayRevenge(List<PlayerData> list)
    {
        if (list.Count > 0)
        {

            if (RevengeContainer.transform.childCount > 0)
            {
                for (int i = 0; i < RevengeContainer.transform.childCount; i++)
                {
                    GameObject p = RevengeContainer.transform.GetChild(i).gameObject;
                    Destroy(p);
                }
            }

            RevengeList = list;
            for (int i = 0; i < list.Count; i++)
            {

                GameObject p = Instantiate(playerPrefab);
                Transform parent = RevengeScroll.transform.FindChild("Container");
                p.transform.SetParent(parent);
                p.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
                p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

                p.GetComponentsInChildren<Text>(true)[2].text = RevengeList[i].BattleTag.ToUpper();
                p.GetComponentsInChildren<Text>(true)[1].text = RevengeList[i].Display.ToUpper();
                p.GetComponentsInChildren<Text>(true)[0].text = "" + RevengeList[i].Id;

                if (!RevengeList[i].IsOnline || RevengeList[i].IsGame)
                {
                    p.GetComponentsInChildren<Image>(true)[0].color = Color.gray;
                    p.GetComponentsInChildren<Image>(true)[3].color = Color.gray;
                    p.GetComponentInChildren<Button>().enabled = false;
                }
            }

        }
    }

    void OnDisable()
    {
        fakeMain.SetActive(false);
    }

    public void CloneData(QK12CloneInvi qk12)
    {
        //QK12CloneInvi qk12 = new QK12CloneInvi();
        if (qk12.lstDuelInvi13.Count > 0)
        {

            for (int i = 0; i < qk12.lstDuelInvi13.Count; i++)
            {
                qk12.lstDuelInvi13[i].GetComponentInChildren<DuelInvitation>().txtName.text = qk12.lstName13[i];
                qk12.lstDuelInvi13[i].GetComponentInChildren<DuelInvitation>().rName = qk12.lstRoomName13[i];
                //qk12.lstDuelInvi13[i].GetComponent<centerController>().rName = qk12.lstRoomName13[i];

                if (qk12.lstCoin13[i] == 0)
                {
                    qk12.lstDuelInvi13[i].GetComponentInChildren<DuelInvitation>().Coin.text = "FREE";
                }
                else
                {
                    qk12.lstDuelInvi13[i].GetComponentInChildren<DuelInvitation>().Coin.text = DuelData.coinFee.ToString();
                }

            }
        }
    }

    public void DuelTabON()
    {
        DuelTab.SetActive(true);
        revengeTab.SetActive(false);
        //lobbyScroll.SetActive(true);
    }

    public void RevengeTabON()
    {
        BaseService.Instance.gameService.SendRequestRevengeList();
        DuelTab.SetActive(false);
        revengeTab.SetActive(true);
        //lobbyScroll.SetActive(false);
    }
}
