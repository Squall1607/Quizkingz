using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QK7_3_2_2_NeedMoreTickets : BasePageController {
	
	public Text ticket;
	public Text txtScore;
	public QK07_GameOver_Controller UC07_6_1_GameOver;
	public GameObject UC07_3_2_3_TimeOutPop;
	public QK07_2_QuestionController UC07_2_Questions;
	public CameraController cam;
	public GameObject btBuyTickets, btEnd, PanelBlurPopUp;
	public GameObject qk7_3;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable(){
		//cam.turnBlurOff();
        PanelBlurPopUp.SetActive(false);
        btBuyTickets.GetComponent<Button> ().enabled = true;
		btEnd.GetComponent<Button> ().enabled = true;

	}
	void OnEnable(){
		ticket.text = GameData.playerData.Token.ToString();
		//cam.turnBlurOn();
        PanelBlurPopUp.SetActive(true);
        //	txtScore.text = "" +GameData.playerData.Score; 
        BaseService.Instance.gameService.GameStopDelegate+= gameStopResponse;
	}

	public void btnByTicketClicked(){
		btBuyTickets.GetComponent<Button> ().enabled = false;

		BaseService.Instance.playerService.sendClientPay(false);
		BaseService.Instance.playerService.ClientPayDelegate += buyTicketSuccess;

		qk7_3.SetActive (false);
	}

	void buyTicketSuccess(bool isSkip, bool isNew){
		UC07_2_Questions.CreateData ();
		BaseService.Instance.gameService.GameStopDelegate -= gameStopResponse;
        //cam.turnBlurOff ();
        PanelBlurPopUp.SetActive(false);

    }

	public void btnEndClicked(){
		
		btEnd.GetComponent<Button> ().enabled = false;
		BaseService.Instance.gameService.sendStopGame ();
	}

	void gameStopResponse(){
        this.DisplayScene(false);
		UC07_2_Questions.DisplayScene(false);
        UC07_6_1_GameOver.DisplayScene(true);
        BaseService.Instance.gameService.GameStopDelegate-= gameStopResponse;
	}
}
