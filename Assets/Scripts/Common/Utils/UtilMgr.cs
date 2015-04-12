using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UtilMgr : MonoBehaviour {

	static UtilMgr _instance;

	static List<EventDelegate> mListBackEvent = new List<EventDelegate>();
	GameObject mProgressCircle;

	public static bool IsUntouchable;

	static UtilMgr Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(UtilMgr)) as UtilMgr;
				Debug.Log("UtilMgr is null");
				if (_instance == null)
				{
					GameObject container = new GameObject();  
					container.name = "UtilMgr";  
					_instance = container.AddComponent(typeof(UtilMgr)) as UtilMgr;
					Debug.Log("and makes new one");
					
				}
			}
			
			return _instance;
		}
	}

	void Awake()
	{
		DontDestroyOnLoad (this);
	}

	public static void SetBackEvent(EventDelegate eventDel)
	{
		mListBackEvent.Add (eventDel);
	}

	public static void RemoveAllBackEvents()
	{
		mListBackEvent.Clear ();
	}

	public static void RemoveBackEvent()
	{
		mListBackEvent.RemoveAt(mListBackEvent.Count-1);
	}

	public static bool HasBackEvent()
	{
		if (mListBackEvent.Count > 0)
						return true;
		return false;
	}

	public static void RunAllBackEvents()
	{
		if (mListBackEvent.Count < 1)
						return;

		for(int i = mListBackEvent.Count-1; i > -1; i = mListBackEvent.Count-1){
			EventDelegate eventDel = mListBackEvent[i];
			RemoveBackEvent();
			eventDel.Execute();
		}
	}

	public static bool OnBackPressed()
	{
		if (UtilMgr.IsUntouchable)
			return false;

		if(mListBackEvent.Count > 0)
		{
			EventDelegate eventDel = mListBackEvent[mListBackEvent.Count-1];
			RemoveBackEvent();
			eventDel.Execute();
			return true;
		}
		else
		{
			DialogueMgr.ShowExitDialogue();
			return false;
		}
	}

	public static void ResizeList(GameObject go)
	{
		Vector3 offset3 = go.transform.localPosition;
		offset3.y += UtilMgr.GetScaledPositionY () ;
		go.transform.localPosition = new Vector3 (0, offset3.y, 0);
		Vector4 offset4 = go.GetComponent<UIPanel> ().baseClipRegion;
		offset4.w -= UtilMgr.GetScaledPositionY () * 2;
		go.GetComponent<UIPanel> ().baseClipRegion = new Vector4 (0, 0, 720f, offset4.w);
	}

	public static float GetScaledPositionY()
	{
		float height = (float)Screen.height;
		float width = (float)Screen.width;
		float ratio = height / width;
		float diff = Constants.DEFAULT_SCR_RATIO - ratio;
//		Debug.Log (""+360f * diff);

		return 360f * diff;
	}

	public static string AddsThousandsSeparator(string number)
	{
		return AddsThousandsSeparator (double.Parse (number));
	}

	public static string AddsThousandsSeparator(int number)
	{
		return string.Format ("{0:n0}", number);
	}

	public static string AddsThousandsSeparator(double number)
	{
		return string.Format ("{0:n0}", number);
	}

	/** "yyyy-MM-dd HH:mm:ss" */
	public static string GetDateTime(string expression)
	{
		return System.DateTime.Now.ToString (expression);
	}
	/** "20150225182000"  */
	public static string ConvertToDate(string timeStr)
	{
		string year = timeStr.Substring (0, 4);
		string month = timeStr.Substring (4, 2);
		string day = timeStr.Substring (6, 2);
		int nTime = int.Parse(timeStr.Substring (8, 2));
		string minute = timeStr.Substring (10, 2);
		string time;
		if(nTime > 11)
		{
			time = "오후 ";
			if(nTime > 12)
			{
				nTime -= 12;
			}
			time += nTime+":";
		}
		else
		{
			time = "오전 ";
			time += nTime+":";
		}
		string final = year + ". " + month + ". " + day + " " + time + minute;
		return final;
	}

	public static string GetTeamEmblem(string imgName)
	{
		switch(imgName)
		{
		case "sports_team_baseball_lg.png":
			return "ic_lg";
		case "sports_team_baseball_lt.png":
			return "ic_lotte";
		case "sports_team_baseball_hh.png":
			return "ic_hanwha";
		case "sports_team_baseball_ob.png":
			return "ic_doosan";
		case "sports_team_baseball_ht.png":
			return "ic_kia";
		case "sports_team_baseball_ss.png":
			return "ic_samsung";
		case "sports_team_baseball_wo.png":
			return "ic_nexen";
		case "sports_team_baseball_sk.png":
			return "ic_sk";
		case "sports_team_baseball_nc.png":
			return "ic_nc";
		case "sports_team_baseball_kt.png":
			return "ic_kt";
		}
		return null;
	}

//	public static string GetTeamEmblem(string teamCode)
//	{
//		switch(teamCode)
//		{
//		case "SS":
//			return "ic_samsung";
//		case "WO":
//			return "ic_nexen";
//		}
//		return null;
//	}

	public static string GetRoundString(int round)
	{

		if(round == 1)
		{
			return "ST";
		}
		else if(round == 2)
		{
			return "ND";
		}
		else if(round == 3)
		{
			return "RD";
		}
		else
		{
			return "TH";
		}
	}

	/** 객체의 이름을 통하여 자식 요소를 찾아서 리턴하는 함수 */
	public static GameObject GetChildObj( GameObject source, string strName) { 
		Transform[] AllData = source.GetComponentsInChildren< Transform >(); 
		GameObject target = null;
		
		foreach( Transform Obj in AllData ) { 
			if( Obj.name == strName ) { 
				target = Obj.gameObject;
				break;
			} 
		}
		
		return target;
	}

	public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
	{
		Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
		Color[] rpixels = result.GetPixels(0);
		float incX = (1.0f / (float)targetWidth);
		float incY = (1.0f / (float)targetHeight);
		for (int px = 0; px < rpixels.Length; px++)
		{
			rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
		}
		result.SetPixels(rpixels, 0);
		result.Apply();
		Destroy (source);
//		System.GC.Collect ();
		return result;
	}

	public static void ShowLoading(bool unTouchable)
	{
		if (Instance.mProgressCircle == null) {
			GameObject prefab = Resources.Load ("ProgressCircle1") as GameObject;
			Instance.mProgressCircle = Instantiate (prefab, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
			Instance.mProgressCircle.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			Instance.mProgressCircle.transform.localPosition = new Vector3(0, 0, 0);
		}

		Instance.mProgressCircle.transform.parent = GameObject.Find ("UI Root").transform;
		Instance.mProgressCircle.SetActive (true);


		UtilMgr.IsUntouchable = unTouchable;
	}

	public static void DismissLoading()
	{
		if(Instance.mProgressCircle != null)
			Instance.mProgressCircle.SetActive (false);

		UtilMgr.IsUntouchable = false;
	}
}
