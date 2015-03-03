using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseRequest : Dictionary<string, object>{
//	protected JSONObject json = new JSONObject();
//	Dictionary<string, string> mDic = new Dictionary<string, string>();
	protected string mParams;

	public string ToRequestString()
	{
//		string str = "?";
		string str = "";
		str += "param=" + mParams;
		str += "&type="+GetType();
		str += "&id=" + GetQueryId ();

		return str;
	}

	public virtual string GetType(){return null;}
	public virtual string GetQueryId(){return null;}

}
