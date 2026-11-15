using UnityEngine;

[MessageClass(typeof(PhysxPlayerCar))]
internal class PhysxPlayerCar : Car
{
	private Vector3 m_DestPosition;

	private Vector3 vDelta = Vector3.zero;

	private float fAlpha;

	private float fAccumulator;

	public PhysxPlayerCar()
	{
		MessageFun<Car>(typeof(PhysxPlayerCar), processMsg);
	}

	protected override void CreateVehicle(Transform lfp, Transform rfp, Transform lbp, Transform rbp)
	{
		VehicleData vehicleData = TSingleton<CarManager>.Instance.GetVehicleData(m_eCarModeType);
		vehicle = new PhysxSimulateKart(vehicleData, base.gameObject.transform, lfp, rfp, lbp, rbp, EnmCollisionLayer.CL_PLAYER, this);
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

	[MessageFun(typeof(PhysxPlayerCar))]
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
		if ((carState & 0x20000) == 0 && (carState & 0x40000) == 0 && (carState & 0x200) == 0 && (carState & 0x10000) == 0 && (carState & 0x10) == 0 && (carState & 0x1000) == 0 && (carState & 0x2000) == 0)
		{
			position.y = pos.y;
			vDelta = m_DestPosition - position;
			vDelta.y = 0f;
			fAccumulator = 0f;
			fAlpha = 0f;
		}
		else
		{
			vDelta = Vector3.zero;
			fAlpha = 0f;
		}
		if (vehicle != null)
		{
			vehicle.SetPosition(position);
		}
	}

	public override void UpdatePhysics()
	{
		if ((carState & 0x20000) == 0 && (carState & 0x40000) == 0 && (carState & 0x200) == 0 && (carState & 0x10000) == 0 && (carState & 0x10) == 0 && (carState & 0x1000) == 0 && (carState & 0x2000) == 0)
		{
			Vector3 position = GetPosition();
			Vector3 vecGlobal = GetVecGlobal();
			Vector3 accelate = GetAccelate();
			accelate.y = 0f;
			position += vecGlobal * Time.deltaTime;
			if (fAccumulator < 0.05f)
			{
				float num = Time.deltaTime;
				if (fAccumulator + Time.deltaTime > 0.05f)
				{
					num = 0.05f - fAccumulator;
				}
				fAlpha = num / 0.05f;
				Vector3 vector = fAlpha * vDelta * 0.8f;
				position += vector;
			}
			else
			{
				fAlpha = 0f;
				vDelta = Vector3.zero;
			}
			fAccumulator += Time.deltaTime;
			if (vehicle != null)
			{
				vehicle.SetPosition(position);
			}
		}
		if (vehicle != null)
		{
			base.UpdatePhysics();
		}
	}

	public override void SetCarState(uint state)
	{
		if (carState != state && ((carState & 0x20000) != 0 || (carState & 0x40000) != 0 || (carState & 0x200) != 0 || (carState & 0x10000) != 0 || (carState & 0x10) != 0 || (carState & 0x1000) != 0 || (carState & 0x2000) != 0))
		{
			carState = state;
		}
	}
}
