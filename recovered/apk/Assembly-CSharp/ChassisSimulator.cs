using System;
using UnityEngine;

public class ChassisSimulator
{
	private struct VibrationSpring
	{
		public float MaxDistance;

		public float x;

		public float v;

		public float k;

		public float time;
	}

	private const float MIN_FLYHEIGHT = 0.5f;

	public CONTACTSTATE m_eContactState;

	public int m_iContactFlag;

	private Transform[] Wheels = new Transform[4];

	private Transform bodyMesh;

	private Transform seatDummy;

	private Vehicle car;

	private VehicleData m_kPhydata;

	public float fFrontTireRadius;

	public float fRearTireRadius;

	public float fsuspensionDistance;

	private VibrationSpring vspring;

	public Vector3[] m_arrPickNormalPre = new Vector3[4];

	public Vector3[] m_arrPickNormalCur = new Vector3[4];

	private Transform ChassisTransform;

	private GameObject BodyObject;

	private Vector3 m_vGroundNormal = Vector3.up;

	public WheelCollider[] WheelsColliders = new WheelCollider[4];

	public NxWheel[] Nxwheels = new NxWheel[4];

	public Vector3 m_vOffsetFL;

	public Vector3 m_vOffsetFR;

	public Vector3 m_vOffsetRR;

	public Vector3 m_vOffsetRL;

	public float m_fFrontDistance = 0.181f;

	public float m_fBackDistacne = 0.181f;

	private Rigidbody kBody;

	private int m_WheelCount;

	protected bool bFallingDown;

	protected Matrix4x4 m_mtChassis = Matrix4x4.identity;

	protected float[] m_afDistanceToGround = new float[4];

	public Vector3[] m_arrGroundNormal = new Vector3[4];

	public Vector3[] m_arrGroundPoint = new Vector3[4];

	private int[] aIndicesOrg = new int[8];

	private float m_fChassisAdjustTime = 0.25f;

	public Vector3 vFrontRight => ChassisTransform.localToWorldMatrix.MultiplyPoint(m_vOffsetFR);

	public Vector3 vFrontLeft => ChassisTransform.localToWorldMatrix.MultiplyPoint(m_vOffsetFL);

	public Vector3 vFrontMid
	{
		get
		{
			Vector3 v = (m_vOffsetFL + m_vOffsetFR) / 2f;
			return ChassisTransform.localToWorldMatrix.MultiplyPoint(v);
		}
	}

	public Vector3 vBackLeft => ChassisTransform.localToWorldMatrix.MultiplyPoint(m_vOffsetRL);

	public Vector3 vBackRight => ChassisTransform.localToWorldMatrix.MultiplyPoint(m_vOffsetRR);

	public Vector3 vLocalMidFrontWheels => (m_vOffsetFL + m_vOffsetFR) / 2f;

	public Vector3 vLocalMidRearWheels => (m_vOffsetRL + m_vOffsetRR) / 2f;

	public Vector3 vForwardDir => ChassisTransform.localToWorldMatrix.MultiplyVector(Vector3.forward).normalized;

	public ChassisSimulator(Transform[] wheels, float fFrontTireRadius, float fRearTireRadius, float fSpring, float fsuspensionDistance, GameObject body, Vehicle car, VehicleData kData)
	{
		BodyObject = body;
		this.car = car;
		this.fFrontTireRadius = fFrontTireRadius;
		this.fRearTireRadius = fRearTireRadius;
		m_kPhydata = kData;
		this.fsuspensionDistance = fsuspensionDistance;
		bodyMesh = body.transform.FindChild("car/root/body").transform;
		seatDummy = body.transform.FindChild("car/root/seat").transform;
		Transform transform = body.transform.FindChild("WheelColliders");
		for (int i = 0; i < 4; i++)
		{
			Wheels[i] = wheels[i];
			float num = fFrontTireRadius;
			float fYoffset = -0.2f;
			switch (i)
			{
			case 0:
				WheelsColliders[i] = transform.FindChild("LFWheel").GetComponent<WheelCollider>();
				num = fFrontTireRadius;
				break;
			case 1:
				WheelsColliders[i] = transform.FindChild("RFWheel").GetComponent<WheelCollider>();
				num = fFrontTireRadius;
				break;
			case 2:
				WheelsColliders[i] = transform.FindChild("RBWheel").GetComponent<WheelCollider>();
				num = fRearTireRadius;
				break;
			case 3:
				WheelsColliders[i] = transform.FindChild("LBWheel").GetComponent<WheelCollider>();
				num = fRearTireRadius;
				break;
			}
			WheelsColliders[i].radius = num;
			Nxwheels[i] = new NxWheel(this, wheels[i], num, fYoffset, WheelsColliders[i]);
			Nxwheels[i].kWheel = WheelsColliders[i];
			Nxwheels[i].fCurrentRollingAngle = 0f;
			Nxwheels[i].fSteeringAngle = 0f;
			Nxwheels[i].kWheel.gameObject.layer = 18;
		}
		ChassisTransform = transform;
		CaculateMassCenter(body);
		vspring = default(VibrationSpring);
		vspring.k = 2000f;
		vspring.v = 0f;
		vspring.MaxDistance = 0.006f;
		vspring.x = vspring.MaxDistance;
		m_eContactState = CONTACTSTATE.CS_OUTOFCONTROL;
		for (int j = 0; j < 4; j++)
		{
			ref Vector3 reference = ref m_arrGroundNormal[j];
			reference = Vector3.up;
			m_afDistanceToGround[j] = 99999f;
		}
		m_vOffsetFL = new Vector3(-0.5f, -0.3f, 0.68f);
		m_vOffsetFR = new Vector3(0.5f, -0.3f, 0.68f);
		m_vOffsetRR = new Vector3(0.68f, -0.3f, -0.5f);
		m_vOffsetRL = new Vector3(-0.68f, -0.3f, -0.5f);
	}

