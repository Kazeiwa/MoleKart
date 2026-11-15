public class CuttleFish
{
	private static CuttleFish instance;

	public static CuttleFish Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new CuttleFish();
			}
			return instance;
		}
	}

	public void HitByCuttleFish(Car car)
	{
		if (null != car)
		{
			car.HitByCuttleFish();
		}
	}
}
