using UnityEngine;

public class Kart : btRaycastVehicle
{
	private float maxTurnScale;

	private float motoForceScale;

	public Vector3 vTractiveForce;

	public Vector3 AirFriction;

	public MotorControl motorcontrol;

	private Vector3 vForwardDir;

	private bool m_bLockMovement;

	private bool bBrake;

	private float m_fBackwardCoolTime;

	protected float fLastMotorForce;

	protected float fLastBrakeForce;

	public float m_fTireTemperature;

	public float fRollingCoefficient;

	public float fBumpAngle;

	private int m_WheelCount;

	public override float MaxTurnScale
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override float MotorForceScale
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public Kart(VehicleData phyData, Transform bodyTransform, Transform lFrontWheel, Transform rFrontWheel, Transform lRearWheel, Transform rRearWheel, Car car, EnmCollisionLayer clLayer, ICollisionCallback callback)
	{
	}

	public override void Uninit()
	{
	}

	public override bool MoveToGround(Vector3 vPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void LockMovement(bool bLock)
	{
	}

	public override bool GetLockMovement()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override bool CheckCanDriveNow()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float CaculateSteering(float fVelcoity)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override float GetMaxSteeringAngle(float speed)
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

	private void IncreaseTireTemperature()
	{
	}

	private void DecreaseTireTemperature()
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

	private void GroundTick(bool handBrake, float motorFroce, float braketorque, float timestamp)
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

	public override void UpdateRunningRegion()
	{
	}

	public override void UpdateVehicle()
	{
	}

	private void UpdateVehicleState()
	{
	}

	public override void SetKeepLiveVehicle(btVehicleBase kOther)
	{
	}

	public override void Hide()
	{
	}

	public override void Show()
	{
	}

	public override void Update()
	{
	}

	public void UpdateRepulsion(float fTickTime)
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

	public override void StartBoost(float fTime)
	{
	}

	public override void StartBoost(float fTime, float fSpeed)
	{
	}

	private void UpdateBoost(float fTickTime)
	{
	}

	public override void StopBoost()
	{
	}

	public override bool GetBoostMode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private ENMTERRAIN_TYPE UpdateTerrainEffect(Collider col, out ENM_SUB_TERRAIN_TYPE subType)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetWheelTerrainType(Collider collider)
	{
	}

	public override bool GetShiftMode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void ChangeVehicleRunningMode(RUNNING_MDOE eMode, object data)
	{
	}

	public override void AddVehicleComponent()
	{
	}

	public override void OnDrawGizmos()
	{
	}
}
