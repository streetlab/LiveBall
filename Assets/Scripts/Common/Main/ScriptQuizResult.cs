using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptQuizResult : MonoBehaviour {
	public GameObject mLblLeft;
	public GameObject mSprLeft;
	public GameObject mLblTopRight;
	public GameObject mLblBtmRight;
	public GameObject mSprBtmRight;
	List<GameObject> mListParticles = new List<GameObject>();

	static Color COLOR_CORRECT = new Color (102f / 255f, 165f / 255f, 242f / 255f);
	static Color COLOR_WRONG = new Color (142f / 255f, 123f / 255f, 103f / 255f);
	static Color[] COLORS_FIREWORK = {
		new Color (255f, 255f, 0f),
		new Color (255f, 0f, 0f),
		new Color (0f, 0f, 255f),
		new Color (0f, 255f, 0f),
		new Color (255f, 0f, 255f),
		new Color (0f, 255f, 255f)};

	public bool Init(List<SimpleResultInfo> listResult){
		double rewardPoint = 0;
		int betPoint = 0;
		foreach (SimpleResultInfo resultInfo in listResult) {
			rewardPoint += double.Parse(resultInfo.rewardPoint);
			betPoint += int.Parse(resultInfo.betPoint);
		}

		QuizInfo quizInfo = null;
		foreach (QuizInfo quiz in QuizMgr.QuizList) {
			if(listResult[0].quizListSeq == quiz.quizListSeq){
				quizInfo = quiz;
				break;
			}
		}

		string info = quizInfo.playerName + " / ";
		int quizValue = int.Parse (listResult [0].quizValue);
		info += quizInfo.order [quizValue - 1].description;
		mLblTopRight.GetComponent<UILabel>().text = info;

		int rand = UnityEngine.Random.Range (1, 3);

		if (rewardPoint > 0) {
			mSprLeft.GetComponent<UISprite>().color = COLOR_CORRECT;
			mSprBtmRight.GetComponent<UISprite>().color = COLOR_CORRECT;

			mLblLeft.GetComponent<UILabel>().text =
				transform.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("Correct"+rand).Value;
			mLblBtmRight.GetComponent<UILabel>().text = ""+rewardPoint;

			double userGoldenBall = double.Parse (UserMgr.UserInfo.userGoldenBall) + rewardPoint;
			UserMgr.UserInfo.userGoldenBall = ""+userGoldenBall;
			return true;
		} else{
			mSprLeft.GetComponent<UISprite>().color = COLOR_WRONG;
			mSprBtmRight.GetComponent<UISprite>().color = COLOR_WRONG;

			mLblLeft.GetComponent<UILabel>().text =
				transform.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("Wrong"+rand).Value;
			mLblBtmRight.GetComponent<UILabel>().text = "-"+betPoint;

			return false;
		}
	}

	public void InitParticle(){
		foreach (GameObject go in mListParticles)
			DestroyImmediate (go);
		mListParticles.Clear ();

		GameObject prefab = Resources.Load ("Firework1") as GameObject;
		for (int i = 0; i < 20; i++) {
			GameObject firework = Instantiate (prefab, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
			firework.transform.parent = transform.FindChild ("ResultWindow").transform;
			firework.transform.localScale = new Vector3 (1f, 1f, 1f);

			float posX = UnityEngine.Random.Range(-200f, 200f);
			float posY = UnityEngine.Random.Range(-90f, -180f);
			firework.transform.localPosition = new Vector3 (posX, posY, 0);

			float delay = UnityEngine.Random.Range(0f, 1f);
			firework.GetComponent<ParticleSystem>().startDelay = delay;
			firework.transform.FindChild("Ring").GetComponent<ParticleSystem>().startDelay = delay;

			int color = UnityEngine.Random.Range(0, COLORS_FIREWORK.Length-1);
			firework.GetComponent<ParticleSystem>().startColor = COLORS_FIREWORK[color];
			firework.transform.FindChild("Ring").GetComponent<ParticleSystem>().startColor = COLORS_FIREWORK[color];

//			firework.GetComponent<ScriptParticleResizer> ().ResizeRatio (0.5f);
			firework.GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 3100;
			firework.transform.FindChild("Ring").GetComponent<ParticleSystem>()
				.GetComponent<Renderer>().material.renderQueue = 3100;
			mListParticles.Add (firework);

			firework.GetComponent<ParticleSystem> ().Play();
		}
	}
}
