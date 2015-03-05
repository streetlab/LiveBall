using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptMatchPlaying : MonoBehaviour {
	public GameObject mScoreBoard;
	public GameObject mList;

	GetGameSposDetailBoardEvent mEvent;

	// Use this for initialization
	void Start () {
		SetScoreBoard ();
	}

	void SetScoreBoard()
	{
		mScoreBoard.transform.FindChild ("Const").gameObject.SetActive (false);
		mScoreBoard.transform.FindChild ("TeamTop").gameObject.SetActive (false);
		mScoreBoard.transform.FindChild ("TeamBottom").gameObject.SetActive (false);
		//Progressing
		mEvent = new GetGameSposDetailBoardEvent (new EventDelegate (this, "GotDetailBoard"));
		NetMgr.GetGameSposDetailBoard (mEvent);
	}

	public void GotDetailBoard()
	{
		mScoreBoard.transform.FindChild ("Const").gameObject.SetActive (true);
		mScoreBoard.transform.FindChild ("TeamTop").gameObject.SetActive (true);
		mScoreBoard.transform.FindChild ("TeamBottom").gameObject.SetActive (true);

		SetAwayScore (mEvent.GetResponse().data.awayScore);
		SetHomeScore (mEvent.GetResponse().data.homeScore);
		SetAwayRHEB (mEvent.GetResponse().data.infoBoard[0]);
		SetHomeRHEB (mEvent.GetResponse().data.infoBoard[1]);
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
