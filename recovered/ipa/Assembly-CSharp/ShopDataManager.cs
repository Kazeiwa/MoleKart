using System.Collections.Generic;

public class ShopDataManager : TSingleton<ShopDataManager>
{
	private const float fDefaultMoney = 1.99f;

	private const float fDefaultMoneyRMB = 12f;

	private const string strPurchasedGoodsFile = "PurchasedGoods.xml";

	private List<string> arrPurchasedGoods;

	private Dictionary<string, float> dicProductsPrices;

	public void Init()
	{
	}

	public void AddNewProductPrice(string product_id, float fV)
	{
	}

	public int GetTotalItemsNumber()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void FetchNewPriceFromItunes()
	{
	}

	private float LocalizeMoney(float fDollars)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public string GetLocalizedPriceString(string strProductName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private List<string> QueryPurchasedGoods()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
