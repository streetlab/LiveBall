using UnityEngine;
using System.Collections;

public class JoinQuizEvent : BaseEvent {

	public JoinQuizEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		Debug.Log ("JoinQuizResponse : " + data);
		response = JsonFx.Json.JsonReader.Deserialize<BaseResponse>(data);

		if (checkError ())
			return;

		eventDelegate.Execute ();
	}

}
