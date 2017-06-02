using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class centerController : MonoBehaviour {

	public ScrollRect childScroll;
	public RectTransform panel;
	public Button[] bttn;
	public RectTransform center;
	public Image img;
	float[] distance;
	bool dragging = false;
	int bttnDistance;
	int minBtnNum;
	ScrollRect scroll;
	float x = 0;
	Color clr = Color.white;
    public string rName;



    void Start(){
		int bttnLength = bttn.Length;
		distance = new float[bttnLength];
		bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
		scroll = GameObject.Find("content").GetComponent<ScrollRect>();
	}

	void Update(){
		for (int i = 0; i < bttn.Length; i++) {
			distance[i] = Mathf.Abs(center.transform.position.x - bttn[i].GetComponent<RectTransform>().anchoredPosition.x);
		}

		float minDistance = Mathf.Min(distance);

		for (int i = 0; i < bttn.Length; i++) {
			if(minDistance == distance[i]){
				minBtnNum = i;
			}
		}

		if(!dragging){
			LerpToBttn(minBtnNum * - bttnDistance);
		}
	}

	void LerpToBttn(int position){
		float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);

		Vector2 newPos = new Vector2(newX, panel.anchoredPosition.y);

		panel.anchoredPosition = newPos;
	}

	public void StartDrag(BaseEventData data){
		
		dragging = true;
		bool flag = false;

		PointerEventData pt = data as PointerEventData;
		//if(pt.pressPosition.y > pt.position.y &&  pt.pressPosition.y - pt.position.y > 20){
		//	x = scroll.verticalNormalizedPosition;
		//	flag = true;
		//	InvokeRepeating("scrollDown",0.005f,0.005f);
		//}else if(pt.pressPosition.y < pt.position.y &&  pt.pressPosition.y - pt.position.y > -20){
		//	x = scroll.verticalNormalizedPosition;
		//	flag = true;
		//	InvokeRepeating("scrollUp",0.001f,0.001f);
		//}



		if(pt.pressPosition.x > pt.position.x &&  pt.pressPosition.x - pt.position.x > 7){
			
		}else if(pt.pressPosition.x < pt.position.x &&  pt.position.x - pt.pressPosition.x > 7){
			
		}

		if(flag){
			//clr.a = 0.5f;
			//img.color = clr;
		}

	}

	public void EndDrag(){
        //clr.a = 1;
        //img.color = clr;
        dragging = false;
		if(childScroll.horizontalNormalizedPosition > 1 || childScroll.horizontalNormalizedPosition < 0.6){
            //Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            if (rName != null)
            {
                BaseService.Instance.playerService.sendAcceptInvite(rName, false);
            }
            Destroy(this.gameObject);
		}
	}


	void scrollUp(){
		if(x > 0){
			x-=0.05f;
		}else{
			CancelInvoke();
		}

		if(scroll.verticalNormalizedPosition > 0){
			scroll.verticalNormalizedPosition = x;
		}
	}

	void scrollDown(){
		if(x < 1){
			x+=0.05f;
		}else{
			CancelInvoke();
		}
		if(scroll.verticalNormalizedPosition < 1){
			scroll.verticalNormalizedPosition = x;
		}
	}

    

}
