using UnityEngine;
using System.Collections;

public class CreateUnitControl : MonoBehaviour {

	// 부대 아이콘
	public GameObject iconAttacker;
	public GameObject iconTrick;
	public GameObject iconCommand;
	public GameObject iconMilBand;
	public GameObject iconPolitical;
	public GameObject iconSiege;
	public GameObject btnBack;
	public GameObject createUI;

	// 부대 생성 UI에서의 부대 정보
	public GameObject attackerFace;
	public GameObject attackerText;
	public GameObject trickFace;
	public GameObject trickText;
	public GameObject commandFace;
	public GameObject commandText;
	public GameObject milbandFace;
	public GameObject milbandText;
	public GameObject politicalFace;
	public GameObject politicalText;
	public GameObject siegeFace;
	public GameObject siegeText;

	// 부대 타입 버튼
	public GameObject typeAT;
	public GameObject typeTR;
	public GameObject typeCOM;
	public GameObject typeMIL;
	public GameObject typePOL;
	public GameObject typeSIG;

	// 스탯 라벨
	public UILabel tong;
	public UILabel moo;
	public UILabel zi;
	public UILabel zeong;

	// 부대타입의 고유 넘버 저장
	string unitNum;
	// 0 빈슬롯 1 돌격형 2 통솔형 3 군악대
	// 4 내정형 5 공성형 6 책략형


	public void statNum() {
		tong.text = PlayerPrefs.GetInt("basic_leadership").ToString();
		moo.text = PlayerPrefs.GetInt("basic_force").ToString();
		zi.text = PlayerPrefs.GetInt("basic_intellect").ToString();
		zeong.text = PlayerPrefs.GetInt("basic_political").ToString();
	}
	// 타입별 생성할 부대 클릭
	public void attackerClick () {
		iconOnOff(false, true);
		unitDataOnOff(true,false,false,false,false,false);

		PlayerPrefs.SetInt("basic_leadership", 10);
		PlayerPrefs.SetInt("basic_force", 50);
		PlayerPrefs.SetInt("basic_intellect", 25);
		PlayerPrefs.SetInt("basic_political", 25);
		PlayerPrefs.SetString("TypeNum", "1");
		PlayerPrefs.SetInt("MAX_Troops", 10000);

		statNum();

	}

	public void commandClick () {
		iconOnOff(false, true);
		unitDataOnOff(false,false,true,false,false,false);

		PlayerPrefs.SetInt("basic_leadership", 30);
		PlayerPrefs.SetInt("basic_force", 30);
		PlayerPrefs.SetInt("basic_intellect", 30);
		PlayerPrefs.SetInt("basic_political", 30);
		PlayerPrefs.SetString("TypeNum", "2");
		PlayerPrefs.SetInt("MAX_Troops", 30000);
		
		statNum();

	}

	public void milBandClick () {
		iconOnOff(false, true);
		unitDataOnOff(false,false,false,true,false,false);

		PlayerPrefs.SetInt("basic_leadership", 10);
		PlayerPrefs.SetInt("basic_force", 10);
		PlayerPrefs.SetInt("basic_intellect", 40);
		PlayerPrefs.SetInt("basic_political", 40);
		PlayerPrefs.SetString("TypeNum", "3");
		PlayerPrefs.SetInt("MAX_Troops", 10000);
		
		statNum();
	}

	public void politicalClick () {
		iconOnOff(false, true);
		unitDataOnOff(false,false,false,false,true,false);

		PlayerPrefs.SetInt("basic_leadership", 5);
		PlayerPrefs.SetInt("basic_force", 20);
		PlayerPrefs.SetInt("basic_intellect", 30);
		PlayerPrefs.SetInt("basic_political", 50);
		PlayerPrefs.SetString("TypeNum", "4");
		PlayerPrefs.SetInt("MAX_Troops", 5000);
		
		statNum();
	}

	public void siegeClick () {
		iconOnOff(false, true);
		unitDataOnOff(false,false,false,false,false,true);

		PlayerPrefs.SetInt("basic_leadership", 20);
		PlayerPrefs.SetInt("basic_force", 35);
		PlayerPrefs.SetInt("basic_intellect", 25);
		PlayerPrefs.SetInt("basic_political", 25);
		PlayerPrefs.SetString("TypeNum", "5");
		PlayerPrefs.SetInt("MAX_Troops", 20000);
		
		statNum();
	}

	public void trickClick () {
		iconOnOff(false, true);
		unitDataOnOff(false,true,false,false,false,false);

		PlayerPrefs.SetInt("basic_leadership", 10);
		PlayerPrefs.SetInt("basic_force", 25);
		PlayerPrefs.SetInt("basic_intellect", 50);
		PlayerPrefs.SetInt("basic_political", 25);
		PlayerPrefs.SetString("TypeNum", "6");
		PlayerPrefs.SetInt("MAX_Troops", 10000);
		
		statNum();
	}

	// UI 이미지 켜고 끄기
	public void iconOnOff(bool icon, bool ui) {
		iconAttacker.SetActive(icon);
		iconTrick.SetActive(icon);
		iconCommand.SetActive(icon);
		iconMilBand.SetActive(icon);
		iconPolitical.SetActive(icon);
		iconSiege.SetActive(icon);
		btnBack.gameObject.collider.enabled = icon;
		createUI.SetActive(ui);
	}

	// UI 부대 생성 세부정보 불러오고 닫기
	public void unitDataOnOff(bool atData, bool trData, bool comData, bool milData, bool polData, bool sigData) {

		attackerFace.SetActive(atData);
		attackerText.SetActive(atData);
		typeAT.SetActive(atData);

		trickFace.SetActive(trData);
		trickText.SetActive(trData);
		typeTR.SetActive(trData);

		commandFace.SetActive(comData);
		commandText.SetActive(comData);
		typeCOM.SetActive(comData);

		milbandFace.SetActive(milData);
		milbandText.SetActive(milData);
		typeMIL.SetActive(milData);

		politicalFace.SetActive(polData);
		politicalText.SetActive(polData);
		typePOL.SetActive(polData);

		siegeFace.SetActive(sigData);
		siegeText.SetActive(sigData);
		typeSIG.SetActive(sigData);



	}

	// 부대 생성창의 돌아가기 버튼
	public void createUIFalse () {
		createUI.SetActive(false);
		iconOnOff(true, false);
	}

	// 돌아가기 버튼
	public void backMyUnitList () {
		Application.LoadLevel("MyUnitView");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
