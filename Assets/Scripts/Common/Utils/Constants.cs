using UnityEngine;
using System.Collections;

public class Constants {	
	public const float DEFAULT_SCR_RATIO = 505f / 296f;

	public const string APPTAG = "StreetLab_Tuby";
	
	public const bool IS_DEBUGGABLE = false;
	
	public const bool	IS_TSTORE = false;
	public const string	MARKET_URI_TSTORE = "PRODUCT_VIEW/0000308300/0";
	
	public const string HOST					= 	"appif.friize.com";
	public const string TEST_HOST					= 	"192.168.0.5";
	
	public const string COMMUNITY_URL = "http://cafe.naver.com/tuby";
	
	public const string CASHSLIDE_APP_ID = "g73e1946";
	
	public const string DF_SPORTS_FOOTBALL = "DF_SPORTS_FOOTBALL";
	public const string DF_SPORTS_BASEBALL = "DF_SPORTS_BASEBALL";
	public const string DF_SPORTS_VOLLEYBALL = "DF_SPORTS_VOLLEYBALL";
	public const string DF_SPORTS_BASKETBALL = "DF_SPORTS_BASKETBALL";
	
	/*/
	// Real
	public static string QUERY_SERVER_HOST 	= 	"http://" + HOST + ":5002/webTuby/query.frz";
	public static string IMAGE_SERVER_HOST 	= 	"http://" + HOST + ":5002/tuby_file/";
	
	public static string WITHDRAW_URL 				= 	"http://auth.friize.com/m/withdraw.php";
	public static string EVENT_URL 					= 	"http://tuby10.friize.com/events";
	public static string EVENT_ATTENDANCE_URL 		= 	"http://tuby10.friize.com/events/attendance/";
	public static string EVENT_ATTENDANCE_CONFIRM_URL = 	"http://tuby10.friize.com/events/attendance_confirm/";
	public static string EVENT_NOTI_URL 				= 	"http://tuby10.friize.com/events/T";
	public static string EVENT_NOTI_CONFIRM_URL 		= 	"http://tuby10.friize.com/events/noti";
	public static string MAIL_BOX_URL 				= 	"http://auth.friize.com/mailbox/";
	public static string MAIL_BOX_RECEIVE_REWARD_URL 	= 	"http://auth.friize.com/mailbox/receive.php";
	/*/
	
//	public const string QUERY_SERVER_HOST 				= 	"http://" + TEST_HOST + ":5002/gameServer/query.frz";
	// Test
	public static string QUERY_SERVER_HOST 				= 	"http://" + HOST + ":6002/webTuby/query.frz";
	public static string IMAGE_SERVER_HOST 				= 	"http://" + HOST + ":6002/tuby_file/";

	public static string WITHDRAW_URL 				= 	"http://auth.friize.com/m/withdraw.php";
	public static string EVENT_URL 						= 	"http://test.streetlab.co.kr/events";
	public static string EVENT_ATTENDANCE_URL 			= 	"http://test.streetlab.co.kr/events/attendance/";
	public static string EVENT_ATTENDANCE_CONFIRM_URL 	= 	"http://test.streetlab.co.kr/events/attendance_confirm/";
	public static string EVENT_NOTI_URL 					= 	"http://test.streetlab.co.kr/events/t/";
	public static string EVENT_NOTI_CONFIRM_URL 			= 	"http://test.streetlab.co.kr/events/noti";
	public static string MAIL_BOX_URL 					= 	"http://auth.friize.com/mailbox/";
	public static string MAIL_BOX_RECEIVE_REWARD_URL 		= 	"http://auth.friize.com/mailbox/receive.php";
	//*/
	
	//	static string QUERY_SERVER_HOST_R 	= 	"http://" + HOST + ":5002/gameServer/query.frz";
	//	static string IMAGE_SERVER_HOST_R 	= 	"http://" + HOST + ":5002/tubyfiles/";
	//
	//	static string WITHDRAW_URL_R 				= 	"http://auth.friize.com/m/withdraw.php";
	//	static string EVENT_URL_R 					= 	"http://tuby10.friize.com/events";
	//	static string EVENT_ATTENDANCE_URL_R 		= 	"http://tuby10.friize.com/events/attendance/";
	//	static string EVENT_ATTENDANCE_CONFIRM_URL_R = 	"http://tuby10.friize.com/events/attendance_confirm/";
	//	static string EVENT_NOTI_URL_R 				= 	"http://tuby10.friize.com/events/T";
	//	static string EVENT_NOTI_CONFIRM_URL_R 		= 	"http://tuby10.friize.com/events/noti";
	//	static string MAIL_BOX_URL_R 				= 	"http://auth.friize.com/mailbox/";
	//	static string MAIL_BOX_RECEIVE_REWARD_URL_R 	= 	"http://auth.friize.com/mailbox/receive.php";
	
	static string QUERY_SERVER_HOST_T 				= 	"http://" + HOST + ":6002/gameServer/query.frz";
	static string IMAGE_SERVER_HOST_T 				= 	"http://" + HOST + ":6002/tubyfiles/";
	
