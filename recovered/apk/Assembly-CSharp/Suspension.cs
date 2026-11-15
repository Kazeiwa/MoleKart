using System;
using System.Collections.Generic;
using UnityEngine;

public class Suspension : MonoBehaviour
{
	private class Shock
	{
		private float m_fTD;

		private float k = 25000f;

		private float m_fLastDistance;

		private float m_fLastForce;

		public Shock(float fLimitD)
		{
			m_fTD = fLimitD;
		}

		public float ApplyForce(float fForce, float fOffset)
		{
			float value = m_fTD - fForce / k;
			if (fOffset < 0f)
			{
				fOffset = 0f;
			}
			fOffset = ((!(fOffset > m_fTD)) ? fOffset : m_fTD);
			m_fLastDistance = Mathf.Clamp(value, fOffset, m_fTD);
			m_fLastForce = (m_fTD - m_fLastDistance) * k;
			return m_fLastForce;
		}

		public float GetCurrentLength()
		{
			return m_fLastDistance;
		}
	}

	public ENMSHAKEDIR m_curShakeDir;

	private List<ISimulator> m_lsSimulators;

	private JumpSimulator m_kJump;

	private LandingSimulator m_kLanding;

	private ChassisSimulator m_chassis;

	private Vehicle m_car;

	private float[] m_afWheelOffset = new float[4];

	private float m_fShockDistance = 0.1f;

	public Transform[] m_aWheels = new Transform[4];

	private float m_fMass;

	private float[] m_afWheelForce = new float[4];

	private Shock[] m_kShock = new Shock[4];

	private bool m_bLock;

	private Vector3[] m_vCompressForce = new Vector3[4];

	private int m_WheelCount;

	private float m_fSpeedPulseCooldown;

	public void Init(Vehicle car, float fMass)
	{
		m_car = car;
		m_fMass = 19600f;
		m_chassis = car.chassis;
		m_lsSimulators = new List<ISimulator>();
		for (int i = 0; i < 4; i++)
		{
			m_afWheelOffset[i] = 0f;
			m_aWheels[i] = m_chassis.Nxwheels[i].ColliderTransform;
			m_afWheelForce[i] = m_fMass / 4f;
			m_kShock[i] = new Shock(m_fShockDistance);
		}
		m_curShakeDir = ENMSHAKEDIR.ESD_NONE;
		m_kJump = new JumpSimulator(car);
		AddSimulator(m_kJump);
		m_kLanding = new LandingSimulator(car);
		AddSimulator(m_kLanding);
	}

	public void Lock(bool bCompress)
	{
		m_bLock = true;
		if (bCompress)
		{
			for (int i = 0; i < 4; i++)
			{
				m_aWheels[i].localPosition = m_chassis.Nxwheels[i].LocalOrignPosition;
			}
		}
		RestSpeedPulseCooldown();
	}

	public void UnLock()
	{
		RestSpeedPulseCooldown();
		m_bLock = false;
	}

	public void Relax()
	{
		Vector3 vector = new Vector3(0f, -1f, 0f);
		for (int i = 0; i < 4; i++)
		{
			Vector3 vector2 = m_fShockDistance * vector;
			m_aWheels[i].localPosition = m_chassis.Nxwheels[i].LocalOrignPosition + vector2;
			m_chassis.Nxwheels[i].OffsetRoot(m_fShockDistance, 0f);
		}
	}

	public void AdjustWheel()
	{
		Vector3 vector = new Vector3(0f, -1f, 0f);
		for (int i = 0; i < 4; i++)
		{
			if (m_chassis.Nxwheels[i].bGrounded)
			{
				float num = m_aWheels[i].localPosition.y - m_chassis.Nxwheels[i].LocalOrignPosition.y;
				if (num != 0f)
				{
					if (Math.Abs(num) < 0.001f)
					{
						m_aWheels[i].localPosition = m_chassis.Nxwheels[i].LocalOrignPosition;
						continue;
					}
					Vector3 vector2 = new Vector3(0f, (float)Math.Sign(num) * -0.001f, 0f);
					m_aWheels[i].localPosition = m_aWheels[i].localPosition + vector2;
				}
			}
			else
			{
				float num2 = m_aWheels[i].localPosition.y - m_chassis.Nxwheels[i].LocalOrignPosition.y;
				if (num2 > -0.1f)
				{
					Vector3 vector3 = new Vector3(0f, -0.001f, 0f);
					m_aWheels[i].localPosition = m_aWheels[i].localPosition + vector3;
				}
			}
		}
	}

	public void AddSimulator(ISimulator pkSim)
	{
		if (!m_lsSimulators.Contains(pkSim))
		{
			m_lsSimulators.Add(pkSim);
		}
	}

