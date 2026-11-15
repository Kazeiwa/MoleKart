using System.Collections;
using UnityEngine;

public class IAssetLoadBase : MonoBehaviour
{
	private ResourceObject remoteResource;

	public virtual IEnumerator DownloadAssetBundle(string assetName)
	{
		remoteResource = UTResourceManager.DownLoadResource(assetName);
		yield return remoteResource.wwwObject;
	}

	public virtual void DownloadAssetBundleAsyn(string assetName)
	{
		StartCoroutine(StartDownloadAsyn(assetName));
	}

	public virtual void OnDownloadFinishedHandler(ResourceObject resource)
	{
		if (resource != null)
		{
			AssetBundle assetBundle = resource.assetBundle;
			if (assetBundle != null)
			{
				Object mainAsset = assetBundle.mainAsset;
				GameObject gameObject = (GameObject)Object.Instantiate(mainAsset);
				if (gameObject != null)
				{
					gameObject.transform.parent = base.transform;
					gameObject.transform.position += base.transform.position;
				}
			}
			else
			{
				Debug.LogError("downloadObject.assetBundle return NULL");
			}
		}
		else
		{
			Debug.LogError("UTResourceManager.DownLoadResource() return NULL");
		}
	}

	public virtual IEnumerator StartDownloadAsyn(string assetName)
	{
		StartCoroutine(UTResourceManager.DownLoadResource(assetName, OnDownloadFinishedHandler));
		yield return 0;
	}

	public virtual void Start()
	{
		UTResourceManager.Start();
	}

	public virtual void FixedUpdate()
	{
		UTResourceManager.FixedUpdate();
	}
}
