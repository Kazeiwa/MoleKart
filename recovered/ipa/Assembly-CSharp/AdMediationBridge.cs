using System.Runtime.InteropServices;

public class AdMediationBridge
{
	public static void ShowAd(int x, int y)
	{
	}

	public static void HideAd()
	{
	}

	[DllImport("__Internal")]
	private static extern void _ShowAd(int x, int y);

	[DllImport("__Internal")]
	private static extern void _HideAd();
}
