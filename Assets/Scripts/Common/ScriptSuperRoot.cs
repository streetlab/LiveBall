using UnityEngine;
using System.Collections;

public class ScriptSuperRoot : MonoBehaviour {

	void Start () {
		transform.FindChild ("Camera").transform.localPosition = new Vector3(0, UtilMgr.GetScaledPositionY(), 0);
		#if(UNITY_EDITOR)
		#elif(UNITY_ANDROID)
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		#else
		iPhoneSettings.screenCanDarken = false;
		#endif
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			OnBackPressed();
		}
	}

	public void OnBackPressed()
	{
		Debug.Log ("DialogueMgr.IsShown : " + DialogueMgr.IsShown);
		if (DialogueMgr.IsShown) {
			DialogueMgr.DismissDialogue();
		} else {
			UtilMgr.OnBackPressed ();
		}
//		if (!UtilMgr.OnBackPressed ()) {
//			UtilMgr.SetBackEvent (new EventDelegate (this, "DismissDialogue"));
//		}
	}
	
//	public void DismissDialogue()
//	{
//		DialogueMgr.DismissDialogue ();
//	}
}
