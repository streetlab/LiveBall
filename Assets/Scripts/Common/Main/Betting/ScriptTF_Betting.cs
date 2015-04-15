using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptTF_Betting : MonoBehaviour {

	public GameObject mTop;
	public GameObject mSprComb;
	public GameObject mSprHit;
	public GameObject mSprOut;
	public GameObject mSprCard;
	public GameObject mSprBetting;
	public GameObject mSprLoaded;

	public AudioClip mBoom;

	public List<JoinQuizInfo> mListJoin = new List<JoinQuizInfo>();

//	QuizInfo quizInfo;
//	public QuizInfo QuizInfo
//	{
//		get{return quizInfo;}
//	}
//	int mStartSec;
//	int mStartMilSec;
	long mStartTime;
	bool mTimeOut;

	static Color YELLOW = new Color(1f, 1f, 0f);
	static Color WHITE = new Color(1f, 1f, 1f);
	static Color RED = new Color(1f, 0f, 0f);

	const float MAX_TIME = 1600f;
	const float BAR_WIDTH = 132f;

	void Start()
	{
//		transform.FindChild ("Lightning Spark").GetComponent<ScriptParticleResizer> ().ResizeRatio (0.5f);

		transform.FindChild ("Lightning Spark").GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 3100;
		transform.FindChild ("Lightning Spark").FindChild("Lightning").GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 3100;
		transform.FindChild ("Lightning Spark").FindChild("Spakles").GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 3100;
		transform.FindChild ("Lightning Spark").FindChild("Ring").GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 3100;
		transform.FindChild ("Lightning Spark").FindChild("Ray").GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 3100;
	}

	void Update()
	{
		if (mTimeOut) {
//			if(UtilMgr.HasBackEvent)
//				UtilMgr.OnBackPressed ();
			UtilMgr.RunAllBackEvents();
			return;
		}

		CalcTime ();
	}

	void CalcTime()
	{
		long diff = mStartTime - System.DateTime.Now.ToFileTime();

		int sec = (int)(diff / 10000000L);
		int milSec = (int)(diff % 10000000L);
		
		if (sec < -15) {
			mTimeOut = true;
			sec = 0;
			milSec = 0;
		} else{
			sec = (15 + sec);

			milSec = milSec / 100000;//
			milSec = (99 + milSec);
		}
		

		SetTimer (sec, milSec);
	}

	void SetTimer(int sec, int milSec)
	{
		string strSec = "" + sec;
		if (sec < 10)
			strSec = "0" + sec;

		string strMilSec = "" + milSec;
		if (milSec < 10)
			strMilSec = "0" + milSec;
				
		mSprComb.transform.FindChild("Timer").FindChild ("LblTimer").GetComponent<UILabel> ().text
			= strSec + " : " + strMilSec;

		float now = float.Parse (strSec + strMilSec);
		float width = BAR_WIDTH * (now / MAX_TIME);
//		Debug.Log ("now : " + now);
//		Debug.Log ("width : " + width);
//		Debug.Log ("width to int : " + (int)width); 

		if (sec < 6) {
			mSprComb.transform.FindChild("Timer").FindChild ("LblTimer").GetComponent<UILabel> ().color = RED;
			mSprComb.transform.FindChild ("Timer").FindChild ("Progress").FindChild ("SprBar")
				.GetComponent<UISprite> ().color = RED;
		} else {
			mSprComb.transform.FindChild("Timer").FindChild ("LblTimer").GetComponent<UILabel> ().color = YELLOW;
			mSprComb.transform.FindChild ("Timer").FindChild ("Progress").FindChild ("SprBar")
				.GetComponent<UISprite> ().color = YELLOW;
		}

		mSprComb.transform.FindChild ("Timer").FindChild ("Progress").FindChild ("SprBar")
			.GetComponent<UISprite> ().width = (int)width;



	}

	public void Init(QuizInfo quizInfo)
	{
		Debug.Log("Init");
		mListJoin.Clear ();
//		quizInfo = quizInfo;
		SetHitter ();
		Debug.Log("Hitter");
		SetPitcher ();
		Debug.Log("Pitcher");
		SetBases ();
		Debug.Log("Bases");
		SetBtns ();
		Debug.Log("Btns");
		mSprBetting.SetActive (false);

		mStartTime = System.DateTime.Now.ToFileTime ();
		mTimeOut = false;
//		mStartSec = System.DateTime.Now.Second;
//		mStartMilSec = System.DateTime.Now.Millisecond / 10;
		TweenAlpha.Begin (mSprComb, 0f, 0f);
		TweenAlpha.Begin (mSprComb, 1f, 1.0f);

		transform.FindChild ("Lightning Spark").gameObject.SetActive (false);
		mSprBetting.SetActive (false);
	}

	void SetBtns()
	{
		if (QuizMgr.QuizInfo.typeCode.Contains ("_QZD_")) {
			mSprHit.SetActive(true);
			mSprOut.SetActive(true);
			mSprLoaded.SetActive(false);

			mSprHit.transform.FindChild ("BtnHit1").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [0].ratio+"x";
			mSprHit.transform.FindChild ("BtnHit2").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [1].ratio+"x";
			mSprHit.transform.FindChild ("BtnHit3").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [2].ratio+"x";
			mSprHit.transform.FindChild ("BtnHit4").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [3].ratio+"x";
			mSprOut.transform.FindChild ("BtnOut1").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [4].ratio+"x";
			mSprOut.transform.FindChild ("BtnOut2").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [5].ratio+"x";
			mSprOut.transform.FindChild ("BtnOut3").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [6].ratio+"x";
			mSprOut.transform.FindChild ("BtnOut4").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [7].ratio+"x";

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

			mSprLoaded.transform.FindChild ("BtnLoaded1").GetComponent<ScriptBettingItem>().Reset();
			mSprLoaded.transform.FindChild ("BtnLoaded2").GetComponent<ScriptBettingItem>().Reset();
			mSprLoaded.transform.FindChild ("BtnLoaded3").GetComponent<ScriptBettingItem>().Reset();
			mSprLoaded.transform.FindChild ("BtnLoaded4").GetComponent<ScriptBettingItem>().Reset();

			if(QuizMgr.QuizInfo.typeCode.Equals("BB_QZC_FULL")){
				Set4Btns();
			} else if(QuizMgr.QuizInfo.typeCode.Equals("BB_QZC_LOS")){
				Set2Btns();
			} else if(QuizMgr.QuizInfo.typeCode.Equals("BB_QZC_EOH")){
				Set2Btns();
			} else if(QuizMgr.QuizInfo.typeCode.Equals("BB_QZC_EOS")){
				Set2Btns();
			}
		}
	}

	void Set4Btns()
	{
		mSprLoaded.transform.FindChild ("BtnLoaded1").localPosition = new Vector3 (-260f, -210f, 0);
		mSprLoaded.transform.FindChild ("BtnLoaded2").localPosition = new Vector3 (-86f, -210f, 0);
		mSprLoaded.transform.FindChild ("BtnLoaded3").localPosition = new Vector3 (86f, -210f, 0);
		mSprLoaded.transform.FindChild ("BtnLoaded4").localPosition = new Vector3 (260f, -210f, 0);

		mSprLoaded.transform.FindChild ("BtnLoaded1").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [0].ratio+"x";
		mSprLoaded.transform.FindChild ("BtnLoaded2").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [1].ratio+"x";
		mSprLoaded.transform.FindChild ("BtnLoaded3").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [2].ratio+"x";
		mSprLoaded.transform.FindChild ("BtnLoaded4").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [3].ratio+"x";
	}

	void Set2Btns()
	{
		mSprLoaded.transform.FindChild ("BtnLoaded1").localPosition = new Vector3 (-164f, -210f, 0);
		mSprLoaded.transform.FindChild ("BtnLoaded2").localPosition = new Vector3 (164f, -210f, 0);
		mSprLoaded.transform.FindChild ("BtnLoaded3").gameObject.SetActive (false);
		mSprLoaded.transform.FindChild ("BtnLoaded4").gameObject.SetActive (false);

		mSprLoaded.transform.FindChild ("BtnLoaded1").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [0].ratio+"x";
		mSprLoaded.transform.FindChild ("BtnLoaded2").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [1].ratio+"x";
	}

	void SetBases()
	{
		PlayInfo playInfo = ScriptMainTop.DetailBoard.play;
		Transform tfStatus = mSprComb.transform.FindChild("SprPitcher").FindChild ("StatusInfo");
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
		if (ScriptMainTop.DetailBoard.player.Count < 2) {
			Debug.Log("No Pitcher");
		}
		Transform tfPitcher = mSprComb.transform.FindChild ("SprPitcher");
		string playerInfo = ScriptMainTop.DetailBoard.player [0].playerName + " No." + ScriptMainTop.DetailBoard.player [0].playerNumber;
		tfPitcher.FindChild ("LblName").GetComponent<UILabel> ().text = playerInfo;
		string playerAVG = ScriptMainTop.DetailBoard.player [0].ERA;
		tfPitcher.FindChild ("LblSave").GetComponent<UILabel> ().text = playerAVG;
		string strImage = ScriptMainTop.DetailBoard.player [0].imageName;
		if (ScriptMainTop.DetailBoard.player [0].imagePath != null 
		    && ScriptMainTop.DetailBoard.player [0].imagePath.Length > 0)
			strImage = ScriptMainTop.DetailBoard.player [0].imagePath
				+ ScriptMainTop.DetailBoard.player [0].imageName;
		WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
		StartCoroutine (GetImage(www, tfPitcher.FindChild ("Panel").FindChild ("Texture").GetComponent<UITexture> ()));
	}

	void SetHitter()
	{ 
		Transform tfHitter = mSprComb.transform.FindChild ("SprHitter");
		string playerInfo = QuizMgr.QuizInfo.playerName + " No." + QuizMgr.QuizInfo.playerNumber;
		tfHitter.FindChild ("LblName").GetComponent<UILabel> ().text = playerInfo;
		string playerAVG = ScriptMainTop.DetailBoard.player [ScriptMainTop.DetailBoard.player.Count-1].AVG;
		tfHitter.FindChild("LblHit").GetComponent<UILabel>().text = playerAVG;
		tfHitter.FindChild ("LblTeam").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.teamName;
		string strImage = QuizMgr.QuizInfo.imageName;
		if (QuizMgr.QuizInfo.imagePath != null && QuizMgr.QuizInfo.imagePath.Length > 0)
			strImage = QuizMgr.QuizInfo.imagePath + QuizMgr.QuizInfo.imageName;
		WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
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

	public void AnimateVS()
	{
		StartCoroutine ("VSAnimation");
//		transform.FindChild ("Lightning Spark").GetComponent<ParticleSystem> ().Simulate (5f, true, false);
	}

	IEnumerator VSAnimation(){
		yield return new WaitForSeconds (1f);

		transform.FindChild ("Lightning Spark").gameObject.SetActive (true);
		transform.FindChild ("Lightning Spark").GetComponent<ParticleSystem> ().Play ();
		transform.root.GetComponent<AudioSource>().PlayOneShot (mBoom);

		GameObject go = transform.FindChild("SprComb").FindChild("Panel").FindChild("SprVS").gameObject;
		TweenAlpha.Begin (go, 0f, 0f);
		TweenScale.Begin (go, 0f, new Vector3(1f, 1f, 1f));
		TweenAlpha.Begin (go, 0.5f, 1f);

		yield return new WaitForSeconds (1f);

		TweenAlpha.Begin (go, 0.5f, 0f);
		TweenScale.Begin(go, 0.5f, new Vector3(1.5f, 1.5f, 1.5f));

	}

//	public void CloseBetting()
//	{
//		Debug.Log ("CloseBetting");
//	}
}
