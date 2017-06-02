using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class QK07_GameOver_Controller : BasePageController {
	
	public GameObject SwitchScreen,navbarPopup,fakeMain;
	public GameObject blockNav;
	public QK04B1_3_GetTickets_Controller QK04B1_3_TicketShop;
	public NavigationBarController navigation;
	public GameObject UIShop;
	public QK07_GameStart_Controller QK07_GameStart;

	public Text txtTScore;
	public Text txtScoreLevelProg;
	public int Score = 0;
	public int ScoreBonus = 0;
	private int TotalScore;
	public GameObject SCORE;
	public GameObject SCOREBONUS;
	public Text txtScoreNumber;
	public Text txtScoreBonusNumber;
	public Text questAnswer;
	public Image imgScore;
	public Image imgScoreBonus;
	public GameObject theBest;
	public static bool isActive = false;
	public float time = 0;
	public float dem1 = 1;
	public float dem2 = 1;
	public float time1 = 0;
	int speed = 30;
	int speed1 = 60;
	private float FScoreBonus;
	private float FTotalScore;
	private float ftime;
	private int iScore;
	private int iScoreBonus;

	void Update () {
		questAnswer.text = "" + GameData.questAnswered;
		if (GameData.weekBest) {
			theBest.SetActive (true);
		} else {
			theBest.SetActive (false);
		}
		Score = GameData.score;
//	Score = 30;
		if (Score == 0) {
			imgScore.fillAmount = 0;
			txtScoreLevelProg.text = "+0";
			txtScoreNumber.text = "0";
		}
		ScoreBonus = GameData.scoreBonus;
//	ScoreBonus = 250;
		if(ScoreBonus == 0){
			imgScoreBonus.fillAmount = 0;
		}
		TotalScore = Score + ScoreBonus;
		txtTScore.text = "" + (Score + ScoreBonus);
		FScoreBonus = (float)ScoreBonus;
		FTotalScore = (float)TotalScore;
		if (Score > 0) {
			dem1 += Time.deltaTime*4;
			time += Time.deltaTime * speed*dem1;
		}
		if (time < Score + 1) {
			iScore = (int)time;
			txtScoreNumber.text = iScore.ToString ();
			txtScoreLevelProg.text = "+" + iScore.ToString ();
			ftime = (float)time;
			imgScore.fillAmount = (ftime-1) / FTotalScore;
		} else if(time>Score+50 && ScoreBonus>0) {
			dem2 += Time.deltaTime*4;
			time1 +=  Time.deltaTime*speed1*dem2;
			SCORE.SetActive (false);
			SCOREBONUS.SetActive (true);
			txtScoreLevelProg.text = "+" + (Score+iScoreBonus).ToString ();
		}
		if (time1 < ScoreBonus+1) {
			if(ScoreBonus==0){
				imgScoreBonus.fillAmount = 0;
			}else{
			iScoreBonus = (int)time1;
			txtScoreBonusNumber.text = iScoreBonus.ToString ();
			float ftime1 = (float)time1;
				imgScoreBonus.fillAmount = imgScore.fillAmount + ftime1 / FTotalScore;
			}
		}
		if (time > Score + 1 && time1 < 1){
			txtScoreNumber.text = Score.ToString ();
			txtScoreLevelProg.text = "+" + Score.ToString ();
		}
		if (time1 > ScoreBonus) {
			txtScoreLevelProg.text = "+" + (Score+ScoreBonus).ToString ();
			txtScoreBonusNumber.text = ScoreBonus.ToString ();
			imgScoreBonus.fillAmount = 1;
		}

	}
	
	// Update is called once per frame
	void OnDisable(){
		dem1 = 1;
		dem2 = 1;
		time = 0;
		time1 = 0;
		Score = 0;
		ScoreBonus = 0;
	}
	void OnEnable(){
		SCORE.SetActive (true);
		SCOREBONUS.SetActive (false);
		blockNav.SetActive (false);

        //PlayerSettings.statusBarHidden = false;
    }

	public void retryClick(){
		if (GameData.playerData.Token <= 0) {
			QK07_GameStart.DisplayScene(true);
            QK04B1_3_TicketShop.DisplayScene(false);
			navigation.clicked(UIShop);
		} else {

			isActive = true;
			QK07_GameStart.Switch ();
			this.DisplayScene (false);
			//navbar.SetActive (false);
			//navbarPopup.SetActive (true);
			fakeMain.SetActive (false);
			blockNav.SetActive (false);
		}
	}




		
}
