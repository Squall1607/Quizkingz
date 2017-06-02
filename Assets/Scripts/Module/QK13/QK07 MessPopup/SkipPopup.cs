using UnityEngine;
using System.Collections;

public class SkipPopup : BasePageController
{
    public GameObject PanelBlurPopUp;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
       
        PanelBlurPopUp.SetActive(true);
    }

    void OnDisable()
    {

        PanelBlurPopUp.SetActive(false);
    }
}
