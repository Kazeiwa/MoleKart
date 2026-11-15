using UnityEngine;

public class LandingSimulator : ISimulator
{
	private float m_fCurTime;

	private float m_fMaxDistance = 0.1f;

	private Vehicle m_car;

	private ChassisSimulator m_chassis;

	private Suspension m_suspension;

	private float[] m_afWheelOffset = new float[4];

	private Transform[] m_aWheels = new Transform[4];

	private bool m_bActive;

	public bool IsActive => m_bActive;

	public LandingSimulator(Vehicle car)
	{
		m_car = car;
		m_chassis = m_car.chassis;
		m_suspension = car.m_suspension;
		for (int i = 0; i < 4; i++)
		{
			m_afWheelOffset[i] = m_chassis.Nxwheels[i].WheelOffset;
			m_aWheels[i] = m_chassis.Nxwheels[i].ColliderTransform;
		}
	}

	public void Start()
	{
		if (!m_bActive)
		{
			m_bActive = true;
			m_fCurTime = 0f;
			for (int i = 0; i < 4; i++)
			{
				m_afWheelOffset[i] = m_chassis.Nxwheels[i].WheelOffset;
			}
		}
	}

	public void Stop()
	{
		m_bActive = false;
	}

	public override void Update(float fTickTime)
	{
		if (!m_bActive)
		{
			return;
		}
		m_fCurTime += fTickTime;
		Vector3 vector = new Vector3(0f, -1f, 0f);
		if (m_fCurTime <= 0.05f)
		{
			for (int i = 0; i < 4; i++)
			{
				m_afWheelOffset[i] -= 3f * m_fCurTime;
				float num = (0.1f - m_afWheelOffset[i]) / 8f;
				if (i == 0 || i == 3)
				{
					num = 0f - num;
				}
				if (i == 0 || i == 1)
				{
					num /= 2f;
				}
				m_chassis.Nxwheels[i].OffsetRoot(m_afWheelOffset[i], num);
				float num2 = Mathf.Clamp(m_afWheelOffset[i], 0f, m_fMaxDistance);
				Vector3 vector2 = num2 * vector;
				m_aWheels[i].localPosition = m_chassis.Nxwheels[i].LocalOrignPosition + vector2;
			}
		}
		if (m_fCurTime > 0.1f && m_fCurTime <= 0.4f)
		{
			for (int j = 0; j < 4; j++)
			{
				m_afWheelOffset[j] += 0.2f * (m_fCurTime - 0.1f);
				m_afWheelOffset[j] = Mathf.Clamp(m_afWheelOffset[j], 0f, m_fMaxDistance);
				Vector3 vector3 = m_afWheelOffset[j] * vector;
				m_aWheels[j].localPosition = m_chassis.Nxwheels[j].LocalOrignPosition + vector3;
				float num3 = (0.1f - m_afWheelOffset[j]) / 8f;
				if (j == 0 || j == 3)
				{
					num3 = 0f - num3;
				}
				if (j == 0 || j == 1)
				{
					num3 /= 2f;
				}
				m_chassis.Nxwheels[j].OffsetRoot(m_afWheelOffset[j], num3);
			}
		}
		if (m_fCurTime > 0.25f)
		{
			m_suspension.EndLanding();
		}
	}
}
