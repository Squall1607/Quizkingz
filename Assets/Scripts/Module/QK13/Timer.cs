using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Timer : BasePageController
{

	public Transform loadingBar;
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;
//	public GameObject loading;
	//public Text status;
	public GameObject playerToInvite;
	public static bool isDone = false;

    void OnEnable()
    {
        isDone = false;
        currentAmount = 0;
        loadingBar.GetComponent<Image>().fillAmount = 1;
    }

    void Update(){
		if(!isDone){
			if (currentAmount <= 20) {
				currentAmount += speed * Time.deltaTime;
			}
			loadingBar.GetComponent<Image> ().fillAmount = 1 - currentAmount / 20;
		}

		if (loadingBar.GetComponent<Image> ().fillAmount <= 0) {
            //qk13Find qk13 = new qk13Find();

            //Debug.Log("xxxxxxxxxxxxxx     " + qk13Find.next);
			currentAmount = 0;
			loadingBar.GetComponent<Image> ().fillAmount = 1;
			isDone = true;

            if (!qk13Find.next)
            {
                playerToInvite.SetActive(false);
            }


        }
	}
}