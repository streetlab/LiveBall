using UnityEngine;
using System.Collections;
using System;

public enum UnitState {

	IDLE,
	WALK,
	ATTACK,
	AT_WALK,
	DEAD

}


public class UnitAction : MonoBehaviour {
		
	public float speed = 0.1f;
	public Transform target; // 마우스 포인트 위치 알기 위한 변수
	public GameObject curser;
	public GameObject moving_curser;
	private bool isMove = false;

	public GameObject cmCon;
	public float cmSpeed = 3f;

	public Animator anim;
	public Animator pointer;
	public Animator be_shotAnim;

	public SoundControl sc;
	public MenuController menuCon;
	public CursorColl cc;
	public MovingCursor mc; // 움직이는 커서 스크립트
	public UnitState us;
	public TutoCon tc;
	
	public GameObject see;
	public GameObject game_over;
	public GameObject tutoBGM;
	public AudioClip victory_sound;
	public AudioClip lose_sound;
	
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
	
	public Animator pointer_at;




	// Use this for initialization
	void Start () {
		StartCoroutine("MyCursor");
		isMove = false;
		us = UnitState.IDLE;
		tutoBGM.SetActive(true);
	}

	IEnumerator MyCursor() {
		
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


	// Update is called once per frame
	void Update () {

		if(tc.ts == TutoState.STAGE1) {
			cameraControll();
		}

		if (tc.ts == TutoState.CLEAR || tc.ts == TutoState.STAGE5 || tc.ts == TutoState.STAGE6) {

			cameraControll();
			
			if (us != UnitState.DEAD) {
				
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
				RaycastHit hit; // 빛이 맞은곳
				
				if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
					Debug.DrawLine(ray.origin, hit.point);
					
					moving_curser.transform.position = new Vector3(hit.point.x, hit.point.y, -0.1f);
					
				}
				
				
				
				if (see.transform.position != transform.position) {
					see.transform.position = transform.position;
				}
				
				
				
				// 일반이동
				if (Input.GetMouseButtonDown(1)) {
					
					us = UnitState.WALK;
					speed = 0.1f;
					gdClick();
					unitRotateAnim();
					sc.moveSound();
					if (!mc.cursorImg) {
						pointer.SetTrigger("onClick");	
					} 
					
				}
				
				if (isMove) {
					
					if (Vector2.Distance(transform.position, curser.transform.position) == 0.0f) {
						isMove = false;
						return;
					} else {
						transform.position = Vector2.Lerp(transform.position, curser.transform.position, speed * Time.deltaTime);
					}
					
				}
				
			}

		}




    }
    
