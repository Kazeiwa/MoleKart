using UnityEngine;

public class FallingSimulator : ISimulator
{
	private btVehicleBase m_car;

	private bool m_bActive;

	private Vector3 m_vPrePosition;

	private Vector3 m_vVelocity;

	public bool IsActive
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public FallingSimulator(btVehicleBase car)
	{
	}

	public void Begin()
	{
	}

	public override void Update(float fTickTime)
	{
	}
}
