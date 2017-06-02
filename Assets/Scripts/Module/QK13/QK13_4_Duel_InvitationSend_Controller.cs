using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QK13_4_Duel_InvitationSend_Controller : BasePageController
{
    public CommonScript cs;
    public CameraController blur;
    public GameObject txtWatiting,txtdenied,txtYou;
    public GameObject QK13_PreGame,QK13_3_FindOppo,overLay, PanelBlurPopUp;
	public static bool isAccept = false;

    bool flag = false;
    float time = 0f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if (time > 0)
        {
            txtWatiting.GetComponent<Text>().text = " Waiting response in "+ (int)(time);
        }


        if (time<0 && !flag)
        {
            flag = true;
            //this.SceneSwitch();
        }
    }

    void OnEnable()
    {
        txtWatiting.SetActive(true);
        txtdenied.SetActive(false);
        txtYou.SetActive(true);
        overLay.SetActive(true);

        if (DuelData.senderTime>0)
        {
            time = DuelData.senderTime;
        }
        
        //blur.turnBlurOn();
        PanelBlurPopUp.SetActive(true);
        BaseService.Instance.gameService.AnswerInvitationDelegate += check;
    }

    void check(bool isaAccept)
    {
		isAccept = isaAccept;

        if (isaAccept)
        {
            BaseService.Instance.gameService.responseStartGameDelegate += start2;
        }
        else
        {
            txtWatiting.SetActive(false);
            txtdenied.SetActive(true);
            txtYou.SetActive(false);
            overLay.SetActive(false);
			//dismiss avatar player 

            //this.SceneSwitch();
        }
    }

    //void startGame() {
    //    Invoke("start2",2f);
       
    //}

    void start2() {
        Debug.Log("zoooooooooooooooooooooooooooooooooooo");
        this.DisplayScene(false);
        cs.turnOffAll(QK13_PreGame);

    }

    void OnDisable()
    {
        //blur.turnBlurOff();
        PanelBlurPopUp.SetActive(false);
    }


}
