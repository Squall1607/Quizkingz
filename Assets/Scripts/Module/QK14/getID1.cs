using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Facebook.Unity;
public class getID1 : MonoBehaviour {

	CanvasGroup canvasGroup;
//	public GameObject ava;
	void Awake(){
		canvasGroup = this.GetComponent<CanvasGroup> ();
	}

	public void onClick(){
	//	ava.SetActive (true);
		string id = this.GetComponentInChildren<Text> ().text;
		GameObject g = GameObject.Find("QK14.3 - StartGame");
		g.GetComponent<QK14_3_StartGame_Controller> ().displayInvite (id);
		StartCoroutine ("FadeOut");
	}

	IEnumerator FadeOut(){
		float time = 1;
		while (canvasGroup.alpha > 0) {
			canvasGroup.alpha -= (3*Time.deltaTime) / time;
			yield return null;
		}
		GameObject p = this.transform.parent.gameObject;
		Destroy (p);
	}
}


