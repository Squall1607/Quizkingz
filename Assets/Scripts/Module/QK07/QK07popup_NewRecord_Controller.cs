using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Spine;
using Spine.Unity;

public class QK07popup_NewRecord_Controller : BasePageController {

	public SkeletonAnimation NewRecordAnim;
	//public CameraController cam;
	public QK07_GameStart_Controller QK07_GameStart;
	public GameObject btnOk,PanelBlurPopUp;
	public float timer = 0.3f;
	public Text txtScore;

	// Use this for initialization
	void OnEnable(){
        //cam.turnBlurOn();
        PanelBlurPopUp.SetActive(true);

        btnOk.GetComponent<Button>().enabled = true;
		txtScore.text = "" + GameData.playerData.Score;
	}
	void OnDisable(){
        PanelBlurPopUp.SetActive(false);
        //cam.turnBlurOff();
    }

	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			showNewRecordAnim ();
		}
	}

	public void showNewRecordAnim()
	{
		timer = 0.3f;
		NewRecordAnim.AnimationName = "show";
	}
	public void hideCashInAnim()
	{
		timer = 2.333f;
		NewRecordAnim.AnimationName = "hide";
	}

	public void okClick(){
        //Debug.Log("gggggggggggggggg");  
		Invoke ("turnOff",  2.333f);
		btnOk.GetComponent<Button>().enabled = false;
	}

	void turnOff(){
		this.DisplayScene(false);
		QK07_GameStart.DisplayScene(true);
    }
}
