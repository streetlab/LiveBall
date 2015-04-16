using UnityEngine;
using System.Collections;

public class Btn_Move : MonoBehaviour {

	public GameObject btn_Move;
	public GameObject btn_Move_Hover;
	
	public GameObject menuBar;
	public Btn_Sound bs;

	public CameraController cct;

	// Use this for initialization
	void Start () {
		btn_Move_Hover.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){

		bs.move_sound();
		cct.uas = UserActionState.MOVE;
		menuBar.SetActive(false);
		
	}

	void OnMouseEnter() {
		
		btn_Move_Hover.SetActive (true);
		
	}
	
	void OnMouseExit() {
		
		btn_Move_Hover.SetActive (false);
		
	}

}
