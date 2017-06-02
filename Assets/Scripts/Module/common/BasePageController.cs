using UnityEngine;
using System.Collections;

public class BasePageController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	[System.Obsolete ("This is deprecated, please use DisplayScene instead.")]
	public virtual void SceneSwitch ()
	{
		if (gameObject.activeSelf) {
			gameObject.SetActive (false);
		} else {
			gameObject.SetActive (true);
		}
	}

	public virtual void DisplayScene (bool isShow)
	{
		if (isShow) {
			gameObject.SetActive (true);
		} else {
			gameObject.SetActive (false);
		}
	}
}