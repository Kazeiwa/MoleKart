using UnityEngine;

public class BounceSimulator : ISimulator
{
	private btVehicleBase m_car;

	private float m_fDistanceToMove;

	private Vector3 m_vMoveDir;

	private Vector3 m_vVec;

	private float K;

	private float a;

	public BounceSimulator(btVehicleBase car)
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
