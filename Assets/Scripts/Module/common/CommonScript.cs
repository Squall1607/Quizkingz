using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommonScript : MonoBehaviour {

    public List<GameObject> lstQK; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void turnOffAll(GameObject g) {
        foreach (var item in lstQK)
        {
            if (item == g)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }
}
