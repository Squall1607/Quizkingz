using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Logging;
using GameDelegates;

public class BaseService
{

	#region Declare variables

	public bool isLoggedIn;
	public bool isJoined;
	public SmartFox smartFox;
	//services
	public PlayerService playerService;
	public GameService gameService;
	public QuestionService questionService;
	public ErrorHandler errorHandler;
	//delegates
	public event ServerResponseDelegate OnConnectionDelegate;
	public event ServerResponseDelegate OnConnectionLostDelegate;
	public event ServerResponseDelegate OnLoginDelegate;
	public event ServerResponseDelegate OnLoginErrorDelegate;
	public event ServerResponseDelegate OnLogoutDelegate;
	public event ServerResponseDelegate OnJoinRoomDelegate;
	public event ServerResponseDelegate OnServerOffDelegate;
	//

	#endregion

	#region Singleton

	private static readonly BaseService instance = new BaseService ();

	public static BaseService Instance {
		get {
			return instance; 
		}
	}

	#endregion

	#region public function

	public void Connect ()
	{
		if (!smartFox.IsConnected) {
			smartFox.Connect (GameConfig.serverName, GameConfig.serverPort);
			//
		}
	}

	#endregion

	#region private function

	private BaseService ()
	{
		smartFox = new SmartFox (true);
		isLoggedIn = false;
		isJoined = false;

		playerService = new PlayerService ();
		questionService = new QuestionService ();
		gameService = new GameService ();
		errorHandler = new ErrorHandler ();
		//
		setupSmartFox ();
	}

