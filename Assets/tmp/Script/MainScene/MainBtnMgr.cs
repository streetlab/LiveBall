using UnityEngine;
using System.Collections;

public class MainBtnMgr : MonoBehaviour {

	// 패널
	public GameObject joinUI; // 회원가입 UI panel
	public GameObject loginUI; // 로그인 UI panel
	public GameObject joinSetPopup; // 회원가입 완료 팝업 UI panel
	public GameObject joinMissPopup; // 회원가입 실패 팝업 UI Panel
	public GameObject LoginMissPopup; // 로그인 실패 팝업 UI Panel
	public GameObject idNullPopup;
	public GameObject pwNullPopup;

	public GameObject btn_LoginOK;
	public GameObject btn_LoginCancle;

	// 아이디생성 and 로그인 UI
	public UIInput inputId; // 로그인 UI의 id 입력창
	public UIInput inputPass; // 로그인 UI의 pass 입력창
	public UIInput joinId; // 회원가입 UI의 id 입력창 
	public UIInput joinPass; // 회원가입 UI의 pass 입력창

	// 메인버튼
	public GameObject BtnQuit; // 게임종료 
	public GameObject BtnJoin; // 아이디 생성
	public GameObject BtnStart; // 게임시작 (로그인)

	// Sound
	public AudioClip okSound;
	public AudioClip noSound;
	public AudioClip notCantSound;


	// 디비파일 이름
	private string dbName = "Users.db";
	// 디비*
	private dbAccess db;

	void Start () {
		Debug.Log("starting SQLiteLoad app");
		
		db = GetComponent<dbAccess>(); // 디비 스크립트 연결
		//testTable();
	}
	/*
	void testTable() {
		db.OpenDB(dbName);
		string[] col = {"id","pass"};
		string[] colType = {"TEXT PRIMARY KEY","TEXT"};
		bool isCompl = db.CreateTable("testuserTable", col, colType);
		
		print ("테이블생성 " + isCompl);

	}
*/
	// 메인 버튼 On Off
	void btnSet(bool tnF) {
		BtnJoin.SetActive(tnF);
		BtnQuit.SetActive(tnF);
		BtnStart.SetActive(tnF);
	} 

	// 로그인  -----------------------------------------------------------------------------------------
	// 게임시작 -> 로그인 UI
	public void gameStart() {
		loginUI.SetActive(true);
		inputId.text = "";
		inputPass.text= "";
		btnSet(false);
	}
	// 로그인 UI 닫기
	public void closeLogin() {
		inputId.text = "";
		inputPass.text= "";
		loginUI.SetActive(false);
		btnSet(true);
	}

	//  로그인 성공시 부대목록 화면으로 이동
	public void loginCompl() {

		string id ="";
		string pass = "";

		if (inputId.label.text == "") {
			audio.clip = notCantSound;
			audio.Play();
			idNullPopup.SetActive(true);
			print ("아이디 입력");
			return;
		} else if (inputPass.label.text == "") {
			audio.clip = notCantSound;
			audio.Play();
			pwNullPopup.SetActive(true);
			print ("비밀번호 입력");
			return;
		} else {
			id = "'"+ inputId.label.text +"'"; //    "   '   " + joinId.label.text + "   '   ";
			pass = ""+ inputPass.label.text +""; // "'" + abcd + "'";     밸류값 샘플
			db.OpenDB(dbName);
			int x = db.loginCheck("User", id, pass);
			print (x);

			if (x == 1) {
				btnSet(true);
				loginUI.SetActive(false);
				print ("로그인 성공.");
				// PlayerPrefs에 인코딩하여 한글 데이터 저장하기 
				string result = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(id));
				PlayerPrefs.SetString("ID", result);
				Application.LoadLevel("MyUnitView");
			} else {
				inputPass.text= "";
				audio.clip = noSound;
				audio.Play();
				LoginMissPopup.SetActive (true);
				print ("로그인 실패.");
				btn_LoginOK.gameObject.collider.enabled = false;
				btn_LoginCancle.gameObject.collider.enabled = false;
			}
			db.CloseDB();
		}

	}

	// 회원가입 -----------------------------------------------------------------------------------------
	// 회원가입 UI 보여주기
	public void userJoin() {
		joinUI.SetActive(true);
		joinId.text = "";
		joinPass.text= "";
		btnSet(false);
	}
	// 회원가입 UI 닫기
	public void closeJoin() {
		btnSet(true);
		joinId.text = "";
		joinPass.text= "";
		joinUI.SetActive(false);
	}
	// 회원가입 완료 
	public void joinCompl() {

		string id = "";
		string pass = "";

		if (joinId.label.text == "") {
			idNullPopup.SetActive(true);
			audio.clip = notCantSound;
			audio.Play();
			print ("아이디 입력");
		} else if (joinPass.label.text == "") {
			audio.clip = notCantSound;
			audio.Play();
			pwNullPopup.SetActive(true);
			print ("비밀번호 입력");
		} else {
			db.OpenDB(dbName);

			id = "'"+ joinId.label.text +"'"; //    "   '   " + joinId.label.text + "   '   ";
			pass = "'"+ joinPass.label.text +"'"; // "'" + abcd + "'";     밸류값 샘플
			string[] val = {id,pass};
			string[] cashCol= {"id"};
			string[] cashVal = {id};


			ArrayList idCheck = db.SingleSelectWhere("User", "*", "id", "=", id);
			if (idCheck.Count > 0) {
				audio.clip = noSound;
				audio.Play();
				joinMissPopup.SetActive(true);
				print ("중복된 아이디 존재");
			} else {

				int dbOKCheck = db.InsertInto("User", val);
				// 신규유저 캐쉬,훈장,군자금 지급
				string[] col = {"cash", "mark", "gold"};
				int[] gold = {2000, 500, 500000};
				db.InsertIntoSpecific("userGold", cashCol, cashVal);
				db.intTypeUpdate("userGold", col, gold, "id", id);

				if (dbOKCheck > 0) {
					audio.clip = okSound;
					audio.Play();
					joinSetPopup.SetActive(true);
					print ("인서트 성공" + id + " : " + pass + "["+ dbOKCheck+"]");
				} else {
					joinMissPopup.SetActive(true);
					audio.clip = noSound;
					audio.Play();
					print ("인서트 실패 " + id + " : " + pass + "["+ dbOKCheck+"]");
                }
            }
			db.CloseDB();
        }
			
        }

	// 가입완료 팝업창 확인버튼
	public void joinOKClick() {
		joinSetPopup.SetActive(false);
		joinUI.SetActive(false);
		btnSet(true);
	}
	// 사용할 수 없는 아이디 확인버튼
	public void idMissOKClick() {
		joinMissPopup.SetActive(false);
	}
	// 로그인 실패 확인 버튼
	public void loginMiss() {
		LoginMissPopup.SetActive (false);
		btn_LoginOK.gameObject.collider.enabled = true; // 박스 콜라이더 체크
		btn_LoginCancle.gameObject.collider.enabled = true;
	}

	public void nullIDpop() {
		idNullPopup.SetActive(false);
	}

	public void nullPWpop() {
		pwNullPopup.SetActive(false);
	}
	

	// 게임종료
	public void  gameQuit() {
		Application.Quit();
	}


}
















