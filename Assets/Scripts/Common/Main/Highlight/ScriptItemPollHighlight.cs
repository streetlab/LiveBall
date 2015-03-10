using UnityEngine;
using System.Collections;

public class ScriptItemPollHighlight : MonoBehaviour {

	GameObject mBack;
	GameObject mSprite;
	GameObject mLabel;
	GameObject mBtnLeft;
	GameObject mBtnCenter;
	GameObject mBtnRight;

	public void Init(QuizInfo quizInfo)
	{
		transform.FindChild ("Label").GetComponent<UILabel> ().text = quizInfo.quizTitle;
	}
}
