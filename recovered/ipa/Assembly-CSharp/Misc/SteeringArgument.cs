namespace Misc;

public struct SteeringArgument
{
	public enum SteeringDirection
	{
		Forth,
		Still,
		Back
	}

	public float Rate;

	public SteeringDirection Direction;

	public bool Braking;

	public static SteeringArgument Default
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static SteeringArgument Forth
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static SteeringArgument Back
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static SteeringArgument Brake
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static SteeringArgument NoOutputForce
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public SteeringArgument(float rate, SteeringDirection direction, bool braking)
	{
	}
}
