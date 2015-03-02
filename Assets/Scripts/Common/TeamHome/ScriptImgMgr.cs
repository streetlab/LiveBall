using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

public class ScriptImgMgr : MonoBehaviour {
	protected List<ScriptImgListItem> mItemList;
//	protected List<Texture2D> mListTexture;
	protected Dictionary<string, Texture2D> mDicTexture;

	// Use this for initialization
	void Start () {
		mItemList = new List<ScriptImgListItem> ();
		mDicTexture = new Dictionary<string, Texture2D> ();
	}
	
	// Update is called once per frame
	int i = 0;
	void Update () {
		if(Input.touches.Length > 0){
			if (Input.GetTouch (0).phase == TouchPhase.Moved
			    || Input.GetTouch(0).phase == TouchPhase.Stationary)
			{
				return;
			}

			if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				int cnt = mItemList.Count;
				if(cnt > 20){
					for(int i = 0; i < cnt-20; i++)
					{
						mItemList.RemoveAt(0);
					}
				}
			}
		}

		if(mItemList.Count > 0)
		{
			ScriptImgListItem item = mItemList[i];
			if(item.GetState() == ScriptImgListItem.STATE.Empty)
			{
				item.ShowImg();
			}
			else if(item.GetState() == ScriptImgListItem.STATE.Shown
			        || item.GetState() == ScriptImgListItem.STATE.Complete)
			{
				mItemList.RemoveAt(0);
//				i++;
			}
		}
	}

	public void Add(ScriptImgListItem item)
	{
		mItemList.Add (item);
	}

	public Dictionary<string, Texture2D> GetDicTexture()
	{
		return mDicTexture;
	}
}