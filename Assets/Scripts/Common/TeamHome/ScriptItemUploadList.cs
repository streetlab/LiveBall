using UnityEngine;
using System.Collections;
using System.Threading;

public class ScriptItemUploadList :  ScriptImgListItem{

	string mKey;
	int mCount;

	public void SetKey(string key)
	{
		mKey = key;
	}

	public string GetKey()
	{
		return mKey;
	}

	public void SetCount(int cnt)
	{
		mCount = cnt;
	}

	public void OnClicked()
	{
		GameObject objUpload = transform.parent.parent.parent.FindChild ("Upload").gameObject;
		GameObject objSelection = transform.parent.parent.parent.FindChild ("Selection").gameObject;
		ScriptSelection ss = objSelection.GetComponent<ScriptSelection> ();
		ScriptUpload su = objUpload.GetComponent<ScriptUpload> ();

		objSelection.SetActive (true);
		objUpload.SetActive (false);

		int lastIdx = GetKey ().LastIndexOf("/");

//		su.StopThread ();
		su.transform.gameObject.SetActive (false);

		ss.SetTitle (GetKey ().Substring (lastIdx + 1));
		ss.SetImgList (su.GetList (GetKey ()));
		ss.InitListUI ();


	}

	public bool LoadImg()	{

		if(mDicTexture.ContainsKey(GetImgData()))
		{
			SetState(ScriptImgListItem.STATE.Switching);	
			return false;
		}

		base.LoadImg ();

		UILabel lblName = UtilMgr.GetChildObj (gameObject, "LblName").GetComponent<UILabel>();
		UILabel lblCount = UtilMgr.GetChildObj (gameObject, "LblCount").GetComponent<UILabel>();

		string thumbsFolder = GetKey ();
		int lastIdx = thumbsFolder.LastIndexOf("/");
		lblName.text = thumbsFolder.Substring(lastIdx+1);
		lblCount.text = mCount + "";
		return true;
	}
}
