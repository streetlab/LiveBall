using UnityEngine;
using System.Collections;

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

	static int sequenceQuiz = 0;
	public static int SequenceQuiz {
		get {	return sequenceQuiz;}
		set {	sequenceQuiz = value;}
	}

	static SposDetailBoard detailBoard;
	public static SposDetailBoard DetailBoard{
		get{return detailBoard;}
		set{detailBoard = value;}
	}

	bool mHasQuiz;
	GetQuizEvent mEventQuiz;
	GetGameSposDetailBoardEvent mBoardEvent;

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
		AndroidMgr.SetMainTop(this);
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
		mHighlight.SetActive (false);
		mLineup.SetActive (false);
		mBingo.SetActive (false);
		mLivetalk.SetActive (true);
		mBetting.SetActive (false);
		mState = STATE.Livetalk;
	}

	public void OpenBetting(QuizInfo quizInfo)
	{
		mHighlight.SetActive (false);
		mLineup.SetActive (false);
		mBingo.SetActive (false);
		mLivetalk.SetActive (false);
		mBetting.SetActive (true);
		mBetting.GetComponent<ScriptTF_Betting> ().Init (quizInfo);

		UserMgr.QuizInfo = quizInfo;
		UtilMgr.SetBackEvent(new EventDelegate(this, "GoPreState"));
	}

	public void RequestBoardInfo(bool hasQuiz)
	{
		if (hasQuiz)
				mHasQuiz = true;

		mBoardEvent = new  GetGameSposDetailBoardEvent(new EventDelegate (this, "GotBoard"));
		NetMgr.GetGameSposPlayBoard(mBoardEvent);
	}

	public void RequestQuiz()
	{
		mEventQuiz = new GetQuizEvent (new EventDelegate (this, "GotQuiz"));
		NetMgr.GetProgressQuiz (SequenceQuiz, mEventQuiz);
	}

	public void GotBoard()
	{
		Debug.Log("GotBoard");
		DetailBoard.play = mBoardEvent.Response.data.play;
		DetailBoard.player = mBoardEvent.Response.data.player;
		SetBoardInfo ();

		if(mHasQuiz){
			RequestQuiz();
		}
	}

	public void GotQuiz()
	{
		Debug.Log("GotQuiz");
		OpenBetting (mEventQuiz.Response.data.quiz[0]);
	}

	void SetBoardInfo()
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
