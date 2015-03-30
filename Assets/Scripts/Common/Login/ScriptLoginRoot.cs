using UnityEngine;
using System.Collections;

public class ScriptLoginRoot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("UtilMgr.GetScaledPositionY : " + UtilMgr.GetScaledPositionY());
		transform.FindChild ("Camera").transform.localPosition = new Vector3(0, UtilMgr.GetScaledPositionY(), 0);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnBackPressed()
	{
		UtilMgr.OnBackPressed ();
	}
}
