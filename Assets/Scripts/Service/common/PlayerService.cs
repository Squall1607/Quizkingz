using UnityEngine;
using System.Collections;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using GameDelegates;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.UI;
using System.Collections;

namespace GameDelegates
{
	public delegate void ChangeNameResponse (bool status, string name);
	public delegate void ChangeAvatarResponse(bool status, string avatar);
	public delegate void ClientPayResponse (bool isSkip, bool isNew);
	public delegate void ReceiveInviteResponse (string name);
	public delegate void ResponseSendToken (bool success);
	public delegate void SignUpResponse (bool success);
	public delegate void SignInResponse (bool success);
	public delegate void FeedBackResponse(bool success);
}

public class PlayerService
{

	public event ChangeNameResponse ChangeNameDelegate;
	public event ChangeAvatarResponse ChangeAvatarDelegate;
	public event ServerResponseDelegate LoginDelegate;
	public event ClientPayResponse ClientPayDelegate;
	public event ReceiveInviteResponse ReceiveInviteDelegate;
	public event ResponseSendToken SendTokenDelegate;
	public event SignUpResponse SignUpDelegate;
	public event SignInResponse SignInDelegate;
	public event FeedBackResponse FeedBackDelegate;

	public PlayerService ()
	{

	}

	public void SendToken (string token)
	{
		ISFSObject param = new SFSObject ();
		param.PutUtfString ("token", token);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.LOGIN_FB, param));

	}

	public void responseSendToken (BaseObject results)
	{ 
		bool success = results.Param.GetBool ("success");
		if (success) {
			string _fid = results.Param.GetUtfString ("fid");
			GameData.playerData.FacebookID = _fid;

			PlayerPrefs.SetString ("facebook", _fid);
			PlayerPrefs.Save ();

			ISFSArray arr = results.Param.GetSFSArray ("lfriend");
			if (arr != null) {
				Debug.Log ("arr :" + arr.Size ());
				for (int i = 0; i < arr.Size (); i++) {
					PlayerData p = new PlayerData ();
					p.Id = arr.GetSFSObject (i).GetInt ("id");
					p.Level = arr.GetSFSObject (i).GetInt ("level");
					p.Display = arr.GetSFSObject (i).GetUtfString ("display");
					p.BattleTag = arr.GetSFSObject (i).GetUtfString ("battletag");
					p.FacebookID = arr.GetSFSObject (i).GetUtfString ("facebook_id");
					FB.API (p.FacebookID + "/picture?width=100&height=100", HttpMethod.GET, delegate(IGraphResult result) {
						p.Avartar = Sprite.Create (result.Texture, new Rect (0, 0, 100, 100), new Vector2 (0.5f, 0.5f));
					});
					GameData.friendList.Add (p);
				}
			}
		}


		if (SendTokenDelegate != null) {
			SendTokenDelegate (success);
			SendTokenDelegate = null;      
		}
	}


	public void sendChangeName (string newName)
	{
		ISFSObject param = new SFSObject ();
		param.PutUtfString ("newName", newName);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.UPDATE_INFORMATION, param));
	}

	public void sendChangeAvatar(string newURL){
		Debug.Log ("sendChangeAvatar");
		ISFSObject param = new SFSObject ();
		param.PutUtfString ("newAvatar", newURL);
		BaseService.Instance.smartFox.Send (new ExtensionRequest(CommandList.UPDATE_INFORMATION, param));
	}

	public void responseChangeName (BaseObject result)
	{
		if (ChangeNameDelegate != null) {
			ChangeNameDelegate (result.Param.GetBool ("status"), result.Param.GetUtfString ("newName"));
			ChangeNameDelegate = null;
		}
	}

	public void responseChangeAvatar(BaseObject result){
		Debug.Log ("responseChangeAvatar");
		bool status = result.Param.GetBool ("status");
		string URL = result.Param.GetUtfString ("newAvatar");

		if (ChangeAvatarDelegate != null) {
			ChangeAvatarDelegate (status, URL);
			ChangeAvatarDelegate = null;
		}
	}

	public void responseJoinZone (BaseObject result)
	{
		Debug.Log ("responseJoinZone");
		GameData.playerData = new PlayerData ();
		GameData.playerData.Points = result.Param.GetInt ("points");
		GameData.playerData.NextPoints = result.Param.GetInt ("nextPoints");
		GameData.playerData.Score = result.Param.GetInt ("score");
		GameData.playerData.ScoreBonus = result.Param.GetInt ("score_bonus");
		GameData.playerData.MatchTotalDuel = result.Param.GetInt ("tDuel");
		GameData.playerData.WinDuel = result.Param.GetInt ("wDuel");
		GameData.playerData.Id = result.Param.GetLong ("id");
		GameData.playerData.Rank = result.Param.GetInt ("wRank");
		GameData.playerData.TotalPlayer = result.Param.GetInt ("tUser");
		GameData.playerData.WeeklyBest = result.Param.GetInt ("wScore");
		GameData.playerData.TotalGame = result.Param.GetInt ("tGame");
		GameData.playerData.TotalQuiz = result.Param.GetInt ("tQuiz");
		GameData.playerData.BestScore = result.Param.GetInt ("bTime");
		GameData.playerData.LastScore = result.Param.GetInt ("lScore");
		GameData.playerData.TotalCorrect = result.Param.GetInt ("tCorrect");
		GameData.playerData.TotalWrong = result.Param.GetInt ("tWrong");
		GameData.playerData.SfsId = result.Param.GetInt ("sfsId");
		GameData.playerData.FacebookID = result.Param.GetUtfString ("fid");
		GameData.playerData.Email = result.Param.GetUtfString ("email");
		GameData.playerData.AvatarURL = result.Param.GetUtfString ("avatar");
		GameData.playerData.Gem = result.Param.GetLong ("gem");
		GameData.playerData.Token = result.Param.GetLong ("token");
		GameData.playerData.Coin = result.Param.GetLong ("coin");
		GameData.playerData.Trophy = result.Param.GetLong ("trophy");
		GameData.playerData.Display = result.Param.GetUtfString ("display");
		GameData.playerData.TitleId = result.Param.GetInt ("titleid");
		GameData.playerData.Fullname = result.Param.GetUtfString ("fullname");
		GameData.playerData.Address = result.Param.GetUtfString ("address");
		GameData.playerData.PhoneNumber = result.Param.GetUtfString ("phone");
		GameData.playerData.Status = result.Param.GetInt ("status");
		GameData.entryFee = result.Param.GetIntArray ("entryfee");
		GameData.playerData.Level = result.Param.GetInt ("level");
		GameData.playerData.BattleTag = result.Param.GetUtfString ("battletag");


		if (!PlayerPrefs.HasKey ("email")) {
			if (GameData.playerData.Email != "none") {
				PlayerPrefs.SetString ("email", GameData.playerData.Email);
			}
		} else {
			if (GameData.playerData.Email != PlayerPrefs.GetString ("email")) {
				PlayerPrefs.SetString ("email", GameData.playerData.Email);
			}
		}
		if (!PlayerPrefs.HasKey ("facebook")) {
			if (GameData.playerData.FacebookID != "-1") {
				PlayerPrefs.SetString ("facebook", GameData.playerData.FacebookID);
			}
		} else {
			if (GameData.playerData.FacebookID != PlayerPrefs.GetString ("facebook")) {
				PlayerPrefs.SetString ("facebook", GameData.playerData.FacebookID);
			}
		}
		PlayerPrefs.Save ();
		if (LoginDelegate != null) {
			LoginDelegate ();
			LoginDelegate = null;
		}
	}


	public void Ditmemay(BaseObject result){
		GameData.playerData.AvatarURL = result.Param.GetUtfString ("avatar");
		Debug.Log ("GameData.playerData.AvatarURL: "+ GameData.playerData.AvatarURL);

	}

	public void sendClientPay (bool isSkip)
	{
		ISFSObject param = new SFSObject ();
		if (isSkip) {
			param.PutBool ("new", true);
		} else {
			param.PutBool ("change", true);
		}

		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_PAY, param));
	}

	public void responseClientPay (BaseObject result)
	{
		bool isSkip = result.Param.GetBool ("change");
		bool isNew = result.Param.GetBool ("new");

		if (ClientPayDelegate != null) {
			ClientPayDelegate (isSkip, isNew);
			ClientPayDelegate = null;
		}
	}

	public void reponseInviteReceive (BaseObject result)
	{
		string masterName = result.Param.GetUtfString ("ownerName");
		//Debug.Log(masterName + " just send you ");
		GameData.gameRoom = result.Param.GetUtfString ("rName");

		if (result.Param.ContainsKey ("room")) {
			GameData.room = result.Param.GetUtfString ("room");
		}
        

		if (ReceiveInviteDelegate != null) {
			ReceiveInviteDelegate (masterName);
			//ReceiveInviteDelegate = null;
		}
	}

	public void sendAcceptInvite (string roomName, bool accept)
	{
		Debug.Log ("sendAcceptInvite");
		ISFSObject param = new SFSObject ();
		param.PutUtfString ("rName", roomName);
		param.PutBool ("accept", accept);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_INVITERESPONSE, param));
	}

	public void sendSignUpSuccess (string email, string password)
	{
		ISFSObject param = new SFSObject ();
		param.PutUtfString ("email", email);
		param.PutUtfString ("password", password);
		GameData.playerData.Email = email;
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.EMAIL_SIGNUP, param));
	}

	public void responseSignUp (BaseObject result)
	{
		PlayerPrefs.SetString ("email", GameData.playerData.Email);
		PlayerPrefs.Save ();

		if (SignUpDelegate != null) {
			SignUpDelegate (result.Param.GetBool ("success"));
			SignUpDelegate = null;
		}
	}

	public void sendSignInSuccess (string email, string password)
	{
		ISFSObject param = new SFSObject ();
		param.PutUtfString ("email", email);
		param.PutUtfString ("password", password);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.EMAIL_SIGNIN, param));
	}

	public void sendFeedBack (string msg)
	{
		ISFSObject param = new SFSObject ();
		param.PutUtfString ("msg", msg);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_FEEDBACK, param));
	}

	public void responseSignIn (BaseObject result)
	{
		if (SignInDelegate != null) {
			SignInDelegate (result.Param.GetBool ("success"));
			SignInDelegate = null;
		}
	}
	public void responseFeedBack(BaseObject result)
	{
		if (FeedBackDelegate != null) {
			FeedBackDelegate (result.Param.GetBool ("success"));
			FeedBackDelegate = null;
		}
	}
}
