using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Facebook.Unity;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;

public class tuto_FaceBookLogout_Controller : BasePageController
{

	public Text txtInformation;
	public Text txtName;
	public GameObject avatar;

	public void OnEnable ()
	{
		Debug.Log (GameData.playerData.Display);
		txtInformation.text = "<color='#FFCB2BFF'>TAG #" + GameData.playerData.BattleTag.ToUpper () + "</color>  LVL. " + GameData.playerData.Level;
		txtName.text = GameData.playerData.Display.ToUpper ();
		avatar.GetComponent<Image>().sprite = GameData.playerData.Avartar;
	}

	public void onClickLogOut ()
	{
		if (FB.IsInitialized) {
			FB.LogOut ();
		}
		PlayerPrefs.DeleteKey ("email");
		PlayerPrefs.DeleteKey ("facebook");
		GameData.playerData.FacebookID = "-1";
		ISFSObject obj = new SFSObject ();
		BaseService.Instance.gameService.SendLogOutFacebook ();
		PlayerPrefs.DeleteKey ("facebook");
	}



}
