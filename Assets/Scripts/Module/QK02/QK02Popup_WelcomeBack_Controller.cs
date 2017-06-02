using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QK02Popup_WelcomeBack_Controller : BasePageController
{
	public Text name;
	public Text battleTag;
	//	public Panel03Controller UC03;
	//public CameraController blur;
	public GameObject QK02_ChangeNow;
    public GameObject PanelBlurPopUp;

    void OnEnable ()
	{
		name.text = GameData.playerData.Display.ToUpper ();
		battleTag.text = "" + GameData.playerData.BattleTag.ToUpper ();
        PanelBlurPopUp.SetActive(true);
    }

	public void onClickChangeNow(){
		this.DisplayScene (false);
		QK02_ChangeNow.SetActive (true);
	}
	public void onClickProceed ()
	{
        //blur.turnBlurOff ();
        PanelBlurPopUp.SetActive(false);
        this.DisplayScene (false);
        
    }
}
