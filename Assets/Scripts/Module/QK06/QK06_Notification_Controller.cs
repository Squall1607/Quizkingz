using UnityEngine;
using System.Collections;

public class QK06_Notification_Controller : BasePageController {

	public GameObject HideNoti1;
	public GameObject HideNoti2;
	public GameObject HideNotiNew1;
	public GameObject HideNotiNew2;
	public GameObject HideNotiPopupNew1;
	public GameObject HideNotiPopupNew2;
	public GameObject Noti3;
	public GameObject NotiThanks;

	public void hideNotiNew()
	{
		if(HideNoti1.activeSelf == false && HideNoti2.activeSelf == false)
		{
			HideNotiNew1.SetActive (false);
			HideNotiNew2.SetActive (false);
			HideNotiPopupNew1.SetActive (false);
			HideNotiPopupNew2.SetActive (false);
		}	
	}
	public void btnYesNo()
	{
		Noti3.SetActive (false);
		NotiThanks.SetActive (true);
	}
}
