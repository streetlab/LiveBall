using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptTitle : MonoBehaviour {

	LoginEvent mLoginEvent;
	public GetProfileEvent mProfileEvent;
	GetCardInvenEvent mCardEvent;
	public LoginInfo mLoginInfo;

	public WebViewObject webViewObject;

	bool mFBInitialized;

	void Start()
	{
//		PlayerPrefs.SetString (Constants.PrefEmail, "");
//		PlayerPrefs.SetString (Constants.PrefPwd, "");
		Init ();
//		transform.FindChild ("Fire Spray").GetComponent<ParticleSystem> ().renderer.material.renderQueue = 3001;
//		transform.FindChild ("Fire Spray").FindChild("Smoke").GetComponent<ParticleSystem> ().renderer.material.renderQueue = 3002;
//		transform.FindChild ("Fire Spray").FindChild("Sparkles").GetComponent<ParticleSystem> ().renderer.material.renderQueue = 3001;
	}

	public void Init()
	{
		transform.FindChild ("ContainerBtns").localPosition = new Vector3(0, UtilMgr.GetScaledPositionY()*2, 0);
		
		transform.FindChild ("ContainerBtns").gameObject.SetActive (false);
		transform.FindChild ("WindowEmail").gameObject.SetActive (false);
		transform.FindChild ("FormJoin").gameObject.SetActive (false);
		transform.FindChild ("ContainerBtns").gameObject.SetActive (false);
		transform.FindChild ("SelectTeam").gameObject.SetActive (false);

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
//		UtilMgr.ShowLoading (true);

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

	public void FBReceived(){
		string jsonStr = AndroidMgr.GetMsg ();
		Dictionary<string, string> dic = new Dictionary<string, string>();
		dic = JsonFx.Json.JsonReader.Deserialize<Dictionary<string, string>> (jsonStr);

		foreach (string key in dic.Keys) {
			Debug.Log(key+" : "+dic[key]);
		}
	}

	public void OpenFacebook()
	{
		#if(UNITY_EDITOR)

		#elif(UNITY_ANDROID)
		AndroidMgr.OpenFB(new EventDelegate(this, "FBReceived"));
		#else

		#endif
//		if (mFBInitialized) {
//			FBLogin();
//		} else{
//			FB.Init (SetInit, FB.OnHideUnity);
//		}
			
	}

	void SetInit()
	{
		Debug.Log("SetInit");
		enabled = true;
		mFBInitialized = true;
	}

//	void FBLogin()
//	{
//		if(FB.IsLoggedIn)
//		{
//			Debug.Log("FB.IsLoggedIn");
//			OnLoggedIn();
//		}
//		else
//		{
//			Debug.Log("FB.Login");
//			FB.Login("email,publish_actions", LoginCallback);
//		}
//	}

//	void LoginCallback(FBResult result)
//	{
//		if (result.Error != null)
//			Debug.Log ("Error Response:\n" + result.Error);
//		else if (!FB.IsLoggedIn)
//		{
//			Debug.Log ("Login cancelled by Player");
//		}
//		else
//		{
//			Debug.Log ("Login was successful!");
//		}
//	}

//	void OnLoggedIn()
//	{
//		DialogueMgr.ShowDialogue ("", FB.UserId + "", DialogueMgr.DIALOGUE_TYPE.Alert, null, null, null);
//		FB.API("/me?fields=id,first_name,friends."
//		FB.API ("/me/picture?type=large", Facebook.HttpMethod.GET, APICallback);
//		StartCoroutine (GetPicture());
//	}

//	IEnumerator GetPicture()
//	{
//		WWW www = new WWW("http://graph.facebook.com/" + FB.UserId + "/picture");
//		yield return www;
//		Texture2D texture = new Texture2D (0, 0);
//		www.LoadImageIntoTexture(texture);
//		transform.FindChild ("Texture").GetComponent<UITexture> ().mainTexture = texture; 
//	}

//	void APICallback(FBResult result)
//	{
//		if (result.Error != null)
//						Debug.Log ("AIP Error Response:\n" + result.Error);
//				else
//						transform.FindChild ("Texture").GetComponent<UITexture> ().mainTexture = result.Texture;
//			
//	}

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
		UtilMgr.DismissLoading ();
		if (mLoginEvent.Response.code > 0) {
			Debug.Log("error : "+mLoginEvent.Response.message);
			if(mLoginEvent.Response.code == 100){
				LoginFailed();
			}
			UtilMgr.DismissLoading ();
			return;
		}
		mLoginInfo = mLoginEvent.Response.data;
		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "GotProfile"));

		NetMgr.GetProfile (mLoginInfo.memSeq, mProfileEvent);
	}

	void LoginFailed()
	{
		PlayerPrefs.SetString(Constants.PrefEmail, "");
		PlayerPrefs.SetString(Constants.PrefPwd, "");
		UtilMgr.RemoveAllBackEvents();
		Init ();
		string title = gameObject.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("loginFailedTitle").Value;
		string body = gameObject.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("loginFailedBody").Value;
		DialogueMgr.ShowDialogue(
		                         title, body, DialogueMgr.DIALOGUE_TYPE.Alert, "", "", "");
		UtilMgr.SetBackEvent (new EventDelegate (transform.root.GetComponent<ScriptLoginRoot>(), "DismissDialogue"));
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
