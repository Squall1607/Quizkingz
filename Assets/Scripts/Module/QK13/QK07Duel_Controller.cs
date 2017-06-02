using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QK07Duel_Controller : BasePageController
{

    public Text lblQuestionNumber;
    public Text lblQuestion;

    public List<GameObject> ListAnswers;
    public List<GameObject> wrongList;
    public List<GameObject> correctList;
    public List<GameObject> chooseList;
    public List<Button> YESLst;

    public GameObject Wrong;
    public GameObject Correct;
    public GameObject Header;
    public GameObject Bottom, TimeOut;

    public GameObject QK13_5_1_win, QK13_5_1_2_Lose, QK13_5_2_DRAW;
    public QK13_7_MatchOver_Controller QK13_7_MatchOver_Controller;
    public QK13_5_2_DRAW QK13_5_2;
    public GameObject QK13_7, QK07Duel;
    public Text seconds;
    public GameObject Blur;
    public GameObject SKIPNOPENALTY, SKIP1Point;

    QuestionData question;
    int choosedAnswer;
    public  int lose = 0;
    public  int win = 0;

    public DuelCountDown DuelCountDown;

    bool flag = false;


    float timeTotal;

    void Update()
    {

        if (flag)
        {
            seconds.text = (int)(timeTotal) + " Seconds";
        }
        else
        {
            timeTotal -= Time.deltaTime;
            if (timeTotal > 0)
            {
                seconds.text = (int)(timeTotal) + " Seconds";

            }
            else
            {
                seconds.text = "0 Seconds";

            }
        }

    }

    void OnEnable()
    {
       
        CreateData();
        BaseService.Instance.gameService.EndRoundDelegate += displayRoundDone;
        BaseService.Instance.gameService.GameStopDelegate += RoundEnd;

    }

    public void onChooseAnswerClick(GameObject button)
    {

        checkClickedButton(button.transform.name);
    }

    public void onConfirmAnswerClick()
    {

        //if (!flag)
        //{
        BaseService.Instance.questionService.AnswerDelegate += checkAnswerResponse;
        BaseService.Instance.questionService.SendQuizDelegate += prepareData;
        BaseService.Instance.questionService.sendAnswer(question.AnswerList[choosedAnswer]);
        YESLst[choosedAnswer].enabled = false;
        //}
    }

    void checkClickedButton(string buttonName)
    {
        switch (buttonName)
        {
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
        hideList(ListAnswers);
        displayList(chooseList);
    }

    void hideList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject g = list[i];
            if (i == choosedAnswer)
            {
                g.SetActive(false);
            }
            else
            {
                g.SetActive(true);
            }
        }
    }

    void displayList(List<GameObject> list)
    {
        if (list == chooseList)
        {
            for (int i = 0; i < list.Count; i++)
            {
                GameObject g = list[i];
                if (i == choosedAnswer)
                {

                    g.SetActive(true);

                    switch (i)
                    {
                        case 0:
                            iTween.MoveTo(g, iTween.Hash("position", new Vector3(-110f, g.transform.localPosition.y, 0), "islocal", true, "time", 0.5f, "oncomplete", "enableButton", "oncompletetarget", gameObject));
                            break;
                        case 1:
                            iTween.MoveTo(g, iTween.Hash("position", new Vector3(-110f, g.transform.localPosition.y, 0), "islocal", true, "time", 0.5f, "oncomplete", "enableButton", "oncompletetarget", gameObject));
                            break;
                        case 2:
                            iTween.MoveTo(g, iTween.Hash("position", new Vector3(-110f, g.transform.localPosition.y, 0), "islocal", true, "time", 0.5f, "oncomplete", "enableButton", "oncompletetarget", gameObject));
                            break;
                        case 3:
                            iTween.MoveTo(g, iTween.Hash("position", new Vector3(-110f, g.transform.localPosition.y, 0), "islocal", true, "time", 0.5f, "oncomplete", "enableButton", "oncompletetarget", gameObject));
                            break;
                        default:
                            break;
                    }


                }
                else
                {
                    //textList [i].GetComponent<Button> ().enabled = false;
                    iTween.Stop(g, "MoveTo");
                    g.transform.localPosition = new Vector3(-900f, g.transform.localPosition.y, g.transform.localPosition.z);
                    g.SetActive(false);


                }
            }

        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                GameObject g = list[i];
                if (i == choosedAnswer)
                {
                    g.SetActive(true);
                }
                else
                {
                    g.SetActive(false);
                }
            }
        }
    }

	void checkAnswerResponse(bool result, int bonus, int score, int currQuiz)
    {

        if (result)
        {

            displayList(correctList);
            StartCoroutine(DuelCountDown.countDownStop());
            flag = true;

            Wrong.SetActive(false);
            Correct.SetActive(true);
            Header.SetActive(false);
            Bottom.SetActive(false);

            for (int i = 0; i < ListAnswers.Count; i++)
            {
                GameObject g = ListAnswers[i];
                if (i == choosedAnswer)
                {
                    g.GetComponent<Button>().enabled = true;
                }
                else
                {
                    g.GetComponent<Button>().enabled = false;
                }
            }

            YESLst[choosedAnswer].enabled = true;

            if (GameData.currentQuestion % 5 == 0)
            {

                //Invoke("displayRoundDone", 1.5f);
            }
            else
            {
                Invoke("displayNext", 1.5f);
            }

        }
        else
        {

            displayList(wrongList);
            StartCoroutine(DuelCountDown.countDownStop());
            flag = true;

            Wrong.SetActive(true);
            Correct.SetActive(false);
            Header.SetActive(false);
            Bottom.SetActive(false);

            for (int i = 0; i < ListAnswers.Count; i++)
            {
                GameObject g = ListAnswers[i];
                if (i == choosedAnswer)
                {
                    g.GetComponent<Button>().enabled = true;
                }
                else
                {
                    g.GetComponent<Button>().enabled = false;
                }
            }
            YESLst[choosedAnswer].enabled = true;
            //Invoke("displayWrongPopup", 1.5f);
            Invoke("displayNext", 1.5f);
            //uc07_3_2_1.SetActive(true);
            //this.SceneSwitch();
        }
    }

    public void prepareData(QuestionData q)
    {

        GameData.question = q;
    }

    public void CreateData()
    {
        //BaseService.Instance.questionService.SendQuizDelegate += timeEnd;

        DuelCountDown.countDownBack();
        DuelCountDown.orange.SetActive(false);

        flag = false;
        timeTotal = 20;


        if (QK13_5_2.DeathMatch)
        {
            lblQuestionNumber.text = "Question";
            SKIP1Point.SetActive(false);
            SKIPNOPENALTY.SetActive(false);
            QK13_5_2.DeathMatch = false;
        }
        else
        {
            lblQuestionNumber.text = "Question " + GameData.currentQuestion;
            if (GameData.currentQuestion > 10)
            {
                SKIP1Point.SetActive(true);
                SKIPNOPENALTY.SetActive(false);
            }
            else
            {
                SKIP1Point.SetActive(false);
                SKIPNOPENALTY.SetActive(true);
            }
        }

        Header.SetActive(true);
        Wrong.SetActive(false);
        Correct.SetActive(false);
        Bottom.SetActive(true);
        TimeOut.SetActive(false);

        



        for (int i = 0; i < ListAnswers.Count; i++)
        {
            ListAnswers[i].GetComponent<Button>().enabled = true;
        }

        choosedAnswer = -1;
        for (int i = 0; i < chooseList.Count; i++)
        {
            chooseList[i].transform.localPosition = new Vector3(-900f, chooseList[i].transform.localPosition.y, chooseList[i].transform.localPosition.z);
            chooseList[i].SetActive(false);
            wrongList[i].SetActive(false);
            correctList[i].SetActive(false);
            ListAnswers[i].SetActive(true);
        }


        question = GameData.question;
        lblQuestion.text = question.Content;



        for (int i = 0; i < ListAnswers.Count; i++)
        {
            ListAnswers[i].GetComponent<Text>().text = question.AnswerList[i];

            //Debug.Log (question.AnswerList [i]);
        }

        DuelCountDown.yellow.SetActive(true);
        DuelCountDown.countDownStart();
    }

    public void btnNextClick()
    {
        if (question != GameData.question)
        {
            this.CreateData();
        }
    }

    public void btnSkipClick()
    {
        StartCoroutine(DuelCountDown.countDownStop());
        for (int i = 0; i < ListAnswers.Count; i++)
        {
            ListAnswers[i].GetComponent<Button>().enabled = false;
        }
        flag = true;
        BaseService.Instance.questionService.SendQuizDelegate += prepareData;
        BaseService.Instance.questionService.SendQuizDelegate += buyTicketSuccess;
        BaseService.Instance.playerService.sendClientPay(true);
        //uiblock.turnOnBlockUI();
    }

    public void timeEnd()
    {
        //StartCoroutine(DuelCountDown.countDownStop());
        //GameData.question = q;

        
            Header.SetActive(false);
            TimeOut.SetActive(true);
            for (int i = 0; i < ListAnswers.Count; i++)
            {
                ListAnswers[i].GetComponent<Button>().enabled = false;
            }


            if (question != GameData.question)
            {
                this.CreateData();
            }
            else
            {
                
                BaseService.Instance.questionService.SendQuizDelegate += timeOUT;
               
            }
    }

    void timeOUT(QuestionData q)
    {
        //Debug.Log("ggggggggggggggggggggggggggggggggggg");
        if (question != GameData.question)
        {
            //Debug.Log("hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh");
            this.CreateData();
        }
    }

    void buyTicketSuccess(QuestionData q)
    {
        //uiblock.turnOffBlockUI();
        question = GameData.question;
        this.CreateData();

    }

    void displayRoundDone()
    {
        //BaseService.Instance.gameService.EndRoundDelegate += displayRoundDone;
        this.DisplayScene(false);
        //Debug.Log(GameData.playerData.BattleTag);
        //Debug.Log(WinnerData.winnerTAG);
        if (WinnerData.isDRAW)
        {
            QK13_5_2_DRAW.SetActive(true);
        }
        else
        {
            if (WinnerData.winnerTAG == GameData.playerData.BattleTag)
            {
                win += 1;
                QK13_5_1_win.SetActive(true);
                //QK13_5_PreGameController.win += 1;
                
            }
            else
            {
                lose += 1;
                QK13_5_1_2_Lose.SetActive(true);
                //QK13_5_PreGameController.lose += 1;
            }
        }
        
    }

    void RoundEnd()
    {
        
        //Debug.Log(WinnerData.winnerTAG);
        //Debug.Log(GameData.playerData.BattleTag);
        if (WinnerData.winnerTAG == GameData.playerData.BattleTag)
        {
            QK07Duel.SetActive(false);
            QK13_7.SetActive(true);
            //QK13_7_MatchOver_Controller.isWin = true;

        }
        else
        {
            QK07Duel.SetActive(false);
            QK13_7.SetActive(true);
            //QK13_7_MatchOver_Controller.isWin = false;
        }
    }

    void displayNext()
    {
        if (question != GameData.question)
        {
            this.CreateData();
        }
    }

}
