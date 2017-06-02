using UnityEngine;
using System.Collections;

public class countDownTimeOut : MonoBehaviour {
	
	public GameObject yellow;
	public QK07popup_TimeOut_Controller QK07popup_TimeOut;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		yellow.SetActive (true);
		iTween.MoveTo(yellow, iTween.Hash("name","abc","x", 700, "islocal", true, "time", 10f,"easetype", iTween.EaseType.linear,"oncomplete", "timeOut", "oncompletetarget", gameObject));

	}

	void timeOut(){
		QK07popup_TimeOut.btnEndClicked ();
	}

	public IEnumerator countDownStop(){
		//Debug.Log ("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
		iTween.StopByName("abc");
		yield return new WaitForSeconds (0.1f);
		yellow.transform.localPosition = new Vector3 (3,yellow.transform.localPosition.y,yellow.transform.localPosition.z);
		yellow.SetActive (false);
		//Debug.Log ("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");
	    
	}

	void OnDisable(){
		yellow.transform.localPosition = new Vector3 (3,yellow.transform.localPosition.y,yellow.transform.localPosition.z);
		yellow.SetActive (false);
	}

}
