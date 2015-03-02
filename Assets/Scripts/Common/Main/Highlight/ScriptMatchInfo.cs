using UnityEngine;
using System.Collections;

public class ScriptMatchInfo : MonoBehaviour {

	static Color WHITE = new Color (1f, 1f, 1f);
	static Color YELLOW = new Color (1f, 1f, 0f);
	static Color GREEN = new Color (0f, 1f, 0f);
	static Color RED = new Color (1f, 0f, 0f);

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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetStrike(int cnt)
	{
		UISprite sprite1 = mStrike.FindChild ("Sprite1").GetComponent<UISprite> ();
		UISprite sprite2 = mStrike.FindChild ("Sprite2").GetComponent<UISprite> ();

		sprite1.color = WHITE;
		sprite2.color = WHITE;
		
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

		sprite1.color = WHITE;
		sprite2.color = WHITE;

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

		sprite1.color = WHITE;
		sprite2.color = WHITE;
		sprite3.color = WHITE;

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

	public void SetBases(int[] cnts)
	{
		UISprite sprite1 = mSprBases.FindChild ("Sprite1").GetComponent<UISprite> ();
		UISprite sprite2 = mSprBases.FindChild ("Sprite2").GetComponent<UISprite> ();
		UISprite sprite3 = mSprBases.FindChild ("Sprite3").GetComponent<UISprite> ();

		sprite1.color = WHITE;
		sprite2.color = WHITE;
		sprite3.color = WHITE;

		for(int i = 0 ; i < cnts.Length; i++)
		{
			if(cnts[i] == 1)
			{
				sprite1.color = GREEN;
			}
			else if(cnts[i] == 2)
			{
				sprite2.color = GREEN;
			}
			else if(cnts[i] == 3)
			{
				sprite3.color = GREEN;
			}
		}
	}

	public void SetRound(string round)
	{
		mRound.GetComponent<UILabel> ().text = round;
		if(round.Equals("1"))
		{
			mRound.FindChild ("Label").GetComponent<UILabel> ().text = "ST";
		}
		else if(round.Equals("2"))
		{
			mRound.FindChild ("Label").GetComponent<UILabel> ().text = "ND";
		}
		else if(round.Equals("3"))
		{
			mRound.FindChild ("Label").GetComponent<UILabel> ().text = "RD";
		}
		else
		{
			mRound.FindChild ("Label").GetComponent<UILabel> ().text = "TH";
		}
	}
}
