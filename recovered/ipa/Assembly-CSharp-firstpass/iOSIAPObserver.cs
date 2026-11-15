using UnityEngine;

public class iOSIAPObserver : MonoBehaviour
{
	private static iOSIAPObserver instance;

	public static iOSIAPObserver Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void Attach()
	{
	}

	public void Detach()
	{
	}

	private void PurchaseCompleted(string product_id)
	{
	}

	private void PurchaseRestored(string product_id)
	{
	}

	private void PurchaseRestoredFail(string product_id)
	{
	}

	private void PurchaseCancelled(string product_id)
	{
	}

	private void ProductInfoRetrieved(string product_info)
	{
	}

	private void ProductsReady(string not_used_yet)
	{
	}
}
