using Misc;
using UnityEngine;

public class JumpPlatformTrigger : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		Car car = CarUtil.FindCarInCollision(other);
		if (null != car)
		{
			car.vehicle.TriggerPlatform = true;
		}
	}
}
