using UnityEngine;
using System.Collections;
using GameDelegates;

namespace GameDelegates {
	public delegate void ErrorReponse(string errorMsg);
}

public class ErrorHandler{
	public event ErrorReponse errorDelegate;
	public void responseError (BaseObject rs){
		string errorMsg = rs.Param.GetUtfString("msg");
		if(errorDelegate != null){
			errorDelegate(errorMsg);
//			errorDelegate = null;
		}
	}
}
