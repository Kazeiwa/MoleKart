using UnityEngine;

internal class DrawForceGizmos : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		Car componentInChildren = base.gameObject.GetComponentInChildren<Car>();
		if (componentInChildren != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(componentInChildren.vehicle.GetMassCenter(), 0.1f);
			btVehicleBase vehicle = componentInChildren.vehicle;
			Vector3 position = vehicle.GetPosition();
			Gizmos.color = Color.red;
			Vector3 currentMotorForce = vehicle.GetCurrentMotorForce();
			Gizmos.DrawRay(position, currentMotorForce);
			Gizmos.color = Color.green;
			Gizmos.DrawRay(position, vehicle.GetGroundNormal());
		}
	}
}
