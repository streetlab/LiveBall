using UnityEngine;
using System.Collections;

public class ScriptTitle : MonoBehaviour {

	LoginEvent mLoginEvent;
	GetProfileEvent mProfileEvent;
	GetCardInvenEvent mCardEvent;
	LoginInfo mLoginInfo;

	void Start()
	{
		transform.FindChild ("ContainerBtns").localPosition = new Vector3(0, UtilMgr.GetScaledPositionY()*2, 0);
	}

	void LoginFacebook()
	{
		//Get info from server

		//Load Scene Teamhome
//		Application.LoadLevel ("SceneTeamHome");
		mLoginInfo = new LoginInfo ();
		mLoginInfo.memberEmail = "gunloves@.";
		mLoginInfo.memberName = "gunloves";
		mLoginInfo.memberPwd = "asdf";
		mLoginEvent = new LoginEvent(new EventDelegate(this, "LoginComplete"));
//		NetMgr.DoLogin (loginInfo, mLoginEvent);

		//Receive UID(Push Key) then do login

		if (Application.platform == RuntimePlatform.Android) {
						AndroidMgr.CallJavaFunc ("RegisterGCM", "", this);
		} else if (Application.platform == RuntimePlatform.IPhonePlayer) {

		} else if(Application.platform == RuntimePlatform.OSXEditor){
			mLoginInfo.memUID = "";
			NetMgr.DoLogin (mLoginInfo, mLoginEvent);
		}

	}

	public void SetGCMId(string key)
	{
		mLoginInfo.memUID = key;
		NetMgr.DoLogin (mLoginInfo, mLoginEvent);
	}


	void LoginComplete()
	{
//		Debug.Log (mLoginEvent.GetResponse().data.memberEmail);
		mLoginInfo = mLoginEvent.Response.data;
//		UserMgr.UserInfo.teamCode = loginInfo.teamCode;
//		UserMgr.UserInfo.teamSeq = loginInfo.teamSeq;
//		UserMgr.UserInfo.memSeq = loginInfo.memSeq;

//		AutoFade.LoadLevel ("SceneTeamHome", 0f, 1f);
		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "GotProfile"));
		NetMgr.GetProfile (mLoginInfo.memSeq, mProfileEvent);
	}

	public void GotProfile()
	{
		UserMgr.UserInfo = mProfileEvent.Response.data;
		UserMgr.UserInfo.teamCode = mLoginInfo.teamCode;
		UserMgr.UserInfo.teamSeq = mLoginInfo.teamSeq;
		Debug.Log ("GotProfile");
		mCardEvent = new GetCardInvenEvent (new EventDelegate (this, "GotCardInven"));
		NetMgr.GetCardInven (mCardEvent);
	}

	public void GotCardInven()
	{
		Debug.Log ("GotCardInven");
		AutoFade.LoadLevel ("SceneTeamHome", 0f, 1f);
	}


	public void BtnClicked(string name)
	{
		switch(name)
		{
		case "Facebook":
			LoginFacebook();
			break;
		case "Cacao":

			break;
		case "Email":

			break;

		
		}
	}
}