	public void cameraControll() {

		// 카메로 조종
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (cmCon.camera.transform.position.x >= -3.45f) {
				cmCon.transform.Translate(Vector3.left * cmSpeed * Time.deltaTime);
			}
		} 		  
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			if (cmCon.camera.transform.position.x <= 3.4f) {
				cmCon.transform.Translate (Vector3.right * cmSpeed * Time.deltaTime);
			}
		}
		
		if (Input.GetKey (KeyCode.UpArrow)) {
			if (cmCon.camera.transform.position.y <= 1.6f) {
				cmCon.transform.Translate (Vector3.up * cmSpeed * Time.deltaTime);
			}
		}
		
		if (Input.GetKey (KeyCode.DownArrow)) {
			if (cmCon.camera.transform.position.y >= -1.6f) {
				cmCon.transform.Translate (Vector3.down * cmSpeed * Time.deltaTime);
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			cameraOneMoveCon();
		}

	}
    
	public void gdClick () {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
		RaycastHit hit; // 빛이 맞은곳
		
		if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
			Debug.DrawLine(ray.origin, hit.point);
			
			curser.transform.position = new Vector3(hit.point.x, hit.point.y, -0.1f);
			isMove = true;

		}

	}
	
	// 부대방향 애니메이션 컨트롤
	public void unitRotateAnim() {

		if (curser.transform.position.x < transform.position.x) {
			anim.SetTrigger("left_walk");
		} else if (curser.transform.position.x > transform.position.x) {
			anim.SetTrigger("right_walk");
		} 

	}

	// 카메라 부대중심으로 이동(마우스로 얼굴클릭)
	public void cameraMyunit() {
		cameraOneMoveCon();
	}

	public void cameraOneMoveCon() {
		cmCon.camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
		
		if (cmCon.camera.transform.position.x >= 3.45f) {
			cmCon.camera.transform.position = new Vector3(3.44f, transform.position.y, -10f);
		}
		
		if (cmCon.camera.transform.position.x <= -3.4f) {
			cmCon.camera.transform.position = new Vector3(-3.3f, transform.position.y, -10f);
		}
		
		if (cmCon.camera.transform.position.y >= 1.6f) {
			cmCon.camera.transform.position = new Vector3(transform.position.x, 1.6f, -10f);
		}
		
		if (cmCon.camera.transform.position.y <= -1.6f) {
			cmCon.camera.transform.position = new Vector3(transform.position.x, -1.6f,  -10f);
		}
		
		
		if (cmCon.camera.transform.position.x >= 3.45f && cmCon.camera.transform.position.y >= 1.6f) {
			cmCon.camera.transform.position = new Vector3(3.44f, 1.6f, -10f);
		}
		
		if (cmCon.camera.transform.position.x >= 3.45f && cmCon.camera.transform.position.y <= -1.6f) {
			cmCon.camera.transform.position = new Vector3(3.44f, -1.6f, -10f);
		}
		
		if (cmCon.camera.transform.position.x <= -3.4f && cmCon.camera.transform.position.y >= 1.6f) {
			cmCon.camera.transform.position = new Vector3(-3.3f, 1.6f, -10f);
		} 
		
		if (cmCon.camera.transform.position.x <= -3.4f && cmCon.camera.transform.position.y <= -1.6f) {
			cmCon.camera.transform.position = new Vector3(-3.3f, -1.6f, -10f);
		} 
		
		if (cmCon.camera.transform.position.y >= 1.6f && cmCon.camera.transform.position.x >= 3.45f) {
			cmCon.camera.transform.position = new Vector3(3.44f, 1.6f, -10f);
		}
		
		if (cmCon.camera.transform.position.y >= 1.6f && cmCon.camera.transform.position.x <= -3.4f) {
			cmCon.camera.transform.position = new Vector3(-3.3f, 1.6f, -10f);
		}
		
		if (cmCon.camera.transform.position.y <= -1.6f && cmCon.camera.transform.position.x >= 3.45f) {
			cmCon.camera.transform.position = new Vector3(3.44f, -1.6f,  -10f);
		} 
		
		if (cmCon.camera.transform.position.y <= -1.6f && cmCon.camera.transform.position.x <= -3.4f) {
			cmCon.camera.transform.position = new Vector3(-3.3f, -1.6f,  -10f);
		} 
	}


	public AudioClip beshot_Sound;

	// 데미지 받는 메소드(적군에게 피격당하면 불림)
	public void shot(float dam) {

		if (!audio.isPlaying) {
			audio.clip = beshot_Sound;
			audio.Play();
		}

		be_shotAnim.SetTrigger("MyBeshot");
		menuCon.unitDamage(dam);

	}
	// 죽으면 불리는
	public void unitDEAD() {

		us = UnitState.DEAD;
		speed = 0f;
		anim.SetTrigger("DEAD");
		StartCoroutine(unitOff());

	}

	IEnumerator unitOff() {

		yield return new WaitForSeconds(0.8f);
		anim.SetTrigger("Absol_DEAD");
		tutoBGM.SetActive(false);
		yield return new WaitForSeconds(2);
		game_over.SetActive(true);
		audio.clip = lose_sound;
		audio.Play();
		// 튜토 실패후 씬이동

	}

	// 적이 죽으면 불리는 메소드 (경험치 획득)

	public void expUp(float exp) {
		menuCon.unitExp(exp);
	}

	public void goldUp(int gold) {
		menuCon.unitGold(gold);
	}

	public void namedUp(int named) {
		menuCon.unitNamedUp(named);
	}








}
