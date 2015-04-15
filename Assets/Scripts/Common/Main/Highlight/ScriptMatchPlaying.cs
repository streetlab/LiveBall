using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptMatchPlaying : MonoBehaviour {
	public GameObject mScoreBoard;
	public GameObject mList;
	public GameObject itemHitter;
	public GameObject itemRound;
	public GameObject itemPoll;
	public GameObject itemInfo;
	public GameObject mTop;

	float mPreItemSize;
	float mPosGuide;
	float mAccumulatedY;
//	int mSequenceQuiz;
	bool mPreGame;
	bool mFirstLoading;
	int mGameRoundCounter;
	int mGameRound;
	int mInningType;
	int mInningCounter;

	GetGameSposDetailBoardEvent mEventDetail;
//	GetQuizEvent mEventPreQuiz;
	GetQuizEvent mEventProgQuiz;

	public List<GameObject> mQuizListItems = new List<GameObject>();

	void Start () {
		UtilMgr.ResizeList (mList);
		mFirstLoading = true;
//		mPosGuide = 0f;
		mPreGame = true;
		mGameRoundCounter = 20;
		mInningCounter = 0;
		JoinGame ();

	}

	void JoinGame()
	{
		NetMgr.JoinGame (new JoinGameEvent (new EventDelegate (this, "CompleteJoin")));
	}

	public void CompleteJoin()
	{
		Debug.Log("CompleteJoin");
		SetScoreBoard ();
	}

	void SetScoreBoard()
	{
		mScoreBoard.transform.FindChild ("Const").gameObject.SetActive (false);
		mScoreBoard.transform.FindChild ("TeamTop").gameObject.SetActive (false);
		mScoreBoard.transform.FindChild ("TeamBottom").gameObject.SetActive (false);
		//Progressing
		mEventDetail = new GetGameSposDetailBoardEvent (new EventDelegate (this, "GotDetailBoard"));
		NetMgr.GetGameSposDetailBoard (mEventDetail);
	}
	
	public void GotDetailBoard()
	{
		InitScoreBoard (mEventDetail);
		
		if (mFirstLoading) {
			SetProgQuiz (0);
//			UtilMgr.ShowLoading (true);
		}

		mTop.GetComponent<ScriptMainTop> ().SetBoardInfo ();
	}

	public void InitScoreBoard(GetGameSposDetailBoardEvent eventDetail)
	{
		mScoreBoard.transform.FindChild ("Const").gameObject.SetActive (true);
		mScoreBoard.transform.FindChild ("TeamTop").gameObject.SetActive (true);
		mScoreBoard.transform.FindChild ("TeamBottom").gameObject.SetActive (true);
		
		ScriptMainTop.DetailBoard = eventDetail.Response.data;
		
		SetAwayScore (ScriptMainTop.DetailBoard.awayScore);
		SetHomeScore (ScriptMainTop.DetailBoard.homeScore);
		SetAwayRHEB (ScriptMainTop.DetailBoard.infoBoard[0]);
		SetHomeRHEB (ScriptMainTop.DetailBoard.infoBoard[1]);
	}

	void SetProgQuiz(int quizListSeq)
	{
		mEventProgQuiz = new GetQuizEvent (new EventDelegate (this, "InitQuizFirst"));
		NetMgr.GetProgressQuiz (quizListSeq, mEventProgQuiz);
	}

	void AddQuizIntoList(QuizInfo quizInfo)
	{
		GameObject obj = null;

		if(quizInfo.typeCode.Contains("_QZA_")
		   && mPreGame){
			mPreGame = false;
			
			obj = Instantiate(itemRound, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
			mAccumulatedY += obj.GetComponent<BoxCollider2D> ().size.y;
			mQuizListItems.Add(obj);

			obj.transform.parent = mList.transform;//.FindChild("Grid");
			obj.transform.localScale = new Vector3(1f, 1f, 1f);
			obj.transform.FindChild("LblHead").gameObject.SetActive(false);
			obj.transform.FindChild("LblTail").gameObject.SetActive(false);
			obj.transform.FindChild("LblRound").gameObject.SetActive(false);
			obj.transform.FindChild("LblPrepared").gameObject.SetActive(true);

			mPosGuide += (obj.GetComponent<BoxCollider2D> ().size.y - mPreItemSize) / 2f;
			obj.transform.localPosition = new Vector3(0f, -mPosGuide, 0f);
			mPosGuide += obj.GetComponent<BoxCollider2D> ().size.y-1;
			mPreItemSize = obj.GetComponent<BoxCollider2D> ().size.y-1;
//			mPosGuide += (122 - 30f) / 2f;
		} else if(quizInfo.typeCode.Contains("_QZD_") && mFirstLoading){
			if(mGameRoundCounter > quizInfo.gameRound){
				if(mGameRoundCounter < 20){
//					mPosGuide -= (122 - 30f) / 2f;
				} else{
					mGameRound = quizInfo.gameRound;
					mInningType = quizInfo.inningType;
				}
				
				mGameRoundCounter = quizInfo.gameRound;
				mInningCounter = quizInfo.inningType;
				
				obj = Instantiate(itemRound, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
				mQuizListItems.Add(obj);
				mAccumulatedY += obj.GetComponent<BoxCollider2D> ().size.y;
				obj.transform.parent = mList.transform;//.FindChild("Grid");
				obj.transform.localScale = new Vector3(1f, 1f, 1f);
				if(mInningCounter == 0)
					obj.transform.FindChild("LblTail").gameObject.SetActive(false);
				else
					obj.transform.FindChild("LblHead").gameObject.SetActive(false);
				
//				Debug.Log(i+", mPosGuide : "+mPosGuide);
				obj.transform.FindChild("LblRound").GetComponent<UILabel>().text = mGameRoundCounter + "";

				mPosGuide += (obj.GetComponent<BoxCollider2D> ().size.y - mPreItemSize) / 2f;
				obj.transform.localPosition = new Vector3(0f, -mPosGuide, 0f);
				mPosGuide += obj.GetComponent<BoxCollider2D> ().size.y-1;
				mPreItemSize = obj.GetComponent<BoxCollider2D>().size.y-1;
//				mPosGuide += (122 - 30f) / 2f;
			} else if(mInningCounter != quizInfo.inningType){
				mInningCounter = quizInfo.inningType;
//				mPosGuide -= (122 - 30f) / 2f;
				
				obj = Instantiate(itemRound, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
				mQuizListItems.Add(obj);
				mAccumulatedY += obj.GetComponent<BoxCollider2D> ().size.y;
				obj.transform.parent = mList.transform;//.FindChild("Grid");
				obj.transform.localScale = new Vector3(1f, 1f, 1f);
				if(mInningCounter == 0)
					obj.transform.FindChild("LblTail").gameObject.SetActive(false);
				else
					obj.transform.FindChild("LblHead").gameObject.SetActive(false);

				obj.transform.FindChild("LblRound").GetComponent<UILabel>().text = mGameRoundCounter + "";

				mPosGuide += (obj.GetComponent<BoxCollider2D> ().size.y - mPreItemSize) / 2f;
				obj.transform.localPosition = new Vector3(0f, -mPosGuide, 0f);
				mPosGuide += obj.GetComponent<BoxCollider2D> ().size.y-1;
				mPreItemSize = obj.GetComponent<BoxCollider2D>().size.y-1;
//				mPosGuide += (122 - 30f) / 2f;

			}
		}

		if (quizInfo.typeCode.Contains ("_QZA_")) {
			obj = Instantiate (itemPoll, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;

			mQuizListItems.Add (obj);
			mAccumulatedY += obj.GetComponent<BoxCollider2D> ().size.y;

			obj.GetComponent<ScriptItemPollHighlight> ().Init (quizInfo);
			obj.transform.parent = mList.transform;
			obj.transform.localScale = new Vector3 (1f, 1f, 1f);

			mPosGuide += (obj.GetComponent<BoxCollider2D> ().size.y - mPreItemSize) / 2f;
			obj.transform.localPosition = new Vector3 (0f, -mPosGuide, 0f);
			mPosGuide += obj.GetComponent<BoxCollider2D> ().size.y;
			mPreItemSize = obj.GetComponent<BoxCollider2D> ().size.y;
		} else {
			obj = Instantiate (itemHitter, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
			if (!mFirstLoading) {
					RepositionItems (obj.GetComponent<BoxCollider2D> ().size.y);
					mQuizListItems.Insert (0, obj);
			} else
					mQuizListItems.Add (obj);

			obj.GetComponent<ScriptItemHitterHighlight> ().mPositionY = mAccumulatedY;
			mAccumulatedY += obj.GetComponent<BoxCollider2D> ().size.y;

			obj.transform.parent = mList.transform;//.FindChild("Grid");
			obj.transform.localScale = new Vector3 (1f, 1f, 1f);		
			obj.GetComponent<ScriptItemHitterHighlight> ().Init (quizInfo,
                                                     transform.FindChild ("ItemDetail").gameObject);

			mPosGuide += (obj.GetComponent<BoxCollider2D> ().size.y - mPreItemSize) / 2f;
			obj.transform.localPosition = new Vector3 (0f, -mPosGuide, 0f);
			mPosGuide += obj.GetComponent<BoxCollider2D> ().size.y;
			mPreItemSize = obj.GetComponent<BoxCollider2D> ().size.y;
		}

		if(QuizMgr.SequenceQuiz < quizInfo.quizListSeq)
			QuizMgr.SequenceQuiz = quizInfo.quizListSeq;


	}

	public void InitQuizFirst()
	{
		UtilMgr.DismissLoading ();
		InitQuizList (null);
	}

	public void InitQuizList(GetQuizEvent quizEvent)
	{
		QuizMgr.SetQuizList (mEventProgQuiz.Response.data.quiz);

		foreach (GameObject go in mQuizListItems) {
			go.transform.parent = null;
			NGUITools.DestroyImmediate(go);		
		}
		mQuizListItems.Clear ();
		mFirstLoading = true;
		mAccumulatedY = 0f;
		mPosGuide = 0f;
		mPreItemSize = 30f;

		for(int i = 0; i < mEventProgQuiz.Response.data.quiz.Count; i++)
		{
			QuizInfo quizInfo = mEventProgQuiz.Response.data.quiz[i];
			AddQuizIntoList(quizInfo);
		}
		
		mList.GetComponent<UIScrollView> ().ResetPosition ();
		mFirstLoading = false;
	}

	public void AddQuizList(QuizInfo quizInfo)
	{


		mAccumulatedY = 0f;
//		mPosGuide = 0f;
		mPosGuide = (122 - 30f) / 2f;
		mPreItemSize = 122f;

		QuizMgr.AddQuizList (quizInfo);
//		QuizInfo quizInfo = quizEvent.Response.data.quiz[quizEvent.Response.data.quiz.Count-1];
		if(quizInfo.gameRound == mGameRound
		   && quizInfo.inningType == mInningType){
			GameObject go = mQuizListItems [0];
			RepositionItems(-go.GetComponent<BoxCollider2D> ().size.y);
			mQuizListItems.RemoveAt (0);
			NGUITools.Destroy (go);
		}
//		Debug.Log ("quizInfo.gameRound : " + quizInfo.gameRound + ", mGameRound : " + mGameRound);
//		Debug.Log ("quizInfo.inningType : " + quizInfo.inningType + ", mInningType : " + mInningType);

		AddQuizIntoList(quizInfo);

		mGameRound = quizInfo.gameRound;
		mInningType = quizInfo.inningType;
		
		GameObject obj = Instantiate(itemRound, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		mQuizListItems.Insert(0, obj);
		RepositionItems(obj.GetComponent<BoxCollider2D> ().size.y);

		obj.transform.parent = mList.transform;//.FindChild("Grid");
		obj.transform.localScale = new Vector3(1f, 1f, 1f);
		if(mInningType == 0)
			obj.transform.FindChild("LblTail").gameObject.SetActive(false);
		else
			obj.transform.FindChild("LblHead").gameObject.SetActive(false);

		obj.transform.FindChild("LblRound").GetComponent<UILabel>().text = mGameRound + "";
		obj.transform.localPosition = new Vector3(0f, 0f, 0f);

		mList.GetComponent<UIScrollView> ().ResetPosition ();
	}

//	void RefreshQuizListDatas()
//	{
//		for(int i = 0; i < QuizMgr.QuizList.Count; i++)
//		{
//			QuizInfo quizInfo = QuizMgr.QuizList[i];
//		}
//	}

	void RepositionItems(float size)
	{
		foreach(GameObject tmp in mQuizListItems){
			Vector3 vector = tmp.transform.localPosition;
			vector.y -= size;
			tmp.transform.localPosition = vector;
			if(tmp.GetComponent<ScriptItemHitterHighlight>() != null)		
				tmp.GetComponent<ScriptItemHitterHighlight>().mPositionY += size;
		}
	}

	void SetAwayScore(List<ScoreInfo> listScore)
	{
		Transform team = mScoreBoard.transform.FindChild ("TeamTop");
		team.FindChild ("LblName").GetComponent<UILabel> ().text = UserMgr.Schedule.extend [0].teamName;

		string strRnd = "LblRnd";
		for(int i = 0; i < listScore.Count; i++)
		{
			ScoreInfo info = listScore[i];
			team.FindChild (strRnd + info.playRound).GetComponent<UILabel> ().text = info.score;
		}
	}

	void SetHomeScore(List<ScoreInfo> listScore)
	{
		Transform team = mScoreBoard.transform.FindChild ("TeamBottom");
		team.FindChild ("LblName").GetComponent<UILabel> ().text = UserMgr.Schedule.extend [1].teamName;
		
		string strRnd = "LblRnd";
		for(int i = 0; i < listScore.Count; i++)
		{
			ScoreInfo info = listScore[i];
			team.FindChild (strRnd + info.playRound).GetComponent<UILabel> ().text = info.score;
		}
	}

	void SetAwayRHEB(HEBInfo info)
	{
		Transform team = mScoreBoard.transform.FindChild ("TeamTop");
		team.FindChild ("LblR").GetComponent<UILabel> ().text = info.score;
		team.FindChild ("LblH").GetComponent<UILabel> ().text = info.countOfH;
		team.FindChild ("LblE").GetComponent<UILabel> ().text = info.countOfE;
		team.FindChild ("LblB").GetComponent<UILabel> ().text = info.countOfB;
	}

	void SetHomeRHEB(HEBInfo info)
	{
		Transform team = mScoreBoard.transform.FindChild ("TeamBottom");
		team.FindChild ("LblR").GetComponent<UILabel> ().text = info.score;
		team.FindChild ("LblH").GetComponent<UILabel> ().text = info.countOfH;
		team.FindChild ("LblE").GetComponent<UILabel> ().text = info.countOfE;
		team.FindChild ("LblB").GetComponent<UILabel> ().text = info.countOfB;
	}
}
