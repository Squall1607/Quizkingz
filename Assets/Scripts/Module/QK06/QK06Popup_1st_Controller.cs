using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class QK06Popup_1st_Controller : BasePageController {

	public GameObject hideNoti;
	public GameObject txtClaimedNoti;
	public GameObject txtClaimNotiPopup;
	public GameObject hideClaimedNotiPopup, PanelBlurPopUp;
	public GameObject Particle;
	//public GameObject NavBar;
	//public GameObject NavBarPopUp;

	public SkeletonAnimation boxAnim;
	public float timer = 0.3f;


    void OnEnable()
    {
        PanelBlurPopUp.SetActive(true);
    }

    void OnDisable()
    {
        PanelBlurPopUp.SetActive(false);
    }

    void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			startCupAnim ();
		}
	}

	public void btnReturnClick()
	{
		boxAnim.AnimationName = "idle";
		Particle.SetActive(false);
		//NavBar.SetActive (false);
		//NavBarPopUp.SetActive (true);
	}
	public void startCupAnim()
	{
		//NavBar.SetActive (true);
		//NavBarPopUp.SetActive (false);
		timer = 0.3f;
		boxAnim.AnimationName = "pop";
	}

	public void clickBtnClaim()
	{
		hideNoti.SetActive (false);
		txtClaimedNoti.SetActive (true);
		txtClaimNotiPopup.SetActive (false);
		hideClaimedNotiPopup.SetActive (true);
		Particle.SetActive(true);
	}
}
