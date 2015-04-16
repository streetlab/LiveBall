using UnityEngine;
using System.Collections;

public class Btn_Sound : MonoBehaviour {

	public AudioClip move;
	public AudioClip info;
	public AudioClip attack;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void at_sound() {
		audio.clip = attack;
		audio.Play();
	}

	public void move_sound() {
		audio.clip = move;
		audio.Play();
	}

	public void info_sound() {
		audio.clip = info;
		audio.Play();
	}


}
