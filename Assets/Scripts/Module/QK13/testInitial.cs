using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class testInitial : MonoBehaviour {
	public GameObject prefab;
	public GameObject grid;


    public float maxTime;
    public float minSwipeDistance;
    float startTime;
    float endTime;
    Vector3 startPos;
    Vector3 endPos;
    float swipeDistance;
    float swipeTime;
    float x = 0;
    public ScrollRect scroll;

    void Start() {
		//for (int y = 0; y < 6; y++) {
		//	GameObject go = Instantiate(prefab) as GameObject; 
		//	go.transform.parent = grid.transform;

  //      }
	}

    void OnEnable()
    {
       
    }

    // Update is called once per frame
    void Update () {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTime = Time.time;
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTime = Time.time;
                endPos = touch.position;

                swipeDistance = (endPos - startPos).magnitude;
                swipeTime = endTime - startTime;

                if (swipeTime < maxTime && swipeDistance > minSwipeDistance)
                {
                    swipe();
                }
            }
        }
    }

    private void swipe()
    {
        Vector2 distance = endPos - startPos;
        if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            //Debug.Log("swipe ngang");
            if (distance.x > 0)
            {
                Debug.Log("right swipe");
                //Destroy(itemBeingDragged);
                //itemBeingDragged.SetActive(false);
            }
            if (distance.x < 0)
            {
                Debug.Log("left swipe");
            }
        }
        if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
        {
            // Debug.Log("swipe doc");
            if (distance.y > 0)
            {
                Debug.Log("up swipe");
               
                x = scroll.verticalNormalizedPosition;
                InvokeRepeating("scrollUp", 0.001f, 0.001f);
            }
            if (distance.y < 0)
            {
                Debug.Log("down swipe");
                x = scroll.verticalNormalizedPosition;
                InvokeRepeating("scrollDown", 0.005f, 0.005f);
            }
        }
    }

    void scrollUp()
    {
        if (x > 0)
        {
            x -= 0.05f;
        }
        else
        {
            CancelInvoke();
        }

        if (scroll.verticalNormalizedPosition > 0)
        {
            scroll.verticalNormalizedPosition = x;
        }
    }

    void scrollDown()
    {
        if (x < 1)
        {
            x += 0.05f;
        }
        else
        {
            CancelInvoke();
        }
        if (scroll.verticalNormalizedPosition < 1)
        {
            scroll.verticalNormalizedPosition = x;
        }
    }
}
