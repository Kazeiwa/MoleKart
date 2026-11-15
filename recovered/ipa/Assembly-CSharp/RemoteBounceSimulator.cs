using UnityEngine;

public class RemoteBounceSimulator : ISimulator
{
	private btVehicleBase m_car;

	private float m_fDistanceToMove;

	private Vector3 m_vMoveDir;

	public RemoteBounceSimulator(btVehicleBase car)
	{
	}

	public void Begin(float fV, Vector3 Dir, VehicleCollisionSide eside)
	{
	}

	public override void Update(float fTickTime)
	{
	}

	public override void OnDrawGizmos()
	{
	}
}