	public void UpdateSimulator(float fTickTime)
	{
		foreach (ISimulator lsSimulator in m_lsSimulators)
		{
			lsSimulator.Update(fTickTime);
		}
	}

	public void RestSpeedPulseCooldown()
	{
		m_fSpeedPulseCooldown = 1f;
	}

	public void Simulate(float fTickTime)
	{
		if (m_car.IsKinematic())
		{
			RestSpeedPulseCooldown();
			return;
		}
		if (m_bLock)
		{
			RestSpeedPulseCooldown();
			return;
		}
		Vector3 vector = Vector3.down;
		bool flag = true;
		int num = 0;
		for (int i = 0; i < 4; i++)
		{
			if (m_chassis.Nxwheels[i].bGrounded)
			{
				flag = false;
				vector = Vector3.down;
				num++;
			}
		}
		bool flag2 = false;
		float num2 = 0f;
		for (int j = 0; j < 4; j++)
		{
			Vector3 position = m_chassis.Nxwheels[j].GetPosition();
			float num3 = HeightMapManager.Instance.Height(position.x, position.z);
			num2 = Mathf.Min(num3, num2);
			if (num3 > position.y && !m_chassis.Nxwheels[j].bGrounded)
			{
				flag2 = true;
			}
		}
		if (num2 < -500f)
		{
			vector = Vector3.zero;
		}
		if (flag)
		{
			ref Vector3 reference = ref m_vCompressForce[0];
			reference = m_fMass * vector * 1f;
			m_car.AddForceGlobal(m_vCompressForce[0], ENMFORCETYPE.EFT_NORMAL, fTickTime);
		}
		else
		{
			bool flag3 = false;
			if (num == 2)
			{
				if (m_chassis.Nxwheels[0].bGrounded && m_chassis.Nxwheels[2].bGrounded)
				{
					flag3 = true;
				}
				else if (m_chassis.Nxwheels[1].bGrounded && m_chassis.Nxwheels[3].bGrounded)
				{
					flag3 = true;
				}
			}
			if (!flag3 && num != 3)
			{
				for (int k = 0; k < 4; k++)
				{
					if (!m_chassis.Nxwheels[k].bGrounded)
					{
						ref Vector3 reference2 = ref m_vCompressForce[k];
						reference2 = m_fMass * vector * 0.25f;
						m_car.AddForceAtPointGlobal(m_vCompressForce[k], m_aWheels[k].position, fTickTime);
					}
				}
			}
		}
		if (m_car.GetLockMovement())
		{
			return;
		}
		if (m_fSpeedPulseCooldown > 0f)
		{
			m_fSpeedPulseCooldown -= fTickTime;
			if (m_fSpeedPulseCooldown < 0f)
			{
				m_fSpeedPulseCooldown = 0f;
			}
		}
		if ((flag || num == 1 || num == 2) && m_car.GetVelocity() < 0.5f && m_fSpeedPulseCooldown <= 0f)
		{
			m_car.m_kRigidBody.AddForce(m_car.GetUpGlobal() * 3f, ForceMode.VelocityChange);
			m_fSpeedPulseCooldown = 1.8f;
		}
		if (flag2 && m_fSpeedPulseCooldown <= 0f)
		{
			m_car.m_kRigidBody.AddForce(m_car.GetUpGlobal() * 6f, ForceMode.VelocityChange);
			m_fSpeedPulseCooldown = 0.8f;
		}
	}

