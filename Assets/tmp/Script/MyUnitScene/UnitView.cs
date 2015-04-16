using UnityEngine;
using System.Collections;

public class UnitView : MonoBehaviour {

	// 부대별 슬롯창
	public GameObject slot1_Empty;
	public GameObject slot2_Empty;
	public GameObject slot3_Empty;
	public GameObject slot1_Attacker;
	public GameObject slot2_Attacker;
	public GameObject slot3_Attacker;
	public GameObject slot1_Command;
	public GameObject slot2_Command;
	public GameObject slot3_Command;
	public GameObject slot1_MilBand;
	public GameObject slot2_MilBand;
	public GameObject slot3_MilBand;
	public GameObject slot1_Political;
	public GameObject slot2_Political;
	public GameObject slot3_Political;
	public GameObject slot1_Siege;
	public GameObject slot2_Siege;
	public GameObject slot3_Siege;
	public GameObject slot1_Trick;
	public GameObject slot2_Trick;
	public GameObject slot3_Trick;

	// 레벨 이름 라벨
	public GameObject slot1_LvText;
	public GameObject slot1_NameText;
	public GameObject slot2_LvText;
	public GameObject slot2_NameText;
	public GameObject slot3_LvText;
	public GameObject slot3_NameText;
	
	public void switchUnitView1 (int slot1Unit) {

		print("슬롯1스위치 불림 : " + slot1Unit);

		switch (slot1Unit) {
			
		case 0:
			
			slot1_Empty.SetActive(true);
			slot1_Attacker.SetActive(false);
			slot1_Command.SetActive(false);
			slot1_MilBand.SetActive(false);
			slot1_Political.SetActive(false);
			slot1_Siege.SetActive(false);
			slot1_Trick.SetActive(false);
			slot1_LvText.SetActive(false);
			slot1_NameText.SetActive(false);
			break;
			
		case 1:
			
			slot1_Empty.SetActive(false);
			slot1_Attacker.SetActive(true);
			slot1_Command.SetActive(false);
			slot1_MilBand.SetActive(false);
			slot1_Political.SetActive(false);
			slot1_Siege.SetActive(false);
			slot1_Trick.SetActive(false);
			slot1_LvText.SetActive(true);
			slot1_NameText.SetActive(true);
			break;
			
		case 2:
			
			slot1_Empty.SetActive(false);
			slot1_Attacker.SetActive(false);
			slot1_Command.SetActive(true);
			slot1_MilBand.SetActive(false);
			slot1_Political.SetActive(false);
			slot1_Siege.SetActive(false);
			slot1_Trick.SetActive(false);
			slot1_LvText.SetActive(true);
			slot1_NameText.SetActive(true);
			break;
			
		case 3:
			
			slot1_Empty.SetActive(false);
			slot1_Attacker.SetActive(false);
			slot1_Command.SetActive(false);
			slot1_MilBand.SetActive(true);
			slot1_Political.SetActive(false);
			slot1_Siege.SetActive(false);
			slot1_Trick.SetActive(false);
			slot1_LvText.SetActive(true);
			slot1_NameText.SetActive(true);
			break;
			
		case 4:
			
			slot1_Empty.SetActive(false);
			slot1_Attacker.SetActive(false);
			slot1_Command.SetActive(false);
			slot1_MilBand.SetActive(false);
			slot1_Political.SetActive(true);
			slot1_Siege.SetActive(false);
			slot1_Trick.SetActive(false);
			slot1_LvText.SetActive(true);
			slot1_NameText.SetActive(true);
			break;
			
		case 5:
			
			slot1_Empty.SetActive(false);
			slot1_Attacker.SetActive(false);
			slot1_Command.SetActive(false);
			slot1_MilBand.SetActive(false);
			slot1_Political.SetActive(false);
			slot1_Siege.SetActive(true);
			slot1_Trick.SetActive(false);
			slot1_LvText.SetActive(true);
			slot1_NameText.SetActive(true);
			break;
			
		case 6:
			
			slot1_Empty.SetActive(false);
			slot1_Attacker.SetActive(false);
			slot1_Command.SetActive(false);
			slot1_MilBand.SetActive(false);
			slot1_Political.SetActive(false);
			slot1_Siege.SetActive(false);
			slot1_Trick.SetActive(true);
			slot1_LvText.SetActive(true);
			slot1_NameText.SetActive(true);
			break;
			
		default:
			
			break;
			
		}
		
	}
	
