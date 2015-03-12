using UnityEngine;
using System.Collections;

public class GetCardInvenEvent : BaseEvent {

	public GetCardInvenEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		response = JsonFx.Json.JsonReader.Deserialize<GetCardInvenResponse>(data);

		if (checkError ())
						return;

		eventDelegate.Execute ();
	}

	public GetCardInvenResponse Response
	{
		get{ return response as GetCardInvenResponse;}
	}

}
