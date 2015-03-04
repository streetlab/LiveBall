using UnityEngine;
using System.Collections;

public class ScriptTeamHomeRoot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.FindChild ("Camera").localPosition = new Vector3 (0, UtilMgr.GetScaledPositionY (), 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
