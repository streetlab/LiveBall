using UnityEngine;
using System.Collections;

public class Beshot_Move : MonoBehaviour {

	public Animator shotAnim;

	public CursorColl cc;

	public string curName;
	public float damagePower;

	private string dbName = "Users.db";
	private dbAccess db;

	int read_basicTong;
	int read_basicMoo;
	int read_addTong;
	int read_addMoo;
	int curTroops;

	int plus_Tong;
	int plus_Moo;
	int addDamage;

	int plus1_damage;
    
	// Use this for initialization
	void Start () {
		cc = GameObject.Find("Cursur").GetComponent("CursorColl") as CursorColl;
		db = GetComponent<dbAccess>(); // 디비 스크립트 연결
		curName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("curUnitName")));

		db.OpenDB(dbName);
		read_basicTong = db.intDBReader("basic_leadership", "UnitInfo", "unit_name ", curName);
		read_basicMoo = db.intDBReader("basic_force", "UnitInfo", "unit_name ", curName);
		read_addTong = db.intDBReader("add_leadership", "UnitInfo", "unit_name ", curName);
		read_addMoo = db.intDBReader("add_force", "UnitInfo", "unit_name ", curName);
		curTroops = db.intDBReader("cur_troops", "UnitInfo", "unit_name", curName);

		plus_Tong =  read_basicTong + read_addTong;
		plus_Moo = read_basicMoo + read_addMoo;
		addDamage = plus_Moo - plus_Tong;
		if (addDamage <= 0) {
			addDamage = 0;
		}

		plus1_damage = curTroops / 100;
		//현재병력의 1% + 무력 + 추가공격력
		damagePower = plus1_damage + plus_Moo + addDamage;
		db.CloseDB();

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "computer") {
			shotAnim.SetTrigger("Coll_OK");
			Dozeok dozeok = col.gameObject.GetComponent<Dozeok>();
			dozeok.dozeokDamage(damagePower);
			print ("나의데미지 " + damagePower);
		} 

	}

	public void beshotDel() {

		cc.at_bool = false;
		Destroy(this.gameObject);

	}

	void OnTriggerExit2D (Collider2D col) {

		if (col.gameObject.tag != "computer" && col.gameObject.tag != "Unit") {
			StartCoroutine(beshotDel2());
		}

	}

	IEnumerator beshotDel2() {

		yield return new WaitForSeconds(3);
		cc.at_bool = false;
		Destroy(this.gameObject);
		
	}


}
