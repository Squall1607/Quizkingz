using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class QK16_Chat_Window : MonoBehaviour {

    public Text oppName, inputMsg;
    public QK12_2_Chat_Controller QK12_2_Chat_Controller;
    public GameObject myChat, oppChat,lobbyChat, lobbyChatContainer;
    public int oppID = 0;
   // public Dictionary<int, Queue> All = new Dictionary<int, Queue>();
    public bool newMsg = false;
    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        if (newMsg)
        {
            if (!string.IsNullOrEmpty(QK12_2_Chat_Controller.chatName))
            {
                oppName.text = QK12_2_Chat_Controller.chatName;
            }

            if (lobbyChatContainer.transform.childCount > 0)
            {
                for (int i = 0; i < lobbyChatContainer.transform.childCount; i++)
                {
                    GameObject p = lobbyChatContainer.transform.GetChild(i).gameObject;
                    Destroy(p);
                }
            }
        }
        else
        {
            if (lobbyChatContainer.transform.childCount > 0)
            {
                for (int i = 0; i < lobbyChatContainer.transform.childCount; i++)
                {
                    GameObject p = lobbyChatContainer.transform.GetChild(i).gameObject;
                    Destroy(p);
                }
            }
        }
       

        
        //BaseService.Instance.gameService.ChatDelegate += reciveMSG;
    }

    public void SendMsg()
    {
        if (QK12_2_Chat_Controller.chatID != 0 && !string.IsNullOrEmpty(inputMsg.text))
        {
            BaseService.Instance.gameService.sendMsg(QK12_2_Chat_Controller.chatID, inputMsg.text);

            GameObject p = Instantiate(myChat);
            Transform parent = lobbyChat.transform.FindChild("Container");
            p.transform.SetParent(parent);
            p.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            p.GetComponent<Text>().text = inputMsg.text;

          
            if (botController.lst.ContainsKey(QK12_2_Chat_Controller.chatID))
            {
                ChatData c = new ChatData();
                c.name = GameData.playerData.Display;
                c.msg = inputMsg.text;
                c.time = DateTime.UtcNow.ToShortTimeString();
                botController.lst[QK12_2_Chat_Controller.chatID].Add(c);
            }
            else
            {
                ChatData c = new ChatData();
                c.name = name;
                c.msg = inputMsg.text;
                c.time = DateTime.UtcNow.ToShortTimeString();
                List<ChatData> cdt = new List<ChatData>();
                cdt.Add(c);
                botController.lst.Add(QK12_2_Chat_Controller.chatID, cdt);

            }

            inputMsg.text = "";

        }
        else
        {
            Debug.Log("chatID null !!!");
        }
        
    }


    //public void reciveMSG(int id, string msg, string oName)
    //{
    //    //Debug.Log("dmmmmmmmmmmmmmmmmmmmmmmmm");
    //    oppID = id;
    //    oppName.text = oName;
    //    GameObject p = Instantiate(oppChat);
    //    Transform parent = lobbyChat.transform.FindChild("Container");
    //    p.transform.SetParent(parent);
    //    p.GetComponent<Text>().text = msg;
    //}
}
