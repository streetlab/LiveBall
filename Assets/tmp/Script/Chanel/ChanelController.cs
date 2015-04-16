using UnityEngine;
using System.Collections;

public class ChanelController : MonoBehaviour {

	private string dbName = "Users.db";
	private dbAccess db;
	string curName;
	string curID;

	public GameObject popUp;
	public AudioClip no;


	int myLv;
	int myLeader;

	// Use this for initialization
	void Start () {
	
		db = GetComponent<dbAccess>(); // 디비 스크립트 연결
		curName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("curUnitName")));	
		curID = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("ID")));
		db.OpenDB(dbName);
		myLv = db.intDBReader("unit_lv", "UnitInfo", "unit_name", curID);
		myLeader = db.intDBReader("basic_leadership ", "UnitInfo", "unit_name", curID);
		db.CloseDB();
		print (myLv + ", " + myLeader);

	}

	public void close_Pop() {
		popUp.SetActive(false);
	}

	public void sound_Play() {
		audio.clip = no;
		audio.Play();
	}

	// 채널별로 씬따로*
	public void chanel1_join () {

		if (myLv > 20) {
			popUp.SetActive(true);
			sound_Play();
		} else {
			Application.LoadLevel("Chanel_1");

		}

	}

	public void chanel2_join () {
		
		if (myLv <= 20 || myLv > 50) {
			popUp.SetActive(true);
			sound_Play();
		} else {
			Application.LoadLevel("Chanel_2");
		}
		
	}

	public void chanel3_join () {
		Application.LoadLevel("Chanel_3");
	}

	public void chanel4_join () {
		
		if (myLv < 40 || myLv > 60 || myLeader > 300) {
			popUp.SetActive(true);
			sound_Play();
		} else {
			Application.LoadLevel("Chanel_4");
		}
		
	}

	public void chanel5_join () {
		
		if (myLv < 55 || myLv > 70 || myLeader > 500) {
			popUp.SetActive(true);
			sound_Play();
		} else {
			Application.LoadLevel("Chanel_5");
		}
		
	}

	public void chanel6_join () {
		
		if (myLv <= 70) {
			popUp.SetActive(true);
			sound_Play();
		} else {
			Application.LoadLevel("Chanel_6");
		}
		
	}





















	// Update is called once per frame
	void Update () {
	
	}


}
