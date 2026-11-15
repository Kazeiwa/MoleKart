using GameNetwork;
using Misc;
using UnityEngine;

public class TollGateTriggerForwarder : MonoBehaviour
{
	public TollGateBehaviour Behaviour;

	protected void OnTriggerEnter(Collider other)
	{
		Car car = CarUtil.FindCarInCollision(other);
		if (null != car && (NetworkManager.Instance.IsStandAlone || car.isHero))
		{
			Behaviour.HitCar(car);
		}
	}
}
