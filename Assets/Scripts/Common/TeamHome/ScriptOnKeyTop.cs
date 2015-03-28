using UnityEngine;
using System.Collections;

public class ScriptOnKeyTop : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BtnClicked(string name)
	{
		switch (name) {
		case "BtnPhoto":
			OpenPhoto();
			break;
		case "BtnLink":
			OpenLinkWindow();
			break;
		case "BtnCamera":
			OpenCamera();
			break;
		}
	}

	ScriptItemPhoto GetCurrentItemPhoto()
	{
		ScriptItemPhoto sip = null;
		ScriptWritten sw = transform.parent.GetComponent<ScriptWritten> ();
		for(int i = 0; i < sw.mListItemPhoto.Count; i++)
		{
			ScriptItemPhoto tmp = sw.mListItemPhoto[i].GetComponent<ScriptItemPhoto>();
			if(tmp.isActive())
			{
				if(i == 3)
				{

				}
				else
				{
					continue;
				}
			}
			else
			{
				sip = tmp;
				break;
			}
		}

		return sip;
	}

	void OpenCamera()
	{
		ScriptItemPhoto sip = GetCurrentItemPhoto ();
		if (sip == null)
						return;
		if(Application.platform == RuntimePlatform.Android)
		{
			AndroidMgr.OpenCamera(new EventDelegate(this, "GotPhoto"));
		}
		else
		{
			
		}

	}

	public void GotPhoto()
	{
		string msg = AndroidMgr.GetMsg ();
	}

	void OpenPhoto()
	{
		ScriptItemPhoto sip = GetCurrentItemPhoto ();
		if (sip == null)
			return;

		if(Application.platform == RuntimePlatform.Android)
		{
			AndroidMgr.OpenGallery(new EventDelegate(this, "GotPhoto"));
		}
		else
		{
			
		}

	}

	void OpenLinkWindow()
	{
		transform.parent.parent.FindChild ("Link").gameObject.SetActive (true);
		transform.parent.parent.FindChild ("Link").gameObject.GetComponent<ScriptLink> ().Init ();
	}










}
