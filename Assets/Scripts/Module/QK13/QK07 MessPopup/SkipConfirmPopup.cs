using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkipConfirmPopup : BasePageController
{

    public CameraController cam;
    public Button btOK;
    public QK07_2_QuestionController UC07_2_Questions;
	public GameObject QK07_3_2_2;
    public GameObject SkipPopUp, PanelBlurPopUp;
	public Text ticket;

    public void btOKclick()
    {
        btOK.enabled = false;
        UC07_2_Questions.btnSkipClick();
        SkipPopUp.SetActive(false);
		GameData.playerData.Token -= 1;
		BaseService.Instance.gameService.BuyTicKetDelegate += buyTicket; 
    }

	void buyTicket(){
		this.DisplayScene(false);
        //cam.turnBlurOn ();
        PanelBlurPopUp.SetActive(true);
        QK07_3_2_2.SetActive (true);
	}

    void OnEnable()
    {
        //cam.turnBlurOn();
        btOK.enabled = true;
        PanelBlurPopUp.SetActive(true);
        ticket.text = GameData.playerData.Token.ToString ();
    }

    void OnDisable()
    {
        //cam.turnBlurOff();
        PanelBlurPopUp.SetActive(false);
        btOK.enabled = true;

    }
}
