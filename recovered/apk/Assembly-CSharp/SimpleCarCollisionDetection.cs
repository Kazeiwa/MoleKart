public class SimpleCarCollisionDetection : CarCollisionDetection
{
	public override void Init(BodyComponent body, CollisionCallback callback, btVehicleBase car)
	{
		car.bRemoteCollider = true;
	}

	public override void Unint()
	{
	}

	public override void UpdatePhysx(float fTicktime)
	{
	}
}
