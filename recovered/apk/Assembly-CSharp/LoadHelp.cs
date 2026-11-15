using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHelp
{
	private static List<www> proList = new List<www>();

	private static bool enable = false;

	private static int process = 0;

	public static void LoadObject<T>(MonoBehaviour thread, string url, LoadDelegate<T> asset) where T : UnityEngine.Object
	{
		thread.StartCoroutine(_LoadObject(url, asset));
	}

	public static void DoGUI()
	{
		enable = true;
		if (proList.Count <= 0)
		{
			return;
		}
		float num = 0f;
		www[] array = proList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Used)
			{
				proList.Remove(array[i]);
				array[i].Dispose();
				array[i] = null;
			}
			else
			{
				num += array[i].progress;
			}
		}
		process = (int)(num * 100f) / array.Length;
		GUI.Box(new Rect((Screen.width - 150) / 2, Screen.height - 70, 150f, 37f), "Loading...\n" + process + "%");
	}

	public static void LoadBytes(MonoBehaviour thread, string url, Action<string> GetString, Action<byte[]> GetBytes)
	{
		thread.StartCoroutine(_LoadBytes(url, GetString, GetBytes));
	}

	private static IEnumerator _LoadBytes(string url, Action<string> GetString, Action<byte[]> GetBytes)
	{
		www rerquest = new www(url);
		if (enable)
		{
			proList.Add(rerquest);
		}
		yield return rerquest;
		try
		{
			if (rerquest.isDone)
			{
				GetBytes?.Invoke(rerquest.bytes);
				GetString?.Invoke(rerquest.data);
			}
		}
		catch (Exception ex)
		{
			Exception e = ex;
			Debug.LogError(e.Message);
		}
		finally
		{
			rerquest.Used = true;
			if (!enable)
			{
				rerquest.Dispose();
				rerquest = null;
			}
		}
	}

	public static IEnumerator _LoadObject<T>(string url, LoadDelegate<T> asset) where T : UnityEngine.Object
	{
		www rerquest = new www(url);
		if (enable)
		{
			proList.Add(rerquest);
		}
		yield return rerquest;
		try
		{
			if (!rerquest.isDone || !string.IsNullOrEmpty(rerquest.error))
			{
				yield break;
			}
			if (typeof(T) == typeof(Texture2D))
			{
				Texture2D tex = rerquest.texture;
				if (tex != null)
				{
					asset?.Invoke(tex as T);
				}
			}
			if (typeof(T) == typeof(AssetBundle))
			{
				asset?.Invoke(rerquest.assetBundle as T);
			}
			if (typeof(T) == typeof(GameObject))
			{
				GameObject mod = rerquest.assetBundle.mainAsset as GameObject;
				if (mod != null)
				{
					asset?.Invoke(mod as T);
				}
			}
			if (typeof(T) == typeof(UnityEngine.Object))
			{
				UnityEngine.Object mod2 = rerquest.assetBundle.mainAsset;
				if (mod2 != null)
				{
					asset?.Invoke(mod2 as T);
				}
			}
			if (typeof(T) == typeof(AudioClip))
			{
				AudioClip aud = rerquest.oggVorbis;
				if (aud != null)
				{
					asset?.Invoke(aud as T);
				}
			}
		}
		catch (Exception ex)
		{
			Exception e = ex;
			Debug.LogError(e.Message);
		}
		finally
		{
			rerquest.Used = true;
			if (!enable)
			{
				rerquest.Dispose();
				rerquest = null;
			}
		}
	}
}
