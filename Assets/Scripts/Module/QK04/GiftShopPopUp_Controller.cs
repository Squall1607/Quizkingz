using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GiftShopPopUp_Controller : BasePageController {

	public GameObject information;
	public GameObject confirmation;

	public GameObject btnConfirm;
	public GameObject btnCancel;
	public GameObject btnPurchase;
	public GameObject btnBack, PanelBlurPopUp;
	public GameObject imageContainer;
	//public GameObject Navbar;
	//public GameObject NavbarPopup;


	public ScrollRect myScrollView;

	void OnEnable(){
		displayContent (information,confirmation);
		displayBtn (btnConfirm,btnCancel,btnPurchase,btnBack);
        PanelBlurPopUp.SetActive(true);
        //Navbar.SetActive (true);
        //NavbarPopup.SetActive (false);
    }
	void  OnDisable(){
        PanelBlurPopUp.SetActive(false);
        //Navbar.SetActive (false);
        //NavbarPopup.SetActive (true);
		Debug.Log ("imageContainer.transform.childCount: " + imageContainer.transform.childCount);
		for (int i = 0; i < imageContainer.transform.childCount; i++) {
			Destroy (imageContainer.transform.GetChild (i).gameObject);
		}


    }

	public void onClickPurchase(){
		myScrollView.GetComponent<ScrollRect> ().horizontal = false;
		displayContent (confirmation, information);
		displayBtn (btnPurchase,btnBack,btnConfirm,btnCancel);
	}

	void displayBtn(GameObject obj1, GameObject obj2, GameObject obj3, GameObject obj4){
		obj1.SetActive (false);
		obj2.SetActive (false);
		obj3.SetActive (true);
		obj4.SetActive (true);
	}

	void displayContent(GameObject obj1, GameObject obj2){
		obj1.SetActive (true);
		obj2.SetActive (false);
	}
}
