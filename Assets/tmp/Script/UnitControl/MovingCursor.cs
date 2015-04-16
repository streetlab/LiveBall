using UnityEngine;
using System.Collections;

public class MovingCursor : MonoBehaviour {

	public UnitAction ua;
	public bool cursorImg;

	void OnTriggerStay2D(Collider2D col) {

		if (ua.us != UnitState.DEAD) {

			if(col.gameObject.tag == "computer") {
				Cursor.SetCursor (ua.at_cursorTexture, ua.hotSpot, CursorMode.Auto);
				cursorImg = true;
			} 

		}

	}
		
	void OnTriggerExit2D (Collider2D col) {
		
		if(col.gameObject.tag == "computer") {	
			Cursor.SetCursor (ua.cursorTexture, ua.hotSpot, CursorMode.Auto);
			cursorImg = false;
		}
		
	}

	void OnTriggerEnter2D (Collider2D col) {
		
		if(col.gameObject.tag != "computer") {	
			Cursor.SetCursor (ua.cursorTexture, ua.hotSpot, CursorMode.Auto);
			cursorImg = false;
		}
		
	}





}
