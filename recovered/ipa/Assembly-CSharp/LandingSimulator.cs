using UnityEngine;

public class LandingSimulator : ISimulator
{
	private float m_fCurTime;

	private float m_fMaxDistance;

	private Vehicle m_car;

	private ChassisSimulator m_chassis;

	private Suspension m_suspension;

	private float[] m_afWheelOffset;

	private Transform[] m_aWheels;

	private bool m_bActive;

	public bool IsActive
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public LandingSimulator(Vehicle car)
	{
	}

	public void Start()
	{
	}

	public void Stop()
	{
	}

	public override void Update(float fTickTime)
	{
	}
}
