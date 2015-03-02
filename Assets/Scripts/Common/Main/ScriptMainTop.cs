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

	enum STATE{
		Highlight,
		Lineup,
		Bingo,
		Livetalk,
		Betting
	};

	STATE mState = STATE.Highlight;

	// Use this for initialization
	void Start () {
//		mHighlight = transform.parent.FindChild ("TF_Highlight").gameObject;
//		mLineup = transform.parent.FindChild ("TF_Lineup").gameObject;
//		mStatistic = transform.parent.FindChild ("TF_Statistic").gameObject;
//		mLivetalk = transform.parent.FindChild ("TF_Livetalk").gameObject;
//		mBetting = transform.parent.FindChild ("TF_Betting").gameObject;

		mHighlight.SetActive (true);
		mLineup.SetActive (false);
		mBingo.SetActive (false);
		mLivetalk.SetActive (false);
		mBetting.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
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

	void OpenBetting()
	{
		mHighlight.SetActive (false);
		mLineup.SetActive (false);
		mBingo.SetActive (false);
		mLivetalk.SetActive (false);
		mBetting.SetActive (true);
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
