using UnityEngine;
using System.Collections;

public class Nameoverlap : MonoBehaviour {

	// 디비파일 이름
	private string dbName = "Users.db";
	// 디비*
	private dbAccess db;

	// 오디오
	public AudioClip okSound;
	public AudioClip noSound;
	public AudioClip createSound;

	public AudioClip atSound;
	public AudioClip trSound;
	public AudioClip comSound;
	public AudioClip milSound;
	public AudioClip polSound;
	public AudioClip sigSound;

	// 팝업
	public GameObject overPop;
	public GameObject notOverPop;
	public GameObject pleaseOver;

	public UIInput unitName; // 부대이름 입력창
	public GameObject btnOverCheck; // 중복확인 버튼
	
	int slotNumber;
	string curID;

	// Use this for initialization
	void Start () {
		Debug.Log("starting SQLiteLoad app");
		
		db = GetComponent<dbAccess>();

		curID = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("ID")));
		slotNumber = PlayerPrefs.GetInt("slotNum");
		print ("클릭한 슬롯 넘버 정보 : " + slotNumber);

	}

	// 중복확인 버튼 누르면 불릴 함수
	public void nameOverCheck () {

		pleaseOverPopClose();

		if (unitName.label.text == "") {
			print ("부대이름 입력 받은 정보 없음 아무것도 안함");
			return;
		} else {
			db.OpenDB(dbName);
			nameCheck(1);
		}

		db.CloseDB();

	} // nameOverCheck

	// 생성하기 버튼 누르면 불릴 함수
	public void createUnit () {

		overPopClose();
		notOverPopClose();
		db.OpenDB(dbName);
		nameCheck(0);
		db.CloseDB();
	}

	public void nameCheck (int visionPop) {

		string unitID;
		string typeNum = "'" + PlayerPrefs.GetString("TypeNum") + "'";

		unitID = "'"+ unitName.label.text +"'";
		ArrayList slot1IDCheck = db.SingleSelectWhere("UnitStop", "Slot1Name", "Slot1Name", "=", unitID);
		ArrayList slot2IDCheck = db.SingleSelectWhere("UnitStop", "Slot2Name", "Slot2Name", "=", unitID);
		ArrayList slot3IDCheck = db.SingleSelectWhere("UnitStop", "Slot3Name", "Slot3Name", "=", unitID);
		
		if (slot1IDCheck.Count > 0 || slot2IDCheck.Count > 0 || slot3IDCheck.Count > 0) {
			audio.clip = noSound;
			audio.Play();

			if (visionPop == 1) {
				overPop.SetActive(true);
			} else {
				pleaseOver.SetActive(true);
			}

			print ("중복된 부대이름 존재");

		} else {

			audio.clip = okSound;
			audio.Play();

			if (visionPop == 1) {
				notOverPop.SetActive(true);
			} else {
				audio.clip = createSound;
				audio.Play();
				print ("생성");
				// DB에 데이타 박고 유닛 리스트로
				// slot1 = 고유넘버, 슬롯네임은 입력한 네임
				string curId = curID;
				string insertSlotN = "'" + slotNumber + "'";

				string[] col1 = {"Slot1", "Slot1Name"};
				string[] val1 = {typeNum, unitID};
				string[] col2 = {"Slot2", "Slot2Name"};
				string[] val2 = {typeNum, unitID};
				string[] col3 = {"Slot3", "Slot3Name"};
				string[] val3 = {typeNum, unitID};

				string[] inCol = {"unit_name", "slot_num", "unit_type", 
										"ch1_force", "ch2_force", "ch3_force", "ch4_force", "ch5_force", "ch6_force"};
				string[] inVal = {unitID, insertSlotN, typeNum, "0", "0", "0", "0", "0", "0"};

				// 캐릭별 스탯을 담은 변수
				int bLeadership = PlayerPrefs.GetInt("basic_leadership");
				int bForce = PlayerPrefs.GetInt("basic_force");
				int bIntellect = PlayerPrefs.GetInt("basic_intellect");
				int bPolitical = PlayerPrefs.GetInt("basic_political");
				int bTroops = PlayerPrefs.GetInt("MAX_Troops");

				// 업뎃할 인트형 데이터를 가진 컬럼들
				string[] intUpdateCol = {"unit_named", "unit_repute", "tutorial", "unit_lv", "need_exp", "cur_exp", 
													"basic_leadership", "basic_force", "basic_intellect", "basic_political", 
													"add_leadership", "add_force", "add_intellect", "add_political", 
													"statpoint", "cur_troops", "max_troops"}; 
				int[] intUpdateData = {0, 0, 0, 1, 3000, 0,
												 bLeadership, bForce, bIntellect, bPolitical,
												 0, 0, 0, 0,
				 								 5, 5000, bTroops}; // 업뎃할 인트형 데이터들

				// 업뎃할 문자열 데이터를 가진 컬럼들
				string[] textUpdateCol = {"unit_class"};
				string[] textUpdateVal = new string[1];

				//"교위", "후장군", "사시랑", "현위", "비장군", "책사"
				// 0 빈슬롯 1 돌격형 2 통솔형 3 군악대
				// 4 내정형 5 공성형 6 책략형
				if (typeNum.Equals("'1'")) {
					textUpdateVal[0] = "'교위'";
				} else if (typeNum.Equals("'2'")) {
					textUpdateVal[0] = "'후장군'";
				} else if (typeNum.Equals("'3'")) {
					textUpdateVal[0] = "'사시랑'";
				} else if (typeNum.Equals("'4'")) {
					textUpdateVal[0] = "'현위'";
				} else if (typeNum.Equals("'5'")) {
					textUpdateVal[0] = "'비장군'";
				} else if (typeNum.Equals("'6'")) {
					textUpdateVal[0] = "'책사'";
				}
				print ("직책위한 타입넘버 = " + typeNum);
				print ("직책=======" + textUpdateVal[0]);

				if (slotNumber == 1) {
					int i = db.UpdateData("UnitStop", col1, val1, "id", curId); // 정적 부대 DB에 업데이트
					db.InsertIntoSpecific("UnitInfo", inCol, inVal); // string 타입 부대정보 DB에 먼저 인서트
					db.intTypeUpdate("UnitInfo", intUpdateCol, intUpdateData, "unit_name", unitID);
					db.UpdateData("UnitInfo", textUpdateCol, textUpdateVal, "unit_name", unitID);
					print ("슬롯1 인서트 성공 : " + i);
					Application.LoadLevel("MyUnitView");
				} else if (slotNumber == 2) {
					int i = db.UpdateData("UnitStop", col2, val2, "id", curId); // 정적 부대 DB에 업데이트
					db.InsertIntoSpecific("UnitInfo", inCol, inVal); // string 타입 부대정보 DB에 먼저 인서트
					db.intTypeUpdate("UnitInfo", intUpdateCol, intUpdateData, "unit_name", unitID);
					db.UpdateData("UnitInfo", textUpdateCol, textUpdateVal, "unit_name", unitID);
					print ("슬롯2 인서트 성공 : " + i);

					Application.LoadLevel("MyUnitView");
				} else if (slotNumber == 3) {
					int i = db.UpdateData("UnitStop", col3, val3, "id", curId); // 정적 부대 DB에 업데이트
					db.InsertIntoSpecific("UnitInfo", inCol, inVal); // string 타입 부대정보 DB에 먼저 인서트
					db.intTypeUpdate("UnitInfo", intUpdateCol, intUpdateData, "unit_name", unitID);
					db.UpdateData("UnitInfo", textUpdateCol, textUpdateVal, "unit_name", unitID);
					print ("슬롯3 인서트 성공 : " + i);
					Application.LoadLevel("MyUnitView");
				}

			}

		}

	}





	// 중복 확인 버튼들 끄기 처리
	public void overPopClose () {
		overPop.SetActive(false);
	}

	public void notOverPopClose () {
		notOverPop.SetActive(false);
	}

	public void pleaseOverPopClose () {
		pleaseOver.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
