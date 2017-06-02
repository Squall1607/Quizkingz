using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public float maxTime;
    public float minSwipeDistance;

    float startTime;
    float endTime;

    Vector3 startPos;
    Vector3 endPos;
    float swipeDistance;
    float swipeTime;

    //public GameObject image;
    public static GameObject itemBeingDragged;
    
    Vector3 startPositon;
    Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Vector2 distance = endPos - startPos;
        //if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
        //{
        //    gameObject.GetComponents<CanvasGroup>(
        //}
        //else
        //{
            itemBeingDragged = gameObject;
            startPositon = transform.position;
            startParent = transform.parent;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        //}
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Vector2 distance = endPos - startPos;
        //if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
        //{
        //    transform.position = startPositon;
        //}
        //else
        //{
            transform.position = Input.mousePosition;
        //}
        

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //itemBeingDragged = null;
        transform.position = startPositon;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent)
        {
            transform.position = startPositon;
        }

    }

    // Update is called once per frame
    void Update()
    {
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
                Destroy(itemBeingDragged);
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
            }
            if (distance.y < 0)
            {
                Debug.Log("down swipe");
            }
        }
    }
}
