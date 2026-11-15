using System.Runtime.InteropServices;
using UnityEngine;

namespace EX_Plugins;

public class iOSLocalizationBinder : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern string _localizeString(string key, string comment);

	[DllImport("__Internal")]
	private static extern string _languageIdentifier();

	[DllImport("__Internal")]
	private static extern string _platformIdentifier();

	[DllImport("__Internal")]
	private static extern void _hardwareIdentifier();

	public static string LocalizedString(string key, string comment)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static string GetLanguageIdentifier()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static string GetPlatformIdentifier()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void CheckHardware()
	{
	}
}
