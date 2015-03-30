using UnityEngine;
using System.Collections;

public class UserInfo {
	int _memSeq;

	public int memSeq {
		get {
			return _memSeq;
		}
		set {
			_memSeq = value;
		}
	}

	int _memberNo;
	public int memberNo {
		get {
			return _memberNo;
		}
		set {
			_memberNo = value;
		}
	}

//: 1000000416,
	string _memberEmail;
	public string memberEmail {
		get {
			return _memberEmail;
		}
		set {
			_memberEmail = value;
		}
	}

//: "gunloves@.",
	string _memberName;
	public string memberName {
		get {
			return _memberName;
		}
		set {
			_memberName = value;
		}
	}

//: "gunloves",
	int _registType;
	public int registType {
		get {
			return _registType;
		}
		set {
			_registType = value;
		}
	}

	private string _imagePath;
	
	public string imagePath {
		get {
			return _imagePath;
		}
		set {
			_imagePath = value;
		}
	}

// 3,
	string _imageName;
	public string imageName {
		get {
			return _imageName;
		}
		set {
			_imageName = value;
		}
	}

// "",
	int _imageWidth;
	public int imageWidth {
		get {
			return _imageWidth;
		}
		set {
			_imageWidth = value;
		}
	}

// 0,
	int _imageHeight;
	public int imageHeight {
		get {
			return _imageHeight;
		}
		set {
			_imageHeight = value;
		}
	}

// 0,
	int _useNotify;
	public int useNotify {
		get {
			return _useNotify;
		}
		set {
			_useNotify = value;
		}
	}

// 1,
	string _userGoldenBall;
	public string userGoldenBall {
		get {
			return _userGoldenBall;
		}
		set {
			_userGoldenBall = value;
		}
	}

// "775296",
	string _userDiamond;
	public string userDiamond {
		get {
			return _userDiamond;
		}
		set {
			_userDiamond = value;
		}
	}

// "982892",
	string _userRuby;
	public string userRuby {
		get {
			return _userRuby;
		}
		set {
			_userRuby = value;
		}
	}

// "9432",
	string _useActiveDiamond;
	public string useActiveDiamond {
		get {
			return _useActiveDiamond;
		}
		set {
			_useActiveDiamond = value;
		}
	}

// "0",
	int _followingCount;
	public int followingCount {
		get {
			return _followingCount;
		}
		set {
			_followingCount = value;
		}
	}

// 0,
	int _followerCount;
	public int followerCount {
		get {
			return _followerCount;
		}
		set {
			_followerCount = value;
		}
	}

// 0,
	int _teamSeq;

	public int teamSeq {
		get {
			return _teamSeq;
		}
		set {
			_teamSeq = value;
		}
	}

	string _teamCode;

	public string teamCode {
		get {
			return _teamCode;
		}
		set {
			_teamCode = value;
		}
	}

		//favoBB;// {
		//	int _teamSeq;// 11,
		//	teamName;// "넥센",
		//	teamFullName;// "넥센 히어로즈",
		//	imageName;// "sports_team_baseball_wo.png",
		//	imageWidth;// 200,
		//	imageHeight;// 200,
		//	teamColor;// "#8f0a27",
		//	teamCode;// "WO",
		//	contentsType;// 2001
		//	},
	int _isBlackList;
	public int isBlackList {
		get {
			return _isBlackList;
		}
		set {
			_isBlackList = value;
		}
	}

// 0,
	int _activeAuth;
	public int activeAuth {
		get {
			return _activeAuth;
		}
		set {
			_activeAuth = value;
		}
	}

// 1,
	int _active;
	public int active {
		get {
			return _active;
		}
		set {
			_active = value;
		}
	}

// 1,
	string _phoneNum;// "01011112341",
	//item;// [
//	       {
//	comboShieldYn;// 0,
//	itemName;// "베팅쉴드",
//	itemCountUse;// 1,
//	multipleRatio;// 1,
//	betShieldYn;// 1,
//	itemCount;// 4,
//	itemId;// 1020,
//	itemCode;// "ITEM_BET_SHIELD"
//	       },
//	       {
//	comboShieldYn;// 1,
//	itemName;// "콤보쉴드",
//	itemCountUse;// 2,
//	multipleRatio;// 1,
//	betShieldYn;// 0,
//	itemCount;// 3,
//	itemId;// 1010,
//	itemCode;// "ITEM_COMBO_SHIELD"
//	       },
//	       {
//	comboShieldYn;// 0,
//	itemName;// "배당금2배",
//	itemCountUse;// 4,
//	multipleRatio;// 2,
//	betShieldYn;// 0,
//	itemCount;// 6,
//	itemId;// 1200,
//	itemCode;// "ITEM_MULTIPLE_200X"
//	       },
//	       {
//	comboShieldYn;// 0,
//	itemName;// "배당금3배",
//	itemCountUse;// 1,
//	multipleRatio;// 3,
//	betShieldYn;// 0,
//	itemCount;// 9,
//	itemId;// 1300,
//	itemCode;// "ITEM_MULTIPLE_300X"
//	       }
//	       ],
//social;// [
//	         {
//	rowNum;// 1,
//	title;// "뚜비 출시이벤트 보상 (골든볼 1000개)이 지급되었습니다."
//	         }
//	         ]



	public string phoneNum {
		get {
			return _phoneNum;
		}
		set {
			_phoneNum = value;
		}
	}
}
