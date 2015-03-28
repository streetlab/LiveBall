using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class ScriptUpload : ScriptImgMgr {
	Dictionary<string, List<string>> mImageDictionary;
	string[] mKeys;

	public List<string> GetList(string key)
	{
		return mImageDictionary [key];
	}

	public void InitUploadList()
	{
		if(Application.platform == RuntimePlatform.Android)
		{
//			AndroidMgr.GetGalleryImages(this);
		}
		else
		{
			
		}

	}

	public void InitListUI()
	{
		mKeys = new string[mImageDictionary.Count];
		mImageDictionary.Keys.CopyTo (mKeys, 0);

		transform.FindChild ("ListUpload").GetComponent<UIDraggablePanel2> ().Init (mKeys.Length, delegate(UIListItem item, int index) {
			ScriptItemUploadList sItem = item.Target.gameObject.GetComponent<ScriptItemUploadList>();
			sItem.SetDicTexture(GetDicTexture());
//			sItem.SetIndex(index);
			sItem.SetKey(mKeys[index]);
			sItem.SetCount(mImageDictionary[mKeys[index]].Count);
			sItem.SetImgData(mImageDictionary[mKeys[index]][0]);
			if(sItem.LoadImg())
			{
				Add (item.Target.gameObject.GetComponent<ScriptItemUploadList>());
			}
		});
	}

//	public void setImageDictionary(JSONObject json)
//	{
//		string log = "";
//		mImageDictionary = new Dictionary<string, List<string>>();
//		
//		for(int i = 0; i < json.Count; i++)
//		{
//			string thumbsData = json[i].str;
//			thumbsData = thumbsData.Replace("\\", "");
//			int lastIdx = thumbsData.LastIndexOf("/");
//			string thumbsFolder = thumbsData.Substring(0, lastIdx);
//			if(!mImageDictionary.ContainsKey(thumbsFolder))
//			{
//				mImageDictionary.Add(thumbsFolder, new List<string>());
//				log += "find new key : " + thumbsFolder +  " , ";
//			}
//			mImageDictionary[thumbsFolder].Add(thumbsData);
//		}
//		
//		log += "size of imageDictionary : " + mImageDictionary.Count;
//		
////		transform.FindChild ("LblTest").gameObject.GetComponent<UILabel> ().text = log;
//		InitListUI ();
//	}
}
