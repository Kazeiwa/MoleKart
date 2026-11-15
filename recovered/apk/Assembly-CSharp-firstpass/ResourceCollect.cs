using System.Collections.Generic;

public class ResourceCollect
{
	public static Dictionary<string, ResourceItem> sharedList = new Dictionary<string, ResourceItem>();

	public static Dictionary<string, ResourceItem> objectList = new Dictionary<string, ResourceItem>();

	public ResourceItem FindResource(string resourceName)
	{
		if (resourceName.Length > 0 && sharedList.ContainsKey(resourceName))
		{
			return sharedList[resourceName];
		}
		if (resourceName.Length > 0 && objectList.ContainsKey(resourceName))
		{
			return objectList[resourceName];
		}
		return null;
	}

	public void AddResource(ResourceItem item, bool shared)
	{
		if (item != null && item.name.Length > 0)
		{
			if (shared && !sharedList.ContainsKey(item.name))
			{
				sharedList.Add(item.name, item);
			}
			else if (!shared && !objectList.ContainsKey(item.name))
			{
				objectList.Add(item.name, item);
			}
		}
	}

	public bool RemoveResource(string name)
	{
		if (name == null || name.Length <= 0)
		{
			return false;
		}
		if (sharedList.ContainsKey(name))
		{
			return sharedList.Remove(name);
		}
		if (objectList.ContainsKey(name))
		{
			return objectList.Remove(name);
		}
		return false;
	}

	public void RemoveAllResources()
	{
		sharedList.Clear();
		objectList.Clear();
	}

	public Dictionary<string, ResourceItem> GetSharedResourceList()
	{
		return sharedList;
	}

	public Dictionary<string, ResourceItem> GetUnSharedResourceList()
	{
		return objectList;
	}
}
