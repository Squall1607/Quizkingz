using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.UI;

public class botController : MonoBehaviour {

    public QK14_2_Fee qk14_2_fee;
    public string id;
    public CommonScript cs;
    public GameObject QK13_PreGame, QK16_Chat_Window, mess,newMSG,myChat,oppChat, lobbyChat;
    //public QK16_Chat_Window QK16_Chat_Window;
    public List<int> lstID;
    public static Dictionary<int, List<ChatData>> lst = new Dictionary<int, List<ChatData>>();

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnEnable()
    {
        BaseService.Instance.gameService.AnswerInvitationDelegate += check;
        BaseService.Instance.gameService.ChatDelegate += CreateChat;

        //GameObject p = Instantiate(newMSG);
        //Transform parent = mess.transform.FindChild("MessScroll");
        //p.transform.SetParent(parent);
        //p.GetComponent<RectTransform>().localPosition = new Vector3(p.GetComponent<RectTransform>().localPosition.x, p.GetComponent<RectTransform>().localPosition.y, 0f);
        //p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

       
    }

    void check(bool isaAccept)
    {
        if (isaAccept)
        {
            
            BaseService.Instance.gameService.responseStartGameDelegate += start2;
        }
    }

    void start2()
    {
        //Debug.Log("zoooooooooooooooooooooooooooooooooooo");
        cs.turnOffAll(QK13_PreGame);

    }

    public void feeClick()
    {
        qk14_2_fee.DisplayScene(true);
        qk14_2_fee.checkRevenge = true;
    }

    void CreateChat(int id , string oppMsg, string oppName)
    {
        if (!lst.ContainsKey(id))
        {
            GameObject p = Instantiate(newMSG);
            Transform parent = mess.transform.FindChild("MessScroll");
            p.transform.SetParent(parent);
            p.GetComponentsInChildren<Text>(true)[2].text = id.ToString();

            ChatData c = new ChatData();
            c.name = name;
            c.msg = oppMsg;
            c.time = DateTime.UtcNow.ToShortTimeString();
            List<ChatData> cdt = new List<ChatData>();
            cdt.Add(c);
            lst.Add(id, cdt);
          
        }
        else
        {
            ChatData c = new ChatData();
            c.name = name;
            c.msg = oppMsg;
            c.time = DateTime.UtcNow.ToShortTimeString();
            lst[id].Add(c);

        }
        
    }
    
    public void loadConver(int id)
    {
        cs.turnOffAll(QK16_Chat_Window);

        foreach (var item in lst[id])
        {
            //if (GameData.playerData.Id == 0)
            //{

            //}

            GameObject p = Instantiate(myChat);
            Transform parent = lobbyChat.transform.FindChild("Container");
            p.transform.SetParent(parent);
            p.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            p.GetComponent<Text>().text = item.msg;
        }
    }

}
