using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.UI;
using System.Collections;

public class panda : MonoBehaviour {
	
	public GameObject bgW;
	public SkeletonAnimation skeletonAnimation;
	//SkeletonData skeletonData;

	public void Start () {
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		//skeletonAnimation.state.SetAnimation (1, "animation",false);


	}


	void OnEnable(){
		Invoke ("fade", 2.1f);
		skeletonAnimation.Initialize(false);
		skeletonAnimation.state.SetAnimation (1, "animation",false);
		skeletonAnimation.AnimationName = "animation";
	}

	void fade(){
		skeletonAnimation.state.SetAnimation (2, "fade", false);
		InvokeRepeating ("abc",0.3f,0.01f);
	}

	void abc(){
		
		bgW.GetComponent<CanvasGroup>().alpha -= 0.2f;
		//Debug.Log (bgW.GetComponent<CanvasGroup>().alpha);
		if (bgW.GetComponent<CanvasGroup>().alpha<0) {
			CancelInvoke ();
		}
	}
}
