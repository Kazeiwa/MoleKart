using Misc;
using UnityEngine;

public class StickingMonitorBehaviour : MonoBehaviour
{
	protected void OnTriggerEnter(Collider other)
	{
		NPCCar nPCCar = CarUtil.FindNpcCarInCollision(other);
		if (!(nPCCar == null))
		{
			nPCCar.SteeringBehavior.StickingMonitorEnable = true;
		}
	}

	protected void OnTriggerExit(Collider other)
	{
		NPCCar nPCCar = CarUtil.FindNpcCarInCollision(other);
		if (!(nPCCar == null))
		{
			nPCCar.SteeringBehavior.StickingMonitorEnable = false;
		}
	}

	protected void OnDrawGizmos()
	{
		Color color = Gizmos.color;
		Gizmos.color = Color.magenta;
		Gizmos.DrawSphere(base.transform.position, 1.5f);
		Gizmos.color = color;
	}
}
