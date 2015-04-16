using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	private string dbName = "Users.db";
	private dbAccess db;

	public GameObject option_Pop;
	public TutoCon tc;

	public string face_type;
	public string curName;
	// 얼굴 이미지 버튼
	public GameObject faceAT;
	public GameObject faceCOM;
	public GameObject faceMIL;
	public GameObject facePOL;
	public GameObject faceSIG;
	public GameObject faceTRICK;
	// 상단 자산정보
	public UILabel cash;
	public UILabel mark;
	public UILabel gold;
	public UILabel unitName;

	public int myCash;
	public int myMark;
	public int myGold;
	public string curID;

	// 레벨,게이지바
	public UILabel unitLv;
	public UILabel troopsText;
	public UILabel expText;
	public UISlider troopsBar;
	public UISlider expBar;
	public float curTroops;
	public float maxTroops;
	public float curExp;
	public float needExp;

	public GameObject statDownBtn; // 펼치기 버튼
	public GameObject statUI; // 스탯정보창 UI
	public GameObject statUpBtn; // 스탯정보창 접기 버튼
	public GameObject statUI_Info; // 스탯정보창 UI의 상세정보

	public UILabel userInfo;
	public UILabel userForce;
	
	void Start () {
		
		db = GetComponent<dbAccess>(); // 디비 스크립트 연결
		curName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("curUnitName")));	
		curID = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("ID")));
		db.OpenDB(dbName);
		up_Barset();
		unitStateUI();
		face_type = db.stringDBReader("unit_type", "UnitInfo", "unit_name", curName);
		unitStatSet();
		db.CloseDB();
		// 0 빈슬롯 1 돌격형 2 통솔형 3 군악대
		// 4 내정형 5 공성형 6 책략형
		if (face_type.Equals("1")) {
			faceAT.SetActive(true);
		} else if (face_type.Equals("2")) {
			faceCOM.SetActive(true);
		} else if (face_type.Equals("3")) {
			faceMIL.SetActive(true);
		} else if (face_type.Equals("4")) {
			facePOL.SetActive(true);
		} else if (face_type.Equals("5")) {
			faceSIG.SetActive(true);
		} else if (face_type.Equals("6")) {
			faceTRICK.SetActive(true);
		}

	}

	// 상단바 텍스트 정보 셋팅
	public void up_Barset() {
		// 현재 접속중인 부대 이름을 불러와서
		curName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("curUnitName")));
		// text에 '   '가 표시되는걸 없애는 DB작업
		string unitString = db.stringDBReader("unit_name", "UnitInfo", "unit_name ", curName);
		int lv = db.intDBReader("unit_lv", "UnitInfo", "unit_name", curName);
		// 캐시,훈장,골드의 갯수 읽어오기
		myCash = db.intDBReader("cash", "userGold", "id", curID);
		myMark = db.intDBReader("mark", "userGold", "id", curID);
		myGold = db.intDBReader("gold", "userGold", "id", curID);
		// 캐시,훈장,골드,아이디 text셋팅
		cash.text = myCash.ToString();
		mark.text = myMark.ToString();
		gold.text = myGold.ToString();
		unitName.text = unitString;

		userInfo.text = "[Lv." + lv + " " + unitString + "]";  // 부대 따라다니는 상태UI

	}

	// 부대상태정보 UI 셋팅
	public void unitStateUI() {
		// 부대레벨
		int lv = db.intDBReader("unit_lv", "UnitInfo", "unit_name", curName);
		unitLv.text = lv.ToString();
		// 병력바(hp)
		maxTroops = db.intDBReader("max_troops", "UnitInfo", "unit_name", curName);
		curTroops = db.intDBReader("cur_troops", "UnitInfo", "unit_name", curName);
		troopsBar.sliderValue = curTroops/maxTroops;
		troopsText.text = curTroops.ToString() + " / " + maxTroops.ToString();
		// 경험치바
		needExp = db.intDBReader("need_exp", "UnitInfo", "unit_name", curName);
		curExp = db.intDBReader("cur_exp", "UnitInfo", "unit_name", curName);
		expBar.sliderValue = curExp/needExp;
		expText.text = curExp.ToString() + " / " + needExp.ToString();

	}

	// 스탯창 펼치기
	public void uiStatDown() {

		if (tc.ts == TutoState.CLEAR || tc.ts == TutoState.STAGE2 || tc.ts == TutoState.STAGE6) {
			statDownBtn.SetActive(false);
			statUI.SetActive(true);
			statUpBtn.SetActive(true);
			statUI_Info.SetActive(true);
		}

		// 튜토리얼 용
		if (tc.ts == TutoState.STAGE2) {
			tc.stage2Cnt = 1;
		}

	}
	// 스탯창 접기
	public void uiStatUp() {

		if (tc.ts == TutoState.CLEAR || tc.ts == TutoState.STAGE4 || tc.ts == TutoState.STAGE6) {
			statDownBtn.SetActive(true);
			statUI.SetActive(false);
			statUpBtn.SetActive(false);
			statUI_Info.SetActive(false);
		}

		if (tc.ts == TutoState.STAGE4) {
			tc.stage4Cnt = 1;
		}

	}

	//public GameObject statDownBtn; // 펼치기 버튼
	//public GameObject statUI; // 스탯정보창 UI
	//public GameObject statUpBtn; // 스탯정보창 접기 버튼
	//public GameObject statUI_Info; // 스탯정보창 UI의 상세정보

	public UILabel className; // 직위이름
	public UILabel statPoint; // 스탯포인트
	public UILabel unitType; // 부대유형

	public GameObject Btn_Tong;
	public GameObject Btn_Moo; // 무력 올리기
	public GameObject Btn_Zi; // 지력 
	public GameObject Btn_Pol; // 정치 
	 // 통솔

	// 기본 능력치와 추가능력치 라벨
	public UILabel basic_Tong;
	public UILabel basic_Moo;
	public UILabel basic_Zi;
	public UILabel basic_Pol;

	public UILabel add_Tong;
	public UILabel add_Moo;
	public UILabel add_Zi;
	public UILabel add_Pol;

	// 명성과 평판 수치
	public UILabel unitNamed;
	public UILabel unitRepute;

	// DB정보 담을 변수들
	string read_unitClass; // 직위정보 
	int read_statPoint; // 보너스 스탯
	// 기본능력치 & 추가능력치
	int read_basicTong;
	int read_basicMoo;
	int read_basicZi;
	int read_basicPol;
	int read_addTong;
	int read_addMoo;
	int read_addZi;
	int read_addPol;
	int read_Named;
	int read_Repute;

	public AudioClip notPoint;
	public AudioClip okPoint;

	public void unitStatSet() {

		// DB로부터 정보 읽어오기
		read_unitClass = db.stringDBReader("unit_class", "UnitInfo", "unit_name ", curName);
		read_statPoint = db.intDBReader("statpoint", "UnitInfo", "unit_name ", curName);

		read_basicTong = db.intDBReader("basic_leadership", "UnitInfo", "unit_name ", curName);
		read_basicMoo = db.intDBReader("basic_force", "UnitInfo", "unit_name ", curName);
		read_basicZi = db.intDBReader("basic_intellect", "UnitInfo", "unit_name ", curName);
		read_basicPol = db.intDBReader("basic_political", "UnitInfo", "unit_name ", curName);

		read_addTong = db.intDBReader("add_leadership", "UnitInfo", "unit_name ", curName);
		read_addMoo = db.intDBReader("add_force", "UnitInfo", "unit_name ", curName);
		read_addZi = db.intDBReader("add_intellect", "UnitInfo", "unit_name ", curName);
		read_addPol = db.intDBReader("add_political", "UnitInfo", "unit_name ", curName);

		read_Named = db.intDBReader("unit_named", "UnitInfo", "unit_name ", curName);
		read_Repute = db.intDBReader("unit_repute ", "UnitInfo", "unit_name ", curName);
		// 라벨 텍스트 셋팅
		className.text = read_unitClass;
		statPoint.text = "+ " + read_statPoint.ToString();
		basic_Tong.text = read_basicTong.ToString();
		basic_Moo.text = read_basicMoo.ToString();
		basic_Zi.text = read_basicZi.ToString();
		basic_Pol.text = read_basicPol.ToString();
		add_Tong.text = "(+" + read_addTong.ToString() +")";
		add_Moo.text = "(+" + read_addMoo.ToString() +")";
		add_Zi.text = "(+" + read_addZi.ToString() +")";
		add_Pol.text = "(+" + read_addPol.ToString() +")";
		unitNamed.text = read_Named.ToString();
		unitRepute.text = read_Repute.ToString();
		// 부대타입 셋팅
		if (face_type.Equals("1")) {
			unitType.text = "돌격형";
		} else if (face_type.Equals("2")) {
			unitType.text = "통솔형";
		} else if (face_type.Equals("3")) {
			unitType.text = "군악대";
		} else if (face_type.Equals("4")) {
			unitType.text = "내정형";
		} else if (face_type.Equals("5")) {
			unitType.text = "공성형";
		} else if (face_type.Equals("6")) {
			unitType.text = "책략형";
		}

	}

	// 스탯 포인트 있을때 능력치 버튼 누르면 해당 능력치 증가되는 함수들
	public void tongClick() {

		if (tc.ts == TutoState.CLEAR || tc.ts == TutoState.STAGE3 || tc.ts == TutoState.STAGE6) {

			if (read_statPoint > 0) {
				string[] col = {"statpoint", "basic_leadership"};
				string[] col2 = {"max_troops"};
				db.OpenDB(dbName);
				read_statPoint--;
				read_basicTong++;
				maxTroops += 1000;
				int[] val = {read_statPoint, read_basicTong};
				float[] val2 = {maxTroops};
				print ("눌렀을때 포인트/무력" + read_statPoint + read_basicTong + maxTroops);
				db.intTypeUpdate("UnitInfo", col, val, "unit_name", curName);
				db.floatTypeUpdate("UnitInfo", col2, val2, "unit_name", curName);
				basic_Tong.text = read_basicTong.ToString();
				statPoint.text = "+ " + read_statPoint.ToString();
				curTroops = db.intDBReader("cur_troops", "UnitInfo", "unit_name", curName);
				troopsBar.sliderValue = curTroops/maxTroops;
				troopsText.text = curTroops.ToString() + " / " + maxTroops.ToString();
				db.CloseDB();
				btnSound(okPoint);
			} else {
				btnSound(notPoint);
				print ("능력치 증가를 위한 스탯포인트 없음");
			}

		}

		if (tc.ts == TutoState.STAGE3) {
			tc.stage3Cnt++;
		}

	} // tong

	public void mooClick() {

		if (tc.ts == TutoState.CLEAR || tc.ts == TutoState.STAGE3 || tc.ts == TutoState.STAGE6) {

			if (read_statPoint > 0) {
				string[] col = {"statpoint", "basic_force"};
				db.OpenDB(dbName);
				read_statPoint--;
				read_basicMoo++;
				int[] val = {read_statPoint, read_basicMoo};
				print ("눌렀을때 포인트/무력" + read_statPoint + read_basicMoo);
				db.intTypeUpdate("UnitInfo", col, val, "unit_name", curName);
				basic_Moo.text = read_basicMoo.ToString();
				statPoint.text = "+ " + read_statPoint.ToString();
				db.CloseDB();
				btnSound(okPoint);
			} else {
				btnSound(notPoint);
				print ("능력치 증가를 위한 스탯포인트 없음");
			}

		}

		if (tc.ts == TutoState.STAGE3) {
			tc.stage3Cnt++;
		}
	} // moo

	public void ziClick() {

		if (tc.ts == TutoState.CLEAR || tc.ts == TutoState.STAGE3 || tc.ts == TutoState.STAGE6) {

			if (read_statPoint > 0) {
				string[] col = {"statpoint", "basic_intellect"};
				db.OpenDB(dbName);
				read_statPoint--;
				read_basicZi++;
				int[] val = {read_statPoint, read_basicZi};
				print ("눌렀을때 포인트/무력" + read_statPoint + read_basicZi);
				db.intTypeUpdate("UnitInfo", col, val, "unit_name", curName);
				basic_Zi.text = read_basicZi.ToString();
				statPoint.text = "+ " + read_statPoint.ToString();
				db.CloseDB();
				btnSound(okPoint);
			} else {
				btnSound(notPoint);
				print ("능력치 증가를 위한 스탯포인트 없음");
			}

		}

		if (tc.ts == TutoState.STAGE3) {
			tc.stage3Cnt++;
		}
	}

	public void polClick() {

		if (tc.ts == TutoState.CLEAR || tc.ts == TutoState.STAGE3 || tc.ts == TutoState.STAGE6) {

			if (read_statPoint > 0) {
				string[] col = {"statpoint", "basic_political"};
				db.OpenDB(dbName);
				read_statPoint--;
				read_basicPol++;
				int[] val = {read_statPoint, read_basicPol};
				print ("눌렀을때 포인트/무력" + read_statPoint + read_basicPol);
				db.intTypeUpdate("UnitInfo", col, val, "unit_name", curName);
				basic_Pol.text = read_basicPol.ToString();
				statPoint.text = "+ " + read_statPoint.ToString();
				db.CloseDB();
				btnSound(okPoint);
			} else {
				btnSound(notPoint);
				print ("능력치 증가를 위한 스탯포인트 없음");
			}

		}

		if (tc.ts == TutoState.STAGE3) {
			tc.stage3Cnt++;
		}
	}

	public void btnSound(AudioClip ok_no) {
		audio.clip = ok_no;
		audio.Play ();
	}


	// 전투

	public UILabel damage_Num; // 데미지 입은 숫자 게임화면에 표시
	public GameObject damage_Text; // 위의 라벨을 담은 오브젝트
	public GameObject create_Point; // 라벨이 생성될 위치를 가진 오브젝트 

	public UILabel exp_Num;
	public GameObject exp_Text;

	public UILabel lvUp_Num;
	public GameObject lvUp_Text;

	public AudioClip lv_Up_Sound;// 레벨업 사운드

	public UnitAction ua;


	// 피격 데미지 계산
	public void unitDamage(float dam) {

		if (curTroops > 0) {
			damage_Num.text = "- " + dam.ToString();
			GameObject dt = Instantiate(damage_Text) as GameObject;
			dt.transform.parent = GameObject.Find("Unit").transform;
			dt.transform.position = create_Point.transform.position;
			
			db.OpenDB(dbName);
			curTroops = db.intDBReader("cur_troops", "UnitInfo", "unit_name", curName);
			curTroops -= dam;
			string[] col = {"cur_troops"};
			float[] val = {curTroops};
			troopsBar.sliderValue = curTroops/maxTroops;
			troopsText.text = curTroops.ToString() + " / " + maxTroops.ToString();
			db.floatTypeUpdate("UnitInfo", col, val, "unit_name", curName);
			db.CloseDB();
		} else if (curTroops <= 0) {
			curTroops = 0;
			string[] col = {"cur_troops"};
			float[] val = {curTroops};
			troopsBar.sliderValue = curTroops/maxTroops;
			troopsText.text = curTroops.ToString() + " / " + maxTroops.ToString();
			db.floatTypeUpdate("UnitInfo", col, val, "unit_name", curName);
			ua.unitDEAD();
			db.CloseDB();
		}


	}

	// 경험치 계산
	public void unitExp(float exp) {

		exp_Num.text = "+Exp " + exp.ToString();
		GameObject et = Instantiate(exp_Text) as GameObject;
		et.transform.parent = GameObject.Find("Unit").transform;
		et.transform.position = create_Point.transform.position;

		db.OpenDB(dbName);
		curExp = db.intDBReader("cur_exp", "UnitInfo", "unit_name", curName);
		needExp = db.intDBReader("need_exp ", "UnitInfo", "unit_name", curName);
		curExp += exp;

		string[] col = {"cur_exp"};
		float[] val = {curExp};
		expBar.sliderValue = curExp/needExp;
		expText.text = curExp.ToString() + " / " + needExp.ToString();
		db.floatTypeUpdate("UnitInfo", col, val, "unit_name", curName);

		string[] lv_col = {"unit_lv", "cur_exp", "need_exp", 
								"basic_leadership", "basic_force", "basic_intellect", "basic_political",
								"statpoint", "max_troops"};
		curExp = db.intDBReader("cur_exp", "UnitInfo", "unit_name", curName);
		needExp = db.intDBReader("need_exp ", "UnitInfo", "unit_name", curName);
		// 경험치가 필요경험치보다 같거나 커지면 레벨업
		if (curExp >= needExp) {

			// DB 정보 읽기
			int lv = db.intDBReader("unit_lv ", "UnitInfo", "unit_name", curName);
			curExp = db.intDBReader("cur_exp", "UnitInfo", "unit_name", curName);
			needExp = db.intDBReader("need_Exp", "UnitInfo", "unit_name", curName);
			read_basicTong = db.intDBReader("basic_leadership", "UnitInfo", "unit_name", curName);
			read_basicMoo = db.intDBReader("basic_force", "UnitInfo", "unit_name", curName);
			read_basicZi = db.intDBReader("basic_intellect", "UnitInfo", "unit_name", curName);
			read_basicPol = db.intDBReader("basic_political", "UnitInfo", "unit_name", curName);
			read_statPoint = db.intDBReader("statpoint", "UnitInfo", "unit_name", curName);
			maxTroops = db.intDBReader("max_troops", "UnitInfo", "unit_name", curName);

			// 데이터 변환 작업
			lv += 1;
			curExp = curExp - needExp;
			float update_needExp = needExp * 2;
			int addToTong = updateTong();
			int addToMoo = updateMoo();
			int addToZi = updateZi();
			int addToPol = updatePol();

			read_basicTong += addToTong;
			read_basicMoo += addToMoo;
			read_basicZi += addToZi;
			read_basicPol += addToPol;
			read_statPoint += 5;
			maxTroops += addToTong * 1000;

			// 바뀐 데이터의 밸류값 선언
			float[] lv_val = {lv, curExp, update_needExp, 
				read_basicTong, read_basicMoo, read_basicZi, read_basicPol, read_statPoint, maxTroops};
			// DB 업데이트
			db.floatTypeUpdate("UnitInfo", lv_col, lv_val, "unit_name", curName);


			// 바뀐 정보 UI 업데이트
			unitLv.text = lv.ToString();
			string unitString = db.stringDBReader("unit_name", "UnitInfo", "unit_name ", curName);
			userInfo.text = "[Lv." + lv + " " + unitString + "]";  // 부대 따라다니는 상태UI

			statPoint.text = "+ " + read_statPoint.ToString();
			basic_Tong.text = read_basicTong.ToString();
			basic_Moo.text = read_basicMoo.ToString();
			basic_Zi.text = read_basicZi.ToString();
			basic_Pol.text = read_basicPol.ToString();

			maxTroops = db.intDBReader("max_troops", "UnitInfo", "unit_name", curName);
			curTroops = db.intDBReader("cur_troops", "UnitInfo", "unit_name", curName);
			troopsBar.sliderValue = curTroops/maxTroops;
			troopsText.text = curTroops.ToString() + " / " + maxTroops.ToString();
			// 경험치바
			needExp = db.intDBReader("need_exp", "UnitInfo", "unit_name", curName);
			curExp = db.intDBReader("cur_exp", "UnitInfo", "unit_name", curName);
			expBar.sliderValue = curExp/needExp;
			expText.text = curExp.ToString() + " / " + needExp.ToString();

			audio.clip = lv_Up_Sound;
			audio.Play();

			lvUp_Num.text = "부대레벨 상승!!";
			GameObject lt = Instantiate(lvUp_Text) as GameObject;
			lt.transform.parent = GameObject.Find("Unit").transform;
			lt.transform.position = create_Point.transform.position;

		}

		db.CloseDB();


		if (tc.ts == TutoState.STAGE5) {
			tc.stage5Cnt = 1;
		}

	}

	public void unitGold(int gold_up) {

		db.OpenDB(dbName);
		myGold = db.intDBReader("gold", "userGold", "id", curID);
		myGold += gold_up;
		gold.text = myGold.ToString();
		string[] col = {"gold"};
		int[] val = {myGold};
		db.intTypeUpdate("userGold", col, val, "id", curID);
		db.CloseDB();

	}

	public void unitNamedUp(int named_up) {

		db.OpenDB(dbName);
		read_Named = db.intDBReader("unit_named", "UnitInfo", "unit_name ", curName);
		read_Named += named_up;
		unitNamed.text = read_Named.ToString();
		string[] col = {"unit_named"};
		int[] val = {read_Named};
		db.intTypeUpdate("UnitInfo", col, val, "unit_name", curName);
		db.CloseDB();

	}




	public int updateMoo () {
		// 1 돌격형 2 통솔형 3 군악대
		// 4 내정형 5 공성형 6 책략형
		if (face_type.Equals("1")) {
			return 4;
		} else if (face_type.Equals("5")) {
			return 3;
		} else {
			return 1;
		}
	}

	public int updateTong () {
		if (face_type.Equals("2")) {
			return 3;
		} else if (face_type.Equals("5")) {
			return 2;
		} else {
			return 1;
		}
	}

	public int updateZi () {
		if (face_type.Equals("3")) {
			return 3;
		} else if (face_type.Equals("5")) {
			return 2;
		} else if (face_type.Equals("6")) {
			return 4;
		} else {
			return 1;
		}
	}

	public int updatePol () {
		if (face_type.Equals("3")) {
			return 3;
		} else if (face_type.Equals("4")) {
			return 4;
		} else {
			return 1;
		}
	}






	// Update is called once per frame
	void Update () {
	
	}

	// 설정버튼
	public void optionClick() {

		if (tc.ts == TutoState.CLEAR) {
			option_Pop.SetActive(true);
		}

	}
	// 부대선택 화면으로 
	public void unitSelectView() {

		option_Pop.SetActive(false);
		Application.LoadLevel("MyUnitView");
	}
	// 로그아웃
	public void logOutMain() {
		option_Pop.SetActive(false);
		Application.LoadLevel("SSamMain");
	}
	// 설정창 닫기
	public void closeOption() {
		option_Pop.SetActive(false);
	}
	// 게임종료
	public void gameExit() {
		Application.Quit();
	}


















}
