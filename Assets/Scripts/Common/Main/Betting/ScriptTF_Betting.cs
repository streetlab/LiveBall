using UnityEngine;
using System.Collections;

public class ScriptTF_Betting : MonoBehaviour {

	public GameObject mTop;
	public GameObject mSprComb;
	public GameObject mSprHit;
	public GameObject mSprOut;
	public GameObject mSprCard;
	public GameObject mSprBetting;
	public GameObject mSprLoaded;

//	QuizInfo quizInfo;
//	public QuizInfo QuizInfo
//	{
//		get{return quizInfo;}
//	}
	int mStartSec;
	int mStartMilSec;

	static Color YELLOW = new Color(1f, 1f, 0f);
	static Color WHITE = new Color(1f, 1f, 1f);
	static Color RED = new Color(1f, 0f, 0f);

	void Update()
	{
		int sec = mStartSec - System.DateTime.Now.Second;
		if (sec > 0)
			sec = sec - 60;

//		int milSec = mStartMilSec - System.DateTime.Now.Millisecond;
//		if (milSec > -1)
//			milSec = milSec - 1000;
//		milSec = milSec / 10;

		if (sec <= -15) {
			UtilMgr.OnBackPressed();
			return;
		}

		mSprComb.transform.FindChild ("LblTimer").GetComponent<UILabel> ().text
			= string.Format ("{0}", (15 + sec));// + " : " + (99+milSec);
	}

	public void Init(QuizInfo quizInfo)
	{
//		quizInfo = quizInfo;
		SetHitter ();
		SetPitcher ();
		SetBases ();
		SetBtns ();

		mStartSec = System.DateTime.Now.Second;
		mStartMilSec = System.DateTime.Now.Millisecond;
	}

	void SetBtns()
	{
		if (QuizMgr.QuizInfo.typeCode.Contains ("_QZD_")) {
			mSprHit.SetActive(true);
			mSprOut.SetActive(true);
			mSprLoaded.SetActive(false);

			mSprHit.transform.FindChild ("BtnHit1").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [0].ratio;
			mSprHit.transform.FindChild ("BtnHit2").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [1].ratio;
			mSprHit.transform.FindChild ("BtnHit3").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [2].ratio;
			mSprHit.transform.FindChild ("BtnHit4").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [3].ratio;
			mSprOut.transform.FindChild ("BtnOut1").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [4].ratio;
			mSprOut.transform.FindChild ("BtnOut2").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [5].ratio;
			mSprOut.transform.FindChild ("BtnOut3").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [6].ratio;
			mSprOut.transform.FindChild ("BtnOut4").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [7].ratio;

			mSprHit.transform.FindChild ("BtnHit1").GetComponent<BoxCollider2D> ().enabled = true;
			mSprHit.transform.FindChild ("BtnHit2").GetComponent<BoxCollider2D> ().enabled = true;
			mSprHit.transform.FindChild ("BtnHit3").GetComponent<BoxCollider2D> ().enabled = true;
			mSprHit.transform.FindChild ("BtnHit4").GetComponent<BoxCollider2D> ().enabled = true;
			mSprOut.transform.FindChild ("BtnOut1").GetComponent<BoxCollider2D> ().enabled = true;
			mSprOut.transform.FindChild ("BtnOut2").GetComponent<BoxCollider2D> ().enabled = true;
			mSprOut.transform.FindChild ("BtnOut3").GetComponent<BoxCollider2D> ().enabled = true;
			mSprOut.transform.FindChild ("BtnOut4").GetComponent<BoxCollider2D> ().enabled = true;

			mSprHit.transform.FindChild ("BtnHit1").GetComponent<ScriptBettingItem>().Reset();
			mSprHit.transform.FindChild ("BtnHit2").GetComponent<ScriptBettingItem>().Reset();
			mSprHit.transform.FindChild ("BtnHit3").GetComponent<ScriptBettingItem>().Reset();
			mSprHit.transform.FindChild ("BtnHit4").GetComponent<ScriptBettingItem>().Reset();
			mSprOut.transform.FindChild ("BtnOut1").GetComponent<ScriptBettingItem>().Reset();
			mSprOut.transform.FindChild ("BtnOut2").GetComponent<ScriptBettingItem>().Reset();
			mSprOut.transform.FindChild ("BtnOut3").GetComponent<ScriptBettingItem>().Reset();
			mSprOut.transform.FindChild ("BtnOut4").GetComponent<ScriptBettingItem>().Reset();
		} else if (QuizMgr.QuizInfo.typeCode.Contains ("_QZC_")) {
			mSprHit.SetActive(false);
			mSprOut.SetActive(false);
			mSprLoaded.SetActive(true);

			mSprLoaded.transform.FindChild("SprQuestionBack").FindChild("Label").GetComponent<UILabel>().text
				= QuizMgr.QuizInfo.quizTitle;

			mSprLoaded.transform.FindChild ("BtnLoaded1").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [0].ratio;
			mSprLoaded.transform.FindChild ("BtnLoaded2").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [1].ratio;
//			mSprLoaded.transform.FindChild ("BtnLoaded3").FindChild ("LblGP").GetComponent<UILabel> ().text = mQuizInfo.order [2].ratio;
//			mSprLoaded.transform.FindChild ("BtnLoaded4").FindChild ("LblGP").GetComponent<UILabel> ().text = mQuizInfo.order [3].ratio;

			mSprLoaded.transform.FindChild ("BtnLoaded1").GetComponent<ScriptBettingItem>().Reset();
			mSprLoaded.transform.FindChild ("BtnLoaded2").GetComponent<ScriptBettingItem>().Reset();
			mSprLoaded.transform.FindChild ("BtnLoaded3").GetComponent<ScriptBettingItem>().Reset();
			mSprLoaded.transform.FindChild ("BtnLoaded4").GetComponent<ScriptBettingItem>().Reset();
		}
	}

