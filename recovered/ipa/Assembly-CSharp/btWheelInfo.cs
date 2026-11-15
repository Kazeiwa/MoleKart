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

	public Vector3 vOrigin
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public btWheelInfo(btWheelInfoConstructionInfo ci)
	{
	}

	public float getSuspensionRestLength()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void setSuspensionRestLength(float fLength)
	{
	}

	public void updateWheel(Rigidbody chassis, ref RaycastInfo raycastInfo)
	{
	}

	public void setOrigin(Vector3 v)
	{
	}

	public void setRotation(Quaternion rot)
	{
	}

	public void TurnAngular(float fAngle)
	{
	}

	public void RotateTurnWheel(float fRpm, float fTickTime, ENMMOTIONDIR VelocityDir, float fSteering)
	{
	}
}
