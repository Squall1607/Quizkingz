 using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QK07_3_2_1_IncorrectAnswer_Controller : BasePageController {
	public Text ticket;
	public Text txtScore;
	public GameObject UC07_6_1_GameOver;
	public GameObject UC07_3_2_3_TimeOutPop;
	public GameObject UC07_2_Questions;
	public GameObject UC07_3_2_2_NeedMoreTickets;
	public QK07_2_QuestionController UC07_2;
	public QK7_3_2_2_NeedMoreTickets QK07_3_2_2;
	//public countDownTimeOut cdto;
	public CameraController cam;
	public Text seconds;
	float timeTotal = 10f;
	public Image img;
	public GameObject blockNav;
	public GameObject btEnd, PanelBlurPopUp;
	//public BlockUIController uiblock;


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
        ticket.text = GameData.playerData.Token.ToString();
		txtScore.text = GameData.playerData.Score.ToString();

	}

	void displayScore(bool status,int bonusScore, int score, int currQuiz ){
		Debug.Log ("score: "+ score);
		int totalScore = score;
		int time = bonusScore;
		txtScore.text = "" + totalScore;
		Debug.Log ("totalscore: "+ txtScore.text);
	}

	void OnDisable(){
		//cam.turnBlurOff();
        PanelBlurPopUp.SetActive(false);
        btEnd.GetComponent<Button> ().enabled = true;
	}
		

	public void btnContinueClicked(){
		this.DisplayScene(false);
		QK07_3_2_2.btnByTicketClicked ();
		GameData.playerData.Token -= 2;
	}

	public void btnEndClicked(){
		Debug.Log ("End clicked!");
		btEnd.GetComponent<Button> ().enabled = false;
		this.DisplayScene (false);

		BaseService.Instance.gameService.GameStopDelegate+= gameStopResponse;
		BaseService.Instance.gameService.sendStopGame();
		UC07_2_Questions.SetActive (false);
		UC07_6_1_GameOver.SetActive(true);
		blockNav.SetActive (false);
	}



	void gameStopResponse(){
		
		BaseService.Instance.gameService.GameStopDelegate -= gameStopResponse;

	}
}
