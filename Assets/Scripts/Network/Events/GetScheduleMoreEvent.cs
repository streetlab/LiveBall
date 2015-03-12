using UnityEngine;
using System.Collections;

public class GetScheduleMoreEvent : BaseEvent {

	public GetScheduleMoreEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		response = JsonFx.Json.JsonReader.Deserialize<GetScheduleMoreResponse>(data);

		if (checkError ())
			return;

		eventDelegate.Execute ();
	}

	public GetScheduleMoreResponse Response
	{
		get{ return response as GetScheduleMoreResponse;}
	}

}
