using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerQueue : MonoBehaviour {

    public Transform loadingBar;
    [SerializeField]
    private float currentAmount;
    [SerializeField]
    private float speed;
    //	public GameObject loading;
    //public Text status;
    
    public static bool isDone = false;


    void Update()
    {
        if (!isDone)
        {
            if (currentAmount <= 20)
            {
                currentAmount += speed * Time.deltaTime;
            }
            loadingBar.GetComponent<Image>().fillAmount = 1 - currentAmount / 20;
        }

        if (loadingBar.GetComponent<Image>().fillAmount <= 0)
        {

            currentAmount = 0;
            loadingBar.GetComponent<Image>().fillAmount = 1;
            isDone = true;

            Destroy(this);
            //			loadingBar.GetComponent<Image> ().fillAmount = 1 - currentAmount / 20;
        }
    }
}
