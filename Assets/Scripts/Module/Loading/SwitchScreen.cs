using UnityEngine;
using System.Collections;

public class SwitchScreen : BasePageController {

	public GameObject top;
	public GameObject bottom;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		
		iTween.MoveTo(top, iTween.Hash("position", new Vector3(-235, 300, 0), "islocal", true, "time", 2f,"oncomplete", "backOut","oncompletetarget",gameObject));
		iTween.MoveTo(bottom, iTween.Hash("position", new Vector3(243, -340, 0), "islocal", true, "time", 2f));
	}

	void backOut(){
		iTween.MoveTo(top, iTween.Hash("position", new Vector3 (-235, 1400, 0), "islocal", true, "time", 2f,"oncomplete", "off","oncompletetarget",gameObject));
		iTween.MoveTo(bottom, iTween.Hash("position", new Vector3(243, -1451, 0), "islocal", true, "time", 2f));
	}

	void off(){
		this.DisplayScene(false);
	}

	void OnDisable(){
		
		top.transform.localPosition = new Vector3 (-235, 1451, 0);
		bottom.transform.localPosition = new Vector3 (243, -1451, 0);
	}
}
