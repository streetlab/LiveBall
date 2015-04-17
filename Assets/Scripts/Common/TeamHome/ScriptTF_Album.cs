using UnityEngine;
using System.Collections;

public class ScriptTF_Album : MonoBehaviour {

	public UniWebView mWebViewObject;

	public void SetWevViewVisible(string status){
		if(status.Equals("")){
//			mWebViewObject.SetVisibility(true);
		} else{
//			mWebViewObject.SetVisibility(false);
		}
	}

	public void OpenWebView()
	{
		//		string strUrl = string.Format("{0}?{1}={2}&{3}={4}&{5}={6}&{7}={8}&{9}={10}"
		//		                              , TDP_URL
		//		                              , "AppID", this.AppID
		//		                              , "EncryptSecret", this.EncryptSecret
		//		                              , "DeviceID", this.DeviceID
		//		                              , "MobileAccountID", this.MobileAccountID
		//		                              , "Price", amount);
		//		
		//		Debug.Log(strUrl);
		//		Application.OpenURL(strUrl);
		string strUrl = "https://game.nanoo.so/liveball/board/42698?cd=185";
		
		//		webViewObject =
		//			(new GameObject("WebViewObject")).AddComponent<WebViewObject>();
		mWebViewObject.url = strUrl;
		mWebViewObject.Load ();
	}
}
