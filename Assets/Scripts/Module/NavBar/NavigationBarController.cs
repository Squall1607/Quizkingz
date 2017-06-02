using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NavigationBarController : BasePageController {


	public List<GameObject> lstActive;
	//public List<GameObject> lstOpen;
	public List<GameObject> lstNav;
	//public Animator mainAnim;
	public GameObject clickedBG;

	public GameObject QK3_3_3_MatchStyle;
	public GameObject QK07_GameStart;
	public GameObject QK07_6_1_GameOver;
    public GameObject tutorial;
    public GameObject MainBlack;
	public GameObject MainActiveBG;
    
    bool flag;

	//public GameObject NavBar;
	//public GameObject NavBarPopup;
	public GameObject fakeMain;
	public GameObject QK12_Battle,QK13_PreGame, QK13DUEL;
    public CommonScript cs;

    public Sprite UIYellow;
	public Sprite UIWhite;
	public Sprite UIBlack;

    public List<GameObject> lstResetON, lstResetOFF;

    public void reset()
    {
        foreach (var item in lstResetON)
        {
            item.SetActive(true);
        }

        foreach (var item in lstResetOFF)
        {
            item.SetActive(false);
        }
    }

    void OnEnable(){
        if (QK07_GameStart.activeSelf || QK07_6_1_GameOver.activeSelf || QK13_PreGame.activeSelf || QK13DUEL.activeSelf)
        {
            // tắt mainBG
            clickedBG.SetActive(false);
            foreach (var item in lstActive)
            {
                item.SetActive(false);
            }

        }
        else
        {
            //for (int i = 0; i < lstNav.Count; i++)
            //{
            //    if (lstNav[i].activeSelf)
            //    {
            //        clicked2(i, clickedBG);
            //    }
            //}
        }
        
	}

	void yellow(){
		MainBlack.GetComponent<Image>().sprite = UIYellow;

	}
	void white(){
		MainBlack.GetComponent<Image>().sprite = UIWhite;
		//iTween.ScaleTo (MainBlack, iTween.Hash("z",1,"time",0.5f,"oncomplete", "black","oncompletetarget",gameObject));
		iTween.ScaleTo (MainBlack, iTween.Hash("scale",new Vector3(1f,1f,1f),"time",0.2f,"oncomplete", "black","oncompletetarget",gameObject));
	}
	void white2(){
		
		MainBlack.GetComponent<Image>().sprite = UIWhite;
		iTween.ScaleTo (MainBlack, iTween.Hash("scale", new Vector3(1.15f,1.15f,1f),"time",0.2f,"oncomplete", "yellow","oncompletetarget",gameObject));
	}
	void white3(){
		MainBlack.GetComponent<Image>().sprite = UIWhite;
		iTween.ScaleTo (MainBlack, iTween.Hash("scale", new Vector3(1f,1f,1f),"time",0.2f,"oncomplete", "black","oncompletetarget",gameObject));
		clickedBG.SetActive (false);


	}
	void black(){

		MainBlack.GetComponent<Image>().sprite = UIBlack;
		
	}


	void mXoayTrai()
	{
		if (QK3_3_3_MatchStyle.activeSelf) {
			QK3_3_3_MatchStyle.GetComponent<QK03_3_MatchStyle_Controller> ().SceneSwitch ();
			MainActiveBG.SetActive (false);

			iTween.RotateTo (MainBlack, iTween.Hash ("z", 720, "time", 0.7f, "onstart", "white", "onstarttarget", gameObject, "easetype", iTween.EaseType.linear));                                               
		} else if (QK07_GameStart.activeSelf || QK12_Battle.activeSelf || QK07_6_1_GameOver.activeSelf || QK13_PreGame.activeSelf) {
			if (fakeMain.activeSelf) {
				fakeMain.SetActive (false);
			}
			MainActiveBG.SetActive (false);
			black ();
			iTween.RotateTo (MainBlack, iTween.Hash ("z", 720, "time", 0.7f, "onstart", "white", "onstarttarget", gameObject, "easetype", iTween.EaseType.linear));                                               



		} else {
            MainActiveBG.SetActive(false);
            iTween.RotateTo (MainBlack, iTween.Hash ("z", 720, "time", 0.7f, "onstart", "white", "onstarttarget", gameObject, "easetype", iTween.EaseType.linear));                                               

		}
	}

    //back button from QK12 to Q03
    public void xoayTemp() {
        MainActiveBG.SetActive(false);
        black();
        iTween.RotateTo(MainBlack, iTween.Hash("z", 720, "time", 0.7f, "onstart", "white", "onstarttarget", gameObject, "easetype", iTween.EaseType.linear));
    }

    void mXoayPhai()
	{
		if (QK3_3_3_MatchStyle.activeSelf) {
			QK3_3_3_MatchStyle.GetComponent<QK03_3_MatchStyle_Controller> ().SceneSwitch ();
			MainActiveBG.SetActive (false);
			iTween.RotateTo (MainBlack, iTween.Hash ("z", -720, "time", 0.7f, "onstart", "white", "onstarttarget", gameObject, "easetype", iTween.EaseType.linear));
		} else if (QK07_GameStart.activeSelf || QK12_Battle.activeSelf|| QK07_6_1_GameOver.activeSelf || QK13_PreGame.activeSelf) {
			if (fakeMain.activeSelf) {
				fakeMain.SetActive (false);
			}
			MainActiveBG.SetActive (false);
			black ();
			iTween.RotateTo (MainBlack, iTween.Hash ("z", -720, "time", 0.7f, "onstart", "white", "onstarttarget", gameObject, "easetype", iTween.EaseType.linear));

		} else {
            MainActiveBG.SetActive(false);
            iTween.RotateTo (MainBlack, iTween.Hash ("z", -720, "time", 0.7f, "onstart", "white", "onstarttarget", gameObject, "easetype", iTween.EaseType.linear));

		}
	}

	void moveNav(int x , GameObject bg){
		float y = 0;
		switch (x) {

		case 0:
			y = -318;
			break;
		case 1:
			y = -158;
			break;
		case 2:
			y = 170;
			break;
		case 3:
			y = 328;
			break;
		case 4:
			y = 5;
			break;

		default:
			break;
		}

		iTween.MoveTo(bg, iTween.Hash("position", new Vector3(y, -650.4998f, 0), "islocal", true, "time", 1f,"oncomplete", "none"));
	}

    //tắt bật icon navbar
    public void clicked(GameObject g){
		for (int i = 0; i < lstActive.Count; i++) {
			if (lstActive [i] == g) {
				
				lstActive [i].SetActive (true);
				clickedBG.SetActive (true);

				if (i == 0 || i == 1) {
					mXoayTrai ();
				} else if (i == 2 || i == 3) {
					mXoayPhai ();
				}

				moveNav (i,clickedBG);
                //MainActiveBG.SetActive(false);
                //fakeMain.SetActive(false);

            } else if (lstActive [i] == MainBlack) {
				lstActive [i].SetActive (true);

			} else {
				lstActive [i].SetActive (false);
			}
		}
	}

    
    public void clicked2(int g , GameObject bg){
		for (int i = 0; i < lstActive.Count; i++) {
			if (i == g) {

				lstActive [i].SetActive (true);
				bg.SetActive (true);


				moveNav (i,bg);
                //MainActiveBG.SetActive(false);
                //fakeMain.SetActive(false);

            } else if (lstActive [i] == MainBlack) {
				lstActive [i].SetActive (true);

			} else {
				lstActive [i].SetActive (false);
			}
		}
	}

	//public void openSceneNavba(GameObject got)
	//{
	//	for (int j = 0; j < lstOpen.Count; j++) {
	//		if (lstOpen [j] == got) {
	//			lstOpen [j].SetActive (true);
	//		} else {
	//			lstOpen [j].SetActive (false);
	//		}
	//	}
	//}	

	public void clickMain(){
		Invoke ("clickM", 0.2f);
	}

	public void clickM()
	{
		//iTween.
		if (!MainActiveBG.activeSelf) {
			//flag = true;
			MainBlack.GetComponent<Button> ().enabled = false;
			MainActiveBG.SetActive (true);
			iTween.RotateTo (MainBlack, iTween.Hash ("rotation", new Vector3 (0f, 0f, 360 * 3f), "time", 2f, "onstart", "white2", "onstarttarget", gameObject));
            QK3_3_3_MatchStyle.GetComponent<QK03_3_MatchStyle_Controller>().up();

        } else {

			MainActiveBG.SetActive (false);
			MainBlack.GetComponent<Button> ().enabled = false;
			iTween.RotateTo (MainBlack, iTween.Hash ("rotation", new Vector3 (0f, 0f, 360 * 3f), "time", 1.5f, "onstart", "white3", "onstarttarget", gameObject));
            QK3_3_3_MatchStyle.GetComponent<QK03_3_MatchStyle_Controller>().down();
        }
	}

    void onDisable()
    {

    }
}
