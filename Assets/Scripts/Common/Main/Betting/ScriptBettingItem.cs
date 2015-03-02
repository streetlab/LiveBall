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
		Reset ();
	}

	public void Reset()
	{
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
	}
}
