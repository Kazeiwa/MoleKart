public class EarthQuake
{
	private static EarthQuake instance;

	public static EarthQuake Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new EarthQuake();
			}
			return instance;
		}
	}

	public void Quake(Car car)
	{
		if (null != car)
		{
			car.QuakeCar();
		}
	}
}
