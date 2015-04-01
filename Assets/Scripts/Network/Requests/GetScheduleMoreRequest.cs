using UnityEngine;
using System.Collections;
using System.Text;

public class GetScheduleMoreRequest : BaseRequest {

	public GetScheduleMoreRequest()
	{
		Add ("memSeq", UserMgr.UserInfo.memSeq);
		Add ("date", UtilMgr.GetDateTime ("yyyyMMdd"));
		Add ("teamCode", UserMgr.UserInfo.teamCode);
		Add ("teamSeq", UserMgr.UserInfo.teamSeq);

		mParams = JsonFx.Json.JsonWriter.Serialize (this);

	}

	public override string GetType ()
	{
		return "apps";
	}

	public override string GetQueryId()
	{
		return "bcastGetScheduleMore";
//		return "bcastGetScheduleAll";
	}

}
