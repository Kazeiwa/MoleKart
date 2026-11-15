using UnityEngine;

[MessageClass(typeof(PlayerCar))]
internal class PlayerCar : Car
{
	private Vector3 m_DestPosition;

	public PlayerCar()
	{
		MessageFun<Car>(typeof(PlayerCar), processMsg);
	}

	protected override void CreateVehicle(Transform lfp, Transform rfp, Transform lbp, Transform rbp)
	{
		VehicleData vehicleData = TSingleton<CarManager>.Instance.GetVehicleData(m_eCarModeType);
		vehicle = new SimulateKart(vehicleData, base.gameObject.transform, lfp, rfp, lbp, rbp, EnmCollisionLayer.CL_PLAYER, this);
		vehicle.m_kUserData = this;
	}

	public override bool Init(int id, GameObject carEffectRoot)
	{
		base.Init(id, carEffectRoot);
		isHero = false;
		m_CarType = CarType.PLAYERCAR;
		EnableKinematic();
		return true;
	}

	private void Update()
	{
		UpdateCarActor();
	}

	[MessageFun(typeof(PlayerCar))]
	public new void processMsg(int msg, ref ABMessageData data)
	{
		if (msg == 0)
		{
		}
	}

	public override void SetPosition(Vector3 pos)
	{
		m_DestPosition = pos;
		Vector3 position = GetPosition();
		position.y = pos.y;
		float num = m_DestPosition.x - position.x;
		float num2 = m_DestPosition.z - position.z;
		if (num * num + num2 * num2 > 1f)
		{
			position.x = pos.x;
			position.z = pos.z;
		}
		if (vehicle != null)
		{
			vehicle.SetPosition(position);
		}
	}

	public override void UpdatePhysics()
	{
		NiLog.Log("Player:Car , DestPosition =" + m_DestPosition);
		if ((carState & 0x20000) == 0 && (carState & 0x40000) == 0)
		{
			Vector3 position = GetPosition();
			Vector3 vecGlobal = GetVecGlobal();
			position += vecGlobal * Time.deltaTime;
			if (vehicle != null)
			{
				vehicle.SetPosition(position);
			}
		}
		else
		{
			ATrace.Say("Player Car" + base.ID + "stop simulate now!");
		}
		if (vehicle != null)
		{
			base.UpdatePhysics();
		}
	}
}
