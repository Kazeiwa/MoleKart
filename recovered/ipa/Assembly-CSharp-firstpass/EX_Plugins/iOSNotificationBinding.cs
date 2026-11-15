using System.Runtime.InteropServices;
using UnityEngine;

namespace EX_Plugins;

public class iOSNotificationBinding : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void _setCanShowNotification();

	[DllImport("__Internal")]
	private static extern void _setCannotShowNotification();

	[DllImport("__Internal")]
	private static extern void _getUUID();

	public static void SetCanShowNotification()
	{
	}

	public static void SetCannotShowNotification()
	{
	}

	public static string GetUUID()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
