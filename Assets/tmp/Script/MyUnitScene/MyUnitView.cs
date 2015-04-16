using UnityEngine;
using System.Collections;

public class MyUnitView : MonoBehaviour {

	// 디비파일 이름
	private string dbName = "Users.db";
	// 디비*
	private dbAccess db;

	// 현재 접속자
	public string curID;

	// 부대별 프리펩
	public GameObject slot;
	public GameObject attacker;
	public GameObject command;
	public GameObject milBand;
	public GameObject political;
	public GameObject siege;
	public GameObject trick;

	public UILabel slot1_unitLv;
	public UILabel slot1_unitName;
	public UILabel slot2_unitLv;
	public UILabel slot2_unitName;
	public UILabel slot3_unitLv;
	public UILabel slot3_unitName;

	public UnitView swController;
	

	string id = "";
	string unitName1 = "";
	string unitName2 = "";
	string unitName3 = "";
	string unitTypeSlot1 = "0"; // def 0 = 부대없음
	string unitTypeSlot2 = "0";
	string unitTypeSlot3 = "0";

	string dbName1;
	string dbName2;
	string dbName3;

	string returnSlot1_Name;
	string returnSlot2_Name;
	string returnSlot3_Name;

	void Start () {
				
		db = GetComponent<dbAccess>(); // 디비 스크립트 연결

		curID = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("ID")));
		print ("현재 접속된 아이디 = " + curID);

		id = curID; //    "   '   " + joinId.label.text + "   '   ";
		unitName1 = "'"+ unitName1 +"'"; // 캐릭터 생성했을 때
		unitName2 = "'"+ unitName2 +"'";
		unitName3 = "'"+ unitName3 +"'";
		unitTypeSlot1 = "'" + unitTypeSlot1 + "'";
		unitTypeSlot2 = "'" + unitTypeSlot2 + "'";
		unitTypeSlot3 = "'" + unitTypeSlot3 + "'";


		db.OpenDB(dbName);
		ArrayList userID = db.SingleSelectWhere("UnitStop", "id", "id", "=", curID);

		if (userID.Count > 0) {
			// 보유 부대 뿌려주기
			// 0 빈슬롯 1 돌격형 2 통솔형 3 군악대
			// 4 내정형 5 공성형 6 책략형
			int slot1Unit = db.slot1Unit("UnitStop", curID);
			int slot2Unit = db.slot2Unit("UnitStop", curID);
			int slot3Unit = db.slot3Unit("UnitStop", curID);
			print ("슬롯1의 부대타입 : " + slot1Unit);
			print ("슬롯2의 부대타입 : " + slot2Unit);
			print ("슬롯3의 부대타입 : " + slot3Unit);

			// 현재 접속중인 사용자 아이디의 각 슬롯의 부대이름 정보 얻어오기
			returnSlot1_Name = db.stringDBReader("Slot1Name", "UnitStop", "id", id);
			returnSlot2_Name = db.stringDBReader("Slot2Name", "UnitStop", "id", id);
			returnSlot3_Name = db.stringDBReader("Slot3Name", "UnitStop", "id", id);

			// 부대이름
			slot1_unitName.text = returnSlot1_Name;
			slot2_unitName.text = returnSlot2_Name;
			slot3_unitName.text = returnSlot3_Name;

			// DB로 보낼 문자열을 쿼리 작업화 과정
			dbName1 = "'" + returnSlot1_Name + "'";
			dbName2 = "'" + returnSlot2_Name + "'";
			dbName3 = "'" + returnSlot3_Name + "'";

			// 위에 코드에서 찾아온 부대이름으로 해당부대 레벨 정보 얻어오기
			int returnSlot1_Lv = db.intDBReader("unit_lv", "UnitInfo", "unit_name", dbName1);
			int returnSlot2_Lv = db.intDBReader("unit_lv", "UnitInfo", "unit_name", dbName2);
			int returnSlot3_Lv = db.intDBReader("unit_lv", "UnitInfo", "unit_name", dbName3);

			// 부대레벨
			slot1_unitLv.text = returnSlot1_Lv.ToString();
			slot2_unitLv.text = returnSlot2_Lv.ToString();
			slot3_unitLv.text = returnSlot3_Lv.ToString();

			// UI On Off
			swController.switchUnitView1(slot1Unit);
			swController.switchUnitView2(slot2Unit);
			swController.switchUnitView3(slot3Unit);

		} else {
			// 부대생성 UI 뿌려주기 ?
			string[] newInsert = {id, unitTypeSlot1, unitName1, unitTypeSlot2, unitName2, unitTypeSlot3, unitName3};
			int newUser = db.InsertInto("UnitStop", newInsert);

			// 클론으로 앵커안에 오브젝트 생성하는것 보류
			//GameObject cp = Instantiate(slot) as GameObject; 
			//cp.transform.parent = GameObject.Find("Anchor").transform; // 오브젝트 생성 위치
			// cp 프리펩 안에 있는 이미지 버튼들을 셋액티브 조절 어케하는가 ?

			swController.slot1_Empty.SetActive(true);
			swController.slot2_Empty.SetActive(true);
			swController.slot3_Empty.SetActive(true);
			print ("인서트 성공 : " + newUser);

		}

		db.CloseDB();


	}

	public void userLogout() {
		// 로그아웃 처리후 씬이동
		print("로그아웃 되었습니다.");
		Application.LoadLevel("SSamMain");		
	}

	// 부대생성 클릭
	public void CreateUnit1() {
		Application.LoadLevel("UnitTypeSelect");
		PlayerPrefs.SetInt("slotNum", 1);
	}

	public void CreateUnit2() {
		Application.LoadLevel("UnitTypeSelect");
		PlayerPrefs.SetInt("slotNum", 2);
	}

	public void CreateUnit3() {
		Application.LoadLevel("UnitTypeSelect");
		PlayerPrefs.SetInt("slotNum", 3);
	}

	public void slot1Start() {
		gameStart(dbName1);
	}

	public void slot2Start() {
		gameStart(dbName2);
	}

	public void slot3Start() {
		gameStart(dbName3);
	}

	// 캐릭터 더블클릭 하여 게임 입장
	public void gameStart(string tutoForName) {
		// 튜토가0이면 튜토리얼 씬으로 이동, 1이면 시나리오 선택 화면으로 이동
		db.OpenDB(dbName);
		int tuto = db.intDBReader("tutorial", "UnitInfo", "unit_name", tutoForName);

		if (tuto == 1) {
			Application.LoadLevel("ChanelView");
			string myName = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(tutoForName));
			PlayerPrefs.SetString("curUnitName", myName);
		} else if (tuto == 0) {
			// 튜토리얼 씬 로드
			string myName = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(tutoForName));
			PlayerPrefs.SetString("curUnitName", myName);
			Application.LoadLevel("tuto");
		}
	}



	// Update is called once per frame
	void Update () {
	
	}
}
