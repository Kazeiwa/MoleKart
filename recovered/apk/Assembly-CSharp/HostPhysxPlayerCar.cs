using UnityEngine;

[MessageClass(typeof(HostPhysxPlayerCar))]
internal class HostPhysxPlayerCar : PhysxPlayerCar
{
	public HostPhysxPlayerCar()
	{
		MessageFun<Car>(typeof(PhysxPlayerCar), base.processMsg);
	}

	protected override void CreateVehicle(Transform lfp, Transform rfp, Transform lbp, Transform rbp)
	{
		VehicleData vehicleData = TSingleton<CarManager>.Instance.GetVehicleData(m_eCarModeType);
		vehicle = new HostPhysxSimulateKart(vehicleData, base.gameObject.transform, lfp, rfp, lbp, rbp, EnmCollisionLayer.CL_PLAYER, this);
		vehicle.m_kUserData = this;
	}
}
