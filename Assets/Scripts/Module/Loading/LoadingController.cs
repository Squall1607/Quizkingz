using UnityEngine;
using System.Collections;
using Spine.Unity;
public class LoadingController : MonoBehaviour {
	public GameObject FirstScene,MainMenu;
	public GameObject loadingNew;
	public GameObject loadingReturn;
	public GameObject navbar;
	//public GameObject Noti;

	string _username;
	string defaultName;

	public SkeletonAnimation welcomeAnim;
	public SkeletonAnimation returnAnim;

	void OnEnable()
	{
		welcomeAnim.Initialize(false);
		welcomeAnim.state.SetAnimation(4, "welcome", false);
		welcomeAnim.AnimationName = "welcome";

		returnAnim.Initialize(false);
		returnAnim.state.SetAnimation(4, "return", false);
		returnAnim.AnimationName = "return";
	}

	void OnDisable()
	{
		welcomeAnim.AnimationName = "";
		returnAnim.AnimationName = "";
	}

	// Use this for initialization
	void Start () {
		BaseService.Instance.OnConnectionDelegate+=startPlay;
	}

	public void startPlay(){
		StartCoroutine(runLogo());
	}

	public IEnumerator runLogo(){ 
		if (PlayerPrefs.HasKey("username")) {
			loadingReturn.SetActive (true);
			yield return new WaitForSeconds(3f);
			MainMenu.SetActive (true);
			navbar.SetActive (true);
			loadingReturn.SetActive (false);

		} else {
			loadingNew.SetActive (true);
			yield return new WaitForSeconds(5.6f);
			PlayerPrefs.SetString ("username", GameData.playerData.Display);
			PlayerPrefs.Save();
			FirstScene.SetActive(true);
		}

	}
}
