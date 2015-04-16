using UnityEngine;
using System.Collections;

public class ScriptTFItems : MonoBehaviour {

	public GameObject mListItems;

	// Use this for initialization
	void Start () {
		mListItems.transform.GetComponent<UIScrollView> ().ResetPosition ();
	}
}
