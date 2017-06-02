using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using GameDelegates;
using UnityEngine.UI;


namespace GameDelegates
{
	public delegate void ChangeLanguageResponse (LanguageData data);
	public delegate void AskCheckPointResponse (int data);
	public delegate void GetLeaderBoardReponse (List<PlayerData> userData);
	public delegate void ResponeSendStartGameMulti ();
	public delegate void responseSearchPlayer (List<PlayerData> list);
	public delegate void responsePlayerLobby (List<PlayerData> list);
	public delegate void responseStartGame ();
	public delegate void ResponseAnswerInviation (bool isAccept);
	public delegate void ResponseEndRound ();
	public delegate void ResponseNotEnoughTicket ();
	public delegate void ResponseRevengeList (List<PlayerData> list);
	public delegate void ResponseTips ();
	public delegate void ResponeseRefreshData ();
	public delegate void ResponseLPriceL (List<PrizeData> list);
    public delegate void ResponseChat(int id , string msg , string oppName);
	public delegate void ResponseLFAQ(List<FAQData> list);

	public delegate void ResponseProduct(List<ProductData> listProduct);
}

public class GameService
{
    
	public event ChangeLanguageResponse ChangeLanguageDelegate;
	public event AskCheckPointResponse AskCheckPointDelegate;
	public event AskCheckPointResponse ResultAskCheckPointDelegate;
	public event ServerResponseDelegate GameStopDelegate;
	public event GetLeaderBoardReponse LeaderBoardDelegate;
	public event ResponeSendStartGameMulti SendStartGameMultiDelegate;
	public event responseSearchPlayer SearchPlayerDelegate;
	public event responsePlayerLobby GetListPlayerLobby;
	public event responseStartGame responseStartGameDelegate;
	public event ResponseAnswerInviation AnswerInvitationDelegate;
	public event ResponseEndRound EndRoundDelegate;
	public event ResponseNotEnoughTicket BuyTicKetDelegate;
	public event ResponseRevengeList RevengeListDelegate;
	public event ResponseTips TipsDelegate;
	public event ResponeseRefreshData RefreshDataDelegate;
	public event ResponseLPriceL LPriceLDelegate;
    public event ResponseChat ChatDelegate;
	public event ResponseLFAQ FAQDelegate;
	public event ResponseProduct ProductDelegate;

    public void sendMsg(int id , string msg)
    {
        ISFSObject obj = new SFSObject();
        obj.PutInt("pmID", id);
        obj.PutUtfString("msg", msg);
        BaseService.Instance.smartFox.Send(new ExtensionRequest(CommandList.CLIENT_MESSAGE, obj));
    }

    public void responseChat(BaseObject result)
    {
        string oppMsg = result.Param.GetUtfString("message");
        int id = result.Param.GetInt("sender_id");
        string oppName = result.Param.GetUtfString("display");
        Debug.Log(id +" "+ oppName +" "+ oppMsg);

        if (ChatDelegate != null)
        {
            ChatDelegate(id, oppMsg, oppName);
            //ChatDelegate = null;
        }
    }


    public void RefreshData (BaseObject result)
	{
		GameData.playerData = new PlayerData ();
		GameData.playerData.Points = result.Param.GetInt ("points");
		GameData.playerData.NextPoints = result.Param.GetInt ("nextPoints");
		GameData.playerData.MatchTotalDuel = result.Param.GetInt ("tDuel");
		GameData.playerData.WinDuel = result.Param.GetInt ("wDuel");
		GameData.playerData.Id = result.Param.GetLong ("id");
		GameData.playerData.Rank = result.Param.GetInt ("weekly");
		GameData.playerData.TotalGame = result.Param.GetInt ("tGame");
		GameData.playerData.TotalQuiz = result.Param.GetInt ("tQuiz");
		GameData.playerData.BestScore = result.Param.GetInt ("bTime");
		GameData.playerData.LastScore = result.Param.GetInt ("lScore");
		GameData.playerData.TotalCorrect = result.Param.GetInt ("tCorrect");
		GameData.playerData.TotalWrong = result.Param.GetInt ("tWrong");
		GameData.playerData.SfsId = result.Param.GetInt ("sfsId");
		GameData.playerData.FacebookID = result.Param.GetUtfString ("fid");
		GameData.playerData.Email = result.Param.GetUtfString ("email");
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
		if (RefreshDataDelegate != null) {
			RefreshDataDelegate ();
			//RefreshDataDelegate = null;
		}
	}

