using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

public class ScriptImgListItem : cUIScrollListBase {
	public enum STATE{
		Empty,
		Loading,
		Complete,
		Switching,
		Shown
	};
	protected static Texture mDefaultTexture = null;
	
	const int ThumbsWidth = 128;
	const int ThumbsHeight = 128;

	int mIndex;

	protected STATE mState;
	protected string mImgData;
	protected Texture2D mTexture;

	Thread mThread;

	protected Dictionary<string, Texture2D> mDicTexture;

	void Start()
	{
		mState = STATE.Empty;
		if(mDefaultTexture == null)
			mDefaultTexture = UtilMgr.GetChildObj (gameObject, "ImgPreview").GetComponent<UITexture>().mainTexture;
	}

	void Update()
	{
//		ShowStatus ();
		if(GetState () == STATE.Complete)
		{
			UITexture imgPreview = UtilMgr.GetChildObj (gameObject, "ImgPreview").GetComponent<UITexture>();
			SetTexture(UtilMgr.ScaleTexture (mTexture, ThumbsWidth, ThumbsHeight));
			imgPreview.mainTexture = GetTexture();
			mTexture = null;
			SetState(STATE.Shown);
		}
//		else if(GetStatus() == Status.Switching)
//		{
//			UITexture imgPreview = UtilMgr.GetChildObj (gameObject, "ImgPreview").GetComponent<UITexture>();
//			imgPreview.mainTexture = GetTexture();
//			mStatus = Status.Shown;
//		}


//		if(GetListItem().GetStatus() != UIListItem.Status.None)
//		{
//			ResetItem();
//			GetListItem().SetStatus(UIListItem.Status.None);
//		}
	}

	public void SetImgData(string data)
	{
		mImgData = data;
	}
	
	public string GetImgData()
	{
		return mImgData;
	}

	public void SetTexture(Texture2D texture)
	{
//		mTexture = texture;
		mDicTexture.Add (GetImgData(), texture);
	}
	
	public Texture2D GetTexture()
	{
//		return mTexture;
		return mDicTexture[GetImgData()];
	}

	public void SetState(STATE state)
	{
		mState = state;
	}

	public STATE GetState()
	{
		return mState;
	}

	public void SetDicTexture(Dictionary<string, Texture2D> dic)
	{
		mDicTexture = dic;
	}

	public void SetIndex(int index)
	{
		mIndex = index;
	}

	public int GetIndex()
	{
		return mIndex;
	}

	public void ShowStatus()
	{
		UILabel lblName = UtilMgr.GetChildObj (gameObject, "LblName").GetComponent<UILabel>();
	
		lblName.text = "" + GetState () + ", "+GetIndex();
//		lblName.text = ""+System.GC.GetTotalMemory(true);
	}

	public void ShowImg()
	{
//		if (GetStatus () != Status.Empty)
//			return;

		SetState (STATE.Loading);

		mThread = new Thread (runThread);
		mThread.Start ();
	}

	public void joinThread()
	{
		if (mThread != null)
			mThread.Join ();
		mThread = null;
	}

	void runThread()
	{
//		byte[] data = System.IO.File.ReadAllBytes (GetImgData());
//		Texture2D texture = new Texture2D(ThumbsWidth, ThumbsHeight,TextureFormat.DXT1,true);   
//		texture.LoadImage (data);
//		texture = UtilMgr.ScaleTexture (texture, ThumbsWidth, ThumbsHeight);
//		texture.Resize (ThumbsWidth, ThumbsHeight);
//		texture.Apply ();
//		SetTexture(texture);

		WWW www = new WWW ("file://" + GetImgData ());
		mTexture = www.texture;

		SetState(ScriptImgListItem.STATE.Complete);				
	}

	protected void LoadImg()
	{
		SetState (STATE.Empty);
//		Destroy ();
//		mTexture = null;
	}

	public void ResetItem()
	{
//		Destroy (mTexture);
//		mTexture = null;

		UITexture imgPreview = UtilMgr.GetChildObj (gameObject, "ImgPreview").GetComponent<UITexture>();
		
		imgPreview.mainTexture = mDefaultTexture;
		
		SetState (STATE.Empty);
	}

	public void SetImgOld()
	{
		UITexture imgPreview = UtilMgr.GetChildObj (gameObject, "ImgPreview").GetComponent<UITexture>();
		imgPreview.mainTexture = GetTexture();
		SetState(STATE.Shown);
	}
}
