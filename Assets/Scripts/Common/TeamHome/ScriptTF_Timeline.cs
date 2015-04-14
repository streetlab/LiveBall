using UnityEngine;
using System.Collections;

public class ScriptTF_Timeline : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.FindChild("Written").gameObject.SetActive(false);
		transform.FindChild("Upload").gameObject.SetActive(false);
		transform.FindChild("Selection").gameObject.SetActive(false);
		transform.FindChild("Link").gameObject.SetActive(false);

//		transform.FindChild("Search").gameObject.SetActive(true);
		transform.FindChild("Match").gameObject.SetActive(true);
		transform.FindChild("Timeline").gameObject.SetActive(true);
	}

}
