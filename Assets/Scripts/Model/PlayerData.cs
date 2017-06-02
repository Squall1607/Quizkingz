using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData{

	long _id;
	int _sfsId;
	long _gem;
	long _token;
	int _ticket;	//day la ticket
	int _currentScore;
	long _coin;
	long _trophy;
	string _display;
	int _titleId;
	string _fullname;
	string _address;
	string _phone;
	string _email;
	string _iemail;
	int _status;
	string _fId;
    int _level;
    string _battleTag;
	Sprite _avartar;
	bool _isGame;
    bool _isOnline;
	int _score;
	int _score_bonus;
	int _bonusPoint;
	int _correctPoint;
	int _totalPlayer;
	string _fbName;
	int _points;
	int _next_points;
	string _avatarURL;
	//UC03 information
	int _rank;
	int _totalGame;
	int _totalQuiz;
	int _bestScore;
	int _lastScore;
	int _totalCorrect;
	int _totalWrong;
	int _matchTotalDuel;
	int _winDuel;
	int _weeklyBest;

	private  List<GameObject> _lstNotiDuelInvi = new List<GameObject>();
	private  List<string> _lstNotiName = new List<string>();
	private  List<string> _lstNotiRoomName = new List<string>();
	private  List<int> _lstNotiCoin = new List<int>();
	private  List<GameObject> _lstRNotiDuelInvi = new List<GameObject>();
	private  List<string> _lstRNotiName = new List<string>();
	private  List<string> _lstRNotiRoomName = new List<string>();
	private  List<int> _lstRNotiCoin = new List<int>();

	public string AvatarURL{
		get{ return this._avatarURL;}
		set{ this._avatarURL = value;}
	}

	public int Points{
		get{ return this._points;}
		set{ this._points = value;}
	}
	public int NextPoints{
		get{ return this._next_points;}
		set{ this._next_points = value;}
	}
	public int TotalPlayer{
		get{ return this._totalPlayer;}
		set{ this._totalPlayer = value;}
	}

	public int WeeklyBest{
		get{ return this._weeklyBest;}
		set{ this._weeklyBest = value;}
	}

	public int WinDuel{
		get
		{
			return this._winDuel;
		}
		set
		{
			this._winDuel = value;
		}
	}
	public int MatchTotalDuel{
		get
		{
			return this._matchTotalDuel;
		}
		set
		{
			this._matchTotalDuel = value;
		}
	}
	public int CurrentScore{
		get
		{
			return this._currentScore;
		}
		set
		{
			this._currentScore = value;
		}
	}

	public int Rank{
		get
		{
			return this._rank;
		}
		set
		{
			this._rank = value;
		}
	}

	public int TotalGame{
		get
		{
			return this._totalGame;
		}
		set
		{
			this._totalGame = value;
		}
	}

	public int TotalQuiz{
		get
		{
			return this._totalQuiz;
		}
		set
		{
			this._totalQuiz = value;
		}
	}

	public int BestScore{
		get
		{
			return this._bestScore;
		}
		set
		{
			this._bestScore = value;
		}
	}

	public int LastScore{
		get
		{
			return this._lastScore;
		}
		set
		{
			this._lastScore = value;
		}
	}
		
	public int TotalCorrect{
		get
		{
			return this._totalCorrect;
		}
		set
		{
			this._totalCorrect = value;
		}
	}

	public int TotalWrong{
		get
		{
			return this._totalWrong;
		}
		set
		{
			this._totalWrong = value;
		}
	}

	public List<GameObject> NotiDuelInvi
	{
		get{ return _lstNotiDuelInvi;}
		set{_lstNotiDuelInvi = value; }
	}
	public List<string> NotiName
	{
		get{ return _lstNotiName;}
		set{ _lstNotiName = value;}
	}
	public List<string> NotiRoomName
	{
		get{ return _lstNotiRoomName;}
		set{ _lstNotiRoomName = value;}
	}
	public List<int> NotiCoin
	{
		get{ return _lstNotiCoin;}
		set{ _lstNotiCoin = value;}
	}
	public List<GameObject> RNotiDuelInvi
	{
		get{ return _lstRNotiDuelInvi;}
		set{_lstRNotiDuelInvi = value; }
	}
	public List<string> RNotiName
	{
		get{ return _lstRNotiName;}
		set{ _lstRNotiName = value;}
	}
	public List<string> RNotiRoomName
	{
		get{ return _lstRNotiRoomName;}
		set{ _lstRNotiRoomName = value;}
	}
	public List<int> RNotiCoin
	{
		get{ return _lstRNotiCoin;}
		set{ _lstRNotiCoin = value;}
	}

	public int Score{
		get
		{
			return this._score;
		}
		set
		{
			this._score = value;
		}
	}
	public int ScoreBonus{ 
		get
		{
			return this._score_bonus;
		}
		set
		{
			this._score_bonus = value;
		}
	}
	public int BonusPoint{
		get
		{
			return this._bonusPoint;
		}
		set
		{
			this._bonusPoint = value;
		}
	}

	public int CorrectPoint{
		get
		{
			return this._correctPoint;
		}
		set
		{
			this._correctPoint = value;
		}
	}

    public bool IsOnline
    {
        get
        {
            return this._isOnline;
        }
        set
        {
            this._isOnline = value;
        }
    }

	public bool IsGame
	{
		get
		{
			return this._isGame;
		}
		set
		{
			this._isGame = value;
		}
	}

    public Sprite Avartar{
		get
		{ 
			return this._avartar;
		}
		set
		{
			this._avartar = value;
		}
	}

    public int Level
    {
        get
        {
            return this._level;
        }
        set
        {
            this._level = value;
        }
    }

    public string BattleTag
    {
        get
        {
            return this._battleTag;
        }
        set
        {
            this._battleTag = value;
        }
    }

	public string FbName
	{
		get
		{
			return this._fbName;
		}
		set
		{
			this._fbName = value;
		}
	}

    public string FacebookID{
		get{ 
			return this._fId;
		}	set{ 
			this._fId = value;
		}
	}

	public int Status{
		get{
			return this._status;
		}
		set{
			this._status = value;
		}
	}

	public long Id{
		get{
			return this._id;
		}
		set{
			this._id = value;
		}
	}

	public long Gem{
		get{
			return this._gem;
		}
		set{
			this._gem = value;
		}
	}
	public long Token{
		get{
			return this._token;
		}
		set{
			this._token = value;
		}
	}

	public long Coin{
		get{
			return this._coin;
		}
		set{
			this._coin = value;
		}
	}

	public long Trophy{
		get{
			return this._trophy;
		}
		set{
			this._trophy = value;
		}
	}

	public int SfsId{
		get{ 
			return this._sfsId;	
		}
		set{ 
			this._sfsId = value;
		}
	}

	public int TitleId{
		get{ 
			return this._titleId;	
		}
		set{ 
			this._titleId = value;
		}
	}

	public string Fullname{
		get{ 
			return this._fullname;
		}
		set{ 
			this._fullname = value;
		}
	}

	public string Display{
		get{ 
			return this._display;
		}
		set{ 
			this._display = value;
		}
	}

	public string Address{
		get{ 
			return this._address;
		}
		set{ 
			this._address = value;
		}
	}

	public string PhoneNumber{
		get{ 
			return this._phone;
		}
		set{ 
			this._phone = value;
		}
	}

	public string Email{
		get{ 
			return this._email;
		}
		set{ 
			this._email = value;
		}
	}
	public string IEmail{
		get{ 
			return this._iemail;
		}
		set{ 
			this._iemail = value;
		}
	}
}
