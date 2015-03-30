using UnityEngine;
using System.Collections;

public class ScriptWindowEmail : MonoBehaviour {

	public GameObject mBtnJoin;
	public GameObject mBtnLogin;

	static Color GRAY = new Color (120f / 255f, 130f / 255f, 150f / 255f);
	static Color WHITE = new Color (1f, 1f, 1f);

	enum SELECTION_STATE{
		JOIN, LOGIN
	}

	SELECTION_STATE mState;

	void Start()
	{
		SetStateLogin ();
	}

	void SetStateLogin()
	{
		mBtnLogin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().color = WHITE;
		mBtnLogin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().height = 4;
		mBtnLogin.transform.FindChild ("Label").GetComponent<UILabel> ().color = WHITE;
		mBtnJoin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().color = GRAY;
		mBtnJoin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().height = 2;
		mBtnJoin.transform.FindChild ("Label").GetComponent<UILabel> ().color = GRAY;
		transform.FindChild ("BtnFindPwd").gameObject.SetActive (true);
		transform.FindChild ("LblPwd").gameObject.SetActive (true);
		mState = SELECTION_STATE.LOGIN;
	}

	void SetStateJoin()
	{
		mBtnJoin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().color = WHITE;
		mBtnJoin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().height = 4;
		mBtnJoin.transform.FindChild ("Label").GetComponent<UILabel> ().color = WHITE;
		mBtnLogin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().color = GRAY;
		mBtnLogin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().height = 2;
		mBtnLogin.transform.FindChild ("Label").GetComponent<UILabel> ().color = GRAY;
		transform.FindChild ("BtnFindPwd").gameObject.SetActive (false);
		transform.FindChild ("LblPwd").gameObject.SetActive (false);
		mState = SELECTION_STATE.JOIN;
	}

	public void JoinClicked()
	{
		if (mState != SELECTION_STATE.JOIN)
			SetStateJoin ();
	}

	public void LoginClicked()
	{
		if (mState != SELECTION_STATE.LOGIN)
			SetStateLogin ();
	}

	public void BackClicked()
	{
		UtilMgr.OnBackPressed ();
	}

	public void NextClicked()
	{
		string eMail = transform.FindChild ("InputEmail").GetComponent<UIInput> ().value;
		string pwd = transform.FindChild ("InputPwd").GetComponent<UIInput> ().value;
		if (mState == SELECTION_STATE.LOGIN) {
			GetComponentInParent<ScriptTitle>().DoLogin(eMail, pwd);
		} else {
			gameObject.SetActive (false);
			transform.parent.FindChild ("FormJoin").gameObject.SetActive (true);
			transform.parent.FindChild ("FormJoin").GetComponent<ScriptJoinForm>().Init(eMail, pwd, true);
		}

	}

	public void FindPwdClicked()
	{

	}
}
