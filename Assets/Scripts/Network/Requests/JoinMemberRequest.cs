using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class JoinMemberRequest : BaseUploadRequest {

	public JoinMemberRequest(JoinMemberInfo memInfo)
	{
		AddField ("memberEmail", memInfo.MemberEmail);
//		Add ("memberEmail", memInfo.MemberID);
		AddField ("memberName", memInfo.MemberName);
		AddField ("osType", memInfo.OsType);
		AddField ("registType", memInfo.RegistType);
		AddField ("memberPwd", memInfo.MemberPwd);
		AddField ("memUID", memInfo.MemUID);
		AddField ("memImage", memInfo.MemImage);

		if (memInfo.Photo != null && memInfo.Photo.Length > 0) {
			byte[] bytes = File.ReadAllBytes(memInfo.Photo);
			AddBinaryData("file", bytes, "profile.png", "image/png");
		}


	}

	public override string GetType ()
	{
		return "apps";
	}

	public override string GetQueryId()
	{
		return "tubyGetCardInven";
	}

}
