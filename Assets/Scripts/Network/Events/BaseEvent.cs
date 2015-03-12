using UnityEngine;
using System.Collections;

public class BaseEvent {

	BaseResponse _response;
	EventDelegate _eventDelegate = null;

	protected delegate void InitDelegate (string data);
	protected event InitDelegate InitEvent;

	protected bool checkError()
	{
		if (response.code > 0) {
			Debug.Log("Response Error : " + response.message);
			return true;
		}
		return false;
	}

	protected EventDelegate eventDelegate
	{
		get{return _eventDelegate;}
		set{_eventDelegate = value;}
	}

	protected BaseResponse response
	{
		get{return _response;}
		set{_response = value;}
	}

	public void Init(string data)
	{
		InitEvent (data);
	}

}
