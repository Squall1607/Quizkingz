using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QK12_Challengemain_Cotroller : BasePageController
{

    public GameObject navbarPopup, bgClickedPopup;
    public GameObject fakeMain;
    public GameObject QK13Duel;
    public GameObject blockNav;
    public GameObject content;
	public Text name;
	public Text tag;
	public Text level;
	public Text coin;
	public GameObject sprAvatar;

    public List<PlayerData> RevengeList = new List<PlayerData>();

    public GameObject RevengeContainer, RevengeScroll, playerPrefab, BattleTab, revengeTab;

    //public GameObject QK12Clone;
    void OnDisable() {
        fakeMain.SetActive(false);
    }

    void OnEnable()
	{
		name.text = GameData.playerData.Display;
        tag.text = "TAG #" + GameData.playerData.BattleTag.ToUpper();
		level.text = "LVL. "+GameData.playerData.Level;
		coin.text = GameData.playerData.Coin.ToString();
		sprAvatar.GetComponent<Image>().sprite = GameData.playerData.Avartar;
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

    public void CloneData(QK12CloneInvi qk12)
    {

        //QK12CloneInvi qk12 = new QK12CloneInvi();
        //QK12CloneInvi qk12 = QK12Clone.GetComponent<QK12CloneInvi>();
        Debug.Log(qk12.lstDuelInvi.Count);

        if (qk12.lstDuelInvi.Count > 0)
        {

            for (int i = 0; i < qk12.lstDuelInvi.Count; i++)
            {
               

                qk12.lstDuelInvi[i].GetComponentInChildren<DuelInvitation>().txtName.text = qk12.lstName[i];
                qk12.lstDuelInvi[i].GetComponentInChildren<DuelInvitation>().rName = qk12.lstRoomName[i];
                //qk12.lstDuelInvi[i].GetComponent<centerController>().rName = qk12.lstRoomName[i];

                Debug.Log("aaaaaaaaaaaaa" + qk12.lstRoomName[i]);
                Debug.Log("aaaaaaaaaaaaa" + qk12.lstDuelInvi[i].GetComponentInChildren<DuelInvitation>().rName);

                if (qk12.lstCoin[i] == 0)
                {
                    qk12.lstDuelInvi[i].GetComponentInChildren<DuelInvitation>().Coin.text = "FREE";
                }
                else
                {
                    qk12.lstDuelInvi[i].GetComponentInChildren<DuelInvitation>().Coin.text = DuelData.coinFee.ToString();
                }

            }
        }
    }

    public void backClick()
    {
        if (navbarPopup.activeSelf)
        {
            navbarPopup.GetComponent<NavigationBarController>().clicked2(0, bgClickedPopup);
            navbarPopup.GetComponent<NavigationBarController>().xoayTemp();
        }

    }

    public void duelClick()
    {
        this.DisplayScene(false);
        QK13Duel.SetActive(true);
        fakeMain.SetActive(true);
        //navbarPopup.SetActive(false);
        //blockNav.SetActive(true);
        //navbar.SetActive(true);


    }

    public void BatlleTabON()
    {
        BattleTab.SetActive(true);
        revengeTab.SetActive(false);
        //lobbyScroll.SetActive(true);
    }

    public void RevengeTabON()
    {
        BaseService.Instance.gameService.SendRequestRevengeList();
        BattleTab.SetActive(false);
        revengeTab.SetActive(true);
        //lobbyScroll.SetActive(false);
    }
}
