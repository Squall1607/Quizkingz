using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PandaLoad2 : MonoBehaviour {

    public GameObject SplashPanda;
    // Use this for initialization
    void Start () {
        StartCoroutine(loadAsync("GamePlay", 1f));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        //SplashPanda.SetActive(true);
    }

    private IEnumerator loadAsync(string levelName, float seconds)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);
        operation.allowSceneActivation = false;
        yield return new WaitForSeconds(seconds);
        operation.allowSceneActivation = true;
        Debug.Log("load done");


    }
}
