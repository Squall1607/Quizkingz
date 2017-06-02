using UnityEngine;
using System.Collections;

public class PrizeData{
	public int _coin;
	public int _gem;

	public int Coin{
		get{ return this._coin;}
		set{ this._coin = value;}
	}

	public int Gem{
		get{ return this._gem;}
		set{ this._gem = value;}
	}
}
