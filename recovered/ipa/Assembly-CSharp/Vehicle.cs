using UnityEngine;

public class Vehicle : btVehicleBase
{
	public ChassisSimulator chassis;

	public Suspension m_suspension;

	public CollisionHandler m_colHandler;

	public MotorControl motorcontrol;

	public Vector3 vForwardDir;

	public Vector3 vLFWheelPosition;

	public Vector3 vRFWheelPosition;

	public Vector3 vRRWheelPosition;

	public Vector3 vLRWheelPosition;

	public Vector3 vMassCenter;

	public Vector2 FrontSize;

	public Vector2 RearSize;

	public Vector3 vTractiveForce;

	public Vector3 AirFriction;

	public Vector3 vGroundFriction;

	public int m_bEnableForceRot;

	private float m_fPreYaw;

	private float m_fPrePitch;

	private float m_fPreBank;

	private bool m_bLockPose;

	private bool m_bEject;

	private Vector3 curForce;

	private float fTopSpeedLimitCoeff;

	private float fLockTime;

	private float m_fCurScaleFactor;

	private float m_fInnerCurScaleFactor;

	private float fRollingCoefficient;

	private float fBumpAngle;

	private float m_fBackwardCoolTime;

	private float m_fLastMotorForce;

	private float m_fLastBrakeForce;

	private Vector3 ejectSpeed;

	private float m_fLastLandingTime;

	private bool m_bLockMovement;

	private float m_fTireTemperature;

	public bool bEject
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vehicle(VehicleData phydata, Transform bodyTransform, Transform lFrontWheel, Transform rFrontWheel, Transform lRearWheel, Transform rRearWheel, Car car, EnmCollisionLayer clLayer, ICollisionCallback callback)
	{
	}

	public void RotateAroundFrontWheelsLocalUp(float fAngle, bool bRotateVelocity)
	{
	}

	public void RotateAroundFrontWheelsWorldUp(float fAngle, bool bRotateVelocity)
	{
	}

	public void RotateAroundRearWheelsLocalUp(float fAngle, bool bRotateVelocity)
	{
	}

	public override void RotateAroundRearWheelsWorldUp(float fAngle, bool bRotateVelocity)
	{
	}

	public override void RotateAroundPoint(Vector3 vOrigo, float fAngle, bool bRotateVelocity)
	{
	}

	public override void RotateCarLocal(float fAngleX, float fAngleY, float fAngleZ)
	{
	}

	public override void TranslateCarLocal(float OffsetX, float OffsetY, float OffsetZ)
	{
	}

	public override void TranslateCarWorld(Vector3 vOffset)
	{
	}

	public void LockPose()
	{
	}

	public void UnLockPose()
	{
	}

	public bool CheckLock()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void OnDrawGizmos()
	{
	}

	public override void SetKeepLiveVehicle(btVehicleBase kOther)
	{
	}

	public override Vector3 GetUpGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override Vector3 GetForwardGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override Vector3 GetRightGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override Vector3 GetGroundNormal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override Vector3 GetAccelate()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void StopWheelDistanceVisual()
	{
	}

	public override void Uninit()
	{
	}

	private void UpdateVehiclePose()
	{
	}

	private bool AdjustVehiclePose()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void Hide()
	{
	}

	public override void Show()
	{
	}

	public override bool CheckCanDriveNow()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void Update()
	{
	}

	public float GetMotorForce(out float braketorque)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override Vector3 GetCurrentMotorForce()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void ValidatePhysicsParam()
	{
	}

	public override void UpdateRunningRegion()
	{
	}

	public override void UpdateVehicle()
	{
	}

	public override Vector3 GetwheelsPosition(int idx)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override bool MoveToGround(Vector3 vPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void GroundTick(bool handBrake, float motorFroce, float braketorque, float timestamp)
	{
	}

	private void UpdateVehicleState()
	{
	}

	private Vector3 CaculateAirFriction(Vector3 v)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void CalculateFriction(float fTickTime)
	{
	}

	private Vector3 CalculateGroundFriction(float fTickTime, Vector3 vDragDir)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UpdateRepulsion(float fTickTime)
	{
	}

	public override void SetDynamicScaleFactor(float fScale)
	{
	}

	private float CaculateSteering(float fVelcoity)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void Steering(bool bAccelation, bool bBrake, float fSteering)
	{
	}

	public override void AISteeringNew(bool bAccelation, bool bBrake, bool bBackward, float fSteering)
	{
	}

	private float AdjustAngleAccordingSimulateState(float fSteering)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void SimulateFlying(float fTickTime, ENMMOTIONDIR dir)
	{
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

	private void IncreaseTireTemperature()
	{
	}

	private void DecreaseTireTemperature()
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

	private void UpdateBoost(float fTickTime)
	{
	}

	public override void LockMovement(bool bLock)
	{
	}

	public override bool GetLockMovement()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private ENMTERRAIN_TYPE UpdateTerrainEffect(Collider col, out ENM_SUB_TERRAIN_TYPE subType)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private ENMTERRAIN_TYPE GetRealWheelTerrainType()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetWheelTerrainType(NxWheel wheel)
	{
	}

	public override void ChangeVehicleRunningMode(RUNNING_MDOE eMode, object data)
	{
	}

	public override void AddVehicleComponent()
	{
	}
}
