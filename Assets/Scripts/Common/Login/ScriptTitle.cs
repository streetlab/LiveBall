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

		transform.FindChild ("ContainerBtns").gameObject.SetActive (false);
		transform.FindChild ("WindowEmail").gameObject.SetActive (false);
		transform.FindChild ("FormJoin").gameObject.SetActive (false);

		CheckPreference ();
	}

	void CheckPreference()
	{
		string email = PlayerPrefs.GetString (Constants.PrefEmail);
		string pwd = PlayerPrefs.GetString (Constants.PrefPwd);
		if (email == null || email.Length < 1) {
			StopLogin();
		}
		else{
			DoLogin(email, pwd);
		}
	}

	void StopLogin()
	{
		transform.FindChild ("ContainerBtns").gameObject.SetActive (true);
	}

	public void DoLogin(string eMail, string pwd)
	{
		//Get info from server
		
		//Load Scene Teamhome
		//		Application.LoadLevel ("SceneTeamHome");
		mLoginInfo = new LoginInfo ();
		mLoginInfo.memberEmail = eMail;
		mLoginInfo.memberName = "";
		mLoginInfo.memberPwd = pwd;
		mLoginEvent = new LoginEvent(new EventDelegate(this, "LoginComplete"));
		//		NetMgr.DoLogin (loginInfo, mLoginEvent);
		
		//Receive UID(Push Key) then do login
		
		if (Application.platform == RuntimePlatform.Android) {
			AndroidMgr.RegistGCM(new EventDelegate(this, "SetGCMId"));
		} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
			
		} else if(Application.platform == RuntimePlatform.OSXEditor){
			mLoginInfo.memUID = "";
			NetMgr.DoLogin (mLoginInfo, mLoginEvent);
		}
	}

	public void OpenFacebook()
	{
		if(FB.IsLoggedIn)
		{
			InitComplete();
		}
		else
		{
			FB.Init (InitComplete, "saffsg324345ddvd");
			Debug.Log("FB.Login");
			FB.Login ();
		}
	}

	public void InitComplete()
	{
		Debug.Log("InitComplete");
	}

	public void OpenKakao()
	{

	}

	public void OpenEmail()
	{
		transform.FindChild ("ContainerBtns").gameObject.SetActive (false);
		transform.FindChild ("SprLogo").gameObject.SetActive (false);

		transform.FindChild ("WindowEmail").gameObject.SetActive (true);

	}

	public void SetGCMId()
	{
		mLoginInfo.memUID = AndroidMgr.GetMsg();
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
		case "BtnFacebook":
			OpenFacebook();
			break;
		case "BtnKakao":
			OpenKakao();
			break;
		case "BtnEmail":
			OpenEmail();
			break;

		
		}
	}
}
