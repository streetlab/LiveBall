using UnityEngine;
using System.Collections;

public class ScriptRankingTop : MonoBehaviour {

	public GameObject mSocial;
	public GameObject mRanking;

	public GameObject mBtnSocial;
	public GameObject mBtnRanking;

	void Start(){
		OpenSocial ();
	}

	public void BtnClicked(string name){
		if (name.Equals ("BtnSocial")) {
			OpenSocial();
		} else if(name.Equals("BtnRanking")){
			OpenRanking();
		}
	}

	void OpenSocial(){
		mBtnSocial.GetComponent<UIButton> ().isEnabled = false;
		mBtnRanking.GetComponent<UIButton> ().isEnabled = true;

		mSocial.SetActive (true);
		mRanking.SetActive (false);
	}

	void OpenRanking(){
		mBtnSocial.GetComponent<UIButton> ().isEnabled = true;
		mBtnRanking.GetComponent<UIButton> ().isEnabled = false;

		mSocial.SetActive (false);
		mRanking.SetActive (true);
	}
}
