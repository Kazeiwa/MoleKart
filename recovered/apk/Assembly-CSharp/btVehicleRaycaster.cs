using UnityEngine;

public class btVehicleRaycaster
{
	public class btVehicleRaycasterResult
	{
		private Vector3 m_hitPointInWorld;

		private Vector3 m_hitNormalInWorld;
	}

	public virtual void castRay(Vector3 from, Vector3 to, btVehicleRaycasterResult result)
	{
	}
}
