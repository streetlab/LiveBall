using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptSelection : ScriptImgMgr {
	List<string> mImgList;
	int mCount = 0;

	public void SetTitle(string title)
	{
		transform.FindChild ("Label").GetComponent<UILabel> ().text = title;
	}

	public void SetImgList(List<string> list)
	{
		mImgList = list;
	}

	public void InitListUI()
	{
		transform.FindChild ("ListSelection").GetComponent<UIDraggablePanel2> ().Init (mImgList.Count, delegate(UIListItem item, int index) {
			ScriptItemSelectionList sItem = item.Target.GetComponent<ScriptItemSelectionList>();
			sItem.SetDicTexture(GetDicTexture());
			sItem.SetImgData(mImgList[index]);
			sItem.SetIndex(index);

				//		{
				//			ResetItem();
				//			GetListItem().SetStatus(UIListItem.Status.None);
				//		}

//			if(sItem.GetListItem().GetStatus() != UIListItem.Status.None)
//			{
				sItem.ResetItem();
//				sItem.GetListItem().SetStatus(UIListItem.Status.None);
//			}

			if(sItem.LoadImg())
			{
				Add (item.Target.gameObject.GetComponent<ScriptItemSelectionList>());
			}

		});


	}
}
