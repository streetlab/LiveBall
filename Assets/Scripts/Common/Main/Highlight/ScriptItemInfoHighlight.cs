using UnityEngine;
using System.Collections;

public class ScriptItemInfoHighlight : MonoBehaviour {

	public GameObject mBack;
	public GameObject mLabel;
	public GameObject mSpr;
	public GameObject mLblBody;

	public void Init()
	{
		UILabel lblBody = mLblBody.GetComponent<UILabel> ();
//		lblBody.text = UserMgr.Schedule.subTitle;
//		Debug.Log (UserMgr.Schedule.subTitle);
	}

	public void OnClicked()
	{
		GameObject top = GameObject.Find ("Top");
		ScriptMainTop smt = top.GetComponent<ScriptMainTop>();
//		smt.mHighlight.SetActive (false);
//		smt.mLineup.SetActive (false);
//		smt.mBingo.SetActive (false);
//		smt.mLivetalk.SetActive (false);
//		smt.mBetting.SetActive (true);
//		smt.OpenBetting ();
	}
}
