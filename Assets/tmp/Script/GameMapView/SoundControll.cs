using UnityEngine;
using System.Collections;
using System;

public class SoundControll : MonoBehaviour {

	public AudioClip playingSound1;
	public AudioClip playingSound2;
	public AudioClip playingSound3;

	int soundNum;

	// Use this for initialization
	void Start () {
	
		playSound();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (!audio.isPlaying) {
			playSound();
		}

	}

	public void playSound() {
		soundNum = randNum(1, 4);
		if (soundNum == 1) {
			audio.clip = playingSound1;
			audio.Play();
		} else if (soundNum == 2) {
			audio.clip = playingSound2;
			audio.Play();
		} else if (soundNum == 3) {
			audio.clip = playingSound3;
			audio.Play();
		}

	}

	// 랜덤 숫자 발생
	public int randNum(int minN, int maxN) {	
		System.Random rd = new System.Random(DateTime.Now.Millisecond);
		int soundNum = 0;
		soundNum = rd.Next(minN, maxN);
		return soundNum;
	}

}
