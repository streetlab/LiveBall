using UnityEngine;
using System.Collections;

public class ScriptTitle : MonoBehaviour {

	LoginEvent mLoginEvent;
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
		LoginInfo loginInfo = mLoginEvent.GetResponse ().data;
		UserMgr.GetUserInfo ().teamCode = loginInfo.teamCode;
		UserMgr.GetUserInfo ().teamSeq = loginInfo.teamSeq;
		UserMgr.GetUserInfo ().memSeq = loginInfo.memSeq;

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
