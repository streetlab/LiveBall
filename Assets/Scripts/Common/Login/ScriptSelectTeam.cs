using UnityEngine;
using System.Collections;

public class ScriptSelectTeam : MonoBehaviour {

	public GameObject mBGBtnConfirm;
	bool mSelected;
	string mTeamCode;

	JoinMemberInfo mMemInfo;

	static Color SELECTED = new Color (67f / 255f, 169f / 255f, 230f / 255f);
	static Color UNSELECTED = new Color (136f / 255f, 143f / 255f, 157f / 255f);

	void Start()
	{
		mBGBtnConfirm.GetComponent<UISprite> ().color = UNSELECTED;
		transform.FindChild ("ListTeam").GetComponent<UIScrollView> ().ResetPosition ();
	}

	public void Init(JoinMemberInfo memInfo){
		gameObject.SetActive (true);
		mMemInfo = memInfo;
	}

	public void SetTeam(string teamCode)
	{
		mSelected = true;
		mTeamCode = teamCode;
		mBGBtnConfirm.GetComponent<UISprite> ().color = SELECTED;
	}

	public void GoNext()
	{
		if (mSelected) {
			mMemInfo.FavoBB = mTeamCode;
			#if(UNITY_EDITOR)
			mMemInfo.OsType = 0;
			CompleteGCM();
			#elif(UNITY_ANDROID)
			mMemInfo.OsType = 1;
			AndroidMgr.RegistGCM(new EventDelegate(this, "CompleteGCM"));
			#else
			memInfo.OsType = 2;
			#endif
		} else if(!mSelected) {
			DialogueMgr.ShowDialogue (
				"Select Team", "You must choose a team!", DialogueMgr.DIALOGUE_TYPE.Alert,
			                          null, null, null);
		}
	}

	public void CompleteGCM()
	{
		Debug.Log ("CompleteGCM");
		string memUID = "";
		#if(UNITY_EDITOR)

		#elif(UNITY_ANDROID)
		memUID = AndroidMgr.GetMsg();
		#else
		#endif
		mMemInfo.MemUID = memUID;
		GetComponentInParent<ScriptTitle>().mProfileEvent = 
			new GetProfileEvent(new EventDelegate(GetComponentInParent<ScriptTitle>(), "GotProfile"));
		
		NetMgr.JoinMember(mMemInfo, GetComponentInParent<ScriptTitle>().mProfileEvent);
	}
}
