using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace iap;

public class Purchase
{
	public enum Status
	{
		NO_ERROR,
		NETWORK_NOT_REACHABLE,
		PURCHASE_FORBIDDEN,
		PURCHASE_IN_PREPARATION,
		PURCHASE_PENDING,
		PURCHASE_PRODUCT_UNKNOWN
	}

	public class Product
	{
		public string id_
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public string localized_title_
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public string localized_price_
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public Product(string title, string price, string id)
		{
		}

		public static Product Factory(string product_info)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public delegate void ProductInventoryCallback(Dictionary<string, Product> products);

	private static Purchase sharedPurchase_;

	private Request request_;

	private bool request_pending_;

	private bool network_reachable_;

	private bool products_ready_;

	private ProductInventoryCallback inventory_observer_;

	private Dictionary<string, Product> product_inventory_;

	public static Purchase sharedInstance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void DisposePurchase()
	{
	}

	public Status FetchProductInventory(ProductInventoryCallback callback)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Status DoPurchase(Request request)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Product RegisterProduct(string product_info)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ProductsReady(string not_used_yet)
	{
	}

	public void UpdatePurchaseState(string product_id, Request.Status status)
	{
	}

	public void NetworkStatusUpdate(object sender, EventArgs arg)
	{
	}

	public void FetchPriceFromItunes()
	{
	}

	public string GetPrice(string id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private Status CheckPurchaseStatus()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void PurchaseCompleted()
	{
	}
}
