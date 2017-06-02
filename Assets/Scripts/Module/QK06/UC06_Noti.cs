using UnityEngine;
using System.Collections;

public class UC06_Noti : MonoBehaviour {


	public GameObject TabNotiNew;
	public GameObject Noti;
	public GameObject NotiNew;
	public GameObject HideNoti1;
	public GameObject HideNoti2;
	public GameObject UC07Question;
	public GameObject UC06Noti;

	public float timer = 3.0f;

	// Use this for initialization
	void Update () {
		timer -= Time.deltaTime;
		showTabNotiNew ();
	}
	public void closeTabNotiNew()
	{
		iTween.MoveTo (NotiNew, iTween.Hash ("x",-835,"islocal",true, "time", 1,"oncomplete", "none","oncompletetarget",gameObject));
		Noti.SetActive (false);
	}
	public void openNotiNewQuestion()
	{
		iTween.MoveTo (NotiNew, iTween.Hash ("x", 0, "islocal", true, "time", 1, "oncomplete", "none", "oncompletetarget", gameObject));
		timer = 3.0f;
		if (timer < 0) {
			iTween.MoveTo (NotiNew, iTween.Hash ("x",-835,"islocal",true, "time", 1,"oncomplete", "none","oncompletetarget",gameObject));
			Noti.SetActive (false);
		}
		
	}
	public void showTabNotiNew()
	{
		TabNotiNew.SetActive (true);
		if (timer < 0) {
			iTween.MoveTo (NotiNew, iTween.Hash ("x",-835,"islocal",true, "time", 1,"oncomplete", "none","oncompletetarget",gameObject));
			Noti.SetActive (false);
		}
		if (HideNoti1.activeSelf == false && HideNoti2.activeSelf == false )
		{
			TabNotiNew.SetActive(false);
		}else if(UC06Noti.activeSelf==true)
		{	
			TabNotiNew.SetActive(false);
		}else{
			TabNotiNew.SetActive(true);
		}
		if(UC07Question.activeSelf==true)
		{
			NotiNew.SetActive(false);
			
		}else{
			NotiNew.SetActive(true);
		}
	}

}
