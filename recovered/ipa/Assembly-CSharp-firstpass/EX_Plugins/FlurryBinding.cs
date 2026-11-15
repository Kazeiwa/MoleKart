using System.Runtime.InteropServices;
using UnityEngine;

namespace EX_Plugins;

public class FlurryBinding : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void _reportAppRun();

	[DllImport("__Internal")]
	private static extern void _reportBuyCharacter(string characterName);

	[DllImport("__Internal")]
	private static extern void _reportBuyTrack(string trackName);

	[DllImport("__Internal")]
	private static extern void _reportUnlockCharacter(string characterName);

	[DllImport("__Internal")]
	private static extern void _reportUnlockTrack(string trackName);

	[DllImport("__Internal")]
	private static extern void _reportPlayOnlineGame();

	[DllImport("__Internal")]
	private static extern void _reportGetMedal(string trackName, string mode, int difficultyLevel);

	public static void ReportAppRun()
	{
	}

	public static void ReportBuyCharacter(string characterName)
	{
	}

	public static void ReportBuyTrack(string trackName)
	{
	}

	public static void ReportUnlockCharacter(string characterName)
	{
	}

	public static void ReportUnlockTrack(string trackName)
	{
	}

	public static void ReportPlayOnlineGame()
	{
	}

	public static void ReportGetMedal(string trackName, string mode, int difficultyLevel)
	{
	}
}
