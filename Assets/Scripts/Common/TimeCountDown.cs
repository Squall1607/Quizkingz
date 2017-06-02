using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCountDown : MonoBehaviour {
	public Slider slider;
	float maxValue =15;
//	float maxTime =15f;
	// Update is called once per frame
	void Update () {
//		slider.value = maxValue;
//		maxTime -= Time.deltaTime;
		maxValue-=Time.deltaTime;
	}
}
