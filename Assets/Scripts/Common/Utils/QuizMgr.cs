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
		Instance.quizList = quizList;
	}

	public static void AddQuizList(QuizInfo quiz)
	{
		Instance.quizList.Insert(0, quiz);
	}

	public static void NotiReceived(string msg)
	{
		Debug.Log ("ReceivedMsg : " + msg);
		NotiMsgInfo msgInfo = JsonFx.Json.JsonReader.Deserialize<NotiMsgInfo> (msg);
		Debug.Log ("push type : " + msgInfo.type);
		
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
				
				Instance.mMainTop.RequestBoardInfo(hasQuiz);
			}
		} else if(msgInfo.type.Equals(Constants.POST_QUIZ_RESULT)){
			
		}
	}
}
