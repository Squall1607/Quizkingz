using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QK04D_PrizePopUp_Controller : BasePageController {

	//public GameObject NavBar;
	public GameObject NavBarPopUp;
	public GameObject gift, PanelBlurPopUp;
	public List<Text> listCoin;
	public Text txtGem;

	void OnEnable(){
		Debug.Log ("OnEnable");
        PanelBlurPopUp.SetActive(true);
        //NavBar.SetActive (true);
        //NavBarPopUp.SetActive (false);
        gift.SetActive (false);
		BaseService.Instance.gameService.LPriceLDelegate += displayPrizePopUp;
	}

	void OnDisable(){
        //NavBar.SetActive (false);
        //NavBarPopUp.SetActive (true);
        PanelBlurPopUp.SetActive(false);
        gift.SetActive (true);
	}

	public void displayPrizePopUp(List<PrizeData> list){
		Debug.Log ("displayPrizePopUp: ");
		for (int i = 0; i < list.Count; i++) {
			listCoin [i].text = list [i].Coin.ToString();
			txtGem.text = list [0].Gem.ToString();
		}
	}
}
