using UnityEngine;
using System.Collections;

public class DamageText : MonoBehaviour {
	
	public float damage_T_S = 2f; // 라벨 텍스트 애니메이션 스피드
	public GameObject move_Point; // 라벨이 이동할 위치를 가진 오브젝트

	// Use this for initialization
	void Start () {
	
		move_Point = GameObject.Find("Move_Point") as GameObject;



	}
	
	// Update is called once per frame
	void Update () {
		transform.position = 
			Vector2.Lerp(transform.position, move_Point.transform.position, damage_T_S * Time.deltaTime);
	}


	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "damageText") {
			StartCoroutine(destDamText());
		}

	}

	IEnumerator destDamText() {

		yield return new WaitForSeconds(1);
		Destroy(this.gameObject);

	}




}
