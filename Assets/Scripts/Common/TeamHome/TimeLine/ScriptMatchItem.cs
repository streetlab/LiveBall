using UnityEngine;
using System.Collections;

public class ScriptMatchItem : cUIScrollListBase {

	public GameObject mSprLeftTeam;
	public GameObject mSprRightTeam;
	public GameObject mLblLeftTeam;
	public GameObject mLblRightTeam;
	public GameObject mLblScore;
	public GameObject mLblDetail;
	public GameObject mBtnArrowLeft;
	public GameObject mBtnArrowRight;
	public GameObject mSprUnderline;
	public GameObject mBGMatch;

	ScheduleInfo mSchedule;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Clicked()
	{
		UserMgr.Schedule = mSchedule;
		AutoFade.LoadLevel ("SceneMain", 0.5f, 1f);
	}

	public void Init(ScheduleInfo schedule, int index)
	{
		mSchedule = schedule;
		ActiveAllBtns ();

		UILabel lblDetail = mLblDetail.GetComponent<UILabel> ();
		UILabel lblLeftTeam = mLblLeftTeam.GetComponent<UILabel> ();
		UILabel lblRightTeam = mLblRightTeam.GetComponent<UILabel> ();
		UILabel lblScore = mLblScore.GetComponent<UILabel> ();
		UISprite sprLeftTeam = mSprLeftTeam.GetComponent<UISprite> ();
		UISprite sprRightTeam = mSprRightTeam.GetComponent<UISprite> ();

		lblDetail.text = UtilMgr.ConvertToDate (schedule.startTime);
		lblLeftTeam.text = schedule.extend [0].teamName;
		lblRightTeam.text = schedule.extend [1].teamName;
		lblScore.text = schedule.extend [0].score + " : " + schedule.extend [1].score;
		sprLeftTeam.spriteName = UtilMgr.GetTeamEmblem (schedule.extend [0].teamSeq);
		sprRightTeam.spriteName = UtilMgr.GetTeamEmblem (schedule.extend [1].teamSeq);

	}

	public void DeactiveLeftBtn()
	{
		mBtnArrowLeft.SetActive(false);
		mBtnArrowRight.SetActive(true);
	}

	public void DeactiveRightBtn()
	{
		mBtnArrowLeft.SetActive(true);
		mBtnArrowRight.SetActive(false);
	}

	public void DeactiveAllBtns()
	{
		mBtnArrowLeft.SetActive(false);
		mBtnArrowRight.SetActive(false);
	}

	public void ActiveAllBtns()
	{
		mBtnArrowLeft.SetActive(true);
		mBtnArrowRight.SetActive(true);
	}
}
