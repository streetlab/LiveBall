using UnityEngine;
using System.Collections;

public class Constants {	
	public static string APPTAG = "StreetLab_Tuby";
	
	public static bool IS_DEBUGGABLE = false;
	
	public static bool	IS_TSTORE = false;
	public static string	MARKET_URI_TSTORE = "PRODUCT_VIEW/0000308300/0";
	
	public static string HOST					= 	"appif.friize.com";
	public static string TEST_HOST					= 	"192.168.0.5";
	
	public static string COMMUNITY_URL = "http://cafe.naver.com/tuby";
	
	public static string CASHSLIDE_APP_ID = "g73e1946";
	
	public static string DF_SPORTS_FOOTBALL = "DF_SPORTS_FOOTBALL";
	public static string DF_SPORTS_BASEBALL = "DF_SPORTS_BASEBALL";
	public static string DF_SPORTS_VOLLEYBALL = "DF_SPORTS_VOLLEYBALL";
	public static string DF_SPORTS_BASKETBALL = "DF_SPORTS_BASKETBALL";
	
	/*/
	// Real
	public static string QUERY_SERVER_HOST 	= 	"http://" + HOST + ":5002/webTuby/query.frz";
	public static string IMAGE_SERVER_HOST 	= 	"http://" + HOST + ":5002/tubyfiles/";
	
	public static string WITHDRAW_URL 				= 	"http://auth.friize.com/m/withdraw.php";
	public static string EVENT_URL 					= 	"http://tuby10.friize.com/events";
	public static string EVENT_ATTENDANCE_URL 		= 	"http://tuby10.friize.com/events/attendance/";
	public static string EVENT_ATTENDANCE_CONFIRM_URL = 	"http://tuby10.friize.com/events/attendance_confirm/";
	public static string EVENT_NOTI_URL 				= 	"http://tuby10.friize.com/events/T";
	public static string EVENT_NOTI_CONFIRM_URL 		= 	"http://tuby10.friize.com/events/noti";
	public static string MAIL_BOX_URL 				= 	"http://auth.friize.com/mailbox/";
	public static string MAIL_BOX_RECEIVE_REWARD_URL 	= 	"http://auth.friize.com/mailbox/receive.php";
	/*/
	
//	public static string QUERY_SERVER_HOST 				= 	"http://" + TEST_HOST + ":5002/gameServer/query.frz";
	// Test
	public static string QUERY_SERVER_HOST 				= 	"http://" + HOST + ":6002/webTuby/query.frz";
	public static string IMAGE_SERVER_HOST 				= 	"http://" + HOST + ":6002/tubyfiles/";

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
	
	public static string IMAGE_HTTP_PREFIX 		= 	"http://";
	public static string IMAGE_ICON_URL_PREFIX 	= 	"icon_";
	public static string IMAGE_POSTER_URL_PREFIX 	= 	"poster_";

//	<!-- Preference String -->
	public static string PrefGcm_registeration_id = "gcm_registeration_id";
	public static string PrefProperty_app_version = "property_app_version";
	public static string PrefMem_seq = "mem_seq";
	public static string PrefBetting_golden_ball = "betting_golden_ball";
	public static string PrefMy_total_golden_ball = "my_total_golden_ball";
	public static string PrefMy_temp_golden_ball = "my_temp_golden_ball";
	public static string PrefMy_total_ruby = "my_total_ruby";
	public static string PrefMy_total_diamond = "my_total_diamond";
	public static string PrefEmail = "emain";
	public static string PrefPwd = "pwd";
	public static string PrefServerTest = "serverTest";
	public static string PrefSetting_vibrate_on_off = "setting_vibrate_on_off";
	public static string PrefSetting_watching_method = "setting_system_watching_method";
	public static string PrefIs_first_installed = "is_first_installed";
			
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

}
