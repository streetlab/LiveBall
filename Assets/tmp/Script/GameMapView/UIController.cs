using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	private string dbName = "Users.db";
	private dbAccess db;
	
	public GameObject option_Pop;
	
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

	public CameraController cc;
	

	void Start () {
		
		db = GetComponent<dbAccess>(); // 디비 스크립트 연결
		curName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("curUnitName")));	
		curID = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("ID")));
		db.OpenDB(dbName);
		up_Barset();
		unitStateUI();
		face_type = db.stringDBReader("unit_type", "UnitInfo", "unit_name", curName);
		unitStatSet();
		forceUIOnOff ();
		db.CloseDB();
		if (forceNum.Equals("1")) {
			cc.uc = UserCastle.nak_yang;
			cc.arrowPivot();
		}
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
		statDownBtn.SetActive(false);
		statUI.SetActive(true);
		statUpBtn.SetActive(true);
		statUI_Info.SetActive(true);
	}
	// 스탯창 접기
	public void uiStatUp() {
		statDownBtn.SetActive(true);
		statUI.SetActive(false);
		statUpBtn.SetActive(false);
		statUI_Info.SetActive(false);
	}
	
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
		
	} // tong
	
	public void mooClick() {
		
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

	} // moo
	
	public void ziClick() {
		
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
	
	public void polClick() {
		
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
	
	public void btnSound(AudioClip ok_no) {
		audio.clip = ok_no;
		audio.Play ();
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
		option_Pop.SetActive(true);
	}

	// 부대선택 화면으로 
	public void unitSelectView() {
		option_Pop.SetActive(false);
		Application.LoadLevel("MyUnitView");
	}

	public void chanelSelectView() {
		option_Pop.SetActive(false);
		Application.LoadLevel("ChanelView");
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
	
	//-------------------------------------------

	public GameObject forcePanel;

	// 하진 = 1
	string forceNum;

	public void forceUIOnOff() {

		forceNum = db.stringDBReader("ch1_force", "UnitInfo", "unit_name", curName);
		if (forceNum.Equals("0")) {
			forcePanel.SetActive(true);
		} else {
			forcePanel.SetActive(false);
		}

	}

	public void forceHazin() {

		forcePanel.SetActive(false);
		forceNum = "1";
		string[] fCol = {"ch1_force"};
		string[] fVal = {forceNum};
		db.OpenDB (dbName);
		db.UpdateData("UnitInfo", fCol, fVal, "unit_name", curName);
		db.CloseDB ();
		cc.uc = UserCastle.nak_yang;
		cc.arrowPivot();

	}
	

	
	
	
}
