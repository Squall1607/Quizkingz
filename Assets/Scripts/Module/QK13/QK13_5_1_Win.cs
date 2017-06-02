using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class QK13_5_1_Win : BasePageController
{

    public Text txtName, txtTAG;
    public QK07Duel_Controller QK7_2_Duel;
    public GameObject SwitchScreen, fakeMain, blockNav;
    public Text txtCorrect, txtWrong, txtSkip, txtBonus;
    public GameObject QK13_7_MatchOver, QK07Duel;
    public Button btnLEAVE;
    public Text lblYOU, lblOPP;
    public GameObject GO11,GO10;
    public qk13Find QK13_Find;

    void OnEnable()
    {
        // Debug.Log(DuelData.ownerName);
        // Debug.Log(QK13_3_FindOpponent_Controller.name);
        BaseService.Instance.questionService.SendQuizDelegate += QK7_2_Duel.prepareData;
        BaseService.Instance.questionService.SendQuizDelegate += StartDuelGame;
        BaseService.Instance.gameService.GameStopDelegate += RoundEnd;
        btnLEAVE.enabled = true;
        Debug.Log(QK7_2_Duel.win + "         " + QK7_2_Duel.lose);

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

        //if (DuelData.ownerName != "" && DuelData.ownerName != null)
        //{
        //    txtName.text = DuelData.ownerName.ToUpper();
        //    txtTAG.text = "TAG #" + DuelData.ownerBattleTAG.ToUpper();
        //}
        //else if (QK13_3_FindOpponent_Controller.name != "" && QK13_3_FindOpponent_Controller.name != null)
        //{
        //    txtName.text = QK13_3_FindOpponent_Controller.name.ToUpper();
        //    txtTAG.text = "TAG #" + QK13_3_FindOpponent_Controller.battleTag.ToUpper();
        //}


        if (GameData.currentQuestion > 4 && GameData.currentQuestion < 8)
        {
            txtCorrect.text = "+2";
            txtWrong.text = "-3";
            txtSkip.text = "0";
            txtBonus.text = "+3";
        }
        else if (GameData.currentQuestion > 8)
        {
            txtCorrect.text = "+5";
            txtWrong.text = "-4";
            txtSkip.text = "-1";
            txtBonus.text = "+6";
        }

        if (QK7_2_Duel.win == 1)
        {
            if (QK7_2_Duel.lose == 1)
            {
                lblYOU.text = "1";
                lblOPP.text = "1";
                GO11.SetActive(true);
                GO10.SetActive(false);
            }
            else
            {
                lblYOU.text = "1";
                lblOPP.text = "0";
                GO11.SetActive(false);
                GO10.SetActive(true);
            }
        }

        
    }

    void StartDuelGame(QuestionData q)
    {
        SwitchScreen.SetActive(true);
        Invoke("prepareToChangeScene", 1.5f);

        fakeMain.SetActive(true);
        //navbar.SetActive(true);
        //navbarPopup.SetActive(false);
        blockNav.SetActive(true);

    }

    void prepareToChangeScene()
    {

        Debug.Log("Change to UC07Duel scene");
        //StartDuelGame();

        DisplayScene(false);

        QK7_2_Duel.DisplayScene(true);

    }

    public void btnLeave()
    {
        BaseService.Instance.gameService.sendStopGame();
        btnLEAVE.enabled = false;
    }

    void RoundEnd()
    {

        //Debug.Log(WinnerData.winnerTAG);
        //Debug.Log(GameData.playerData.BattleTag);
        if (gameObject.activeSelf)
        {
            if (WinnerData.winnerTAG == GameData.playerData.BattleTag)
            {
                gameObject.SetActive(false);
                QK07Duel.SetActive(false);
                QK13_7_MatchOver.SetActive(true);
                //QK13_7_MatchOver_Controller.isWin = true;

            }
            else
            {
                gameObject.SetActive(false);
                QK07Duel.SetActive(false);
                QK13_7_MatchOver.SetActive(true);
                //QK13_7_MatchOver_Controller.isWin = false;
            }
        }
    }

}
