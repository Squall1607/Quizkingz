using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class qk13Find : BasePageController
{
    public GameObject inviteTab, revengeTab, btnRandom, sprBgInvite, lobbyScroll, inputField, findingContainer, findingScroll, playerPrefab;
    public GameObject PlayerInviNow, lobbyContainer, tempPlayer , queuePlayerPrefab, queueContainer, queueScroll, queuePanel, Name, Tag, Level, Status, QK13_PreGame;
    public CommonScript cs;
    public Text inputName;

    public List<PlayerData> lobbyList = new List<PlayerData>();
    public List<PlayerData> foundLst = new List<PlayerData>();
    public List<PlayerData> QLst = new List<PlayerData>();
    public List<int> lstID = new List<int>();
    public List<PlayerData> RevengeList = new List<PlayerData>();

    public GameObject DuelInviRandomNoti;
    public GameObject QK13_Duel, fakeMain,RevengeContainer, RevengeScroll;

    public string nameFind = "", batterTAG= "";
    //bool flag = false;
    //float timer = 20f;
    // Use this for initialization
    bool flag = false;
    public static bool next = false;
    bool back = false;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Timer.isDone)
        {
            if (queueContainer.transform.childCount > 0)
            {
                
                print("xoaaaaaaaaaaaaaaaaaaaaaaaaaa");
                GameObject g = queueContainer.transform.GetChild(0).gameObject;
                Destroy(g);
                Timer.isDone = false;
                //Debug.Log("zzzzzzzzzzzzzzzz     " + queueContainer.transform.childCount);
                if (queueContainer.transform.childCount == 1)
                {
                    BackToSoloQ();
                    Timer.isDone = false;
                    next = false;
                }
            }
        }
    }

    void BackToSoloQ()
    {
        queuePanel.SetActive(false);

        Name.SetActive(true);
        Tag.SetActive(true);
        Level.SetActive(true);
        Status.SetActive(true);

        iTween.MoveTo(PlayerInviNow, iTween.Hash("x", 57, "islocal", true, "time", 1));
    }

    void OnDisable()
    {
        fakeMain.SetActive(false);
    }

    void OnEnable()
    {
        nameFind = GameData.playerData.Display;
        batterTAG = GameData.playerData.BattleTag;

        Debug.Log(nameFind+"           " + batterTAG);
        back = false;
        //BaseService.Instance.gameService.GetListPlayerLobby += displayLobby;
        BaseService.Instance.gameService.AnswerInvitationDelegate += check;
       
        BaseService.Instance.gameService.RevengeListDelegate += displayRevenge;
    }

    void displayRevenge(List<PlayerData> list)
    {
        if (list.Count > 0)
        {
            if (RevengeList.Count == 0)
            {
                RevengeList = list;
                //Debug.Log(lobbyList.Count);
                for (int i = 0; i < RevengeList.Count; i++)
                {
                    //if (i < 20)
                    //{
                   
                    //Debug.Log(p.GetComponentsInChildren<Text>(true)[2].text);
                    //print(GameData.playerData.BattleTag);

                    if (RevengeList[i].BattleTag.ToUpper() == GameData.playerData.BattleTag.ToUpper())
                    {

                    }
                    else
                    {
                        GameObject p = Instantiate(playerPrefab);
                        Transform parent = RevengeScroll.transform.FindChild("Container");
                        p.transform.SetParent(parent);
                        p.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
                        p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

                        p.GetComponentsInChildren<Text>(true)[2].text = RevengeList[i].BattleTag.ToUpper();
                        p.GetComponentsInChildren<Text>(true)[1].text = RevengeList[i].Display.ToUpper();
                        p.GetComponentsInChildren<Text>(true)[0].text = "" + RevengeList[i].Id;
                    }  //}
                }
            }
            else
            {

                for (int i = 0; i < list.Count; i++)
                {
                    if (!RevengeList.Contains(list[i]))
                    {
                        
                        if (RevengeList[i].BattleTag.ToUpper() == GameData.playerData.BattleTag.ToUpper())
                        {

                        }
                        else
                        {
                            GameObject p = Instantiate(playerPrefab);
                            Transform parent = RevengeScroll.transform.FindChild("Container");
                            p.transform.SetParent(parent);
                            p.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
                            p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

                            p.GetComponentsInChildren<Text>(true)[1].text = list[i].Display.ToUpper();
                            p.GetComponentsInChildren<Text>(true)[0].text = "" + list[i].Id;
                        }

                    }
                }

                for (int i = 0; i < RevengeList.Count; i++)
                {
                    if (!list.Contains(RevengeList[i]) && int.Parse(RevengeContainer.transform.GetChild(i).GetComponentsInChildren<Text>(true)[0].text) == (int)RevengeList[i].Id)
                    {
                        Debug.Log("DESTROY");
                        GameObject p = RevengeContainer.transform.GetChild(i).gameObject;
                        Destroy(p);
                    }
                }
            }
        }
        
    }

    void check(bool isaAccept)
    {
        //Debug.Log("vvvvvvvvvvvvvvvvvvvvvvvvvv");
        if (!isaAccept)
        {
            if (QLst.Count == 1)
            {
                Debug.Log("aaaaaaaaaaaaaaaaaâ");
                StopCoroutine(StartQ());

                QLst.Remove(QLst[0]);
                if (lstID.Count > 0)
                {
                    lstID.Remove(lstID[0]);
                }
                PlayerInviNow.SetActive(false);
            }
            else if ( QLst.Count > 1)
            {
                Debug.Log("bbbbbbbbbbbbbbbbbbbbb");
                StopCoroutine(StartQ());

                QLst.Remove(QLst[0]);
                if (lstID.Count > 0)
                {
                    lstID.Remove(lstID[0]);
                }
                PlayerInviNow.SetActive(false);

                GameObject g = queueContainer.transform.GetChild(0).gameObject;
                Destroy(g);
                Timer.isDone = false;

                StartCoroutine(StartQ());
            }

        }
        else
        {
            Debug.Log("yeahhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh");
            backClick();
            BaseService.Instance.gameService.responseStartGameDelegate += start2;
        }
    }

    void start2()
    {
        Debug.Log("zoooooooooooooooooooooooooooooooooooo");

        this.DisplayScene(false);
        cs.turnOffAll(QK13_PreGame);

    }

    /*Hien thi danh sach nguoi choi trong lobby*/
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

    public void displayLobby(List<PlayerData> list)
    {
        if (list.Count > 0)
        {
            if (lobbyContainer.transform.childCount > 0)
            {
                for (int i = 0; i < lobbyContainer.transform.childCount; i++)
                {
                    GameObject p = lobbyContainer.transform.GetChild(i).gameObject;
                    Destroy(p);
                }
            }

            lobbyList = list;
            for (int i = 0; i < list.Count; i++)
            {
                if (lobbyList[i].BattleTag.ToUpper() == GameData.playerData.BattleTag.ToUpper())
                {

                }
                else
                {
                    GameObject p = Instantiate(playerPrefab);
                    Transform parent = lobbyScroll.transform.FindChild("Container");
                    p.transform.SetParent(parent);
                    p.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
                    p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

                    p.GetComponentsInChildren<Text>(true)[2].text = lobbyList[i].BattleTag.ToUpper();
                    p.GetComponentsInChildren<Text>(true)[1].text = lobbyList[i].Display.ToUpper();
                    p.GetComponentsInChildren<Text>(true)[0].text = "" + lobbyList[i].Id;
                }
               
            }

            //if (lobbyList.Count == 0)
            //{
            //    lobbyList = list;
            //    //Debug.Log(lobbyList.Count);
            //    for (int i = 0; i < lobbyList.Count; i++)
            //    {
          

            //        if (lobbyList[i].BattleTag.ToUpper() == GameData.playerData.BattleTag.ToUpper())
            //        {

            //        }
            //        else
            //        {
            //            GameObject p = Instantiate(playerPrefab);
            //            Transform parent = lobbyScroll.transform.FindChild("Container");
            //            p.transform.SetParent(parent);
            //            p.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            //            p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            //            p.GetComponentsInChildren<Text>(true)[2].text = lobbyList[i].BattleTag.ToUpper();
            //            p.GetComponentsInChildren<Text>(true)[1].text = lobbyList[i].Display.ToUpper();
            //            p.GetComponentsInChildren<Text>(true)[0].text = "" + lobbyList[i].Id;
            //        }  //}
            //    }
            //}
            //else
            //{

            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        if (!lobbyList.Contains(list[i]))
            //        {
            //            //Debug.Log("co00000000000000000000000");

            //            //if (lobbyList[i].BattleTag.ToUpper() == GameData.playerData.BattleTag.ToUpper())
            //            //{

            //            //}
            //            //else
            //            //{
            //                GameObject p = Instantiate(playerPrefab);
            //                Transform parent = lobbyScroll.transform.FindChild("Container");
            //                p.transform.SetParent(parent);
            //                p.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            //                p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            //                p.GetComponentsInChildren<Text>(true)[1].text = list[i].Display.ToUpper();
            //                p.GetComponentsInChildren<Text>(true)[0].text = "" + list[i].Id;
            //            //}

            //        }
            //        else
            //        {

            //        }
            //    }

            //for (int i = 0; i < lobbyList.Count; i++)
            //{
            //    if (!list.Contains(lobbyList[i]) && int.Parse(lobbyContainer.transform.GetChild(i).GetComponentsInChildren<Text>(true)[0].text) == (int)lobbyList[i].Id)
            //    {
            //        Debug.Log("DESTROY");
            //        GameObject p = lobbyContainer.transform.GetChild(i).gameObject;
            //        Destroy(p);
            //    }
            //}

            //foreach (var item in lobbyList)
            //{
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        Debug.Log(lobbyContainer.transform.GetChild(i).GetComponentsInChildren<Text>(true)[0].text);
            //        if (!list.Contains(lobbyList[i]) && int.Parse(lobbyContainer.transform.GetChild(i).GetComponentsInChildren<Text>(true)[0].text) == (int)list[i].Id)
            //        {
            //            Debug.Log("DESTROY");
            //            GameObject p = lobbyContainer.transform.GetChild(i).gameObject;
            //            Destroy(p);
            //        }
            //    }
            //}
            //}
        }
    }

    //move panel when search
    public void onClickInputField()
    {
        inviteTab.SetActive(false);
        revengeTab.SetActive(false);
        btnRandom.SetActive(false);
        lobbyScroll.SetActive(false);

        sprBgInvite.SetActive(true);

        lobbyScroll.GetComponent<RectTransform>().sizeDelta = new Vector2(809,378);
        iTween.MoveTo(lobbyScroll, iTween.Hash("y", -423, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
        iTween.MoveTo(inputField, iTween.Hash("y", 34, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
        iTween.MoveTo(sprBgInvite, iTween.Hash("y", -290, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
        iTween.MoveTo(PlayerInviNow, iTween.Hash("position", new Vector3(56, -290, 0), "islocal", true, "time", 1));
        iTween.MoveTo(queuePanel, iTween.Hash("y", -136, "islocal", true, "time", 1));
        flag = true;
        back = true;
    }

    //move panel
    public void onClickLobby()
    {
        inviteTab.SetActive(false);
        revengeTab.SetActive(false);
        btnRandom.SetActive(false);
        //lobbyScroll.SetActive(false);

        //iTween.MoveTo(lobbyScroll, iTween.Hash("y", -214, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
        iTween.MoveTo(inputField, iTween.Hash("y", 287, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
        iTween.MoveTo(PlayerInviNow, iTween.Hash("y", 307, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
        iTween.MoveTo(lobbyScroll, iTween.Hash("y", -232, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
        back = true;
    }

    //search
    public void onClickFind()
    {
        if (inputName.text.Trim() != "")
        {
            findingScroll.SetActive(true);
            for (int i = 0; i < findingContainer.transform.childCount; i++)
            {
                GameObject p = findingContainer.transform.GetChild(i).gameObject;
                Destroy(p);
            }
            BaseService.Instance.gameService.SearchPlayer(inputName.text);
            BaseService.Instance.gameService.SearchPlayerDelegate += displayWhenFind;
        }
    }

    //add search result to content panel
    public void displayWhenFind(List<PlayerData> lst)
    {
        foundLst = lst;

        foreach (var item in foundLst)
        {

            GameObject p = Instantiate(playerPrefab);
            Transform parent = findingScroll.transform.FindChild("Container");
            p.transform.SetParent(parent);
            p.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            p.GetComponentsInChildren<Text>(true)[0].text = "" + item.Id;
            p.GetComponentsInChildren<Text>(true)[1].text = item.Display.ToUpper();
            if (!item.IsOnline || item.IsGame)
            {
                p.GetComponentsInChildren<Image>(true)[0].color = Color.gray;
                p.GetComponentsInChildren<Image>(true)[3].color = Color.gray;
                p.GetComponentInChildren<Button>().enabled = false;
            }
        }
    }
    //click invite
    public void inviteClick(PlayerData p)
    {

        if (!flag)
        {
          
            Debug.Log("lobby add Q");
            onClickLobby();


            if (QLst.Count == 0)
            {
                next = false;
                QLst.Add(p);
                StartCoroutine(StartQ());
                PlayerInviNow.SetActive(true);

                //Debug.Log(QLst[0].BattleTag);

                PlayerInviNow.GetComponentsInChildren<Text>(true)[0].text = QLst[0].Display.ToUpper();
                PlayerInviNow.GetComponentsInChildren<Text>(true)[1].text = "TAG #" + QLst[0].BattleTag.ToUpper();
                PlayerInviNow.GetComponentsInChildren<Text>(true)[2].text = "LVL. " + QLst[0].Level.ToString();

                
            }
            else
            {
                next = true;
                QLst.Add(p);

                Name.SetActive(false);
                Tag.SetActive(false);
                Level.SetActive(false);
                Status.SetActive(false);

                iTween.MoveTo(PlayerInviNow, iTween.Hash("x", -56, "islocal", true, "time", 1));
                iTween.MoveTo(queuePanel, iTween.Hash("y", 431, "islocal", true, "time", 1));

                queuePanel.SetActive(true);
                GameObject go = Instantiate(queuePlayerPrefab);
                Transform parent = queueScroll.transform.FindChild("Container");
                go.transform.SetParent(parent);
                go.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
                go.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            }


        }
        else
        {
            //Debug.Log("case 2");
            //add Qlist
            if (QLst.Count == 0)
            {
                next = false;
                foreach (var item in foundLst)
                {
                    if (item.Id == (int)p.Id)
                    {
                        QLst.Add(item);
                    }
                }
                StartCoroutine(StartQ());

                tempPlayer.SetActive(true);
                tempPlayer.transform.localScale = new Vector3(1f, 1f, 1f);
                tempPlayer.transform.localPosition = new Vector3(-335, 523, 0);
                
                iTween.ScaleTo(tempPlayer, iTween.Hash("x", 2.3f, "y", 2.3f, "islocal", true, "time", 1));
                iTween.MoveTo(tempPlayer, iTween.Hash("position", new Vector3(-139, -136, 0), "islocal", true, "time", 1, "oncomplete", "SelfDestroy", "oncompletetarget", gameObject));

                for (int i = 0; i < foundLst.Count; i++)
                {
                    if (QLst[0].Id == (int)(p.Id))
                    {
                        PlayerInviNow.GetComponentsInChildren<Text>(true)[0].text = QLst[0].Display.ToUpper();
                        PlayerInviNow.GetComponentsInChildren<Text>(true)[1].text = "TAG #" + QLst[0].BattleTag.ToUpper();
                        PlayerInviNow.GetComponentsInChildren<Text>(true)[2].text = "LVL. " + QLst[0].Level.ToString();
                    }

                }
                PlayerInviNow.SetActive(true);

            }
            else
            {
                next = true;

                if (foundLst.Count == 0)
                {
                    Debug.Log("found list == 0");
                    QLst.Add(p);


                    //PlayerInviNow.SetActive(false);

                    tempPlayer.SetActive(true);
                    tempPlayer.transform.localScale = new Vector3(1f, 1f, 1f);
                    tempPlayer.transform.localPosition = new Vector3(-335, 523, 0);
                    iTween.ScaleTo(tempPlayer, iTween.Hash("x", 2f, "y", 2f, "islocal", true, "time", 1));
                    iTween.MoveTo(tempPlayer, iTween.Hash("position", new Vector3(-139, -136, 0), "islocal", true, "time", 1, "oncomplete", "SelfDestroy", "oncompletetarget", gameObject));

                    Name.SetActive(false);
                    Tag.SetActive(false);
                    Level.SetActive(false);
                    Status.SetActive(false);

                    iTween.MoveTo(PlayerInviNow, iTween.Hash("x", -56, "islocal", true, "time", 1));

                    queuePanel.SetActive(true);
                    GameObject go = Instantiate(queuePlayerPrefab);
                    Transform parent = queueScroll.transform.FindChild("Container");
                    go.transform.SetParent(parent);
                    go.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
                    go.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

                }
                else
                {
                    Debug.Log("found list > 0");
                    //next = true;
                    foreach (var item in foundLst)
                    {
                        if (item.Id == (int)p.Id)
                        {
                            QLst.Add(item);

                            //PlayerInviNow.SetActive(false);
                            Name.SetActive(false);
                            Tag.SetActive(false);
                            Level.SetActive(false);
                            Status.SetActive(false);

                            tempPlayer.SetActive(true);
                            tempPlayer.transform.localScale = new Vector3(1f, 1f, 1f);
                            tempPlayer.transform.localPosition = new Vector3(-335, 523, 0);
                            iTween.ScaleTo(tempPlayer, iTween.Hash("x", 2f, "y", 2f, "islocal", true, "time", 1));
                            iTween.MoveTo(tempPlayer, iTween.Hash("position", new Vector3(-139, -136, 0), "islocal", true, "time", 1, "oncomplete", "SelfDestroy", "oncompletetarget", gameObject));

                            iTween.MoveTo(PlayerInviNow, iTween.Hash("x", -56, "islocal", true, "time", 1));


                            queuePanel.SetActive(true);
                            GameObject go = Instantiate(queuePlayerPrefab);
                            Transform parent = queueScroll.transform.FindChild("Container");
                            go.transform.SetParent(parent);
                            go.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
                            go.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

                            //Debug.Log("bbbbbbbbbbbbb     " + next);
                            //Debug.Log("Zooooooooooooooooo");
                            //StartCoroutine(ProcessQueue());

                        }
                    }
                }
            }
        }
    }
    //Queue process
    IEnumerator StartQ()
    {
        while (QLst.Count > 0)
        {
            
            lstID.Add((int)QLst[0].Id);
            //Debug.Log("aaaaaaaaaaaaaaaaaaaaaaa    " + lstID[0]);
            BaseService.Instance.gameService.sendStartGameMulti(1, lstID, 0, QK14_2_Fee.fee);
            yield return new WaitForSeconds(20);
            QLst.Remove(QLst[0]);
            if (lstID.Count > 0)
            {
                lstID.Remove(lstID[0]);
            }

        }

    }
    
    void SelfDestroy()
    {
        tempPlayer.SetActive(false);
    }

    public void backClick()
    {
        if (back)
        {
            inviteTab.SetActive(true);
            revengeTab.SetActive(true);
            btnRandom.SetActive(true);
            lobbyScroll.SetActive(true);
            sprBgInvite.SetActive(false);

            lobbyScroll.GetComponent<RectTransform>().sizeDelta = new Vector2(809, 871);
            iTween.MoveTo(lobbyScroll, iTween.Hash("position", new Vector3(0, -172, 0), "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
            iTween.MoveTo(inputField, iTween.Hash("y", 539, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));
            iTween.MoveTo(sprBgInvite, iTween.Hash("y", -1100, "islocal", true, "time", 1, "easetype", iTween.EaseType.easeInOutQuad));

            iTween.MoveTo(PlayerInviNow, iTween.Hash("position", new Vector3(56, -290, 0), "islocal", true, "time", 1));
            iTween.MoveTo(queuePanel, iTween.Hash("y", -136, "islocal", true, "time", 1));
            PlayerInviNow.SetActive(false);

            if (findingContainer.transform.childCount > 0)
            {
                for (int i = 0; i < findingContainer.transform.childCount; i++)
                {
                    GameObject p = findingContainer.transform.GetChild(i).gameObject;
                    Destroy(p);
                }
                findingScroll.SetActive(false);
            }
            else
            {
                findingScroll.SetActive(false);
            }
            back = false;
            flag = false;
        }
        else
        {
            //Debug.Log("sao ko vao day ?");
            this.DisplayScene(false);
            QK13_Duel.SetActive(true);
        }
       
    }

    public void inviteON()
    {
        inviteTab.SetActive(true);
        revengeTab.SetActive(false);
        lobbyScroll.SetActive(true);
    }

    public void RevengeON()
    {
        BaseService.Instance.gameService.SendRequestRevengeList();
        inviteTab.SetActive(false);
        revengeTab.SetActive(true);
        lobbyScroll.SetActive(false);
    }
}
