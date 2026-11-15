using UnityEngine;

public class DownloadRemote : IDownload
{
	private WWW wwwDownload;

	private string name;

	private string folder;

	private string _bundleName;

	public WWW wwwObject => wwwDownload;

	public DownloadMode downloadMode => DownloadMode.Remote;

	public string bundleName => (_bundleName != null) ? _bundleName : string.Empty;

	public bool isDone => wwwDownload != null && wwwDownload.isDone;

	public float progress => (wwwDownload != null && wwwDownload.error == null) ? wwwDownload.progress : 0f;

	public string text => (wwwDownload != null && wwwDownload.error == null) ? wwwDownload.text : string.Empty;

	public byte[] bytes => (wwwDownload != null && wwwDownload.error == null) ? wwwDownload.bytes : null;

	public string error => (wwwDownload != null && wwwDownload.error == null) ? wwwDownload.error : "WWW object unavailable!";

	public Texture2D texture => (wwwDownload != null && wwwDownload.error == null) ? wwwDownload.texture : null;

	public AudioClip audioClip => (wwwDownload != null && wwwDownload.error == null) ? wwwDownload.audioClip : null;

	public AssetBundle assetBundle => (wwwDownload != null && wwwDownload.error == null) ? wwwDownload.assetBundle : null;

	public string url => (wwwDownload != null && wwwDownload.error == null) ? wwwDownload.url : string.Empty;

	public ThreadPriority threadPriority
	{
		get
		{
			return (wwwDownload != null && wwwDownload.error == null) ? wwwDownload.threadPriority : ThreadPriority.Normal;
		}
		set
		{
			if (wwwDownload != null)
			{
				wwwDownload.threadPriority = threadPriority;
			}
		}
	}

	public DownloadRemote(string name)
	{
		this.name = name;
		_bundleName = name;
		folder = GetRomoteFolder();
	}

	public void Download()
	{
		if (name.Length <= 0)
		{
			Debug.LogError("Must provide resource name befor using this method");
			return;
		}
		string text = folder + name;
		Download(text);
	}

	public void DownloadWithPath(string path)
	{
		if (path == null || path.Length <= 0)
		{
			Download();
			return;
		}
		_bundleName = path;
		string text = folder + path;
		Download(text);
	}

	private void Download(string url)
	{
		if (url.Length <= 0)
		{
			Debug.LogError("DownloadRemote:Download(string) input an empty string!!!");
		}
		else
		{
			wwwDownload = new WWW(url);
		}
	}

	public void Remove(bool unloadAllLoadedObjects)
	{
		if (wwwDownload != null && wwwDownload.assetBundle != null)
		{
			wwwDownload.assetBundle.Unload(unloadAllLoadedObjects);
		}
	}

	private string GetRomoteFolder()
	{
		if (Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer)
		{
			return Application.dataPath + "/";
		}
		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
		{
			return "file://" + Application.dataPath + "/../";
		}
		if (Application.platform == RuntimePlatform.OSXPlayer)
		{
			return "file://" + Application.dataPath + "/../../";
		}
		if (Application.platform == RuntimePlatform.WindowsPlayer)
		{
			return "file://" + Application.dataPath + "/../";
		}
		return string.Empty;
	}
}
