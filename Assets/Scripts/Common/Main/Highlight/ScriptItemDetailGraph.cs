using UnityEngine;
using System.Collections;

public class ScriptItemDetailGraph : MonoBehaviour {

	public GameObject mLblName;
	public GameObject mLblLeft;
	public GameObject mLblRight;
	public GameObject mSprRight;
	public GameObject mSprLeft;

	public void Init(QuizResultGlobal global, QuizResultGlobal friends)
	{
		mLblName.GetComponent<UILabel> ().text = global.orderTitle;
		mLblLeft.GetComponent<UILabel> ().text = global.selectPercentage + "%";
		mLblLeft.transform.localPosition = new Vector3 (-(global.selectPercentage * 2 + 100), 0f, 0f);
		mLblRight.GetComponent<UILabel> ().text = friends.selectPercentage + "%";
		mLblRight.transform.localPosition = new Vector3 ((friends.selectPercentage * 2 + 100), 0f, 0f);
		mSprLeft.GetComponent<UISprite> ().width = global.selectPercentage * 2;
		mSprRight.GetComponent<UISprite> ().width = friends.selectPercentage * 2;
	}
}