	public void SendLogOutFacebook ()
	{
		Debug.Log ("Logout facebook");
		ISFSObject obj = new SFSObject ();
		obj.PutUtfString ("facebook", SystemInfo.deviceUniqueIdentifier);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_LOGOUT, obj));
	}

	public void SendLogOutEmail ()
	{
		Debug.Log ("Logout email");
		ISFSObject obj = new SFSObject ();
		obj.PutUtfString ("email", SystemInfo.deviceUniqueIdentifier);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_LOGOUT, obj));
	}

	public void sendLPriceL ()
	{
		ISFSObject param = new SFSObject ();
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.LEADERBOARD_PRICE_LIST, param));
	}

	public void responseLeaderboardPriceList (BaseObject result)
	{
		List<PrizeData> list = new List<PrizeData> ();
		ISFSArray arr = result.Param.GetSFSArray ("lpricel");
		for (int i = 0; i < arr.Size (); i++) {
			PrizeData p = new PrizeData ();
			p.Coin = arr.GetSFSObject (i).GetInt ("coins");
			p.Gem = arr.GetSFSObject (i).GetInt ("gems");
			list.Add (p);
		}
		if (LPriceLDelegate != null) {
			LPriceLDelegate (list);
		}
	}

	public void SendTips ()
	{
		ISFSObject param = new SFSObject ();
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_RANDOM_TIP, param));
	}
    
    public void ResponseTip (BaseObject result)
	{
		GameData.tips = result.Param.GetUtfString ("tips");
		if (TipsDelegate != null) {
			
			TipsDelegate ();
			TipsDelegate = null;
		}
	}

	public void SendRequestRevengeList ()
	{
		ISFSObject param = new SFSObject ();
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_REVENGE, param));
	}

	public void ResponseRevengeListDelegate (BaseObject result)
	{
		List<PlayerData> list = new List<PlayerData> ();
		ISFSArray arr = result.Param.GetSFSArray ("lrv");
		for (int i = 0; i < arr.Size (); i++) {
			PlayerData p = new PlayerData ();
			p.FacebookID = arr.GetSFSObject (i).GetUtfString ("fid");
			p.Display = arr.GetSFSObject (i).GetUtfString ("display");
			p.IsOnline = arr.GetSFSObject (i).GetBool ("isOnline");
			p.BattleTag = arr.GetSFSObject (i).GetUtfString ("battletag");
			p.Id = arr.GetSFSObject (i).GetInt ("id");
			p.Level = arr.GetSFSObject (i).GetInt ("level");
			p.IsGame = arr.GetSFSObject (i).GetBool ("isGame");
			//p.TitleId = arr.GetSFSObject(i).GetInt("title_id");
			list.Add (p);
		}

		if (RevengeListDelegate != null) {
			RevengeListDelegate (list);
			//RevengeListDelegate = null;
		}
	}

	public void responseTicket (BaseObject result)
	{
		GameData.playerData.Token = result.Param.GetInt ("cTicket");
		Debug.Log ("ticket: " + GameData.playerData.Token);
		GameData.playerData.CurrentScore = result.Param.GetInt ("cScore");
		Debug.Log ("current score: " + GameData.playerData.CurrentScore);
		if (BuyTicKetDelegate != null) {
			BuyTicKetDelegate ();
			BuyTicKetDelegate = null;
		}
	}

	public void sendFeedBack (string msg)
	{
		ISFSObject param = new SFSObject ();
		param.PutUtfString ("msg", msg);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_FEEDBACK, param));
	}

	public void responseChangeLanguage (BaseObject result)
	{
		LanguageData data = new LanguageData ();
		data.ID = result.Param.GetInt ("id");
		data.Code = result.Param.GetUtfString ("code");
		data.Name = result.Param.GetUtfString ("name");
		if (ChangeLanguageDelegate != null) {
			ChangeLanguageDelegate (data);
			ChangeLanguageDelegate = null;
		}
	}

	public void sendStartGameSolo (bool resume)
	{
		ISFSObject param = new SFSObject ();

		param.PutBool ("solo", true);
		if (resume) {
			param.PutBool ("resume", resume);
		}

		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_START, param));
	}

	public void sendStartGameMulti (int mode, List<int> listInvite, int gem_fee, int coin_fee)
	{
		ISFSObject param = new SFSObject ();
		switch (mode) {
		case 1:
			param.PutBool ("pvp", true);
			break;
		case 2:
			param.PutBool ("multi", true);
			break;
		}
		//param.PutInt("setQuiz",setQuestion);
		param.PutIntArray ("inviteList", listInvite.ToArray ());
		param.PutInt ("gemFee", gem_fee);
		param.PutInt ("coinFee", coin_fee);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_START, param));
	}

	public void sendStartGameMultiRandom (int mode, bool isRandom, int gem_fee, int coin_fee)
	{
		Debug.Log ("okRanđom");
		ISFSObject param = new SFSObject ();
		switch (mode) {
		case 1:
			param.PutBool ("pvp", true);
			break;
		case 2:
			param.PutBool ("multi", true);
			break;
		}
		//param.PutInt("setQuiz",setQuestion);
		param.PutBool ("random", isRandom);
		param.PutInt ("gemFee", gem_fee);
		param.PutInt ("coinFee", coin_fee);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_START, param));
	}

	public void sendCancelInviteRandom (bool isCancel)
	{
		Debug.Log ("Cancel: " + isCancel);
		SFSObject param = new SFSObject ();
		param.PutBool ("cancel", isCancel);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_INVITE, param));
	}

	public void responeStartGameMulti (BaseObject result)
	{

		DuelData.coinFee = result.Param.GetInt ("coinFee");
		DuelData.gemFee = result.Param.GetInt ("gemFee");
		DuelData.senderTime = result.Param.GetInt ("waitTime");
		DuelData.receiverTime = result.Param.GetInt ("time");
		DuelData.ownerName = result.Param.GetUtfString ("ownerName");
		DuelData.rName = result.Param.GetUtfString ("rName");
		DuelData.ownerBattleTAG = result.Param.GetUtfString ("battletag");
		if (SendStartGameMultiDelegate != null) {
			SendStartGameMultiDelegate ();
			//SendStartGameMultiDelegate = null;
		}

	}

	public void responseStartGame (BaseObject result)
	{
		GameData.time = result.Param.GetInt ("time");
		GameData.totalQuestion = result.Param.GetInt ("numQuiz");
		Debug.Log ("totalQues: " + GameData.totalQuestion);
		GameData.currentQuestion = result.Param.GetInt ("currQuiz");

		if (responseStartGameDelegate != null) {
			responseStartGameDelegate ();
			responseStartGameDelegate = null;
		}
	}

	public void responseAskCheckPoint (BaseObject result)
	{
		int data = result.Param.GetInt ("answer");
		if (ResultAskCheckPointDelegate != null) {
			ResultAskCheckPointDelegate (data);
			ResultAskCheckPointDelegate = null;
		}
	}

	public void sendAskCheckPoint (int answer)
	{
		ISFSObject param = new SFSObject ();
		param.PutInt ("answer", answer);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.ASK, param));
	}

	public void responseResultAskCheckPoint (BaseObject result)
	{
		int data = result.Param.GetInt ("answer");
		if (ResultAskCheckPointDelegate != null) {
			ResultAskCheckPointDelegate (data);
			ResultAskCheckPointDelegate = null;
		}
	}

	public void sendStopGame ()
	{
		ISFSObject param = new SFSObject ();
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.STOP, param));
	}

	public void responseStopGame (BaseObject result)
	{

		GameData.weekBest = result.Param.GetBool ("weekBest");
		GameData.questAnswered = result.Param.GetInt ("total_question");
		GameData.score = result.Param.GetInt ("score");
		GameData.scoreBonus = result.Param.GetInt ("score_bonus");
		Debug.Log ("con cac: " + GameData.weekBest + "/ " + GameData.questAnswered + "/ " + GameData.score);
		WinnerData.winnerTAG = result.Param.GetUtfString ("wtag");
		WinnerData.winnerCoin = result.Param.GetInt ("coins");
		WinnerData.winnerPointCorrect = result.Param.GetInt ("pointCorrect");
		WinnerData.winnerPointTotal = result.Param.GetInt ("pointTotal");
		WinnerData.win = result.Param.GetInt ("win");

		if (GameStopDelegate != null) {
			GameStopDelegate ();
			GameStopDelegate = null;
		}
	}

	public void sendLeaderBoard ()
	{
		ISFSObject param = new SFSObject ();
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_LEADERBOARD, param));
	}

	public void responseLeaderBoard (BaseObject result)
	{
		List<PlayerData> list = new List<PlayerData> ();
		if (LeaderBoardDelegate != null) {
			ISFSArray arr = result.Param.GetSFSArray ("lboard");
			for (int i = 0; i < arr.Size (); i++) {
				PlayerData p = new PlayerData ();
				p.Id = arr.GetSFSObject (i).GetInt ("id");
				p.Display = arr.GetSFSObject (i).GetUtfString ("display");
				p.FacebookID = arr.GetSFSObject (i).GetUtfString ("fbid");
//				p.Token = arr.GetSFSObject (i).GetLong ("token");
				p.Coin = arr.GetSFSObject (i).GetLong ("coin");
				p.Trophy = arr.GetSFSObject (i).GetLong ("tropphy");
				p.TitleId = arr.GetSFSObject (i).GetInt ("titleid");
				p.Fullname = arr.GetSFSObject (i).GetUtfString ("fullname");
				p.Address = arr.GetSFSObject (i).GetUtfString ("address");
				p.PhoneNumber = arr.GetSFSObject (i).GetUtfString ("phone");
//				p.Email = arr.GetSFSObject (i).GetUtfString ("email");
				p.Score = arr.GetSFSObject (i).GetInt ("score");
				p.AvatarURL = arr.GetSFSObject (i).GetUtfString ("avatar");
				list.Add (p);

			}
			LeaderBoardDelegate (list);
			LeaderBoardDelegate = null;

		}

	}

	public void SearchPlayer (string searchKey)
	{
		ISFSObject param = new SFSObject ();
		param.PutUtfString ("searchKey", searchKey);
		BaseService.Instance.smartFox.Send (new ExtensionRequest (CommandList.CLIENT_FIND, param));
	}

	public void responseSearchPlayer (BaseObject result)
	{
		List<PlayerData> list = new List<PlayerData> ();
		ISFSArray arr = result.Param.GetSFSArray ("lFound");
		for (int i = 0; i < arr.Size (); i++) {
			PlayerData p = new PlayerData ();
			p.FacebookID = arr.GetSFSObject (i).GetUtfString ("fid");
			p.Display = arr.GetSFSObject (i).GetUtfString ("display");
			p.IsOnline = arr.GetSFSObject (i).GetBool ("isOnline");
			p.BattleTag = arr.GetSFSObject (i).GetUtfString ("battletag");
			p.Id = arr.GetSFSObject (i).GetInt ("id");
			p.Level = arr.GetSFSObject (i).GetInt ("level");
			p.IsGame = arr.GetSFSObject (i).GetBool ("isGame");
			list.Add (p);
		}
		if (SearchPlayerDelegate != null) {
			SearchPlayerDelegate (list);
			SearchPlayerDelegate = null;
		}

	}

	public void responseAnswerInvite (BaseObject result)
	{
		bool isAccept = result.Param.GetBool ("accepted");
		if (AnswerInvitationDelegate != null) {
			AnswerInvitationDelegate (isAccept);
			AnswerInvitationDelegate = null;
		}
		if (QK13_7_MatchOver_Controller.isRematchClick) {
			GameObject g = GameObject.Find ("QK13.7-MatchOver"); 
			g.GetComponent<QK13_7_MatchOver_Controller> ().turnOffLoading (isAccept);
		}
	}

	public void responseEndRound (BaseObject result)
	{
		//Debug.Log("RESPONE 26");
		WinnerData.winnerRoundName = result.Param.GetUtfString ("wname");
		WinnerData.winnerRoundID = result.Param.GetInt ("wid");
		WinnerData.winnerTAG = result.Param.GetUtfString ("wtag");
		WinnerData.isDRAW = result.Param.GetBool ("isDraw");

		WinnerData.winnerCoin = result.Param.GetInt ("coins");
		WinnerData.winnerPointCorrect = result.Param.GetInt ("pointCorrect");
		WinnerData.winnerPointTotal = result.Param.GetInt ("pointTotal");

		if (EndRoundDelegate != null) {
			EndRoundDelegate ();
			EndRoundDelegate = null;
		}
	}

	public void responsePlayerOnlineList (BaseObject result)
	{
		List<PlayerData> onlineList = new List<PlayerData> ();
		ISFSArray arr = result.Param.GetSFSArray ("lFound");
		for (int i = 0; i < arr.Size (); i++) {
			PlayerData p = new PlayerData ();
			p.FacebookID = arr.GetSFSObject (i).GetUtfString ("fid");
			p.Display = arr.GetSFSObject (i).GetUtfString ("display");
			p.IsOnline = arr.GetSFSObject (i).GetBool ("isOnline");
			p.BattleTag = arr.GetSFSObject (i).GetUtfString ("battletag");
			p.Id = arr.GetSFSObject (i).GetInt ("id");
			p.Level = arr.GetSFSObject (i).GetInt ("level");
			p.IsGame = arr.GetSFSObject (i).GetBool ("isGame");
			onlineList.Add (p);
		}

		if (GetListPlayerLobby != null) {
			GetListPlayerLobby (onlineList);
			//GetListPlayerLobby = null;
		}

	}
	public void responseFAQ(BaseObject result)
	{
		List<FAQData> lstFAQ = new List<FAQData> ();
		ISFSArray arr = result.Param.GetSFSArray ("arr1");
		for (int i = 0; i < arr.Size (); i++) {
			FAQData f = new FAQData ();

//			f.Content = arr.GetUtfString ("all");
//			f.lstArr1 = arr.GetSFSObject (i).GetUtfString ("arr" + i.ToString ());
		}
	}


	public void responseProduct(BaseObject result){
		List<ProductData> listProduct = new List<ProductData> ();
		ISFSArray arr = result.Param.GetSFSArray ("lprod");
		for (int i = 0; i < arr.Size (); i++) {
			ProductData pD = new ProductData ();
			pD.InStock = arr.GetSFSObject (i).GetBool ("in_stock");
			pD.QTY = arr.GetSFSObject (i).GetInt ("qty");
			pD.PriceGem = arr.GetSFSObject (i).GetInt ("price_gems");
			pD.Description = arr.GetSFSObject (i).GetUtfString ("description");
			pD.ImageTab = arr.GetSFSObject (i).GetUtfString ("imagetab");
			pD.ID = arr.GetSFSObject (i).GetInt ("id");
			pD.Title = arr.GetSFSObject (i).GetUtfString ("title");

			if (arr.GetSFSObject (i).GetSFSArray ("limg").Count > 0 && arr.GetSFSObject (i).GetSFSArray ("limg") != null) {
				ISFSArray tempArray = arr.GetSFSObject (i).GetSFSArray ("limg");
				for (int j = 0; j < tempArray.Count; j++) {
					ImageData iD = new ImageData ();
					iD.PositionNum = tempArray.GetSFSObject (j).GetInt ("position_num");
					iD.ImageURL = tempArray.GetSFSObject (j).GetUtfString ("image");
					pD.ListImg.Add (iD);
				}
			}

			pD.PriceCoin = arr.GetSFSObject(i).GetInt("price_coins");
			listProduct.Add (pD);
		}
		if (ProductDelegate != null) {
			ProductDelegate (listProduct);
			ProductDelegate = null;
		}
	}

}
