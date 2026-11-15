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

	public Vector3 vGroundNormal = Vector3.up;

	public Vector3 vGroundPoint = Vector3.zero;

	public bool bGrounded;

	public Vector3 LocalOrignPosition;

	public RoadPonit m_kRoadPoint = new RoadPonit();

	private ChassisSimulator chassis;

	private WheelFrictionCurve forward = default(WheelFrictionCurve);

	private WheelFrictionCurve sideway = default(WheelFrictionCurve);

	private Transform meshnode;

	private Transform parentnode;

	private ENMSTATUS eState;

	public ENMTERRAIN_TYPE GroundType;

	public bool m_bEnableWheelVisual = true;

	private Vector3 m_vOriginPos;

	private Vector3 m_vOffsetDirection = new Vector3(0f, 0f, -1f);

	private Vector3 m_vExtendDirection = new Vector3(1f, 0f, 0f);

	private float m_fSuspensionOffset;

	public ENMSTATUS TouchState => eState;

	public Transform SuspensionTrasnform => parentnode;

	public float WheelOffset => m_fSuspensionOffset;

	public NxWheel(ChassisSimulator chassis, Transform wheelroot, float fRadius, float fYoffset, WheelCollider kCollider)
	{
		this.chassis = chassis;
		meshnode = ((!(wheelroot == null)) ? wheelroot.GetChild(0) : null);
		parentnode = wheelroot;
		m_vOriginPos = ((!(wheelroot == null)) ? wheelroot.localPosition : Vector3.zero);
		this.fRadius = fRadius;
		ColliderTransform = kCollider.transform;
		LocalOrignPosition = ColliderTransform.localPosition;
		kWheel = kCollider;
		JointSpring suspensionSpring = new JointSpring
		{
			spring = 400f,
			damper = 500f,
			targetPosition = 1f
		};
		kWheel.suspensionDistance = 0.2f;
		kWheel.suspensionSpring = suspensionSpring;
		forward.extremumValue = 20000f;
		forward.extremumSlip = 1f;
		forward.asymptoteValue = 10000f;
		forward.asymptoteSlip = 2f;
		forward.stiffness = 1f;
		sideway.extremumValue = 10000f;
		sideway.extremumSlip = 1f;
		sideway.asymptoteValue = 10000f;
		sideway.asymptoteSlip = 2f;
		sideway.stiffness = 1f;
		kWheel.forwardFriction = forward;
		kWheel.sidewaysFriction = sideway;
		kWheel.radius = fRadius;
		eState = ENMSTATUS.ENS_NONE;
	}

	public void AdjustSideStiffness(float val)
	{
		sideway.stiffness = val;
		kWheel.sidewaysFriction = sideway;
	}

	public float GetSideStiffness()
	{
		return sideway.stiffness;
	}

	public void StopWheelDistanceVisual()
	{
		m_bEnableWheelVisual = false;
	}

	public void RotateTurnWheel(float fRpm, float fTickTime, ENMMOTIONDIR VelocityDir, float fSteering)
	{
		float num = fTickTime * fRpm;
		num = ((VelocityDir != ENMMOTIONDIR.ECMD_BACK) ? num : (0f - num));
		fCurrentRollingAngle += num;
		Quaternion quaternion = Quaternion.AngleAxis(fCurrentRollingAngle, Vector3.right);
		float angle = 20f * fSteering;
		Quaternion quaternion2 = Quaternion.AngleAxis(angle, Vector3.forward);
		if (meshnode != null)
		{
			meshnode.localRotation = quaternion2 * quaternion;
			meshnode.localPosition = position;
		}
		if (kWheel.isGrounded)
		{
			kWheel.GetGroundHit(out var hit);
			vGroundNormal = hit.normal;
			vGroundPoint = hit.point;
			bGrounded = true;
			eState = ENMSTATUS.ENS_NEAR;
			GroundedCollider = hit.collider;
			GroundPhyMaterial = GroundedCollider.material;
			if (meshnode != null && m_bEnableWheelVisual)
			{
				float num2 = Vector3.Dot(kWheel.transform.position - hit.point, hit.normal);
				position.z = kWheel.radius - num2;
				position.z *= 0.5f;
				meshnode.localPosition = position;
			}
		}
		else
		{
			bGrounded = false;
			vGroundNormal = Vector3.up;
			vGroundPoint = position - chassis.GetUpGlobal() * fRadius;
			eState = ENMSTATUS.ENS_NONE;
			GroundedCollider = null;
			GroundPhyMaterial = null;
			if (meshnode != null && m_bEnableWheelVisual)
			{
				position.z = 0f - kWheel.suspensionDistance;
				position.z *= 0.5f;
				meshnode.localPosition = position;
			}
		}
		m_kRoadPoint.Push(vGroundPoint, vGroundNormal);
	}

	public void TurnAngular(float fAngle)
	{
		if (!(Mathf.Abs(fAngle - kWheel.steerAngle) < 0.01f))
		{
			kWheel.steerAngle = fAngle;
		}
	}

	public void OffsetRoot(float fOffset, float fExtend)
	{
	}

	public Vector3 GetPosition()
	{
		return kWheel.transform.position;
	}

	private void Trace()
	{
		Debug.Log(meshnode.name + "Touch" + GroundedCollider.transform.name);
		Debug.Log("TouchMaterial:" + GroundPhyMaterial);
	}
}
