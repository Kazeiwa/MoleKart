using System.Collections;
using System.Xml;
using UnityEngine;

public class UTResourceManager : IUTResourceManager
{
	private static ResourceManagerEx resourceManager = new ResourceManagerEx();

	public static void Start()
	{
		if (resourceManager != null)
		{
			resourceManager.Start();
		}
		else
		{
			Debug.LogError("Failed to create ResourceManagerEx");
		}
	}

	public static void Start(XmlDocument config)
	{
		if (resourceManager != null)
		{
			resourceManager.Start(config);
		}
		else
		{
			Debug.LogError("Failed to create ResourceManagerEx");
		}
	}

	public static void FixedUpdate()
	{
		if (resourceManager != null)
		{
			resourceManager.FixedUpdate();
		}
		else
		{
			Debug.LogError("Failed to create ResourceManagerEx");
		}
	}

	public static void Quit()
	{
		if (resourceManager != null)
		{
			resourceManager.Quit();
		}
		else
		{
			Debug.LogError("Failed to create ResourceManagerEx");
		}
	}

	public static IEnumerator DownLoadResource(string name, OnDownloadFinished listener)
	{
		if (resourceManager != null)
		{
			resourceManager.DownLoadResource(name, listener);
		}
		else
		{
			Debug.LogError("Failed to create ResourceManagerEx");
		}
		yield return 0;
	}

	public static ResourceObject DownLoadResource(string name)
	{
		if (resourceManager != null)
		{
			return resourceManager.DownLoadResource(name);
		}
		Debug.LogError("Failed to create ResourceManagerEx");
		return null;
	}

	public static void RemoveAllResources()
	{
		if (resourceManager != null)
		{
			resourceManager.RemoveAllResources();
		}
		else
		{
			Debug.LogError("Failed to create ResourceManagerEx");
		}
	}

	public static bool RemoveResource(string name)
	{
		if (resourceManager != null)
		{
			return resourceManager.RemoveResource(name);
		}
		Debug.LogError("Failed to create ResourceManagerEx");
		return false;
	}

	public static ResourceObject GetResourceObject(string name)
	{
		if (resourceManager != null)
		{
			return resourceManager.GetResourceObject(name);
		}
		Debug.LogError("Failed to create ResourceManagerEx");
		return null;
	}
}
