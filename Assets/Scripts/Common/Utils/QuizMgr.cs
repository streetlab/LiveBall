using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuizMgr : MonoBehaviour {

	ScriptMainTop mMainTop;

	int sequenceQuiz;
	public static int SequenceQuiz {
		get {	return Instance.sequenceQuiz;}
		set {	Instance.sequenceQuiz = value;}
	}

	bool isBettingOpended;
	public static bool IsBettingOpended{
		get{return Instance.isBettingOpended;}
		set{Instance.isBettingOpended = value;}
	}
	bool needsDetailInfo;
	public static bool NeedsDetailInfo{
		get{return Instance.needsDetailInfo;}
		set{Instance.needsDetailInfo = value;}
	}
	bool hasQuiz;
	public static bool HasQuiz{
		get{return Instance.hasQuiz;}
		set{Instance.hasQuiz = value;}
	}
	bool moreQuiz;
	public static bool MoreQuiz{
		get{return Instance.moreQuiz;}
		set{Instance.moreQuiz = value;}
	}
	int joinCount;
	public static int JoinCount{
		get{return Instance.joinCount;}
		set{Instance.joinCount = value;}
	}

	List<QuizInfo> quizList;
	public static List<QuizInfo> QuizList{
		get{return Instance.quizList;}
		set{Instance.quizList = value;}
	}
	QuizInfo quizInfo;
	public static QuizInfo QuizInfo
	{
		get{return Instance.quizInfo;}
		set{Instance.quizInfo = value;}
	}

	static QuizMgr _instance;

	static QuizMgr Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(QuizMgr)) as QuizMgr;
				Debug.Log("QuizMgr is null");
				if (_instance == null)
				{
					GameObject container = new GameObject();  
					container.name = "QuizMgr";  
					_instance = container.AddComponent(typeof(QuizMgr)) as QuizMgr;
					Debug.Log("and makes new one");
					
				}
			}
			
			return _instance;
		}
	}

	void Awake()
	{
		DontDestroyOnLoad (this);
	}

	public static void EnterMain(ScriptMainTop script)
	{
		Instance.mMainTop = script;
	}

	public static void LeaveMain()
	{
		Instance.mMainTop = null;
	}

	public static void InitBetting()
	{
		Instance.joinCount = 0;
	}

	public static void SetQuizList(List<QuizInfo> quizList)
	{
		QuizMgr.QuizList = quizList;
	}

	public static void AddQuizList(QuizInfo quiz)
	{
		QuizMgr.QuizList.Insert(0, quiz);
	}

	public static void InitSimpleResult(GetSimpleResultEvent simpleEvent
	                                    , ScriptBetting scriptBetting, ScriptQuizResult scriptQuizResult){

		if (simpleEvent.Response.data == null
		    || simpleEvent.Response.data.Count < 1)
			return;
		
		QuizInfo quiz = null;
		foreach (QuizInfo quizInfo in QuizMgr.QuizList) {
			if(quizInfo.quizListSeq == simpleEvent.Response.data [0].quizListSeq){
				quiz = quizInfo;
				break;
			}
		}
		if (quiz == null)
			return;

		Debug.Log ("InitSimpleResult2");
		quiz.quizValue = simpleEvent.Response.data [0].quizValue;
		
		quiz.resp = new List<QuizRespInfo> ();
		QuizRespInfo tmpInfo;
		if (simpleEvent.Response.data.Count > 1) {
			//got 2 answers
			tmpInfo = new QuizRespInfo();
			tmpInfo.respValue = simpleEvent.Response.data[1].respValue;
			tmpInfo.expectRewardPoint = int.Parse(simpleEvent.Response.data[1].rewardPoint);
			quiz.resp.Add(tmpInfo);
		} 
		
		tmpInfo = new QuizRespInfo();
		tmpInfo.respValue = simpleEvent.Response.data[0].respValue;
		tmpInfo.expectRewardPoint = int.Parse(simpleEvent.Response.data[0].rewardPoint);
		quiz.resp.Insert(0, tmpInfo);
		
		if (ShowQuizResult (quiz, simpleEvent, scriptQuizResult)) {
			scriptQuizResult.InitParticle();
		}
		
		scriptBetting.UpdateHitterItem(quiz);
	}

	static bool ShowQuizResult(QuizInfo quiz, GetSimpleResultEvent simpleEvent, ScriptQuizResult scriptQuizResult)
	{
		scriptQuizResult.GetComponent<PlayMakerFSM>().SendEvent("OpenResultEvent");
		return scriptQuizResult.GetComponent<ScriptQuizResult> ().Init (simpleEvent.Response.data);
	}

	public static void NotiReceived(string msg)
	{
		Debug.Log ("ReceivedMsg : " + msg);
		NotiMsgInfo msgInfo = JsonFx.Json.JsonReader.Deserialize<NotiMsgInfo> (msg);
		Debug.Log ("push type : " + msgInfo.type);
//		Debug.Log ("msgInfo.info.gameSeq : " + msgInfo.info.gameSeq);
//		Debug.Log ("msgInfo.info.scheduleSeq : " + msgInfo.info.scheduleSeq);
//		Debug.Log ("msgInfo.info.quizListSeq : " + msgInfo.info.quizListSeq);
		
		if(msgInfo.type.Equals(Constants.POST_MSG)){
			
		} else if(msgInfo.type.Equals(Constants.POST_GAME_START)){
			//refresh schedule list
		} else if(msgInfo.type.Equals(Constants.POST_GAME_STATUS)){
			if(Instance.mMainTop != null){
				bool hasQuiz = false;
				if(msgInfo.info.quiz != null
				   && msgInfo.info.quiz.Equals("1")){
					if(QuizMgr.IsBettingOpended)
						MoreQuiz = true;
					else
						HasQuiz = true;
				}

				if(msgInfo.info.inning != null
				   && msgInfo.info.inning.Equals("1")){
					NeedsDetailInfo = true;
				} else if(msgInfo.info.score != null
				          && msgInfo.info.score.Equals("1")){
					NeedsDetailInfo = true;
				} else{
					NeedsDetailInfo = false;
				}
				
				Instance.mMainTop.RequestBoardInfo();
			}
		} else if(msgInfo.type.Equals(Constants.POST_QUIZ_RESULT)){
			if(Instance.mMainTop != null){
				Instance.mMainTop.GetComponent<ScriptMainTop>().GetSimpleResult(int.Parse(msgInfo.info.quizListSeq));

//				Instance.mMainTop.mBetting.transform.FindChild("SprBetting")
//					.GetComponent<ScriptBetting>().UpdateHitterItem(int.Parse(msgInfo.info.quizListSeq));
			}
		}
	}
}
