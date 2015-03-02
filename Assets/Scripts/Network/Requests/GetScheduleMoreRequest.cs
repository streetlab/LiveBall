using UnityEngine;
using System.Collections;
using System.Text;

public class GetScheduleMoreRequest : BaseRequest {
	int _memSeq;
	string _date;
	private string _teamCode;
	private int _teamSeq;


	public string date
	{
		get{return _date;}
		set{_date = value;}
	}

	public int memSeq
	{
		get{return _memSeq;}
		set{_memSeq = value;}
	}

	public int teamSeq
	{
		get{return _teamSeq;}
		set{_teamSeq = value;}
	}

	public string teamCode
	{
		get{return _teamCode;}
		set{_teamCode = value;}
	}

	public GetScheduleMoreRequest()
	{
		this.memSeq = UserMgr.GetUserInfo ().memSeq;
		this.date = UtilMgr.GetDateTime ("yyyyMMdd");
		this.teamCode = UserMgr.GetUserInfo ().teamCode;
		this.teamSeq = UserMgr.GetUserInfo ().teamSeq;

		mParams = JsonFx.Json.JsonWriter.Serialize (this);

	}

	public override string GetType ()
	{
		return "apps";
	}

	public override string GetQueryId()
	{
		return "bcastGetScheduleMore";
	}

}
