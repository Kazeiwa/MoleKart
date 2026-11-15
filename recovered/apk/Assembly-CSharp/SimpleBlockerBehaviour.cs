using Misc;
using UnityEngine;

public class SimpleBlockerBehaviour : MonoBehaviour
{
	protected void OnTriggerEnter(Collider other)
	{
		Car car = CarUtil.FindCarInCollision(other);
		if (null != car)
		{
			car.HitSimpleBlocker();
		}
	}
}
