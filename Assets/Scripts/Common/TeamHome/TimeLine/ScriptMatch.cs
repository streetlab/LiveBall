using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptMatch : MonoBehaviour {

	public List<ScriptMatchItem> mListScriptMatchItem;

	public GameObject mListMatch;
	GetScheduleMoreEvent mScheduleEvent;
	List<ScheduleInfo> mScheduleList;
	public GameObject mListMatchItem;

	// Use this for initialization
	void Start () {
		mListScriptMatchItem = new List<ScriptMatchItem> ();
		InitMatchList ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitMatchList()
	{
		mScheduleEvent = new GetScheduleMoreEvent (new EventDelegate (this, "GotSchedule"));
		NetMgr.GetScheduleMore (mScheduleEvent);
	}

	void GotSchedule()
	{
		mScheduleList = mScheduleEvent.GetResponse ().data;

		for (int i = 0; i < mScheduleList.Count; i++) {
			Debug.Log ("for : "+i);
			GameObject obj = Instantiate(mListMatchItem, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;

			obj.transform.parent = mListMatch.transform.FindChild("Grid").transform;
			obj.transform.localScale = new Vector3(1f, 1f, 1f);
			ScriptMatchItem sItem = obj.GetComponent<ScriptMatchItem> ();
			mListScriptMatchItem.Add(sItem);
			sItem.Init (mScheduleList[i], i);
			if(mScheduleList.Count == 1)
			{
				sItem.DeactiveAllBtns();
			}
			else if(i == 0)
			{
				sItem.DeactiveLeftBtn();
			}
			else if(i == mScheduleList.Count-1)
			{
				sItem.DeactiveRightBtn();
			}
			Debug.Log ("after for : "+i);
		}
		mListMatch.transform.FindChild("Grid").GetComponent<UIGrid>().Reposition();

//		mListMatch.GetComponent<UIDraggablePanel2> ().Init (mScheduleList.Count, delegate(UIListItem item, int index) {
//			ScriptMatchItem sItem = item.Target.GetComponent<ScriptMatchItem>();
//			sItem.Init(mScheduleList[index], index);
//			if(mScheduleList.Count == 1)
//			{
//				sItem.DeactiveAllBtns();
//			}
//			else if(index == 0)
//			{
//				sItem.DeactiveLeftBtn();
//			}
//			else if(index == mScheduleList.Count-1)
//			{
//				sItem.DeactiveRightBtn();
//			}
//		});
//		UIDraggablePanel2 listMatch = mListMatch.GetComponent<UIDraggablePanel2> ();
	
	}

	public void Clicked()
	{

	}

	public void SetListMatchDisable()
	{
		Debug.Log("Finished");
	}
}