	public void switchUnitView2 (int slot2Unit) {

		print("슬롯2스위치 불림 : " + slot2Unit);

		switch (slot2Unit) {
			
		case 0:
			
			slot2_Empty.SetActive(true);
			slot2_Attacker.SetActive(false);
			slot2_Command.SetActive(false);
			slot2_MilBand.SetActive(false);
			slot2_Political.SetActive(false);
			slot2_Siege.SetActive(false);
			slot2_Trick.SetActive(false);
			slot2_LvText.SetActive(false);
			slot2_NameText.SetActive(false);
			break;
			
		case 1:
			
			slot2_Empty.SetActive(false);
			slot2_Attacker.SetActive(true);
			slot2_Command.SetActive(false);
			slot2_MilBand.SetActive(false);
			slot2_Political.SetActive(false);
			slot2_Siege.SetActive(false);
			slot2_Trick.SetActive(false);
			slot2_LvText.SetActive(true);
			slot2_NameText.SetActive(true);
			break;
			
		case 2:
			
			slot2_Empty.SetActive(false);
			slot2_Attacker.SetActive(false);
			slot2_Command.SetActive(true);
			slot2_MilBand.SetActive(false);
			slot2_Political.SetActive(false);
			slot2_Siege.SetActive(false);
			slot2_Trick.SetActive(false);
			slot2_LvText.SetActive(true);
			slot2_NameText.SetActive(true);
			break;
			
		case 3:
			
			slot2_Empty.SetActive(false);
			slot2_Attacker.SetActive(false);
			slot2_Command.SetActive(false);
			slot2_MilBand.SetActive(true);
			slot2_Political.SetActive(false);
			slot2_Siege.SetActive(false);
			slot2_Trick.SetActive(false);
			slot2_LvText.SetActive(true);
			slot2_NameText.SetActive(true);
			break;
			
		case 4:
			
			slot2_Empty.SetActive(false);
			slot2_Attacker.SetActive(false);
			slot2_Command.SetActive(false);
			slot2_MilBand.SetActive(false);
			slot2_Political.SetActive(true);
			slot2_Siege.SetActive(false);
			slot2_Trick.SetActive(false);
			slot2_LvText.SetActive(true);
			slot2_NameText.SetActive(true);
			break;
			
		case 5:
			
			slot2_Empty.SetActive(false);
			slot2_Attacker.SetActive(false);
			slot2_Command.SetActive(false);
			slot2_MilBand.SetActive(false);
			slot2_Political.SetActive(false);
			slot2_Siege.SetActive(true);
			slot2_Trick.SetActive(false);
			slot2_LvText.SetActive(true);
			slot2_NameText.SetActive(true);
			break;
			
		case 6:
			
			slot2_Empty.SetActive(false);
			slot2_Attacker.SetActive(false);
			slot2_Command.SetActive(false);
			slot2_MilBand.SetActive(false);
			slot2_Political.SetActive(false);
			slot2_Siege.SetActive(false);
			slot2_Trick.SetActive(true);
			slot2_LvText.SetActive(true);
			slot2_NameText.SetActive(true);
			break;
			
		default:
			
			break;
			
		}
		
	}
	
	public void switchUnitView3 (int slot3Unit) {

		print("슬롯3스위치 불림 : " + slot3Unit);

		switch (slot3Unit) {
			
		case 0:
			
			slot3_Empty.SetActive(true);
			slot3_Attacker.SetActive(false);
			slot3_Command.SetActive(false);
			slot3_MilBand.SetActive(false);
			slot3_Political.SetActive(false);
			slot3_Siege.SetActive(false);
			slot3_Trick.SetActive(false);
			slot3_LvText.SetActive(false);
			slot3_NameText.SetActive(false);
			break;
			
		case 1:
			
			slot3_Empty.SetActive(false);
			slot3_Attacker.SetActive(true);
			slot3_Command.SetActive(false);
			slot3_MilBand.SetActive(false);
			slot3_Political.SetActive(false);
			slot3_Siege.SetActive(false);
			slot3_Trick.SetActive(false);
			slot3_LvText.SetActive(true);
			slot3_NameText.SetActive(true);
			break;
			
		case 2:
			
			slot3_Empty.SetActive(false);
			slot3_Attacker.SetActive(false);
			slot3_Command.SetActive(true);
			slot3_MilBand.SetActive(false);
			slot3_Political.SetActive(false);
			slot3_Siege.SetActive(false);
			slot3_Trick.SetActive(false);
			slot3_LvText.SetActive(true);
			slot3_NameText.SetActive(true);
			break;
			
		case 3:
			
			slot3_Empty.SetActive(false);
			slot3_Attacker.SetActive(false);
			slot3_Command.SetActive(false);
			slot3_MilBand.SetActive(true);
			slot3_Political.SetActive(false);
			slot3_Siege.SetActive(false);
			slot3_Trick.SetActive(false);
			slot3_LvText.SetActive(true);
			slot3_NameText.SetActive(true);
			break;
			
		case 4:
			
			slot3_Empty.SetActive(false);
			slot3_Attacker.SetActive(false);
			slot3_Command.SetActive(false);
			slot3_MilBand.SetActive(false);
			slot3_Political.SetActive(true);
			slot3_Siege.SetActive(false);
			slot3_Trick.SetActive(false);
			slot3_LvText.SetActive(true);
			slot3_NameText.SetActive(true);
			break;
			
		case 5:
			
			slot3_Empty.SetActive(false);
			slot3_Attacker.SetActive(false);
			slot3_Command.SetActive(false);
			slot3_MilBand.SetActive(false);
			slot3_Political.SetActive(false);
			slot3_Siege.SetActive(true);
			slot3_Trick.SetActive(false);
			slot3_LvText.SetActive(true);
			slot3_NameText.SetActive(true);
			break;
			
		case 6:
			
			slot3_Empty.SetActive(false);
			slot3_Attacker.SetActive(false);
			slot3_Command.SetActive(false);
			slot3_MilBand.SetActive(false);
			slot3_Political.SetActive(false);
			slot3_Siege.SetActive(false);
			slot3_Trick.SetActive(true);
			slot3_LvText.SetActive(true);
			slot3_NameText.SetActive(true);
			break;
			
		default:
			
			break;
			
		}
		
	}

}
