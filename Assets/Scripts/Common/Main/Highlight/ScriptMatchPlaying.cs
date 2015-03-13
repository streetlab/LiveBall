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

	float mPosGuide;
//	int mSequenceQuiz;
	bool mFirstLoading;

	GetGameSposDetailBoardEvent mEventDetail;
	GetQuizEvent mEventPreQuiz;
	GetQuizEvent mEventProgQuiz;

	void Start () {
		UtilMgr.ResizeList (mList);
		mFirstLoading = true;
		mPosGuide = 0f;
		JoinGame ();
	}

	void Update(){
//		Debug.Log ("panel y : " + mList.GetComponent<UIPanel> ().transform.localPosition.y);
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

	void SetPreQuiz()
	{
//		for(int i = 0; i < mEventPreQuiz.GetResponse().data.quiz.Count; i++)
//		{
//			GameObject obj = Instantiate(itemPoll, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
//			obj.transform.parent = mList.transform;
//			obj.transform.localScale = new Vector3(1f, 1f, 1f);		
//			//			obj.GetComponent<ScriptItemInfoHighlight> ().Init ();
//			obj.transform.localPosition = new Vector3(0f, -mPosGuide, 0f);
//			mPosGuide += obj.GetComponent<BoxCollider2D> ().size.y;
//			
//		}
	}

//	public int GetSeqQuiz()
//	{
//		return mSequenceQuiz;
//	}

	void SetProgQuiz(int quizListSeq)
	{
		mEventProgQuiz = new GetQuizEvent (new EventDelegate (this, "GotProgQuiz"));
		NetMgr.GetProgressQuiz (quizListSeq, mEventProgQuiz);
	}

	public void GotProgQuiz()
	{
		int gameRound = 20;
		int inningType = 0;
		for(int i = 0; i < mEventProgQuiz.Response.data.quiz.Count; i++)
		{
			QuizInfo quizInfo = mEventProgQuiz.Response.data.quiz[i];

			if(quizInfo.typeCode.Contains("_QZA_")
			   && gameRound == 1){
				mPosGuide -= (122 - 30f) / 2f;
				gameRound = 20;

				GameObject obj = Instantiate(itemRound, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
				obj.transform.parent = mList.transform;
				obj.transform.localScale = new Vector3(1f, 1f, 1f);
				obj.transform.FindChild("LblHead").gameObject.SetActive(false);
				obj.transform.FindChild("LblTail").gameObject.SetActive(false);
				obj.transform.FindChild("LblRound").gameObject.SetActive(false);
				obj.transform.FindChild("LblPrepared").gameObject.SetActive(true);
				obj.transform.localPosition = new Vector3(0f, -mPosGuide, 0f);
				mPosGuide += obj.GetComponent<BoxCollider2D> ().size.y;
				mPosGuide += (122 - 30f) / 2f;
			} else if(quizInfo.typeCode.Contains("_QZD_")){
				if(gameRound > quizInfo.gameRound){
					if(gameRound < 20)
						mPosGuide -= (122 - 30f) / 2f;

					gameRound = quizInfo.gameRound;
					inningType = quizInfo.inningType;

					GameObject obj = Instantiate(itemRound, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
					obj.transform.parent = mList.transform;
					obj.transform.localScale = new Vector3(1f, 1f, 1f);
					if(inningType == 0)
						obj.transform.FindChild("LblTail").gameObject.SetActive(false);
					else
						obj.transform.FindChild("LblHead").gameObject.SetActive(false);
					
					obj.transform.FindChild("LblRound").GetComponent<UILabel>().text = gameRound + "";
					obj.transform.localPosition = new Vector3(0f, -mPosGuide, 0f);
					mPosGuide += obj.GetComponent<BoxCollider2D> ().size.y;
					mPosGuide += (122 - 30f) / 2f;
				} else if(inningType != quizInfo.inningType){
					inningType = quizInfo.inningType;
					mPosGuide -= (122 - 30f) / 2f;

					GameObject obj = Instantiate(itemRound, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
					obj.transform.parent = mList.transform;
					obj.transform.localScale = new Vector3(1f, 1f, 1f);
					if(inningType == 0)
						obj.transform.FindChild("LblTail").gameObject.SetActive(false);
					else
						obj.transform.FindChild("LblHead").gameObject.SetActive(false);

					obj.transform.FindChild("LblRound").GetComponent<UILabel>().text = gameRound + "";
					obj.transform.localPosition = new Vector3(0f, -mPosGuide, 0f);
					mPosGuide += obj.GetComponent<BoxCollider2D> ().size.y;
					mPosGuide += (122 - 30f) / 2f;
				}
			}



			GameObject go = Instantiate(itemHitter, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
			go.transform.parent = mList.transform;
			go.transform.localScale = new Vector3(1f, 1f, 1f);		
			go.GetComponent<ScriptItemHitterHighlight> ().Init (quizInfo);
			go.transform.localPosition = new Vector3(0f, -mPosGuide, 0f);
			mPosGuide += go.GetComponent<BoxCollider2D> ().size.y;
			if(ScriptMainTop.SequenceQuiz < quizInfo.quizListSeq)
				ScriptMainTop.SequenceQuiz = quizInfo.quizListSeq;
		}

		mList.GetComponent<UIScrollView> ().ResetPosition ();
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
		mScoreBoard.transform.FindChild ("Const").gameObject.SetActive (true);
		mScoreBoard.transform.FindChild ("TeamTop").gameObject.SetActive (true);
		mScoreBoard.transform.FindChild ("TeamBottom").gameObject.SetActive (true);

		ScriptMainTop.DetailBoard = mEventDetail.Response.data;

		SetAwayScore (ScriptMainTop.DetailBoard.awayScore);
		SetHomeScore (ScriptMainTop.DetailBoard.homeScore);
		SetAwayRHEB (ScriptMainTop.DetailBoard.infoBoard[0]);
		SetHomeRHEB (ScriptMainTop.DetailBoard.infoBoard[1]);

		if (mFirstLoading)
			SetProgQuiz (0);
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
