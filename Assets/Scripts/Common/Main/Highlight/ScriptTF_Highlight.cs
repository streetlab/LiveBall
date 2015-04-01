using UnityEngine;
using System.Collections;

public class ScriptTF_Highlight : MonoBehaviour {

	public GameObject mMatchReady;
	public GameObject mMatchPlaying;
	public GameObject mMatchEnded;

	void Start()
	{
//		if(UserMgr.Schedule.gameStatus == 0)
//		{
//			OpenMatchReady();
//		} else if(UserMgr.Schedule.gameStatus == 1)
//		{
			OpenMatchPlaying();
//		} else
//		{
//			OpenMatchEnded();
//		}
	}

//	void Update(){
//		Debug.Log ("mil sec : " + System.DateTime.Now.Millisecond);
//	}

	void OpenMatchReady()
	{
		mMatchReady.SetActive (true);
		mMatchPlaying.SetActive (false);
		mMatchEnded.SetActive (false);
	}

	void OpenMatchPlaying()
	{
		mMatchReady.SetActive (false);
		mMatchPlaying.SetActive (true);
		mMatchEnded.SetActive (false);
	}

	void OpenMatchEnded()
	{
		mMatchReady.SetActive (false);
		mMatchPlaying.SetActive (false);
		mMatchEnded.SetActive (true);
	}

}
