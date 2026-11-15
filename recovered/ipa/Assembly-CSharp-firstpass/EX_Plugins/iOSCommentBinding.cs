using System.Runtime.InteropServices;
using UnityEngine;

namespace EX_Plugins;

public class iOSCommentBinding : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void _showCommentAlert();

	public static void ShowCommentAlert()
	{
	}
}
