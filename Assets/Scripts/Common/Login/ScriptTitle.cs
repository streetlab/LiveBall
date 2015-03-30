using UnityEngine;
using System.Collections;

public class ScriptTitle : MonoBehaviour {

	LoginEvent mLoginEvent;
	public GetProfileEvent mProfileEvent;
	GetCardInvenEvent mCardEvent;
	public LoginInfo mLoginInfo;

	void Start()
	{
		Init ();
	}

	public void Init()
	{
		transform.FindChild ("ContainerBtns").localPosition = new Vector3(0, UtilMgr.GetScaledPositionY()*2, 0);
		
		transform.FindChild ("ContainerBtns").gameObject.SetActive (false);
		transform.FindChild ("WindowEmail").gameObject.SetActive (false);
		transform.FindChild ("FormJoin").gameObject.SetActive (false);
		transform.FindChild ("ContainerBtns").gameObject.SetActive (false);

		transform.FindChild ("SprLogo").gameObject.SetActive (true);
		
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
		mLoginInfo = new LoginInfo ();
		mLoginInfo.memberEmail = eMail;
		mLoginInfo.memberName = "";
		mLoginInfo.memberPwd = pwd;
		mLoginEvent = new LoginEvent(new EventDelegate(this, "LoginComplete"));

		PlayerPrefs.SetString (Constants.PrefEmail, eMail);
		PlayerPrefs.SetString (Constants.PrefPwd, pwd);
		
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
		if (mLoginEvent.Response.code > 0) {
			Debug.Log("error : "+mLoginEvent.Response.message);
			if(mLoginEvent.Response.code == 100){
				PlayerPrefs.DeleteKey(Constants.PrefEmail);
				PlayerPrefs.DeleteKey(Constants.PrefPwd);
				UtilMgr.RemoveAllBackEvents();
				Init ();
			}

			return;
		}
		mLoginInfo = mLoginEvent.Response.data;
		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "GotProfile"));

		NetMgr.GetProfile (mLoginInfo.memSeq, mProfileEvent);
	}

	public void GotProfile()
	{
		UserMgr.UserInfo = mProfileEvent.Response.data;
		if (mLoginInfo != null) {
			UserMgr.UserInfo.teamCode = mLoginInfo.teamCode;
			UserMgr.UserInfo.teamSeq = mLoginInfo.teamSeq;
		}
		Debug.Log ("GotProfile");
		mCardEvent = new GetCardInvenEvent (new EventDelegate (this, "GotCardInven"));
		NetMgr.GetCardInven (mCardEvent);
	}

	public void GotCardInven()
	{
		Debug.Log ("GotCardInven");

		UtilMgr.RemoveAllBackEvents ();
		AutoFade.LoadLevel ("SceneTeamHome", 0f, 1f);
	}


	public void BtnClicked(string name)
	{
		UtilMgr.SetBackEvent (new EventDelegate (this, "Init"));
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
