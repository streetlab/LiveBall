using UnityEngine;
using System.Collections;

public class CursorColl : MonoBehaviour {

	public UnitAction ua;
	public GameObject userUnit;

	public GameObject colliderBeShot;
	public Transform at_Point;

	public bool at_bool = false;

	// Use this for initialization
	void Start () {
		at_bool = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col) {
		if (ua.us != UnitState.DEAD) {

			if (col.gameObject.tag == "computer" && Input.GetMouseButtonDown(1)) {
				
				if(Vector2.Distance(userUnit.transform.position, col.transform.position) >= 0.95f) {
					
					ua.us = UnitState.WALK;
					ua.speed = 0.1f;
					ua.gdClick();
					ua.unitRotateAnim();
					ua.sc.moveSound();

				} else {
					// 0.8보 다가까우 면그 냥공 격
					if (!at_bool) {
						at_bool = true;
						at_ok();
					}

				}
	
			} 


		} // DEAD End

	} // Colli End

	public void at_ok() {

		if (at_bool) {
			ua.us = UnitState.ATTACK;
			ua.speed = 0f;
			ua.sc.atSound ();
			ua.anim.SetTrigger ("ATTACK");
			StartCoroutine (beShot ());
		} else {
			return;
		}

	}


	public float atBox_Speed = 20f;

	public IEnumerator beShot() {

		GameObject attack_beShot = Instantiate (colliderBeShot) as GameObject;
		attack_beShot.transform.position = userUnit.transform.position;
		attack_beShot.transform.position =  Vector2.Lerp(userUnit.transform.position, transform.position, atBox_Speed * Time.deltaTime);
		yield return new WaitForSeconds (0.15f);
		
	}








}
