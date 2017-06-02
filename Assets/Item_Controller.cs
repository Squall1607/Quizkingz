using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Item_Controller : MonoBehaviour {

	string itemName;

	public void OnClickItem(){
		name = this.GetComponentInChildren<Text> ().text;
		GameObject g = GameObject.Find ("QK04B1.2-GiftShop");
		g.GetComponent<QK04B1_2_GiftShop_Controller> ().displayItemInformation (name);
	}
}
