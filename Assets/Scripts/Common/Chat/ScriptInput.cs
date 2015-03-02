using UnityEngine;
using System.Collections;

public class ScriptInput : MonoBehaviour {

	UIInput input;
	// Use this for initialization
	void Start () {
		input = this.gameObject.GetComponent<UIInput> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ClearInput(){
		input.value = "";
		input.defaultText = "";
		Debug.Log("ClearInput");

		// 라벨 텍스트를 변경합니다.
//		UIInput input = NGUITools.FindInParents<UIInput>(lbl.gameObject);
//		if (input != null && input.label == lbl) 
//			input.defaultText = val;
//		else 
//			lbl.text = val
	}
}
