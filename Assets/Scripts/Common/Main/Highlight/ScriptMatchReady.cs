using UnityEngine;
using System.Collections;

public class ScriptMatchReady : MonoBehaviour {
	public GameObject itemInfo;
	public GameObject itemPoll;

	// Use this for initialization
	void Start () {
		Transform transformList = transform.FindChild ("List").GetComponent<UIScrollView> ().transform;

		GameObject obj = Instantiate(itemInfo, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		obj.transform.parent = transformList;
		obj.transform.localScale = new Vector3(1f, 1f, 1f);		
		obj.GetComponent<ScriptItemInfoHighlight> ().Init ();

		obj = Instantiate(itemPoll, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		obj.transform.parent = transformList;
		obj.transform.localScale = new Vector3(1f, 1f, 1f);
		obj.transform.localPosition = new Vector3 (0f, -140f);

		transform.FindChild ("List").GetComponent<UIScrollView> ().ResetPosition ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
