using UnityEngine;
using System.Collections;
using System;

public enum DO_STATE {

	IDLE,
	WALK,
	BESHOT,
	ATTACK,
	DEAD

}

// 도적 이동위치에 따라 좌우 애니메이션 시행하기

public class Dozeok : MonoBehaviour {

	public UILabel do_name;
	public UILabel do_force;
	public UISlider do_HPBar;
	public UILabel do_HPText;

	public GameObject ob_name;
	public GameObject ob_force;
	public GameObject ob_HPBar;
	public GameObject ob_HPText;

	public float do_curHP;
	public float do_maxHP;
	
	public float do_speed = 0.1f;
	public GameObject userUnit;
	public GameObject tuto;
	public DO_STATE ds;

	public Animator anim;
	public bool do_At_bool = false;

	public AudioClip do_DEAD_Sound;

	void Start() {

		userUnit = GameObject.Find("Unit") as GameObject;
		do_curHP = 500;
		do_maxHP = 500;
		do_HPBar.sliderValue = do_curHP/do_maxHP;
		do_HPText.text = do_curHP.ToString() + " / " + do_maxHP.ToString();
		do_At_bool = false;

	}


	void Update() {

		dozeokState();

	}

	// 이스크립트를 부착하면 시야에 부딪히는 오브젝트를 끄고 킴

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "see") {
			GetComponent<SpriteRenderer>().enabled = true;
			ob_HPBar.SetActive(true);
			ob_HPText.SetActive(true);
			ob_name.SetActive(true);
			ob_force.SetActive(true);

		}
		
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "see") {
			GetComponent<SpriteRenderer>().enabled = false;
			ob_HPBar.SetActive(false);
			ob_HPText.SetActive(false);
			ob_name.SetActive(false);
			ob_force.SetActive(false);

		}
	}

	public void dozeokState() {
		// 이동
		if (ds != DO_STATE.DEAD) {
			if (Vector2.Distance(transform.position, userUnit.transform.position) <= 2.5f && Vector2.Distance(transform.position, userUnit.transform.position) > 0.95f) {
				do_speed = 0.1f;
				ds = DO_STATE.WALK;
				transform.position = Vector2.Lerp(transform.position, userUnit.transform.position, do_speed * Time.deltaTime);

				if (transform.position.x < userUnit.transform.position.x) {
					anim.SetTrigger("right_walk");
				} else if (transform.position.x > userUnit.transform.position.x) {
					anim.SetTrigger("left_walk");
				}

			} 
			
			// 공격
			if (Vector2.Distance (transform.position, userUnit.transform.position) <= 0.95f) {

				if (!do_At_bool) {
					ds = DO_STATE.ATTACK;
					do_speed = 0f;
					anim.SetTrigger("ATTACK");
					do_At_bool = true;
					userUnit.GetComponent<UnitAction>().shot(50f); // ****** 자주 쓰는것
					StartCoroutine(do_beShot());


				} 

			}


			if(ds != DO_STATE.DEAD) {
				
				if (do_curHP <= 0) {
					
					tuto.GetComponent<TutoCon>().stage6CntUp();
					int randGold = randNum(100, 500);
					int randNamed = randNum(5, 10);
					audio.clip = do_DEAD_Sound;
					audio.Play();
					anim.SetTrigger("DEAD");
					ds = DO_STATE.DEAD;
					do_curHP = 0f;
					userUnit.GetComponent<UnitAction>().expUp(1100f); // 경험치 획득
					userUnit.GetComponent<UnitAction>().goldUp(randGold); // 군자금 획득
					userUnit.GetComponent<UnitAction>().namedUp(randNamed); // 명성 획득
					StartCoroutine("do_Destroy");
				}
				
			}

		}



	}

	public IEnumerator do_beShot() {
		
		yield return new WaitForSeconds (2);
		do_At_bool = false;
		
	}
	
	IEnumerator do_Destroy() {

		yield return new WaitForSeconds(0.8f);
		Destroy(this.gameObject);
		
	}

	// 중간 선언부 ----------------------------------------------------------------

	public float atBox_Speed = 20f;
	public AudioClip shot_sound;
	
	public void dozeokDamage(float dam) {

		if (!audio.isPlaying) {
			audio.clip = shot_sound;
			audio.Play();
		}



		if (do_curHP > 0) {
			do_curHP -= dam;
			do_HPBar.sliderValue = do_curHP/do_maxHP;
			do_HPText.text = do_curHP.ToString() + " / " + do_maxHP.ToString();
		} else if (do_curHP <= 0) {
			do_curHP = 0;
			do_HPBar.sliderValue = do_curHP/do_maxHP;
			do_HPText.text = do_curHP.ToString() + " / " + do_maxHP.ToString();
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
