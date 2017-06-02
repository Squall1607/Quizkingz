using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class countDown : BasePageController {

	public GameObject yellow;
	public GameObject orange;
	public QK07popup_TimeOut_Controller QK07_3_2_3_TimeOut;
    public GameObject LeavePopup;
    public GameObject SkipPopup;

    void Update() {
        //Debug.Log(yellow.transform.localPosition.x);
        if (yellow.transform.localPosition.x > 600)
        {
            orange.SetActive(true);
        }
    }

	void OnEnable(){
        orange.SetActive(false);
    }

	public void countDownStart(int t){
		yellow.SetActive (true);
		iTween.MoveTo(yellow, iTween.Hash("name","abc","x", 800, "islocal", true, "time", t,"easetype", iTween.EaseType.linear,"oncomplete", "timeOut", "oncompletetarget", gameObject));

	}

	public void timeOut(){
		Debug.Log ("dmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm");
		QK07_3_2_3_TimeOut.DisplayScene (true);
        if (LeavePopup.activeSelf)
        {
            LeavePopup.SetActive(false);
        }

        if (SkipPopup.activeSelf)
        {
            SkipPopup.SetActive(false);
        }

    }

	public void countDownBack(){
		//Debug.Log ("backkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
		yellow.transform.localPosition = new Vector3 (-1,yellow.transform.localPosition.y,yellow.transform.localPosition.z);
//		countDownResum ();
	}

	void OnDisable(){
        orange.SetActive(false);
    }

	public IEnumerator countDownStop(){
		iTween.StopByName("abc");
		yield return new WaitForSeconds (0.01f);
		yellow.transform.localPosition = new Vector3 (-1,yellow.transform.localPosition.y,yellow.transform.localPosition.z);
		yellow.SetActive (false);
	}

	public void countDownResum(){
		iTween.Resume (yellow);
	}
}
