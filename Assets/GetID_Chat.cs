using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetID_Chat : MonoBehaviour {


    public string name;
    public string id;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
   

    public void onClick()
    {
        string tag = this.GetComponentsInChildren<Text>(true)[2].text;
        name = this.GetComponentsInChildren<Text>(true)[1].text;
        id = this.GetComponentsInChildren<Text>(true)[0].text;
        //Debug.Log(name + " " + id+ " " + tag);

        PlayerData p = new PlayerData();
        p.Id = long.Parse(id);
        p.Display = name;
        p.BattleTag = tag;

        GameObject g = GameObject.Find("QK12.2 - ChatFind");
        g.GetComponent<QK12_2_Chat_Controller>().Display(p);
        
    }

   
}
