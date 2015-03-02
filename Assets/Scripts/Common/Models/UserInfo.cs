using UnityEngine;
using System.Collections;

public class UserInfo {

	int _memSeq;
	int _teamSeq;
	string _teamCode;

	public int memSeq
	{
		get{ return _memSeq;}
		set{_memSeq = value;}
	}

	public int teamSeq
	{
		get{ return _teamSeq;}
		set{_teamSeq = value;}
	}

	public string teamCode
	{
		get{return _teamCode;}
		set{_teamCode = value;}
	}



}
