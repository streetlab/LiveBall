﻿using UnityEngine;
using System.Collections;



public class NetMgr : MonoBehaviour{

	private static NetMgr _instance = null;
	public static NetMgr Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(NetMgr)) as NetMgr;
				if(!_instance)
				{
					GameObject container = new GameObject();
					container.name = "NetMgrContainer";
					_instance = container.AddComponent(typeof(NetMgr)) as NetMgr;
				}
			}
			return _instance;
		}
	}

	IEnumerator webAPIProcess(WWW www, BaseEvent baseEvent)
	{
		yield return www;
		
		if(www.error == null)
		{
			Debug.Log(www.text);
//			CommonDialogue.Show (www.text);
			baseEvent.Init(www.text);
		}
		else
		{
			Debug.Log(www.error);
		}
	}

	private void webAPIProcessEvent(BaseRequest request, BaseEvent baseEvent)
	{
		string reqParam = "";
		string httpUrl = "";
		if (request != null) {
			reqParam = request.ToRequestString();
//			httpUrl = (Constants.QUERY_SERVER_HOST + reqParam);
//			httpUrl = reqParam;
		} else {
//			httpUrl = Constants.QUERY_SERVER_HOST;
		}

		WWW www = new WWW (Constants.QUERY_SERVER_HOST , System.Text.Encoding.UTF8.GetBytes(reqParam));

		Debug.Log (reqParam);
		StartCoroutine (webAPIProcess(www, baseEvent));
	}

	public static void DoLogin(LoginInfo loginInfo, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new LoginRequest(loginInfo), baseEvent);
	}

	public static void GetScheduleMore(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetScheduleMoreRequest(), baseEvent);
	}

	public static void GetGameSposDetailBoard(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetGameSposDetailBoardRequest (), baseEvent);
	}

	public static void GetGameSposPlayBoard(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetGameSposPlayBoardRequest (), baseEvent);
	}

	public static void GetCardInven(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetCardInvenRequest (), baseEvent);
	}

	public static void GetPreparedQuiz(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetPreparedQuizRequest (), baseEvent);
	}

	public static void GetProgressQuiz(int quizListSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetProgressQuizRequest (quizListSeq), baseEvent);
	}

	public static void GetProfile(int memSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetProfileRequest (memSeq), baseEvent);
	}
}