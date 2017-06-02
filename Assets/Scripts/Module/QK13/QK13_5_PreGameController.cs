using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class QK13_5_PreGameController : BasePageController
{

    public Text txtStartGame;
    public QK07Duel_Controller QK7_2_Duel;
    public GameObject SwitchScreen, fakeMain;
    public GameObject blockNav;
    public Text txtName,txtTAG;
    public Text txtCorrect, txtWrong , txtSkip , txtBonus;
    //public GameObject UIMain;
    public qk13Find QK13_Find;

    bool flag = false;

    void OnEnable() {

        BaseService.Instance.questionService.SendQuizDelegate += QK7_2_Duel.prepareData;
        BaseService.Instance.questionService.SendQuizDelegate += StartDuelGame;
        //Debug.Log("vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv   " + GameData.currentQuestion);
        //Debug.Log(DuelData.ownerName);
        //Debug.Log(QK13_3_FindOpponent_Controller.name);

        //navbar.SetActive(false);
        //navbar.SetActive(true);
        fakeMain.SetActive(true);
        //navbarPopup.SetActive(false);
        blockNav.SetActive(true);

        if (!String.IsNullOrEmpty(DuelData.ownerName))
        {
            //neu la nguoi dc moi
            txtName.text = DuelData.ownerName.ToUpper();
            txtTAG.text = "TAG #" + DuelData.ownerBattleTAG.ToUpper();

        }
        else if (!String.IsNullOrEmpty(QK13_Find.nameFind))
        {
            //neu la chu phong
            txtName.text = QK13_Find.nameFind.ToUpper();
            txtTAG.text = "TAG #" + QK13_Find.batterTAG.ToUpper();

        }
        //else if (!String.IsNullOrEmpty(QK13_3_FindOpponent_Controller.name))
        //{
        //    //neu la chu phong
        //    txtName.text = QK13_3_FindOpponent_Controller.name.ToUpper();
        //    txtTAG.text = "TAG #" + QK13_3_FindOpponent_Controller.battleTag.ToUpper();

        //}

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void StartDuelGame(QuestionData q)
    {
        SwitchScreen.SetActive(true);
        Invoke("prepareToChangeScene", 1.8f);

       

    }

    void prepareToChangeScene()
    {

        Debug.Log("Change to UC07Duel scene");
        //StartDuelGame();

        DisplayScene(false);

        QK7_2_Duel.DisplayScene(true);

    }
}
