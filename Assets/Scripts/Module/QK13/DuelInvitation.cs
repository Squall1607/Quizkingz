using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DuelInvitation : MonoBehaviour {

    //public CommonScript cs;
    public GameObject youSure;
    //public GameObject QK13_5_PreGame;
    bool flag = true;
    public Text txtName, Coin;
    public string rName;
    public Button yes;
    public GameObject bg;
    // Use this for initialization
    void Start () {
	
	}

    void Awake()
    {
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnEnable()
    {
        BaseService.Instance.gameService.responseStartGameDelegate += start2;
    }

    private void start2()
    {
        GameObject.Find("QK12Clone").GetComponent<QK12CloneInvi>().GameStart();
    }

    public void Click()
    {
        iTween.MoveTo(youSure, iTween.Hash("position", new Vector3(-69f, youSure.transform.localPosition.y, 0), "islocal", true, "time", 0.5f, "oncomplete", "hide", "oncompletetarget", gameObject));
        this.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, Time.deltaTime * 0.9f );

    }
    void hide()
    {
        bg.SetActive(false);
    }

    public void back()
    {
        iTween.MoveTo(youSure, iTween.Hash("position", new Vector3(-900f, youSure.transform.localPosition.y, 0), "islocal", true, "time", 0.5f, "oncomplete", "show", "oncompletetarget", gameObject));
        
        this.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, Time.deltaTime * 0.9f);
    }
    void show()
    {
        bg.SetActive(true);
    }

    public void acceptClick()
    {
        Debug.Log(rName);

        if (rName != null && rName != "")
        {
            BaseService.Instance.playerService.sendAcceptInvite(rName, true);
            yes.enabled = false;
        }

    }

   
}
