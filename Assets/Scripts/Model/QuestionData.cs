using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionData{

	public string Content{ set; get;}
	public string[] AnswerList{ set; get;}
	public string imageURL { set; get;}

	//Luu sprite cua cac image question
	public List<Sprite> imageSprite { set; get;}

}
