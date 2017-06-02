using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class QK07popup_TimeOut_Controller : BasePageController {

    public QK7_3_2_2_NeedMoreTickets UC07_3_2_2_NeedMoreTickets;
    public QK07_2_QuestionController UC07_2_Question;
	public QK07_GameOver_Controller UC07_6_1_GameOver;
	public CameraController cam;
	public Text seconds;
	public GameObject blockNav;
	public GameObject btContinue, btEnd, PanelBlurPopUp;
	public Text txtScore;

	//public BlockUIController uiblock;
	float timeTotal = 10f;

	void Update () {
		timeTotal -= Time.deltaTime;
		//Debug.Log (timeTotal);
		if (timeTotal > 0) {
			seconds.text = (int)(timeTotal) + " Seconds";
		} else {
			seconds.text = "0 Seconds";
		}
	}

	void OnEnable(){
		timeTotal = 10f;
		//cam.turnBlurOn();
        PanelBlurPopUp.SetActive(true);
        txtScore.text = "" +GameData.playerData.Score; 

	}
	void OnDisable(){
		//cam.turnBlurOff();
        PanelBlurPopUp.SetActive(false);
        btContinue.GetComponent<Button> ().enabled = true;
		btEnd.GetComponent<Button> ().enabled = true;
	}


	public void btnEndClicked(){
		Debug.Log ("End Click");
		btEnd.GetComponent<Button> ().enabled = false;
		BaseService.Instance.gameService.GameStopDelegate+= gameStopResponse;
		BaseService.Instance.gameService.sendStopGame();
		blockNav.SetActive (false);

	}

	void gameStopResponse(){
		//uiblock.turnOffBlockUI();
		this.DisplayScene(false);
		//cam.turnBlurOff();
        PanelBlurPopUp.SetActive(false);
        UC07_2_Question.DisplayScene(false);
        UC07_6_1_GameOver.DisplayScene(true);
    }

	public void btnContinueClicked(){
		btContinue.GetComponent<Button> ().enabled = false;
		GameData.playerData.Token -=2;
		this.DisplayScene(false);
        UC07_3_2_2_NeedMoreTickets.DisplayScene(true);
        BaseService.Instance.gameService.GameStopDelegate-= gameStopResponse;

	}
}
