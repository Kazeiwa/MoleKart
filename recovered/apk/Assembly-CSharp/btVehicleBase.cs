using System;
using System.Collections.Generic;
using Misc;
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

	public const float fBoostVelocity = 39f;

	public static List<btVehicleBase> ms_ListVehicel = new List<btVehicleBase>();

	public ENMMOTIONDIR AccelDir;

	public ENMMOTIONDIR PreAccelDir;

	public ENMMOTIONDIR VelocityDir;

	public ENMMOTIONDIR PreVelocityDir;

	public ENMTERRAIN_TYPE GroundedType;

	public CarCollisionDetection m_kCarCollisionCenter;

	public VehicleCollisionSide CollisionSide;

	protected bool m_bBrake;

	protected float m_fRPM;

	protected float m_fSteering;

	protected float m_fGearRate = 3f;

	protected Transform[] wheels = new Transform[4];

	protected float m_fWheelPosScale = 1f;

	public Vector3 m_vPreVelGlobal = Vector3.zero;

	protected Vector3 m_vGravity;

	protected float m_fMaxVelocity = 29f;

	protected float m_fMass = 2000f;

	protected float m_fCollisionMass = 2000f;

	public bool Flag_ProccessCarToTreeAndBox = true;

	public bool Flag_ProccessCarToFenceEx = true;

	public bool Flag_ProccessCarToCar = true;

	public bool InShallowWater;

	public bool InDeepWater;

	public int m_CarTorqueFlag;

	public Vector3 m_vOffsetFL;

	public Vector3 m_vOffsetFR;

	public Vector3 m_vOffsetRR;

	public Vector3 m_vOffsetRL;

	public float fFrontTireRadius = 0.2f;

	public float fRearTireRadius = 0.2f;

	public float fSpring = 5000f;

	public float fsuspensionDistance = 0.3f;

	protected Vector3 prePos = Vector3.zero;

	public BoostVar m_kBoostVar = new BoostVar();

	protected VehicleData m_kPhydata;

	public float fAeroDynamicCoeff = 0.01f;

	public UnityEngine.Object m_kUserData;

	public bool m_LockSpeed;

	public bool m_bPhysicsUpdated;

	public bool bHeroVehicle;

	protected bool m_bVisible;

	protected bool triggerPlatform;

	protected Vector3 vAccelate = Vector3.zero;

	public BodyComponent m_kCompositeCollider;

	protected bool m_bGroundFriction = true;

	protected bool m_bMaxColliderMass;

	protected bool m_bBreakVehicleOnce;

	protected Vector3 m_vCurWaterPoint = Vector3.zero;

	protected VehicleCollisionSimulator m_kCollsionSimulator;

	protected BounceSimulator m_kBounceSimulator;

	public bool bRemoteCollider;

	public virtual float MaxTurnScale
	{
		get
		{
			return 1f;
		}
		set
		{
		}
	}

	public virtual float MotorForceScale
	{
		get
		{
			return 1f;
		}
		set
		{
		}
	}

	public float MaxVelocity
	{
		get
		{
			return m_fMaxVelocity;
		}
		set
		{
			m_fMaxVelocity = value;
		}
	}

	public float CurrentSteering => m_fSteering;

	public Vector3 Position => GetPosition();

	public Vector2 Pos => Vector3Ex.To2D(GetPosition());

	public bool bVisible => m_bVisible;

	public float fLimitSpeed { get; set; }

	public bool TriggerPlatform
	{
		get
		{
			return triggerPlatform;
		}
		set
		{
			triggerPlatform = value;
		}
	}

	public Vector3 vFrontLeft => m_CenterofMassTransform.localToWorldMatrix.MultiplyPoint(m_vOffsetFL);

	public Vector3 vFrontRight => m_CenterofMassTransform.localToWorldMatrix.MultiplyPoint(m_vOffsetFR);

	public Vector3 vFrontMid
	{
		get
		{
			Vector3 v = (m_vOffsetFL + m_vOffsetFR) / 2f;
			return m_CenterofMassTransform.localToWorldMatrix.MultiplyPoint(v);
		}
	}

	public Vector3 vBackLeft => m_CenterofMassTransform.localToWorldMatrix.MultiplyPoint(m_vOffsetRL);

	public Vector3 vBackRight => m_CenterofMassTransform.localToWorldMatrix.MultiplyPoint(m_vOffsetRR);

	public Vector3 vLocalMidFrontWheels => (m_vOffsetFL + m_vOffsetFR) / 2f;

	public Vector3 vLocalMidRearWheels => (m_vOffsetRL + m_vOffsetRR) / 2f;

	public Vector2 Velocity
	{
		get
		{
			Vector3 vecGlobal = GetVecGlobal();
			return new Vector2(vecGlobal.x, vecGlobal.z);
		}
	}

	public Vector2 Heading
	{
		get
		{
			Vector2 vector = Vector3Ex.To2D(GetForwardGlobal());
			if (vector.magnitude <= float.Epsilon)
			{
				return Vector2.right;
			}
			return vector.normalized;
		}
	}

	public Vector2 Right => Vector2Ex.RotateRight(Heading);

	public Vector2 Left => Vector2Ex.RotateLeft(Heading);

	public bool bGroundFriction => m_bGroundFriction;

	public float CurrentMinimalCurveRadius => GetMinimalCurveRadius(m_vVecGlobal.magnitude);

	public btVehicleBase(VehicleData phydata, Transform bodyTransform, Transform lFrontWheel, Transform rFrontWheel, Transform lRearWheel, Transform rRearWheel)
	{
		m_kPhydata = phydata;
		fLimitSpeed = m_kPhydata.m_fextremumVelocity;
		m_fMass = m_kPhydata.m_fMass;
		wheels[0] = lFrontWheel;
		wheels[1] = rFrontWheel;
		wheels[3] = lRearWheel;
		wheels[2] = rRearWheel;
		m_CenterofMassTransform = bodyTransform;
		AccelDir = ENMMOTIONDIR.ECMD_STOP;
		VelocityDir = ENMMOTIONDIR.ECMD_STOP;
		GroundedType = ENMTERRAIN_TYPE.ETT_NONE;
		m_kRigidBody = bodyTransform.GetComponent<Rigidbody>();
		m_vGravity = m_kRigidBody.mass * Physics.gravity;
		m_kRigidBody.useGravity = false;
		m_vOffsetFL = new Vector3(-0.5f, -0.3f, 0.68f);
		m_vOffsetFR = new Vector3(0.5f, -0.3f, 0.68f);
		m_vOffsetRR = new Vector3(0.68f, -0.3f, -0.5f);
		m_vOffsetRL = new Vector3(-0.68f, -0.3f, -0.5f);
		Transform transform = bodyTransform.FindChild("WheelColliders");
		for (int i = 0; i < 4; i++)
		{
			WheelCollider wheelCollider = null;
			switch (i)
			{
			case 0:
				wheelCollider = transform.FindChild("LFWheel").GetComponent<WheelCollider>();
				m_vOffsetFL = TransformPositionToRoot(wheelCollider.gameObject, new Vector3(0f, 0f, 0f));
				break;
			case 1:
				wheelCollider = transform.FindChild("RFWheel").GetComponent<WheelCollider>();
				m_vOffsetFR = TransformPositionToRoot(wheelCollider.gameObject, new Vector3(0f, 0f, 0f));
				break;
			case 2:
				wheelCollider = transform.FindChild("RBWheel").GetComponent<WheelCollider>();
				m_vOffsetRR = TransformPositionToRoot(wheelCollider.gameObject, new Vector3(0f, 0f, 0f));
				break;
			case 3:
				wheelCollider = transform.FindChild("LBWheel").GetComponent<WheelCollider>();
				m_vOffsetRL = TransformPositionToRoot(wheelCollider.gameObject, new Vector3(0f, 0f, 0f));
				break;
			}
		}
		ms_ListVehicel.Add(this);
		m_bVisible = true;
	}

	public VehicleData GetPhyData()
	{
		return m_kPhydata;
	}

	public virtual void Uninit()
	{
		ms_ListVehicel.Remove(this);
	}

	private Vector3 TransformPositionToRoot(GameObject wheelroot, Vector3 vPos)
	{
		vPos = wheelroot.transform.localToWorldMatrix.MultiplyPoint(vPos);
		vPos = m_CenterofMassTransform.worldToLocalMatrix.MultiplyPoint(vPos);
		return vPos;
	}

	public Transform GetNode()
	{
		return m_CenterofMassTransform;
	}

	public Vector3 GetMassCenter()
	{
		return m_kRigidBody.worldCenterOfMass;
	}

	public virtual void Hide()
	{
	}

	public virtual void Show()
	{
	}

	public virtual Vector3 GetAccelate()
	{
		return vAccelate;
	}

	public virtual void SetAccelate(Vector3 vAccel)
	{
		vAccelate = vAccel;
	}

	public virtual BodyComponent GetBody()
	{
		return m_kCompositeCollider;
	}

	public virtual BoxComponent GetBox(int i)
	{
		if (m_kCompositeCollider != null)
		{
			return m_kCompositeCollider[i];
		}
		return null;
	}

	public virtual void SetDynamicScaleFactor(float fScale)
	{
		if (m_kCompositeCollider != null)
		{
			m_kCompositeCollider.Scale(fScale);
		}
		float num = fScale / m_fWheelPosScale;
		m_fWheelPosScale = fScale;
		if (this is Vehicle)
		{
			Vehicle vehicle = this as Vehicle;
			for (int i = 0; i < 4; i++)
			{
				Vector3 localOrignPosition = vehicle.chassis.Nxwheels[i].LocalOrignPosition;
				localOrignPosition.x *= num;
				localOrignPosition.z *= num;
				vehicle.chassis.Nxwheels[i].LocalOrignPosition = localOrignPosition;
				vehicle.chassis.WheelsColliders[i].transform.localPosition = vehicle.chassis.Nxwheels[i].LocalOrignPosition;
			}
		}
	}

	public virtual void EnableGroundFriction(bool bEnable)
	{
		m_bGroundFriction = bEnable;
	}

	public virtual void SetMaxColliderMass(bool bMax)
	{
		m_bMaxColliderMass = bMax;
	}

	public virtual bool GetMaxColliderMass()
	{
		return m_bMaxColliderMass;
	}

	public virtual void BreakVehicle(float fAutoBreakTime)
	{
		m_bBreakVehicleOnce = true;
	}

	public virtual void SetMass(float fValue)
	{
		m_fMass = fValue;
	}

	public virtual float GetMass()
	{
		return m_fMass;
	}

	public virtual float GetCollisionMass()
	{
		return m_fCollisionMass;
	}

	public virtual Vector3 GetCurrentWaterPoint()
	{
		return m_vCurWaterPoint;
	}

	public virtual float GetMinimalCurveRadius(float speed)
	{
		float interpolatePointFromCurve = m_kPhydata.GetInterpolatePointFromCurve(speed);
		interpolatePointFromCurve = Mathf.Abs(interpolatePointFromCurve) * ((float)Math.PI / 180f);
		if (interpolatePointFromCurve < 0.001f)
		{
			interpolatePointFromCurve = 0.001f;
		}
		return m_kPhydata.fWheelBase / Mathf.Sin(interpolatePointFromCurve);
	}

	public virtual float GetSteeringAngle(float fRadius)
	{
		return Mathf.Asin(m_kPhydata.fWheelBase / fRadius);
	}

	public virtual float GetMaxSteeringAngleInDegree(float speed)
	{
		return m_kPhydata.GetInterpolatePointFromCurve(speed);
	}

	public virtual float GetMaxSteeringAngle(float speed)
	{
		return GetMaxSteeringAngleInDegree(speed) * ((float)Math.PI / 180f);
	}

	public VehicleCollisionSimulator GetCollisionSimulator()
	{
		return m_kCollsionSimulator;
	}

	public BounceSimulator GetBounceSimulator()
	{
		return m_kBounceSimulator;
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

	public abstract void OnGUI();

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

	public virtual void OnDrawGizmos()
	{
		Color color = Gizmos.color;
		if (SystemConfig.Instance.GizmosTotalForce)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay(Position, m_vTotalForce);
		}
		if (SystemConfig.Instance.GizmosMinimalCurveRadius)
		{
			Gizmos.color = Color.cyan;
			float currentMinimalCurveRadius = CurrentMinimalCurveRadius;
			if (currentMinimalCurveRadius < 200f)
			{
				Vector2 v = currentMinimalCurveRadius * Heading;
				Vector2 vector = Vector2Ex.Rotate(v, (float)Math.PI / 2f);
				Vector2 v2 = -vector;
				Vector3 vector2 = Vector3Ex.Add(Position, vector);
				Vector3 vector3 = Vector3Ex.Add(Position, v2);
				Gizmos.color = Color.cyan;
				GizmosEx.DrawCircle(vector2, currentMinimalCurveRadius);
				GizmosEx.DrawCircle(vector3, currentMinimalCurveRadius);
				Gizmos.color = Color.white;
				Gizmos.DrawLine(vector2, vector3);
			}
		}
		Gizmos.color = color;
	}

	public void OnDrawGizmosSelected()
	{
		if (RaceWorld.Instance.PartitionOfVehicle != null)
		{
			RaceWorld.Instance.PartitionOfVehicle.OnDrawGizmosCellsNearPos(Pos);
		}
	}
}
