using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class newPandaLoading : BasePageController
{

    public GameObject bgW;
    public SkeletonAnimation skeletonAnimation;
    
    //public string levelName;
   

    public void Start()
    {
        skeletonAnimation = this.GetComponent<SkeletonAnimation>();

        //if (PlayerPrefs.HasKey("username"))
        //{
        //    StartCoroutine(loadAsync("GamePlay", 0f));
        //}
        //else
        //{
        //    StartCoroutine(loadAsync("GamePlay",2.5f));
        //}

        StartCoroutine(loadAsync("GamePlay",2.5f));
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnEnable()
    {
        
    }

    void panda()
    {
        Invoke("fade", 2.1f);

        skeletonAnimation.Initialize(false);
        skeletonAnimation.state.SetAnimation(1, "animation", false);
        skeletonAnimation.AnimationName = "animation";
    }

    void fade()
    {
        skeletonAnimation.state.SetAnimation(2, "fade", false);
        InvokeRepeating("abc", 0.3f, 0.01f);
       
    }

    void abc()
    {

        bgW.GetComponent<CanvasGroup>().alpha -= 0.2f;
        if (bgW.GetComponent<CanvasGroup>().alpha < 0)
        {
            CancelInvoke();
        }
       
    }

    //public void StartLoading()
    //{
    //    StartCoroutine("load");
    //}

    //IEnumerator load()
    //{
    //    //Debug.LogWarning("ASYNC LOAD STARTED - " +
    //    //   "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
    //    async = SceneManager.LoadSceneAsync("Gameplay");
    //    async.allowSceneActivation = false;
    //    yield return async;
    //}

    //public void ActivateScene()
    //{
    //    async.allowSceneActivation = true;
    //}

    private IEnumerator loadAsync(string levelName,float seconds)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);
        operation.allowSceneActivation = false;
        yield return new WaitForSeconds(seconds);
        operation.allowSceneActivation = true;
        Debug.Log("load done");
    }

}
