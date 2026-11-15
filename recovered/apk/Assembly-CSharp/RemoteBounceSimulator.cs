using UnityEngine;

public class RemoteBounceSimulator : ISimulator
{
	private btVehicleBase m_car;

	private float m_fDistanceToMove;

	private Vector3 m_vMoveDir;

	public RemoteBounceSimulator(btVehicleBase car)
	{
		m_car = car;
	}

	public void Begin(float fV, Vector3 Dir, VehicleCollisionSide eside)
	{
		m_vMoveDir = Dir;
	}

	public override void Update(float fTickTime)
	{
		if (HeightMapManager.Instance.bValidate)
		{
		}
	}

	public override void OnDrawGizmos()
	{
		Vector3 position = m_car.GetPosition();
		Gizmos.color = Color.gray;
		Gizmos.DrawRay(position, m_vMoveDir);
	}
}
