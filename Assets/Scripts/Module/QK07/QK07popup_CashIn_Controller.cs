using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Spine;
using Spine.Unity;

public class QK07popup_CashIn_Controller : BasePageController {
	

	public QK07_2_QuestionController QK07_2_Question;
	public QK07_GameOver_Controller QK07_GameOver;
	public SkeletonAnimation CashInAnim;
	public CameraController cam;
	public GameObject blockNav;
	public GameObject btnOk, PanelBlurPopUp;
	public float timer = 0.3f;
	public Text txtScore;

	// Use this for initialization
	void Update () {
		//timer -= Time.deltaTime;
		//if (timer < 0) {
		//	showCashInAnim ();
		//}
	}

	void OnEnable(){
		//cam.turnBlurOn();
        PanelBlurPopUp.SetActive(true);
        btnOk.GetComponent<Button>().enabled = true;

	//	txtScore.text = "" + GameData.playerData.Score;
	}
	void OnDisable(){
        PanelBlurPopUp.SetActive(false);
        //cam.turnBlurOff();
	}

	public void showCashInAnim()
	{
		timer = 0.3f;
		CashInAnim.AnimationName = "show";
	}
	public void hideCashInAnim()
	{
		timer = 1.467f;
		CashInAnim.AnimationName = "hide";
	}

	public void btnOkClicked(){
       // Debug.Log("aaaaaaaaaaaaaaâ");
        Invoke("disableBlack", 1.467f);
        btnOk.GetComponent<Button>().enabled = false;
		
	}
	void disableBlack(){
        //Debug.Log("aaaaaaaaaaaaaaâ");
        //cam.turnBlurOff ();
        PanelBlurPopUp.SetActive(false);
        DisplayScene(false); 
		QK07_2_Question.DisplayScene(false);
        QK07_GameOver.DisplayScene(true);
        blockNav.SetActive (false);
	}
}
