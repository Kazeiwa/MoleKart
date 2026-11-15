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

	public static SteeringArgument Default => new SteeringArgument(0f, SteeringDirection.Still, braking: false);

	public static SteeringArgument Forth => new SteeringArgument(0f, SteeringDirection.Forth, braking: false);

	public static SteeringArgument Back => new SteeringArgument(0f, SteeringDirection.Back, braking: false);

	public static SteeringArgument Brake => new SteeringArgument(0f, SteeringDirection.Still, braking: true);

	public static SteeringArgument NoOutputForce => new SteeringArgument(0f, SteeringDirection.Still, braking: false);

	public SteeringArgument(float rate, SteeringDirection direction, bool braking)
	{
		Rate = rate;
		Direction = direction;
		Braking = braking;
	}
}
