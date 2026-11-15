using UnityEngine;

public class btWheelInfo
{
	public enum ENMSTATUS
	{
		ENS_NONE,
		ENS_OTHER,
		ENS_NEAR,
		ENS_FAR
	}

	public struct RaycastInfo
	{
		public Vector3 m_contactNormalWS;

		public Vector3 m_contactPointWS;

		public float m_suspensionLength;

		public Vector3 m_hardPointWS;

		public Vector3 m_wheelDirectionWS;

		public Vector3 m_wheelAxleWS;

		public bool m_isInContact;

		public Collider m_groundObject;
	}

	public RaycastInfo m_raycastInfo;

	public Vector3 m_chassisConnectionPointCS;

	public Vector3 m_wheelDirectionCS;

	public Vector3 m_wheelAxleCS;

	public float steerAngle;

	private Transform m_worldTransform;

	private Transform m_meshNode;

	private float m_suspensionRestLength1;

	public float m_maxSuspensionTravel;

	public float m_wheelsRadius;

	public float m_suspensionStiffness;

	public float m_wheelsDampingCompression;

	public float m_wheelsDampingRelaxation;

	public float m_frictionSlip;

	public float m_steering;

	public float m_rotation;

	public float m_deltaRotation;

	public float m_rollInfluence;

	public float m_maxSuspensionForce;

	public float m_engineForce;

	public float m_brake;

	public bool m_bIsFrontWheel;

	public float m_clippedInvContactDotSuspension;

	public float m_suspensionRelativeVelocity;

	public float m_wheelsSuspensionForce;

	public float m_skidInfo;

	public ENMSTATUS m_eState;

	private float fCurrentRollingAngle;

	public Vector3 vOrigin => m_worldTransform.position;

	public btWheelInfo(btWheelInfoConstructionInfo ci)
	{
		m_suspensionRestLength1 = ci.m_suspensionRestLength;
		m_maxSuspensionTravel = ci.m_maxSuspensionTravel;
		m_wheelsRadius = ci.m_wheelRadius;
		m_suspensionStiffness = ci.m_suspensionStiffness;
		m_wheelsDampingCompression = ci.m_wheelsDampingCompression;
		m_wheelsDampingRelaxation = ci.m_wheelsDampingRelaxation;
		m_chassisConnectionPointCS = ci.m_chassisConnectionCS;
		m_wheelDirectionCS = ci.m_wheelDirectionCS;
		m_wheelAxleCS = ci.m_wheelAxleCS;
		m_frictionSlip = ci.m_frictionSlip;
		m_steering = 0f;
		m_engineForce = 0f;
		m_rotation = 0f;
		m_deltaRotation = 0f;
		m_brake = 0f;
		m_rollInfluence = 0.1f;
		m_bIsFrontWheel = ci.m_bIsFrontWheel;
		m_maxSuspensionForce = ci.m_maxSuspensionForce;
	}

	public float getSuspensionRestLength()
	{
		return m_suspensionRestLength1;
	}

	public void setSuspensionRestLength(float fLength)
	{
		m_suspensionRestLength1 = fLength;
	}

	public void updateWheel(Rigidbody chassis, ref RaycastInfo raycastInfo)
	{
		if (m_raycastInfo.m_isInContact)
		{
			float num = Vector3.Dot(m_raycastInfo.m_contactNormalWS, m_raycastInfo.m_wheelDirectionWS);
			Vector3 relativePoint = m_raycastInfo.m_contactPointWS - chassis.worldCenterOfMass;
			Vector3 relativePointVelocity = chassis.GetRelativePointVelocity(relativePoint);
			float num2 = Vector3.Dot(m_raycastInfo.m_contactNormalWS, relativePointVelocity);
			if (num >= -0.1f)
			{
				m_suspensionRelativeVelocity = 0f;
				m_clippedInvContactDotSuspension = 10f;
			}
			else
			{
				float num3 = -1f / num;
				m_suspensionRelativeVelocity = num2 * num3;
				m_clippedInvContactDotSuspension = num3;
			}
		}
		else
		{
			m_raycastInfo.m_suspensionLength = getSuspensionRestLength();
			m_suspensionRelativeVelocity = 0f;
			m_raycastInfo.m_contactNormalWS = -m_raycastInfo.m_wheelDirectionWS;
			m_clippedInvContactDotSuspension = 1f;
		}
	}

	public void setOrigin(Vector3 v)
	{
		if (null != m_worldTransform)
		{
			m_worldTransform.position = v;
		}
	}

	public void setRotation(Quaternion rot)
	{
		if (null != m_worldTransform)
		{
			m_worldTransform.localRotation = rot;
		}
	}

	public void TurnAngular(float fAngle)
	{
		if (!(Mathf.Abs(fAngle - steerAngle) < 0.01f))
		{
			steerAngle = fAngle;
		}
	}

	public void RotateTurnWheel(float fRpm, float fTickTime, ENMMOTIONDIR VelocityDir, float fSteering)
	{
		float num = fTickTime * fRpm;
		num = ((VelocityDir != ENMMOTIONDIR.ECMD_BACK) ? num : (0f - num));
		fCurrentRollingAngle += num;
		Quaternion quaternion = Quaternion.AngleAxis(fCurrentRollingAngle, Vector3.right);
		float angle = 20f * fSteering;
		Quaternion quaternion2 = Quaternion.AngleAxis(angle, Vector3.forward);
		if (m_meshNode != null)
		{
			m_meshNode.localRotation = quaternion2 * quaternion;
		}
		m_eState = (m_raycastInfo.m_isInContact ? ENMSTATUS.ENS_NEAR : ENMSTATUS.ENS_NONE);
	}
}
