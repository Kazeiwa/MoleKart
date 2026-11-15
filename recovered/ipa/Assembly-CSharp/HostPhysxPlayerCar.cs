using UnityEngine;

[MessageClass(typeof(HostPhysxPlayerCar))]
internal class HostPhysxPlayerCar : PhysxPlayerCar
{
	protected override void CreateVehicle(Transform lfp, Transform rfp, Transform lbp, Transform rbp)
	{
	}
}