	static string WITHDRAW_URL_T 				= 	"http://auth.friize.com/m/withdraw.php";
	static string EVENT_URL_T 						= 	"http://test.streetlab.co.kr/events";
	static string EVENT_ATTENDANCE_URL_T 			= 	"http://test.streetlab.co.kr/events/attendance/";
	static string EVENT_ATTENDANCE_CONFIRM_URL_T 	= 	"http://test.streetlab.co.kr/events/attendance_confirm/";
	static string EVENT_NOTI_URL_T 					= 	"http://test.streetlab.co.kr/events/t/";
	static string EVENT_NOTI_CONFIRM_URL_T 			= 	"http://test.streetlab.co.kr/events/noti";
	static string MAIL_BOX_URL_T 					= 	"http://auth.friize.com/mailbox/";
	static string MAIL_BOX_RECEIVE_REWARD_URL_T 		= 	"http://auth.friize.com/mailbox/receive.php";
	
	public const string IMAGE_HTTP_PREFIX 		= 	"http://";
	public const string IMAGE_ICON_URL_PREFIX 	= 	"icon_";
	public const string IMAGE_POSTER_URL_PREFIX 	= 	"poster_";

//	<!-- Preference String -->
	public const string PrefGcm_registeration_id = "gcm_registeration_id";
	public const string PrefProperty_app_version = "property_app_version";
	public const string PrefMem_seq = "mem_seq";
	public const string PrefBetting_golden_ball = "betting_golden_ball";
	public const string PrefMy_total_golden_ball = "my_total_golden_ball";
	public const string PrefMy_temp_golden_ball = "my_temp_golden_ball";
	public const string PrefMy_total_ruby = "my_total_ruby";
	public const string PrefMy_total_diamond = "my_total_diamond";
	public const string PrefEmail = "email";
	public const string PrefPwd = "pwd";
	public const string PrefRegistType = "RegistType";
	public const string PrefServerTest = "serverTest";
	public const string PrefSetting_vibrate_on_off = "setting_vibrate_on_off";
	public const string PrefSetting_watching_method = "setting_system_watching_method";
	public const string PrefIs_first_installed = "is_first_installed";
			
	public static void setServerTest(){
		string flag = PlayerPrefs.GetString (PrefServerTest);
		Debug.Log ("test flag : " + flag);
		if(flag.Equals("D")){
			QUERY_SERVER_HOST = "http://" + TEST_HOST + ":5002/gameServer/query.frz";
			IMAGE_SERVER_HOST = IMAGE_SERVER_HOST_T;
			
			WITHDRAW_URL =  WITHDRAW_URL_T;
			EVENT_URL = EVENT_URL_T;
			EVENT_ATTENDANCE_URL = EVENT_ATTENDANCE_URL_T;
			EVENT_ATTENDANCE_CONFIRM_URL = EVENT_ATTENDANCE_CONFIRM_URL_T;
			EVENT_NOTI_URL = EVENT_NOTI_URL_T;
			EVENT_NOTI_CONFIRM_URL = EVENT_NOTI_CONFIRM_URL_T;
			MAIL_BOX_URL = MAIL_BOX_URL_T;
			MAIL_BOX_RECEIVE_REWARD_URL = MAIL_BOX_RECEIVE_REWARD_URL_T;
		} else{
			QUERY_SERVER_HOST = QUERY_SERVER_HOST_T;
			IMAGE_SERVER_HOST = IMAGE_SERVER_HOST_T;
			
			WITHDRAW_URL =  WITHDRAW_URL_T;
			EVENT_URL = EVENT_URL_T;
			EVENT_ATTENDANCE_URL = EVENT_ATTENDANCE_URL_T;
			EVENT_ATTENDANCE_CONFIRM_URL = EVENT_ATTENDANCE_CONFIRM_URL_T;
			EVENT_NOTI_URL = EVENT_NOTI_URL_T;
			EVENT_NOTI_CONFIRM_URL = EVENT_NOTI_CONFIRM_URL_T;
			MAIL_BOX_URL = MAIL_BOX_URL_T;
			MAIL_BOX_RECEIVE_REWARD_URL = MAIL_BOX_RECEIVE_REWARD_URL_T;
		}
	}

	public const string POST_SPOS_STATUS = "1000";//	경기상태통보
	public const string POST_GAME_STATUS = "1100";//경기상태변경
	public const string POST_GAME_START = "1101";//경기시작
	public const string POST_GAME_STOP = "1102";//경기종료
	public const string POST_QUIZ_START = "1201";//퀴즈시작
	public const string POST_QUIZ_RESULT = "1202";//퀴즈결과
	public const string POST_QUIZ_CANCEL = "1203";//퀴즈무효
	public const string POST_QUIZ_REFUND = "1204";//퀴즈환불
	public const string POST_QUIZ_MODIFY = "1205";//퀴즈정정
	public const string POST_GAME = "1301";//스코어보드 (모든이닝점수, 각팀의 에러,히트, 홈런....)
	public const string POST_SCORE = "1302";//점수, 주루, 볼카운트, 선수(투수,타자)정보
	public const string POST_PLAYER = "1303";//점수, 주루, 볼카운트, 선수(투수,타자)정보
	public const string POST_BCNT = "1304";//점수, 주루, 볼카운트, 선수(투수,타자)정보
	public const string POST_MSG = "2000";//일반메시지 - 앱을 실행시킴
	public const string POST_POPUP = "2100";//팝업공지

}
