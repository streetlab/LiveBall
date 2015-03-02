using UnityEngine;
using System.Collections;

public class AutoFade : MonoBehaviour
{
	private static AutoFade m_Instance = null;
	private string m_LevelName = "";
	
	private static AutoFade Instance
	{
		get
		{
			if (m_Instance == null)
			{
				m_Instance = (new GameObject("AutoFade")).AddComponent<AutoFade>();
			}
			return m_Instance;
		}
	}
	
	private void Awake()
	{
		DontDestroyOnLoad(this);
		m_Instance = this;
	}
	
	private IEnumerator Fade(float aFadeOutTime, float aFadeInTime)
	{
		TweenAlpha.Begin (GameObject.Find("UI Root"), aFadeOutTime, 0f);
		yield return new WaitForSeconds(aFadeOutTime);

		Application.LoadLevel(m_LevelName);
		yield return new AsyncOperation ();

		TweenAlpha.Begin (GameObject.Find("UI Root"), 0f, 0f);
		TweenAlpha.Begin (GameObject.Find("UI Root"), aFadeInTime, 1.0f);
		yield return new WaitForSeconds(aFadeInTime);

	}
	private void StartFade(float aFadeOutTime, float aFadeInTime)
	{
		StartCoroutine(Fade(aFadeOutTime, aFadeInTime));
	}
	
	public static void LoadLevel(string aLevelName,float aFadeOutTime, float aFadeInTime)
	{
		Instance.m_LevelName = aLevelName;
		Instance.StartFade(aFadeOutTime, aFadeInTime);
	}

}