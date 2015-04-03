using UnityEngine;
using System.Collections;

public class ScriptBtnSelectTeam : MonoBehaviour {

	public GameObject mSelectTeam;

	public void Clicked(string teamCode)
	{
		mSelectTeam.GetComponent<ScriptSelectTeam> ().SetTeam (teamCode);
		transform.GetComponent<UIButton> ().normalSprite = "btn_team_check_on";
	}

	public void ReleaseSelection()
	{
		transform.GetComponent<UIButton> ().normalSprite = "ic_plus";
	}
}
