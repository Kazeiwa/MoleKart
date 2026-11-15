using System.Runtime.InteropServices;
using UnityEngine;

namespace EX_Plugins;

public class MKLeaderboardBinding : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void _createRankView(int x, int y, int width, int height);

	[DllImport("__Internal")]
	private static extern void _removeRankView();

	[DllImport("__Internal")]
	private static extern void _responseRankListData(string data);

	[DllImport("__Internal")]
	private static extern void _responseMyRankData(string data);

	[DllImport("__Internal")]
	private static extern void _clearAllRankList();

	[DllImport("__Internal")]
	private static extern void _beginUpdateRankList();

	[DllImport("__Internal")]
	private static extern void _endUpdateRankList();

	[DllImport("__Internal")]
	private static extern void _purgeDefaultRankController();

	public static void SendMyScoreToView(string info)
	{
	}

	public static void SendScoreListToView(string info)
	{
	}

	public static void CreateLeaderBoardTableView()
	{
	}

	public static void RemoveLeaderboardTalbeView()
	{
	}

	public static void ClearTableView()
	{
	}
}
