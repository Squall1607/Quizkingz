using UnityEngine;
using System.Collections;

public class Tutorial : BasePageController {

    //public GameObject navBar, navBarPopup;
    public GameObject bgClickedPopup, PanelBlurPopUp;
    // Use this for initialization
    void Start () {
	
	}
	

	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {

        //if (!navBar.activeSelf)
        //{
        //    navBar.SetActive(true);
        //    navBarPopup.SetActive(false);

        //    navBar.GetComponent<NavigationBarController>().clicked2(0, bgClicked);



        //}

        //if (navbarPopup.activeSelf)
        //{

        //    navbarPopup.GetComponent<NavigationBarController>().clicked2(i, bgClickedPopup);

        //}
        PanelBlurPopUp.SetActive(true);
    }

    void OnDisable()
    {
        PanelBlurPopUp.SetActive(false);
    }
}
