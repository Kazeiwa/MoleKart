using UnityEngine;

[MessageClass(typeof(VehicleCollisionSimulator))]
public class VehicleCollisionSimulator : ISimulator
{
	protected btVehicleBase m_car;

	protected Vector3 m_vMoveDir;

	protected float m_fStartTime;

	private float K;

	private float V;

	private float a;

	private Vector3 m_vVec;

	public VehicleCollisionSimulator(btVehicleBase car)
	{
	}

	public void Begin(float fDistance, Vector3 Dir, VehicleCollisionSide eside, float fTime)
	{
	}

	public override void Update(float fTickTime)
	{
	}

	[MessageFun(typeof(VehicleCollisionSimulator))]
	public override void processMsg(int msg, ref ABMessageData data)
	{
	}

	public override void OnDrawGizmos()
	{
	}
}
