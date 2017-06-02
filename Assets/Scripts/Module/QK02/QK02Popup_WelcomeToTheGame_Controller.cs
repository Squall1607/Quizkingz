using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QK02Popup_WelcomeToTheGame_Controller : BasePageController {
	public Text txtTagNumber;
    public GameObject PanelBlurPopUp;

    void OnEnable(){
		txtTagNumber.text = "#"+GameData.playerData.BattleTag;
        PanelBlurPopUp.SetActive(true);
    }

    void OnDisable()
    {

        PanelBlurPopUp.SetActive(false);
    }
}
