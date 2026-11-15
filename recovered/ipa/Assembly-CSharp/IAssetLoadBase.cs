using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class IAssetLoadBase : MonoBehaviour
{
	private ResourceObject remoteResource;

	[DebuggerHidden]
	public virtual IEnumerator DownloadAssetBundle(string assetName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void DownloadAssetBundleAsyn(string assetName)
	{
	}

	public virtual void OnDownloadFinishedHandler(ResourceObject resource)
	{
	}

	[DebuggerHidden]
	public virtual IEnumerator StartDownloadAsyn(string assetName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
	}
}
