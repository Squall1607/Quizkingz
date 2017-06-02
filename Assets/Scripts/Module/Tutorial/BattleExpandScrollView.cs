using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BattleExpandScrollView : MonoBehaviour {

	public ScrollRect myScrollRect;

	void OnEnable(){
		Debug.Log ("con cac");
		myScrollRect.verticalNormalizedPosition = 1f;
	
	}
}
