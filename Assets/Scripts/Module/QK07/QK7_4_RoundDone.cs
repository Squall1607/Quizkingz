using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QK7_4_RoundDone : BasePageController {

	public QK07_2_QuestionController UC07_2_Questions;
	public QK07_GameStart_Controller UC07_GameStart;
	public QK07popup_CashIn_Controller UC07_5_CashIn;
	public CameraController blur;
	public GameObject blockNav;
	public Text seconds;
	public countDownRoundDone cdto;
	public GameObject btCashin, btcontinue, btSave, PanelBlurPopUp;
	public Text txtScore;
	float timeTotal = 10f;

	void Update () {
		timeTotal -= Time.deltaTime;
		if (timeTotal > 0) {
			seconds.text = (int)(timeTotal) + " Seconds";
		} else {
			seconds.text = "0 Seconds";
		}
	}

	void OnEnable(){
		timeTotal = 10f;
		//blur.turnBlurOn ();
        PanelBlurPopUp.SetActive(true);
        //	txtScore.text = "" + GameData.playerData.Score;
        //CountDown.timeOut += this.btnContinueClicked;
    }

	void OnDisable(){
		cdto.countDownStop ();
		//blur.turnBlurOff ();
        PanelBlurPopUp.SetActive(false);

        btcontinue.GetComponent<Button> ().enabled = true;
		btCashin.GetComponent<Button> ().enabled = true;
		btSave.GetComponent<Button> ().enabled = true;
	}

	public void btnCashInClicked(){
		btCashin.GetComponent<Button> ().enabled = false;
		BaseService.Instance.gameService.ResultAskCheckPointDelegate += serverResponse;
		BaseService.Instance.gameService.sendAskCheckPoint(0);
		//uiblock.turnOnBlockUI();

	}

	public void btnContinueClicked(){
		btcontinue.GetComponent<Button> ().enabled = false;
		BaseService.Instance.gameService.sendAskCheckPoint(1);
		//uiblock.turnOnBlockUI();
		BaseService.Instance.gameService.ResultAskCheckPointDelegate += serverResponse;
		BaseService.Instance.questionService.SendQuizDelegate += UC07_2_Questions.prepareData;
		BaseService.Instance.questionService.SendQuizDelegate += continueResponse;

	}
		
	public void btnSaveClicked(){
		btSave.GetComponent<Button> ().enabled = false;
		BaseService.Instance.gameService.ResultAskCheckPointDelegate += serverResponse;
		BaseService.Instance.gameService.sendAskCheckPoint(2);
		//uiblock.turnOnBlockUI();
	}

	void serverResponse(int answer){
		//uiblock.turnOffBlockUI();
		switch(answer){
		case 0:
			this.DisplayScene(false);
                UC07_5_CashIn.DisplayScene(true);
                break;
		case 2:
			this.DisplayScene(false);
                UC07_2_Questions.DisplayScene(false);
                UC07_GameStart.DisplayScene(true);
                break;
		}
	}

	void continueResponse(QuestionData d){
		this.DisplayScene(false);
        UC07_2_Questions.CreateData ();
	}

}
