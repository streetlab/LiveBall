using UnityEngine;
using System.Collections;

public class GetQuizEvent : BaseEvent {

	public GetQuizEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		response = JsonFx.Json.JsonReader.Deserialize<GetQuizResponse>(data);

		eventDelegate.Execute ();
	}

	public GetQuizResponse GetResponse()
	{
		return response as GetQuizResponse;
	}

}
