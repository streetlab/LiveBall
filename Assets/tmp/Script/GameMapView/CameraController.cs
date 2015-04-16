using UnityEngine;
using System.Collections;

public enum UserCastle {

	moo_wi,
	seo_pyeong,
	cheon_soo,
	moo_do,
	an_zeong,
	han_zoong,
	zang_an,
	za_dong,
	seong_do,
	gang_zoo,
	geon_nyeong,
	oon_nam,
	yeong_chang,
	gyo_zi,
	nam_hae,
	gye_yang,
	yeong_rng,
	zang_sa,
	moo_rng,
	gang_rng,
	gang_ha,
	yang_yang,
	sin_ya,
	wan,
	sang_yong,
	yeong_an,
	nak_yang,
	yeo_nam,
	heo_chang,
	zin_ru,
	bok_yang,
	ha_nae,
	sang_dang,
	zin_yang,
	up,
	pyeong_won,
	nam_pi,
	gye,
	book_pyeong,
	yang_pyeong,
	book_hae,
	ha_bi,
	so_pae,
	soo_choon,
	yeo_gang,
	geon_up,
	o,
	hoi_gye,
	geon_an,
	si_sang,
	myeon_zook_gwan,
	geom_gak,
	yang_pyeong_gwan,
	moo_gwan,
	dong_gwan,
	horo_gwan,
	ham_gok_gwan,
	ho_gwan

}

public enum CastleClickState {
	
	moo_wi,
	seo_pyeong,
	cheon_soo,
	moo_do,
	an_zeong,
	han_zoong,
	zang_an,
	za_dong,
	seong_do,
	gang_zoo,
	geon_nyeong,
	oon_nam,
	yeong_chang,
	gyo_zi,
	nam_hae,
	gye_yang,
	yeong_rng,
	zang_sa,
	moo_rng,
	gang_rng,
	gang_ha,
	yang_yang,
	sin_ya,
	wan,
	sang_yong,
	yeong_an,
	nak_yang,
	yeo_nam,
	heo_chang,
	zin_ru,
	bok_yang,
	ha_nae,
	sang_dang,
	zin_yang,
	up,
	pyeong_won,
	nam_pi,
	gye,
	book_pyeong,
	yang_pyeong,
	book_hae,
	ha_bi,
	so_pae,
	soo_choon,
	yeo_gang,
	geon_up,
	o,
	hoi_gye,
	geon_an,
	si_sang,
	myeon_zook_gwan,
	geom_gak,
	yang_pyeong_gwan,
	moo_gwan,
	dong_gwan,
	horo_gwan,
	ham_gok_gwan,
	ho_gwan
	
}

public enum UserActionState {

	MOVE,
	WAIT

}

public class CameraController : MonoBehaviour {

	public UserCastle uc;
	public UserActionState uas;
	public CastleClickState ccs;

	private string dbName = "Users.db";
	private dbAccess db;

	// 성, 관문 변수
	public GameObject moo_wi;
	public GameObject seo_pyeong;
	public GameObject cheon_soo;
	public GameObject moo_do;
	public GameObject an_zeong;
	public GameObject han_zoong;
	public GameObject zang_an;
	public GameObject za_dong;
	public GameObject seong_do;
	public GameObject gang_zoo;
	public GameObject geon_nyeong;
	public GameObject oon_nam;
	public GameObject yeong_chang;
	public GameObject gyo_zi;
	public GameObject nam_hae;
	public GameObject gye_yang;
	public GameObject yeong_rng;
	public GameObject zang_sa;
	public GameObject moo_rng;
	public GameObject gang_rng;
	public GameObject gang_ha;
	public GameObject yang_yang;
	public GameObject sin_ya;
	public GameObject wan;
	public GameObject sang_yong;
	public GameObject yeong_an;
	public GameObject nak_yang;
	public GameObject yeo_nam;
	public GameObject heo_chang;
	public GameObject zin_ru;
	public GameObject bok_yang;
	public GameObject ha_nae;
	public GameObject sang_dang;
	public GameObject zin_yang;
	public GameObject up;
	public GameObject pyeong_won;
	public GameObject nam_pi;
	public GameObject gye;
	public GameObject book_pyeong;
	public GameObject yang_pyeong;
	public GameObject book_hae;
	public GameObject ha_bi;
	public GameObject so_pae;
	public GameObject soo_choon;
	public GameObject yeo_gang;
	public GameObject geon_up;
	public GameObject o;
	public GameObject hoi_gye;
	public GameObject geon_an;
	public GameObject si_sang;
	public GameObject myeon_zook_gwan;
	public GameObject geom_gak;
	public GameObject yang_pyeong_gwan;
	public GameObject moo_gwan;
	public GameObject dong_gwan;
	public GameObject horo_gwan;
	public GameObject ham_gok_gwan;
	public GameObject ho_gwan;
	// 위치표시 화살표
	public GameObject arrow;




	// 마우스 포인터로 사용할 텍스터
	public Texture2D cursorTexture;
	public Texture2D at_cursorTexture;
	//텍스처의 중심을 마우스 좌표로 할 것인지 체크박스로 입력받습니다.
	public bool hotSpotIsCenter = false;
	//텍스처의 어느부분을 마우스의 좌표로 할 것인지 텍스처의 
	//좌표를 입력받습니다.
	public Vector2 adjustHotSpot = Vector2.zero;
	//내부에서 사용할 필드를 선업합니다.
	public Vector2 hotSpot;

	public float cmSpeed = 5f;
	string curName;

