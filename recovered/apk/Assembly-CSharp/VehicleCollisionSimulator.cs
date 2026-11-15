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
		m_car = car;
		MessageFun<ABCmdTarget>(typeof(VehicleCollisionSimulator), processMsg);
	}

	public void Begin(float fDistance, Vector3 Dir, VehicleCollisionSide eside, float fTime)
	{
		if (fTime <= 0f)
		{
			fTime = 0.3f;
		}
		m_vMoveDir = Dir;
		m_fStartTime = fTime;
		K = 2f * fDistance / m_fStartTime;
		a = (0f - K) / m_fStartTime;
		m_vVec = m_car.GetVecGlobal();
		V = m_vVec.magnitude;
	}

	public override void Update(float fTickTime)
	{
		if (!HeightMapManager.Instance.bValidate)
		{
			return;
		}
		NiLog.Log("VehicleCollisionSimulator: Update");
		m_fStartTime -= fTickTime;
		if (m_fStartTime < 0f)
		{
			m_car.ChangeVehicleRunningMode(RUNNING_MDOE.RM_GROUNDRUNING, null);
			m_vVec = m_vVec.normalized * V;
			m_car.SetVecGlobal(m_vVec);
			return;
		}
		K += a * fTickTime;
		Vector3 position = m_car.GetPosition();
		float num = m_car.vTotalForce.magnitude / 1000f;
		if (Vector3.Dot(m_car.GetForwardGlobal(), m_car.vTotalForce) < 0f)
		{
			num = 0f - num;
		}
		V += num * fTickTime;
		if (Physics.CheckSphere(position, m_car.GetBody().BoundRadius, 512))
		{
			m_vMoveDir = Vector3.zero;
		}
		position += K * fTickTime * m_vMoveDir;
		m_car.SetVecGlobal(m_vVec);
		m_car.MoveToGround(position);
	}

	[MessageFun(typeof(VehicleCollisionSimulator))]
	public override void processMsg(int msg, ref ABMessageData data)
	{
		if (msg == 0)
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
