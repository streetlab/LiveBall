using UnityEngine;
using System.Collections;

public class DanageText_do : MonoBehaviour {

	public float damage_T_S_do = 2f; // 라벨 텍스트 애니메이션 스피드
	public GameObject move_Point_do; // 라벨이 이동할 위치를 가진 오브젝트

	// Use this for initialization
	void Start () {
	
		move_Point_do = GameObject.Find("Move_Point_do") as GameObject;

	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = 
			Vector2.Lerp(transform.position, move_Point_do.transform.position, damage_T_S_do * Time.deltaTime);

	}

	void OnTriggerEnter2D (Collider2D col) {
		
		if (col.gameObject.tag == "damageText_do") {
			StartCoroutine(destDamText_do());
		}
		
	}
	
	IEnumerator destDamText_do() {
		
		yield return new WaitForSeconds(1);
		Destroy(this.gameObject);
		
	}






}
