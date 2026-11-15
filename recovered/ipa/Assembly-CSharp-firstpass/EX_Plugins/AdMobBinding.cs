using System.Runtime.InteropServices;
using UnityEngine;

namespace EX_Plugins;

public class AdMobBinding : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void _reportAppOpenToAdMob();

	public static void ReportAppOpenToAdMob()
	{
	}
}
