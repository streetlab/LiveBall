using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetPreparedQuizResponse : BaseResponse {
	QuizListInfo _data;

	public QuizListInfo data
	{
		get{ return _data;}
		set{ _data = value;}
	}
}