	public void Uninit()
	{
	}

	private void CaculateMassCenter(GameObject body)
	{
		kBody = body.GetComponent<Rigidbody>();
		Vector3 position = WheelsColliders[0].transform.position;
		Vector3 position2 = WheelsColliders[1].transform.position;
		Vector3 position3 = WheelsColliders[2].transform.position;
		Vector3 position4 = WheelsColliders[3].transform.position;
		Vector3 vector = (position + position2) / 2f;
		Vector3 vector2 = (position4 + position3) / 2f;
		Vector3 position5 = (vector + vector2) / 2f;
		float y = -1.1f * (fFrontTireRadius + fRearTireRadius) / 2f;
		position5 += new Vector3(0f, y, 0f);
		kBody.centerOfMass = body.transform.InverseTransformPoint(position5);
		kBody.centerOfMass = Vector3.zero;
	}

	public void ResetMassCenter()
	{
		CaculateMassCenter(BodyObject);
	}

	public void OffsetMassCenter(float fOffset)
	{
		Vector3 vector = kBody.worldCenterOfMass - GetUpGlobal() * fOffset;
		kBody.centerOfMass = BodyObject.transform.InverseTransformPoint(vector);
		NiLog.Log("vCG = " + vector);
	}

	public Vector3 GetWorldPosition()
	{
		return ChassisTransform.position;
	}

	public void SetWorldPosition(Vector3 vPos)
	{
		ChassisTransform.position = vPos;
	}

	public Matrix4x4 GetMat()
	{
		return ChassisTransform.localToWorldMatrix;
	}

	public Vector3 TransformToWorld(Vector3 vLocal)
	{
		return ChassisTransform.localToWorldMatrix.MultiplyPoint(vLocal);
	}

	public WheelCollider[] GetWheels(out int iWheels)
	{
		iWheels = 4;
		return WheelsColliders;
	}

	public void EnableWheels()
	{
		for (int i = 0; i < 4; i++)
		{
			WheelsColliders[i].gameObject.active = true;
		}
		CaculateMassCenter(BodyObject);
	}

	public void DisableWheels()
	{
		for (int i = 0; i < 4; i++)
		{
			WheelsColliders[i].gameObject.active = false;
		}
	}

	public Vector3 GetWheelsPosition(int index, out Vector3 vNormal)
	{
		Vector3 zero = Vector3.zero;
		vNormal = Vector3.up;
		if (index < 0)
		{
			index = 0;
		}
		if (index > 3)
		{
			index = 3;
		}
		vNormal = Nxwheels[index].vGroundNormal;
		return Nxwheels[index].position;
	}

	public void MoveWheels(int idx, Vector3 vPos)
	{
		if (idx < 4)
		{
			Transform child = Wheels[idx].GetChild(0);
			child.localPosition = Wheels[idx].InverseTransformPoint(vPos);
		}
	}

	public void SimpleHarmonicVibration(float fTickTime)
	{
		vspring.time += fTickTime;
		vspring.v -= vspring.k * vspring.x * fTickTime;
		vspring.x += vspring.v * fTickTime;
		if (vspring.x > vspring.MaxDistance)
		{
			vspring.x = vspring.MaxDistance;
			vspring.v = 0f;
		}
		if (vspring.x < 0f - vspring.MaxDistance)
		{
			vspring.x = 0f - vspring.MaxDistance;
			vspring.v = 0f;
		}
		bodyMesh.GetChild(0).transform.localPosition = new Vector3(0f, 0f, vspring.x);
		seatDummy.Translate(0f, 0f, vspring.x * 0.5f, Space.Self);
	}

