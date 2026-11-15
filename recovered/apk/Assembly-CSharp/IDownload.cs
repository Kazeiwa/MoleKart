using UnityEngine;

public interface IDownload
{
	WWW wwwObject { get; }

	DownloadMode downloadMode { get; }

	string bundleName { get; }

	bool isDone { get; }

	float progress { get; }

	string text { get; }

	byte[] bytes { get; }

	string error { get; }

	Texture2D texture { get; }

	AudioClip audioClip { get; }

	AssetBundle assetBundle { get; }

	string url { get; }

	ThreadPriority threadPriority { get; set; }

	void Download();

	void DownloadWithPath(string path);

	void Remove(bool unloadAllLoadedObjects);
}
