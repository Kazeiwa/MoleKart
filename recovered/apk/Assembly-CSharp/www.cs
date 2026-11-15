using UnityEngine;

public class www
{
	public WWW httpserver;

	private bool _Used;

	public bool Used
	{
		get
		{
			return _Used;
		}
		set
		{
			_Used = value;
		}
	}

	public float progress
	{
		get
		{
			if (httpserver != null)
			{
				return httpserver.progress;
			}
			return 0f;
		}
	}

	public bool isDone
	{
		get
		{
			if (httpserver != null)
			{
				return httpserver.isDone;
			}
			return false;
		}
	}

	public byte[] bytes
	{
		get
		{
			if (httpserver != null)
			{
				return httpserver.bytes;
			}
			return null;
		}
	}

	public string data
	{
		get
		{
			if (httpserver != null)
			{
				return httpserver.text;
			}
			return null;
		}
	}

	public string error
	{
		get
		{
			if (httpserver != null)
			{
				return httpserver.error;
			}
			return null;
		}
	}

	public Texture2D texture
	{
		get
		{
			if (httpserver != null)
			{
				return httpserver.texture;
			}
			return null;
		}
	}

	public AssetBundle assetBundle
	{
		get
		{
			if (httpserver != null)
			{
				return httpserver.assetBundle;
			}
			return null;
		}
	}

	public AudioClip oggVorbis
	{
		get
		{
			if (httpserver != null)
			{
				return httpserver.audioClip;
			}
			return null;
		}
	}

	public www(string url)
	{
		httpserver = new WWW(url);
	}

	public void Dispose()
	{
		if (httpserver != null)
		{
			httpserver.Dispose();
		}
	}
}
