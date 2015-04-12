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
		UtilMgr.ShowLoading (true);

		yield return www;
		
		if(www.error == null)
		{
			Debug.Log(www.text);
//			CommonDialogue.Show (www.text);
			baseEvent.Init(www.text);
		}
		else
		{
			Debug.Log(www.error);
		}

		UtilMgr.DismissLoading ();
	}

	private void webAPIUploadProcessEvent(BaseUploadRequest request, BaseEvent baseEvent)
	{	
		WWWForm form = request.GetRequestWWWForm ();
		WWW www = new WWW (Constants.QUERY_SERVER_HOST , form);

		StartCoroutine (webAPIProcess(www, baseEvent));
	}

	private void webAPIProcessEvent(BaseRequest request, BaseEvent baseEvent)
	{
		string reqParam = "";
		string httpUrl = "";
		if (request != null) {
			reqParam = request.ToRequestString();
//			httpUrl = (Constants.QUERY_SERVER_HOST + reqParam);
//			httpUrl = reqParam;
		} else {
//			httpUrl = Constants.QUERY_SERVER_HOST;
		}

		WWW www = new WWW (Constants.QUERY_SERVER_HOST , System.Text.Encoding.UTF8.GetBytes(reqParam));

		Debug.Log (reqParam);
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

	public static void GetGameSposDetailBoard(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetGameSposDetailBoardRequest (), baseEvent);
	}

	public static void GetGameSposPlayBoard(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetGameSposPlayBoardRequest (), baseEvent);
	}

	public static void GetCardInven(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetCardInvenRequest (), baseEvent);
	}

	public static void GetPreparedQuiz(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetPreparedQuizRequest (), baseEvent);
	}

	public static void GetProgressQuiz(int quizListSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetProgressQuizRequest (quizListSeq), baseEvent);
	}

	public static void GetProfile(int memSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetProfileRequest (memSeq), baseEvent);
	}

	public static void JoinGame(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new JoinGameRequest (), baseEvent);
	}

	public static void JoinQuiz(JoinQuizInfo joinInfo, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new JoinQuizRequest (joinInfo), baseEvent);
	}

	public static void GetQuizResult(int quizListSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetQuizResultRequest (quizListSeq), baseEvent);
	}

	public static void GetSimpleResult(int quizListSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetSimpleResultRequest (quizListSeq), baseEvent);
	}

	public static void JoinMember(JoinMemberInfo memInfo, BaseEvent baseEvent)
	{
		Instance.webAPIUploadProcessEvent (new JoinMemberRequest (memInfo), baseEvent);
	}
}
