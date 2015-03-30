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
	bool mIsTail = false;
	int mIndex;

	public void GoLeftBtn(){
		UIPanel panel = NGUITools.FindInParents<UIPanel>(gameObject);
		NGUITools.FindInParents<ScriptMatch> (panel.gameObject).mListMatch.GetComponent<UIScrollView> ().MoveRelative (new Vector3 (500f, 0, 0));
		GoLeft ();
	}

	public void GoLeft()
	{
		UIPanel panel = NGUITools.FindInParents<UIPanel>(gameObject);
//		Debug.Log("move prev");
		if(mIndex > 0){
			NGUITools.FindInParents<ScriptMatch>(panel.gameObject).mListScriptMatchItem[mIndex-1].OnCenter();
		}
	}

	public void GoRightBtn(){
		UIPanel panel = NGUITools.FindInParents<UIPanel>(gameObject);
		NGUITools.FindInParents<ScriptMatch> (panel.gameObject).mListMatch.GetComponent<UIScrollView> ().MoveRelative (new Vector3 (-500f, 0, 0));
		GoRight ();
	}

	public void GoRight()
	{
		UIPanel panel = NGUITools.FindInParents<UIPanel>(gameObject);
//		Debug.Log("move next");
		if(!mIsTail){
			NGUITools.FindInParents<ScriptMatch>(panel.gameObject).mListScriptMatchItem[mIndex+1].OnCenter();
		}

//		NGUITools.FindInParents<ScriptMatch> (panel.gameObject).mListMatch.GetComponent<UIScrollView> ().Scroll (1f);
//		NGUITools.FindInParents<ScriptMatch> (panel.gameObject).mListMatch.GetComponent<UIScrollView> ().Scroll (-1f);
//		float delta = transform.TransformPoint (new Vector3 (1440f, 0, 0)).x;
//		Debug.Log ("delta : " + delta);
//		NGUITools.FindInParents<ScriptMatch> (panel.gameObject).mListMatch.GetComponent<UIScrollView> ().Scroll (delta	);
	}

	public void Clicked()
	{
		UserMgr.Schedule = mSchedule;
		AutoFade.LoadLevel ("SceneMain", 0.5f, 1f);
	}

	public void Init(ScheduleInfo schedule, int index)
	{
		mSchedule = schedule;
		mIndex = index;
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
		sprLeftTeam.spriteName = UtilMgr.GetTeamEmblem (schedule.extend [0].imageName);
		sprRightTeam.spriteName = UtilMgr.GetTeamEmblem (schedule.extend [1].imageName);

		//temp
		if(schedule.gameStatus == 0)
		{

		} else if(schedule.gameStatus == 1)
		{
			mBGMatch.GetComponent<UISprite>().color = new Color(0f, 1f, 0f, 0.5f);
		} else
		{
			mBGMatch.GetComponent<UISprite>().color = new Color(0f, 0f, 1f, 0.5f);
		}

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
		mIsTail = true;
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

	public void OnCenter()
	{
		UIPanel panel = NGUITools.FindInParents<UIPanel>(gameObject);

		UIScrollView sv = panel.GetComponent<UIScrollView>();
//		UIDraggablePanel2 sv2 = panel.GetComponent<UIDraggablePanel2>();
		Vector3 offset = -panel.cachedTransform.InverseTransformPoint(transform.position);
		if (!sv.canMoveHorizontally) offset.x = panel.cachedTransform.localPosition.x;
		if (!sv.canMoveVertically) offset.y = panel.cachedTransform.localPosition.y;


		float myX = panel.cachedTransform.localPosition.x - offset.x;
//		Debug.Log ("offset : " + offset.x);
//		Debug.Log ("panel.cachedTransform.localPosition.x : " + panel.cachedTransform.localPosition.x);
//		Debug.Log ("myX : " + myX);
		if (myX < -360f) {
			GoRight ();
		} else 
		if(myX > 360f){
			GoLeft ();
//		} else if(Mathf.Abs(myX) > 180f){
//			GoRight ();
		} else{
//			Debug.Log("on Center");

			SpringPanel.Begin(panel.cachedGameObject, offset, 6f);
		}
	}
}
