using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptMatchInfo : MonoBehaviour {

	static Color WHITE = new Color (1f, 1f, 1f);
	static Color YELLOW = new Color (1f, 1f, 0f);
	static Color GREEN = new Color (0f, 1f, 0f);
	static Color RED = new Color (1f, 0f, 0f);
	static Color DISABLE = new Color (114f/255f, 118f/255f, 133f/255f);

	Transform mStrike;
	Transform mRound;
	Transform mSprBases;
	Transform mOut;
	Transform mBall;

	// Use this for initialization
	void Start () {
		mStrike = transform.FindChild ("Strike");
		mBall = transform.FindChild ("Ball");
		mOut = transform.FindChild ("Out");
		mSprBases = transform.FindChild ("SprBases");
		mRound = transform.FindChild ("Round");

		ClearBoard ();
	}

	public void SetBoard()
	{
		PlayInfo play = ScriptMainTop.DetailBoard.play;
		SetStrike (play.strikeCount);
		SetBall (play.ballCount);
		SetOut (play.outCount);
		SetRound (play.playRound);
		SetHalftime (play.playInningType);
		List<int> bases = new List<int> ();
		if (play.base1st > 0)
			bases.Add (1);
		if (play.base2nd > 0)
			bases.Add (2);
		if (play.base3rd > 0)
			bases.Add (3);
		SetBases (bases);
	}

	public void SetBoard(int strike, int ball, int outCnt, int half, int round, List<int> bases)
	{
		SetStrike (strike);
		SetBall (ball);
		SetOut (outCnt);
		SetBases (bases);
		SetHalftime (half);
		SetRound (round);
	}

	public void ClearBoard()
	{
		SetStrike (0);
		SetBall (0);
		SetOut (0);
		SetBases (new List<int> ());
		SetHalftime (0);
		SetRound (1);
	}

	public void SetHalftime(int value)
	{
		UISprite up = mRound.FindChild ("Up").GetComponent<UISprite> ();
		UISprite down = mRound.FindChild ("Down").GetComponent<UISprite> ();

		up.color = DISABLE;
		down.color = DISABLE;

		if(value == 0)
		{
			up.color = YELLOW;
		}
		else if(value == 1)
		{
			down.color = YELLOW;
		}

	}

	public void SetStrike(int cnt)
	{
		UISprite sprite1 = mStrike.FindChild ("Sprite1").GetComponent<UISprite> ();
		UISprite sprite2 = mStrike.FindChild ("Sprite2").GetComponent<UISprite> ();

		sprite1.color = DISABLE;
		sprite2.color = DISABLE;
		
		switch(cnt)
		{
		case 2:
			sprite2.color = GREEN;
			goto case 1;
		case 1:
			sprite1.color = GREEN;
			break;
		}
	}

	public void SetOut(int cnt)
	{
		UISprite sprite1 = mOut.FindChild ("Sprite1").GetComponent<UISprite> ();
		UISprite sprite2 = mOut.FindChild ("Sprite2").GetComponent<UISprite> ();

		sprite1.color = DISABLE;
		sprite2.color = DISABLE;

		switch(cnt)
		{
		case 2:
			sprite2.color = RED;
			goto case 1;
		case 1:
			sprite1.color = RED;
			break;
		}
	}

	public void SetBall(int cnt)
	{
		UISprite sprite1 = mBall.FindChild ("Sprite1").GetComponent<UISprite> ();
		UISprite sprite2 = mBall.FindChild ("Sprite2").GetComponent<UISprite> ();
		UISprite sprite3 = mBall.FindChild ("Sprite3").GetComponent<UISprite> ();

		sprite1.color = DISABLE;
		sprite2.color = DISABLE;
		sprite3.color = DISABLE;

		switch(cnt)
		{
		case 3:
			sprite3.color = YELLOW;
			goto case 2;
		case 2:
			sprite2.color = YELLOW;
			goto case 1;
		case 1:
			sprite1.color = YELLOW;
			break;
		}
	}

	public void SetBases(List<int> bases)
	{
		UISprite sprite1 = mSprBases.FindChild ("Sprite1").GetComponent<UISprite> ();
		UISprite sprite2 = mSprBases.FindChild ("Sprite2").GetComponent<UISprite> ();
		UISprite sprite3 = mSprBases.FindChild ("Sprite3").GetComponent<UISprite> ();

		sprite1.color = DISABLE;
		sprite2.color = DISABLE;
		sprite3.color = DISABLE;

		if (bases == null || bases.Count < 1)
						return;

		foreach(int value in bases)
		{
			if(value == 1)
			{
				sprite1.color = GREEN;
			}
			else if(value == 2)
			{
				sprite2.color = GREEN;
			}
			else if(value == 3)
			{
				sprite3.color = GREEN;
			}
		}
	}

	public void SetRound(int round)
	{
		mRound.GetComponent<UILabel> ().text = round.ToString ();
		mRound.FindChild ("Label").GetComponent<UILabel> ().text = UtilMgr.GetRoundString (round);
	}
}
