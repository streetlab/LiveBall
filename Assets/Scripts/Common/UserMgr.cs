using UnityEngine;
using System.Collections;

public class UserMgr : MonoBehaviour {

	static UserMgr _instance;

	UserInfo _userInfo = new UserInfo ();
	ScheduleInfo _schedule;
	
	static UserMgr Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(UserMgr)) as UserMgr;
				Debug.Log("UserMgr is null");
				if (_instance == null)
				{
					GameObject container = new GameObject();  
					container.name = "UserMgr";  
					_instance = container.AddComponent(typeof(UserMgr)) as UserMgr;
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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static UserInfo GetUserInfo()
	{
		return Instance._userInfo;
	}

	public static ScheduleInfo Schedule
	{
		get{return Instance._schedule;}
		set{Instance._schedule = value;}
	}
}
