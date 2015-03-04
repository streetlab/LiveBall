using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptBetting : MonoBehaviour {

	string mSelectedName;
	ScriptBettingItem mSbi;
	
	public GameObject mBtnHit1;
	public GameObject mBtnHit2;
	public GameObject mBtnHit3;
	public GameObject mBtnHit4;
	public GameObject mBtnOut1;
	public GameObject mBtnOut2;
	public GameObject mBtnOut3;
	public GameObject mBtnOut4;
	public GameObject mBtnLoaded1;
	public GameObject mBtnLoaded2;
	public GameObject mBtnLoaded3;
	public GameObject mBtnLoaded4;

	UIButton mBtnCancel;
	UIButton mBtnConfirm;
	UIButton mBtnConfirm2;
	UILabel mLblGot;
	UILabel mLblUse;
	UILabel mLblExpect;

	double mAmountUse;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		Transform panel = transform.FindChild ("Panel").transform;
		mBtnCancel = panel.FindChild ("BtnCancel").GetComponent<UIButton> ();
		mBtnConfirm = panel.FindChild ("BtnConfirm").GetComponent<UIButton> ();
		mBtnConfirm2 = panel.FindChild ("BtnConfirm2").GetComponent<UIButton> ();
		mLblGot = panel.FindChild ("SprBack1").FindChild("LblAmount").GetComponent<UILabel> ();
		mLblUse = panel.FindChild ("SprBack2").FindChild("LblAmount").GetComponent<UILabel> ();
		mLblExpect = panel.FindChild ("SprBack3").FindChild("LblAmount").GetComponent<UILabel> ();

		mAmountUse = 1000;
	}

	public void Init(string name)
	{
		mSelectedName = name;
		mSbi = GetBettingItem ();
		if(mSbi.IsSelected)
		{
			mBtnCancel.gameObject.SetActive(true);
			mBtnConfirm.gameObject.SetActive(true);
			mBtnConfirm2.gameObject.SetActive(false);
		}
		else
		{
			mBtnCancel.gameObject.SetActive(false);//
			mBtnConfirm.gameObject.SetActive(false);
			mBtnConfirm2.gameObject.SetActive(true);
		}
	}

	void SetConfirm()
	{		 
		mSbi.SetSelected ();
//		CloseWindow ();
		UtilMgr.OnBackPressed ();
	}

	void SetCancel()
	{
		mSbi.SetUnselected ();
//		CloseWindow ();
		UtilMgr.OnBackPressed ();
	}

	ScriptBettingItem GetBettingItem()
	{
		switch(mSelectedName)
		{
		case "BtnHit1":
			return mBtnHit1.GetComponent<ScriptBettingItem>();
		case "BtnHit2":
			return mBtnHit2.GetComponent<ScriptBettingItem>();
		case "BtnHit3":
			return mBtnHit3.GetComponent<ScriptBettingItem>();
		case "BtnHit4":
			return mBtnHit4.GetComponent<ScriptBettingItem>();
		case "BtnOut1":
			return mBtnOut1.GetComponent<ScriptBettingItem>();
		case "BtnOut2":
			return mBtnOut2.GetComponent<ScriptBettingItem>();
		case "BtnOut3":
			return mBtnOut3.GetComponent<ScriptBettingItem>();
		case "BtnOut4":
			return mBtnOut4.GetComponent<ScriptBettingItem>();
		case "BtnLoaded1":
			return mBtnLoaded1.GetComponent<ScriptBettingItem>();
		case "BtnLoaded2":
			return mBtnLoaded2.GetComponent<ScriptBettingItem>();
		case "BtnLoaded3":
			return mBtnLoaded3.GetComponent<ScriptBettingItem>();
		case "BtnLoaded4":
			return mBtnLoaded4.GetComponent<ScriptBettingItem>();
			
		}
		return null;
	}

	public void CloseWindow()
	{
		gameObject.SetActive(false);
	}

	void Bet(int amount)
	{
		mAmountUse += amount;
		//if the Amount over the Max then process under
		if(mAmountUse >= 123123123123000)
		{
			mAmountUse = 123123123123000;
		}

		mLblUse.text = UtilMgr.AddsThousandsSeparator (mAmountUse);
		mLblExpect.text = UtilMgr.AddsThousandsSeparator (mAmountUse*2);

	}

	void BetMax()
	{
		mAmountUse = 123123123123000;
		mLblUse.text = UtilMgr.AddsThousandsSeparator (mAmountUse);
		mLblExpect.text = UtilMgr.AddsThousandsSeparator (mAmountUse*2);
	}

	void BetMin()
	{
		mAmountUse = 1000;
		mLblUse.text = UtilMgr.AddsThousandsSeparator (mAmountUse);
		mLblExpect.text = UtilMgr.AddsThousandsSeparator (mAmountUse*2);
	}

	public void BtnClicked(string name)
	{
		switch(name)
		{
		case "BtnClose":
//			CloseWindow();
			UtilMgr.OnBackPressed();
			break;
		case "BtnConfirm":
			SetConfirm();
			break;
		case "BtnCancel":
			SetCancel();
			break;
		case "Btn10":
			Bet(10);
			break;
		case "Btn100":
			Bet(100);
			break;
		case "Btn1000":
			Bet (1000);
			break;
		case "BtnMax":
			BetMax();
			break;
		case "BtnMin":
			BetMin();
			break;
		}
	}
}
