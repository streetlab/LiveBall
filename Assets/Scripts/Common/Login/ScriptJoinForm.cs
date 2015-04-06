using UnityEngine;
using System.Collections;

public class ScriptJoinForm : MonoBehaviour {

	public GameObject mSelectTeam;
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
		string value = CheckValidation ();
		if (value == null) {
			mMemInfo = new JoinMemberInfo();
			mMemInfo.MemberEmail = transform.FindChild ("InputEmail").GetComponent<UIInput> ().value;
			mMemInfo.MemberName = transform.FindChild ("InputNick").GetComponent<UIInput> ().value;
			mMemInfo.MemberPwd = transform.FindChild ("InputPwd").GetComponent<UIInput> ().value;
			mMemInfo.MemImage = "";//preprocess
			mMemInfo.Photo = mImgPath;

			gameObject.SetActive(false);
			mSelectTeam.GetComponent<ScriptSelectTeam>().Init(mMemInfo);
		} else
		{
			DialogueMgr.ShowDialogue("join error", value, DialogueMgr.DIALOGUE_TYPE.Alert, null, null, null);
		}
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
		#if(UNITY_EDITOR)
		#elif(UNITY_ANDROID)
		AndroidMgr.OpenGallery(new EventDelegate(this, "GotImage"));
		#else

		#endif
	}

	public void GotImage()
	{
		#if(UNITY_EDITOR)
		#elif(UNITY_ANDROID)
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
