using UnityEngine;
using System.Collections;

public class ScriptItemHitterHighlight : MonoBehaviour {

	GameObject mBG;
	GameObject mLineT;
	GameObject mLineB;
	GameObject mPhoto;
	GameObject mLblName;
	GameObject mLblNumber;
	GameObject mLblResult;
	GameObject mLblReward;

	public void Init(QuizInfo quizInfo)
	{
		transform.FindChild("LblName").GetComponent<UILabel> ().text = quizInfo.playerName;
		transform.FindChild("LblNumber").GetComponent<UILabel> ().text = string.Format("{0}", quizInfo.playerNumber);
	}
}
