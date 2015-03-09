using UnityEngine;
using System.Collections;

public class ScriptMatchArrowItem : MonoBehaviour {

	public void OnCenter()
	{
		transform.parent.GetComponent<ScriptMatchItem> ().OnCenter ();
	}
}
