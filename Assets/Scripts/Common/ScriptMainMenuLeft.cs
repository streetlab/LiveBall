using UnityEngine;
using System.Collections;

public class ScriptMainMenuLeft : MonoBehaviour {

	public GameObject mBtnTeamHome;
	public GameObject mBtnGameHome;
	public GameObject mBtnCards;
	public GameObject mBtnIamPlayer;
	public GameObject mBtnRanking;
	public GameObject mBtnProfile;
	public GameObject mBtnItem;
	public GameObject mBtnNotice;
	public GameObject mBtnSettings;

	void Start(){
		SetBtnDisable ();
	}

	void SetBtnsEnable(){
		mBtnTeamHome.GetComponent<UIButton> ().isEnabled = true;
		mBtnGameHome.GetComponent<UIButton> ().isEnabled = true;
		mBtnCards.GetComponent<UIButton> ().isEnabled = true;
		mBtnIamPlayer.GetComponent<UIButton> ().isEnabled = true;
		mBtnRanking.GetComponent<UIButton> ().isEnabled = true;
		mBtnProfile.GetComponent<UIButton> ().isEnabled = true;
		mBtnItem.GetComponent<UIButton> ().isEnabled = true;
		mBtnNotice.GetComponent<UIButton> ().isEnabled = true;
		mBtnSettings.GetComponent<UIButton> ().isEnabled = true;
	}

	void SetBtnDisable(){
		if(Application.loadedLevelName.Equals("SceneTeamHome"))
		   mBtnTeamHome.GetComponent<UIButton> ().isEnabled = false;
		else if(Application.loadedLevelName.Equals("SceneGame"))
			mBtnGameHome.GetComponent<UIButton> ().isEnabled = false;
		else if(Application.loadedLevelName.Equals("SceneCards"))
			mBtnCards.GetComponent<UIButton> ().isEnabled = false;
		else if(Application.loadedLevelName.Equals("SceneRanking"))
			mBtnRanking.GetComponent<UIButton> ().isEnabled = false;
		else if(Application.loadedLevelName.Equals("SceneProfile"))
			mBtnProfile.GetComponent<UIButton> ().isEnabled = false;
		else if(Application.loadedLevelName.Equals("SceneItems"))
			mBtnItem.GetComponent<UIButton> ().isEnabled = false;
	}

	public void BtnClicked(string name)
	{
		SetBtnsEnable ();
		Debug.Log (Application.loadedLevelName);
		switch(name)
		{
		case "BtnTeamHome":
			if(!Application.loadedLevelName.Equals("SceneTeamHome"))
				AutoFade.LoadLevel("SceneTeamHome", 0f, 1f);
			break;
		case "BtnGameHome":
			if(!Application.loadedLevelName.Equals("SceneGame"))
				AutoFade.LoadLevel("SceneGame", 0f, 1f);
			break;
		case "BtnCards":
			if(!Application.loadedLevelName.Equals("SceneCards"))
				AutoFade.LoadLevel("SceneCards", 0f, 1f);
			break;
		case "BtnIamPlayer":

			break;
		case "BtnRanking":
			if(!Application.loadedLevelName.Equals("SceneRanking"))
				AutoFade.LoadLevel("SceneRanking", 0f, 1f);
			break;
		case "BtnProfile":
			if(!Application.loadedLevelName.Equals("SceneProfile"))
				AutoFade.LoadLevel("SceneProfile", 0f, 1f);
			break;
		case "BtnItem":
			if(!Application.loadedLevelName.Equals("SceneItems"))
				AutoFade.LoadLevel("SceneItems", 0f, 1f);
			break;
		case "BtnNotice":

			break;
		case "BtnSettings":

			break;
		}
	}
}
