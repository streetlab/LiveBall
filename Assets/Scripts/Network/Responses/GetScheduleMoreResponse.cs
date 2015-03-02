using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetScheduleMoreResponse : BaseResponse {
	List<ScheduleInfo> _data;

	public List<ScheduleInfo> data
	{
		get{ return _data;}
		set{ _data = value;}
	}
}
