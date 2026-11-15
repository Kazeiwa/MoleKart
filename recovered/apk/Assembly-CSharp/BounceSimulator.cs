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
		m_car = car;
	}

	public void Begin(float fV, Vector3 Dir, VehicleCollisionSide eside)
	{
		m_vMoveDir = Dir;
		m_vVec = m_car.GetVecGlobal();
		K = fV;
		a = (0f - fV) / 3f;
	}

	public override void Update(float fTickTime)
	{
		if (HeightMapManager.Instance.bValidate)
		{
			NiLog.Log("BounceSimulator: Update");
			K += a * fTickTime;
			Vector3 position = m_car.GetPosition();
			position += K * fTickTime * m_vMoveDir + fTickTime * m_vVec + 9.8f * Vector3.down * fTickTime;
			Vector3 vector = position;
			Vector3 vector2 = m_car.GetwheelsPosition(0);
			Vector3 vector3 = m_car.GetwheelsPosition(1);
			Vector3 vector4 = m_car.GetwheelsPosition(2);
			Vector3 vector5 = m_car.GetwheelsPosition(3);
			Vector3 vector6 = -m_car.GetUpGlobal();
			Vector3 vector7 = -vector6 * 10f;
			CollisionLayer.Instance.RaycastDistancetoGround(vector2 + vector7, vector6, 30f, out var _, out var vHitPoint);
			CollisionLayer.Instance.RaycastDistancetoGround(vector3 + vector7, vector6, 30f, out var _, out var vHitPoint2);
			CollisionLayer.Instance.RaycastDistancetoGround(vector4 + vector7, vector6, 30f, out var _, out var vHitPoint3);
			CollisionLayer.Instance.RaycastDistancetoGround(vector5 + vector7, vector6, 30f, out var _, out var vHitPoint4);
			float num = (vector2 - vHitPoint).magnitude - m_car.fFrontTireRadius;
			float b = (vector3 - vHitPoint2).magnitude - m_car.fFrontTireRadius;
			float b2 = (vector4 - vHitPoint3).magnitude - m_car.fRearTireRadius;
			float b3 = (vector5 - vHitPoint4).magnitude - m_car.fRearTireRadius;
			float num2 = Mathf.Min(num, b);
			num2 = Mathf.Min(num2, b2);
			num2 = Mathf.Min(num2, b3);
			if (num2 < 0f)
			{
				vector += -1f * vector6;
				m_car.SetPosition(vector);
				m_car.MoveToGround(vector);
				m_car.ChangeVehicleRunningMode(RUNNING_MDOE.RM_GROUNDRUNING, null);
				m_car.SetVecGlobal(m_vVec);
			}
			else
			{
				m_car.SetPosition(position);
			}
		}
	}

	public override void OnDrawGizmos()
	{
		Vector3 position = m_car.GetPosition();
		Gizmos.color = Color.gray;
		Gizmos.DrawRay(position, m_vMoveDir);
	}
}
