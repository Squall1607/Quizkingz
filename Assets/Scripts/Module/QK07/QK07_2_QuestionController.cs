using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QK07_2_QuestionController : BasePageController
{

	public Text lblQuestionNumber;
	public Text lblQuestion;
	public Text lblBonus;
	public Text lblCorrect;

	public GameObject headerBg;
	public List<GameObject> ListAnswers;
	public List<GameObject> wrongList;
	public List<GameObject> correctList;
	public List<GameObject> chooseList;
	//public List<GameObject> textList;
	public GameObject goQuestion;
	public GameObject Wrong;
	public GameObject Correct;
	public GameObject Header;
	public GameObject Bottom;
	public GameObject questionContent;

	public GameObject uc07_3_2_1_Incorrect;
	public GameObject uc07_6_GameOver;
	public GameObject UC7_4_RoundDone;
	public Text seconds;
	//blic GameObject Blur;

	QuestionData question;
	int choosedAnswer;

	public countDown countdown;

	bool flag = false;
	float timeTotal;

	List<Sprite> listSprite = new List<Sprite> ();

	void Update ()
	{
		if (flag) {
			seconds.text = (int)(timeTotal) + " Seconds";
		} else {
			timeTotal -= Time.deltaTime;
			if (timeTotal > 0) {
				seconds.text = (int)(timeTotal) + " Seconds";
			} else {
				seconds.text = "0 Seconds";
			}
		}
	}

	void OnEnable ()
	{
		CreateData ();
	}


	public void onChooseAnswerClick (GameObject button)
	{
		
		checkClickedButton (button.transform.name);
	}

	public void onConfirmAnswerClick ()
	{
		Debug.Log ("clicked!");
		BaseService.Instance.questionService.AnswerDelegate += checkAnswerResponse;
		BaseService.Instance.questionService.SendQuizDelegate += prepareData;
		BaseService.Instance.questionService.sendAnswer (question.AnswerList [choosedAnswer]);
		//uiblock.turnOnBlockUI();
	}

	void checkClickedButton (string buttonName)
	{
		switch (buttonName) {
		case "InputField":
			choosedAnswer = 0;
			break;
		case "InputField1":
			choosedAnswer = 1;
			break;
		case "InputField2":
			choosedAnswer = 2;
			break;
		case "InputField3":
			choosedAnswer = 3;
			//Debug.Log ("abc");
			break;
		}
		hideList (ListAnswers);
		displayList (chooseList);
	}

	void hideList (List<GameObject> list)
	{
		for (int i = 0; i < list.Count; i++) {
			GameObject g = list [i];
			if (i == choosedAnswer) {
				g.SetActive (false);
			} else {
				g.SetActive (true);
			}
		}
	}

	void displayList (List<GameObject> list)
	{
		if (list == chooseList) {
			for (int i = 0; i < list.Count; i++) {
				GameObject g = list [i];
				if (i == choosedAnswer) {
					
					g.SetActive (true);

					switch (i) {
					case 0:
						iTween.MoveTo (g, iTween.Hash ("position", new Vector3 (-110f, g.transform.localPosition.y, 0), "islocal", true, "time", 0.5f, "oncomplete", "enableButton", "oncompletetarget", gameObject));
						break;
					case 1:
						iTween.MoveTo (g, iTween.Hash ("position", new Vector3 (-110f, g.transform.localPosition.y, 0), "islocal", true, "time", 0.5f, "oncomplete", "enableButton", "oncompletetarget", gameObject));
						break;
					case 2:
						iTween.MoveTo (g, iTween.Hash ("position", new Vector3 (-110f, g.transform.localPosition.y, 0), "islocal", true, "time", 0.5f, "oncomplete", "enableButton", "oncompletetarget", gameObject));
						break;
					case 3:
						iTween.MoveTo (g, iTween.Hash ("position", new Vector3 (-110f, g.transform.localPosition.y, 0), "islocal", true, "time", 0.5f, "oncomplete", "enableButton", "oncompletetarget", gameObject));
						break;
					default:
						break;
					}


				} else {
					//textList [i].GetComponent<Button> ().enabled = false;
					iTween.Stop (g, "MoveTo");
					g.transform.localPosition = new Vector3 (-900f, g.transform.localPosition.y, g.transform.localPosition.z);
					g.SetActive (false);


				}
			}

		} else {
			for (int i = 0; i < list.Count; i++) {
				GameObject g = list [i];
				if (i == choosedAnswer) {
					g.SetActive (true);
					chooseList [i].SetActive (false);
				} else {
					g.SetActive (false);
					//chooseList[i].SetActive(true);
				}
			}
		}
	}

	void enableButton ()
	{
//		foreach (var item in textList) {
//			item.GetComponent<Button> ().enabled = true;
//		}	
	}

	void checkAnswerResponse (bool result, int bonus, int score, int currQuiz)
	{
		
		if (result) {
			
			displayList (correctList);
			StartCoroutine (countdown.countDownStop ());
			flag = true;

			Wrong.SetActive (false);
			Correct.SetActive (true);
			lblBonus.text = "" + GameData.playerData.BonusPoint;
			lblCorrect.text = "" + GameData.playerData.CorrectPoint;
			Header.SetActive (false);
			Bottom.SetActive (false);

			for (int i = 0; i < ListAnswers.Count; i++) {
				GameObject g = ListAnswers [i];
				if (i == choosedAnswer) {
					g.GetComponent<Button> ().enabled = true;
				} else {
					g.GetComponent<Button> ().enabled = false;
				}
			}


			if (GameData.currentQuestion % 5 == 0) {
				
				Invoke ("displayRoundDone", 1.5f);
			} else {
				Invoke ("displayNext", 1.5f);
			}

		} else {

			displayList (wrongList);
			StartCoroutine (countdown.countDownStop ());
			flag = true;
			;

			Wrong.SetActive (true);
			Correct.SetActive (false);
			Header.SetActive (false); 
			Bottom.SetActive (false);

			for (int i = 0; i < ListAnswers.Count; i++) {
				GameObject g = ListAnswers [i];
				if (i == choosedAnswer) {
					g.GetComponent<Button> ().enabled = true;
				} else {
					g.GetComponent<Button> ().enabled = false;
				}
			}

			Invoke ("displayWrongPopup", 1.5f);
			//uc07_3_2_1.SetActive(true);
			//this.SceneSwitch();
		}
	}

	public void prepareData (QuestionData q)
	{
		GameData.question = q;
	}

	public void CreateData ()
	{
		countdown.countDownBack ();
		countdown.orange.SetActive (false);
		flag = false;
		timeTotal = GameData.time + 1;
		Header.SetActive (true);
		Wrong.SetActive (false);
		Correct.SetActive (false);
		Bottom.SetActive (true);



		for (int i = 0; i < ListAnswers.Count; i++) {
			ListAnswers [i].GetComponent<Button> ().enabled = true;
		}

		choosedAnswer = -1;
		for (int i = 0; i < chooseList.Count; i++) {
			chooseList [i].transform.localPosition = new Vector3 (-900f, chooseList [i].transform.localPosition.y, chooseList [i].transform.localPosition.z);
			chooseList [i].SetActive (false);
			wrongList [i].SetActive (false);
			correctList [i].SetActive (false);
			ListAnswers [i].SetActive (true);
		}

		question = GameData.question;
		lblQuestionNumber.text = "Question " + GameData.currentQuestion;
		lblQuestion.text = question.Content;
		Debug.Log ("ques: " + lblQuestion.text);
		for (int i = 0; i < QK07_GameStart_Controller.listImage.Count; i++) {
			if (GameData.currentQuestion >= 6) {
				int n = GameData.currentQuestion / 5;
				if (QK07_GameStart_Controller.listImage [GameData.currentQuestion - 1 - 5 * n].Length != 0) {
					StartCoroutine (Utils.LoadImage (QK07_GameStart_Controller.listImage [i], value => headerBg.GetComponent<Image> ().sprite = value));
//					questionContent.GetComponent<RectTransform> ().position = new Vector3 (-1, 29, 0);		
				} else {
					headerBg.SetActive (false);
//					questionContent.GetComponent<RectTransform> ().position = new Vector3 (-1, 277, 0);				
				}
			} else {
				if (QK07_GameStart_Controller.listImage [GameData.currentQuestion - 1].Length != 0) {
					StartCoroutine (Utils.LoadImage (QK07_GameStart_Controller.listImage [i], value => headerBg.GetComponent<Image> ().sprite = value));
//					questionContent.GetComponent<RectTransform> ().position = new Vector3 (-1, 29, 0);	
				} else {
					headerBg.SetActive (false);
//					questionContent.GetComponent<RectTransform> ().position = new Vector3 (-1, 277, 0);			
				}
			}
		}

		for (int i = 0; i < ListAnswers.Count; i++) {
			ListAnswers [i].GetComponent<Text> ().text = question.AnswerList [i];
			//Debug.Log (question.AnswerList [i]);
		}

		countdown.countDownStart (GameData.time);
	}

	public void onPointDown ()
	{
		Debug.Log ("onPointDown");
		goQuestion.SetActive (false);
	}

	public void onPointUp ()
	{
		Debug.Log ("onPointUp");
		goQuestion.SetActive (true);
	}

	public void btnNextClick ()
	{
		if (question != GameData.question) {
			this.CreateData ();
		}
	}

	public void btnSkipClick ()
	{
		StartCoroutine (countdown.countDownStop ());
		BaseService.Instance.questionService.SendQuizDelegate += prepareData;
		BaseService.Instance.questionService.SendQuizDelegate += buyTicketSuccess;
		BaseService.Instance.playerService.sendClientPay (true);
		//uiblock.turnOnBlockUI();
	}

	void buyTicketSuccess (QuestionData q)
	{
		//uiblock.turnOffBlockUI();
		question = GameData.question;
		this.CreateData ();
	}

	void displayWrongPopup ()
	{
		uc07_3_2_1_Incorrect.SetActive (true);
	}

	void displayRoundDone ()
	{
		UC7_4_RoundDone.SetActive (true);
	}

	void displayNext ()
	{
		if (question != GameData.question) {
			this.CreateData ();
		}
	}

}
