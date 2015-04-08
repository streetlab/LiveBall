using UnityEngine;
using System.Collections;

public class ScriptGameTop : MonoBehaviour {

	public GameObject mSchedule;
	public GameObject mRanking;
	public GameObject mLeague;
	public GameObject mStatistics;

	// Use this for initialization
	void Start () {
		mSchedule.SetActive (true);

		mRanking.SetActive (false);
		mLeague.SetActive (false);
		mStatistics.SetActive (false);
	}

	public void BtnClicked(string name){
		switch(name){
		case "BtnSchedule":
			OpenSchedule();
			break;
		case "BtnRanking":
			OpenRanking();
			break;
		case "BtnLeague":
			OpenLeague();
			break;
		case "BtnStatistics":
			OpenStatistics();
			break;
		}
	}

	void OpenSchedule(){
		mSchedule.SetActive (true);
		
		mRanking.SetActive (false);
		mLeague.SetActive (false);
		mStatistics.SetActive (false);
	}

	void OpenRanking(){
		mRanking.SetActive (true);
		
		mSchedule.SetActive (false);
		mLeague.SetActive (false);
		mStatistics.SetActive (false);
	}

	void OpenLeague(){
		mLeague.SetActive (true);
		
		mRanking.SetActive (false);
		mSchedule.SetActive (false);
		mStatistics.SetActive (false);
	}

	void OpenStatistics(){
		mStatistics.SetActive (true);
		
		mRanking.SetActive (false);
		mLeague.SetActive (false);
		mSchedule.SetActive (false);
	}


}
