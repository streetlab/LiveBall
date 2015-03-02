using UnityEngine;
using System.Collections;

public class ScriptItemPhoto : MonoBehaviour {

	string mImgData;
	UITexture mImgPreview;
	UIButton mBtnClose;
	GameObject mEdges;
	Texture mDefaultTexture;

	bool bActive;


	// Use this for initialization
	void Start () {
		mImgPreview = transform.FindChild ("ImgPreview").gameObject.GetComponent <UITexture>();
		mBtnClose = transform.FindChild ("BtnClose").gameObject.GetComponent <UIButton>();
		mEdges = transform.FindChild ("Edges").gameObject;
		mDefaultTexture = mImgPreview.mainTexture;
		bActive = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isActive()
	{
		return bActive;
	}

	public void SetImgData(string data)
	{
		mImgData = data;
		WWW www = new WWW ("file://" + data);
		mImgPreview.mainTexture = www.texture;
		mBtnClose.gameObject.SetActive (true);
		bActive = true;
	}

	public void OnClicked(string name)
	{
		switch(name)
		{
		case "Close" :
			Destroy(mImgPreview.mainTexture);
			mImgPreview.mainTexture = null;
			mImgPreview.mainTexture = mDefaultTexture;
			mBtnClose.gameObject.SetActive (false);
			bActive = false;
//			CommonDialogue.Instance.show ("close");
			break;
		}
	}
}
