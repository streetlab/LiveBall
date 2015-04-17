using UnityEngine;
using System.Collections;

public class ScriptTHTop : MonoBehaviour {

	public GameObject mTimeline;
	public GameObject mAlbum;
	public GameObject mSeason;
	public GameObject mSquad;

	public GameObject mBtnTimeline;
	public GameObject mBtnAlbum;
	public GameObject mBtnSeason;
	public GameObject mBtnSquad;

	void Start(){
//		mTimeline.SetActive (true);
//
//		mAlbum.SetActive (false);
//		mSeason.SetActive (false);
//		mSquad.SetActive (false);
		OpenTimeline ();
	}

	public void BtnClicked(string name){
		switch (name) {
		case "BtnTimeline":
			OpenTimeline();
			break;
		case "BtnAlbum":
			OpenAlbum();
			break;
		case "BtnSeason":
			OpenSeason();
			break;
		case "BtnSquad":
			OpenSquad();
			break;
		}
	}

	void OpenTimeline(){
		mBtnTimeline.GetComponent<UIButton> ().isEnabled = false;

		mBtnAlbum.GetComponent<UIButton> ().isEnabled = true;
		mBtnSeason.GetComponent<UIButton> ().isEnabled = true;
		mBtnSquad.GetComponent<UIButton> ().isEnabled = true;

		mTimeline.SetActive (true);
		
		mAlbum.SetActive (false);
		mSeason.SetActive (false);
		mSquad.SetActive (false);
	}

	void OpenAlbum(){
		mBtnAlbum.GetComponent<UIButton> ().isEnabled = false;

		mBtnTimeline.GetComponent<UIButton> ().isEnabled = true;
		mBtnSeason.GetComponent<UIButton> ().isEnabled = true;
		mBtnSquad.GetComponent<UIButton> ().isEnabled = true;

		mAlbum.SetActive (true);
		mAlbum.GetComponent<ScriptTF_Album> ().OpenWebView ();
		
		mTimeline.SetActive (false);
		mSeason.SetActive (false);
		mSquad.SetActive (false);
	}

	void OpenSeason(){
		mBtnSeason.GetComponent<UIButton> ().isEnabled = false;

		mBtnTimeline.GetComponent<UIButton> ().isEnabled = true;
		mBtnAlbum.GetComponent<UIButton> ().isEnabled = true;
		mBtnSquad.GetComponent<UIButton> ().isEnabled = true;

		mSeason.SetActive (true);
		
		mAlbum.SetActive (false);
		mTimeline.SetActive (false);
		mSquad.SetActive (false);
	}

	void OpenSquad(){
		mBtnSquad.GetComponent<UIButton> ().isEnabled = false;

		mBtnAlbum.GetComponent<UIButton> ().isEnabled = true;
		mBtnSeason.GetComponent<UIButton> ().isEnabled = true;
		mBtnTimeline.GetComponent<UIButton> ().isEnabled = true;

		mSquad.SetActive (true);
		
		mAlbum.SetActive (false);
		mSeason.SetActive (false);
		mTimeline.SetActive (false);
	}

}
