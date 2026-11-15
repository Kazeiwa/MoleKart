using System;
using System.Reflection;
using UnityEngine;

public class WWWAssemblyLoader : MonoBehaviour
{
	public string m_AssemblyURL;

	private string m_ErrorString = string.Empty;

	private WWW m_WWW;

	private bool m_Complete = true;

	public string AssemblyURL
	{
		get
		{
			return m_AssemblyURL;
		}
		set
		{
			if (m_AssemblyURL != value)
			{
				ReloadAssembly(value);
			}
		}
	}

	public float Progress => (!m_Complete) ? m_WWW.progress : 1f;

	public string Error => m_ErrorString;

	public void Start()
	{
		if (m_AssemblyURL != string.Empty)
		{
			ReloadAssembly(m_AssemblyURL);
		}
	}

	public void ReloadAssembly(string url)
	{
		m_Complete = false;
		m_ErrorString = string.Empty;
		m_AssemblyURL = url;
		m_WWW = new WWW(m_AssemblyURL);
	}

	public void Update()
	{
		if (m_Complete)
		{
			return;
		}
		if (m_WWW.error != null)
		{
			m_ErrorString = m_WWW.error;
			m_Complete = true;
			SendMessage("OnAssemblyLoadFailed", m_AssemblyURL);
		}
		else if (m_WWW.isDone)
		{
			Assembly assembly = LoadAssembly();
			m_Complete = true;
			if (assembly != null)
			{
				Debug.Log("Done");
				SendMessage("OnAssemblyLoaded", new WWWAssembly(m_AssemblyURL, assembly));
			}
			else
			{
				Debug.Log("Failed");
				SendMessage("OnAssemblyLoadFailed", m_AssemblyURL);
			}
		}
	}

	private Assembly LoadAssembly()
	{
		try
		{
			return Assembly.Load(m_WWW.bytes);
		}
		catch (Exception ex)
		{
			m_ErrorString = ex.ToString();
			return null;
		}
	}
}