	public void SynchronizeWheel(float fAngle, float fRpm, float fTime, ENMMOTIONDIR VelocityDir)
	{
		UpdateDistToGround();
		Nxwheels[0].TurnAngular(fAngle);
		Nxwheels[1].TurnAngular(fAngle);
		for (int i = 0; i < 4; i++)
		{
			if (!car.GetLockMovement() || (i != 0 && i != 1))
			{
				float fSteering = 0f;
				if (i == 0 || i == 1)
				{
					fSteering = car.CurrentSteering;
				}
				Nxwheels[i].RotateTurnWheel(fRpm, fTime, VelocityDir, fSteering);
			}
		}
		UpdateContaceState();
		m_WheelCount++;
		m_WheelCount %= 4;
		car.SetWheelTerrainType(Nxwheels[m_WheelCount]);
	}

	public void SimulateFlying(float fTime, ENMMOTIONDIR VelocityDir)
	{
		UpdateDistToGround();
		Nxwheels[0].TurnAngular(0f);
		Nxwheels[1].TurnAngular(0f);
		for (int i = 0; i < 4; i++)
		{
			float fSteering = 0f;
			if (i == 0 || i == 1)
			{
				fSteering = car.CurrentSteering;
			}
			Nxwheels[i].RotateTurnWheel(0f, fTime, VelocityDir, fSteering);
		}
		if (car.m_state == RUNNINGSTATES.RS_INAIR)
		{
			Vector3 vWorldPos = car.GetPosition() + m_kPhydata.fVehicleHeight * Vector3.up;
			Vector3 down = Vector3.down;
			if (CollisionLayer.Instance.CheckHitDeepWater(vWorldPos, down, out var vWaterPoint))
			{
				float num = car.GetPosition().y - vWaterPoint.y;
				if (num <= 0.1f)
				{
					ABMessageData data = new COM_DATA();
					((COM_DATA)data).iValue = 64;
					((COM_DATA)data).vValue = vWaterPoint;
					CTMessagePipeline.sendMessage(car.m_kUserData, 1001, ref data);
				}
			}
		}
		UpdateContaceState();
	}

	private float GetMinDistToGround()
	{
		float num = 99999f;
		for (int i = 0; i < 4; i++)
		{
			float num2 = ((i >= 2) ? m_fBackDistacne : m_fFrontDistance);
			float num3 = m_afDistanceToGround[i] - num2;
			if (num3 < num)
			{
				num = num3;
			}
		}
		return num;
	}

	public float GetDistToGround(int iWheelIndex)
	{
		return m_afDistanceToGround[iWheelIndex];
	}

	private void SortDec<T>(T[] aV, int[] aIndices, int num) where T : IComparable<T>
	{
		for (int i = 0; i < num; i++)
		{
			aIndicesOrg[i] = i;
		}
		for (int num2 = num; num2 > 0; num2--)
		{
			for (int j = 0; j < num2 - 1; j++)
			{
				ref T reference = ref aV[aIndicesOrg[j]];
				T other = aV[aIndicesOrg[j + 1]];
				if (reference.CompareTo(other) < 0)
				{
					int num3 = aIndicesOrg[j];
					aIndicesOrg[j] = aIndicesOrg[j + 1];
					aIndicesOrg[j + 1] = num3;
				}
			}
		}
		for (int k = 0; k < num; k++)
		{
			aIndices[k] = aIndicesOrg[k];
		}
	}

	private void UpdateDistToGround()
	{
		int num = 0;
		Vector3 zero = Vector3.zero;
		Vector3 vector = -GetUpGlobal();
		for (int i = 0; i < 4; i++)
		{
			Vector3 vector2 = ChassisTransform.localToWorldMatrix.MultiplyPoint(Nxwheels[i].LocalOrignPosition);
			if (CollisionLayer.Instance.RaycastDistancetoGround(vector2 + Vector3.up, Vector3.down, 30f, out var vGroundNormal, out var vHitPoint))
			{
				m_arrGroundPoint[i] = vHitPoint;
				vHitPoint = vector2 - vHitPoint;
				m_afDistanceToGround[i] = vHitPoint.y;
			}
			else
			{
				num++;
				zero = vector2;
				m_afDistanceToGround[i] = 9999f;
				vGroundNormal = Vector3.up;
				ref Vector3 reference = ref m_arrGroundPoint[i];
				reference = new Vector3(vector2.x, -9999f, vector2.z);
			}
			m_arrGroundNormal[i] = vGroundNormal;
		}
	}

