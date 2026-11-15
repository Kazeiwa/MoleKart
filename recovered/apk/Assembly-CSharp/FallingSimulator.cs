using UnityEngine;

public class FallingSimulator : ISimulator
{
	private btVehicleBase m_car;

	private bool m_bActive;

	private Vector3 m_vPrePosition;

	private Vector3 m_vVelocity;

	public bool IsActive => m_bActive;

	public FallingSimulator(btVehicleBase car)
	{
		m_car = car;
	}

	public void Begin()
	{
	}

	public override void Update(float fTickTime)
	{
		if (m_car.m_state == RUNNINGSTATES.RS_INAIR)
		{
			NiLog.Log("Current Velocity " + m_car.GetVecGlobal());
			Vector3 vecGlobal = m_car.GetVecGlobal();
			if (vecGlobal.y < -15f)
			{
				vecGlobal.y = -15f;
			}
			m_car.SetVecGlobal(vecGlobal);
		}
		else
		{
			m_car.ChangeVehicleRunningMode(RUNNING_MDOE.RM_GROUNDRUNING, null);
		}
	}
}
