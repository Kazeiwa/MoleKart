using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LoadHelp
{
	private static List<www> proList;

	private static bool enable;

	private static int process;

	public static void LoadObject<T>(MonoBehaviour thread, string url, LoadDelegate<T> asset) where T : UnityEngine.Object
	{
	}

	public static void DoGUI()
	{
	}

	public static void LoadBytes(MonoBehaviour thread, string url, Action<string> GetString, Action<byte[]> GetBytes)
	{
	}

	[DebuggerHidden]
	private static IEnumerator _LoadBytes(string url, Action<string> GetString, Action<byte[]> GetBytes)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DebuggerHidden]
	public static IEnumerator _LoadObject<T>(string url, LoadDelegate<T> asset) where T : UnityEngine.Object
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
