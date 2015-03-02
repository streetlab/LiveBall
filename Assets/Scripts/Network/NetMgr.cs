using UnityEngine;
using System.Collections;



public class NetMgr : MonoBehaviour{

	private static NetMgr _instance = null;
	public static NetMgr Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(NetMgr)) as NetMgr;
				if(!_instance)
				{
					GameObject container = new GameObject();
					container.name = "NetMgrContainer";
					_instance = container.AddComponent(typeof(NetMgr)) as NetMgr;
				}
			}
			return _instance;
		}
	}

	IEnumerator webAPIProcess(WWW www, BaseEvent baseEvent)
	{
		yield return www;
		
		if(www.error == null)
		{
			Debug.Log(www.text);
			baseEvent.Init(www.text);
		}
		else
		{
			Debug.Log(www.error);
		}
	}

	private void webAPIProcessEvent(BaseRequest request, BaseEvent baseEvent)
	{
		string reqParam = null;
		string httpUrl = "";
		if (request != null) {
			reqParam = request.ToRequestString();
			httpUrl = (Constants.QUERY_SERVER_HOST + reqParam);
		} else {
			httpUrl = Constants.QUERY_SERVER_HOST;
		}

		WWW www = new WWW (httpUrl);

		Debug.Log (httpUrl);
		StartCoroutine (webAPIProcess(www, baseEvent));
	}

	public static void DoLogin(LoginInfo loginInfo, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new LoginRequest(loginInfo), baseEvent);
	}

	public static void GetScheduleMore(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetScheduleMoreRequest(), baseEvent);
	}

}
