using System.Collections.Generic;
using UnityEngine;

public class Suspension : MonoBehaviour
{
	private class Shock
	{
		private float m_fTD;

		private float k;

		private float m_fLastDistance;

		private float m_fLastForce;

		public Shock(float fLimitD)
		{
		}

		public float ApplyForce(float fForce, float fOffset)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public float GetCurrentLength()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public ENMSHAKEDIR m_curShakeDir;

	private List<ISimulator> m_lsSimulators;

	private JumpSimulator m_kJump;

	private LandingSimulator m_kLanding;

	private ChassisSimulator m_chassis;

	private Vehicle m_car;

	private float[] m_afWheelOffset;

	private float m_fShockDistance;

	public Transform[] m_aWheels;

	private float m_fMass;

	private float[] m_afWheelForce;

	private Shock[] m_kShock;

	private bool m_bLock;

	private Vector3[] m_vCompressForce;

	private int m_WheelCount;

	private float m_fSpeedPulseCooldown;

	public void Init(Vehicle car, float fMass)
	{
	}

	public void Lock(bool bCompress)
	{
	}

	public void UnLock()
	{
	}

	public void Relax()
	{
	}

	public void AdjustWheel()
	{
	}

	public void AddSimulator(ISimulator pkSim)
	{
	}

	public void UpdateSimulator(float fTickTime)
	{
	}

	public void RestSpeedPulseCooldown()
	{
	}

	public void Simulate(float fTickTime)
	{
	}

	public void Simulate2(float fTickTime)
	{
	}

	public void BeginLanding()
	{
	}

	public void EndLanding()
	{
	}
}
