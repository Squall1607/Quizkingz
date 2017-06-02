using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Facebook.Unity;
public class getID : MonoBehaviour
{

    CanvasGroup canvasGroup;
    public string name;
    public string id;
    void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();


    }

    public void onClick()
    {
        string tag = this.GetComponentsInChildren<Text>(true)[2].text;
        name = this.GetComponentsInChildren<Text>(true)[1].text;
        id = this.GetComponentsInChildren<Text>(true)[0].text;
        //Debug.Log(name + " " + id+ " " + tag);

        PlayerData p = new PlayerData();
        p.Id = long.Parse(id);
        p.Display = name;
        p.BattleTag = tag;

        GameObject g = GameObject.Find("QK13.3-Find");
        g.GetComponent<qk13Find>().inviteClick(p);
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        float time = 1;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= (3 * Time.deltaTime) / time;
            yield return null;
        }
        GameObject p = this.transform.parent.gameObject;
        Destroy(p);
    }

}


