using Misc;
using UnityEngine;

public class SpeedUpTrigger : MonoBehaviour
{
	private void Start()
	{
		base.transform.gameObject.layer = 28;
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		bool flag = false;
		Car car = CarUtil.FindCarInCollision(other);
		if (null != car && car.CheckSpeedUpCondition())
		{
			if (car.currentTouchSpeedUp != base.transform)
			{
				flag = true;
			}
			if (Time.time - car.lastTimeTouchSpeedUp > 0.75f)
			{
				flag = true;
			}
			if (flag)
			{
				car.AccelerateCar(1f);
				car.lastTimeTouchSpeedUp = Time.time;
				car.currentTouchSpeedUp = base.transform;
			}
		}
	}
}
