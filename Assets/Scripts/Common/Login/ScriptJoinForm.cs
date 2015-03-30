using UnityEngine;
using System.Collections;

public class ScriptJoinForm : MonoBehaviour {

//	GetProfileEvent mEvent;
	JoinMemberInfo mMemInfo;
	string mImgPath;

	public void Init(string eMail, string pwd, bool pwdEnable)
	{
		transform.FindChild ("InputEmail").GetComponent<UIInput> ().value = eMail;
		transform.FindChild ("InputPwd").GetComponent<UIInput> ().value = pwd;
		transform.FindChild ("InputPwd").GetComponent<UIInput> ().enabled = pwdEnable;
	}

	public void CameraClicked()
	{
		if(Application.platform == RuntimePlatform.Android){
			//need selection window
			AndroidMgr.OpenGallery(new EventDelegate(this, "GotUserImg"));
		} else{

		}
	}

	public void GotUserImg()
	{
		string path = AndroidMgr.GetMsg ();
		//here we go!!!!
	}

	public void BackClicked()
	{
		UtilMgr.OnBackPressed ();
	}

	public void NextClicked()
	{
		//need joinInfoClass
//		transform.FindChild("InputEmail").GetComponent<UILabel>().text
		string value = CheckValidation ();
		if (value == null) {
			mMemInfo = new JoinMemberInfo();
			mMemInfo.MemberEmail = transform.FindChild ("InputEmail").GetComponent<UIInput> ().value;
			mMemInfo.MemberName = transform.FindChild ("InputNick").GetComponent<UIInput> ().value;
			mMemInfo.MemberPwd = transform.FindChild ("InputPwd").GetComponent<UIInput> ().value;
			mMemInfo.MemImage = "";//preprocess
			mMemInfo.Photo = mImgPath;
			#if(UNITY_ANDROID)
			mMemInfo.OsType = 1;
			AndroidMgr.RegistGCM(new EventDelegate(this, "CompleteGCM"));
			#else
			memInfo.OsType = 2;
			#endif


		} else
		{
			Debug.Log(value);
		}
	}

	public void CompleteGCM()
	{
		Debug.Log ("CompleteGCM");
		string memUID = "";
		#if(UNITY_ANDROID)
		memUID = AndroidMgr.GetMsg();
		#else
		#endif
		mMemInfo.MemUID = memUID;
		GetComponentInParent<ScriptTitle>().mProfileEvent = 
			new GetProfileEvent(new EventDelegate(GetComponentInParent<ScriptTitle>(), "GotProfile"));

		NetMgr.JoinMember(mMemInfo, GetComponentInParent<ScriptTitle>().mProfileEvent);
	}

//	public void JoinComplete()
//	{
//		Debug.Log (mEvent.Response.data.memberEmail);
//	}

	string CheckValidation()
	{
		string value = transform.FindChild ("InputEmail").GetComponent<UIInput> ().value;
		if (value.Length > 4
				&& value.Contains ("@")
				&& value.Contains (".")) {

		} else
		{
			return transform.GetComponent<PlayMakerFSM> ().FsmVariables.FindFsmString ("StrEmailError").Value;
		}

		value = transform.FindChild("InputPwd").GetComponent<UIInput> ().value;
		if(value.Length < 4)
			return transform.GetComponent<PlayMakerFSM> ().FsmVariables.FindFsmString ("StrPwdError").Value;

		value = transform.FindChild("InputNick").GetComponent<UIInput> ().value;
		if(value.Length < 2)
			return transform.GetComponent<PlayMakerFSM> ().FsmVariables.FindFsmString ("StrNickError").Value;

		return null;

	}

	public void OpenCamera()
	{
		//need commonPopup
		#if(UNITY_ANDROID)
		AndroidMgr.OpenGallery(new EventDelegate(this, "GotImage"));
		#else

		#endif
	}

	public void GotImage()
	{
		#if(UNITY_ANDROID)
		mImgPath = AndroidMgr.GetMsg();
		#else
		
		#endif

		WWW www = new WWW ("file://"+mImgPath);
		StartCoroutine (LoadImage (www));

	}

	IEnumerator LoadImage(WWW www)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D(0,0);
		www.LoadImageIntoTexture (tmpTex);
		transform.FindChild ("PanelPhoto").FindChild ("TexPhoto").GetComponent<UITexture> ().mainTexture = tmpTex;
	}
}
