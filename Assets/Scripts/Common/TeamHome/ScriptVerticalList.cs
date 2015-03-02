using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Linq;

public class ScriptVerticalList : MonoBehaviour {

//	Dictionary<string, List<string>> mImageDictionary;

	// Use this for initialization
	void Start () {
		CloseMenu ();
//		transform.FindChild ("LblTest").gameObject.GetComponent<UILabel> ().text = AndroidMgr.Instance.strLog;


	}

//	string ddd = "";
//	void OnGUI()
//	{
////		ddd = GUI.TextField (new Rect (10, 10, 700, 700), ddd, 25);
//		ddd = GUI.TextArea(new Rect(10, 10, 700, 700), ddd, 250);
//	}

	
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
		transform.parent.parent.FindChild("Search").gameObject.SetActive (false);
		transform.parent.parent.FindChild("Match").gameObject.SetActive (false);
		transform.parent.parent.FindChild("Timeline").gameObject.SetActive (false);
		transform.parent.parent.FindChild("Upload").gameObject.SetActive (false);

		Transform transformWritten = transform.parent.parent.FindChild ("Written");
		transformWritten.gameObject.SetActive (true);
		UIInput uiInputInputBody = transformWritten.FindChild ("InputBody").gameObject.GetComponent<UIInput> ();
//		BoxCollider2D colliderInputBody = uiInputInputBody.GetComponent<BoxCollider2D> ();
//		UISprite uiSpriteSprBG = uiInputInputBody.transform.FindChild ("SprBG").gameObject.GetComponent<UISprite> ();
//		UILabel lblHint = 
//		uiSpriteSprBG.SetDimensions (720, 1090);

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

//	public void setTestText(string str)
//	{
//		transform.FindChild ("LblTest").gameObject.GetComponent<UILabel> ().text = str;
//	}
}
