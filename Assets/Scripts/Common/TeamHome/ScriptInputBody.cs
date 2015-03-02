using UnityEngine;
using System.Collections;

public class ScriptInputBody : MonoBehaviour {
	UIInput mInput;
	int mKeyboardHeight;
	Transform mLineOnKeyTop;

	// Use this for initialization
	void Start () {
		mInput = gameObject.GetComponent<UIInput> ();
		mLineOnKeyTop = transform.parent.FindChild ("LineOnKeyTop");
	}
	
	// Update is called once per frame
	void Update () {
//		CommonDialogue.Instance.show ("height : " + TouchScreenKeyboard.area.height + ", width : " + TouchScreenKeyboard.area.width);
//		AndroidMgr.Instance.CallJavaFunc ("CallActiveWindowSize", "", this);
//		mLineOnKeyTop.position = new Vector3(0, Screen.height
//		CommonDialogue.Instance.show ("screen height : " + Screen.height+", keyboard height : "+mKeyboardHeight);
	}

	public void SetKeyboardHeight(int height)
	{
		mKeyboardHeight = height;
	}
	
}
