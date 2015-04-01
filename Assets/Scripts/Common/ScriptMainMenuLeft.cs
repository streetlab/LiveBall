using UnityEngine;
using System.Collections;

public class ScriptMainMenuLeft : MonoBehaviour {

//	string _state;
//
//	public string State
//	{
//		get{return _state;}
//		set{_state = value;}
//	}

	public void BtnClicked(string name)
	{
		Debug.Log (Application.loadedLevelName);
		switch(name)
		{
		case "BtnTeamHome":
			if(!Application.loadedLevelName.Equals("SceneTeamHome"))
				AutoFade.LoadLevel("SceneTeamHome", 0f, 1f);
			break;
		case "BtnGameHome":
//			if(!Application.loadedLevelName.Equals("SceneGame"))
//				AutoFade.LoadLevel("SceneGame", 0f, 1f);
			break;
		case "BtnCards":

			break;
		case "BtnIamPlayer":

			break;
		case "BtnRanking":

			break;
		case "BtnProfile":

			break;
		case "BtnItem":

			break;
		case "BtnNotice":

			break;
		case "BtnSettings":

			break;
		}
	}
}
