using UnityEngine;
using System.Collections;

public enum TutoState {

	START,
	STAGE1,
	STAGE2,
	STAGE3,
	STAGE4,
	STAGE5,
	STAGE6,
	CLEAR
    
}

public class TutoCon : MonoBehaviour {

	public UnitAction ua;
	public TutoState ts;
	public int tutoCnt = 0; // 튜토리얼 진행 할때마다 증가 (일정 수치가 되면 클리어 상태로)

	public UILabel upLine;
	public UILabel downLine;

	public GameObject tuto_pan; // 튜토패널
	public GameObject allowBtn; // 화살표 버튼 

	public GameObject basic_black; // 기본 블랙
	public GameObject cash_black; // 캐쉬 밝음
	public GameObject hoon_black; // 훈장 밝음 
	public GameObject gold_black; // 군자금 밝음 
	public GameObject statOpenUI_black; // 펼치기 버튼
	public GameObject statCloseUI_black; // 접기 버튼
	public GameObject stat_black; // 통무지정 능력치 밝음
	public GameObject statPoint_balck; // 스탯포인트 표시 밝음

	public GameObject imsi_Block; // 임시 바리게이트
	public string curName;



	// Use this for initialization
	void Start () {
	
		ts = TutoState.START;
		tutoCnt = 0;
		stage2Cnt = 0;
		stage3Cnt = 0;
		stage4Cnt = 0;
		stage5Cnt = 0;
		stage6Cnt = 0;
		basic_black.SetActive(true);
		db = GetComponent<dbAccess>(); // 디비 스크립트 연결
		curName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("curUnitName")));

	}
	
	// Update is called once per frame
	void Update () {

		if (ts == TutoState.STAGE1) {

			if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
			   Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) {
				downLine.text = "아주 잘했어. 첩보원으로 삼고 싶군";
				StartCoroutine(mission1());
			}

		} // stage1 if

		if (ts == TutoState.STAGE2 && stage2Cnt > 0) {
			mission2();
		}

		if (ts == TutoState.STAGE3 && stage3Cnt >= 5) {
			mission3();
		}

		if (ts == TutoState.STAGE4 && stage4Cnt > 0) {
			mission4();
		}

		if (ts == TutoState.STAGE5 && stage5Cnt > 0) {
			mission5();
		}

		if (ts == TutoState.STAGE6 && stage6Cnt >= 7) {
			mission6();
		}


	} // End Update



	public void clickBtn_nosik() {

		upLine.text = " ";
		downLine.text = " ";

		switch (tutoCnt) {

			case 0:
			upLine.text = "그.. 그렇군 아무것도 모르는 풋내기겠어";
			downLine.text = "걱정말게 자네가 바로 전투에 임할수 있도록 도와줄테니";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 1:
			upLine.text = "방향키를 움직여봐 지형 전체를 훑어 볼 수 있을거야";
			ts = TutoState.STAGE1;
			allowBtn.SetActive(false);
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 2:

			upLine.text = "벌써 싸우고 싶어서 몸이 근질근질 한가?";
			downLine.text = "난세를 구할 인재로군!";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 3:

			upLine.text = "자네의 재력을 살펴보도록 하지";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 4:
			basic_black.SetActive(false);
			cash_black.SetActive(true);
			upLine.text = "밝은 부분을 보시게 이건 자네가 돈지랄";
			downLine.text = "아니.. 과금을 하면 생기는 캐쉬라는 것이네.";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 5:
			cash_black.SetActive(false);
			hoon_black.SetActive(true);
			upLine.text = "딴짓하지말고 밝은 부분을 보라니까!";
			downLine.text = "이건 훈장이지. 큰 공적을 쌓게 되면 얻게될거야";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 6:
			upLine.text = "캐쉬만큼 상당히 유용하게 사용될테니";
			downLine.text = "공적을 열심히 쌓아서 훈장을 받으시게";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 7:
			hoon_black.SetActive(false);
			gold_black.SetActive(true);
			upLine.text = "이건 바로 자네의 군자금이지! 가장 중요해";
			downLine.text = "군자금으로 병력을 징병할 수 있어.";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 8:
			basic_black.SetActive(true);
			gold_black.SetActive(false);
			upLine.text = "병력이 있어야 적과 싸울 수 있으니까";
			downLine.text = "징병외에도 필요로 하는 곳이 많으니 명심하게";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 9:
			upLine.text = "이번엔 자네의 능력을 살펴보도록 하지.";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 10:
			upLine.text = "이번엔 자네의 능력을 살펴보도록 하지.";
			downLine.text = "자네의 능력수준에 맞춰서 토벌군을 내줄테니 그리 알게";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 11:
			upLine.text = "자자 지금부터 집중하라고 두번 설명은 없다.";
			downLine.text = "다시 말하지만 밝은 부분을 잘보고 나의 말을 넘기도록.";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 12:
			basic_black.SetActive(false);
			statOpenUI_black.SetActive(true);
			upLine.text = "자 내가 가리키는 곳에 펼치기라는 글자가 보이는가?";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 13:
			upLine.text = "자 내가 가리키는 곳에 펼치기라는 글자가 보이는가?";
			downLine.text = "그걸 한번 눌러보시게.";
			ts = TutoState.STAGE2;
			allowBtn.SetActive(false);
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 14:
			upLine.text = "자네 능력은 좋은데 그 능력이 전투에 어떤 도움이 되는지";
			downLine.text = "모르니 또 설명을 해줘야 겠군..";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 15:
			basic_black.SetActive(false);
			stat_black.SetActive(true);
			upLine.text = "통솔력, 무력, 지력, 정치 이 네가지의 능력치가 핵심일세";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 16:
			upLine.text = "통솔력, 무력, 지력, 정치 이 네가지의 능력치가 핵심일세";
			downLine.text = "통솔력만큼 중요한 능력치가 없네!";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 17:
			upLine.text = "한 부대를 이끄는 장수라면 필수요소! 통솔력 1당 1000명의";
			downLine.text = "병사를 거느릴 수 있어. 통솔력이 높을수록 대군을 ";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 18:
			upLine.text = "이끌 수 있지. 자 무력은 자기부대의 공격력에 영향을 끼치지";
			downLine.text = "무력이 높을수록 적에게 큰 피해를 입힐 수 있어";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 19:
			upLine.text = "무력은 낮은데 통솔력만 높으면 부대의 위력은 그만큼 떨어져.";
			downLine.text = "능력치의 균형은 그 어떤것보다 중요해";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 20:
			upLine.text = "자 다음 지력은 적에게 계략을 걸기 위해 필요한 능력치야";
			downLine.text = "주유의 화공술..제갈량의 계략들 이 모든것이";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 21:
			upLine.text = "높은 지력에서 나오는 것이다!";
			downLine.text = "아무리 무용을 뽐내도 계략에 걸려들면 한순간이야.";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 22:
			upLine.text = "정치? 어떻게 보면 나라를 부강하는데엔 가장 강력한 ";
			downLine.text = "능력치이지. 정치가 썩으면 나라도 썩어가는 법..";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 23:
			upLine.text = "이렇게 중원이 난세가 된것도 전부 썩은 정치인들 때문이지.";
			downLine.text = "자네 정치력이 좋다면 정치에 입문 해보는건 어떠한가?";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 24:
			upLine.text = "정치력이 좋으면 내정능력이 좋아서 주둔한 성을 강하게";
			downLine.text = "만들 수 있다네. 수호하는 병력..성벽..등을 말이야";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 25:
			upLine.text = "능력치에 대해 알아 보았어. 뭐 지금보다 강해지고 싶다고?";
			downLine.text = "그럼 또 알려주지. 곧 전투를 해야 하니까 말이야.";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 26:
			upLine.text = "전투와 내정을 통해서 경험치를 얻게 되면 부대레벨이 상승하지.";
			downLine.text = "부대레벨이 상승하면서 능력치 또한 같이 상승해";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 27:
			statPoint_balck.SetActive(true);
			basic_black.SetActive(false);
			upLine.text = "그리고! 이곳을 보시게나";
			downLine.text = "보았는가? 스탯포인트라고 하지.";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 28:
			upLine.text = "레벨업을 상승하는 능력치와는 별개로 5개의 스탯 포인트를";
			downLine.text = "얻을 수 있어. 이제 활용을 한번 해볼까?";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 29:
			upLine.text = "보유한 스탯포인트를 소비해 원하는 능력치를 올릴수 있어.";
			downLine.text = "한번 올린 능력치는 다시 포인트로 되돌릴 수 없으니 명심해.";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 30:
			upLine.text = "자. 어디 한번 원하는 능력치를 올려봐. 5개 모두 사용하면";
			downLine.text = "나한테 다시 말을 걸도록";
			ts = TutoState.STAGE3;
			statPoint_balck.SetActive(false);
			stat_black.SetActive(true);
			allowBtn.SetActive(false);
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 31:
			upLine.text = "자네 조금은 강해진 것 같군. 아차!";
			downLine.text = "자기 부대성향에 맞게 능력치를 올려주는게 좋을게야.";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 32:
			upLine.text = "알아서 잘 강해질거라 믿지만 말이야.";
			downLine.text = "";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 33:
			upLine.text = "알아서 잘 강해질거라 믿지만 말이야.";
			downLine.text = "능력치를 다 올렸으면 이제 그 창을 닫아봐";
			ts = TutoState.STAGE4;
			allowBtn.SetActive(false);
			statCloseUI_black.SetActive(true);
			basic_black.SetActive(false);
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 34:
			upLine.text = "이제 저 성 밖에 주둔하고 있는 골치아픈 도적무리를";
			downLine.text = "때려잡을 날이 왔구만!";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 35:
			upLine.text = "우선 동남쪽 하단에 도적 무리 하나가 노략질을 하고 있다는군.";
			downLine.text = "그 도적 먼저 무찌르고 와!";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 36:
			upLine.text = "오른쪽 클릭을 하면 이동, 적을 만나면 마찬가지로";
			downLine.text = "적에게 마우스 오른쪽 클릭을 하면 공격을 할거야.";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 37:
			upLine.text = "";
			downLine.text = "";
			ts = TutoState.STAGE5;
			tuto_pan.SetActive(false);
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 38:
			upLine.text = "자네 설마 벌써 해치웠나?";
			downLine.text = "대단한 녀석이 왔어..!";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 39:
			upLine.text = "남은 도적들도 싸그리 토벌하고 오게나!";
			downLine.text = "자네의 공적에 따라 큰 포상을 하지!";
			Destroy(imsi_Block.gameObject);
			ts = TutoState.STAGE6;
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 40:
			tuto_pan.SetActive(false);
			upLine.text = " ";
			downLine.text = " ";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 41:
			ua.game_over.SetActive(false);
			upLine.text = "자네는 이제 난세를 진정시킬 준비가 다 되었다.";
			downLine.text = "가세 황건적들을 토벌하러!";
			tutoCnt++;
			break;
			//----------------------------------------------------------------------------------------------
			case 42:
			scene_ChanelView();
			break;
			//----------------------------------------------------------------------------------------------
			default:
			break;

		}

	}





	//--------------------------------
	public void scene_ChanelView() {
		
		string[] tCol = {"tutorial"};
		int[] tVal = {1};
		
		db.OpenDB(dbName);
		db.intTypeUpdate ("UnitInfo", tCol, tVal, "unit_name", curName);
		db.CloseDB ();

		Application.LoadLevel ("ChanelView");
		
	}

	private string dbName = "Users.db";
	private dbAccess db;
	public int stage2Cnt = 0;
	public int stage3Cnt = 0;
	public int stage4Cnt = 0;
	public int stage5Cnt = 0;
	public int stage6Cnt = 0;
	public AudioClip victory_sound;

	public void stage6CntUp(){
		stage6Cnt++;
		print ("도적잡은 숫자 : " + stage6Cnt);
	}

	public void mission6() {

		ts = TutoState.START;
		tuto_pan.SetActive(true);
		allowBtn.SetActive(true);
		upLine.text = "골칫덩어리 도적들을 모두 해치웠군.";
		downLine.text = "이제 황건적 토벌에 투입 시켜도 되겠어.";
		ua.tutoBGM.SetActive(false);
		ua.game_over.SetActive(true);
		audio.clip = victory_sound;
		audio.Play();

	}

	public void mission5() {

		ts = TutoState.START;
		tuto_pan.SetActive(true);
		allowBtn.SetActive(true);
		upLine.text = "자네 설마 벌써 해치웠나?";
		downLine.text = "";

	}

	public void mission4() {

		ts = TutoState.START;
		allowBtn.SetActive(true);
		basic_black.SetActive(true);
		statCloseUI_black.SetActive(false);
		upLine.text = "필요 할 때마다 그렇게 접고 펴고 쓰면 된다네.";
		downLine.text = "";

	}

	public void mission3() {

		ts = TutoState.START;
		allowBtn.SetActive(true);
		basic_black.SetActive(true);
		stat_black.SetActive(false);
		statPoint_balck.SetActive(false);
		upLine.text = "응? 자네나 불렀나?";
		downLine.text = "";
	}

	public void mission2() {
		ts = TutoState.START;
		allowBtn.SetActive(true);
		basic_black.SetActive(true);
		statOpenUI_black.SetActive(false);
		upLine.text = "자네의 능력치가 한눈에 보이는군!";
		downLine.text = "어디보자.. 나름 쓸만한 능력이군.";
	}

	IEnumerator mission1() {
		yield return new WaitForSeconds(5);
		ts = TutoState.START;
		ua.cameraOneMoveCon();
		allowBtn.SetActive(true);

	}

}
