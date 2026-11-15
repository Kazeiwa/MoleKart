using System.Runtime.InteropServices;
using UnityEngine;

namespace EX_Plugins;

public class MKUpgradeBinding : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void _checkUpgrade();

	public static void CheckUpgrade()
	{
	}
}