	// Use this for initialization
	void Start () {
	
		db = GetComponent<dbAccess>(); // 디비 스크립트 연결
		db.OpenDB (dbName);
		uas = UserActionState.WAIT;
		StartCoroutine("MyCursor2");
		curName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString("curUnitName")));
		set_Ki();
		castlePivotCamera();
		db.CloseDB();

	}

	IEnumerator MyCursor2() {
		
		yield return new WaitForEndOfFrame();
		
		//텍스처의 중심을 마우스의 좌표로 사용하는 경우 
		//텍스처의 폭과 높이의 1/2을 hot Spot 좌표로 입력합니다.
		if (hotSpotIsCenter) {
			hotSpot.x = cursorTexture.width / 2;
			hotSpot.y = cursorTexture.height / 2;
		} else {
			//중심을 사용하지 않을 경우 Adjust Hot Spot으로 입력 받은 
			//것을 사용합니다.
			hotSpot = adjustHotSpot;
		}
		//이제 새로운 마우스 커서를 화면에 표시합니다.
		Cursor.SetCursor (cursorTexture, hotSpot, CursorMode.Auto);
		
	}

	public void cameraControll() {
		
		// 카메로 조종
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (camera.transform.position.x >= -12.55f) {
				transform.Translate(Vector3.left * cmSpeed * Time.deltaTime);
			}
		} 		  
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			if (camera.transform.position.x <= 21.8f) {
				transform.Translate (Vector3.right * cmSpeed * Time.deltaTime);
			}
		}
		
		if (Input.GetKey (KeyCode.UpArrow)) {
			if (camera.transform.position.y <= 9.5f) {
				transform.Translate (Vector3.up * cmSpeed * Time.deltaTime);
			}
		}
		
		if (Input.GetKey (KeyCode.DownArrow)) {
			if (camera.transform.position.y >= -11.25f) {
				transform.Translate (Vector3.down * cmSpeed * Time.deltaTime);
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			castlePivotCamera();
			cameraOneMoveCon();
		}
		
	}


	public void cameraOneMoveCon() {
		camera.transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
		
		if (camera.transform.position.x >= 21.8f) {
			camera.transform.position = new Vector3(21.7f, transform.position.y, -5f);
		}
		
		if (camera.transform.position.x <= -12.55f) {
			camera.transform.position = new Vector3(-12.54f, transform.position.y, -5f);
		}
		
		if (camera.transform.position.y >= 9.5) {
			camera.transform.position = new Vector3(transform.position.x, 9.4f, -5f);
		}
		
		if (camera.transform.position.y <= -11.25f) {
			camera.transform.position = new Vector3(transform.position.x, -11.24f,  -5f);
		}
		
		
		if (camera.transform.position.x >= 21.8f && camera.transform.position.y >= 9.5f) {
			camera.transform.position = new Vector3(21.7f, 9.4f, -5f);
		}
		
		if (camera.transform.position.x >= 21.8f && camera.transform.position.y <= -11.55f) {
			camera.transform.position = new Vector3(21.7f, -11.54f, -5f);
		}
		
		if (camera.transform.position.x <= -12.55f && camera.transform.position.y >= 9.5f) {
			camera.transform.position = new Vector3(-12.54f, 9.4f, -5f);
		} 
		
		if (camera.transform.position.x <= -12.55f && camera.transform.position.y <= -11.25f) {
			camera.transform.position = new Vector3(-12.55f, -11.24f, -5f);
		} 
		
		if (camera.transform.position.y >= 9.5f && camera.transform.position.x >= 21.8f) {
			camera.transform.position = new Vector3(21.7f, 9.4f, -5f);
		}
		
		if (camera.transform.position.y >= 9.5f && camera.transform.position.x <= -12.55f) {
			camera.transform.position = new Vector3(-12.54f, 9.4f, -5f);
		}
		
		if (camera.transform.position.y <= -11.25f && camera.transform.position.x >= 21.8f) {
			camera.transform.position = new Vector3(21.7f, -11.24f,  -5f);
		} 
		
		if (camera.transform.position.y <= -11.25f && camera.transform.position.x <= -12.55f) {
			camera.transform.position = new Vector3(-12.55f, -11.24f,  -5f);
		} 
	}


	public void castlePivotCamera() {

		switch(uc) {

		case UserCastle.an_zeong :
			camera.transform.position = new Vector3(an_zeong.transform.position.x, an_zeong.transform.position.y, -5f);
			break;

		case UserCastle.bok_yang :
			camera.transform.position = new Vector3(bok_yang.transform.position.x, bok_yang.transform.position.y, -5f);
			break;

		case UserCastle.book_hae :
			camera.transform.position = new Vector3(book_hae.transform.position.x, book_hae.transform.position.y, -5f);
			break;

		case UserCastle.book_pyeong :
			camera.transform.position = new Vector3(book_pyeong.transform.position.x, book_pyeong.transform.position.y, -5f);
			break;

		case UserCastle.cheon_soo :
			camera.transform.position = new Vector3(cheon_soo.transform.position.x, cheon_soo.transform.position.y, -5f);
			break;

		case UserCastle.gang_ha :
			camera.transform.position = new Vector3(gang_ha.transform.position.x, gang_ha.transform.position.y, -5f);
			break;

		case UserCastle.gang_rng :
			camera.transform.position = new Vector3(gang_rng.transform.position.x, gang_rng.transform.position.y, -5f);
			break;

		case UserCastle.gang_zoo :
			camera.transform.position = new Vector3(gang_zoo.transform.position.x, gang_zoo.transform.position.y, -5f);
			break;

		case UserCastle.geon_an :
			camera.transform.position = new Vector3(geon_an.transform.position.x, geon_an.transform.position.y, -5f);
			break;

		case UserCastle.geon_nyeong :
			camera.transform.position = new Vector3(geon_nyeong.transform.position.x, geon_nyeong.transform.position.y, -5f);
			break;

		case UserCastle.geon_up :
			camera.transform.position = new Vector3(geon_up.transform.position.x, geon_up.transform.position.y, -5f);
			break;

		case UserCastle.gye :
			camera.transform.position = new Vector3(gye.transform.position.x, gye.transform.position.y, -5f);
			break;

		case UserCastle.gye_yang :
			camera.transform.position = new Vector3(gye_yang.transform.position.x, gye_yang.transform.position.y, -5f);
			break;

		case UserCastle.gyo_zi :
			camera.transform.position = new Vector3(gyo_zi.transform.position.x, gyo_zi.transform.position.y, -5f);
			break;

		case UserCastle.ha_bi :
			camera.transform.position = new Vector3(ha_bi.transform.position.x, ha_bi.transform.position.y, -5f);
			break;

		case UserCastle.ha_nae :
			camera.transform.position = new Vector3(ha_nae.transform.position.x, ha_nae.transform.position.y, -5f);
			break;

		case UserCastle.han_zoong :
			camera.transform.position = new Vector3(han_zoong.transform.position.x, han_zoong.transform.position.y, -5f);
			break;

		case UserCastle.heo_chang :
			camera.transform.position = new Vector3(heo_chang.transform.position.x, heo_chang.transform.position.y, -5f);
			break;

		case UserCastle.hoi_gye :
			camera.transform.position = new Vector3(hoi_gye.transform.position.x, hoi_gye.transform.position.y, -5f);
			break;

		case UserCastle.moo_do :
			camera.transform.position = new Vector3(moo_do.transform.position.x, moo_do.transform.position.y, -5f);
			break;

		case UserCastle.moo_rng :
			camera.transform.position = new Vector3(moo_rng.transform.position.x, moo_rng.transform.position.y, -5f);
			break;

		case UserCastle.moo_wi :
			camera.transform.position = new Vector3(moo_wi.transform.position.x, moo_wi.transform.position.y, -5f);
			break;

		case UserCastle.nak_yang :
			camera.transform.position = new Vector3(nak_yang.transform.position.x, nak_yang.transform.position.y, -5f);
			break;

		case UserCastle.nam_hae :
			camera.transform.position = new Vector3(nam_hae.transform.position.x, nam_hae.transform.position.y, -5f);
			break;

		case UserCastle.nam_pi :
			camera.transform.position = new Vector3(nam_pi.transform.position.x, nam_pi.transform.position.y, -5f);
			break;

		case UserCastle.o :
			camera.transform.position = new Vector3(o.transform.position.x, o.transform.position.y, -5f);
			break;

		case UserCastle.oon_nam :
			camera.transform.position = new Vector3(oon_nam.transform.position.x, oon_nam.transform.position.y, -5f);
			break;

		case UserCastle.pyeong_won :
			camera.transform.position = new Vector3(pyeong_won.transform.position.x, pyeong_won.transform.position.y, -5f);
			break;

		case UserCastle.sang_dang :
			camera.transform.position = new Vector3(sang_dang.transform.position.x, sang_dang.transform.position.y, -5f);
			break;

		case UserCastle.sang_yong :
			camera.transform.position = new Vector3(sang_yong.transform.position.x, sang_yong.transform.position.y, -5f);
			break;

		case UserCastle.seo_pyeong :
			camera.transform.position = new Vector3(seo_pyeong.transform.position.x, seo_pyeong.transform.position.y, -5f);
			break;

		case UserCastle.seong_do :
			camera.transform.position = new Vector3(seong_do.transform.position.x, seong_do.transform.position.y, -5f);
			break;

		case UserCastle.si_sang :
			camera.transform.position = new Vector3(si_sang.transform.position.x, si_sang.transform.position.y, -5f);
			break;

		case UserCastle.sin_ya :
			camera.transform.position = new Vector3(sin_ya.transform.position.x, sin_ya.transform.position.y, -5f);
			break;

		case UserCastle.so_pae :
			camera.transform.position = new Vector3(so_pae.transform.position.x, so_pae.transform.position.y, -5f);
			break;

		case UserCastle.soo_choon :
			camera.transform.position = new Vector3(soo_choon.transform.position.x, soo_choon.transform.position.y, -5f);
			break;

		case UserCastle.up :
			camera.transform.position = new Vector3(up.transform.position.x, up.transform.position.y, -5f);
			break;

		case UserCastle.wan :
			camera.transform.position = new Vector3(wan.transform.position.x, wan.transform.position.y, -5f);
			break;

		case UserCastle.yang_pyeong :
			camera.transform.position = new Vector3(yang_pyeong.transform.position.x, yang_pyeong.transform.position.y, -5f);
			break;

		case UserCastle.yang_yang :
			camera.transform.position = new Vector3(yang_yang.transform.position.x, yang_yang.transform.position.y, -5f);
			break;

		case UserCastle.yeo_gang :
			camera.transform.position = new Vector3(yeo_gang.transform.position.x, yeo_gang.transform.position.y, -5f);
			break;

		case UserCastle.yeo_nam :
			camera.transform.position = new Vector3(yeo_nam.transform.position.x, yeo_nam.transform.position.y, -5f);
			break;

		case UserCastle.yeong_an :
			camera.transform.position = new Vector3(yeong_an.transform.position.x, yeong_an.transform.position.y, -5f);
			break;

		case UserCastle.yeong_chang :
			camera.transform.position = new Vector3(yeong_chang.transform.position.x, yeong_chang.transform.position.y, -5f);
			break;

		case UserCastle.yeong_rng :
			camera.transform.position = new Vector3(yeong_rng.transform.position.x, yeong_rng.transform.position.y, -5f);
			break;

		case UserCastle.za_dong :
			camera.transform.position = new Vector3(za_dong.transform.position.x, za_dong.transform.position.y, -5f);
			break;

		case UserCastle.zang_an :
			camera.transform.position = new Vector3(zang_an.transform.position.x, zang_an.transform.position.y, -5f);
			break;

		case UserCastle.zang_sa :
			camera.transform.position = new Vector3(zang_sa.transform.position.x, zang_sa.transform.position.y, -5f);
			break;

		case UserCastle.zin_ru :
			camera.transform.position = new Vector3(zin_ru.transform.position.x, zin_ru.transform.position.y, -5f);
			break;

		case UserCastle.myeon_zook_gwan :
			camera.transform.position = new Vector3(myeon_zook_gwan.transform.position.x, myeon_zook_gwan.transform.position.y, -5f);
			break;

		case UserCastle.geom_gak :
			camera.transform.position = new Vector3(geom_gak.transform.position.x, geom_gak.transform.position.y, -5f);
			break;

		case UserCastle.yang_pyeong_gwan :
			camera.transform.position = new Vector3(yang_pyeong_gwan.transform.position.x, yang_pyeong_gwan.transform.position.y, -5f);
			break;

		case UserCastle.moo_gwan :
			camera.transform.position = new Vector3(moo_gwan.transform.position.x, moo_gwan.transform.position.y, -5f);
			break;

		case UserCastle.dong_gwan :
			camera.transform.position = new Vector3(dong_gwan.transform.position.x, dong_gwan.transform.position.y, -5f);
			break;

		case UserCastle.horo_gwan :
			camera.transform.position = new Vector3(horo_gwan.transform.position.x, horo_gwan.transform.position.y, -5f);
			break;

		case UserCastle.ham_gok_gwan :
			camera.transform.position = new Vector3(ham_gok_gwan.transform.position.x, ham_gok_gwan.transform.position.y, -5f);
			break;

		case UserCastle.ho_gwan :
			camera.transform.position = new Vector3(ho_gwan.transform.position.x, ho_gwan.transform.position.y, -5f);
			break;

		}

	}



	public void arrowPivot() {
		
		switch(uc) {
			
		case UserCastle.an_zeong :
			arrow.transform.position = new Vector3(an_zeong.transform.position.x + 0.6f, an_zeong.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.bok_yang :
			arrow.transform.position = new Vector3(bok_yang.transform.position.x  + 0.6f, bok_yang.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.book_hae :
			arrow.transform.position = new Vector3(book_hae.transform.position.x + 0.6f, book_hae.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.book_pyeong :
			arrow.transform.position = new Vector3(book_pyeong.transform.position.x + 0.6f, book_pyeong.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.cheon_soo :
			arrow.transform.position = new Vector3(cheon_soo.transform.position.x + 0.6f, cheon_soo.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.gang_ha :
			arrow.transform.position = new Vector3(gang_ha.transform.position.x + 0.6f, gang_ha.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.gang_rng :
			arrow.transform.position = new Vector3(gang_rng.transform.position.x + 0.6f, gang_rng.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.gang_zoo :
			arrow.transform.position = new Vector3(gang_zoo.transform.position.x + 0.6f, gang_zoo.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.geon_an :
			arrow.transform.position = new Vector3(geon_an.transform.position.x + 0.6f, geon_an.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.geon_nyeong :
			arrow.transform.position = new Vector3(geon_nyeong.transform.position.x + 0.6f, geon_nyeong.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.geon_up :
			arrow.transform.position = new Vector3(geon_up.transform.position.x + 0.6f, geon_up.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.gye :
			arrow.transform.position = new Vector3(gye.transform.position.x + 0.6f, gye.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.gye_yang :
			arrow.transform.position = new Vector3(gye_yang.transform.position.x + 0.6f, gye_yang.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.gyo_zi :
			arrow.transform.position = new Vector3(gyo_zi.transform.position.x + 0.6f, gyo_zi.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.ha_bi :
			arrow.transform.position = new Vector3(ha_bi.transform.position.x + 0.6f, ha_bi.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.ha_nae :
			arrow.transform.position = new Vector3(ha_nae.transform.position.x + 0.6f, ha_nae.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.han_zoong :
			arrow.transform.position = new Vector3(han_zoong.transform.position.x + 0.6f, han_zoong.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.heo_chang :
			arrow.transform.position = new Vector3(heo_chang.transform.position.x + 0.6f, heo_chang.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.hoi_gye :
			arrow.transform.position = new Vector3(hoi_gye.transform.position.x + 0.6f, hoi_gye.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.moo_do :
			arrow.transform.position = new Vector3(moo_do.transform.position.x + 0.6f, moo_do.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.moo_rng :
			arrow.transform.position = new Vector3(moo_rng.transform.position.x + 0.6f, moo_rng.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.moo_wi :
			arrow.transform.position = new Vector3(moo_wi.transform.position.x + 0.6f, moo_wi.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.nak_yang :
			arrow.transform.position = new Vector3(nak_yang.transform.position.x + 0.6f, nak_yang.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.nam_hae :
			arrow.transform.position = new Vector3(nam_hae.transform.position.x + 0.6f, nam_hae.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.nam_pi :
			arrow.transform.position = new Vector3(nam_pi.transform.position.x + 0.6f, nam_pi.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.o :
			arrow.transform.position = new Vector3(o.transform.position.x + 0.6f, o.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.oon_nam :
			arrow.transform.position = new Vector3(oon_nam.transform.position.x + 0.6f, oon_nam.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.pyeong_won :
			arrow.transform.position = new Vector3(pyeong_won.transform.position.x + 0.6f, pyeong_won.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.sang_dang :
			arrow.transform.position = new Vector3(sang_dang.transform.position.x + 0.6f, sang_dang.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.sang_yong :
			arrow.transform.position = new Vector3(sang_yong.transform.position.x + 0.6f, sang_yong.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.seo_pyeong :
			arrow.transform.position = new Vector3(seo_pyeong.transform.position.x + 0.6f, seo_pyeong.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.seong_do :
			arrow.transform.position = new Vector3(seong_do.transform.position.x + 0.6f, seong_do.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.si_sang :
			arrow.transform.position = new Vector3(si_sang.transform.position.x + 0.6f, si_sang.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.sin_ya :
			arrow.transform.position = new Vector3(sin_ya.transform.position.x + 0.6f, sin_ya.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.so_pae :
			arrow.transform.position = new Vector3(so_pae.transform.position.x + 0.6f, so_pae.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.soo_choon :
			arrow.transform.position = new Vector3(soo_choon.transform.position.x + 0.6f, soo_choon.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.up :
			arrow.transform.position = new Vector3(up.transform.position.x + 0.6f, up.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.wan :
			arrow.transform.position = new Vector3(wan.transform.position.x + 0.6f, wan.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.yang_pyeong :
			arrow.transform.position = new Vector3(yang_pyeong.transform.position.x + 0.6f, yang_pyeong.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.yang_yang :
			arrow.transform.position = new Vector3(yang_yang.transform.position.x + 0.6f, yang_yang.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.yeo_gang :
			arrow.transform.position = new Vector3(yeo_gang.transform.position.x + 0.6f, yeo_gang.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.yeo_nam :
			arrow.transform.position = new Vector3(yeo_nam.transform.position.x + 0.6f, yeo_nam.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.yeong_an :
			arrow.transform.position = new Vector3(yeong_an.transform.position.x + 0.6f, yeong_an.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.yeong_chang :
			arrow.transform.position = new Vector3(yeong_chang.transform.position.x + 0.6f, yeong_chang.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.yeong_rng :
			arrow.transform.position = new Vector3(yeong_rng.transform.position.x + 0.6f, yeong_rng.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.za_dong :
			arrow.transform.position = new Vector3(za_dong.transform.position.x + 0.6f, za_dong.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.zang_an :
			arrow.transform.position = new Vector3(zang_an.transform.position.x + 0.6f, zang_an.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.zang_sa :
			arrow.transform.position = new Vector3(zang_sa.transform.position.x + 0.6f, zang_sa.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.zin_ru :
			arrow.transform.position = new Vector3(zin_ru.transform.position.x + 0.6f, zin_ru.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.myeon_zook_gwan :
			arrow.transform.position = new Vector3(myeon_zook_gwan.transform.position.x, myeon_zook_gwan.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.geom_gak :
			arrow.transform.position = new Vector3(geom_gak.transform.position.x, geom_gak.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.yang_pyeong_gwan :
			arrow.transform.position = new Vector3(yang_pyeong_gwan.transform.position.x, yang_pyeong_gwan.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.moo_gwan :
			arrow.transform.position = new Vector3(moo_gwan.transform.position.x, moo_gwan.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.dong_gwan :
			arrow.transform.position = new Vector3(dong_gwan.transform.position.x, dong_gwan.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.horo_gwan :
			arrow.transform.position = new Vector3(horo_gwan.transform.position.x, horo_gwan.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.ham_gok_gwan :
			arrow.transform.position = new Vector3(ham_gok_gwan.transform.position.x, ham_gok_gwan.transform.position.y + 0.4f, -3f);
			break;
			
		case UserCastle.ho_gwan :
			arrow.transform.position = new Vector3(ho_gwan.transform.position.x, ho_gwan.transform.position.y + 0.4f, -3f);
			break;
			
		}
		
	}
	//-------------------------------------------------------------------------------
	string set_moo_wi = "'" + "무위" + "'";
	string set_seo_pyeong = "'" + "서평" + "'";
	string set_cheon_soo = "'" + "천수" + "'";
	string set_moo_do = "'" + "무도" + "'";
	string set_an_zeong = "'" + "안정" + "'";
	string set_han_zoong = "'" + "한중" + "'";
	string set_zang_an = "'" + "장안" + "'";
	string set_za_dong = "'" + "자동" + "'";
	string set_seong_do = "'" + "성도" + "'";
	string set_gang_zoo = "'" + "강주" + "'";
	string set_geon_nyeong = "'" + "건녕" + "'";
	string set_oon_nam = "'" + "운남" + "'";
	string set_yeong_chang = "'" + "영창" + "'";
	string set_gyo_zi = "'" + "교지" + "'";
	string set_nam_hae = "'" + "남해" + "'";
	string set_gye_yang = "'" + "계양" + "'";
	string set_yeong_rng = "'" + "영릉" + "'";
	string set_zang_sa = "'" + "장사" + "'";
	string set_moo_rng = "'" + "무릉" + "'";
	string set_gang_rng = "'" + "강릉" + "'";
	string set_gang_ha = "'" + "강하" + "'";
	string set_yang_yang = "'" + "양양" + "'";
	string set_sin_ya = "'" + "신야" + "'";
	string set_wan = "'" + "완" + "'";
	string set_sang_yong = "'" + "상용" + "'";
	string set_yeong_an = "'" + "영안" + "'";
	string set_nak_yang = "'" + "낙양" + "'";
	string set_yeo_nam = "'" + "여남" + "'";
	string set_heo_chang = "'" + "허창" + "'";
	string set_zin_ru = "'" + "진류" + "'";
	string set_bok_yang = "'" + "복양" + "'";
	string set_ha_nae = "'" + "하내" + "'";
	string set_sang_dang = "'" + "상당" + "'";
	string set_zin_yang = "'" + "진양" + "'";
	string set_up = "'" + "업" + "'";
	string set_pyeong_won = "'" + "평원" + "'";
	string set_nam_pi = "'" + "남피" + "'";
	string set_gye = "'" + "계" + "'";
	string set_book_pyeong = "'" + "북평" + "'";
	string set_yang_pyeong = "'" + "양평" + "'";
	string set_book_hae = "'" + "북해" + "'";
	string set_ha_bi = "'" + "하비" + "'";
	string set_so_pae = "'" + "소패" + "'";
	string set_soo_choon = "'" + "수춘" + "'";
	string set_yeo_gang = "'" + "여강" + "'";
	string set_geon_up = "'" + "건업" + "'";
	string set_o = "'" + "오" + "'";
	string set_hoi_gye = "'" + "회계" + "'";
	string set_geon_an = "'" + "건안" + "'";
	string set_si_sang = "'" + "시상" + "'";
	string set_myeon_zook_gwan = "'" + "면죽관" + "'";
	string set_geom_gak = "'" + "검각" + "'";
	string set_yang_pyeong_gwan = "'" + "양평관" + "'";
	string set_moo_gwan = "'" + "무관" + "'";
	string set_dong_gwan = "'" + "동관" + "'";
	string set_horo_gwan = "'" + "호로관" + "'";
	string set_ham_gok_gwan = "'" + "함곡관" + "'";
	string set_ho_gwan = "'" + "호관" + "'";

	// 깃발 변경 위한 애니메이터
	public Animator ki_moo_wi;
	public Animator ki_seo_pyeong;
	public Animator ki_cheon_soo;
	public Animator ki_moo_do;
	public Animator ki_an_zeong;
	public Animator ki_han_zoong;
	public Animator ki_zang_an;
	public Animator ki_za_dong;
	public Animator ki_seong_do;
	public Animator ki_gang_zoo;
	public Animator ki_geon_nyeong;
	public Animator ki_oon_nam;
	public Animator ki_yeong_chang;
	public Animator ki_gyo_zi;
	public Animator ki_nam_hae;
	public Animator ki_gye_yang;
	public Animator ki_yeong_rng;
	public Animator ki_zang_sa;
	public Animator ki_moo_rng;
	public Animator ki_gang_rng;
	public Animator ki_gang_ha;
	public Animator ki_yang_yang;
	public Animator ki_sin_ya;
	public Animator ki_wan;
	public Animator ki_sang_yong;
	public Animator ki_yeong_an;
	public Animator ki_nak_yang;
	public Animator ki_yeo_nam;
	public Animator ki_heo_chang;
	public Animator ki_zin_ru;
	public Animator ki_bok_yang;
	public Animator ki_ha_nae;
	public Animator ki_sang_dang;
	public Animator ki_zin_yang;
	public Animator ki_up;
	public Animator ki_pyeong_won;
	public Animator ki_nam_pi;
	public Animator ki_gye;
	public Animator ki_book_pyeong;
	public Animator ki_yang_pyeong;
	public Animator ki_book_hae;
	public Animator ki_ha_bi;
	public Animator ki_so_pae;
	public Animator ki_soo_choon;
	public Animator ki_yeo_gang;
	public Animator ki_geon_up;
	public Animator ki_o;
	public Animator ki_hoi_gye;
	public Animator ki_geon_an;
	public Animator ki_si_sang;
	public Animator ki_myeon_zook_gwan;
	public Animator ki_geom_gak;
	public Animator ki_yang_pyeong_gwan;
	public Animator ki_moo_gwan;
	public Animator ki_dong_gwan;
	public Animator ki_horo_gwan;
	public Animator ki_ham_gok_gwan;
	public Animator ki_ho_gwan;

	int int_moo_wi;
	int int_seo_pyeong;
	int int_cheon_soo;
	int int_moo_do;
	int int_an_zeong;
	int int_han_zoong;
	int int_zang_an;
	int int_za_dong;
	int int_seong_do;
	int int_gang_zoo;
	int int_geon_nyeong;
	int int_oon_nam;
	int int_yeong_chang;
	int int_gyo_zi;
	int int_nam_hae;
	int int_gye_yang;
	int int_yeong_rng;
	int int_zang_sa;
	int int_moo_rng;
	int int_gang_rng;
	int int_gang_ha;
	int int_yang_yang;
	int int_sin_ya;
	int int_wan;
	int int_sang_yong;
	int int_yeong_an;
	int int_nak_yang;
	int int_yeo_nam;
	int int_heo_chang;
	int int_zin_ru;
	int int_bok_yang;
	int int_ha_nae;
	int int_sang_dang;
	int int_zin_yang;
	int int_up;
	int int_pyeong_won;
	int int_nam_pi;
	int int_gye;
	int int_book_pyeong;
	int int_yang_pyeong;
	int int_book_hae;
	int int_ha_bi;
	int int_so_pae;
	int int_soo_choon;
	int int_yeo_gang;
	int int_geon_up;
	int int_o;
	int int_hoi_gye;
	int int_geon_an;
	int int_si_sang;
	int int_myeon_zook_gwan;
	int int_geom_gak;
	int int_yang_pyeong_gwan;
	int int_moo_gwan;
	int int_dong_gwan;
	int int_horo_gwan;
	int int_ham_gok_gwan;
	int int_ho_gwan;


	public void set_Ki() {

		print (set_o);

		int_moo_wi = db.intDBReader("force_num", "Castle_1", "castle_name", set_moo_wi);
		int_seo_pyeong = db.intDBReader("force_num", "Castle_1", "castle_name", set_seo_pyeong);
		int_cheon_soo = db.intDBReader("force_num", "Castle_1", "castle_name", set_cheon_soo);
		int_moo_do = db.intDBReader("force_num", "Castle_1", "castle_name", set_moo_do);
		int_an_zeong = db.intDBReader("force_num", "Castle_1", "castle_name", set_an_zeong);
		int_han_zoong = db.intDBReader("force_num", "Castle_1", "castle_name", set_han_zoong);
		int_zang_an = db.intDBReader("force_num", "Castle_1", "castle_name", set_zang_an);
		int_za_dong = db.intDBReader("force_num", "Castle_1", "castle_name", set_za_dong);
		int_seong_do = db.intDBReader("force_num", "Castle_1", "castle_name", set_seong_do);
		int_gang_zoo = db.intDBReader("force_num", "Castle_1", "castle_name", set_gang_zoo);
		int_geon_nyeong = db.intDBReader("force_num", "Castle_1", "castle_name", set_geon_nyeong);
		int_oon_nam = db.intDBReader("force_num", "Castle_1", "castle_name", set_oon_nam);
		int_yeong_chang = db.intDBReader("force_num", "Castle_1", "castle_name", set_yeong_chang);
		int_gyo_zi = db.intDBReader("force_num", "Castle_1", "castle_name", set_gyo_zi);
		int_nam_hae = db.intDBReader("force_num", "Castle_1", "castle_name", set_nam_hae);
		int_gye_yang = db.intDBReader("force_num", "Castle_1", "castle_name", set_gye_yang);
		int_yeong_rng = db.intDBReader("force_num", "Castle_1", "castle_name", set_yeong_rng);
		int_zang_sa = db.intDBReader("force_num", "Castle_1", "castle_name", set_zang_sa);
		int_moo_rng = db.intDBReader("force_num", "Castle_1", "castle_name", set_moo_rng);
		int_gang_rng = db.intDBReader("force_num", "Castle_1", "castle_name", set_gang_rng);
		int_gang_ha = db.intDBReader("force_num", "Castle_1", "castle_name", set_gang_ha);
		int_yang_yang = db.intDBReader("force_num", "Castle_1", "castle_name", set_yang_yang);
		int_sin_ya = db.intDBReader("force_num", "Castle_1", "castle_name", set_sin_ya);
		int_wan = db.intDBReader("force_num", "Castle_1", "castle_name", set_wan);
		int_sang_yong = db.intDBReader("force_num", "Castle_1", "castle_name", set_sang_yong);
		int_yeong_an = db.intDBReader("force_num", "Castle_1", "castle_name", set_yeong_an);
		int_nak_yang = db.intDBReader("force_num", "Castle_1", "castle_name", set_nak_yang);
		int_yeo_nam = db.intDBReader("force_num", "Castle_1", "castle_name", set_yeo_nam);
		int_heo_chang = db.intDBReader("force_num", "Castle_1", "castle_name", set_heo_chang);
		int_zin_ru = db.intDBReader("force_num", "Castle_1", "castle_name", set_zin_ru);
		int_bok_yang = db.intDBReader("force_num", "Castle_1", "castle_name", set_bok_yang);
		int_ha_nae = db.intDBReader("force_num", "Castle_1", "castle_name", set_ha_nae);
		int_sang_dang = db.intDBReader("force_num", "Castle_1", "castle_name", set_sang_dang);
		int_zin_yang = db.intDBReader("force_num", "Castle_1", "castle_name", set_zin_yang);
		int_up = db.intDBReader("force_num", "Castle_1", "castle_name", set_up);
		int_pyeong_won = db.intDBReader("force_num", "Castle_1", "castle_name", set_pyeong_won);
		int_nam_pi = db.intDBReader("force_num", "Castle_1", "castle_name", set_nam_pi);
		int_gye = db.intDBReader("force_num", "Castle_1", "castle_name", set_gye);
		int_book_pyeong = db.intDBReader("force_num", "Castle_1", "castle_name", set_book_pyeong);
		int_yang_pyeong = db.intDBReader("force_num", "Castle_1", "castle_name", set_yang_pyeong);
		int_book_hae = db.intDBReader("force_num", "Castle_1", "castle_name", set_book_hae);
		int_ha_bi = db.intDBReader("force_num", "Castle_1", "castle_name", set_ha_bi);
		int_so_pae = db.intDBReader("force_num", "Castle_1", "castle_name", set_so_pae);
		int_soo_choon = db.intDBReader("force_num", "Castle_1", "castle_name", set_soo_choon);
		int_yeo_gang = db.intDBReader("force_num", "Castle_1", "castle_name", set_yeo_gang);
		int_geon_up = db.intDBReader("force_num", "Castle_1", "castle_name", set_geon_up);
		int_o = db.intDBReader("force_num", "Castle_1", "castle_name", set_o);
		int_hoi_gye = db.intDBReader("force_num", "Castle_1", "castle_name", set_hoi_gye);
		int_geon_an = db.intDBReader("force_num", "Castle_1", "castle_name", set_geon_an);
		int_si_sang = db.intDBReader("force_num", "Castle_1", "castle_name", set_si_sang);
		int_myeon_zook_gwan = db.intDBReader("force_num", "Castle_1", "castle_name", set_myeon_zook_gwan);
		int_geom_gak = db.intDBReader("force_num", "Castle_1", "castle_name", set_geom_gak);
		int_yang_pyeong_gwan = db.intDBReader("force_num", "Castle_1", "castle_name", set_yang_pyeong_gwan);
		int_moo_gwan = db.intDBReader("force_num", "Castle_1", "castle_name", set_moo_gwan);
		int_dong_gwan = db.intDBReader("force_num", "Castle_1", "castle_name", set_dong_gwan);
		int_horo_gwan = db.intDBReader("force_num", "Castle_1", "castle_name", set_horo_gwan);
		int_ham_gok_gwan = db.intDBReader("force_num", "Castle_1", "castle_name", set_ham_gok_gwan);
		int_ho_gwan = db.intDBReader("force_num", "Castle_1", "castle_name", set_ho_gwan);

		set_Force_Ki (int_moo_wi, ki_moo_wi);
		set_Force_Ki (int_an_zeong, ki_an_zeong);
		set_Force_Ki (int_bok_yang, ki_bok_yang);
		set_Force_Ki (int_book_hae, ki_book_hae);
		set_Force_Ki (int_book_pyeong, ki_book_pyeong);
		set_Force_Ki (int_cheon_soo, ki_cheon_soo);
		set_Force_Ki (int_dong_gwan, ki_dong_gwan);
		set_Force_Ki (int_gang_ha, ki_gang_ha);
		set_Force_Ki (int_gang_rng, ki_gang_rng);
		set_Force_Ki (int_gang_zoo, ki_gang_zoo);
		set_Force_Ki (int_geom_gak, ki_geom_gak);
		set_Force_Ki (int_geon_an, ki_geon_an);
		set_Force_Ki (int_geon_nyeong, ki_geon_nyeong);
		set_Force_Ki (int_geon_up, ki_geon_up);
		set_Force_Ki (int_gye, ki_gye);
		set_Force_Ki (int_gye_yang, ki_gye_yang);
		set_Force_Ki (int_gyo_zi, ki_gyo_zi);
		set_Force_Ki (int_ha_bi, ki_ha_bi);
		set_Force_Ki (int_ha_nae, ki_ha_nae);
		set_Force_Ki (int_ham_gok_gwan, ki_ham_gok_gwan);
		set_Force_Ki (int_han_zoong, ki_han_zoong);
		set_Force_Ki (int_heo_chang, ki_heo_chang);
		set_Force_Ki (int_hoi_gye, ki_hoi_gye);
		set_Force_Ki (int_horo_gwan, ki_horo_gwan);
		set_Force_Ki (int_ho_gwan, ki_ho_gwan);
		set_Force_Ki (int_moo_do, ki_moo_do);
		set_Force_Ki (int_moo_gwan, ki_moo_gwan);
		set_Force_Ki (int_moo_rng, ki_moo_rng);
		set_Force_Ki (int_myeon_zook_gwan, ki_myeon_zook_gwan);
		set_Force_Ki (int_nak_yang, ki_nak_yang);
		set_Force_Ki (int_nam_hae, ki_nam_hae);
		set_Force_Ki (int_nam_pi, ki_nam_pi);
		set_Force_Ki (int_o, ki_o);
		set_Force_Ki (int_oon_nam, ki_oon_nam);
		set_Force_Ki (int_pyeong_won, ki_pyeong_won);
		set_Force_Ki (int_sang_dang, ki_sang_dang);
		set_Force_Ki (int_sang_yong, ki_sang_yong);
		set_Force_Ki (int_seong_do, ki_seong_do);
		set_Force_Ki (int_seo_pyeong, ki_seo_pyeong);
		set_Force_Ki (int_sin_ya, ki_sin_ya);
		set_Force_Ki (int_si_sang, ki_si_sang);
		set_Force_Ki (int_soo_choon, ki_soo_choon);
		set_Force_Ki (int_so_pae, ki_so_pae);
		set_Force_Ki (int_up, ki_up);
		set_Force_Ki (int_wan, ki_wan);
		set_Force_Ki (int_yang_pyeong, ki_yang_pyeong);
		set_Force_Ki (int_yang_pyeong_gwan, ki_yang_pyeong_gwan);
		set_Force_Ki (int_yang_yang, ki_yang_yang);
		set_Force_Ki (int_yeong_an, ki_yeong_an);
		set_Force_Ki (int_yeong_chang, ki_yeong_chang);
		set_Force_Ki (int_yeong_rng, ki_yeong_rng);
		set_Force_Ki (int_yeo_gang, ki_yeo_gang);
		set_Force_Ki (int_yeo_nam, ki_yeo_nam);
		set_Force_Ki (int_zang_an, ki_zang_an);
		set_Force_Ki (int_zang_sa, ki_zang_sa);
		set_Force_Ki (int_za_dong, ki_za_dong);
		set_Force_Ki (int_zin_ru, ki_zin_ru);
		set_Force_Ki (int_zin_yang, ki_zin_yang);

	}


	public void set_Force_Ki (int c_name, Animator anim) {

		if (c_name == 0) {
			anim.GetComponent<SpriteRenderer>().enabled = false;
		} else if (c_name == 1) {
			anim.GetComponent<SpriteRenderer>().enabled = true;
			anim.SetTrigger("ha_zin");
		} else if (c_name == 2) {
			anim.GetComponent<SpriteRenderer>().enabled = true;
			anim.SetTrigger("zang_gak");
		}

	}

	//-----------------------------------------------------------------------

	string forceNum;
	public GameObject menu1;
	public GameObject menu2;
	public GameObject menu3;

	public void menuBarChoice(string cName) {

		db.OpenDB (dbName);
		string eq_moo_wi = int_moo_wi.ToString();
		string eq_seo_pyeong = int_seo_pyeong.ToString();
		string eq_cheon_soo = int_cheon_soo.ToString();
		string eq_moo_do = int_moo_do.ToString();
		string eq_an_zeong = int_an_zeong.ToString();
		string eq_han_zoong = int_han_zoong.ToString();
		string eq_zang_an = int_zang_an.ToString();
		string eq_za_dong = int_za_dong.ToString();
		string eq_seong_do = int_seong_do.ToString();
		string eq_gang_zoo = int_gang_zoo.ToString();
		string eq_geon_nyeong = int_geon_nyeong.ToString();
		string eq_oon_nam = int_oon_nam.ToString();
		string eq_yeong_chang = int_yeong_chang.ToString();
		string eq_gyo_zi = int_gyo_zi.ToString();
		string eq_nam_hae = int_nam_hae.ToString();
		string eq_gye_yang = int_gye_yang.ToString();
		string eq_yeong_rng = int_yeong_rng.ToString();
		string eq_zang_sa = int_zang_sa.ToString();
		string eq_moo_rng = int_moo_rng.ToString();
		string eq_gang_rng = int_gang_rng.ToString();
		string eq_gang_ha = int_gang_ha.ToString();
		string eq_yang_yang = int_yang_yang.ToString();
		string eq_sin_ya = int_sin_ya.ToString();
		string eq_wan = int_wan.ToString();
		string eq_sang_yong = int_sang_yong.ToString();
		string eq_yeong_an = int_yeong_an.ToString();
		string eq_nak_yang = int_nak_yang.ToString();
		string eq_yeo_nam = int_yeo_nam.ToString();
		string eq_heo_chang = int_heo_chang.ToString();
		string eq_zin_ru = int_zin_ru.ToString();
		string eq_bok_yang = int_bok_yang.ToString();
		string eq_ha_nae = int_ha_nae.ToString();
		string eq_sang_dang = int_sang_dang.ToString();
		string eq_zin_yang = int_zin_yang.ToString();
		string eq_up = int_up.ToString();
		string eq_pyeong_won = int_pyeong_won.ToString();
		string eq_nam_pi = int_nam_pi.ToString();
		string eq_gye = int_gye.ToString();
		string eq_book_pyeong = int_book_pyeong.ToString();
		string eq_yang_pyeong = int_yang_pyeong.ToString();
		string eq_book_hae = int_book_hae.ToString();
		string eq_ha_bi = int_ha_bi.ToString();
		string eq_so_pae = int_so_pae.ToString();
		string eq_soo_choon = int_soo_choon.ToString();
		string eq_yeo_gang = int_yeo_gang.ToString();
		string eq_geon_up = int_geon_up.ToString();
		string eq_o = int_o.ToString();
		string eq_hoi_gye = int_hoi_gye.ToString();
		string eq_geon_an = int_geon_an.ToString();
		string eq_si_sang = int_si_sang.ToString();
		string eq_myeon_zook_gwan = int_myeon_zook_gwan.ToString();
		string eq_geom_gak = int_geom_gak.ToString();
		string eq_yang_pyeong_gwan = int_yang_pyeong_gwan.ToString();
		string eq_moo_gwan = int_moo_gwan.ToString();
		string eq_dong_gwan = int_dong_gwan.ToString();
		string eq_horo_gwan = int_horo_gwan.ToString();
		string eq_ham_gok_gwan = int_ham_gok_gwan.ToString();
		string eq_ho_gwan = int_ho_gwan.ToString();
		forceNum = db.stringDBReader("ch1_force", "UnitInfo", "unit_name", curName);
		db.CloseDB();

		switch(uc) {
			
		case UserCastle.an_zeong :
			// 자기성에서 자기성 클릭(아군성)
			if (cName.Equals("an_zeong") && forceNum.Equals(eq_an_zeong)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
			// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("zang_an") && forceNum.Equals(eq_zang_an)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("cheon_soo") && forceNum.Equals(eq_cheon_soo)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			// 자기성에서 공격가능한 성클릭(적군성)
			} else if (cName.Equals("zang_an") && !forceNum.Equals(eq_zang_an)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("cheon_soo") && !forceNum.Equals(eq_cheon_soo)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
			
		case UserCastle.bok_yang :
			// 자기성에서 자기성 클릭(아군성)
			if (cName.Equals("bok_yang") && forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("so_pae") && forceNum.Equals(eq_so_pae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_ru") && forceNum.Equals(eq_zin_ru)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && forceNum.Equals(eq_heo_chang)) {
					menu1.SetActive(false);
					menu2.SetActive(true);
					menu3.SetActive(false);
			} else if (cName.Equals("ho_ro_gwan") && forceNum.Equals(eq_horo_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("book_hae") && forceNum.Equals(eq_book_hae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("pyeong_won") && forceNum.Equals(eq_pyeong_won)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && forceNum.Equals(eq_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("so_pae") && !forceNum.Equals(eq_so_pae)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_ru") && !forceNum.Equals(eq_zin_ru)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && !forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_ro_gwan") && !forceNum.Equals(eq_horo_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("book_hae") && !forceNum.Equals(eq_book_hae)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("pyeong_won") && !forceNum.Equals(eq_pyeong_won)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && !forceNum.Equals(eq_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------	
		case UserCastle.book_hae :
			if (cName.Equals("book_hae") && forceNum.Equals(eq_book_hae)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
			} else if (cName.Equals("ha_bi") && forceNum.Equals(eq_ha_bi)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("pyeong_won") && forceNum.Equals(eq_pyeong_won)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_bi") && !forceNum.Equals(eq_ha_bi)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && !forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("pyeong_won") && !forceNum.Equals(eq_pyeong_won)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------	
		case UserCastle.book_pyeong :
			if (cName.Equals("book_pyeong") && forceNum.Equals(eq_book_pyeong)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
			} else if (cName.Equals("yang_pyeong") && forceNum.Equals(eq_yang_pyeong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gye") && forceNum.Equals(eq_gye)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yang_pyeong") && !forceNum.Equals(eq_yang_pyeong)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gye") && !forceNum.Equals(eq_gye)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------	
		case UserCastle.cheon_soo :
			if (cName.Equals("cheon_soo") && forceNum.Equals(eq_cheon_soo)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("moo_wi") && forceNum.Equals(eq_moo_wi)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("seo_pyeong") && forceNum.Equals(eq_seo_pyeong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("an_zeong") && forceNum.Equals(eq_an_zeong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_do") && forceNum.Equals(eq_moo_do)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("myeon_zook_gwan") && forceNum.Equals(eq_myeon_zook_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_wi") && !forceNum.Equals(eq_moo_wi)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("seo_pyeong") && !forceNum.Equals(eq_seo_pyeong)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("an_zeong") && !forceNum.Equals(eq_an_zeong)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_do") && !forceNum.Equals(eq_moo_do)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("myeon_zook_gwan") && !forceNum.Equals(eq_myeon_zook_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.gang_ha :
			if (cName.Equals("gang_ha") && forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("gang_rng") && forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("sin_ya") && forceNum.Equals(eq_sin_ya)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_nam") && forceNum.Equals(eq_yeo_nam)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_rng") && !forceNum.Equals(eq_gang_rng)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("sin_ya") && !forceNum.Equals(eq_sin_ya)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_nam") && !forceNum.Equals(eq_yeo_nam)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && !forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && !forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.gang_rng :
			if (cName.Equals("gang_rng") && forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("yeong_an") && forceNum.Equals(eq_yeong_an)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_rng") && forceNum.Equals(eq_moo_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yang_yang") && forceNum.Equals(eq_yang_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_ha") && forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			}else if (cName.Equals("yeong_an") && !forceNum.Equals(eq_yeong_an)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_rng") && !forceNum.Equals(eq_moo_rng)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && !forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yang_yang") && !forceNum.Equals(eq_yang_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_ha") && !forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && !forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.gang_zoo :
			if (cName.Equals("gang_zoo") && forceNum.Equals(eq_gang_zoo)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("za_dong") && forceNum.Equals(eq_za_dong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("seong_do") && forceNum.Equals(eq_seong_do)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_nyeong") && forceNum.Equals(eq_geon_nyeong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeong_an") && forceNum.Equals(eq_yeong_an)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("za_dong") && !forceNum.Equals(eq_za_dong)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("seong_do") && !forceNum.Equals(eq_seong_do)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_nyeong") && !forceNum.Equals(eq_geon_nyeong)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yeong_an") && !forceNum.Equals(eq_yeong_an)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------	
		case UserCastle.geon_an :
			if (cName.Equals("geon_an") && forceNum.Equals(eq_geon_an)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("hoi_gye") && forceNum.Equals(eq_hoi_gye)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_hae") && forceNum.Equals(eq_nam_hae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("hoi_gye") && !forceNum.Equals(eq_hoi_gye)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_hae") && !forceNum.Equals(eq_nam_hae)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.geon_nyeong :
			if (cName.Equals("geon_nyeong") && forceNum.Equals(eq_geon_nyeong)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("oon_nam") && forceNum.Equals(eq_oon_nam)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_zoo") && forceNum.Equals(eq_gang_zoo)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("seong_do") && forceNum.Equals(eq_seong_do)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("oon_nam") && !forceNum.Equals(eq_oon_nam)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_zoo") && !forceNum.Equals(eq_gang_zoo)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("seong_do") && !forceNum.Equals(eq_seong_do)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.geon_up :
			if (cName.Equals("geon_up") && forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("o") && forceNum.Equals(eq_o)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gye_yang") && forceNum.Equals(eq_gye_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_hae") && forceNum.Equals(eq_nam_hae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_gang") && forceNum.Equals(eq_yeo_gang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_bi") && forceNum.Equals(eq_ha_bi)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("o") && !forceNum.Equals(eq_o)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && !forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && !forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gye_yang") && !forceNum.Equals(eq_gye_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_hae") && !forceNum.Equals(eq_nam_hae)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_gang") && !forceNum.Equals(eq_yeo_gang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && !forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_bi") && !forceNum.Equals(eq_ha_bi)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.gye :
			if (cName.Equals("gye") && forceNum.Equals(eq_gye)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("zin_yang") && forceNum.Equals(eq_zin_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_pi") && forceNum.Equals(eq_nam_pi)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("book_pyeong") && forceNum.Equals(eq_book_pyeong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_yang") && !forceNum.Equals(eq_zin_yang)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_pi") && !forceNum.Equals(eq_nam_pi)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("book_pyeong") && !forceNum.Equals(eq_book_pyeong)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.gye_yang :
			if (cName.Equals("gye_yang") && forceNum.Equals(eq_gye_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("nam_hae") && forceNum.Equals(eq_nam_hae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeong_rng") && forceNum.Equals(eq_yeong_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_hae") && !forceNum.Equals(eq_nam_hae)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yeong_rng") && !forceNum.Equals(eq_yeong_rng)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && !forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && !forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && !forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.gyo_zi :
			if (cName.Equals("gyo_zi") && forceNum.Equals(eq_gyo_zi)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("nam_hae") && forceNum.Equals(eq_nam_hae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("oon_nam") && forceNum.Equals(eq_oon_nam)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_hae") && !forceNum.Equals(eq_nam_hae)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("oon_nam") && !forceNum.Equals(eq_oon_nam)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.ha_bi :
			if (cName.Equals("ha_bi") && forceNum.Equals(eq_ha_bi)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("o") && forceNum.Equals(eq_o)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("so_pae") && forceNum.Equals(eq_so_pae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("book_hae") && forceNum.Equals(eq_book_hae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("o") && !forceNum.Equals(eq_o)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && !forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && !forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("so_pae") && !forceNum.Equals(eq_so_pae)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("book_hae") && !forceNum.Equals(eq_book_hae)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.ha_nae :
			if (cName.Equals("ha_nae") && forceNum.Equals(eq_ha_nae)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("ham_gok_gwan") && forceNum.Equals(eq_ham_gok_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_gwan") && forceNum.Equals(eq_ho_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && forceNum.Equals(eq_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ham_gok_gwan") && !forceNum.Equals(eq_ham_gok_gwan)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_gwan") && !forceNum.Equals(eq_ho_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && !forceNum.Equals(eq_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.han_zoong :
			if (cName.Equals("han_zoong") && forceNum.Equals(eq_han_zoong)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("cheon_soo") && forceNum.Equals(eq_cheon_soo)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_do") && forceNum.Equals(eq_moo_do)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_an") && forceNum.Equals(eq_zang_an)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("sang_yong") && forceNum.Equals(eq_sang_yong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yang_pyeong_gwan") && forceNum.Equals(eq_yang_pyeong_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("cheon_soo") && !forceNum.Equals(eq_cheon_soo)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_do") && !forceNum.Equals(eq_moo_do)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_an") && !forceNum.Equals(eq_zang_an)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("sang_yong") && !forceNum.Equals(eq_sang_yong)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yang_pyeong_gwan") && !forceNum.Equals(eq_yang_pyeong_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.heo_chang :
			if (cName.Equals("heo_chang") && forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("wan") && forceNum.Equals(eq_wan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && forceNum.Equals(eq_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_ro_gwan") && forceNum.Equals(eq_horo_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_ru") && forceNum.Equals(eq_zin_ru)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_nam") && forceNum.Equals(eq_yeo_nam)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("wan") && !forceNum.Equals(eq_wan)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && !forceNum.Equals(eq_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_ro_gwan") && !forceNum.Equals(eq_horo_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_ru") && !forceNum.Equals(eq_zin_ru)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_nam") && !forceNum.Equals(eq_yeo_nam)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && !forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && !forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.hoi_gye :
			if (cName.Equals("hoi_gye") && forceNum.Equals(eq_hoi_gye)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("o") && forceNum.Equals(eq_o)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_an") && forceNum.Equals(eq_geon_an)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("o") && !forceNum.Equals(eq_o)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_an") && !forceNum.Equals(eq_geon_an)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.moo_do :
			if (cName.Equals("moo_do") && forceNum.Equals(eq_moo_do)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("cheon_soo") && forceNum.Equals(eq_cheon_soo)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("han_zoong") && forceNum.Equals(eq_han_zoong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_an") && forceNum.Equals(eq_zang_an)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("myeon_zook_gwan") && forceNum.Equals(eq_myeon_zook_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yang_pyeong_gwan") && forceNum.Equals(eq_yang_pyeong_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("cheon_soo") && !forceNum.Equals(eq_cheon_soo)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("han_zoong") && !forceNum.Equals(eq_han_zoong)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_an") && !forceNum.Equals(eq_zang_an)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("myeon_zook_gwan") && !forceNum.Equals(eq_myeon_zook_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yang_pyeong_gwan") && !forceNum.Equals(eq_yang_pyeong_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.moo_rng :
			if (cName.Equals("moo_rng") && forceNum.Equals(eq_moo_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("gang_rng") && forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeong_rng") && forceNum.Equals(eq_yeong_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_rng") && !forceNum.Equals(eq_gang_rng)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && !forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yeong_rng") && !forceNum.Equals(eq_yeong_rng)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.moo_wi :
			if (cName.Equals("moo_wi") && forceNum.Equals(eq_moo_wi)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("seo_pyeong") && forceNum.Equals(eq_seo_pyeong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("cheon_soo") && forceNum.Equals(eq_cheon_soo)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("seo_pyeong") && !forceNum.Equals(eq_seo_pyeong)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("cheon_soo") && !forceNum.Equals(eq_cheon_soo)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.nak_yang :
			if (cName.Equals("nak_yang") && forceNum.Equals(eq_nak_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("ham_gok_gwan") && forceNum.Equals(eq_ham_gok_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_ro_gwan") && forceNum.Equals(eq_horo_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ham_gok_gwan") && !forceNum.Equals(eq_ham_gok_gwan)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_ro_gwan") && !forceNum.Equals(eq_horo_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.nam_hae :
			if (cName.Equals("nam_hae") && forceNum.Equals(eq_nam_hae)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("gyo_zi") && forceNum.Equals(eq_gyo_zi)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gye_yang") && forceNum.Equals(eq_gye_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_an") && forceNum.Equals(eq_geon_an)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gyo_zi") && !forceNum.Equals(eq_gyo_zi)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gye_yang") && !forceNum.Equals(eq_gye_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_an") && !forceNum.Equals(eq_geon_an)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && !forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && !forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && !forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.nam_pi :
			if (cName.Equals("nam_pi") && forceNum.Equals(eq_nam_pi)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("pyeong_won") && forceNum.Equals(eq_pyeong_won)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && forceNum.Equals(eq_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gye") && forceNum.Equals(eq_gye)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("pyeong_won") && !forceNum.Equals(eq_pyeong_won)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && !forceNum.Equals(eq_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gye") && !forceNum.Equals(eq_gye)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.o :
			if (cName.Equals("o") && forceNum.Equals(eq_o)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("hoi_gye") && forceNum.Equals(eq_hoi_gye)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_bi") && forceNum.Equals(eq_ha_bi)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("hoi_gye") && !forceNum.Equals(eq_hoi_gye)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && !forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_bi") && !forceNum.Equals(eq_ha_bi)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && !forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.oon_nam :
			if (cName.Equals("oon_nam") && forceNum.Equals(eq_oon_nam)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("gyo_zi") && forceNum.Equals(eq_gyo_zi)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeong_chang") && forceNum.Equals(eq_yeong_chang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_nyeong") && forceNum.Equals(eq_geon_nyeong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("seong_do") && forceNum.Equals(eq_seong_do)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_zoo") && forceNum.Equals(eq_gang_zoo)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gyo_zi") && !forceNum.Equals(eq_gyo_zi)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yeong_chang") && !forceNum.Equals(eq_yeong_chang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_nyeong") && !forceNum.Equals(eq_geon_nyeong)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("seong_do") && !forceNum.Equals(eq_seong_do)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_zoo") && !forceNum.Equals(eq_gang_zoo)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.pyeong_won :
			if (cName.Equals("pyeong_won") && forceNum.Equals(eq_pyeong_won)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("up") && forceNum.Equals(eq_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_pi") && forceNum.Equals(eq_nam_pi)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("book_hae") && forceNum.Equals(eq_book_hae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && !forceNum.Equals(eq_up)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_pi") && !forceNum.Equals(eq_nam_pi)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("book_hae") && !forceNum.Equals(eq_book_hae)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && !forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.sang_dang :
			if (cName.Equals("sang_dang") && forceNum.Equals(eq_sang_dang)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("zin_yang") && forceNum.Equals(eq_zin_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_gwan") && forceNum.Equals(eq_ho_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_yang") && !forceNum.Equals(eq_zin_yang)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_gwan") && !forceNum.Equals(eq_ho_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.sang_yong :
			if (cName.Equals("sang_yong") && forceNum.Equals(eq_sang_yong)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("han_zoong") && forceNum.Equals(eq_han_zoong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yang_yang") && forceNum.Equals(eq_yang_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("han_zoong") && !forceNum.Equals(eq_han_zoong)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yang_yang") && !forceNum.Equals(eq_yang_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.seo_pyeong :
			if (cName.Equals("seo_pyeong") && forceNum.Equals(eq_seo_pyeong)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("moo_wi") && forceNum.Equals(eq_moo_wi)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("cheon_soo") && forceNum.Equals(eq_cheon_soo)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_wi") && !forceNum.Equals(eq_moo_wi)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("cheon_soo") && !forceNum.Equals(eq_cheon_soo)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.seong_do :
			if (cName.Equals("seong_do") && forceNum.Equals(eq_seong_do)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("oon_nam") && forceNum.Equals(eq_oon_nam)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_nyeong") && forceNum.Equals(eq_geon_nyeong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_zoo") && forceNum.Equals(eq_gang_zoo)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("myeon_zook_gwan") && forceNum.Equals(eq_myeon_zook_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("oon_nam") && !forceNum.Equals(eq_oon_nam)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_nyeong") && !forceNum.Equals(eq_geon_nyeong)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_zoo") && !forceNum.Equals(eq_gang_zoo)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("myeon_zook_gwan") && !forceNum.Equals(eq_myeon_zook_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.si_sang :
			if (cName.Equals("si_sang") && forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("zang_sa") && forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_rng") && forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_ha") && forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gye_yang") && forceNum.Equals(eq_gye_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_hae") && forceNum.Equals(eq_nam_hae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_gang") && forceNum.Equals(eq_yeo_gang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && !forceNum.Equals(eq_zang_sa)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_rng") && !forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_ha") && !forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gye_yang") && !forceNum.Equals(eq_gye_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_hae") && !forceNum.Equals(eq_nam_hae)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_gang") && !forceNum.Equals(eq_yeo_gang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && !forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && !forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.sin_ya :
			if (cName.Equals("sin_ya") && forceNum.Equals(eq_sin_ya)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("yang_yang") && forceNum.Equals(eq_yang_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("wan") && forceNum.Equals(eq_wan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_ha") && forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_nam") && forceNum.Equals(eq_yeo_nam)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yang_yang") && !forceNum.Equals(eq_yang_yang)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("wan") && !forceNum.Equals(eq_wan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_ha") && !forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_nam") && !forceNum.Equals(eq_yeo_nam)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.so_pae :
			if (cName.Equals("so_pae") && forceNum.Equals(eq_so_pae)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("soo_choon") && forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_bi") && forceNum.Equals(eq_ha_bi)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_ru") && forceNum.Equals(eq_zin_ru)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && !forceNum.Equals(eq_soo_choon)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_bi") && !forceNum.Equals(eq_ha_bi)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_ru") && !forceNum.Equals(eq_zin_ru)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && !forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.soo_choon :
			if (cName.Equals("soo_choon") && forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("yeo_gang") && forceNum.Equals(eq_yeo_gang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("o") && forceNum.Equals(eq_o)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_bi") && forceNum.Equals(eq_ha_bi)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("so_pae") && forceNum.Equals(eq_so_pae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_nam") && forceNum.Equals(eq_yeo_nam)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_gang") && !forceNum.Equals(eq_yeo_gang)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && !forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("o") && !forceNum.Equals(eq_o)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_bi") && !forceNum.Equals(eq_ha_bi)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("so_pae") && !forceNum.Equals(eq_so_pae)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_nam") && !forceNum.Equals(eq_yeo_nam)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && !forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.up :
			if (cName.Equals("up") && forceNum.Equals(eq_up)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("ha_nae") && forceNum.Equals(eq_ha_nae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_ru") && forceNum.Equals(eq_zin_ru)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_ro_gwan") && forceNum.Equals(eq_horo_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("pyeong_won") && forceNum.Equals(eq_pyeong_won)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_pi") && forceNum.Equals(eq_nam_pi)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_yang") && forceNum.Equals(eq_zin_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_nae") && !forceNum.Equals(eq_ha_nae)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && !forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_ru") && !forceNum.Equals(eq_zin_ru)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && !forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_ro_gwan") && !forceNum.Equals(eq_horo_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("pyeong_won") && !forceNum.Equals(eq_pyeong_won)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_pi") && !forceNum.Equals(eq_nam_pi)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_yang") && !forceNum.Equals(eq_zin_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.wan :
			if (cName.Equals("wan") && forceNum.Equals(eq_wan)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("sin_ya") && forceNum.Equals(eq_sin_ya)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_gwan") && forceNum.Equals(eq_moo_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("sin_ya") && !forceNum.Equals(eq_sin_ya)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && !forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_gwan") && !forceNum.Equals(eq_moo_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.yang_pyeong :
			if (cName.Equals("yang_pyeong") && forceNum.Equals(eq_yang_pyeong)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("book_pyeong") && forceNum.Equals(eq_book_pyeong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("book_pyeong") && !forceNum.Equals(eq_book_pyeong)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.yang_yang :
			if (cName.Equals("yang_yang") && forceNum.Equals(eq_yang_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("sin_ya") && forceNum.Equals(eq_sin_ya)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("sang_yong") && forceNum.Equals(eq_sang_yong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_rng") && forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("sin_ya") && !forceNum.Equals(eq_sin_ya)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("sang_yong") && !forceNum.Equals(eq_sang_yong)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_rng") && !forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.yeo_gang :
			if (cName.Equals("yeo_gang") && forceNum.Equals(eq_yeo_gang)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("si_sang") && forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_ha") && forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_rng") && forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && !forceNum.Equals(eq_si_sang)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_ha") && !forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_rng") && !forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && !forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && !forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && !forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.yeo_nam :
			if (cName.Equals("yeo_nam") && forceNum.Equals(eq_yeo_nam)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("sin_ya") && forceNum.Equals(eq_sin_ya)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_ha") && forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("sin_ya") && !forceNum.Equals(eq_sin_ya)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_ha") && !forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && !forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("soo_choon") && !forceNum.Equals(eq_soo_choon)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.yeong_an :
			if (cName.Equals("yeong_an") && forceNum.Equals(eq_yeong_an)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("gang_zoo") && forceNum.Equals(eq_gang_zoo)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_rng") && forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_zoo") && !forceNum.Equals(eq_gang_zoo)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_rng") && !forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.yeong_chang :
			if (cName.Equals("yeong_chang") && forceNum.Equals(eq_yeong_chang)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("oon_nam") && forceNum.Equals(eq_oon_nam)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("oon_nam") && !forceNum.Equals(eq_oon_nam)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.yeong_rng :
			if (cName.Equals("yeong_rng") && forceNum.Equals(eq_yeong_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("gye_yang") && forceNum.Equals(eq_gye_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_rng") && forceNum.Equals(eq_moo_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gye_yang") && !forceNum.Equals(eq_gye_yang)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_sa") && !forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_rng") && !forceNum.Equals(eq_moo_rng)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.za_dong :
			if (cName.Equals("za_dong") && forceNum.Equals(eq_za_dong)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("gang_zoo") && forceNum.Equals(eq_gang_zoo)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geom_gak") && forceNum.Equals(eq_geom_gak)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("myeon_zook_gwan") && forceNum.Equals(eq_myeon_zook_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_zoo") && !forceNum.Equals(eq_gang_zoo)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geom_gak") && !forceNum.Equals(eq_geom_gak)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("myeon_zook_gwan") && !forceNum.Equals(eq_myeon_zook_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.zang_an :
			if (cName.Equals("zang_an") && forceNum.Equals(eq_zang_an)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("han_zoong") && forceNum.Equals(eq_han_zoong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("an_zeong") && forceNum.Equals(eq_an_zeong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("dong_gwan") && forceNum.Equals(eq_dong_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_gwan") && forceNum.Equals(eq_moo_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("han_zoong") && !forceNum.Equals(eq_han_zoong)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("an_zeong") && !forceNum.Equals(eq_an_zeong)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("dong_gwan") && !forceNum.Equals(eq_dong_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_gwan") && !forceNum.Equals(eq_moo_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.zang_sa :
			if (cName.Equals("zang_sa") && forceNum.Equals(eq_zang_sa)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("gye_yang") && forceNum.Equals(eq_gye_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeong_rng") && forceNum.Equals(eq_yeong_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_rng") && forceNum.Equals(eq_moo_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_hae") && forceNum.Equals(eq_nam_hae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_rng") && forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_ha") && forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_gang") && forceNum.Equals(eq_yeo_gang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("gye_yang") && !forceNum.Equals(eq_gye_yang)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yeong_rng") && !forceNum.Equals(eq_yeong_rng)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_rng") && !forceNum.Equals(eq_moo_rng)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("nam_hae") && !forceNum.Equals(eq_nam_hae)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_rng") && !forceNum.Equals(eq_gang_rng)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("gang_ha") && !forceNum.Equals(eq_gang_ha)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("si_sang") && !forceNum.Equals(eq_si_sang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yeo_gang") && !forceNum.Equals(eq_yeo_gang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geon_up") && !forceNum.Equals(eq_geon_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.zin_ru :
			if (cName.Equals("zin_ru") && forceNum.Equals(eq_zin_ru)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("so_pae") && forceNum.Equals(eq_so_pae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && forceNum.Equals(eq_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			}  else if (cName.Equals("ho_ro_gwan") && forceNum.Equals(eq_horo_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("so_pae") && !forceNum.Equals(eq_so_pae)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && !forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && !forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && !forceNum.Equals(eq_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_ro_gwan") && !forceNum.Equals(eq_horo_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.myeon_zook_gwan :
			if (cName.Equals("myeon_zook_gwan") && forceNum.Equals(eq_myeon_zook_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("za_dong") && forceNum.Equals(eq_za_dong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("seong_do") && forceNum.Equals(eq_seong_do)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_do") && forceNum.Equals(eq_moo_do)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("cheon_soo") && forceNum.Equals(eq_cheon_soo)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("za_dong") && !forceNum.Equals(eq_za_dong)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("seong_do") && !forceNum.Equals(eq_seong_do)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_do") && !forceNum.Equals(eq_moo_do)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("cheon_soo") && !forceNum.Equals(eq_cheon_soo)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.geom_gak :
			if (cName.Equals("geom_gak") && forceNum.Equals(eq_geom_gak)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("za_dong") && forceNum.Equals(eq_za_dong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("yang_pyeong_gwan") && forceNum.Equals(eq_yang_pyeong_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("za_dong") && !forceNum.Equals(eq_za_dong)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("yang_pyeong_gwan") && !forceNum.Equals(eq_yang_pyeong_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.yang_pyeong_gwan :
			if (cName.Equals("yang_pyeong_gwan") && forceNum.Equals(eq_yang_pyeong_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("han_zoong") && forceNum.Equals(eq_han_zoong)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_do") && forceNum.Equals(eq_moo_do)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			}  else if (cName.Equals("geom_gak") && forceNum.Equals(eq_geom_gak)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("han_zoong") && !forceNum.Equals(eq_han_zoong)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_do") && !forceNum.Equals(eq_moo_do)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("geom_gak") && !forceNum.Equals(eq_geom_gak)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.moo_gwan :
			if (cName.Equals("moo_gwan") && forceNum.Equals(eq_moo_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("wan") && forceNum.Equals(eq_wan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_an") && forceNum.Equals(eq_zang_an)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			}  else if (cName.Equals("dong_gwan") && forceNum.Equals(eq_dong_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("wan") && !forceNum.Equals(eq_wan)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_an") && !forceNum.Equals(eq_zang_an)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("dong_gwan") && !forceNum.Equals(eq_dong_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.dong_gwan :
			if (cName.Equals("dong_gwan") && forceNum.Equals(eq_dong_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("ham_gok_gwan") && forceNum.Equals(eq_ham_gok_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_an") && forceNum.Equals(eq_zang_an)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			}  else if (cName.Equals("moo_gwan") && forceNum.Equals(eq_moo_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ham_gok_gwan") && !forceNum.Equals(eq_ham_gok_gwan)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zang_an") && !forceNum.Equals(eq_zang_an)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("moo_gwan") && !forceNum.Equals(eq_moo_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.horo_gwan :
			if (cName.Equals("ho_ro_gwan") && forceNum.Equals(eq_horo_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("nak_yang") && forceNum.Equals(eq_nak_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_ru") && forceNum.Equals(eq_zin_ru)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && forceNum.Equals(eq_up)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("nak_yang") && !forceNum.Equals(eq_nak_yang)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("heo_chang") && !forceNum.Equals(eq_heo_chang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("zin_ru") && !forceNum.Equals(eq_zin_ru)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("bok_yang") && !forceNum.Equals(eq_bok_yang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("up") && !forceNum.Equals(eq_up)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.ham_gok_gwan :
			if (cName.Equals("ham_gok_gwan") && forceNum.Equals(eq_ham_gok_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("nak_yang") && forceNum.Equals(eq_nak_yang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_nae") && forceNum.Equals(eq_ha_nae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("dong_gwan") && forceNum.Equals(eq_dong_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_gwan") && forceNum.Equals(eq_ho_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("nak_yang") && !forceNum.Equals(eq_nak_yang)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_nae") && !forceNum.Equals(eq_ha_nae)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("dong_gwan") && !forceNum.Equals(eq_dong_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ho_gwan") && !forceNum.Equals(eq_ho_gwan)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
		case UserCastle.ho_gwan :
			if (cName.Equals("ho_gwan") && forceNum.Equals(eq_ho_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(true);
				// 자기성에서 이동가능한 성클릭(아군성)
			} else if (cName.Equals("ham_gok_gwan") && forceNum.Equals(eq_ham_gok_gwan)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_nae") && forceNum.Equals(eq_ha_nae)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("sang_dang") && forceNum.Equals(eq_sang_dang)) {
				menu1.SetActive(false);
				menu2.SetActive(true);
				menu3.SetActive(false);
			} else if (cName.Equals("ham_gok_gwan") && !forceNum.Equals(eq_ham_gok_gwan)) { // --------------------------------------------
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("ha_nae") && !forceNum.Equals(eq_ha_nae)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else if (cName.Equals("sang_dang") && !forceNum.Equals(eq_sang_dang)) {
				menu1.SetActive(true);
				menu2.SetActive(false);
				menu3.SetActive(false);
			} else {
				menu1.SetActive(false);
				menu2.SetActive(false);
				menu3.SetActive(false);
			}
			break;
		//----------------------------------------------------------------------------------------------------------------------
			
		}
		
	}



	// Update is called once per frame
	void Update () {
		arrowPivot ();
		cameraControll();

	}
}