	public void Simulate2(float fTickTime)
	{
		Vector3 vector = new Vector3(0f, -1f, 0f);
		Vector3 down = Vector3.down;
		if (m_bLock)
		{
			m_kLanding.Update(fTickTime);
			ref Vector3 reference = ref m_vCompressForce[0];
			ref Vector3 reference2 = ref m_vCompressForce[1];
			reference = (reference2 = m_fMass * down * 0.25f);
			m_car.AddForceAtPointGlobal(m_vCompressForce[0], m_aWheels[0].position, 0.02f);
			m_car.AddForceAtPointGlobal(m_vCompressForce[1], m_aWheels[1].position, 0.02f);
			ref Vector3 reference3 = ref m_vCompressForce[2];
			ref Vector3 reference4 = ref m_vCompressForce[3];
			reference3 = (reference4 = m_fMass * down * 0.25f);
			m_car.AddForceAtPointGlobal(m_vCompressForce[2], m_aWheels[2].position, 0.02f);
			m_car.AddForceAtPointGlobal(m_vCompressForce[3], m_aWheels[3].position, 0.02f);
			return;
		}
		int eContactState = (int)m_chassis.m_eContactState;
		float num = ((eContactState != 0) ? (m_fMass / (float)eContactState) : 0f);
		float num2 = m_fShockDistance;
		for (int i = 0; i < 4; i++)
		{
			float distToGround = m_chassis.GetDistToGround(i);
			float num3 = ((i >= 2) ? m_chassis.m_fBackDistacne : m_chassis.m_fFrontDistance);
			float fOffset = distToGround - num3;
			m_afWheelForce[i] = ((!m_chassis.Nxwheels[i].bGrounded) ? 0f : num);
			m_kShock[i].ApplyForce(m_afWheelForce[i], fOffset);
			fOffset = m_kShock[i].GetCurrentLength();
			m_afWheelOffset[i] = fOffset;
			if (num2 > fOffset)
			{
				num2 = fOffset;
			}
		}
		for (int j = 0; j < 4; j++)
		{
			m_afWheelOffset[j] -= num2;
			Vector3 vector2 = m_afWheelOffset[j] * vector;
			m_aWheels[j].localPosition = m_chassis.Nxwheels[j].LocalOrignPosition + vector2;
			m_chassis.Nxwheels[j].OffsetRoot(m_afWheelOffset[j], 0f);
		}
		if (m_car.GetVelocity() > 8f)
		{
			m_WheelCount++;
			m_WheelCount %= 2;
			Vector3 velGlobal = m_chassis.GetVelGlobal();
			RoadPonit.ROADTYPE rOADTYPE = m_chassis.Nxwheels[m_WheelCount].m_kRoadPoint.CaculateNextType(velGlobal);
			if (rOADTYPE == RoadPonit.ROADTYPE.RT_SIDESTEP)
			{
				m_kJump.BeginJump(m_car.GetVecGlobal(), m_WheelCount);
			}
		}
		ref Vector3 reference5 = ref m_vCompressForce[0];
		reference5 = Vector3.zero;
		ref Vector3 reference6 = ref m_vCompressForce[1];
		reference6 = Vector3.zero;
		ref Vector3 reference7 = ref m_vCompressForce[2];
		reference7 = Vector3.zero;
		ref Vector3 reference8 = ref m_vCompressForce[3];
		reference8 = Vector3.zero;
		if (m_kJump.CanAdjustPicth())
		{
			switch (m_car.VelocityDir)
			{
			case ENMMOTIONDIR.ECMD_FORWARD:
			{
				if ((!m_chassis.Nxwheels[0].bGrounded || !m_chassis.Nxwheels[1].bGrounded) && m_car.LockMotion != ENMLOCKMOTION.ELM_MOVE_ZAXIS)
				{
					if (m_car.m_state != RUNNINGSTATES.RS_INAIR)
					{
						ref Vector3 reference11 = ref m_vCompressForce[0];
						reference11 = m_fMass * down * 0.25f;
						ref Vector3 reference12 = ref m_vCompressForce[1];
						reference12 = m_fMass * down * 0.25f;
						m_car.AddForceAtPointGlobal(m_vCompressForce[0], m_aWheels[0].position, 0.02f);
						m_car.AddForceAtPointGlobal(m_vCompressForce[1], m_aWheels[1].position, 0.02f);
					}
					else
					{
						ref Vector3 reference13 = ref m_vCompressForce[0];
						reference13 = m_fMass * down * 0.09f;
						ref Vector3 reference14 = ref m_vCompressForce[1];
						reference14 = m_fMass * down * 0.09f;
						m_car.AddForceAtPointGlobal(m_vCompressForce[0], m_aWheels[0].position, 0.02f);
						m_car.AddForceAtPointGlobal(m_vCompressForce[1], m_aWheels[1].position, 0.02f);
					}
				}
				Vector3 vForce = m_fMass * Vector3.down * 0.1f;
				m_car.AddForceAtPointGlobal(vForce, m_aWheels[2].position, 0.02f);
				m_car.AddForceAtPointGlobal(vForce, m_aWheels[3].position, 0.02f);
				break;
			}
			case ENMMOTIONDIR.ECMD_BACK:
				if (!m_chassis.Nxwheels[2].bGrounded || !m_chassis.Nxwheels[3].bGrounded)
				{
					ref Vector3 reference9 = ref m_vCompressForce[2];
					reference9 = m_fMass * down * 0.25f;
					ref Vector3 reference10 = ref m_vCompressForce[3];
					reference10 = m_fMass * down * 0.25f;
					m_car.AddForceAtPointGlobal(m_vCompressForce[2], m_aWheels[2].position, 0.02f);
					m_car.AddForceAtPointGlobal(m_vCompressForce[3], m_aWheels[3].position, 0.02f);
				}
				break;
			}
		}
		UpdateSimulator(fTickTime);
	}

	public void BeginLanding()
	{
		m_kLanding.Start();
	}

	public void EndLanding()
	{
		m_kLanding.Stop();
		for (int i = 0; i < 4; i++)
		{
			m_afWheelOffset[i] = m_chassis.Nxwheels[i].WheelOffset;
		}
	}
}
