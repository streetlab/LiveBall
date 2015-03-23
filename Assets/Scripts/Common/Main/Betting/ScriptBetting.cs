using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptBetting : MonoBehaviour {

	string mSelectedName;
	ScriptBettingItem mSbi;
	
	public GameObject mBtnHit1;
	public GameObject mBtnHit2;
	public GameObject mBtnHit3;
	public GameObject mBtnHit4;
	public GameObject mBtnOut1;
	public GameObject mBtnOut2;
	public GameObject mBtnOut3;
	public GameObject mBtnOut4;
	public GameObject mBtnLoaded1;
	public GameObject mBtnLoaded2;
	public GameObject mBtnLoaded3;
	public GameObject mBtnLoaded4;

	UIButton mBtnCancel;
	UIButton mBtnConfirm;
	UIButton mBtnConfirm2;
	UILabel mLblGot;
	UILabel mLblUse;
	UILabel mLblExpect;

	double mAmountUse = 1000d;

	JoinQuizEvent mJoinQuizEvent;

	void Start () {
		gameObject.SetActive (false);
		Transform panel = transform.FindChild ("Panel").transform;
		mBtnCancel = panel.FindChild ("BtnCancel").GetComponent<UIButton> ();
		mBtnConfirm = panel.FindChild ("BtnConfirm").GetComponent<UIButton> ();
		mBtnConfirm2 = panel.FindChild ("BtnConfirm2").GetComponent<UIButton> ();
		mLblGot = panel.FindChild ("SprBack1").FindChild("LblAmount").GetComponent<UILabel> ();
		mLblUse = panel.FindChild ("SprBack2").FindChild("LblAmount").GetComponent<UILabel> ();
		mLblExpect = panel.FindChild ("SprBack3").FindChild("LblAmount").GetComponent<UILabel> ();
	}

	void OnEnable()
	{
		mLblGot.text = UtilMgr.AddsThousandsSeparator (UserMgr.UserInfo.userGoldenBall);
		mLblUse.text = UtilMgr.AddsThousandsSeparator (mAmountUse);
		mLblExpect.text = UtilMgr.AddsThousandsSeparator (mAmountUse * float.Parse(GetOrder().ratio));
	}

	void OnDisable()
	{
		Debug.Log("Betting Disable");
		QuizMgr.IsBettingOpended = false;
	}

	public void Init(string name)
	{
		mSelectedName = name;
		mSbi = GetBettingItem ();
		if(mSbi.IsSelected)
		{
			mBtnCancel.gameObject.SetActive(true);
			mBtnConfirm.gameObject.SetActive(true);
			mBtnConfirm2.gameObject.SetActive(false);
		}
		else
		{
			mBtnCancel.gameObject.SetActive(false);//
			mBtnConfirm.gameObject.SetActive(false);
			mBtnConfirm2.gameObject.SetActive(true);
		}
	}

	void SetConfirm()
	{	
		//send to server
		//param={%22memSeq%22:423%20,%22gameSeq%22:1216%20,%22quizListSeq%22:9%20,%22qzType%22:1%20,%22useCardNo%22:140300988901%20,%22betPoint%22:%22100%22%20,%22item%22:1000%20,%22selectValue%22:%221%22%20,%22extendValue%22:%220%22%20}&type=spos&id=gameSposQuizJoin
		JoinQuizInfo joinInfo = new JoinQuizInfo ();
		joinInfo.GameSeq = UserMgr.Schedule.gameSeq;
		joinInfo.MemSeq = UserMgr.UserInfo.memSeq;
		joinInfo.QuizListSeq = QuizMgr.SequenceQuiz;
		joinInfo.QzType = GetOrder ().quizType;
		joinInfo.UseCardNo = 0;
		joinInfo.BetPoint = string.Format ("{0}", double.Parse (mLblUse.text));
		joinInfo.Item = 1000;
		joinInfo.SelectValue = string.Format("{0}", GetOrder ().orderSeq);
		joinInfo.ExtendValue = "0";
		mJoinQuizEvent = new JoinQuizEvent(new EventDelegate(this, "CompleteSending"));
		NetMgr.JoinQuiz (joinInfo, mJoinQuizEvent);
	}

	public void CompleteSending()
	{
		mSbi.SetSelected ();
		SetBtnsDisable ();
		CheckToClose ();
		UtilMgr.OnBackPressed ();

	}

	void CheckToClose()
	{
		QuizMgr.JoinCount += 1;
		if (transform.parent.GetComponent<ScriptTF_Betting>().QuizInfo.typeCode.Contains ("_QZD_")) {
			if(QuizMgr.JoinCount > 1)
				UtilMgr.OnBackPressed();
		} else{
			UtilMgr.OnBackPressed();
		}
	}

	void SetBtnsDisable()
	{
		switch(mSelectedName)
		{
		case "BtnHit1":
		case "BtnHit2":
		case "BtnHit3":
		case "BtnHit4":
			mBtnHit1.GetComponent<BoxCollider2D>().enabled = false;
			mBtnHit2.GetComponent<BoxCollider2D>().enabled = false;
			mBtnHit3.GetComponent<BoxCollider2D>().enabled = false;
			mBtnHit4.GetComponent<BoxCollider2D>().enabled = false;
			Debug.Log("Hit Disabled");
			break;
		case "BtnOut1":
		case "BtnOut2":
		case "BtnOut3":
		case "BtnOut4":
			mBtnOut1.GetComponent<BoxCollider2D>().enabled = false;
			mBtnOut2.GetComponent<BoxCollider2D>().enabled = false;
			mBtnOut3.GetComponent<BoxCollider2D>().enabled = false;
			mBtnOut4.GetComponent<BoxCollider2D>().enabled = false;
			Debug.Log("Out Disabled");
			break;
		}
	}

	void SetCancel()
	{
		mSbi.SetUnselected ();
//		CloseWindow ();
		UtilMgr.OnBackPressed ();
	}

	OrderInfo GetOrder()
	{
		switch (mSelectedName) {
		case "BtnHit1":
			return UserMgr.QuizInfo.order [0];
		case "BtnHit2":
			return UserMgr.QuizInfo.order [1];
		case "BtnHit3":
			return UserMgr.QuizInfo.order [2];
		case "BtnHit4":
			return UserMgr.QuizInfo.order [3];
		case "BtnOut1":
			return UserMgr.QuizInfo.order [4];
		case "BtnOut2":
			return UserMgr.QuizInfo.order [5];
		case "BtnOut3":
			return UserMgr.QuizInfo.order [6];
		case "BtnOut4":
			return UserMgr.QuizInfo.order [7];
		case "BtnLoaded1":
			return UserMgr.QuizInfo.order [0];
		case "BtnLoaded2":
			return UserMgr.QuizInfo.order [1];
		case "BtnLoaded3":
			return UserMgr.QuizInfo.order [2];
		case "BtnLoaded4":
			return UserMgr.QuizInfo.order [3];
		}
		return null;
	}

	ScriptBettingItem GetBettingItem()
	{
		switch(mSelectedName)
		{
		case "BtnHit1":
			return mBtnHit1.GetComponent<ScriptBettingItem>();
		case "BtnHit2":
			return mBtnHit2.GetComponent<ScriptBettingItem>();
		case "BtnHit3":
			return mBtnHit3.GetComponent<ScriptBettingItem>();
		case "BtnHit4":
			return mBtnHit4.GetComponent<ScriptBettingItem>();
		case "BtnOut1":
			return mBtnOut1.GetComponent<ScriptBettingItem>();
		case "BtnOut2":
			return mBtnOut2.GetComponent<ScriptBettingItem>();
		case "BtnOut3":
			return mBtnOut3.GetComponent<ScriptBettingItem>();
		case "BtnOut4":
			return mBtnOut4.GetComponent<ScriptBettingItem>();
		case "BtnLoaded1":
			return mBtnLoaded1.GetComponent<ScriptBettingItem>();
		case "BtnLoaded2":
			return mBtnLoaded2.GetComponent<ScriptBettingItem>();
		case "BtnLoaded3":
			return mBtnLoaded3.GetComponent<ScriptBettingItem>();
		case "BtnLoaded4":
			return mBtnLoaded4.GetComponent<ScriptBettingItem>();
			
		}
		return null;
	}

	public void CloseWindow()
	{
		gameObject.SetActive(false);
	}

	void Bet(int amount)
	{
		mAmountUse += amount;
		//if the Amount over the Max then process under
		if(mAmountUse >= double.Parse(UserMgr.UserInfo.userGoldenBall))
		{
			mAmountUse = double.Parse(UserMgr.UserInfo.userGoldenBall);
		}

		mLblUse.text = UtilMgr.AddsThousandsSeparator (mAmountUse);
		mLblExpect.text = UtilMgr.AddsThousandsSeparator (mAmountUse*2);

	}

	void BetMax()
	{
		mAmountUse = double.Parse (UserMgr.UserInfo.userGoldenBall);
		mLblUse.text = UtilMgr.AddsThousandsSeparator (mAmountUse);
		mLblExpect.text = UtilMgr.AddsThousandsSeparator (mAmountUse * float.Parse(GetOrder().ratio));
	}

	void BetMin()
	{
		mAmountUse = 1000;
		mLblUse.text = UtilMgr.AddsThousandsSeparator (mAmountUse);
		mLblExpect.text = UtilMgr.AddsThousandsSeparator (mAmountUse * float.Parse(GetOrder().ratio));
	}

	public void BtnClicked(string name)
	{
		switch(name)
		{
		case "BtnClose":
//			CloseWindow();
			UtilMgr.OnBackPressed();
			break;
		case "BtnConfirm":
			SetConfirm();
			break;
		case "BtnCancel":
			SetCancel();
			break;
		case "Btn10":
			Bet(10);
			break;
		case "Btn100":
			Bet(100);
			break;
		case "Btn1000":
			Bet (1000);
			break;
		case "BtnMax":
			BetMax();
			break;
		case "BtnMin":
			BetMin();
			break;
		}
	}
}
