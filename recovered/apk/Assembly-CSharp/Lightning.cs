public class Lightning
{
	private static Lightning instance;

	public static Lightning Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new Lightning();
			}
			return instance;
		}
	}

	public void Flashing(Car car)
	{
		if (!(null == car))
		{
			car.HitByLightning();
		}
	}
}
