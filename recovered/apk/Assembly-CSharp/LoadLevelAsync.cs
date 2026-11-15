using System.Collections;
using UnityEngine;

public class LoadLevelAsync : MonoBehaviour
{
	private LoadLevelCallback callback;

	private string m_strLevel;

	public void LoadLevel(LoadLevelCallback callback, string strName)
	{
		m_strLevel = strName;
		this.callback = callback;
		StartCoroutine(Load());
	}

	private IEnumerator Load()
	{
		yield return Application.LoadLevelAsync(m_strLevel);
		if (callback != null)
		{
			callback();
		}
	}
}
