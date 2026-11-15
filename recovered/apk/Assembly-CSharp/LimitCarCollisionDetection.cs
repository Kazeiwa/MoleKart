public class LimitCarCollisionDetection : CarCollisionDetection
{
	public override void Init(BodyComponent body, CollisionCallback callback, btVehicleBase car)
	{
		m_body = body;
		m_car = car;
		kLayer = CollisionLayer.Instance;
		base.callback = callback;
		car.bRemoteCollider = false;
		CollisionCenter.Instance.Add(this);
	}

	public override void Unint()
	{
		CollisionCenter.Instance.Remove(this);
	}

	public override void UpdatePhysx(float fTicktime)
	{
	}
}
