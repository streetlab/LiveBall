using UnityEngine;
using System.Collections;
using System.Threading;

public class ScriptItemSelectionList : ScriptImgListItem {

	UISprite mSprCheck;
	UISprite mSprCurtain;
	bool isSelected;
	
	UIPanel mListPanel;
	UIScrollView mListView;

	// if texture isn't null then it returns false
	public bool LoadImg()
	{
		UILabel lblName = UtilMgr.GetChildObj (gameObject, "LblName").GetComponent<UILabel>();
		string thumbsData = GetImgData ();
		int lastIdx = thumbsData.LastIndexOf("/");
		lblName.text = thumbsData.Substring(lastIdx+1);

		if(mDicTexture.ContainsKey(GetImgData()))
		{
//			lblName.text = "SetOld";
//			SetStatus(ScriptImgListItem.Status.Switching);	
			SetImgOld();
			return false;
		}

		base.LoadImg ();

		return true;
	}



//	void SetItem()
//	{
//		byte[] data = System.IO.File.ReadAllBytes (mImgData);
//		mTexture = new Texture2D(1,1,TextureFormat.DXT1,true);   
//		mTexture.LoadImage (data);
//		mStatus = Status.Complete;
//	}
	
	public void OnClick()
	{
//		SetTexture (mImgData);
//		LoadImg ();
		if(isSelected)
		{
//			SetUnSelected();
		}
		else
		{
//			SetSelected ();
		}
		
	}
	
	public void SetInvisible()
	{
		gameObject.SetActive (false);
	}
	
	public void SetSelected()
	{
		mSprCheck.spriteName = "btn_photo_check_on";
		mSprCurtain.gameObject.SetActive (true);
		isSelected = true;
	}
	
	public void SetUnSelected()
	{
		mSprCheck.spriteName = "btn_photo_check_off";
		mSprCurtain.gameObject.SetActive (false);
		isSelected = false;
	}
}
