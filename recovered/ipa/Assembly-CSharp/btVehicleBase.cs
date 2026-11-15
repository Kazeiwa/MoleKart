using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RaceTrack;
using UnityEngine;

public abstract class btVehicleBase : MovableObject, INpcCar
{
	public const int MAXWHEELNUM = 4;

	public const int WHEEL_0 = 1;

	public const int WHEEL_1 = 2;

	public const int WHEEL_2 = 4;

	public const int WHEEL_3 = 8;

	public const int WHEELS_FRONT = 3;

	public const int WHEELS_BACK = 12;

	public const int WHEELS_LEFT = 9;

	public const int WHEELS_RIGHT = 6;

	public const int WHEELS_ALL = 15;

	public const int WHEEL0_INDEX = 0;

	public const int WHEEL1_INDEX = 1;

	public const int WHEEL2_INDEX = 2;

	public const int WHEEL3_INDEX = 3;

	public const float NORMAL_FRICTION = 0.5f;

	public const float SLOW_FRICTION = 0.56f;

	public const float fBoostVelocity = 39f;

	public static List<btVehicleBase> ms_ListVehicel;

	public ENMMOTIONDIR AccelDir;

	public ENMMOTIONDIR PreAccelDir;

	public ENMMOTIONDIR VelocityDir;

	public ENMMOTIONDIR PreVelocityDir;

	public ENMTERRAIN_TYPE GroundedType;

	public ENM_SUB_TERRAIN_TYPE SlowEffectType;

	public CarCollisionDetection m_kCarCollisionCenter;

	public VehicleCollisionSide CollisionSide;

	protected bool m_bBrake;

	protected float m_fRPM;

	protected float m_fSteering;

	protected float m_fGearRate;

	protected Transform[] wheels;

	protected float m_fWheelPosScale;

	public Vector3 m_vPreVelGlobal;

	protected Vector3 m_vGravity;

	protected float m_fMaxVelocity;

	protected float m_fMass;

	protected float m_fCollisionMass;

	public bool Flag_ProccessCarToTreeAndBox;

	public bool Flag_ProccessCarToFenceEx;

	public bool Flag_ProccessCarToCar;

	public bool InShallowWater;

	public bool InDeepWater;

	public int m_CarTorqueFlag;

	public Vector3 m_vOffsetFL;

	public Vector3 m_vOffsetFR;

	public Vector3 m_vOffsetRR;

	public Vector3 m_vOffsetRL;

	public float fFrontTireRadius;

	public float fRearTireRadius;

	public float fSpring;

	public float fsuspensionDistance;

	protected Vector3 prePos;

	public BoostVar m_kBoostVar;

	protected VehicleData m_kPhydata;

	public float fAeroDynamicCoeff;

	public Object m_kUserData;

	public bool m_LockSpeed;

	public bool bHeroVehicle;

	protected bool m_bVisible;

	protected bool triggerPlatform;

	protected Vector3 vAccelate;

	public BodyComponent m_kCompositeCollider;

	protected int m_iGroundFrictionLevel;

	protected bool m_bGroundFriction;

	protected bool m_bMaxColliderMass;

	protected bool m_bBreakVehicleOnce;

	protected Vector3 m_vCurWaterPoint;

	public bool bRemoteCollider;

	protected VehicleGravityController m_VehicleGravityController;

	protected VehicleShift m_kShift;

	protected VehicleUpSpeedController m_VehicleUpSpeedController;

	protected FlySimulatorComponent m_FlySimulatorComponent;

	protected FallSimulatorComponent m_FallSimulatorComponent;

	protected LandSimulatorComponent m_LandSimulatorComponent;

	protected BounceSimulatorComponent m_BounceSimulatorComponent;

	protected VehicleCollisionSimulatorComponent m_VehicleCollisionSimulatorComponent;

	public virtual float MaxTurnScale
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual float MotorForceScale
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public Vector3 Gravity
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float Mass
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float MaxVelocity
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float CurrentSteering
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 Position
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Pos
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool bVisible
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float fLimitSpeed
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public bool TriggerPlatform
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public Vector3 vFrontLeft
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vFrontRight
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vFrontMid
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vBackLeft
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vBackRight
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vLocalMidFrontWheels
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vLocalMidRearWheels
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Velocity
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Heading
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Right
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Left
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool bGroundFriction
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float CurrentMinimalCurveRadius
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public btVehicleBase(VehicleData phydata, Transform bodyTransform, Transform lFrontWheel, Transform rFrontWheel, Transform lRearWheel, Transform rRearWheel, Car car)
	{
	}

	public VehicleData GetPhyData()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void Uninit()
	{
	}

