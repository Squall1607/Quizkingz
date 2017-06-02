using UnityEngine;
using System.Collections;

public class AnimUIQuestion : MonoBehaviour {

	public GameObject UIQuestion;

	void OnEnable(){
		InvokeRepeating ("randomCall", 1f, 1f);
	}

	void randomCall(){
		int check = Random.Range (1, 100);
		if(check % 2 == 0){
			move ();
			Invoke ("stop", 2f);
		}
	}

	void move(){
		iTween.ScaleTo (UIQuestion, iTween.Hash ("scale", new Vector3 (1.5f, 1.5f, 1f),"islocal",true, "time", 0.7f,"looptype",iTween.LoopType.pingPong));
		left ();
	}
	void left(){
		iTween.RotateTo(UIQuestion, iTween.Hash ("rotation", new Vector3 (0f, 0f, -15f),"islocal",true, "time", 0.3f,"oncomplete", "right","oncompletetarget",gameObject));

	}
	void right(){
		iTween.RotateTo(UIQuestion, iTween.Hash ("rotation", new Vector3 (0f, 0f, 15f),"islocal",true, "time", 0.3f,"oncomplete", "left","oncompletetarget",gameObject));

	}

	void stop(){
		//iTween.Pause ();
		iTween.ScaleTo (UIQuestion, iTween.Hash ("scale", new Vector3 (1f, 1f, 1f),"islocal",true, "time", 0.7f));
		iTween.RotateTo(UIQuestion, iTween.Hash ("rotation", new Vector3 (0f, 0f, 0f),"islocal",true, "time", 0.3f));

	}

	void OnDisable(){
		CancelInvoke ();
	}
}
