using UnityEngine;

public class ResourceObject
{
	public string name;

	private IDownload downloadObject;

	public string bundleName => (downloadObject != null) ? downloadObject.bundleName : string.Empty;

	public bool isDone => downloadObject != null && downloadObject.isDone;

	public float progress => (downloadObject != null) ? downloadObject.progress : 0f;

	public string text => (downloadObject != null) ? downloadObject.text : string.Empty;

	public byte[] bytes => (downloadObject != null) ? downloadObject.bytes : null;

	public string error => (downloadObject != null) ? downloadObject.error : "WWW object unavailable!";

	public Texture2D texture => (downloadObject != null) ? downloadObject.texture : null;

	public AudioClip audioClip => (downloadObject != null) ? downloadObject.audioClip : null;

	public AssetBundle assetBundle => (downloadObject != null) ? downloadObject.assetBundle : null;

	public string url => (downloadObject != null) ? downloadObject.url : string.Empty;

	public ThreadPriority threadPriority
	{
		get
		{
			return (downloadObject != null) ? downloadObject.threadPriority : ThreadPriority.Normal;
		}
		set
		{
			if (downloadObject != null)
			{
				downloadObject.threadPriority = threadPriority;
			}
		}
	}

	public WWW wwwObject => (downloadObject != null) ? downloadObject.wwwObject : null;

	public ResourceObject(string name)
	{
		this.name = name;
		downloadObject = new DownloadRemote(this.name);
	}

	public void StartDownload()
	{
		if (downloadObject == null)
		{
			downloadObject = new DownloadRemote(name);
		}
	}

	public void Download()
	{
		if (downloadObject != null)
		{
			downloadObject.Download();
		}
		else
		{
			Debug.LogError("DownloadRemote MUST be created before using it.");
		}
	}

	public void DownloadWithPath(string path)
	{
		if (downloadObject != null)
		{
			downloadObject.DownloadWithPath(path);
		}
		else
		{
			Debug.LogError("DownloadRemote MUST be created before using it.");
		}
	}

	public void Clear(bool unloadAllLoadedObjects)
	{
		if (downloadObject != null)
		{
			downloadObject.Remove(unloadAllLoadedObjects);
		}
		else
		{
			Debug.LogError("DownloadRemote MUST be created before using it.");
		}
	}
}
