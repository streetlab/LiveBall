using UnityEngine;
using System.Collections;

public class ScriptTitle : MonoBehaviour {

	LoginEvent mLoginEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LoginFacebook()
	{
		//Get info from server

		//Load Scene Teamhome
//		Application.LoadLevel ("SceneTeamHome");
		LoginInfo loginInfo = new LoginInfo ();
		loginInfo.memberEmail = "gunloves@.";
		loginInfo.memUID = "gunloves";
		loginInfo.memberPwd = "asdf";
		mLoginEvent = new LoginEvent(new EventDelegate(this, "LoginComplete"));
		NetMgr.DoLogin (loginInfo, mLoginEvent);

		if(Application.platform == RuntimePlatform.Android)
		{
			AndroidMgr.CallJavaFunc ("RegisterGCM", "");
		}
		else
		{

		}

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
