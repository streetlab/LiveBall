using UnityEngine;
using System.Collections;
//using System.Net;
//using System.Net.Sockets;
using System.IO;

public class ScriptMainTop : MonoBehaviour {

	public GameObject mHighlight;
	public GameObject mLineup;
	public GameObject mBingo;
	public GameObject mLivetalk;
	public GameObject mBetting;

	public GameObject mBtnHighlight;
	public GameObject mBtnLineup;
	public GameObject mBtnBingo;
	public GameObject mBtnLivetalk;

	public GameObject mLblGold;
	public GameObject mLblRuby;
	public GameObject mLblDia;

	GetQuizEvent mEventQuiz;
	GetGameSposDetailBoardEvent mBoardEvent;

	static SposDetailBoard detailBoard;
	public static SposDetailBoard DetailBoard{
		get{return detailBoard;}
		set{detailBoard = value;}
	}

	enum STATE{
		Highlight,
		Lineup,
		Bingo,
		Livetalk,
		Betting
	};

	STATE mState = STATE.Highlight;

	void Start () {
		mHighlight.SetActive (true);
		mLineup.SetActive (false);
		mBingo.SetActive (false);
		mLivetalk.SetActive (false);
		mBetting.SetActive (false);

		#if(UNITY_ANDROID)
		QuizMgr.EnterMain(this);
		#else
		#endif

		SetTopInfo ();
	}

	void SetTopInfo()
	{
		mLblDia.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userDiamond);
		mLblGold.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userGoldenBall);
		mLblRuby.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userRuby);
	}

	public void GoPreState()
	{
		QuizMgr.IsBettingOpended = false;

		if (QuizMgr.MoreQuiz) {
			QuizMgr.MoreQuiz = false;
			RequestQuiz();
		}

		Debug.Log ("GoPreState");
		switch(mState)
		{
		case STATE.Highlight:
			OpenHighlight();
			break;
		case STATE.Lineup:
			OpenLineup();
			break;
		case STATE.Livetalk:
			OpenLivetalk();
			break;
		case STATE.Bingo:
			OpenBingo();
			break;
		}
	}

	void OpenHighlight()
	{
		Debug.Log("OpenHighlight");
		mHighlight.SetActive (true);
		mLineup.SetActive (false);
		mBingo.SetActive (false);
		mLivetalk.SetActive (false);
		mBetting.SetActive (false);
		mState = STATE.Highlight;
		SetBoardInfo ();
	}

	void OpenLineup()
	{
		mHighlight.SetActive (false);
		mLineup.SetActive (true);
		mBingo.SetActive (false);
		mLivetalk.SetActive (false);
		mBetting.SetActive (false);
		mState = STATE.Lineup;
	}

	void OpenBingo()
	{
		mHighlight.SetActive (false);
		mLineup.SetActive (false);
		mBingo.SetActive (true);
		mLivetalk.SetActive (false);
		mBetting.SetActive (false);
		mState = STATE.Bingo;
	}

	void OpenLivetalk()
	{
//		mHighlight.SetActive (false);
//		mLineup.SetActive (false);
//		mBingo.SetActive (false);
//		mLivetalk.SetActive (true);
//		mBetting.SetActive (false);
//		mState = STATE.Livetalk;

//		TcpClient client = new TcpClient ("", 21);
//
//		NetworkStream stream = client.GetStream();
//		StreamWriter sw = new StreamWriter (stream);
//		StreamReader sr = new StreamReader (stream);
//
//		sw.Write ("");
//		sw.Flush ();

	}

	public void OpenBetting(QuizInfo quizInfo)
	{
		QuizMgr.QuizInfo = quizInfo;
		QuizMgr.IsBettingOpended = true;
		QuizMgr.JoinCount = 0;

		UtilMgr.SetBackEvent(new EventDelegate(this, "GoPreState"));

		mHighlight.SetActive (false);
		mLineup.SetActive (false);
		mBingo.SetActive (false);
		mLivetalk.SetActive (false);

		mBetting.SetActive (true);
		mBetting.GetComponent<ScriptTF_Betting> ().Init (quizInfo);
	}

	public void RequestBoardInfo(bool hasQuiz)
	{
		if (hasQuiz)
			QuizMgr.HasQuiz = true;

		mBoardEvent = new  GetGameSposDetailBoardEvent(new EventDelegate (this, "GotBoard"));
		NetMgr.GetGameSposPlayBoard(mBoardEvent);
	}

	public void GotBoard()
	{
		Debug.Log("GotBoard");
		DetailBoard.play = mBoardEvent.Response.data.play;
		DetailBoard.player = mBoardEvent.Response.data.player;
		SetBoardInfo ();
		
		if(QuizMgr.HasQuiz){
			QuizMgr.HasQuiz = false;
			RequestQuiz();
		}
	}

	public void RequestQuiz()
	{
		mEventQuiz = new GetQuizEvent (new EventDelegate (this, "GotQuiz"));
		Debug.Log ("QuizMgr.SequenceQuiz : " + QuizMgr.SequenceQuiz);
		NetMgr.GetProgressQuiz (QuizMgr.SequenceQuiz, mEventQuiz);
	}

	public void GotQuiz()
	{
		Debug.Log("GotQuiz");
		if (mEventQuiz.Response.data.quiz == null
						|| mEventQuiz.Response.data.quiz.Count < 1) {
			Debug.Log("NoQuiz");
			return;
		}

		if (mEventQuiz.Response.data.quiz.Count > 1) {
			QuizMgr.MoreQuiz = true;
		}

		AddQuizIntoList ();

		if(!QuizMgr.IsBettingOpended)
			OpenBetting (mEventQuiz.Response.data.quiz[mEventQuiz.Response.data.quiz.Count-1]);


	}

	void AddQuizIntoList()
	{
		mHighlight.transform.FindChild ("MatchPlaying").GetComponent<ScriptMatchPlaying> ().AddQuizList (mEventQuiz);
//		mHighlight.transform.FindChild ("MatchPlaying").GetComponent<ScriptMatchPlaying> ().InitQuizList (mEventQuiz);
	}

	public void SetBoardInfo()
	{
		mHighlight.transform.FindChild ("MatchInfoTop").GetComponent<ScriptMatchInfo> ().SetBoard ();
	}

	void OnBackPressed()
	{
		UtilMgr.RemoveBackEvent ();
		GoPreState ();
	}

	public void BtnClicked(string name)
	{
		switch(name)
		{
		case "Highlight":
			OpenHighlight();
			break;
		case "Lineup":
			OpenLineup();
			break;
		case "Statistic":
			OpenBingo();
			break;
		case "Livetalk":
			OpenLivetalk();
			break;
		}
	}
}
