using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class newInvitePopupController : BasePageController
{
    public CommonScript cs;

    public CameraController blur;
    public Text txtName, txtReply;
    float time = 0f;
    public GameObject QK13_PreGame, PanelBlurPopUp;
    public Button btAccept,btDeny;
    bool flag = false;
    // Use this for initialization
    void Start()
    {
        BaseService.Instance.gameService.responseStartGameDelegate += start2;
        ///BaseService.Instance.questionService.SendQuizDelegate += start2;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time > 0)
        {
            txtReply.text = " Reply in " + (int)(time);
        }


        if (time < 0 && !flag)
        {
            flag = true;
            //this.SceneSwitch();
        }
    }

    void OnEnable()
    {
        btAccept.enabled = true;
        btDeny.enabled = true;
        PanelBlurPopUp.SetActive(true);
        if (DuelData.receiverTime > 0)
        {
            time = DuelData.receiverTime;
        }

        //blur.turnBlurOn();
        txtName.text = DuelData.ownerName.ToString();
       
    }

    void OnDisable()
    {
        //blur.turnBlurOff();
        PanelBlurPopUp.SetActive(false);
    }

    public void acceptClick()
    {
        if (DuelData.rName != null)
        {
            BaseService.Instance.playerService.sendAcceptInvite(DuelData.rName, true);
            btAccept.enabled = false;
            btDeny.enabled = false;
        }
        
    }

    public void denyClick()
    {
        if (DuelData.rName != null)
        {
            BaseService.Instance.playerService.sendAcceptInvite(DuelData.rName, false);
            btDeny.enabled = false;
            btAccept.enabled = false;
        }
    }

    //void turnOnStartGame(QuestionData q)
    //{
    //    Invoke("start2",2f);
    //}

    void start2() {
        //Debug.Log("vaoooooooooooooooooooooooooooooooooooooooooooooooo");
        this.DisplayScene(false);
        cs.turnOffAll(QK13_PreGame);
    }
}
