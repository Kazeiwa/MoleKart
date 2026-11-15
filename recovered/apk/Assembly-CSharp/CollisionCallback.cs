using UnityEngine;

public interface CollisionCallback
{
	void OnHitFence(Collider colfrom, Collider colto);

	bool OnHitAnotherCar(btVehicleBase other, VehicleCollisionSide eSide);
}