	void SetBases()
	{
		PlayInfo playInfo = ScriptMainTop.DetailBoard.play;
		Transform tfStatus = mSprComb.transform.FindChild ("SprStatus");
		tfStatus.FindChild ("LblNum").GetComponent<UILabel> ().text = string.Format("{0}",playInfo.playRound);
		tfStatus.FindChild ("LblRound").GetComponent<UILabel> ().text = UtilMgr.GetRoundString (playInfo.playRound);
		tfStatus.FindChild ("SprUp").GetComponent<UISprite> ().color = WHITE;
		tfStatus.FindChild ("SprDown").GetComponent<UISprite> ().color = WHITE;
		if(playInfo.playInningType > 0)
			tfStatus.FindChild ("SprDown").GetComponent<UISprite> ().color = YELLOW;
		else
			tfStatus.FindChild ("SprUp").GetComponent<UISprite> ().color = YELLOW;
		tfStatus.FindChild ("SprOut1").GetComponent<UISprite> ().color = WHITE;
		tfStatus.FindChild ("SprOut2").GetComponent<UISprite> ().color = WHITE;
		switch (playInfo.outCount) {
		case 2:
			tfStatus.FindChild ("SprOut2").GetComponent<UISprite> ().color = RED;
			goto case 1;
		case 1:
			tfStatus.FindChild ("SprOut1").GetComponent<UISprite> ().color = RED;
			break;
		}
		tfStatus.FindChild ("SprBases").FindChild ("Base1").gameObject.SetActive (false);
		tfStatus.FindChild ("SprBases").FindChild ("Base2").gameObject.SetActive (false);
		tfStatus.FindChild ("SprBases").FindChild ("Base3").gameObject.SetActive (false);
		if(playInfo.base1st > 0)
			tfStatus.FindChild ("SprBases").FindChild ("Base1").gameObject.SetActive (true);
		if(playInfo.base2nd > 0)
			tfStatus.FindChild ("SprBases").FindChild ("Base2").gameObject.SetActive (true);
		if(playInfo.base3rd > 0)
			tfStatus.FindChild ("SprBases").FindChild ("Base3").gameObject.SetActive (true);

	}

	void SetPitcher()
	{
		Transform tfPitcher = mSprComb.transform.FindChild ("SprPitcher");
		string playerInfo = ScriptMainTop.DetailBoard.player [0].playerName + " No." + ScriptMainTop.DetailBoard.player [0].playerNumber;
		tfPitcher.FindChild ("LblName").GetComponent<UILabel> ().text = playerInfo;
		string playerAVG = ScriptMainTop.DetailBoard.player [0].ERA;
		tfPitcher.FindChild ("LblSave").GetComponent<UILabel> ().text = playerAVG;
		//		WWW www = new WWW (Constants.IMAGE_SERVER_HOST + quizInfo.imageName);
		//		StartCoroutine (GetImage(www, tfHitter.FindChild ("Panel").FindChild ("Texture").GetComponent<UITexture> ()));
	}

	void SetHitter()
	{ 
		Transform tfHitter = mSprComb.transform.FindChild ("SprHitter");
		string playerInfo = QuizMgr.QuizInfo.playerName + " No." + QuizMgr.QuizInfo.playerNumber;
		tfHitter.FindChild ("LblName").GetComponent<UILabel> ().text = playerInfo;
		string playerAVG = ScriptMainTop.DetailBoard.player [1].AVG;
		tfHitter.FindChild("LblHit").GetComponent<UILabel>().text = playerAVG;
		tfHitter.FindChild ("LblTeam").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.teamName;
		WWW www = new WWW (Constants.IMAGE_SERVER_HOST + QuizMgr.QuizInfo.imageName);
		StartCoroutine (GetImage(www, tfHitter.FindChild ("Panel").FindChild ("Texture").GetComponent<UITexture> ()));
	}

	IEnumerator GetImage(WWW www, UITexture texture)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		www.LoadImageIntoTexture (tmpTex);
		texture.mainTexture = tmpTex;
	}

	void OnEnable()
	{
		ScriptMainTop smt = mTop.GetComponent<ScriptMainTop> ();
		smt.mBtnHighlight.SetActive (false);
		smt.mBtnLineup.SetActive (false);
		smt.mBtnLivetalk.SetActive (false);
		smt.mBtnBingo.SetActive (false);
	}

	void OnDisable()
	{
		ScriptMainTop smt = mTop.GetComponent<ScriptMainTop> ();
		smt.mBtnHighlight.SetActive (true);
		smt.mBtnLineup.SetActive (true);
		smt.mBtnLivetalk.SetActive (true);
		smt.mBtnBingo.SetActive (true);
	}

	public void CloseBetting()
	{

	}
}
