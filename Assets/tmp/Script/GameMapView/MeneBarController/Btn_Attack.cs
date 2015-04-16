using UnityEngine;
using System.Collections;

public class Btn_Attack : MonoBehaviour {

	public GameObject btn_AT;
	public GameObject btn_AT_Hover;
	
	public GameObject menuBar;
	public Btn_Sound bs;

	public CameraController cct;

	// Use this for initialization
	void Start () {
		btn_AT_Hover.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){

		bs.at_sound();
		cct.uas = UserActionState.MOVE;
		menuBar.SetActive(false);
	
	}


	void OnMouseEnter() {
		
		btn_AT_Hover.SetActive (true);
		
	}
	
	void OnMouseExit() {
		
		btn_AT_Hover.SetActive (false);
		
	}




}
