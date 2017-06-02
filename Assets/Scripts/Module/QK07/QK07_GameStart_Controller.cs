using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;

public class QK07_GameStart_Controller : BasePageController  {

	//public BlockUIController uiBlock;
	public GameObject QK07_2_Question;
	public GameObject QK07_3_2_2;
	public QK07_GameStart_Controller QK07_0_GameStart;
	public NavigationBarController navigation;
	public GameObject UIShop;
	public QK04B1_3_GetTickets_Controller QK04B1_3_TicketShop;
	public GameObject SwitchScreen,fakeMain;
	//public GameObject UC10_1_1_GetTickets;
	public GameObject blockNav;
	public GameObject UIMainPopup;
	//public GameObject UIMain;
	public Button startGame;
	//public CameraController blur;
	public GameObject headerBg;
	public static List<string> listImage = new List<string>();
	public static int pack = 0;
	public Text ticket;
	public Text tips;
	public GameObject sceneGetTicket, PanelBlurPopUp;

	public Text previousScore;
	public Text weeklyBest;
	public Text alltimeBest;

	//public GameObject timeBar;
	void OnEnable(){
		startGame.interactable = true;
		previousScore.text = GameData.playerData.LastScore.ToString();
		weeklyBest.text = GameData.playerData.WeeklyBest.ToString ();
		alltimeBest.text = GameData.playerData.BestScore.ToString ();
		ticket.text = GameData.playerData.Token.ToString();
		if (UIMainPopup.activeSelf) {
			UIMainPopup.GetComponent<Button> ().enabled = false;
		}
  //      else if (UIMain.activeSelf){
		//	UIMain.GetComponent<Button> ().enabled = false;
		//}
		tips.text = "Tip: " + GameData.tips;
	}

	void OnDisable(){
		if (UIMainPopup.activeSelf) {
			UIMainPopup.GetComponent<Button> ().enabled = true;
		}
  //      else if (UIMain.activeSelf) {
		//	UIMain.GetComponent<Button> ().enabled = true;
		//}
	}


	public void Switch(){	
		Debug.Log ("Ticket: "+ GameData.playerData.Token);	
		if (GameData.playerData.Token <= 0) {
			QK07_0_GameStart.DisplayScene(true);
            QK04B1_3_TicketShop.DisplayScene(false);
//			fakeMain.SetActive (false);
			navigation.clicked(UIShop);
		} else {
			GameData.playerData.Token -= 1;
			SwitchScreen.SetActive (true);
			Invoke ("clickStartSoloGame", 1.5f);
			startGame.interactable = false;
		}
	}

	void buyTicket(){
        //blur.turnBlurOn ();
        PanelBlurPopUp.SetActive(true);

        QK07_3_2_2.SetActive (true);
	}

	public void clickStartSoloGame(){
//#if UNITY_EDITOR
//        // Editor specific code here
//        UnityEditor .PlayerSettings.statusBarHidden = true;
//#endif

		GameData.playerData.Score = 0;

        fakeMain.SetActive(true);
		//navbar.SetActive(true);
		//navbarPopup.SetActive(false);
		blockNav.SetActive (true);

		BaseService.Instance.gameService.sendStartGameSolo(false);
		BaseService.Instance.questionService.SendQuizDelegate += QK07_2_Question.GetComponent<QK07_2_QuestionController>().prepareData;
		BaseService.Instance.questionService.SendLstQuizDelegate += GetImageURL;
		BaseService.Instance.questionService.SendQuizDelegate += prepareToChangeScene;

	}

	public void GetImageURL(List<QuestionData>q){
//		List<string> listImage = new List<string> ();
		for (int i = 0; i < q.Count; i++) {
			if (q [i].imageURL.Length != 0) {
				listImage.Add (q [i].imageURL);
			} else {
				listImage.Add ("");
			}


//			StartCoroutine (displayImage(listImage[i]));
		}
	}



	private void prepareToChangeScene(QuestionData q){
        DisplayScene(false);
		if(QK07_GameOver_Controller.isActive){
            DisplayScene(false);
			QK07_GameOver_Controller.isActive = false;
		}
		QK07_2_Question.GetComponent<QK07_2_QuestionController>().DisplayScene(true);
	}


}
