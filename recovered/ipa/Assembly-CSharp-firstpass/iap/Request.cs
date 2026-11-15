namespace iap;

public class Request
{
	public enum Status
	{
		CANCELLED,
		STARTED,
		SUCCEED,
		FAILED
	}

	private PurchaseCallback callback_;

	private string product_id_;

	public Request(string product_id, PurchaseCallback callback)
	{
	}

	public string ProductId()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void NotifyPurchaser(Status status)
	{
	}
}
