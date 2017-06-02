using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.UI;
using System.Collections;

public class shield : BasePageController {
	//public NavigationBarController navbar;
	public GameObject QK07GameStart,navPopup;
    public CommonScript cs;
    public SkeletonAnimation skeletonAnimation;
//	SkeletonData skeletonData;

	public void Start () {
		//skeletonAnimation = this.GetComponent<SkeletonAnimation>();
	}

	void OnEnable(){
		
		Invoke ("gameStart",1.6f);
		skeletonAnimation.Initialize(false);
		skeletonAnimation.state.SetAnimation (4, "shield_anim",false);
		skeletonAnimation.AnimationName = "shield_anim";
	}

	void OnDisable()
	{
		skeletonAnimation.AnimationName = "";
	}

	void gameStart(){
        this.DisplayScene(false);
        navPopup.SetActive (true);
        cs.turnOffAll(QK07GameStart);
		//navbar.openSceneNavba (QK07GameStart);
	}
}
