using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ResourceManagerEx : IUTResourceManager
{
	private class ResourceCollection
	{
		private static Dictionary<string, ResourceObject> resourceObjectList = new Dictionary<string, ResourceObject>();

		public static ResourceObject FindResource(string name)
		{
			if (name.Length > 0 && resourceObjectList.ContainsKey(name))
			{
				return resourceObjectList[name];
			}
			return null;
		}

		public static ResourceObject AddResource(string name)
		{
			if (!resourceObjectList.ContainsKey(name))
			{
				ResourceObject resourceObject = new ResourceObject(name);
				resourceObjectList.Add(name, resourceObject);
				return resourceObject;
			}
			return null;
		}

		public static bool RemoveResource(string name)
		{
			if (resourceObjectList.ContainsKey(name))
			{
				resourceObjectList[name]?.Clear(unloadAllLoadedObjects: false);
				return resourceObjectList.Remove(name);
			}
			return false;
		}

		public static void RemoveAllResources()
		{
			foreach (ResourceObject value in resourceObjectList.Values)
			{
				value.Clear(unloadAllLoadedObjects: false);
			}
			resourceObjectList.Clear();
		}
	}

	private static Dictionary<string, string> RemoteFolderMap = new Dictionary<string, string>();

	public void Start()
	{
	}

	public void Start(XmlDocument xml)
	{
		if (RemoteFolderMap.Count <= 0)
		{
			ParseResourceXml parseResourceXml = new ParseResourceXml(xml);
			parseResourceXml.GetAllObjectsName(RemoteFolderMap);
			Debug.Log("RemoteFolderMap count : " + RemoteFolderMap.Count);
		}
		Start();
	}

	public void FixedUpdate()
	{
	}

	public void Quit()
	{
	}

	private IEnumerator MyWaitForFixedUpdate()
	{
		yield return new WaitForFixedUpdate();
	}

	public void DownLoadResource(string name, OnDownloadFinished listener)
	{
		ResourceObject resourceObject = ResourceCollection.FindResource(name);
		if (resourceObject != null)
		{
			MyWaitForFixedUpdate();
			listener(resourceObject);
			return;
		}
		resourceObject = ResourceCollection.AddResource(name);
		if (resourceObject != null)
		{
			if (RemoteFolderMap.ContainsKey(name))
			{
				resourceObject.DownloadWithPath(RemoteFolderMap[name]);
			}
			else
			{
				resourceObject.Download();
			}
			listener?.Invoke(resourceObject);
		}
		else
		{
			Debug.LogError("Failed to create Download Object!");
		}
	}

	public ResourceObject DownLoadResource(string name)
	{
		ResourceObject resourceObject = ResourceCollection.FindResource(name);
		if (resourceObject != null)
		{
			return resourceObject;
		}
		resourceObject = ResourceCollection.AddResource(name);
		if (resourceObject != null)
		{
			if (RemoteFolderMap.ContainsKey(name))
			{
				resourceObject.DownloadWithPath(RemoteFolderMap[name]);
			}
			else
			{
				resourceObject.Download();
			}
			return resourceObject;
		}
		Debug.LogError("Failed to create Download Object!");
		return null;
	}

	public void RemoveAllResources()
	{
		ResourceCollection.RemoveAllResources();
	}

	public bool RemoveResource(string name)
	{
		return ResourceCollection.RemoveResource(name);
	}

	public ResourceObject GetResourceObject(string name)
	{
		return ResourceCollection.FindResource(name);
	}
}
