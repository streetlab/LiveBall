using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptMatch : MonoBehaviour {

	public GameObject mListMatch;
	GetScheduleMoreEvent mScheduleEvent;
	List<ScheduleInfo> mScheduleList;

	// Use this for initialization
	void Start () {
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
		mListMatch.GetComponent<UIDraggablePanel2> ().Init (mScheduleList.Count, delegate(UIListItem item, int index) {
			ScriptMatchItem sItem = item.Target.GetComponent<ScriptMatchItem>();
			sItem.Init(mScheduleList[index], index);

			if(mScheduleList.Count == 1)
			{
				sItem.DeactiveAllBtns();
			}
			else if(index == 0)
			{
				sItem.DeactiveLeftBtn();
			}
			else if(index == mScheduleList.Count-1)
			{
				sItem.DeactiveRightBtn();
			}
		});
		UIDraggablePanel2 listMatch = mListMatch.GetComponent<UIDraggablePanel2> ();
	}

	public void Clicked()
	{

	}

	public void SetListMatchDisable()
	{
		Debug.Log("Finished");
	}
}
