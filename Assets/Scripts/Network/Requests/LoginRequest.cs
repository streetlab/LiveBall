using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class LoginRequest : BaseRequest {
//	private string _memberEmail;
//	private string _memberName;
//	private string _memUID;
//	private int _osType;
//	private int _registType;
//	private string _memberPwd;

//	Dictionary<string, object> mDic = new Dictionary<string, object> ();

//	public string memberEmail
//	{
//		get{return _memberEmail;}
//		set{_memberEmail = value;}
//	}
//
//	public string memberName
//	{
//		get{return _memberName;}
//		set{_memberName = value;}
//	}
//
//	public string memUID
//	{
//		get{return _memUID;}
//		set{_memUID = value;}
//	}
//
//	public int osType
//	{
//		get{return _osType;}
//		set{_osType = value;}
//	}
//
//	public int registType
//	{
//		get{return _registType;}
//		set{_registType = value;}
//	}
//
//	public string memberPwd
//	{
//		get{return _memberPwd;}
//		set{_memberPwd = value;}
//	}

	public LoginRequest(LoginInfo loginInfo)
	{
//		new Dictionary<string, object> ();
//		memberEmail = loginInfo.memberEmail;
//		memberName = loginInfo.memberName;
//		memUID = loginInfo.memUID;
//		osType = loginInfo.osType;
//		registType = loginInfo.registType;
//		memberPwd = loginInfo.memberPwd;

		Add ("memberEmail", loginInfo.memberEmail);
		Add ("memberName", loginInfo.memberName);
		Add ("memUID", loginInfo.memUID);
		Add ("osType", loginInfo.osType);
		Add ("registType", loginInfo.registType);
		Add ("memberPwd", loginInfo.memberPwd);

		mParams = JsonFx.Json.JsonWriter.Serialize (this);

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override string GetType ()
	{
		return "apps";
	}

	public override string GetQueryId()
	{
		return "tubyLogin";
	}

}
