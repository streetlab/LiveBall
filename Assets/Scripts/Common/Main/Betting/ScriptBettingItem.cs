using UnityEngine;
using System.Collections;

public class ScriptBettingItem : MonoBehaviour {
	public enum TYPE
	{
		Batter,
		Loaded
	}

	public TYPE mType;

	public GameObject mBetting;
	public GameObject mSprBetting;

	GameObject mSprSelected;
	UISprite mSprSilhouette;
	UISprite[] mSprCombos;

	ScriptTF_Betting mSb;
	bool _isSelected;

	static Color ColorSilhouetteDisable = new Color(78f/255f, 89f/255f, 104f/255f);
	static Color ColorSilhouetteEnable = new Color(67f/255f, 75f/255f, 89f/255f);
	static Color ColorComboDisable = new Color(141f/255f, 150f/255f, 166f/255f, 100f/255f);
	static Color ColorComboEnable = new Color(1f, 1f, 0f, 100f/255f);

	// Use this for initialization
	void Start () {
		Init ();
	}

	void Init(){
		mSb = mBetting.GetComponent<ScriptTF_Betting> ();
		mSprSelected = transform.FindChild ("SprSelected").gameObject;
		
		if(mType == TYPE.Batter)
		{
			mSprSilhouette = transform.FindChild ("SprSilhouette").GetComponent<UISprite>();
			mSprCombos = new UISprite[3];
			mSprCombos [0] = transform.FindChild ("SprCombo1").GetComponent<UISprite> ();
			mSprCombos [1] = transform.FindChild ("SprCombo2").GetComponent<UISprite> ();
			mSprCombos [2] = transform.FindChild ("SprCombo3").GetComponent<UISprite> ();
			mSprCombos [0].color = ColorComboDisable;
			mSprCombos [1].color = ColorComboDisable;
			mSprCombos [2].color = ColorComboDisable;
		}

		SetUnselected ();
	}

	public void Reset()
	{
		Init ();

		int index = mSprBetting.GetComponent<ScriptBetting> ().GetIndex (transform.name);
		transform.FindChild ("LblBody").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [index].description;
		



	}

	public bool IsSelected{
		get{return _isSelected;}
		set{_isSelected = value;}
	}

	public void SetSelected()
	{
		IsSelected = true;
		mSprSelected.SetActive (true);

		if(mType == TYPE.Loaded)
		{
			return;
		}

		mSprSilhouette.color = ColorSilhouetteEnable;
		SetCombo (3);
	}

	public void SetUnselected()
	{
		IsSelected = false;
		mSprSelected.SetActive (false);

		if(mType == TYPE.Loaded)
		{
			return;
		}

		mSprSilhouette.color = ColorSilhouetteDisable;
	}

	public void OnClicked(string name)
	{
		OpenBetWindow (name);
	}

	void OpenBetWindow(string name)
	{
		mSprBetting.SetActive (true);
		mSprBetting.GetComponent<ScriptBetting> ().Init (name);

		UtilMgr.SetBackEvent(
			new EventDelegate (mSprBetting.GetComponent<ScriptBetting> (),
		                   "CloseWindow"));
	}

	public void ClearCombos()
	{
		transform.FindChild("SprCombo3").GetComponent<UISprite>().color = ColorComboDisable;
		transform.FindChild("SprCombo2").GetComponent<UISprite>().color = ColorComboDisable;
		transform.FindChild("SprCombo1").GetComponent<UISprite>().color = ColorComboDisable;
	}

	public void SetCombo(int count)
	{
		Vector3 pos;
		switch(count){
		case 3:
			pos = transform.FindChild("SprCombo3").localPosition;
			pos.x -= 46f;
			pos.y += 20f;
			transform.FindChild("SprCombo3").GetComponent<UISprite>().color = ColorComboEnable;
			iTween.MoveFrom(transform.FindChild("SprCombo3").gameObject, iTween.Hash(
				"easetype", "easeOutCirc",
				"islocal", true,
				"time", 0.5f,
				"position", pos
				));
			iTween.ScaleFrom(transform.FindChild("SprCombo3").gameObject, new Vector3(2f, 2f, 2f), 0.5f);

			goto case 2;
		case 2:
			pos = transform.FindChild("SprCombo2").localPosition;
			pos.y -= 46f;
			transform.FindChild("SprCombo2").GetComponent<UISprite>().color = ColorComboEnable;
			iTween.MoveFrom(transform.FindChild("SprCombo2").gameObject, iTween.Hash(
				"easetype", "easeOutCirc",
				"islocal", true,
				"time", 0.5f,
				"position", pos
				));
			iTween.ScaleFrom(transform.FindChild("SprCombo2").gameObject, new Vector3(2f, 2f, 2f), 0.5f);

			goto case 1;
		case 1:
			pos = transform.FindChild("SprCombo1").localPosition;
			pos.x += 46f;
			pos.y += 20f;
			transform.FindChild("SprCombo1").GetComponent<UISprite>().color = ColorComboEnable;
			iTween.MoveFrom(transform.FindChild("SprCombo1").gameObject, iTween.Hash(
				"easetype", "easeOutCirc",
				"islocal", true,
				"time", 0.5f,
				"position", pos
				));
			iTween.ScaleFrom(transform.FindChild("SprCombo1").gameObject, new Vector3(2f, 2f, 2f), 0.5f);

			break;
		}
	}

}