	private Vector3 TransformPositionToRoot(GameObject wheelroot, Vector3 vPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Transform GetNode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetMassCenter()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void Hide()
	{
	}

	public virtual void Show()
	{
	}

	public virtual Vector3 GetAccelate()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void SetAccelate(Vector3 vAccel)
	{
	}

	public virtual BodyComponent GetBody()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual BoxComponent GetBox(int i)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void SetDynamicScaleFactor(float fScale)
	{
	}

	public virtual void EnableGroundFriction(bool bEnable)
	{
	}

	public virtual void SetMaxColliderMass(bool bMax)
	{
	}

	public virtual bool GetMaxColliderMass()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void BreakVehicle(float fAutoBreakTime)
	{
	}

	public virtual void SetMass(float fValue)
	{
	}

	public virtual float GetMass()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual float GetCollisionMass()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual Vector3 GetCurrentWaterPoint()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual float GetMinimalCurveRadius(float speed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual float GetSteeringAngle(float fRadius)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual float GetMaxSteeringAngleInDegree(float speed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual float GetMaxSteeringAngle(float speed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public abstract void SetKeepLiveVehicle(btVehicleBase kOther);

	public abstract void LockMovement(bool bLock);

	public abstract bool GetLockMovement();

	public abstract void Update();

	public abstract void UpdateVehicle();

	public abstract void UpdateRunningRegion();

	public abstract bool GetBoostMode();

	public abstract bool GetShiftMode();

	public abstract bool MoveToGround(Vector3 vPos);

	public abstract void EnableKinematic();

	public abstract void DisableKinematic();

	public abstract bool IsKinematic();

	public abstract void StartBoost(float fTime);

	public abstract void StartBoost(float fTime, float fSpeed);

	public abstract void StopBoost();

	public abstract Vector3 GetUpGlobal();

	public abstract Vector3 GetForwardGlobal();

	public abstract Vector3 GetRightGlobal();

	public abstract void AISteeringNew(bool bAccelation, bool bBrake, bool bBackward, float fSteering);

	public abstract void Steering(bool bAccelation, bool bBrake, float fSteering);

	public abstract bool CheckCanDriveNow();

	public abstract void ChangeVehicleRunningMode(RUNNING_MDOE eMode, object data);

	public abstract void RotateCarLocal(float fAngleX, float fAngleY, float fAngleZ);

	public abstract void TranslateCarLocal(float OffsetX, float OffsetY, float OffsetZ);

	public abstract void TranslateCarWorld(Vector3 vOffset);

	public abstract void RotateAroundRearWheelsWorldUp(float fAngle, bool bRotateVelocity);

	public abstract void RotateAroundPoint(Vector3 vOrigo, float fAngle, bool bRotateVelocity);

	public abstract Vector3 GetCurrentMotorForce();

	public abstract Vector3 GetGroundNormal();

	public abstract Vector3 GetwheelsPosition(int idx);

	public virtual void UpdateGravity(float dt)
	{
	}

	protected virtual void BeginEject(Collider col)
	{
	}

	protected virtual void UpdateEject(float dt)
	{
	}

	protected virtual void EndEject()
	{
	}

	protected bool IsEject()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetFlySimulatorVariables(JumpPlatformData data, bool isAccelerate)
	{
	}

	public virtual void SimulateFlying(float fTickTime, ENMMOTIONDIR dir)
	{
	}

	protected virtual void BeginFall()
	{
	}

	protected virtual void UpdateFall(float dt)
	{
	}

	protected virtual void EndFall()
	{
	}

	protected virtual void BeginLand()
	{
	}

	protected virtual void UpdateLand(float dt)
	{
	}

	protected virtual void EndLand()
	{
	}

	public virtual bool IsShiftMode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual int GetShiftLevel()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual bool CanDrawTireEffect()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual float GetShiftFirePower()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual int GetBoostTriggerState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void BreakShift()
	{
	}

	protected virtual void UpdateUpSpeedController()
	{
	}

	protected virtual void BeginBounce()
	{
	}

	protected virtual void UpdateBounce(float dt)
	{
	}

	protected virtual void StopBounce()
	{
	}

	public virtual void SetBounceVarible(float fV, Vector3 Dir, VehicleCollisionSide eside)
	{
	}

	public virtual void DoBounce()
	{
	}

	protected virtual void BeginSideSlip()
	{
	}

	protected virtual void UpdateSideSlip(float dt)
	{
	}

	protected virtual void StopSideSlip()
	{
	}

	public virtual void SetSideSlipVarible(float fDistance, Vector3 Dir, VehicleCollisionSide eside, float fTime)
	{
	}

	public virtual void DoSideSlip()
	{
	}

	public virtual void AISteeringNew2Forth(Vector2 targetPos)
	{
	}

	public virtual void AISteeringNew2Back()
	{
	}

	public virtual void AISteeringNew2Still()
	{
	}

	public virtual void OnDrawGizmos()
	{
	}

	public void OnDrawGizmosSelected()
	{
	}

	public void CancelFlyBoost()
	{
	}

	public virtual void AddVehicleComponent()
	{
	}
}
