using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class QuestExpandScrollView : MonoBehaviour {

	public ScrollRect myScrollRect;

	void OnEnable(){
		myScrollRect.verticalNormalizedPosition = 1f;
	}
}
