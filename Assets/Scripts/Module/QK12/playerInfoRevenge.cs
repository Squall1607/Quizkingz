using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerInfoRevenge : MonoBehaviour {

    public GameObject Invite;
    public GameObject bg;
    //public QK14_2_Fee qk14_2, QK14_2_Fee;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void back()
    {
        iTween.MoveTo(Invite, iTween.Hash("position", new Vector3(-902f, Invite.transform.localPosition.y, 0), "islocal", true, "time", 0.5f, "oncomplete", "show", "oncompletetarget", gameObject));

        bg.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, Time.deltaTime * 0.9f);
    }
    void hide()
    {
        bg.SetActive(false);
    }
    void show()
    {
        bg.SetActive(true);
    }
    public void Click()
    {
        iTween.MoveTo(Invite, iTween.Hash("position", new Vector3(-100f, Invite.transform.localPosition.y, 0), "islocal", true, "time", 0.5f, "oncomplete", "hide", "oncompletetarget", gameObject));
        bg.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, Time.deltaTime * 0.9f);
    }

    public void YesClick()
    {
        GameObject go = GameObject.Find("bot");
        go.GetComponent<botController>().feeClick();
        go.GetComponent<botController>().id = this.GetComponentsInChildren<Text>(true)[0].text;
        back();
    }

}
