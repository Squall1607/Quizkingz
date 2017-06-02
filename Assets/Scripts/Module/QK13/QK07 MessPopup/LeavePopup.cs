using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LeavePopup : BasePageController
{
	public Text txtScore;
    public GameObject PanelBlurPopUp;
    // Use this for initialization
    void OnEnable(){
		txtScore.text = "" + GameData.playerData.Score;
        PanelBlurPopUp.SetActive(true);
    }

    void OnDisable()
    {

        PanelBlurPopUp.SetActive(false);
    }
}
