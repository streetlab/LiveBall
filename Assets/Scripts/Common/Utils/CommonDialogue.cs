using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommonDialogue : MonoBehaviour {
	static CommonDialogue _instance = null;

//	public GameObject gameObject;
	UILabel label;
	UIButton BtnLeft;
	UIButton BtnRight;
	static int value = 0;
	GameObject obj = null;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	private static CommonDialogue Instance
	{
		get
		{
			if (_instance == null)
			{
				// 현재 씬 내에서 CommonDialogue 컴포넌트를 검색
				_instance = FindObjectOfType(typeof(CommonDialogue)) as CommonDialogue;
				Debug.Log("is null");
				if (_instance == null)
				{
					// 현재 씬에 CommonDialogue 컴포넌트가 없으면 새로 생성
					GameObject container = new GameObject();  
					container.name = "CommonDialogue";  
					_instance = container.AddComponent(typeof(CommonDialogue)) as CommonDialogue;
					Debug.Log("and makes new one");

				}
			}
			
			return _instance;
		}
	}

	public void dismiss()
	{
		if (obj != null)
						obj.SetActive (false);
	}

	public static void Show(string str)
	{
		Instance.show (str);
	}

	void show(string str){
		if (value == 0) {
			GameObject prefab = Resources.Load ("Common Dialogue") as GameObject;
			obj = Instantiate (prefab, new Vector3 (1f, 1f, 1f), Quaternion.identity) as GameObject;
			Debug.Log("Makes a new object");
			value = 2;
		}
		obj.SetActive (true);

		UILabel label = GetChildObj (obj, "Label").GetComponent<UILabel>(); 
		label.text = str;
	}

	/** 객체의 이름을 통하여 자식 요소를 찾아서 리턴하는 함수 */
	GameObject GetChildObj( GameObject source, string strName  ) { 
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
	
	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
	}

}