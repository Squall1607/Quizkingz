using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimerInviteRumble : MonoBehaviour {

	public Transform loadingBar;
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;
	public GameObject playerToInvite;
	public static bool isDone = false;


	void Update(){
		if(!isDone){
			if (currentAmount <= 20) {
				currentAmount += speed * Time.deltaTime;
			}
			loadingBar.GetComponent<Image> ().fillAmount = 1 - currentAmount / 20;
		}
		if (loadingBar.GetComponent<Image> ().fillAmount <= 0) {
			currentAmount = 0;
			loadingBar.GetComponent<Image> ().fillAmount = 1;
			isDone = true;
			//			loadingBar.GetComponent<Image> ().fillAmount = 1 - currentAmount / 20;
		}
	}
}