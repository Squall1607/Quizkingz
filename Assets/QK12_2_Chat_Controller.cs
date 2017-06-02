using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QK12_2_Chat_Controller : MonoBehaviour {

    public Text inputName;
    public GameObject playerPrefab, lobbyContainer , PlayerInviNow;
    public GameObject lobby , facebooktab , recentTab;
    public List<PlayerData> foundLst = new List<PlayerData>();
    public Button StartChat;
    public int chatID = 0;
    public string chatName;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        StartChat.interactable = false;
    }

    //search
    public void onClickFind()
    {
        if (!string.IsNullOrEmpty(inputName.text.Trim()))
        {
            print(inputName.text);
            lobby.SetActive(true);
            facebooktab.SetActive(false);
            recentTab.SetActive(false);

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
            Transform parent = lobby.transform.FindChild("Container");
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

    public void Display(PlayerData p)
    {
        PlayerInviNow.SetActive(true);
        chatID = (int)p.Id;
        chatName = p.Display;
        PlayerInviNow.GetComponentsInChildren<Text>(true)[0].text = p.Display.ToUpper();
        PlayerInviNow.GetComponentsInChildren<Text>(true)[1].text = "TAG #" + p.BattleTag.ToUpper();
        PlayerInviNow.GetComponentsInChildren<Text>(true)[2].text = "LVL. " + p.Level.ToString();

        clearLobby();
        StartChat.interactable = true;

        lobby.SetActive(false);
        facebooktab.SetActive(false);
        recentTab.SetActive(true);

    }

    void clearLobby()
    {
        if (lobbyContainer.transform.childCount > 0)
        {
            for (int i = 0; i < lobbyContainer.transform.childCount; i++)
            {
                GameObject p = lobbyContainer.transform.GetChild(i).gameObject;
                Destroy(p);
            }
        }
    }

    public void fbClick()
    {

        facebooktab.SetActive(true);
        recentTab.SetActive(false);
    }

    public void recentClick()
    {

        facebooktab.SetActive(false);
        recentTab.SetActive(true);
    }


}
