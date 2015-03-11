using UnityEngine;
using System.Collections;

public class ScriptMatchReady : MonoBehaviour {
	public GameObject itemInfo;
	public GameObject itemPoll;
	public GameObject mList;

	GetQuizEvent mEvent;
	float mPosGuide;

	// Use this for initialization
	void Start () {
		UtilMgr.ResizeList (mList);
		mEvent = new GetQuizEvent (new EventDelegate (this, "GotQuiz"));
		NetMgr.GetPreparedQuiz (mEvent);
	}

	void SetMatchInfo()
	{
		GameObject obj = Instantiate(itemInfo, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		obj.transform.parent = mList.transform;
		obj.transform.localScale = new Vector3(1f, 1f, 1f);		
		obj.GetComponent<ScriptItemInfoHighlight> ().Init ();
		obj.transform.localPosition = new Vector3(0f, 0f, 0f);
		mPosGuide += obj.GetComponent<BoxCollider2D> ().size.y+10f;
		mPosGuide += (374f - 226f) / 2f;
	}

	void SetPreparedGames()
	{
		for(int i = 0; i < mEvent.Response.data.quiz.Count; i++)
		{
			GameObject obj = Instantiate(itemPoll, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
			obj.GetComponent<ScriptItemPollHighlight> ().Init (mEvent.Response.data.quiz[i]);
			obj.transform.parent = mList.transform;
			obj.transform.localScale = new Vector3(1f, 1f, 1f);
			obj.transform.localPosition = new Vector3(0f, -mPosGuide, 0f);
			mPosGuide += obj.GetComponent<BoxCollider2D> ().size.y+10f;

		}
	}

	public void GotQuiz()
	{
		mPosGuide = 0f;
		SetMatchInfo ();
		SetPreparedGames ();
		mList.GetComponent<UIScrollView> ().ResetPosition ();
	}
}
