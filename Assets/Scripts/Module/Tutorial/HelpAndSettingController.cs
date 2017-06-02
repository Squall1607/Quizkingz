using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpAndSettingController : BasePageController
{
	
	public tuto_FaceBookLogin_Controller fbLogin;
	public tuto_FaceBookLogout_Controller fbLogout;
	public QK02_LoginFacebook_Controller QK02;
	public GameObject panel03;

	void OnEnable ()
	{
		
	}

	public void onPressPrev ()
	{
		panel03.SetActive (true);
		this.DisplayScene (false);
	}

	public void onClickFacebookBtn ()
	{
		
		if (GameData.playerData.FacebookID == "-1") {

			fbLogin.DisplayScene (true);		
		} else {
			fbLogout.DisplayScene (true);
		}
	}

}