	private void setupSmartFox ()
	{
		smartFox.ThreadSafeMode = true;
		
		// Register callback delegate
		smartFox.AddEventListener (SFSEvent.CONNECTION, OnConnection);
		smartFox.AddEventListener (SFSEvent.CONNECTION_LOST, OnConnectionLost);
		smartFox.AddEventListener (SFSEvent.LOGIN, OnLogin);
		smartFox.AddEventListener (SFSEvent.LOGIN_ERROR, OnLoginError);
		smartFox.AddEventListener (SFSEvent.LOGOUT, OnLogout);
		smartFox.AddEventListener (SFSEvent.ROOM_JOIN, OnJoinRoom);
		smartFox.AddEventListener (SFSEvent.CONNECTION_ATTEMPT_HTTP, OnServerOff);
		smartFox.AddEventListener (SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
	}

	#endregion

	#region reponse event callback

	private void OnConnection (BaseEvent evt)
	{
		//
		if (OnConnectionDelegate != null) {
			OnConnectionDelegate ();
		}
		Debug.Log ("On Connection");
		//Cai duoi nay de login 
		ISFSObject obj = new SFSObject ();
		if (PlayerPrefs.HasKey ("facebook") && !PlayerPrefs.HasKey ("email")) {
			obj.PutUtfString ("fid", PlayerPrefs.GetString ("facebook"));
		} else if (PlayerPrefs.HasKey ("email") && !PlayerPrefs.HasKey ("facebook")) {
			obj.PutUtfString ("em", PlayerPrefs.GetString ("email"));
		} else if (PlayerPrefs.HasKey ("email") && PlayerPrefs.HasKey ("facebook")) {
			obj.PutUtfString ("fid", PlayerPrefs.GetString ("facebook"));
		} else {
			obj.PutUtfString ("did", SystemInfo.deviceUniqueIdentifier);
		}
//		obj.PutUtfString ("did", "testABC");
		bool success = (bool)evt.Params ["success"];
		if (success) {
			smartFox.Send (new LoginRequest ("", "", GameConfig.zone, obj));
		} else {

		}
		
	}

	private void OnConnectionLost (BaseEvent evt)
	{
		// Reset all internal states so we kick back to login screen
		isLoggedIn = false;
		isJoined = false;
		Debug.Log ("Connection was lost; reason is: " + (string)evt.Params ["reason"]);
		
		if (OnConnectionLostDelegate != null) {
			OnConnectionLostDelegate ();
		}
	}

	private void OnLogin (BaseEvent evt)
	{
		// Make sure we got in and then populate the room list string array
		isLoggedIn = true;
//		smartFox.Send (new JoinRoomRequest (GameConfig.room));
		
		if (OnLoginDelegate != null) {
			OnLoginDelegate ();
		}
	}

	private void OnLoginError (BaseEvent evt)
	{
		if (OnLoginErrorDelegate != null) {
			OnLoginErrorDelegate ();
		}
	}

	private void OnLogout (BaseEvent evt)
	{
		isLoggedIn = false;
		isJoined = false;
		if (OnLogoutDelegate != null) {
			OnLogoutDelegate ();
		}
	}

	private void OnJoinRoom (BaseEvent evt)
	{
		isJoined = true;
		if (OnJoinRoomDelegate != null) {
			OnJoinRoomDelegate ();
		}
	}

	private void OnServerOff (BaseEvent evt)
	{
		if (OnServerOffDelegate != null) {
			OnServerOffDelegate ();
		}
	}

	private void OnExtensionResponse (BaseEvent evt)
	{
		string command = (string)evt.Params ["cmd"];
		ISFSObject rs = (SFSObject)evt.Params ["params"];
		BaseObject result = new BaseObject (command, rs);
		Debug.Log ("Response: cmd: " + command + " param: " + rs.GetDump ());
		checkResponse (result);
	}

	void checkResponse (BaseObject result)
	{
		switch (result.Cmd) {
		case CommandList.JOIN_ZONE:
			playerService.responseJoinZone (result);
//			playerService.Ditmemay (result);
			break;
		case CommandList.REFRESH_DATA:
			gameService.RefreshData (result);
			break;
		case CommandList.NOT_ENOUGH_TICKET:
			gameService.responseTicket (result);
			break;
		case CommandList.UPDATE_INFORMATION:
			playerService.responseChangeName (result);
			playerService.responseChangeAvatar (result);
			break;
		case CommandList.CLIENT_FEEDBACK:
			playerService.responseFeedBack (result);
			break;
		case CommandList.CLIENT_START:
			gameService.responseStartGame (result);
			break;
		case CommandList.SEND_QUIZ:
			questionService.responseSendQuiz (result);
			break;
		case CommandList.ANSWER:
			questionService.responseAnswer (result);
			break;
		case CommandList.FULL_TIME:
			questionService.responseTimeOut ();
			break;
		case CommandList.STOP:
			gameService.responseStopGame (result);
			break;
		case CommandList.CLIENT_PAY:
			playerService.responseClientPay (result);
			break;
		case CommandList.CLIENT_INVITE:
			playerService.reponseInviteReceive (result);
			gameService.responeStartGameMulti (result);   
			break;
		case CommandList.ASK:
			gameService.responseAskCheckPoint (result);
			break;
		case CommandList.ASK_RS:
			gameService.responseResultAskCheckPoint (result);
			break;
		case CommandList.CLIENT_LEADERBOARD:
			gameService.responseLeaderBoard (result);
			break;	
		case CommandList.ERROR:
			errorHandler.responseError (result);
			break;
		case CommandList.LOGIN_FB:
			playerService.responseSendToken (result);
			break;
		case CommandList.CLIENT_FIND:
			gameService.responseSearchPlayer (result);
			break;

		case CommandList.CLIENT_INVITERESPONSE:
			gameService.responseAnswerInvite (result);
			break;

		case CommandList.END_ROUND:
			gameService.responseEndRound (result);
			break;

		case CommandList.ONLINE_LIST:
			gameService.responsePlayerOnlineList (result);
			break;

		case CommandList.CLIENT_REVENGE:
			gameService.ResponseRevengeListDelegate (result);
			break;

		case CommandList.SEND_LST_QUIZ:
			questionService.responseSendLstQuiz (result);
			break;

		case CommandList.CLIENT_RANDOM_TIP:
			gameService.ResponseTip (result);
			break;

		case CommandList.LEADERBOARD_PRICE_LIST:
			gameService.responseLeaderboardPriceList (result);
			break;

		case CommandList.EMAIL_SIGNIN:
			playerService.responseSignIn (result);
			break;

		case CommandList.EMAIL_SIGNUP:
			playerService.responseSignUp (result);
			break;

        case CommandList.CLIENT_MESSAGE:
            gameService.responseChat(result);

            break;
		
		case CommandList.CLIENT_FAQ:
			gameService.responseFAQ (result);
			break;

		case CommandList.SERVER_PRODUCT:
			gameService.responseProduct (result);
			break;
        }

	}

	#endregion
}
