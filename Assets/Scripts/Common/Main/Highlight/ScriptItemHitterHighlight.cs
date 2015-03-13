using UnityEngine;
using System.Collections;

public class ScriptItemHitterHighlight : MonoBehaviour {

	public GameObject mBG;
	public GameObject mPhoto;
	public GameObject mLblName;
	public GameObject mLblNumber;
	public GameObject mLblResult;
	public GameObject mLblReward;
	public GameObject mLblSelect1;
	public GameObject mLblSelect2_1;
	public GameObject mLblSelect2_2;

	public void Init(QuizInfo quizInfo)
	{
		mLblName.transform.GetComponent<UILabel> ().text = quizInfo.playerName;
		mLblNumber.transform.GetComponent<UILabel> ().text = "No."+quizInfo.playerNumber;
		mLblReward.transform.GetComponent<UILabel> ().text = quizInfo.rewardDividend;
		WWW www = new WWW (Constants.IMAGE_SERVER_HOST + quizInfo.imageName);
		StartCoroutine(GetImage (www));
		SetQuizResult (quizInfo);
		
	}

	void SetQuizResult(QuizInfo quizInfo)
	{
		mLblReward.SetActive (false);
		mLblSelect1.SetActive (false);
		mLblSelect2_1.SetActive (false);
		mLblSelect2_2.SetActive (false);

		if(quizInfo.quizValue.Length > 0){
			int idx = int.Parse(quizInfo.quizValue) -1;
			mLblResult.GetComponent<UILabel>().text = quizInfo.order[idx].description;

			bool isCorrect = false;
			QuizRespInfo resp = null;
			if(quizInfo.resp != null){
				for(int i = 0; i < quizInfo.resp.Count; i++){
					resp = quizInfo.resp[i];
					if(resp.respValue.Equals(quizInfo.quizValue)){
						isCorrect = true;
						break;
					}
				}
			}

			if(isCorrect){
				mLblReward.SetActive(true);
				mLblReward.GetComponent<UILabel>().text = resp.expectRewardPoint+"";
				return;
			}
		} else if(quizInfo.resultMsg.Length > 0){
			mLblResult.GetComponent<UILabel>().text = quizInfo.resultMsg;
			mLblSelect1.SetActive(true);
			mLblSelect1.GetComponent<UILabel> ().text = "X";
		}

		if(quizInfo.resp != null && quizInfo.resp.Count == 1){
			mLblSelect1.SetActive (true);
			int respValue = int.Parse(quizInfo.resp[0].respValue) -1;
			mLblSelect1.GetComponent<UILabel>().text = quizInfo.order[respValue].description;
		} else if(quizInfo.resp.Count == 2){
			mLblSelect2_1.SetActive (true);
			int respValue = int.Parse(quizInfo.resp[0].respValue) -1;
			mLblSelect2_1.GetComponent<UILabel>().text = quizInfo.order[respValue].description;
			mLblSelect2_2.SetActive (true);
			respValue = int.Parse(quizInfo.resp[1].respValue) -1;
			mLblSelect2_2.GetComponent<UILabel>().text = quizInfo.order[respValue].description;
		}
	}

	IEnumerator GetImage(WWW www)
	{
		yield return www;
		Texture2D temp = new Texture2D (0, 0);
		www.LoadImageIntoTexture (temp);
		mPhoto.transform.FindChild("Panel").FindChild("Texture").GetComponent<UITexture>().mainTexture = temp;
	}

	public void OnClicked()
	{
		transform.parent.GetComponent<SpringPanel> ().enabled = false;
		transform.parent.transform.localPosition = new Vector3 (0f, 1395f, 0f);
		NGUITools.FindInParents<UIPanel>(gameObject).clipOffset = new Vector2(0f, -1531f);
	}
}
