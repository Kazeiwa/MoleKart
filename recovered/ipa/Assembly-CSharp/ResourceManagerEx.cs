using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

public class ResourceManagerEx : IUTResourceManager
{
	private class ResourceCollection
	{
		private static Dictionary<string, ResourceObject> resourceObjectList;

		public static ResourceObject FindResource(string name)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public static ResourceObject AddResource(string name)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public static bool RemoveResource(string name)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public static void RemoveAllResources()
		{
		}
	}

	private static Dictionary<string, string> RemoteFolderMap;

	public void Start()
	{
	}

	public void Start(XmlDocument xml)
	{
	}

	public void FixedUpdate()
	{
	}

	public void Quit()
	{
	}

	[DebuggerHidden]
	private IEnumerator MyWaitForFixedUpdate()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void DownLoadResource(string name, OnDownloadFinished listener)
	{
	}

	public ResourceObject DownLoadResource(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void RemoveAllResources()
	{
	}

	public bool RemoveResource(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public ResourceObject GetResourceObject(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
