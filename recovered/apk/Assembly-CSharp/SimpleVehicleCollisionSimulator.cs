using UnityEngine;

[MessageClass(typeof(SimpleVehicleCollisionSimulator))]
public class SimpleVehicleCollisionSimulator : VehicleCollisionSimulator
{
	public SimpleVehicleCollisionSimulator(btVehicleBase car)
		: base(car)
	{
	}

	public override void Update(float fTickTime)
	{
	}

	public override void OnDrawGizmos()
	{
		Vector3 position = m_car.GetPosition();
		Gizmos.color = Color.gray;
		Gizmos.DrawRay(position, m_vMoveDir);
	}
}
