using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData{
	public static PlayerData playerData;
	public static int[] questionPack;
	public static int totalQuestion;
	public static int time;
	public static int currentQuestion;
	public static bool isFinish;
	public static QuestionData question;
	public static string tips;
	public static string smallImage = "-small";
	public static int coin;
	public static int gem;
	//GameOver
	public static bool weekBest = false;
	public static int questAnswered;
	public static int score;
	public static int scoreBonus;
	//multi
	public static List<int> listPlayerInvite = new List<int>();
	public static string gameRoom;
    public static string room;
    public static int[] entryFee;
	public static List<PlayerData> friendList = new List<PlayerData> ();

}



