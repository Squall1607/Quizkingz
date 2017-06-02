using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class tuto_aboutAndFAQ_Controller : BasePageController {

	public List<GameObject> lstShowL1;
	public List<GameObject> lstHideL1;
	public List<GameObject> lstText1;
	public List<GameObject> lstAboutFAQL1;


	public void showL1(GameObject g)
	{
		for (int i = 0; i < lstShowL1.Count; i++) {
			if (lstShowL1 [i] == g) {
				lstHideL1 [i].SetActive (false);
				lstShowL1 [i].SetActive (true);
				lstText1 [i].SetActive (true);
				if (i < lstShowL1.Count) {
					for (int j = i; j < lstShowL1.Count; j++) {
						lstAboutFAQL1 [j + 1].transform.localPosition = new Vector3 (lstAboutFAQL1 [j + 1].transform.localPosition.x,

							lstAboutFAQL1 [j + 1].transform.localPosition.y - 160 , 0);					
					}
				} 
			}
		}
	}
	public void hideL1(GameObject g)
	{
		for (int i = 0; i < lstHideL1.Count; i++) {
			if (lstHideL1 [i] == g) {
				lstShowL1 [i].SetActive (false);
				lstHideL1 [i].SetActive (true);
				lstText1 [i].SetActive (false);
				if (i < lstHideL1.Count) {
					for (int j = i; j < lstShowL1.Count; j++) {
						lstAboutFAQL1 [j+1].transform.localPosition = new Vector3 (lstAboutFAQL1 [j + 1].transform.localPosition.x,
							lstAboutFAQL1 [j + 1].transform.localPosition.y + 160, 0);					
					}
				}
			}
		}
	}
}
