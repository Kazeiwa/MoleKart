using GameNetwork;
using Misc;
using UnityEngine;

public class TrapTriggerForwarder : MonoBehaviour
{
	public TrapBehaviour trap;

	protected void OnTriggerEnter(Collider other)
	{
		Car car = CarUtil.FindCarInCollision(other);
		if (null != car && (NetworkManager.Instance.IsStandAlone || car.isHero))
		{
			trap.HitCar(car);
		}
	}
}
