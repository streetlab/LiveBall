using UnityEngine;
using System.Collections;

public enum CastleState {

	SMALL,
	MIDLLE,
	BIG

}


public class CastleController : MonoBehaviour {

	public GameObject small_Castle;
	public GameObject midlle_Castle;
	public GameObject big_Castle;

	public GameObject small_Castle_Hover;
	public GameObject midlle_Castle_Hoveer;
	public GameObject big_Castle_Hover;

	public CastleState cs;

	public GameObject menuBox;
	public AudioClip menuOn;

	public CameraController cct;


	// Use this for initialization
	void Start () {
	
		cs = CastleState.SMALL;
		if (cs == CastleState.SMALL) {
			small_Castle.SetActive(true);
			midlle_Castle.SetActive(false);
			big_Castle.SetActive(false);
		} else if (cs == CastleState.MIDLLE) {
			small_Castle.SetActive(false);
			midlle_Castle.SetActive(true);
			big_Castle.SetActive(false);
		} else if (cs == CastleState.BIG) {
			small_Castle.SetActive(false);
			midlle_Castle.SetActive(false);
			big_Castle.SetActive(true);
		}


	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown(1)) {
			menuBox.SetActive (false);
		}

	}


	void OnMouseDown(){

		if (cct.uas == UserActionState.WAIT) {
			cct.menuBarChoice(gameObject.name);
			menuBox.SetActive (true);
			menuBox.transform.position = transform.position;
			audio.clip = menuOn;
			audio.Play();
			print(gameObject.name);
		} else if (cct.uas == UserActionState.MOVE) {
			menuBox.SetActive (false);
		}

	}
	
	void OnMouseEnter() {

		if (cs == CastleState.SMALL) {
			small_Castle_Hover.SetActive(true);
		} else if (cs == CastleState.MIDLLE) {
			midlle_Castle_Hoveer.SetActive(true);
		} else if (cs == CastleState.BIG) {
			big_Castle_Hover.SetActive(true);
		}


	}

	void OnMouseExit() {

		if (cs == CastleState.SMALL) {
			small_Castle_Hover.SetActive(false);
		} else if (cs == CastleState.MIDLLE) {
			midlle_Castle_Hoveer.SetActive(false);
		} else if (cs == CastleState.BIG) {
			big_Castle_Hover.SetActive(false);
		}

	}

	public void asdasd() {
		print ("공격눌 림");
	}



}
