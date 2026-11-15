using UnityEngine;

public interface ICollisionCallback
{
	void OnHitFence(Collider colfrom, Collider colto);

	bool OnHitAnotherCar(btVehicleBase other, VehicleCollisionSide eSide);
}
