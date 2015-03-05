using UnityEngine;
using System.Collections;
using System.Text;

public class GetGameSposDetailBoardRequest : BaseRequest {

	public GetGameSposDetailBoardRequest()
	{
		Add ("memSeq", UserMgr.GetUserInfo ().memSeq);
		Add ("gameSeq", UserMgr.Schedule.gameSeq);

		mParams = JsonFx.Json.JsonWriter.Serialize (this);

	}

	public override string GetType ()
	{
		return "spos";
	}

	public override string GetQueryId()
	{
		return "gameSposDetailBoard";
	}

}
