using UnityEngine;
using System.Collections;
using System.Text;

public class GetSimpleResultRequest : BaseRequest {

	public GetSimpleResultRequest(int quizListSeq)
	{
		Add ("memSeq", UserMgr.UserInfo.memSeq);
		Add ("gameSeq", QuizMgr.QuizInfo.gameSeq);
		Add ("quizListSeq", quizListSeq);

		mParams = JsonFx.Json.JsonWriter.Serialize (this);

	}

	public override string GetType ()
	{
		return "spos";
	}

	public override string GetQueryId()
	{
		return "gameSposSimpleQuizResult";
	}

}
