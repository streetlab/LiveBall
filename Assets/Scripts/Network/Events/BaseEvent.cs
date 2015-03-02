using UnityEngine;
using System.Collections;

public class BaseEvent : MonoBehaviour{

	BaseResponse _response;
	EventDelegate _eventDelegate = null;

	protected delegate void InitDelegate (string data);
	protected event InitDelegate InitEvent;

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

//
//	public Object data;
//	public string message;
//	public int code;
//	public int result;
//
//	public void Init(string data)
//	{
//		//do Parsing
//
//		//call Event
//		if (_eventDelegate != null)
//				_eventDelegate.Execute ();
//	}

}
