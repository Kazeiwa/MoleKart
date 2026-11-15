using System.Collections.Generic;

public class ShopDataManager : TSingleton<ShopDataManager>
{
	private const float fDefaultMoney = 0.99f;

	private const float fDefaultMoneyRMB = 6f;

	private const string strPurchasedGoodsFile = "PurchasedGoods.xml";

	private List<string> arrPurchasedGoods = new List<string>();

	private Dictionary<string, float> dicProductsPrices = new Dictionary<string, float>();

	public void Init()
	{
		AddNewProductPrice("com.taomee.molekart.HairHead", 0.99f);
		AddNewProductPrice("com.taomee.molekart.RK", 0.99f);
		AddNewProductPrice("com.taomee.molekart.sky", 0.99f);
		AddNewProductPrice("com.taomee.molekart.ice", 0.99f);
		AddNewProductPrice("com.taomee.molekart.garden", 0.99f);
	}

	public void AddNewProductPrice(string product_id, float fV)
	{
		if (dicProductsPrices.ContainsKey(product_id))
		{
			dicProductsPrices[product_id] = fV;
		}
		else
		{
			dicProductsPrices.Add(product_id, fV);
		}
	}

	public int GetTotalItemsNumber()
	{
		return dicProductsPrices.Count;
	}

	public void FetchNewPriceFromItunes()
	{
	}

	private float LocalizeMoney(float fDollars)
	{
		string currentSystemLanguage = AppMain.Instance.GetCurrentSystemLanguage();
		if (currentSystemLanguage.Equals("zh-Hans") || currentSystemLanguage.Equals(string.Empty))
		{
			if (fDollars == 0.99f)
			{
				return 6f;
			}
			return (int)((double)fDollars * 6.5);
		}
		return fDollars;
	}

	public string GetLocalizedPriceString(string strProductName)
	{
		return string.Empty;
	}

	private List<string> QueryPurchasedGoods()
	{
		return arrPurchasedGoods;
	}
}
