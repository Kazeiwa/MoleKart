using UnityEngine;

public class NxWheel
{
	public enum ENMSTATUS
	{
		ENS_NONE,
		ENS_OTHER,
		ENS_NEAR,
		ENS_FAR
	}

	public WheelCollider kWheel;

	public Transform ColliderTransform;

	public Collider GroundedCollider;

	public PhysicMaterial GroundPhyMaterial;

	public float fCurrentRollingAngle;

	public float fSteeringAngle;

	public float fRadius;

	public Vector3 position;

	public Vector3 vGroundNormal;

	public Vector3 vGroundPoint;

	public bool bGrounded;

	public Vector3 LocalOrignPosition;

	public RoadPoint m_kRoadPoint;

	private ChassisSimulator chassis;

	private WheelFrictionCurve forward;

	private WheelFrictionCurve sideway;

	private Transform meshnode;

	private Transform parentnode;

	private ENMSTATUS eState;

	public ENMTERRAIN_TYPE GroundType;

	public bool m_bEnableWheelVisual;

	private Vector3 m_vOriginPos;

	private Vector3 m_vOffsetDirection;

	private Vector3 m_vExtendDirection;

	private float m_fSuspensionOffset;

	public ENMSTATUS TouchState
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Transform SuspensionTrasnform
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float WheelOffset
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public NxWheel(ChassisSimulator chassis, Transform wheelroot, float fRadius, float fYoffset, WheelCollider kCollider)
	{
	}

	public void AdjustSideStiffness(float val)
	{
	}

	public float GetSideStiffness()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void StopWheelDistanceVisual()
	{
	}

	public void RotateTurnWheel(float fRpm, float fTickTime, ENMMOTIONDIR VelocityDir, float fSteering)
	{
	}

	public void TurnAngular(float fAngle)
	{
	}

	public void OffsetRoot(float fOffset, float fExtend)
	{
	}

	public Vector3 GetPosition()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void Trace()
	{
	}
}
