using UnityEngine;
using System.Collections;

public class ScriptTimeline : MonoBehaviour {

	public GameObject mSearch;
	public GameObject mMatch;
	public GameObject mWritten;
	public GameObject mUpload;
	public GameObject mSelection;
	public GameObject mLink;
	Transform mContainerBtns;

	// Use this for initialization
	void Start () {
		mContainerBtns = transform.FindChild ("ContainerBtns");
		mContainerBtns.localPosition = new Vector3 (0, UtilMgr.GetScaledPositionY () * 2f, 0);
		UtilMgr.ResizeList (transform.FindChild ("ListTimeline").gameObject);
		transform.FindChild ("ListTimeline").GetComponent<UIScrollView> ().ResetPosition ();
//		Vector3 offset = transform.FindChild ("ListTimeline").localPosition;
//		offset.y += UtilMgr.GetScaledPositionY () ;
//		transform.FindChild ("ListTimeline").localPosition = new Vector3 (0, offset.y, 0);
//		transform.FindChild ("ListTimeline").GetComponent<UIPanel> ().baseClipRegion = new Vector4 (0, 0, 720f, 690f - UtilMgr.GetScaledPositionY ()*2);
		CloseMenu ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenMenu()
	{
		mContainerBtns.FindChild("BtnPlus").gameObject.SetActive(false);
		mContainerBtns.FindChild("BtnClose").gameObject.SetActive(true);
		mContainerBtns.FindChild("BtnLink").gameObject.SetActive(true);
		mContainerBtns.FindChild("BtnCamera").gameObject.SetActive(true);
		mContainerBtns.FindChild("BtnWrite").gameObject.SetActive(true);
	}
	
	public void CloseMenu()
	{
		mContainerBtns.FindChild("BtnPlus").gameObject.SetActive(true);
		mContainerBtns.FindChild("BtnClose").gameObject.SetActive(false);
		mContainerBtns.FindChild("BtnLink").gameObject.SetActive(false);
		mContainerBtns.FindChild("BtnCamera").gameObject.SetActive(false);
		mContainerBtns.FindChild("BtnWrite").gameObject.SetActive(false);
	}
	
	void OpenWriteWindow()
	{
		mSearch.SetActive (false);
		mMatch.SetActive (false);
		gameObject.SetActive (false);
		mUpload.SetActive (false);
		
		Transform transformWritten = mWritten.transform;
		mWritten.SetActive (true);
		UIInput uiInputInputBody = transformWritten.FindChild ("InputBody").gameObject.GetComponent<UIInput> ();
		
		TouchScreenKeyboard.hideInput = true;
	}
	
	void CloseWriteWindow()
	{
		
	}
	
	public void BtnClicked(string name)
	{
		switch(name)
		{
		case "BtnPlus":
//			OpenMenu();
			break;
		case "BtnClose":
			CloseMenu();
			break;
		case "BtnWrite":
//			OpenWriteWindow();
			break;
		case "BtnCamera":
			
			break;
		case "BtnLink":
			
			break;
		}
	}
}
