using UnityEngine;

public class SimulateKart : btRaycastVehicle
{
	private bool m_bLockMovement;

	public SimulateKart(VehicleData phyData, Transform bodyTransform, Transform lFrontWheel, Transform rFrontWheel, Transform lRearWheel, Transform rRearWheel, Car car, EnmCollisionLayer clLayer, ICollisionCallback callback)
	{
	}

	public override void Uninit()
	{
	}

	public override void Hide()
	{
	}

	public override void LockMovement(bool bLock)
	{
	}

	public override bool GetLockMovement()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void EnableKinematic()
	{
	}

	public override void DisableKinematic()
	{
	}

	public override bool IsKinematic()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void SetKeepLiveVehicle(btVehicleBase kOther)
	{
	}

	public override void StartBoost(float fTime)
	{
	}

	public override void StartBoost(float fTime, float fSpeed)
	{
	}

	public override void StopBoost()
	{
	}

	public override bool GetBoostMode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override bool GetShiftMode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override Vector3 GetCurrentMotorForce()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override bool MoveToGround(Vector3 vPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void AISteeringNew(bool bAccelation, bool bBrake, bool bBackward, float fSteering)
	{
	}

	public override void Steering(bool bAccelation, bool bBrake, float fSteering)
	{
	}

	public override bool CheckCanDriveNow()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void Update()
	{
	}

	public override void UpdateRunningRegion()
	{
	}

	public override void UpdateVehicle()
	{
	}

	public override void ChangeVehicleRunningMode(RUNNING_MDOE eMode, object data)
	{
	}
}
