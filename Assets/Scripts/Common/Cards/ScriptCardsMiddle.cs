using UnityEngine;
using System.Collections;

public class ScriptCardsMiddle : MonoBehaviour {

	GetCardInvenEvent mEvent;

	void Start () {
		mEvent = new GetCardInvenEvent(new EventDelegate(this, "GotCardsInven"));
		NetMgr.GetCardInven (mEvent);
	}

	public void GotCardsInven()
	{
		Debug.Log ("GotCardsInven : "+mEvent.Response.data.cardClass.Count);
	}
}
