using System.Runtime.InteropServices;
using UnityEngine;

namespace EX_Plugins;

public class MKGameCenterBinding : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern bool _isGameCenterAPIAvailable();

	[DllImport("__Internal")]
	private static extern void _authenticateLocalPlayer(bool showAlert);

	[DllImport("__Internal")]
	private static extern void _reportScore(long score, string category);

	[DllImport("__Internal")]
	private static extern void _showLeaderboard();

	public static bool IsGameCenterAPIAvailable()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void AuthenticateLocalPlayer(bool showAlert)
	{
	}

	public static void ReportScore(long score, string category)
	{
	}

	public static void ShowLeaderboard()
	{
	}
}
