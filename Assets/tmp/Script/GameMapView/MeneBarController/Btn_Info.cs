using UnityEngine;
using System.Collections;

public class Btn_Info : MonoBehaviour {

	public GameObject btn_Info;
	public GameObject btn_Info_Hover;
	
	public GameObject menuBar;
	public Btn_Sound bs;

	public CameraController cct;

	// Use this for initialization
	void Start () {
		btn_Info_Hover.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){

		bs.info_sound();
		cct.uas = UserActionState.WAIT;
		menuBar.SetActive(false);
		
	}

	void OnMouseEnter() {
		
		btn_Info_Hover.SetActive (true);
		
	}
	
	void OnMouseExit() {
		
		btn_Info_Hover.SetActive (false);
		
	}

}
