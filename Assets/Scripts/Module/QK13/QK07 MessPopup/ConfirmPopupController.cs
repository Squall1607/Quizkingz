using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfirmPopupController : BasePageController
{
    public CameraController cam;
    public Button btOK;
    public GameObject UC07_6_1_GameOver;
    public GameObject UC07_2_Questions;
    public GameObject LeavePopUp, PanelBlurPopUp;
    public countDown countdown;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void btOKclick() {
        btOK.enabled = false;
        
        StartCoroutine(countdown.countDownStop());
        BaseService.Instance.gameService.GameStopDelegate += gameStopResponse;
        BaseService.Instance.gameService.sendStopGame();

        LeavePopUp.SetActive(false);
        UC07_2_Questions.SetActive(false);
        UC07_6_1_GameOver.SetActive(true);
    }

    void gameStopResponse()
    {
        BaseService.Instance.gameService.GameStopDelegate -= gameStopResponse;
    }

    void OnEnable()
    {
        //cam.turnBlurOn();
        PanelBlurPopUp.SetActive(true);
        btOK.enabled = true;
    }

    void OnDisable()
    {
        //cam.turnBlurOff();
        PanelBlurPopUp.SetActive(false);
        btOK.enabled = true;

    }
}
