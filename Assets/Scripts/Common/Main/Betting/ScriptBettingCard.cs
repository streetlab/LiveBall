using UnityEngine;
using System.Collections;

public class ScriptBettingCard : UIDragDropItem {
	protected override void OnDragDropRelease (GameObject surface)
	{
		Debug.Log ("x : " + transform.localPosition.x + ", y : " + transform.localPosition.y);
		if (surface != null) {
			Debug.Log("surface.name : "+surface.name);
		}
		base.OnDragDropRelease (surface);
	}

	protected override void OnDragDropEnd () {

	}

//	public void OnDrop(GameObject droppedItem){
//		Debug.Log ("droppedItem.name : " + droppedItem.name);
//		Debug.Log ("x : " + droppedItem.transform.localPosition.x + ", y : " + droppedItem.transform.localPosition.y);
//	}
}
