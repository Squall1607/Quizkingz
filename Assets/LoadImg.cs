using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class LoadImg : MonoBehaviour
{
	
	public GameObject ava;
	Texture2D img;
	public GameObject txtQuestion;
	public GameObject overlay;
	string value;

	void Start ()
	{
		
		ava.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("687474703a2f2f646c2e64726f70626f782e636f6d2f752f34383239313231372f53637265656e73686f74732f787770642e706e67");

	}

	public void onPointerDown ()
	{
		Debug.Log ("OnDown");
		txtQuestion.SetActive (false);
		overlay.SetActive (false);
	}

	public void onPointerUp ()
	{
		Debug.Log ("OnUp");
		txtQuestion.SetActive (true);
		overlay.SetActive (true);
	}

	//	void OnGUI(){
	//		GUI.DrawTexture(new Rect(10,10,800,678), img, ScaleMode.ScaleToFit, true, 1f);
	//	}
}
