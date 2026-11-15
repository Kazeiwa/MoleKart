using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class btRaycastVehicle : btVehicleBase
{
	private const float DRAG = 5f;

	private const float RESISTANCE = 50f;

	private const float CA_R = -5.2f;

	private const float CA_F = -5f;

	private const float MAX_GRIP = 2f;

	protected float m_tau;

	protected float m_damping;

	protected btVehicleRaycaster m_vehicleRaycaster;

	protected float m_pitchControl;

	protected float m_steeringValue;

	protected int m_indexRightAxis;

	protected int m_indexUpAxis;

	protected int m_indexForwardAxis;

	protected float m_fSteeringAngle;

	protected List<btWheelInfo> m_wheelInfo = new List<btWheelInfo>();

	public CONTACTSTATE m_eContactState = CONTACTSTATE.CS_FOURWHEEL;

	protected GameObject BodyObject;

	public float m_fFrontDistance = 0.2f;

	public float m_fBackDistacne = 0.2f;

	protected Vector2 m_vTraction = Vector2.zero;

	protected Vector2 m_vResistance = Vector2.zero;

	private Vector2 velocity_wc = Vector2.zero;

	protected float motortorque;

	protected float braketorque;

	private float m_track_connect_accel;

	private int iCurIngore;

	private Vector3 m_vGroundNormal = Vector3.up;

	public Vector3[] m_arrGroundNormal = new Vector3[4];

	public Vector3[] m_arrGroundPoint = new Vector3[4];

	public btRaycastVehicle(VehicleData phydata, Transform bodyTransform, Transform lFrontWheel, Transform rFrontWheel, Transform lRearWheel, Transform rRearWheel)
		: base(phydata, bodyTransform, lFrontWheel, rFrontWheel, lRearWheel, rRearWheel)
	{
		m_pitchControl = 0f;
		m_indexRightAxis = 0;
		m_indexUpAxis = 2;
		m_indexForwardAxis = 1;
	}

	private bool Init(btVehicleTuning tuning, Rigidbody chassis, btVehicleRaycaster raycaster)
	{
		if (null == chassis || tuning == null || raycaster == null)
		{
			return false;
		}
		m_vehicleRaycaster = raycaster;
		m_pitchControl = 0f;
		m_kRigidBody = chassis;
		m_indexRightAxis = 0;
		m_indexUpAxis = 2;
		m_indexForwardAxis = 1;
		defaultInit(tuning);
		return true;
	}

	private void defaultInit(btVehicleTuning tuning)
	{
		m_steeringValue = 0f;
	}

	public void InitChassis(Transform[] wheels, float fFrontTireRadius, float fRearTireRadius, float fSpring, float fsuspensionDistance, GameObject body)
	{
		m_kRigidBody = body.GetComponent<Rigidbody>();
		Init(new btVehicleTuning(), m_kRigidBody, new btVehicleRaycaster());
		BodyObject = body;
		base.fFrontTireRadius = fFrontTireRadius;
		base.fRearTireRadius = fRearTireRadius;
		base.fsuspensionDistance = fsuspensionDistance;
		Transform transform = body.transform.FindChild("WheelColliders");
		if (null != transform)
		{
			transform.gameObject.SetActiveRecursively(state: false);
		}
		CaculateMassCenter(body);
		addWheel(m_vOffsetFL, Vector3.down, Vector3.right, fsuspensionDistance, fFrontTireRadius, new btVehicleTuning(), isFrontWheel: true);
		addWheel(m_vOffsetFR, Vector3.down, Vector3.right, fsuspensionDistance, fFrontTireRadius, new btVehicleTuning(), isFrontWheel: true);
		addWheel(m_vOffsetRR, Vector3.down, Vector3.right, fsuspensionDistance, fRearTireRadius, new btVehicleTuning(), isFrontWheel: false);
		addWheel(m_vOffsetRL, Vector3.down, Vector3.right, fsuspensionDistance, fRearTireRadius, new btVehicleTuning(), isFrontWheel: false);
	}

	private void CaculateMassCenter(GameObject body)
	{
		m_kRigidBody.centerOfMass = Vector3.zero;
	}

	private btWheelInfo addWheel(Vector3 connectionPointCS, Vector3 wheelDirectionCS0, Vector3 wheelAxleCS, float suspensionRestLength, float wheelRadius, btVehicleTuning tuning, bool isFrontWheel)
	{
		btWheelInfoConstructionInfo ci = default(btWheelInfoConstructionInfo);
		ci.m_chassisConnectionCS = connectionPointCS;
		ci.m_wheelDirectionCS = wheelDirectionCS0;
		ci.m_wheelAxleCS = wheelAxleCS;
		ci.m_suspensionRestLength = suspensionRestLength;
		ci.m_wheelRadius = wheelRadius;
		ci.m_suspensionStiffness = tuning.m_suspensionStiffness;
		ci.m_wheelsDampingCompression = tuning.m_suspensionCompression;
		ci.m_wheelsDampingRelaxation = tuning.m_suspensionDamping;
		ci.m_frictionSlip = tuning.m_frictionSlip;
		ci.m_bIsFrontWheel = isFrontWheel;
		ci.m_maxSuspensionTravel = tuning.m_maxSuspensionTravel;
		ci.m_maxSuspensionForce = tuning.m_maxSuspensionForce;
		m_wheelInfo.Add(new btWheelInfo(ci));
		btWheelInfo btWheelInfo2 = m_wheelInfo[getNumWheels() - 1];
		updateWheelTransformsWS(btWheelInfo2, interpolatedTransform: false);
		updateWheelTransform(getNumWheels() - 1, interpolatedTransform: false);
		return btWheelInfo2;
	}

	protected int getNumWheels()
	{
		return m_wheelInfo.Count;
	}

	protected btWheelInfo Wheels(int idx)
	{
		if (idx < getNumWheels() && idx >= 0)
		{
			return m_wheelInfo[idx];
		}
		return null;
	}

	public Transform getChassisWorldTransform()
	{
		return m_CenterofMassTransform;
	}

	private void updateWheelTransformsWS(btWheelInfo wheel, bool interpolatedTransform)
	{
		wheel.m_raycastInfo.m_isInContact = false;
		wheel.m_raycastInfo.m_hardPointWS = m_CenterofMassTransform.localToWorldMatrix.MultiplyPoint(wheel.m_chassisConnectionPointCS);
		wheel.m_raycastInfo.m_wheelDirectionWS = m_CenterofMassTransform.TransformDirection(wheel.m_wheelDirectionCS);
		wheel.m_raycastInfo.m_wheelAxleWS = m_CenterofMassTransform.TransformDirection(wheel.m_wheelAxleCS);
	}

	private void updateWheelTransform(int wheelIndex, bool interpolatedTransform)
	{
		btWheelInfo btWheelInfo2 = m_wheelInfo[wheelIndex];
		updateWheelTransformsWS(btWheelInfo2, interpolatedTransform);
		Vector3 axis = -btWheelInfo2.m_raycastInfo.m_wheelDirectionWS;
		Vector3 wheelAxleWS = btWheelInfo2.m_raycastInfo.m_wheelAxleWS;
		float steering = btWheelInfo2.m_steering;
		Quaternion quaternion = Quaternion.AngleAxis(steering, axis);
		Quaternion quaternion2 = Quaternion.AngleAxis(0f - btWheelInfo2.m_rotation, wheelAxleWS);
		btWheelInfo2.setRotation(quaternion * quaternion2);
		btWheelInfo2.setOrigin(btWheelInfo2.m_raycastInfo.m_hardPointWS + btWheelInfo2.m_raycastInfo.m_wheelDirectionWS * btWheelInfo2.m_raycastInfo.m_suspensionLength);
	}

	protected bool projectVehicleToSurface(Vector3 ray, bool btranslate_vehicle)
	{
		if (ray.magnitude <= 0f)
		{
			return false;
		}
		for (int i = 0; i < getNumWheels(); i++)
		{
			updateWheelTransform(i, interpolatedTransform: false);
		}
		float num = -1f;
		int num2 = 0;
		int num3 = 0;
		int index = 0;
		float[] array = new float[4];
		for (int j = 0; j < m_wheelInfo.Count; j++)
		{
			array[j] = rayCast(m_wheelInfo[j], ray);
			array[j] -= m_wheelInfo[j].m_wheelsRadius;
			if (!m_wheelInfo[j].m_raycastInfo.m_isInContact)
			{
				return false;
			}
			if (array[j] < num || j == 0)
			{
				num = array[j];
				num2 = j;
			}
		}
		bool flag = true;
		for (int k = 0; k < m_wheelInfo.Count; k++)
		{
			if (k != num2 && (array[k] < num || flag))
			{
				num = array[k];
				num3 = k;
				flag = false;
			}
		}
		flag = true;
		for (int l = 0; l < m_wheelInfo.Count; l++)
		{
			if (l != num2 && l != num3 && (array[l] < num || flag))
			{
				num = array[l];
				index = l;
				flag = false;
			}
		}
		num = array[num2];
		btWheelInfo btWheelInfo2 = m_wheelInfo[num2];
		btWheelInfo btWheelInfo3 = m_wheelInfo[num3];
		btWheelInfo btWheelInfo4 = m_wheelInfo[index];
		Vector3 position = GetPosition();
		position.y -= num;
		SetPosition(position);
		return true;
	}

	private float rayCast(btWheelInfo wheel)
	{
		float num = -1f;
		float num2 = wheel.getSuspensionRestLength() + wheel.m_wheelsRadius + wheel.m_maxSuspensionTravel;
		Vector3 vector = -GetUpGlobal();
		Vector3 ray = vector * num2;
		num = rayCast(wheel, ray);
		if (num == -1f)
		{
			wheel.m_raycastInfo.m_suspensionLength = wheel.getSuspensionRestLength();
			wheel.m_suspensionRelativeVelocity = 0f;
			wheel.m_raycastInfo.m_contactNormalWS = -wheel.m_raycastInfo.m_wheelDirectionWS;
			wheel.m_clippedInvContactDotSuspension = 1f;
		}
		else
		{
			wheel.m_raycastInfo.m_suspensionLength = num - wheel.m_wheelsRadius;
			float min = wheel.getSuspensionRestLength() - wheel.m_maxSuspensionTravel;
			float max = wheel.getSuspensionRestLength() + wheel.m_maxSuspensionTravel;
			wheel.m_raycastInfo.m_suspensionLength = Mathf.Clamp(wheel.m_raycastInfo.m_suspensionLength, min, max);
			Vector3 relativePoint = wheel.m_raycastInfo.m_contactPointWS - m_CenterofMassTransform.position;
			Vector3 relativePointVelocity = m_kRigidBody.GetRelativePointVelocity(relativePoint);
			float num3 = Vector3.Dot(wheel.m_raycastInfo.m_contactNormalWS, relativePointVelocity);
			float num4 = Vector3.Dot(wheel.m_raycastInfo.m_contactNormalWS, wheel.m_raycastInfo.m_wheelDirectionWS);
			if (num4 >= -0.1f)
			{
				wheel.m_suspensionRelativeVelocity = 0f;
				wheel.m_clippedInvContactDotSuspension = 10f;
			}
			else
			{
				float num5 = -1f / num4;
				wheel.m_suspensionRelativeVelocity = num3 * num5;
				wheel.m_clippedInvContactDotSuspension = num5;
			}
		}
		return num;
	}

	private float rayCast(btWheelInfo wheel, Vector3 ray)
	{
		updateWheelTransformsWS(wheel, interpolatedTransform: false);
		float result = -1f;
		Vector3 hardPointWS = wheel.m_raycastInfo.m_hardPointWS;
		wheel.m_raycastInfo.m_contactPointWS = hardPointWS + ray;
		float magnitude = ray.magnitude;
		Vector3 vector = hardPointWS + ray;
		if (HeightMapManager.Instance.bValidate)
		{
			float num = HeightMapManager.Instance.Height(vector.x, vector.z);
			float num2 = HeightMapManager.Instance.Height(hardPointWS.x, hardPointWS.z);
			float num3 = hardPointWS.y - num2;
			float num4 = vector.y - num;
			if (num3 * num4 < 0f)
			{
				Vector3 vector2 = num3 / (num3 - num4) * ray + hardPointWS;
				Vector3 contactNormalWS = HeightMapManager.Instance.Normal(vector2.x, vector2.y, vector2.z);
				result = (vector2 - hardPointWS).magnitude;
				wheel.m_raycastInfo.m_contactPointWS = vector2;
				wheel.m_raycastInfo.m_contactNormalWS = contactNormalWS;
				wheel.m_raycastInfo.m_isInContact = true;
				wheel.m_raycastInfo.m_groundObject = HeightMapManager.Instance.Collider(vector2.x, vector2.y, vector2.z);
			}
			else if (num3 <= 0f)
			{
				Vector3 contactPointWS = hardPointWS;
				contactPointWS.y = num2;
				Vector3 contactNormalWS2 = HeightMapManager.Instance.Normal(contactPointWS.x, contactPointWS.y, contactPointWS.z);
				result = num3;
				wheel.m_raycastInfo.m_contactPointWS = contactPointWS;
				wheel.m_raycastInfo.m_contactNormalWS = contactNormalWS2;
				wheel.m_raycastInfo.m_isInContact = true;
				wheel.m_raycastInfo.m_groundObject = HeightMapManager.Instance.Collider(contactPointWS.x, contactPointWS.y, contactPointWS.z);
			}
			else
			{
				wheel.m_raycastInfo.m_isInContact = false;
				wheel.m_raycastInfo.m_contactNormalWS = Vector3.up;
				wheel.m_wheelsSuspensionForce = 0f;
			}
		}
		else
		{
			wheel.m_raycastInfo.m_isInContact = false;
			wheel.m_raycastInfo.m_contactNormalWS = Vector3.up;
			wheel.m_wheelsSuspensionForce = 0f;
		}
		wheel.updateWheel(m_kRigidBody, ref wheel.m_raycastInfo);
		return result;
	}

	public override Vector3 GetwheelsPosition(int idx)
	{
		btWheelInfo btWheelInfo2 = m_wheelInfo[idx];
		Vector3 result = btWheelInfo2.m_raycastInfo.m_wheelDirectionWS * btWheelInfo2.m_raycastInfo.m_suspensionLength;
		if (idx == 0)
		{
			result += base.vFrontLeft;
		}
		return result;
	}

	private void DoWheelSteering(float fTickTime)
	{
		GetEulerAngle();
		float num = Mathf.Sin((float)Math.PI / 180f * m_fYaw);
		float num2 = Mathf.Cos((float)Math.PI / 180f * m_fYaw);
		Vector2 zero = Vector2.zero;
		zero.x = num2 * velocity_wc.y + num * velocity_wc.x;
		zero.y = (0f - num) * velocity_wc.y + num2 * velocity_wc.x;
		float y = m_kPhydata.fWheelBase * 0.5f * m_fAngularVelocity;
		float num3 = 0f;
		num3 = ((zero.x != 0f) ? Mathf.Atan2(y, zero.x) : 0f);
		float num4 = 0f;
		num4 = ((zero.x != 0f) ? Mathf.Atan2(zero.y, zero.x) : 0f);
		float num5 = num4 + num3 - m_fSteeringAngle;
		float num6 = num4 - num3;
		float num7 = m_kRigidBody.mass * 0.5f;
		float num8 = m_kRigidBody.mass * 9.8f * 0.5f;
		Vector2 zero2 = Vector2.zero;
		Vector2 zero3 = Vector2.zero;
		zero2.x = 0f;
		zero2.y = -5f * num5;
		zero2.y = Mathf.Min(2f, zero2.y);
		zero2.y = Mathf.Max(-2f, zero2.y);
		zero2.y *= num8;
		zero3.x = 0f;
		zero3.y = -5.2f * num6;
		zero3.y = Mathf.Min(2f, zero3.y);
		zero3.y = Mathf.Max(-2f, zero3.y);
		zero3.y *= num8;
		Vector2 zero4 = Vector2.zero;
		Vector2 zero5 = Vector2.zero;
		m_vTraction.x = motortorque - braketorque * Mathf.Sign(zero.x);
		m_vTraction.y = 0f;
		m_vResistance.x = (0f - m_vResistance.x) * Mathf.Sign(zero.x);
		zero5 = m_vTraction + m_vResistance;
		zero4.x = Mathf.Sin(m_fSteeringAngle) * zero2.x + zero3.x + zero5.x;
		Vector2 zero6 = Vector2.zero;
		zero6.x = zero4.x / num7;
		zero6.y = zero4.y / num7;
		Vector2 zero7 = Vector2.zero;
		zero7.x = num2 * zero6.y + num * zero6.x;
		zero7.y = (0f - num) * zero6.y + num2 * zero6.x;
		velocity_wc.x += fTickTime * zero7.x;
		velocity_wc.y += fTickTime * zero7.y;
		m_vVecGlobal.x = velocity_wc.y;
		m_vVecGlobal.y = 0f;
		m_vVecGlobal.z = velocity_wc.x;
		Vector3 position = GetPosition();
		position.x += velocity_wc.x * fTickTime;
		position.z += velocity_wc.y * fTickTime;
		if (HeightMapManager.Instance.bValidate)
		{
			position.y = HeightMapManager.Instance.Height(position.x, position.z);
		}
		SetPosition(position);
		float num9 = m_kPhydata.b * zero2.y - m_kPhydata.c * zero3.y;
		float num10 = num9 / m_kPhydata.inertia;
		m_fAngularVelocity += fTickTime * num10;
		m_fYaw += fTickTime * m_fAngularVelocity * 57.29578f;
		SetEulerAngleY(m_fYaw);
	}

	public void doSteering(float fTickTime)
	{
		if (Mathf.Abs(m_fSteeringAngle) < 0.001f || m_eContactState != CONTACTSTATE.CS_FOURWHEEL || m_kRigidBody.isKinematic)
		{
			return;
		}
		GetEulerAngle();
		float num = float.PositiveInfinity;
		if (m_fSteeringAngle != 0f)
		{
			num = m_kPhydata.fWheelBase / Mathf.Sin(m_fSteeringAngle);
		}
		float f = m_vVecGlobal.x * m_vVecGlobal.x + m_vVecGlobal.z * m_vVecGlobal.z;
		f = Mathf.Sqrt(f);
		m_fAngularVelocity = f / num;
		float num2 = fTickTime * m_fAngularVelocity * 57.29578f;
		m_fYaw += num2;
		SetEulerAngleY(m_fYaw);
		int num3 = 0;
		Vector3 up = Vector3.up;
		for (int i = 0; i < 4; i++)
		{
			btWheelInfo btWheelInfo2 = Wheels(i);
			if (btWheelInfo2.m_raycastInfo.m_isInContact)
			{
				num3++;
				up += btWheelInfo2.m_raycastInfo.m_contactNormalWS;
			}
		}
		if (num3 != 0)
		{
			up /= (float)num3;
		}
		Vector3 vector = -Vector3.Cross(up, GetRightGlobal());
		vector.Normalize();
		Vector3 vector2 = Vector3.Cross(up, vector);
		Quaternion rotation = Quaternion.LookRotation(vector, up);
		SetRotation(rotation);
		Vector3 zero = Vector3.zero;
		switch (VelocityDir)
		{
		case ENMMOTIONDIR.ECMD_FORWARD:
			zero = vector * m_vVecGlobal.magnitude;
			break;
		case ENMMOTIONDIR.ECMD_BACK:
			vector = -vector;
			zero = vector * m_vVecGlobal.magnitude;
			break;
		default:
			vector = Vector3.zero;
			zero = Vector3.zero;
			break;
		}
		SetVecGlobal(zero);
	}

	public void SimulateFlying(float fTime, ENMMOTIONDIR VelocityDir)
	{
		for (int i = 0; i < 4; i++)
		{
			rayCast(m_wheelInfo[i]);
			ref Vector3 reference = ref m_arrGroundNormal[i];
			reference = m_wheelInfo[i].m_raycastInfo.m_contactNormalWS;
		}
		m_wheelInfo[0].TurnAngular(0f);
		m_wheelInfo[1].TurnAngular(0f);
		for (int j = 0; j < 4; j++)
		{
			float fSteering = 0f;
			if (j == 0 || j == 1)
			{
				fSteering = base.CurrentSteering;
			}
			m_wheelInfo[j].RotateTurnWheel(0f, fTime, VelocityDir, fSteering);
		}
		UpdateContaceState();
	}

	public void SynchronizeWheel(float fAngle, float fRpm, float fTime, ENMMOTIONDIR VelocityDir)
	{
		iCurIngore++;
		for (int i = 0; i < 4; i++)
		{
			iCurIngore %= 4;
			if (i == iCurIngore)
			{
				float num = rayCast(m_wheelInfo[i]);
				if (num == -1f)
				{
					for (int j = 0; j < 4; j++)
					{
						if (j != iCurIngore)
						{
							rayCast(m_wheelInfo[j]);
							ref Vector3 reference = ref m_arrGroundNormal[j];
							reference = m_wheelInfo[j].m_raycastInfo.m_contactNormalWS;
						}
					}
				}
				else
				{
					int num2 = (iCurIngore + 2) % 4;
					rayCast(m_wheelInfo[num2]);
					ref Vector3 reference2 = ref m_arrGroundNormal[num2];
					reference2 = m_wheelInfo[num2].m_raycastInfo.m_contactNormalWS;
				}
			}
			ref Vector3 reference3 = ref m_arrGroundNormal[i];
			reference3 = m_wheelInfo[i].m_raycastInfo.m_contactNormalWS;
		}
		m_wheelInfo[0].TurnAngular(fAngle);
		m_wheelInfo[1].TurnAngular(fAngle);
		for (int k = 0; k < 4; k++)
		{
			float fSteering = 0f;
			if (k == 0 || k == 1)
			{
				fSteering = base.CurrentSteering;
			}
			m_wheelInfo[k].RotateTurnWheel(fRpm, fTime, VelocityDir, fSteering);
		}
		if (m_kRigidBody.isKinematic)
		{
			return;
		}
		Vector3 angularVelocity = m_kRigidBody.angularVelocity;
		angularVelocity.x = 0f;
		angularVelocity.y = 0f;
		angularVelocity.z = 0f;
		m_kRigidBody.angularVelocity = angularVelocity;
		if (m_wheelInfo[0].m_raycastInfo.m_isInContact != m_wheelInfo[3].m_raycastInfo.m_isInContact)
		{
			int num3 = 0;
			int num4 = 3;
			if (m_wheelInfo[0].m_raycastInfo.m_isInContact)
			{
				num3 = 3;
				num4 = 0;
			}
			btWheelInfo btWheelInfo2 = m_wheelInfo[num3];
			btWheelInfo btWheelInfo3 = m_wheelInfo[num4];
			btWheelInfo2.m_raycastInfo = btWheelInfo3.m_raycastInfo;
			ref Vector3 reference4 = ref m_arrGroundNormal[num3];
			reference4 = m_arrGroundNormal[num4];
		}
		if (m_wheelInfo[1].m_raycastInfo.m_isInContact != m_wheelInfo[2].m_raycastInfo.m_isInContact)
		{
			int num5 = 1;
			int num6 = 2;
			if (m_wheelInfo[1].m_raycastInfo.m_isInContact)
			{
				num5 = 2;
				num6 = 1;
			}
			btWheelInfo btWheelInfo4 = m_wheelInfo[num5];
			btWheelInfo btWheelInfo5 = m_wheelInfo[num6];
			btWheelInfo4.m_raycastInfo = btWheelInfo5.m_raycastInfo;
			ref Vector3 reference5 = ref m_arrGroundNormal[num5];
			reference5 = m_arrGroundNormal[num6];
		}
		UpdateContaceState();
		for (int l = 0; l < getNumWheels(); l++)
		{
			btWheelInfo btWheelInfo6 = m_wheelInfo[l];
			if (btWheelInfo6.m_raycastInfo.m_isInContact)
			{
				float suspensionRestLength = btWheelInfo6.getSuspensionRestLength();
				float suspensionLength = btWheelInfo6.m_raycastInfo.m_suspensionLength;
				float num7 = suspensionRestLength - suspensionLength;
				float num8 = btWheelInfo6.m_suspensionStiffness * num7 * btWheelInfo6.m_clippedInvContactDotSuspension;
				float suspensionRelativeVelocity = btWheelInfo6.m_suspensionRelativeVelocity;
				float num9;
				if ((double)suspensionRelativeVelocity < 0.0)
				{
					float wheelsDampingCompression = btWheelInfo6.m_wheelsDampingCompression;
					num9 = wheelsDampingCompression * suspensionRelativeVelocity;
				}
				else
				{
					float wheelsDampingCompression = btWheelInfo6.m_wheelsDampingRelaxation;
					num9 = wheelsDampingCompression * suspensionRelativeVelocity;
				}
				num8 -= num9;
				btWheelInfo6.m_wheelsSuspensionForce = num8 * m_kRigidBody.mass;
				if (btWheelInfo6.m_wheelsSuspensionForce < 0f)
				{
					btWheelInfo6.m_wheelsSuspensionForce = 0f;
				}
			}
			else
			{
				btWheelInfo6.m_wheelsSuspensionForce = (0f - m_track_connect_accel) * m_kRigidBody.mass;
			}
		}
		for (int m = 0; m < m_wheelInfo.Count; m++)
		{
			btWheelInfo btWheelInfo7 = m_wheelInfo[m];
			float num10 = btWheelInfo7.m_wheelsSuspensionForce;
			float num11 = 6000f;
			if (num10 > num11)
			{
				num10 = num11;
			}
			Vector3 force = btWheelInfo7.m_raycastInfo.m_contactNormalWS * num10;
			Vector3 contactPointWS = btWheelInfo7.m_raycastInfo.m_contactPointWS;
			m_kRigidBody.AddForceAtPosition(force, contactPointWS, ForceMode.Force);
		}
	}

	public override Vector3 GetGroundNormal()
	{
		return m_vGroundNormal;
	}

	private void UpdateContaceState()
	{
		m_state = RUNNINGSTATES.RS_INAIR;
		m_vGroundNormal = Vector3.zero;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < 4; i++)
		{
			if (m_wheelInfo[i].m_raycastInfo.m_isInContact)
			{
				m_vGroundNormal += m_wheelInfo[i].m_raycastInfo.m_contactNormalWS;
				m_state = RUNNINGSTATES.RS_ONEARTH;
				if (i < 2)
				{
					num++;
				}
				else
				{
					num2++;
				}
			}
		}
		m_eContactState = (CONTACTSTATE)(num + num2);
		if (num + num2 > 0)
		{
			m_vGroundNormal /= (float)(num + num2);
			m_vGroundNormal.Normalize();
		}
		else
		{
			m_vGroundNormal = Vector3.up;
		}
	}

	public override Vector3 GetUpGlobal()
	{
		Vector3 up = Vector3.up;
		up = m_CenterofMassTransform.localToWorldMatrix.MultiplyVector(up);
		up.Normalize();
		return up;
	}

	public override Vector3 GetForwardGlobal()
	{
		Vector3 forward = Vector3.forward;
		forward = m_CenterofMassTransform.localToWorldMatrix.MultiplyVector(forward);
		forward.Normalize();
		return forward;
	}

	public override Vector3 GetRightGlobal()
	{
		Vector3 right = Vector3.right;
		right = m_CenterofMassTransform.localToWorldMatrix.MultiplyVector(right);
		right.Normalize();
		return right;
	}

	public override void RotateCarLocal(float fAngleX, float fAngleY, float fAngleZ)
	{
	}

	public override void RotateAroundRearWheelsWorldUp(float fAngle, bool bRotateVelocity)
	{
		Vector3 v = base.vLocalMidRearWheels;
		v = m_CenterofMassTransform.localToWorldMatrix.MultiplyPoint(v);
		m_CenterofMassTransform.RotateAround(v, Vector3.up, fAngle);
		if (bRotateVelocity)
		{
			Quaternion quaternion = Quaternion.AngleAxis(fAngle, Vector3.up);
			Vector3 vecGlobal = quaternion * m_vVecGlobal;
			SetVecGlobal(vecGlobal);
		}
	}

	public override void RotateAroundPoint(Vector3 vOrigo, float fAngle, bool bRotateVelocity)
	{
		m_CenterofMassTransform.RotateAround(vOrigo, GetUpGlobal(), fAngle);
		if (bRotateVelocity)
		{
			Quaternion quaternion = Quaternion.AngleAxis(fAngle, GetUpGlobal());
			Vector3 vecGlobal = quaternion * m_vVecGlobal;
			SetVecGlobal(vecGlobal);
		}
	}

	public override void TranslateCarLocal(float OffsetX, float OffsetY, float OffsetZ)
	{
		Vector3 translation = new Vector3(OffsetX, OffsetY, OffsetZ);
		m_CenterofMassTransform.Translate(translation, Space.Self);
	}

	public override void TranslateCarWorld(Vector3 vOffset)
	{
		m_CenterofMassTransform.Translate(vOffset, Space.World);
	}

	public override void OnDrawGizmos()
	{
		Color color = Gizmos.color;
		Gizmos.color = Color.green;
		for (int i = 0; i < 4; i++)
		{
			Vector3 vector = -GetUpGlobal();
			Vector3 vector2 = vector * m_wheelInfo[i].m_wheelsRadius;
			Vector3 hardPointWS = m_wheelInfo[i].m_raycastInfo.m_hardPointWS;
			m_wheelInfo[i].m_raycastInfo.m_contactPointWS = hardPointWS + vector2;
			Gizmos.DrawSphere(hardPointWS, 0.1f);
			Gizmos.DrawLine(hardPointWS, m_wheelInfo[i].m_raycastInfo.m_contactPointWS);
			Vector3 contactPointWS = m_wheelInfo[i].m_raycastInfo.m_contactPointWS;
			Vector3 to = contactPointWS + 0.01f * m_wheelInfo[i].m_raycastInfo.m_contactNormalWS * m_wheelInfo[i].m_wheelsSuspensionForce;
			Gizmos.DrawLine(contactPointWS, to);
		}
		Gizmos.color = color;
	}
}
