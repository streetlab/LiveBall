using UnityEngine;
using System.Collections;

public class ScriptTFProfile : MonoBehaviour {

	public GameObject mListProfile;
	public GameObject mProfile;
	public GameObject mPoint;
	public GameObject mMessages;
	public GameObject mLogs;

	// Use this for initialization
	void Start () {
		mProfile.SetActive (true);
		mPoint.SetActive (true);
		mMessages.SetActive (true);
		mLogs.SetActive (true);

		mProfile.transform.localPosition = new Vector3 (0f, 0f, 0f);
		float height = mProfile.transform.FindChild ("SprBG").GetComponent<UISprite> ().height + 70f;
		mPoint.transform.localPosition = new Vector3 (0f, -height, 0f);
		height += mPoint.transform.FindChild ("SprBG").GetComponent<UISprite> ().height + 200f;
		mMessages.transform.localPosition = new Vector3 (0f, -height, 0f);
		height += mMessages.transform.FindChild ("SprBG").GetComponent<UISprite> ().height + 80f;
		mLogs.transform.localPosition = new Vector3 (0f, -height, 0f);

		mListProfile.GetComponent<UIScrollView> ().ResetPosition ();
	}
	

}
