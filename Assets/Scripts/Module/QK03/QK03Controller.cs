using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QK03Controller : BasePageController {
	
	public Text name;
	public Text medal;
	public Text coin;
	public Text diamond;

	public Text weeklyRank;
	public Text totalGame;
	public Text totalQuiz;
	public Text	bestScore;
	public Text lastScrore;
	public Text cAnswer;
	public Text wAnswer;
	public Text rank;
	public Text txtInformation;

	public GameObject sprAvatar;
	public GameObject BGNotiTuto;
	//public GameObject brightHome;
	//public GameObject bgHome;

	public GameObject panel03;
	public ScrollRect myScrollRect;
    public GameObject StatTab, MessTab;

	public Image imgPoints;
	private float iPoints;
	private float iNextPoints;
	public Text txtPointEXP;

    public float timer = 10.0f;

	// Use this for initialization
	void Update () {
		timer -= Time.deltaTime;
		showNotiTuto ();
	}
	public void showNotiTuto(){
		if (timer < 0) {
			BGNotiTuto.SetActive (false);
		}
	}
	public void hideNotiTuto(){
		BGNotiTuto.SetActive (false);
	}

	void OnEnable(){
		
//		BaseService.Instance.smartFox.Disconnect ();
		myScrollRect.verticalNormalizedPosition = 1f;
		sprAvatar.GetComponent<Image>().sprite = GameData.playerData.Avartar;
		name.text = GameData.playerData.Display.ToUpper();
		txtInformation.text = "<color='#FFCB2BFF'>TAG #" + GameData.playerData.BattleTag.ToUpper()+"</color>  LVL. "+GameData.playerData.Level;
		medal.text = GameData.playerData.Trophy.ToString();
		coin.text = GameData.playerData.Coin.ToString();
		diamond.text = GameData.playerData.Gem.ToString();

		weeklyRank.text = GameData.playerData.Rank.ToString();
		totalGame.text = GameData.playerData.TotalGame.ToString();
		totalQuiz.text = GameData.playerData.TotalQuiz.ToString();
		bestScore.text = GameData.playerData.BestScore.ToString();
		lastScrore.text = GameData.playerData.LastScore.ToString();
		cAnswer.text = GameData.playerData.TotalCorrect.ToString();
		wAnswer.text = GameData.playerData.TotalWrong.ToString();
		rank.text = GameData.playerData.Rank.ToString () + " of " + GameData.playerData.TotalPlayer.ToString ();
		txtPointEXP.text = "+" + GameData.playerData.Points;

		iPoints = (float) GameData.playerData.Points;
		iNextPoints = (float) GameData.playerData.NextPoints;
		imgPoints.fillAmount = iPoints / iNextPoints;
		//brightHome.SetActive (true);
	//	bgHome.SetActive (true);

	}

	void OnDisable(){
		//brightHome.SetActive (false);
		//bgHome.SetActive (false);
	}

	public void turnOff(){
		panel03.SetActive (false);
	}

    public void StatsClick()
    {
        StatTab.SetActive(true);
        MessTab.SetActive(false);
    }

    public void MessClick()
    {
        StatTab.SetActive(false);
        MessTab.SetActive(true);
    }
}
