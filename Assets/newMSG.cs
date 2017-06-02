using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class newMSG : MonoBehaviour {
    public Text id;
    //public QK16_Chat_Window QK16_Chat_Window;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void msgClick()
    {
        if (!string.IsNullOrEmpty(id.text))
        {
            GameObject g = GameObject.Find("bot");
            //g.GetComponent<QK16_Chat_Window>()
        }
    }
}
