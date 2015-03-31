using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptItemListFriends : MonoBehaviour {

	public GameObject mLblname;
	public GameObject mLblChoice;
	public GameObject mLblGold;
	public GameObject mPhoto;

	public void Init(QuizResultResults friend, List<QuizResultGlobal> orders)
	{
		mLblname.GetComponent<UILabel> ().text = friend.memberName;
		mLblGold.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(friend.rewardPoint);
		string order = orders [int.Parse (friend.respValue) - 1].orderTitle;
		mLblChoice.GetComponent<UILabel> ().text = order;
	}
}
