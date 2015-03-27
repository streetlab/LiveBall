using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseUploadRequest : WWWForm{

	public WWWForm GetRequestWWWForm()
	{
		AddField("type", GetType());
		AddField("id", GetQueryId());

		return this;
	}

	public virtual string GetType(){return null;}
	public virtual string GetQueryId(){return null;}

}
