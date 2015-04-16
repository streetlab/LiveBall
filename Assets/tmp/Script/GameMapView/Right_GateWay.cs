using UnityEngine;
using System.Collections;

public class Right_GateWay : MonoBehaviour {
	
	public GameObject gateWay_Hover;
	public GameObject menuBox;
	public AudioClip menuOn;

	public CameraController cct;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		if (cct.uas == UserActionState.WAIT) {
			cct.menuBarChoice(gameObject.name);
			menuBox.SetActive (true);
			menuBox.transform.position = new Vector2(transform.position.x + -0.6f, transform.position.y);
			audio.clip = menuOn;
			audio.Play();
		} else if (cct.uas == UserActionState.MOVE) {
			menuBox.SetActive (false);
		}


	}
	
	void OnMouseEnter() {
		gateWay_Hover.SetActive(true);
	}
	
	void OnMouseExit() {
		gateWay_Hover.SetActive(false);
	}


}
