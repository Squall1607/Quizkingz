using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using GameDelegates;

namespace GameDelegates {
	public delegate void SendQuizResponse(QuestionData q);
	public delegate void SendLstQuizResponse(List<QuestionData> list);
	public delegate void SendAnswerResponse(bool rs, int bonus, int score, int currQuiz);
}

public class QuestionService {

	public event SendQuizResponse SendQuizDelegate;
	public event SendLstQuizResponse SendLstQuizDelegate;
	public event SendAnswerResponse AnswerDelegate;
	public event ServerResponseDelegate TimeOutDelegate;


	public void reponsePackQuestion(BaseObject result){
		GameData.questionPack = result.Param.GetIntArray("set");
	}

	public void responseSendQuiz(BaseObject result){
		
		QuestionData q = new QuestionData();
		q.Content = result.Param.GetUtfString("question");
		q.AnswerList = result.Param.GetUtfStringArray("answer");
		GameData.currentQuestion = result.Param.GetInt("currQuiz");
		GameData.time  = result.Param.GetInt("timer");
        GameData.question = q;
        //Debug.Log(QuestionData);

        if (SendQuizDelegate != null){
			SendQuizDelegate(q);
			SendQuizDelegate = null;
		}
	}

	public void responseSendLstQuiz(BaseObject result){
		List<QuestionData> list = new List<QuestionData> ();
		ISFSArray arr = result.Param.GetSFSArray ("lQ");
		for (int i = 0; i < arr.Size(); i++) {
			QuestionData q = new QuestionData ();
			q.Content = arr.GetSFSObject (i).GetUtfString ("question");
			q.imageURL = arr.GetSFSObject (i).GetUtfString ("image");
			q.AnswerList = arr.GetSFSObject (i).GetUtfStringArray ("answer");
			list.Add (q);
		}

		if (SendLstQuizDelegate != null) {
			SendLstQuizDelegate (list);
			SendLstQuizDelegate = null;
		}
	}

	public void sendAnswer(string answer){
		ISFSObject param = new SFSObject();
		param.PutUtfString("answer",answer);
		BaseService.Instance.smartFox.Send (new ExtensionRequest(CommandList.ANSWER,param));
	}

	public void responseAnswer(BaseObject result){
		int score = result.Param.GetInt ("score"); //score: diem tra loi dung
		int bonusScore = result.Param.GetInt ("bonus_score"); 
		if (!result.Param.GetBool ("status")) {
			GameData.playerData.Score = score + bonusScore;
		} 
		GameData.playerData.BonusPoint = bonusScore;
		GameData.playerData.CorrectPoint = score;
		if(AnswerDelegate != null){
			AnswerDelegate(result.Param.GetBool("status"), bonusScore ,
					 score , result.Param.GetInt("currQuiz")
			);
			AnswerDelegate = null;
		};
	}

	public void responseTimeOut(){
		if(TimeOutDelegate != null){
			TimeOutDelegate();
			TimeOutDelegate = null;
		}
	}

}
