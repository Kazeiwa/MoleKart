using System.Runtime.InteropServices;

public static class iOSStoreKit
{
	public static void Buy(string product_id)
	{
	}

	public static void Init()
	{
	}

	public static bool CanMakePayments()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void RestorePurchased()
	{
	}

	public static void Dispose()
	{
	}

	public static void FetchPriceFromItunes()
	{
	}

	[DllImport("__Internal")]
	private static extern void _Buy(string product_id);

	[DllImport("__Internal")]
	private static extern bool _CanMakePayments();

	[DllImport("__Internal")]
	private static extern bool _InitIAP();

	[DllImport("__Internal")]
	private static extern void _DisposeIAP();

	[DllImport("__Internal")]
	private static extern void _RestorePurchased();

	[DllImport("__Internal")]
	private static extern void _FetchPriceFromItunes();
}
