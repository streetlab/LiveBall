using UnityEngine;
using System.Collections;

public class ScriptJoinForm : MonoBehaviour {

	GetProfileEvent mEvent;

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

	}

	public void NextClicked()
	{
		//need joinInfoClass
//		transform.FindChild("InputEmail").GetComponent<UILabel>().text
		string value = CheckValidation ();
		if (value == null) {
			JoinMemberInfo memInfo = new JoinMemberInfo();
			memInfo.MemberEmail = transform.FindChild ("InputEmail").GetComponent<UIInput> ().value;
			memInfo.MemberName = transform.FindChild ("InputNick").GetComponent<UIInput> ().value;
			memInfo.MemberPwd = transform.FindChild ("InputPwd").GetComponent<UIInput> ().value;
#if(UNITY_ANDROID)
			memInfo.OsType = 1;
#else
			memInfo.OsType = 2;
#endif
			memInfo.MemUID = "";//coroutine
			memInfo.MemImage = "";//preprocess

			mEvent = new GetProfileEvent(new EventDelegate(this, "JoinComplete"));
			NetMgr.JoinMember(memInfo, mEvent);
		} else
		{
			Debug.Log(value);
		}
	}

	public void JoinComplete()
	{
		Debug.Log (mEvent.Response.data.memberEmail);
	}

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
}
