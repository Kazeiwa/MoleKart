public class BigMushroom
{
	private static BigMushroom instance;

	public static BigMushroom Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new BigMushroom();
			}
			return instance;
		}
	}

	public void UseBigMushroom(Car car)
	{
		if (null != car)
		{
			car.UseBigMushroom();
		}
	}
}
