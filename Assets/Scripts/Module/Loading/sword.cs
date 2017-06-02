using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.UI;
using System.Collections;

public class sword : BasePageController {

	//public NavigationBarController navbar;
	public GameObject QK12Battle,navPopup;
    public CommonScript cs;

	public SkeletonAnimation skeletonAnimation;
	//SkeletonData skeletonData;

//	AtlasAsset atlasdata;
//	SkeletonDataAsset playerData;

	public void Start () {
		//skeletonAnimation = this.GetComponent<SkeletonAnimation>();
	}

	void OnEnable(){
		
		Invoke ("gameStart",2.2f);
		skeletonAnimation.Initialize(false);
		skeletonAnimation.state.SetAnimation (4, "sword_anim",false);
		skeletonAnimation.AnimationName = "sword_anim";
	}

	void OnDisable()
	{
		skeletonAnimation.AnimationName = "";
	}

	void gameStart(){
        this.DisplayScene(false);
        navPopup.SetActive (true);
        //navbar.openSceneNavba (QK12Battle);
        cs.turnOffAll(QK12Battle);
	}
}
