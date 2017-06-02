using UnityEngine;
using System.Collections;

public class countDownIncorrectAswers : MonoBehaviour {

	public GameObject yellow;
	public QK07_3_2_1_IncorrectAnswer_Controller QK07_3_2_1_IncorrectAnswer;
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
		QK07_3_2_1_IncorrectAnswer.btnEndClicked ();
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
