using UnityEngine;
using System.Collections;
using System;

public class SoundControl : MonoBehaviour {

	// 디비파일 이름
	private string dbName = "Users.db";
	// 디비*
	private dbAccess db;

	public AudioClip moo_move1;
	public AudioClip moo_move2;
	public AudioClip moo_move3;
	public AudioClip moo_at1;
	public AudioClip moo_at2;
	public AudioClip moo_at3;
	public AudioClip moo_at4;
	public AudioClip moo_at5;
	public AudioClip moo_at6;
	public AudioClip moo_at7;
	public AudioClip moo_dead1;
	public AudioClip moo_dead2;
	public AudioClip moo_dead3;
	public AudioClip moo_dead4;
	public AudioClip moo_dead5;
	public AudioClip moo_dead6;

	public AudioClip com_move1;
	public AudioClip com_move2;
	public AudioClip com_move3;
	public AudioClip com_move4;
	public AudioClip com_move5;
	public AudioClip com_at1;
	public AudioClip com_at2;
	public AudioClip com_at3;
	public AudioClip com_at4;
	public AudioClip com_at5;
	public AudioClip com_at6;
	public AudioClip com_dead1;
	public AudioClip com_dead2;

	
	public AudioClip girl_move1;
	public AudioClip girl_move2;
	public AudioClip girl_move3;
	public AudioClip girl_move4;
	public AudioClip girl_move5;
	public AudioClip girl_move6;
	public AudioClip girl_move7;
	public AudioClip girl_move8;
	public AudioClip girl_at1;
	public AudioClip girl_at2;
	public AudioClip girl_at3;
	public AudioClip girl_at4;
	public AudioClip girl_at5;
	public AudioClip girl_at6;
	public AudioClip girl_at7;
	public AudioClip girl_at8;
	public AudioClip girl_at9;
	public AudioClip girl_dead1;
	public AudioClip girl_dead2;
	public AudioClip girl_dead3;
	public AudioClip girl_dead4;
	public AudioClip girl_dead5;
	public AudioClip girl_dead6;
	public AudioClip girl_dead7;
	public AudioClip girl_dead8;
	
	public AudioClip pol_move1;
	public AudioClip pol_move2;
	public AudioClip pol_move3;
	public AudioClip pol_move4;
	public AudioClip pol_move5;
	public AudioClip pol_move6;
	public AudioClip pol_at1;
	public AudioClip pol_at2;
	public AudioClip pol_at3;
	public AudioClip pol_at4;
	public AudioClip pol_at5;
	public AudioClip pol_at6;
	public AudioClip pol_dead1;
	public AudioClip pol_dead2;
	public AudioClip pol_dead3;
	public AudioClip pol_dead4;
	public AudioClip pol_dead5;
	
	public AudioClip zi_move1;
	public AudioClip zi_move2;
	public AudioClip zi_move3;
	public AudioClip zi_move4;
	public AudioClip zi_move5;
	public AudioClip zi_at1;
	public AudioClip zi_at2;
	public AudioClip zi_at3;
	public AudioClip zi_at4;
	public AudioClip zi_at5;
	public AudioClip zi_at6;
	public AudioClip zi_dead1;
	public AudioClip zi_dead2;
	public AudioClip zi_dead3;
	public AudioClip zi_dead4;
	


	public string unit_Type;
	public string curName;

