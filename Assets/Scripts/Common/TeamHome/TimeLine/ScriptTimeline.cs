using UnityEngine;
using System.Collections;

public class ScriptTimeline : MonoBehaviour {

	public GameObject mSearch;
	public GameObject mMatch;
	public GameObject mWritten;
	public GameObject mUpload;
	public GameObject mSelection;
	public GameObject mLink;

	// Use this for initialization
	void Start () {
		CloseMenu ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenMenu()
	{
		transform.FindChild("BtnPlus").gameObject.SetActive(false);
		transform.FindChild("BtnClose").gameObject.SetActive(true);
		transform.FindChild("BtnLink").gameObject.SetActive(true);
		transform.FindChild("BtnCamera").gameObject.SetActive(true);
		transform.FindChild("BtnWrite").gameObject.SetActive(true);
	}
	
	public void CloseMenu()
	{
		transform.FindChild("BtnPlus").gameObject.SetActive(true);
		transform.FindChild("BtnClose").gameObject.SetActive(false);
		transform.FindChild("BtnLink").gameObject.SetActive(false);
		transform.FindChild("BtnCamera").gameObject.SetActive(false);
		transform.FindChild("BtnWrite").gameObject.SetActive(false);
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
			OpenMenu();
			//			transform.FindChild ("LblTest").gameObject.GetComponent<UILabel> ().text = AndroidMgr.Instance.strLog;
			//			transform.FindChild ("LblTest").gameObject.renderer.material.SetTexture("tex", AndroidMgr.Instance.texTmp);
			//			transform.FindChild ("Texture").gameObject.GetComponent<UITexture>().mainTexture = AndroidMgr.Instance.texTmp;
			break;
		case "BtnClose":
			CloseMenu();
			break;
		case "BtnWrite":
			OpenWriteWindow();
			break;
		case "BtnCamera":
			
			break;
		case "BtnLink":
			
			break;
		}
	}
}
