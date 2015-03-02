using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

public class ScriptWritten : MonoBehaviour {

	public List<GameObject> mListItemPhoto;

	// Use this for initialization
	void Start () {
//		transform.FindChild ("TestList").gameObject.GetComponent<UIDraggablePanel2> ().Init (100, delegate(UIListItem item, int index) {
//			Debug.Log("index : "+index);
//		});
		mListItemPhoto = new List<GameObject>();
		mListItemPhoto.Add (transform.FindChild ("ItemPhoto1").gameObject);
		mListItemPhoto.Add (transform.FindChild ("ItemPhoto2").gameObject);
		mListItemPhoto.Add (transform.FindChild ("ItemPhoto3").gameObject);
		mListItemPhoto.Add (transform.FindChild ("ItemPhoto4").gameObject);



	}

	// Update is called once per frame
	void Update () {
	
	}

	public void BtnClicked(string name)
	{
		switch(name)
		{
		case "BtnClose":

			break;
		case "BtnSend":

			break;
		}
	}
}
