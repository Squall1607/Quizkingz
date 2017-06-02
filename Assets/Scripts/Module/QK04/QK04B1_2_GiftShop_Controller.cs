using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class QK04B1_2_GiftShop_Controller : BasePageController {

	public ScrollRect myScrollView;
	public GameObject allTab;
	public GameObject coinTab;
	public GameObject gemTab;

	//------------------------------------

	public GameObject allTabActive;
	public GameObject allTabDeactive;

	public GameObject coinTabActive;
	public GameObject coinTabDeactive;

	public GameObject gemTabActive;
	public GameObject gemTabDeactive;

	public List<Text> itemName;

	public GameObject itemPrefab;
	public GameObject itemImagePrefab;
	public GameObject dotPrefab;

	public GameObject scroll;
	public GameObject container;

	public GameObject imageScroll;
	public GameObject imageContainer;

	public GameObject dotScroll;

	public CameraController blur;
	public static List<ProductData> productList = new List<ProductData>();
	public static List<ImageData> imageList;
	public static List<ImageData> listImageData = new List<ImageData> ();

	public Text qty;
	public Text priceGems;
	public Text description;
	public Text title;
	public Text priceCoins;
	List<string> imageTab = new List<string> ();
	List<Sprite> imageTabList = new List<Sprite>();

	public List<GameObject> tabBackground = new List<GameObject> ();

	public GameObject giftShopPopup;
	//------------------------------------


	void OnEnable(){
		setActive (allTabActive,allTabDeactive,coinTabDeactive,coinTabActive,gemTabDeactive,gemTabActive);
		myScrollView.verticalNormalizedPosition = 1f;

		//Display image tab
		for (int i = 0; i < productList.Count; i++) {
			imageTab.Add (productList[i].ImageTab);
			if (imageTab [i] != "none") {
				StartCoroutine (Utils.LoadImage (imageTab [i], value => imageTabList.Add (value)));
			}
		}

		for (int j = 0; j < imageTabList.Count; j++) {
			tabBackground [j].GetComponent<Image> ().sprite = imageTabList [j];
		}
	}



	public void onClickAllTab(){
		setActive (allTabActive,allTabDeactive,coinTabDeactive,coinTabActive,gemTabDeactive,gemTabActive);
	}

	public void onClickCoinTab(){
		setActive (allTabDeactive,allTabActive,coinTabActive,coinTabDeactive,gemTabDeactive,gemTabActive);
	}

	public void onClickGemTab(){
		setActive (allTabDeactive,allTabActive,coinTabDeactive,coinTabActive,gemTabActive,gemTabDeactive);
	}

	void setActive(GameObject obj1, GameObject obj2, GameObject obj3, GameObject obj4, GameObject obj5, GameObject obj6){
		obj1.SetActive (true);
		obj2.SetActive (false);
		obj3.SetActive (true);
		obj4.SetActive (false);
		obj5.SetActive (true);
		obj6.SetActive (false);
	}

	//----------------------------------------------------------

	public void InstantiateItem(List<ProductData> listProduct){
		productList = listProduct;
		Debug.Log ("Instantiate Item");
		for (int i = 0; i < listProduct.Count; i++) {
			GameObject p = Instantiate(itemPrefab);
			Transform parent = scroll.transform.FindChild("Panel");
			p.transform.SetParent(parent);
			p.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
			p.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			p.GetComponentInChildren<Text> ().text = listProduct [i].Title.ToUpper();
		}

//		foreach (ProductData p in listProduct) {
//			Debug.Log ("Information: "+ p.QTY + "/ " + p.PriceGem + "/ " + p.Description + "/ " + p.PriceCoin + "/ " +  p.Title);
//			if (p.ListImg.Count > 0) {
//				foreach (ImageData img in p.ListImg) {
//					Debug.Log ("--- anh: " + img.PositionNum + "/url: " + img.ImageURL);
//				}
//			} else {
//				Debug.Log ("bo may deo co anh nhin con cac");
//			}
//		}
	}

	public void displayItemInformation(string n){
		giftShopPopup.SetActive (true);
		for (int i = 0; i < productList.Count; i++) {
			if (productList [i].Title.ToUpper() == n) {
				qty.text = productList [i].QTY.ToString();
				priceGems.text = productList [i].PriceGem.ToString();
				description.text = productList [i].Description;
				title.text = productList [i].Title.ToUpper();
				priceCoins.text = productList[i].PriceCoin.ToString();
					
				if (productList [i].ListImg.Count > 0) {
					foreach (ImageData img in productList[i].ListImg) {
						GameObject iP = Instantiate(itemImagePrefab);
						Transform iP_parent = imageScroll.transform.FindChild("Container");
						iP.transform.SetParent(iP_parent);
						iP.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
						iP.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
						//--------------------

						//--------------------
						StartCoroutine (Utils.LoadImage(img.ImageURL, value => iP.GetComponent<Image>().sprite = value));
					}
				} else {
					GameObject iP = Instantiate(itemImagePrefab);
					Transform iP_parent = imageScroll.transform.FindChild("Container");
					iP.transform.SetParent(iP_parent);
					iP.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
					iP.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
				}
			}
		}
	}

}



