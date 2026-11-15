using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class LoadLevelAsync : MonoBehaviour
{
	private LoadLevelCallback callback;

	private string m_strLevel;

	public void LoadLevel(LoadLevelCallback callback, string strName)
	{
	}

	[DebuggerHidden]
	private IEnumerator Load()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
