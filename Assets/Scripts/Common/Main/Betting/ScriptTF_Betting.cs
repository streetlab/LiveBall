using UnityEngine;
using System.Collections;

public class ScriptTF_Betting : MonoBehaviour {

	public GameObject mTop;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Debug.Log("Touched");
		}
	}

	void OnEnable()
	{
		ScriptMainTop smt = mTop.GetComponent<ScriptMainTop> ();
		smt.mBtnHighlight.SetActive (false);
		smt.mBtnLineup.SetActive (false);
		smt.mBtnLivetalk.SetActive (false);
		smt.mBtnBingo.SetActive (false);
	}

	void OnDisable()
	{
		ScriptMainTop smt = mTop.GetComponent<ScriptMainTop> ();
		smt.mBtnHighlight.SetActive (true);
		smt.mBtnLineup.SetActive (true);
		smt.mBtnLivetalk.SetActive (true);
		smt.mBtnBingo.SetActive (true);
	}

	public void CloseBetting()
	{

	}
}
