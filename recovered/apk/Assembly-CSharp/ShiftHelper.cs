using UnityEngine;

public class ShiftHelper : MonoBehaviour
{
	private Vector3 m_vDir = Vector3.zero;

	private float m_fLifeTime;

	private float m_fOffsetDistance = 0.4f;

	private float m_fAccelate;

	private float m_fStartVelocity;

	private bool m_bStop = true;

	private btVehicleBase m_other;

	private btVehicleBase m_mine;

	private int m_Count0;

	private float GetCollisionLifeTime()
	{
		return 0.1f;
	}

	public void AvoidCollision(btVehicleBase other, Vector3 vTargetPosition)
	{
		if (!m_bStop)
		{
			m_fLifeTime = GetCollisionLifeTime();
			m_fStartVelocity = m_fAccelate * m_fLifeTime;
			if (other != m_other)
			{
				m_Count0++;
			}
		}
		else
		{
			m_Count0++;
			ClearCollision();
			m_other = other;
			m_vDir = vTargetPosition - m_mine.GetPosition();
			m_vDir.Normalize();
			m_bStop = false;
		}
	}

	public void ClearCollision()
	{
		m_bStop = true;
		m_fStartVelocity = 0f;
		m_other = null;
		m_fLifeTime = GetCollisionLifeTime();
		m_fStartVelocity = m_fAccelate * m_fLifeTime;
		m_fAccelate = 2f * m_fOffsetDistance / (m_fLifeTime * m_fLifeTime);
	}

	public void SetTarget(btVehicleBase car, CarCollisionDetection kCCDet)
	{
		m_mine = car;
	}

	private void Update()
	{
		if (!m_bStop)
		{
			Vector3 vecGlobal = m_mine.GetVecGlobal();
			m_fLifeTime -= Time.deltaTime;
			float num = ((!(m_fLifeTime > 0f)) ? m_fLifeTime : Time.deltaTime);
			float num2 = m_fStartVelocity * num - m_fAccelate * num * num / 2f;
			m_fStartVelocity -= m_fAccelate * num;
			Vector3 position = m_mine.GetPosition() + m_vDir * num2;
			m_mine.SetPosition(position);
			m_mine.SetVecGlobal(vecGlobal);
			if (m_fStartVelocity < 0f || m_fLifeTime <= 0f)
			{
				ClearCollision();
			}
		}
	}
}
