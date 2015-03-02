using UnityEngine;
using System.Collections;
using HtmlAgilityPack;
using System.Linq;

public class ScriptLink : MonoBehaviour {

	UIInput mInput;
	UILabel mLblTitle;
	UILabel mLblBody;
	UILabel mLblLink;
	UILabel mLblError;
	GameObject mContents;
	UITexture mTexture;
	UIButton mBtnClear;
	UIButton mBtnNext;

	static Color Gray = new Color (0.73f, 0.73f, 0.73f);
	static Color Blue = new Color (0.38f, 0.46f, 0.63f);

//	string mTitle;
//	string mBody;
//	string mLink;
	string mImgUrl;
	bool bComplete;

	// Use this for initialization
	void Start () {
		mInput = transform.FindChild ("InputUrl").GetComponent<UIInput> ();
		mLblError = transform.FindChild ("LblError").GetComponent<UILabel> ();
		mContents = transform.FindChild ("Contents").gameObject;
		mLblTitle = mContents.transform.FindChild ("LblTitle").gameObject.GetComponent<UILabel> ();
		mLblBody = mContents.transform.FindChild ("LblBody").gameObject.GetComponent<UILabel> ();
		mLblLink = mContents.transform.FindChild ("BtnLink").FindChild("Label"). gameObject.GetComponent<UILabel> ();
		mTexture = mContents.transform.FindChild ("Texture").gameObject.GetComponent<UITexture> ();
		mBtnClear = transform.FindChild ("BtnClear").GetComponent<UIButton> ();
		mBtnNext = transform.FindChild ("BtnNext").GetComponent<UIButton> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (mInput.value.Length > 0)
		{
			mBtnClear.transform.gameObject.SetActive (true);
		}
		else
		{
			mBtnClear.transform.gameObject.SetActive (false);
		}
	}

	public void Init()
	{
		mContents.SetActive (false);
		mLblError.transform.gameObject.SetActive (false);
		bComplete = false;
		mBtnNext.defaultColor = Gray;
		mInput.value = string.Empty;
	}

	void GetHtmlInfo()
	{
		StartCoroutine ("GetHtml");
	}

	IEnumerator GetHtml()
	{
		string url = mInput.value;
		WWW www = new WWW (url);
		yield return www;
		
		if(www.error == null)
		{
			mContents.SetActive (true);

			HtmlDocument htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(www.text);

			HtmlNode hn = null;
			HtmlNodeCollection hnc = htmlDoc.DocumentNode.SelectNodes("//meta[@property='og:title']");
			if(hnc != null && hnc.Count > 0)
			{
				hn = hnc.FirstOrDefault();
				mLblTitle.text = hn.GetAttributeValue("content", string.Empty);
			}
			else
			{
				mLblTitle.text = string.Empty;
			}

			hnc = htmlDoc.DocumentNode.SelectNodes("//meta[@property='og:description']");
			if(hnc != null && hnc.Count > 0)
			{
				hn = hnc.FirstOrDefault();
				mLblBody.text = hn.GetAttributeValue("content", string.Empty);
			}
			else
			{
				mLblBody.text = string.Empty;
			}

			hnc = htmlDoc.DocumentNode.SelectNodes("//meta[@property='og:url']");
			if(hnc != null && hnc.Count > 0)
			{
				hn = hnc.FirstOrDefault();
				mLblLink.text = hn.GetAttributeValue("content", string.Empty);
			}
			else
			{
				mLblLink.text = string.Empty;
			}

			hnc = htmlDoc.DocumentNode.SelectNodes("//meta[@property='og:image']");
			if(hnc != null && hnc.Count > 0)
			{
				hn = hnc.FirstOrDefault();
				mImgUrl = hn.GetAttributeValue("content", string.Empty);
			}
			else
			{
				mImgUrl = string.Empty;
			}

			SetPreview();

			if(mImgUrl != string.Empty)
			{
				StartCoroutine("GetImg");
			}

		}
		else
		{
//			CommonDialogue.Instance.show (www.error);
			mLblTitle.text = string.Empty;
			SetPreview();
		}
	}

	IEnumerator GetImg()
	{
		WWW www = new WWW (mImgUrl);
		yield return www;

		if(www.error == null)
		{
			mTexture.mainTexture = www.texture;
		}
		else
		{
//			CommonDialogue.Instance.show (www.error);
		}
	}

	void SetPreview()
	{
		if(mLblTitle.text == string.Empty)
		{
			mContents.SetActive (false);
			mLblError.transform.gameObject.SetActive(true);

		}
		else
		{

		}
		bComplete = true;
		mBtnNext.defaultColor = Blue;
	}

	public void ConfirmClicked()
	{
		Init ();
		GetHtmlInfo();
	}

	public void BtnClicked(string name)
	{
		switch(name)
		{
		case "Close":
			transform.gameObject.SetActive(false);
			break;
		case "Confirm":
			ConfirmClicked();
			break;
		case "Clear":
			mInput.value = string.Empty;
			break;
		case "Next":
			if(bComplete)
			{
				//step next
			}
			break;

		}
	}
}