	// Use this for initialization
	void Start () {
	
		db = GetComponent<dbAccess>();

		curName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("curUnitName")));

		db.OpenDB(dbName);
		unit_Type = db.stringDBReader("unit_type", "UnitInfo", "unit_name", curName);
		db.CloseDB();

	}


	// 전멸시 사운드 
	public void deathSound() {

		if (unit_Type.Equals("1") && !audio.isPlaying) {
			
			int sn = randNum(1, 7);
			if (sn == 1) {
				audio.clip = moo_dead1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = moo_dead2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = moo_dead3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = moo_dead4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = moo_dead5;
				audio.Play();
			} else if (sn == 6) {
				audio.clip = moo_dead6;
				audio.Play();
			} 
			
		} else if (unit_Type.Equals("2") && !audio.isPlaying) {
			
			int sn = randNum(1, 3);
			if (sn == 1) {
				audio.clip = com_dead1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = com_dead2;
				audio.Play();
			}
			
		} else if (unit_Type.Equals("3") && !audio.isPlaying) {
			
			int sn = randNum(1, 9);
			if (sn == 1) {
				audio.clip = girl_dead1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = girl_dead2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = girl_dead3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = girl_dead4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = girl_dead5;
				audio.Play();
			} else if (sn == 6) {
				audio.clip = girl_dead6;
				audio.Play();
			} else if (sn == 7) {
				audio.clip = girl_dead7;
				audio.Play();
			} else if (sn == 8) {
				audio.clip = girl_dead8;
				audio.Play();
			}  
			
		} else if (unit_Type.Equals("4") && !audio.isPlaying) {
			
			int sn = randNum(1, 6);
			if (sn == 1) {
				audio.clip = pol_dead1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = pol_dead2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = pol_dead3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = pol_dead4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = pol_dead5;
				audio.Play();
			} 
			
		} else if (unit_Type.Equals("5") && !audio.isPlaying) {
			
			int sn = randNum(1, 3);
			if (sn == 1) {
				audio.clip = com_dead1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = com_dead2;
				audio.Play();
			}
			
		} else if (unit_Type.Equals("6") && !audio.isPlaying) {
			
			int sn = randNum(1, 5);
			if (sn == 1) {
				audio.clip = zi_dead1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = zi_dead2;
				audio.Play();	
			} else if (sn == 3) {
				audio.clip = zi_dead3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = zi_dead4;
				audio.Play();
			} 
			
		}

	}


	// 공격시 사운드
	public void atSound() {

		if (unit_Type.Equals("1") && !audio.isPlaying) {
			
			int sn = randNum(1, 8);
			if (sn == 1) {
				audio.clip = moo_at1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = moo_at2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = moo_at3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = moo_at4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = moo_at5;
				audio.Play();
			} else if (sn == 6) {
				audio.clip = moo_at6;
				audio.Play();
			} else if (sn == 7) {
				audio.clip = moo_at7;
				audio.Play();
			}
			
		} else if (unit_Type.Equals("2") && !audio.isPlaying) {
			
			int sn = randNum(1, 7);
			if (sn == 1) {
				audio.clip = com_at1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = com_at2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = com_at3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = com_at4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = com_at5;
				audio.Play();
			} else if (sn == 6) {
				audio.clip = com_at6;
				audio.Play();
			} 
			
		} else if (unit_Type.Equals("3") && !audio.isPlaying) {
			
			int sn = randNum(1, 10);
			if (sn == 1) {
				audio.clip = girl_at1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = girl_at2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = girl_at3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = girl_at4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = girl_at5;
				audio.Play();
			} else if (sn == 6) {
				audio.clip = girl_at6;
				audio.Play();
			} else if (sn == 7) {
				audio.clip = girl_at7;
				audio.Play();
			} else if (sn == 8) {
				audio.clip = girl_at8;
				audio.Play();
			} else if (sn == 9) {
				audio.clip = girl_at9;
				audio.Play();
			} 
			
		} else if (unit_Type.Equals("4") && !audio.isPlaying) {
			
			int sn = randNum(1, 7);
			if (sn == 1) {
				audio.clip = pol_at1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = pol_at2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = pol_at3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = pol_at4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = pol_at5;
				audio.Play();
			} else if (sn == 6) {
				audio.clip = pol_at6;
				audio.Play();
			} 
			
		} else if (unit_Type.Equals("5") && !audio.isPlaying) {
			
			int sn = randNum(1, 7);
			if (sn == 1) {
				audio.clip = com_at1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = com_at2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = com_at3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = com_at4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = com_at5;
				audio.Play();
			} else if (sn == 6) {
				audio.clip = com_at6;
				audio.Play();
			} 
			
		} else if (unit_Type.Equals("6") && !audio.isPlaying) {
			
			int sn = randNum(1, 7);
			if (sn == 1) {
				audio.clip = zi_at1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = zi_at2;
				audio.Play();	
			} else if (sn == 3) {
				audio.clip = zi_at3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = zi_at4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = zi_at5;
				audio.Play();
			} else if (sn == 6) {
				audio.clip = zi_at6;
				audio.Play();
			} 
			
		}
	
	
	}


	// 이동 클릭시 사운드 발생
	public void moveSound() {

		if (unit_Type.Equals("1") && !audio.isPlaying) {

			int sn = randNum(1, 4);
			if (sn == 1) {
				audio.clip = moo_move1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = moo_move2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = moo_move3;
				audio.Play();
			}

		} else if (unit_Type.Equals("2") && !audio.isPlaying) {

			int sn = randNum(1, 6);
			if (sn == 1) {
				audio.clip = com_move1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = com_move2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = com_move3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = com_move4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = com_move5;
				audio.Play();
			} 

		} else if (unit_Type.Equals("3") && !audio.isPlaying) {

			int sn = randNum(1, 9);
			if (sn == 1) {
				audio.clip = girl_move1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = girl_move2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = girl_move3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = girl_move4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = girl_move5;
				audio.Play();
			} else if (sn == 6) {
				audio.clip = girl_move6;
				audio.Play();
			} else if (sn == 7) {
				audio.clip = girl_move7;
				audio.Play();
			} else if (sn == 8) {
				audio.clip = girl_move8;
				audio.Play();
			} 

		} else if (unit_Type.Equals("4") && !audio.isPlaying) {

			int sn = randNum(1, 7);
			if (sn == 1) {
				audio.clip = pol_move1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = pol_move2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = pol_move3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = pol_move4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = pol_move5;
				audio.Play();
			} else if (sn == 6) {
				audio.clip = pol_move6;
				audio.Play();
			} 

		} else if (unit_Type.Equals("5") && !audio.isPlaying) {

			int sn = randNum(1, 6);
			if (sn == 1) {
				audio.clip = com_move1;
				audio.Play();
			} else if (sn == 2) {
				audio.clip = com_move2;
				audio.Play();
			} else if (sn == 3) {
				audio.clip = com_move3;
				audio.Play();
			} else if (sn == 4) {
				audio.clip = com_move4;
				audio.Play();
			} else if (sn == 5) {
				audio.clip = com_move5;
				audio.Play();
			} 

		} else if (unit_Type.Equals("6") && !audio.isPlaying) {

			int sn = randNum(1, 6);
			if (sn == 1) {
				audio.clip = zi_move1;
				audio.Play();

			} else if (sn == 2) {
				audio.clip = zi_move2;
				audio.Play();

			} else if (sn == 3) {
				audio.clip = zi_move3;
				audio.Play();

			} else if (sn == 4) {
				audio.clip = zi_move4;
				audio.Play();

			} else if (sn == 5) {
				audio.clip = zi_move5;
				audio.Play();

			} 

		}

	}// movesound


	
	// 랜덤 숫자 발생
	public int randNum(int minN, int maxN) {	
		System.Random rd = new System.Random(DateTime.Now.Millisecond);
		int soundNum = 0;
		soundNum = rd.Next(minN, maxN);
		return soundNum;
	}





}
