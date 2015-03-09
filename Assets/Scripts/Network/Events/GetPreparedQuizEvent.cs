using UnityEngine;
using System.Collections;

public class GetPreparedQuizEvent : BaseEvent {

	public GetPreparedQuizEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		response = JsonFx.Json.JsonReader.Deserialize<GetPreparedQuizResponse>(data);

		eventDelegate.Execute ();
	}

	public GetPreparedQuizResponse GetResponse()
	{
		return response as GetPreparedQuizResponse;
	}

}
