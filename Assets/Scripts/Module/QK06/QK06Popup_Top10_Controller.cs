using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class QK06Popup_Top10_Controller : BasePageController {

	// Use this for initialization
	public GameObject hideNoti;
	public GameObject txtClaimedNoti;
	public GameObject txtClaimNotiPopup;
	public GameObject hideClaimedNotiPopup;
	public GameObject Particle, PanelBlurPopUp;
	//public GameObject NavBar;
	//public GameObject NavBarPopUp;


	public SkeletonAnimation boxAnim;

	public float timer = 0.3f;

	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			startBoxAnim ();
		}
	}

	public void btnReturnClick()
	{
		boxAnim.AnimationName = "idle";
		Particle.SetActive(false);
		//NavBar.SetActive (false);
		//NavBarPopUp.SetActive (true);
	}
	public void startBoxAnim()
	{
		timer = 0.3f;
		boxAnim.AnimationName = "pop";
		//NavBar.SetActive (true);
		//NavBarPopUp.SetActive (false);
	}

	public void clickBtnClaim()
	{
		hideNoti.SetActive (false);
		txtClaimedNoti.SetActive (true);
		txtClaimNotiPopup.SetActive (false);
		hideClaimedNotiPopup.SetActive (true);
		Particle.SetActive(true);
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