	private void ChassisAdjust()
	{
		if (car.m_state != RUNNINGSTATES.RS_INAIR)
		{
			return;
		}
		Vector3 angularVelocity = car.m_kRigidBody.angularVelocity;
		angularVelocity.x = 0f;
		angularVelocity.z = 0f;
		if (!car.m_kRigidBody.isKinematic)
		{
			car.m_kRigidBody.angularVelocity = angularVelocity;
		}
		Quaternion rotation = car.m_kRigidBody.rotation;
		Vector3 eulerAngles = rotation.eulerAngles;
		if (eulerAngles.x < 179f)
		{
			if (eulerAngles.x > 0f)
			{
				eulerAngles.x -= Time.deltaTime * 90f;
			}
			if (eulerAngles.x < 0f)
			{
				eulerAngles.x = 0f;
			}
		}
		if (eulerAngles.x > 181f)
		{
			if (eulerAngles.x < 360f)
			{
				eulerAngles.x += Time.deltaTime * 90f;
			}
			if (eulerAngles.x >= 360f)
			{
				eulerAngles.x = 0f;
			}
		}
		if (eulerAngles.z < 179f)
		{
			if (eulerAngles.z > 0f)
			{
				eulerAngles.z -= Time.deltaTime * 90f;
			}
			if (eulerAngles.z < 0f)
			{
				eulerAngles.z = 0f;
			}
		}
		if (eulerAngles.z > 181f)
		{
			if (eulerAngles.z < 360f)
			{
				eulerAngles.z += Time.deltaTime * 90f;
			}
			if (eulerAngles.z >= 360f)
			{
				eulerAngles.z = 0f;
			}
		}
		rotation.eulerAngles = eulerAngles;
		car.m_CenterofMassTransform.eulerAngles = eulerAngles;
	}

	private void UpdateContaceState()
	{
		RUNNINGSTATES state = car.m_state;
		if (GetMinDistToGround() < 0.5f + m_kPhydata.fChassisHeight)
		{
			car.m_state = RUNNINGSTATES.RS_ONEARTH;
			m_fChassisAdjustTime = 0.25f;
		}
		else
		{
			car.m_state = RUNNINGSTATES.RS_INAIR;
			m_fChassisAdjustTime -= Time.deltaTime;
		}
		if (m_fChassisAdjustTime <= 0f)
		{
			m_fChassisAdjustTime = 0f;
		}
		if (!car.m_kRigidBody.isKinematic && state == RUNNINGSTATES.RS_INAIR && car.m_state == RUNNINGSTATES.RS_ONEARTH)
		{
			Vector3 velocity = car.m_kRigidBody.velocity;
			if (velocity.y < -10f)
			{
				velocity.y = -10f;
				car.m_kRigidBody.velocity = velocity;
			}
		}
		m_iContactFlag = 0;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < 4; i++)
		{
			if (Nxwheels[i].bGrounded)
			{
				if (i < 2)
				{
					num++;
				}
				else
				{
					num2++;
				}
				m_iContactFlag |= 1 << i;
			}
		}
		m_eContactState = (CONTACTSTATE)(num + num2);
		int[] aIndices = new int[4];
		SortDec(m_afDistanceToGround, aIndices, 4);
		m_vGroundNormal = -Vector3.Cross(m_arrGroundPoint[2] - m_arrGroundPoint[1], m_arrGroundPoint[2] - m_arrGroundPoint[0]);
		m_vGroundNormal.Normalize();
	}

	public Vector3 GetUpGlobal()
	{
		Vector3 up = Vector3.up;
		up = ChassisTransform.localToWorldMatrix.MultiplyVector(up);
		up.Normalize();
		return up;
	}

	public Vector3 GetForwardGlobal()
	{
		Vector3 forward = Vector3.forward;
		forward = ChassisTransform.localToWorldMatrix.MultiplyVector(forward);
		forward.Normalize();
		return forward;
	}

	public Vector3 GetRightGlobal()
	{
		Vector3 right = Vector3.right;
		right = ChassisTransform.localToWorldMatrix.MultiplyVector(right);
		right.Normalize();
		return right;
	}

	public Quaternion GetRootRotate()
	{
		return ChassisTransform.rotation;
	}

	public Vector3 LocalToWroldPoint(Vector3 vPos)
	{
		return ChassisTransform.localToWorldMatrix.MultiplyPoint(vPos);
	}

	public Vector3 GetVelGlobal()
	{
		return car.GetVecGlobal();
	}

	public Vector3 GetGroundNormal()
	{
		return m_vGroundNormal;
	}
}
