using System;
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

	public Vector2 FrontSize = new Vector2(0.5f, 0.68f);

	public Vector2 RearSize = new Vector2(0.63f, 0.5f);

	public Vector3 vTractiveForce = Vector3.zero;

	public Vector3 AirFriction = Vector3.zero;

	public Vector3 vGroundFriction = Vector3.zero;

	public int m_bEnableForceRot;

	public VehicleShift m_kShift;

	private float fTopSpeedLimitCoeff = 1f;

	private float fLockTime;

	public float fRollingCoefficient = 0.02f;

	public float fBumpAngle;

	private float m_fCurScaleFactore = 1f;

	private float m_fBackwardCoolTime;

	protected float m_fLastMotorForce;

	protected float m_fLastBrakeForce;

	protected float m_fFlyingTime;

	protected Collider m_curPlatformCollider;

	public FlySimulator m_Flysimulator;

	public Collider cHead;

	public Collider cTail;

	public Collider cBody;

	private Vector3 ejectSpeed = Vector3.zero;

	private float m_fLastLandingTime;

	private FallingSimulator m_kFallingSimulator;

	private bool m_bLockMovement;

	public float m_fTireTemperature;

	private float m_fPreYaw;

	private float m_fPrePitch;

	private float m_fPreBank;

	private bool m_bLockPose;

	private bool m_bEject;

	private Vector3 curForce = Vector3.zero;

	public bool bEject => m_bEject;

	public Vehicle(VehicleData phydata, Transform bodyTransform, Transform lFrontWheel, Transform rFrontWheel, Transform lRearWheel, Transform rRearWheel, EnmCollisionLayer clLayer, CollisionCallback callback)
		: base(phydata, bodyTransform, lFrontWheel, rFrontWheel, lRearWheel, rRearWheel)
	{
		m_bVisible = true;
		GroundedType = ENMTERRAIN_TYPE.ETT_NONE;
		chassis = new ChassisSimulator(wheels, fFrontTireRadius, fRearTireRadius, fSpring, fsuspensionDistance, bodyTransform.gameObject, this, m_kPhydata);
		m_kCompositeCollider = new BodyComponent(bodyTransform.gameObject, this, EnmCollisionLayer.CL_PLAYER, callback);
		m_kCarCollisionCenter = new CarCollisionDetection();
		m_kCarCollisionCenter.Init(m_kCompositeCollider, callback, this);
		motorcontrol = new MotorControl(m_kPhydata);
		m_kShift = new VehicleShift(this);
		m_kShift.m_carModel = callback as Car;
		m_kShift.SetPhyXData(phydata);
		m_Flysimulator = new FlySimulator(this);
		m_kFallingSimulator = new FallingSimulator(this);
		m_kCollsionSimulator = new VehicleCollisionSimulator(this);
		m_kBounceSimulator = new BounceSimulator(this);
		m_suspension = bodyTransform.gameObject.AddComponent<Suspension>();
		m_suspension.Init(this, m_fMass);
		btVehicleBase.ms_ListVehicel.Add(this);
		m_kRigidBody.maxAngularVelocity = 1.8f;
		Flag_ProccessCarToFenceEx = false;
		Flag_ProccessCarToTreeAndBox = false;
	}

	public void StopWheelDistanceVisual()
	{
		for (int i = 0; i < 4; i++)
		{
			chassis.Nxwheels[i].StopWheelDistanceVisual();
		}
	}

	public override void Uninit()
	{
		btVehicleBase.ms_ListVehicel.Remove(this);
		m_kCarCollisionCenter.Unint();
		chassis.Uninit();
		m_kCompositeCollider.Uninit();
		base.Uninit();
	}

	private void UpdateVehiclePose()
	{
		if (!AdjustVehiclePose())
		{
			Vector3 eulerAngles = m_CenterofMassTransform.rotation.eulerAngles;
			m_fYaw = eulerAngles.y;
			m_fPitch = eulerAngles.x;
			m_fBank = eulerAngles.z;
			if (m_fBank > 180f)
			{
				m_fBank = Mathf.Clamp(m_fBank, 280f, 360f);
			}
			if (m_fBank < 180f)
			{
				m_fBank = Mathf.Clamp(m_fBank, 0f, 80f);
			}
			if (m_fPitch > 180f)
			{
				m_fPitch = Mathf.Clamp(m_fPitch, 280f, 360f);
			}
			if (m_fPitch < 180f)
			{
				m_fPitch = Mathf.Clamp(m_fPitch, 0f, 80f);
			}
			if (m_bLockPose)
			{
				m_fYaw = m_fPreYaw;
				m_fPitch = m_fPrePitch;
				m_fBank = m_fPreBank;
				m_CenterofMassTransform.rotation = Quaternion.Euler(m_fPitch, m_fYaw, m_fBank);
			}
			else
			{
				m_fPreYaw = m_fYaw;
				m_fPrePitch = m_fPitch;
				m_fPreBank = m_fBank;
				m_CenterofMassTransform.rotation = Quaternion.Euler(m_fPitch, m_fYaw, m_fBank);
			}
		}
	}

	private bool AdjustVehiclePose()
	{
		if (!HeightMapManager.Instance.bValidate)
		{
			return false;
		}
		Vector3[] array = new Vector3[4] { chassis.vFrontLeft, chassis.vFrontRight, chassis.vBackRight, chassis.vBackLeft };
		Vector3[] array2 = new Vector3[4];
		float[] array3 = new float[4];
		float a = 10f;
		for (int i = 0; i < 4; i++)
		{
			float num = HeightMapManager.Instance.Height(array[i].x, array[i].z);
			array3[i] = array[i].y - num;
			a = Mathf.Min(a, array3[i]);
			ref Vector3 reference = ref array2[i];
			reference = HeightMapManager.Instance.Normal(array[i].x, array[i].y, array[i].z);
		}
		Vector3 to = (array2[0] + array2[1]) / 2f;
		to.Normalize();
		float num2 = Vector3.Angle(chassis.GetForwardGlobal(), to);
		if (num2 > 120f && m_state == RUNNINGSTATES.RS_ONEARTH)
		{
			m_eLockMotion = ENMLOCKMOTION.ELM_MOVE_ZAXIS;
		}
		else
		{
			m_eLockMotion = ENMLOCKMOTION.ELM_LOCKNONE;
		}
		if (!(num2 < 45f) || m_state == RUNNINGSTATES.RS_ONEARTH)
		{
		}
		return false;
	}

	public override void Hide()
	{
		m_bVisible = false;
		GroundedType = ENMTERRAIN_TYPE.ETT_NONE;
		chassis.DisableWheels();
		MeshRenderer[] componentsInChildren = m_CenterofMassTransform.FindChild("car/root").GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
		m_CenterofMassTransform.FindChild("collision").gameObject.SetActiveRecursively(state: false);
	}

	public override void Show()
	{
		m_bVisible = true;
		for (int i = 0; i < m_CenterofMassTransform.childCount; i++)
		{
			m_CenterofMassTransform.GetChild(i).gameObject.active = true;
		}
		chassis.EnableWheels();
		MeshRenderer[] componentsInChildren = m_CenterofMassTransform.FindChild("car/root").GetComponentsInChildren<MeshRenderer>();
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].enabled = true;
		}
		m_CenterofMassTransform.FindChild("collision").gameObject.SetActiveRecursively(state: true);
	}

	public override bool CheckCanDriveNow()
	{
		if (m_kCarCollisionCenter.IsSimulating())
		{
			return true;
		}
		return m_state != RUNNINGSTATES.RS_INAIR;
	}

	public override void Update()
	{
		m_vPreVelGlobal = m_vVecGlobal;
		vForwardDir = chassis.vForwardDir;
		if (m_kRigidBody.isKinematic)
		{
			if (m_eRunnigMode == RUNNING_MDOE.RM_FLYING)
			{
				m_vVecGlobal = m_Flysimulator.GetVelocity();
			}
		}
		else
		{
			m_vVecGlobal = m_kRigidBody.velocity;
		}
		m_kCompositeCollider.SynchronousSpherebuddy(GetPosition());
		UpdateVehicleState();
		vMassCenter = m_kRigidBody.worldCenterOfMass;
	}

	public float GetMotorForce(out float braketorque)
	{
		float num = 0f;
		braketorque = 0f;
		num = motorcontrol.GetMotorForce(m_fRPM, m_fSteering, AccelDir, 0);
		if (m_bBrake || m_bBreakVehicleOnce)
		{
			if (VelocityDir == ENMMOTIONDIR.ECMD_FORWARD)
			{
				braketorque = 100f;
				num = 0f;
			}
			else if (!m_bBreakVehicleOnce)
			{
				braketorque = 0f;
				num = 0f - num;
			}
		}
		return num * m_kPhydata.GetMotorFactor(m_fMaxVelocity);
	}

	public override Vector3 GetCurrentMotorForce()
	{
		return vTractiveForce;
	}

	private void ValidatePhysicsParam()
	{
		if (m_vVecGlobal.y < -100f)
		{
			m_vVecGlobal.y = -100f;
			SetVecGlobal(m_vVecGlobal);
		}
	}

	public override void UpdateRunningRegion()
	{
		Vector3 position = GetPosition();
		bool flag = CollisionLayer.Instance.IsInShallowWater(position);
		bool flag2 = CollisionLayer.Instance.IsInDeepWater(position);
		bool flag3 = false;
		if (InDeepWater != flag2)
		{
			if (flag2)
			{
				Debug.Log("Enter Deep");
				flag3 = true;
				ABMessageData data = new COM_DATA();
				((COM_DATA)data).iValue = 64;
				((COM_DATA)data).vValue = position;
				CTMessagePipeline.sendMessage(m_kUserData, 1001, ref data);
			}
			InDeepWater = flag;
		}
		if (InShallowWater != flag)
		{
			if (!flag || !flag3)
			{
			}
			InShallowWater = flag;
		}
	}

	public override void UpdateVehicle()
	{
		vTractiveForce = Vector3.zero;
		AirFriction = Vector3.zero;
		vGroundFriction = Vector3.zero;
		float fixedDeltaTime = Time.fixedDeltaTime;
		UpdateBoost(fixedDeltaTime);
		if (m_eRunnigMode != RUNNING_MDOE.RM_GROUNDRUNING && m_kShift.IsShiftMode())
		{
			m_kShift.BreakShift();
		}
		if (GetLockMovement() && m_kShift.IsShiftMode())
		{
			m_kShift.BreakShift();
		}
		switch (m_eRunnigMode)
		{
		case RUNNING_MDOE.RM_GROUNDRUNING:
		{
			ValidatePhysicsParam();
			m_fLastMotorForce = 0f;
			m_fLastBrakeForce = 0f;
			float fAngle = CaculateSteering(m_vVecGlobal.magnitude);
			if (m_bBreakVehicleOnce)
			{
				fAngle = (m_fSteering = 0f);
			}
			if (m_kShift.IsShiftMode())
			{
				fAngle = m_kShift.AdjustShiftSteering(m_fSteering, fAngle);
			}
			chassis.SynchronizeWheel(fAngle, m_fRPM, fixedDeltaTime, VelocityDir);
			chassis.SimpleHarmonicVibration(fixedDeltaTime);
			m_suspension.Simulate(fixedDeltaTime);
			m_kShift.UpdateShift();
			UpdateGravity(fixedDeltaTime);
			UpdateRepulsion(fixedDeltaTime);
			CalculateFriction(fixedDeltaTime);
			float num = 0f;
			float braketorque = 0f;
			num = GetMotorForce(out braketorque);
			if (m_CarTorqueFlag != 0)
			{
				if (m_CarTorqueFlag == 1)
				{
					num *= m_kPhydata.fBigTorqueScale;
				}
				else if (m_CarTorqueFlag == 2)
				{
					num *= m_kPhydata.fSmallTorqueScale;
				}
			}
			if (m_kShift.IsShiftMode())
			{
				num = m_kShift.ModifyTorque(num, vGroundFriction.magnitude);
			}
			GroundTick(m_bBrake, num, braketorque, fixedDeltaTime);
			m_fLastMotorForce = num;
			m_fLastBrakeForce = braketorque;
			TriggerForceTwistPool(fixedDeltaTime);
			break;
		}
		case RUNNING_MDOE.RM_FLYING:
			UpdateEjectMode(fixedDeltaTime, VelocityDir);
			break;
		case RUNNING_MDOE.RM_LANDING:
			ValidatePhysicsParam();
			chassis.SynchronizeWheel(0f, m_fRPM, fixedDeltaTime, VelocityDir);
			m_suspension.Simulate(fixedDeltaTime);
			UpdateGravity(fixedDeltaTime);
			UpdateLandingMode(fixedDeltaTime);
			TriggerForceTwistPool(fixedDeltaTime);
			break;
		case RUNNING_MDOE.RM_FALLINGDOWN:
			chassis.SynchronizeWheel(0f, m_fRPM, fixedDeltaTime, VelocityDir);
			UpdateGravity(fixedDeltaTime);
			UpdateFallingMode(fixedDeltaTime, VelocityDir);
			TriggerForceTwistPool(fixedDeltaTime);
			break;
		case RUNNING_MDOE.RM_SIDESLIP:
		{
			UpdateGravity(fixedDeltaTime);
			CalculateFriction(fixedDeltaTime);
			float num = 0f;
			float braketorque = 0f;
			num = GetMotorForce(out braketorque);
			GroundTick(m_bBrake, num, braketorque, fixedDeltaTime);
			m_fLastMotorForce = num;
			m_fLastBrakeForce = braketorque;
			TriggerForceTwistPool(fixedDeltaTime);
			UpdateSideSlipMode(fixedDeltaTime);
			UpdateRepulsion(fixedDeltaTime);
			break;
		}
		case RUNNING_MDOE.RM_BOUNCE:
		{
			UpdateGravity(fixedDeltaTime);
			CalculateFriction(fixedDeltaTime);
			float num = 0f;
			float braketorque = 0f;
			num = GetMotorForce(out braketorque);
			GroundTick(m_bBrake, num, braketorque, fixedDeltaTime);
			m_fLastMotorForce = num;
			m_fLastBrakeForce = braketorque;
			TriggerForceTwistPool(fixedDeltaTime);
			UpdateBounceMode(fixedDeltaTime);
			break;
		}
		}
		UpdateVehiclePose();
		UpdateRunningRegion();
		if (!m_kRigidBody.isKinematic)
		{
			if (m_kRigidBody.angularVelocity.y > 1.78f)
			{
				Vector3 angularVelocity = m_kRigidBody.angularVelocity;
				angularVelocity.y = 1f;
				m_kRigidBody.angularVelocity = angularVelocity;
				m_kRigidBody.angularDrag = 1f;
			}
			else
			{
				m_kRigidBody.angularDrag = 0.05f;
			}
		}
	}

	public override Vector3 GetwheelsPosition(int idx)
	{
		return chassis.Nxwheels[idx].GetPosition();
	}

	public override bool MoveToGround(Vector3 vPos)
	{
		Vector3 vWorldPos = vPos;
		vWorldPos.y += 100f;
		if (!CollisionLayer.Instance.VerticalIntersectHitPosition(vWorldPos, out var _, out var vHitPoint))
		{
			return false;
		}
		SetPosition(vHitPoint);
		Vector3 vector = GetwheelsPosition(0);
		Vector3 vector2 = GetwheelsPosition(1);
		Vector3 vector3 = GetwheelsPosition(2);
		Vector3 vector4 = GetwheelsPosition(3);
		Vector3 down = Vector3.down;
		Vector3 vector5 = -down * 10f;
		Vector3 vector6 = ((!CollisionLayer.Instance.RaycastDistancetoGround(vector + vector5, down, 30f, out var _, out vector6)) ? vector : vector6);
		Vector3 vector7 = ((!CollisionLayer.Instance.RaycastDistancetoGround(vector2 + vector5, down, 30f, out var _, out vector7)) ? vector2 : vector7);
		Vector3 vector8 = ((!CollisionLayer.Instance.RaycastDistancetoGround(vector3 + vector5, down, 30f, out var _, out vector8)) ? vector3 : vector8);
		Vector3 vector9 = ((!CollisionLayer.Instance.RaycastDistancetoGround(vector4 + vector5, down, 30f, out var _, out vector9)) ? vector4 : vector9);
		float a = vector.y - vector6.y - chassis.fFrontTireRadius;
		float b = vector2.y - vector7.y - chassis.fFrontTireRadius;
		float b2 = vector3.y - vector8.y - chassis.fRearTireRadius;
		float b3 = vector4.y - vector9.y - chassis.fRearTireRadius;
		float a2 = Mathf.Min(a, b);
		a2 = Mathf.Min(a2, b2);
		a2 = Mathf.Min(a2, b3);
		vHitPoint += a2 * down;
		SetPosition(vHitPoint);
		return true;
	}

	private void GroundTick(bool handBrake, float motorFroce, float braketorque, float timestamp)
	{
		int iWheels;
		WheelCollider[] array = chassis.GetWheels(out iWheels);
		int num = 0;
		Vector3 up = Vector3.up;
		for (int i = 0; i < 4; i++)
		{
			if (array[i].isGrounded)
			{
				num++;
				array[i].brakeTorque = braketorque;
				up += chassis.Nxwheels[i].vGroundNormal;
			}
		}
		if (fLockTime > 0f)
		{
			fLockTime -= Time.deltaTime;
			if (fLockTime <= 0f)
			{
				fLockTime = 0f;
				m_kRigidBody.maxAngularVelocity = 1.8f;
			}
		}
		if (num != 0)
		{
			up /= (float)num;
		}
		vTractiveForce = Vector3.zero;
		if ((chassis.m_iContactFlag & 8) > 0 && (chassis.m_iContactFlag & 4) > 0)
		{
			if (!m_bLockMovement && motorFroce != 0f && fLockTime == 0f)
			{
				Vector3 vector = -Vector3.Cross(up, chassis.GetRightGlobal());
				vector.Normalize();
				curForce = vector;
				float velocity = GetVelocity();
				float num2 = velocity - base.fLimitSpeed;
				if (num2 >= -0.02f)
				{
					fTopSpeedLimitCoeff -= 1f * Time.deltaTime;
					if (fTopSpeedLimitCoeff < 0.1f)
					{
						fTopSpeedLimitCoeff = 0.1f;
					}
				}
				else
				{
					fTopSpeedLimitCoeff = 1f;
				}
				vTractiveForce = vector * motorFroce * 10f * fTopSpeedLimitCoeff;
				AddForceGlobal(vTractiveForce, ENMFORCETYPE.EFT_PULL, timestamp);
				if (m_bEnableForceRot != 0)
				{
					Vector3 vForce = m_bEnableForceRot * chassis.GetRightGlobal() * motorFroce * 1f;
					m_bEnableForceRot = 0;
					AddForceAtPointGlobal(vForce, chassis.vFrontMid, timestamp);
				}
			}
		}
		else if (!m_kRigidBody.isKinematic)
		{
			fLockTime = 0.25f;
			m_kRigidBody.maxAngularVelocity = 0.5f;
		}
		if (!(m_kBoostVar.m_fBoostTime > 0f) || m_kRigidBody.isKinematic || m_LockSpeed)
		{
			return;
		}
		if (m_kBoostVar.m_bPauseBoost)
		{
			m_kBoostVar.m_bPauseBoost = false;
		}
		else if (chassis.m_eContactState == CONTACTSTATE.CS_FOURWHEEL)
		{
			float num3 = m_kBoostVar.m_fBoostMaxSpeed;
			if (num3 < 1f)
			{
				num3 = m_kPhydata.m_fextremumVelocity;
			}
			float num4 = num3 - GetVelocity();
			if (num4 > 0f)
			{
				Vector3 force = GetForwardGlobal() * (num4 * Time.deltaTime * 5f);
				m_kRigidBody.AddForce(force, ForceMode.VelocityChange);
			}
		}
	}

	private void UpdateVehicleState()
	{
		PreVelocityDir = VelocityDir;
		m_fBackwardCoolTime -= Time.deltaTime;
		if (m_vVecGlobal.magnitude > 0.1f)
		{
			float num = Vector3.Dot(vForwardDir, m_vVecGlobal);
			if (num > 0f)
			{
				VelocityDir = ENMMOTIONDIR.ECMD_FORWARD;
			}
			else
			{
				VelocityDir = ENMMOTIONDIR.ECMD_BACK;
			}
			m_fRPM = m_vVecGlobal.magnitude / fFrontTireRadius * 30f / (float)Math.PI * m_fGearRate;
		}
		else if (m_bLockMovement)
		{
			m_vVecGlobal = Vector3.zero;
			VelocityDir = ENMMOTIONDIR.ECMD_STOP;
			if (AccelDir == ENMMOTIONDIR.ECMD_FORWARD)
			{
				m_fRPM = ((!(m_fRPM > 3000f)) ? (m_fRPM += 1000f * Time.deltaTime) : 3000f);
			}
			else
			{
				m_fRPM -= 5000f * Time.deltaTime;
				m_fRPM = ((!(m_fRPM < 0f)) ? m_fRPM : 0f);
			}
		}
		else
		{
			m_vVecGlobal = Vector3.zero;
			VelocityDir = ENMMOTIONDIR.ECMD_STOP;
			m_fRPM = 0f;
		}
		if (m_bBreakVehicleOnce && m_vVecGlobal.magnitude < 1f)
		{
			m_bBreakVehicleOnce = false;
		}
	}

	private Vector3 CaculateAirFriction(Vector3 v)
	{
		AirFriction = -v;
		AirFriction.y = 0f;
		AirFriction.Normalize();
		float num = v.x * v.x + v.z * v.z;
		float num2 = v.y * v.y;
		if (m_state == RUNNINGSTATES.RS_INAIR)
		{
			AirFriction = AirFriction * fAeroDynamicCoeff * num * 10f;
			if (v.y < 0f)
			{
				AirFriction += Vector3.down * fAeroDynamicCoeff * num2 * 50f;
			}
		}
		else
		{
			AirFriction = AirFriction * fAeroDynamicCoeff * num * 10f;
		}
		return AirFriction;
	}

	private void CalculateFriction(float fTickTime)
	{
		Vector3 vVecGlobal = m_vVecGlobal;
		Vector3 vector = (0f - Vector3.Dot(vVecGlobal, vForwardDir)) * vForwardDir;
		vVecGlobal.Normalize();
		Vector3 vForce = CaculateAirFriction(m_vVecGlobal);
		AddForceGlobal(vForce, ENMFORCETYPE.EFT_NORMAL, fTickTime);
		vGroundFriction = Vector3.zero;
		if (m_state == RUNNINGSTATES.RS_ONEARTH)
		{
			float num = m_kRigidBody.mass * 9.8f * fRollingCoefficient;
			Vector3 vector2 = vector * num;
			AddForceGlobal(vector2, ENMFORCETYPE.EFT_NORMAL, fTickTime);
			Vector3 vector3 = CalculateGroundFriction(fTickTime, vector);
			AddForceGlobal(vector3, ENMFORCETYPE.EFT_FRICTROLL, fTickTime);
			vGroundFriction = vector2 + vector3;
		}
		if ((VelocityDir == ENMMOTIONDIR.ECMD_FORWARD || VelocityDir == ENMMOTIONDIR.ECMD_BACK) && AccelDir == ENMMOTIONDIR.ECM_NONE && m_state == RUNNINGSTATES.RS_ONEARTH && !m_kBoostVar.m_bBoost)
		{
			if (m_vVecGlobal.magnitude > 10f)
			{
				Vector3 vector4 = -vVecGlobal * m_kRigidBody.mass * 18f;
				AddForceGlobal(vector4, ENMFORCETYPE.EFT_NORMAL, fTickTime);
				vGroundFriction += vector4;
			}
			else
			{
				Vector3 vector5 = -vVecGlobal * m_kRigidBody.mass * 5f;
				AddForceGlobal(vector5, ENMFORCETYPE.EFT_NORMAL, fTickTime);
				vGroundFriction += vector5;
			}
		}
	}

	private Vector3 CalculateGroundFriction(float fTickTime, Vector3 vDragDir)
	{
		Vector3 result = Vector3.zero;
		if (m_state == RUNNINGSTATES.RS_INAIR || !m_bGroundFriction)
		{
			return result;
		}
		int num = 0;
		float num2 = 0f;
		for (int i = 0; i < 4; i++)
		{
			if (chassis.Nxwheels[i].bGrounded)
			{
				PhysicMaterial groundPhyMaterial = chassis.Nxwheels[i].GroundPhyMaterial;
				num2 = ((!(groundPhyMaterial != null)) ? (num2 + 0.5f) : (num2 + groundPhyMaterial.staticFriction));
				num++;
			}
		}
		if (num > 0)
		{
			num2 = (num2 - (float)num * 0.5f) / (float)num;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			float velocity = GetVelocity();
			float num3 = velocity / 5f;
			result = m_kRigidBody.mass * 9.8f * num2 * vDragDir * num3;
		}
		return result;
	}

	private void UpdateGravity(float fTickTime)
	{
		if (m_state == RUNNINGSTATES.RS_INAIR)
		{
			AddForceGlobal(m_vGravity, ENMFORCETYPE.EFT_GRAVITY, fTickTime);
			return;
		}
		AddForceGlobal(m_vGravity, ENMFORCETYPE.EFT_GRAVITY, fTickTime);
		if (m_eRunnigMode != RUNNING_MDOE.RM_BOUNCE && m_eRunnigMode != RUNNING_MDOE.RM_FLYING && m_eRunnigMode != RUNNING_MDOE.RM_FALLINGDOWN && m_eRunnigMode != RUNNING_MDOE.RM_SIDESLIP)
		{
			Vector3 groundNormal = chassis.GetGroundNormal();
			Vector3 lhs = m_vGravity - groundNormal * Vector3.Dot(m_vGravity, groundNormal);
			lhs = (0f - Vector3.Dot(lhs, vForwardDir)) * vForwardDir;
			AddForceGlobal(lhs, ENMFORCETYPE.EFT_GRAVITY, fTickTime);
		}
	}

	public void UpdateRepulsion(float fTickTime)
	{
		foreach (btVehicleBase item in btVehicleBase.ms_ListVehicel)
		{
			if (item == this)
			{
				continue;
			}
			Vector3 position = GetPosition();
			Vector3 position2 = item.GetPosition();
			position = position2 - position;
			position.y = 0f;
			float magnitude = position.magnitude;
			float num = GetBody().BoundRadius + item.GetBody().BoundRadius;
			if (magnitude <= num * 0.9f && magnitude > 0.01f)
			{
				Vector3 vector = -position / magnitude;
				float velocity = GetVelocity();
				float num2 = 0f;
				float num3 = 20f;
				float num4 = Mathf.Clamp(num / magnitude, 1f, 10f);
				num2 = (num3 - velocity) / num3 * num4;
				if (num2 > 0f)
				{
					m_kRigidBody.AddForce(vector * num2, ForceMode.VelocityChange);
				}
			}
		}
	}

	public override void SetDynamicScaleFactor(float fScale)
	{
		base.SetDynamicScaleFactor(fScale);
		m_fCurScaleFactore = fScale;
	}

	private float CaculateSteering(float fVelcoity)
	{
		float interpolatePointFromCurve = m_kPhydata.GetInterpolatePointFromCurve(fVelcoity);
		interpolatePointFromCurve *= m_fCurScaleFactore;
		if (m_kShift.RequireModifyMaxSteering())
		{
			interpolatePointFromCurve = m_kShift.ModifyMaxSteering(interpolatePointFromCurve);
		}
		return m_fSteering * interpolatePointFromCurve;
	}

	public override void Steering(bool bAccelation, bool bBrake, float fSteering)
	{
		fSteering = AdjustAngleAccordingSimulateState(fSteering);
		m_bBrake = bBrake;
		m_fSteering = fSteering;
		PreAccelDir = AccelDir;
		if (PreVelocityDir == ENMMOTIONDIR.ECMD_FORWARD && AccelDir == ENMMOTIONDIR.ECMD_BACK)
		{
			m_fBackwardCoolTime = 0.5f;
		}
		if (bAccelation && !m_bBrake)
		{
			AccelDir = ENMMOTIONDIR.ECMD_FORWARD;
			if (m_bLockMovement)
			{
				IncreaseTireTemperature();
			}
			else
			{
				DecreaseTireTemperature();
			}
		}
		else if (m_bBrake && !bAccelation)
		{
			if (m_fBackwardCoolTime <= 0f)
			{
				AccelDir = ENMMOTIONDIR.ECMD_BACK;
			}
			else
			{
				AccelDir = ENMMOTIONDIR.ECM_NONE;
			}
			if (m_bLockMovement)
			{
				IncreaseTireTemperature();
			}
			else
			{
				DecreaseTireTemperature();
			}
		}
		else
		{
			AccelDir = ENMMOTIONDIR.ECM_NONE;
			DecreaseTireTemperature();
		}
	}

	public override void AISteeringNew(bool bAccelation, bool bBrake, bool bBackward, float fSteering)
	{
		if (bBackward)
		{
			bBrake = true;
		}
		Steering(bAccelation, bBrake, fSteering);
	}

	private float AdjustAngleAccordingSimulateState(float fSteering)
	{
		float result = fSteering;
		if (m_kCarCollisionCenter.IsSimulating())
		{
			result = 0f;
		}
		return result;
	}

	public void EnableWallCollider(bool enable)
	{
		if (cHead == null)
		{
			Transform transform = m_CenterofMassTransform.gameObject.transform.FindChild("collision/bodyhead");
			if (transform != null)
			{
				cHead = transform.collider;
			}
		}
		if (cTail == null)
		{
			Transform transform2 = m_CenterofMassTransform.gameObject.transform.FindChild("collision/bodytail");
			if (transform2 != null)
			{
				cTail = transform2.collider;
			}
		}
		if (cBody == null)
		{
			Transform transform3 = m_CenterofMassTransform.gameObject.transform.FindChild("collision/bodybox");
			if (transform3 != null)
			{
				cBody = transform3.collider;
			}
		}
		if (cHead != null)
		{
			cHead.enabled = enable;
		}
		if (cTail != null)
		{
			cTail.enabled = enable;
		}
		if (cBody != null)
		{
			Debug.Log("Body=" + enable);
		}
	}

	private void BeginEject(Collider col, float speed)
	{
		if (!m_bEject)
		{
			m_bEject = true;
			m_fFlyingTime = 0f;
			m_curPlatformCollider = col;
			Vector3 vVecGlobal = m_vVecGlobal;
			vVecGlobal.Normalize();
			if (VelocityDir != ENMMOTIONDIR.ECMD_FORWARD)
			{
				vVecGlobal = vForwardDir;
				vVecGlobal.Normalize();
				m_bEject = false;
			}
			Vector3 vecGlobal = vVecGlobal * speed;
			SetVecGlobal(vecGlobal);
			ejectSpeed = vecGlobal;
			m_eRunnigMode = RUNNING_MDOE.RM_FLYING;
			EnableWallCollider(enable: false);
		}
	}

	private void UpdateEjectMode2(float fTickTime, ENMMOTIONDIR dir)
	{
		chassis.SimulateFlying(fTickTime, dir);
		Vector3 vector = new Vector3(0f, 1f, 0f);
		Ray ray = new Ray(GetPosition() + vector, Vector3.down);
		if (m_curPlatformCollider != null)
		{
			if (!m_curPlatformCollider.Raycast(ray, out var hitInfo, 3f))
			{
				if (m_state == RUNNINGSTATES.RS_INAIR)
				{
					if (!m_kRigidBody.isKinematic)
					{
						EnableKinematic();
						m_Flysimulator.BeginSimulator(ejectSpeed);
						StartBoost(1f);
					}
					m_fFlyingTime += Time.deltaTime;
				}
				else
				{
					Ray ray2 = new Ray(chassis.vFrontLeft + vector, Vector3.down);
					Ray ray3 = new Ray(chassis.vFrontRight + vector, Vector3.down);
					Ray ray4 = new Ray(chassis.vBackLeft + vector, Vector3.down);
					Ray ray5 = new Ray(chassis.vBackRight + vector, Vector3.down);
					bool flag = m_curPlatformCollider.Raycast(ray2, out hitInfo, 2f);
					bool flag2 = m_curPlatformCollider.Raycast(ray3, out hitInfo, 2f);
					bool flag3 = m_curPlatformCollider.Raycast(ray4, out hitInfo, 2f);
					bool flag4 = m_curPlatformCollider.Raycast(ray5, out hitInfo, 2f);
					if (!flag && !flag2 && !flag3 && !flag4)
					{
						ChangeVehicleRunningMode(RUNNING_MDOE.RM_LANDING, null);
					}
				}
			}
		}
		else if (m_state == RUNNINGSTATES.RS_INAIR)
		{
			if (!m_kRigidBody.isKinematic)
			{
				EnableKinematic();
				m_Flysimulator.BeginSimulator();
				StartBoost(1f);
			}
			m_fFlyingTime += Time.deltaTime;
		}
		else if (m_Flysimulator.IsStarted() && !m_Flysimulator.IsVelocityUp())
		{
			ChangeVehicleRunningMode(RUNNING_MDOE.RM_LANDING, null);
		}
		m_Flysimulator.Update(fTickTime);
	}

	private void UpdateEjectMode(float fTickTime, ENMMOTIONDIR dir)
	{
		chassis.SimulateFlying(fTickTime, dir);
		if (!m_kRigidBody.isKinematic && !m_Flysimulator.IsStarted())
		{
			EnableKinematic();
			m_Flysimulator.BeginSimulator(ejectSpeed);
			StartBoost(1f);
		}
		if (m_Flysimulator.IsStarted())
		{
			m_Flysimulator.Update(fTickTime);
			m_fFlyingTime += Time.deltaTime;
			if (!m_Flysimulator.IsVelocityUp() && m_state == RUNNINGSTATES.RS_ONEARTH)
			{
				ChangeVehicleRunningMode(RUNNING_MDOE.RM_LANDING, null);
			}
		}
	}

	private void EndEject()
	{
		EnableWallCollider(enable: true);
		m_curPlatformCollider = null;
		m_bEject = false;
		m_kBoostVar.m_bBoost = false;
		DisableKinematic();
		Vector3[] array = new Vector3[2] { chassis.vFrontLeft, chassis.vFrontRight };
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < 2; i++)
		{
			zero += HeightMapManager.Instance.Normal(array[i].x, array[i].y, array[i].z);
		}
		zero /= 2f;
		Vector3 vector = -Vector3.Cross(zero, chassis.GetRightGlobal());
		vector.Normalize();
		vector = m_fMaxVelocity * vector;
		SetVecGlobal(vector);
		m_Flysimulator.StopSimulator();
	}

	public override void EnableKinematic()
	{
		m_kRigidBody.isKinematic = true;
		chassis.DisableWheels();
		m_suspension.Lock(bCompress: true);
	}

	public override void DisableKinematic()
	{
		m_kRigidBody.isKinematic = false;
		chassis.EnableWheels();
		m_suspension.UnLock();
	}

	public override bool IsKinematic()
	{
		return m_kRigidBody.isKinematic;
	}

	private void BeginLanding()
	{
		m_suspension.BeginLanding();
		m_fLastLandingTime = 0f;
		LockRotation(bLockX: false, bLockY: true, bLockZ: false);
		m_suspension.Lock(bCompress: true);
	}

	private void UpdateLandingMode(float fTickTime)
	{
		m_fLastLandingTime += fTickTime;
		if (m_fLastLandingTime > 0.3f)
		{
			ChangeVehicleRunningMode(RUNNING_MDOE.RM_GROUNDRUNING, null);
		}
	}

	private void EndLanding()
	{
		LockRotation(bLockX: false, bLockY: false, bLockZ: false);
		m_suspension.EndLanding();
		m_suspension.UnLock();
	}

	private void BeginFalling()
	{
		if (m_eRunnigMode != RUNNING_MDOE.RM_FALLINGDOWN)
		{
			m_eRunnigMode = RUNNING_MDOE.RM_FALLINGDOWN;
			m_kFallingSimulator.Begin();
		}
	}

	private void UpdateFallingMode(float fTickTime, ENMMOTIONDIR dir)
	{
		m_kFallingSimulator.Update(fTickTime);
	}

	private void EndFalling()
	{
		MoveToGround(GetPosition());
		m_eRunnigMode = RUNNING_MDOE.RM_GROUNDRUNING;
	}

	private void IncreaseTireTemperature()
	{
		m_fTireTemperature += 10f * Time.deltaTime;
	}

	private void DecreaseTireTemperature()
	{
		m_fTireTemperature -= 10f * Time.deltaTime;
		if (m_fTireTemperature < 0f)
		{
			m_fTireTemperature = 0f;
		}
	}

	public override void StartBoost(float fTime)
	{
		m_kBoostVar.m_bBoost = true;
		m_kBoostVar.m_fBoostTime = fTime;
		m_kBoostVar.m_fBoostMaxSpeed = 0f;
	}

	public override void StartBoost(float fTime, float fSpeed)
	{
		m_kBoostVar.m_bBoost = true;
		m_kBoostVar.m_fBoostTime = fTime;
		m_kBoostVar.m_fBoostMaxSpeed = fSpeed;
	}

	public override void StopBoost()
	{
		m_kBoostVar.m_bBoost = false;
		m_kBoostVar.m_fBoostTime = 0f;
		m_kBoostVar.m_bHandleEndOfBoost = true;
		m_kBoostVar.m_fBoostMaxSpeed = 0f;
	}

	public override bool GetBoostMode()
	{
		return m_kBoostVar.m_bBoost;
	}

	public override bool GetShiftMode()
	{
		return m_kShift.m_bShiftMode;
	}

	private void UpdateBoost(float fTickTime)
	{
		if (m_kBoostVar.m_bBoost)
		{
			if (m_kBoostVar.m_fBoostTime > 0f)
			{
				m_kBoostVar.m_fBoostTime -= Time.deltaTime;
				return;
			}
			m_kBoostVar.m_fBoostTime = 0f;
			m_kBoostVar.m_bBoost = false;
			m_kBoostVar.m_bHandleEndOfBoost = true;
		}
		else
		{
			m_kBoostVar.m_fBoostTime = 0f;
			m_kBoostVar.m_bBoost = false;
		}
	}

	public override void LockMovement(bool bLock)
	{
		m_bLockMovement = bLock;
	}

	public override bool GetLockMovement()
	{
		return m_bLockMovement;
	}

	private void BeginSideSlip()
	{
		LockRotation(bLockX: true, bLockY: true, bLockZ: true);
	}

	private void UpdateSideSlipMode(float fTickTime)
	{
		m_kCollsionSimulator.Update(fTickTime);
	}

	private void StopSideSlip()
	{
		LockRotation(bLockX: false, bLockY: false, bLockZ: false);
	}

	private void BeginBounce()
	{
		m_eRunnigMode = RUNNING_MDOE.RM_BOUNCE;
		EnableKinematic();
	}

	private void UpdateBounceMode(float fTickTime)
	{
		m_kBounceSimulator.Update(fTickTime);
	}

	private void StopBounce()
	{
		DisableKinematic();
		m_eRunnigMode = RUNNING_MDOE.RM_GROUNDRUNING;
	}

	private ENMTERRAIN_TYPE UpdateTerrainEffect(Collider col)
	{
		if (col == null)
		{
			return ENMTERRAIN_TYPE.ETT_NONE;
		}
		string tag = col.tag;
		ENMTERRAIN_TYPE result = ENMTERRAIN_TYPE.ETT_NONE;
		if (tag.Equals("JumpPlatform"))
		{
			return ENMTERRAIN_TYPE.ETT_BOOSTPLATFORM;
		}
		Vector3 vWorldPos = GetPosition() + m_kPhydata.fVehicleHeight * Vector3.up;
		Vector3 down = Vector3.down;
		if (CollisionLayer.Instance.CheckHitDeepWater(vWorldPos, down, out var _))
		{
			return ENMTERRAIN_TYPE.ETT_DEEPWATER;
		}
		if (CollisionLayer.Instance.CheckHitShallowWater(vWorldPos, down, out m_vCurWaterPoint))
		{
			return ENMTERRAIN_TYPE.ETT_WATER;
		}
		if (tag.Equals("Sand"))
		{
			return ENMTERRAIN_TYPE.ETT_SAND;
		}
		if (tag.Equals("Grass"))
		{
			return ENMTERRAIN_TYPE.ETT_GRASS;
		}
		return result;
	}

	private ENMTERRAIN_TYPE GetRealWheelTerrainType()
	{
		ENMTERRAIN_TYPE eNMTERRAIN_TYPE = ENMTERRAIN_TYPE.ETT_NONE;
		for (int i = 0; i < chassis.Nxwheels.Length; i++)
		{
			eNMTERRAIN_TYPE |= chassis.Nxwheels[i].GroundType;
		}
		if ((eNMTERRAIN_TYPE & ENMTERRAIN_TYPE.ETT_BOOSTPLATFORM) != ENMTERRAIN_TYPE.ETT_NONE)
		{
			return ENMTERRAIN_TYPE.ETT_BOOSTPLATFORM;
		}
		if ((eNMTERRAIN_TYPE & ENMTERRAIN_TYPE.ETT_DEEPWATER) != ENMTERRAIN_TYPE.ETT_NONE)
		{
			return ENMTERRAIN_TYPE.ETT_DEEPWATER;
		}
		if ((eNMTERRAIN_TYPE & ENMTERRAIN_TYPE.ETT_WATER) != ENMTERRAIN_TYPE.ETT_NONE)
		{
			return ENMTERRAIN_TYPE.ETT_WATER;
		}
		if ((eNMTERRAIN_TYPE & ENMTERRAIN_TYPE.ETT_SAND) != ENMTERRAIN_TYPE.ETT_NONE)
		{
			return ENMTERRAIN_TYPE.ETT_SAND;
		}
		if ((eNMTERRAIN_TYPE & ENMTERRAIN_TYPE.ETT_GRASS) != ENMTERRAIN_TYPE.ETT_NONE)
		{
			return ENMTERRAIN_TYPE.ETT_GRASS;
		}
		return ENMTERRAIN_TYPE.ETT_NONE;
	}

	public void SetWheelTerrainType(NxWheel wheel)
	{
		Collider groundedCollider = wheel.GroundedCollider;
		ENMTERRAIN_TYPE groundType = UpdateTerrainEffect(groundedCollider);
		ENMTERRAIN_TYPE realWheelTerrainType = GetRealWheelTerrainType();
		ENMTERRAIN_TYPE groundedType = GroundedType;
		GroundedType = realWheelTerrainType;
		wheel.GroundType = groundType;
		if (GroundedType == ENMTERRAIN_TYPE.ETT_BOOSTPLATFORM && base.TriggerPlatform && !m_bEject)
		{
			base.TriggerPlatform = false;
			ABMessageData data = new COM_DATA();
			((COM_DATA)data).iValue = 0;
			CTMessagePipeline.sendMessage(m_kUserData, 1006, ref data);
			BeginEject(groundedCollider, 39f);
		}
		if (GroundedType == ENMTERRAIN_TYPE.ETT_BOOSTPLATFORM || GroundedType == ENMTERRAIN_TYPE.ETT_DEEPWATER)
		{
			if (m_kShift.IsShiftMode())
			{
				m_kShift.BreakShift();
			}
		}
		else if ((GroundedType == ENMTERRAIN_TYPE.ETT_GRASS || GroundedType == ENMTERRAIN_TYPE.ETT_SAND) && m_kShift.IsShiftMode())
		{
			if (m_kShift.m_carModel != null)
			{
				if ((m_kShift.m_carModel.CarState & 0x100) == 0 && (m_kShift.m_carModel.CarState & 0x80) == 0 && (m_kShift.m_carModel.CarState & 0x40) == 0)
				{
					m_kShift.BreakShift();
				}
			}
			else
			{
				m_kShift.BreakShift();
			}
		}
		if (groundedType != GroundedType)
		{
			ABMessageData data2 = new COM_DATA();
			((COM_DATA)data2).iValue = (int)GroundedType;
			CTMessagePipeline.sendMessage(m_kUserData, 1002, ref data2);
			ENMTERRAIN_TYPE groundedType2 = GroundedType;
			if (groundedType2 != ENMTERRAIN_TYPE.ETT_BOOSTPLATFORM && groundedType2 == ENMTERRAIN_TYPE.ETT_DEEPWATER && !m_kRigidBody.isKinematic)
			{
				((COM_DATA)data2).iValue = (int)GroundedType;
				((COM_DATA)data2).vValue = m_vCurWaterPoint;
				CTMessagePipeline.sendMessage(m_kUserData, 1001, ref data2);
			}
		}
	}

	public override void ChangeVehicleRunningMode(RUNNING_MDOE eMode, object data)
	{
		if (m_eRunnigMode != eMode)
		{
			ABMessageData data2 = new COM_DATA();
			((COM_DATA)data2).iValue = (int)eMode;
			CTMessagePipeline.sendMessage(m_kUserData, 1006, ref data2);
		}
		switch (m_eRunnigMode)
		{
		case RUNNING_MDOE.RM_FLYING:
			EndEject();
			break;
		case RUNNING_MDOE.RM_LANDING:
			EndLanding();
			break;
		case RUNNING_MDOE.RM_BOUNCE:
			StopBounce();
			break;
		case RUNNING_MDOE.RM_FALLINGDOWN:
			EndFalling();
			break;
		case RUNNING_MDOE.RM_SIDESLIP:
			StopSideSlip();
			break;
		}
		m_eRunnigMode = eMode;
		switch (m_eRunnigMode)
		{
		case RUNNING_MDOE.RM_BOUNCE:
			BeginBounce();
			break;
		case RUNNING_MDOE.RM_FALLINGDOWN:
			BeginFalling();
			break;
		case RUNNING_MDOE.RM_FLYING:
		{
			Collider collider = data as Collider;
			float speed = 39f;
			if (collider == null)
			{
				speed = GetVelocity();
			}
			BeginEject(collider, speed);
			break;
		}
		case RUNNING_MDOE.RM_GROUNDRUNING:
		case RUNNING_MDOE.RM_GROUNDJUMP:
			break;
		case RUNNING_MDOE.RM_LANDING:
			BeginLanding();
			break;
		case RUNNING_MDOE.RM_SIDESLIP:
			BeginSideSlip();
			break;
		}
	}

	public void RotateAroundFrontWheelsLocalUp(float fAngle, bool bRotateVelocity)
	{
		Vector3 vLocal = chassis.vLocalMidFrontWheels;
		vLocal = chassis.TransformToWorld(vLocal);
		m_CenterofMassTransform.RotateAround(vLocal, chassis.GetUpGlobal(), fAngle);
		if (bRotateVelocity)
		{
			Quaternion quaternion = Quaternion.AngleAxis(fAngle, chassis.GetUpGlobal());
			Vector3 vecGlobal = quaternion * m_vVecGlobal;
			SetVecGlobal(vecGlobal);
		}
	}

	public void RotateAroundFrontWheelsWorldUp(float fAngle, bool bRotateVelocity)
	{
		Vector3 vLocal = chassis.vLocalMidFrontWheels;
		vLocal = chassis.TransformToWorld(vLocal);
		m_CenterofMassTransform.RotateAround(vLocal, Vector3.up, fAngle);
		if (bRotateVelocity)
		{
			Quaternion quaternion = Quaternion.AngleAxis(fAngle, Vector3.up);
			Vector3 vecGlobal = quaternion * m_vVecGlobal;
			SetVecGlobal(vecGlobal);
		}
	}

	public void RotateAroundRearWheelsLocalUp(float fAngle, bool bRotateVelocity)
	{
		Vector3 vLocal = chassis.vLocalMidRearWheels;
		vLocal = chassis.TransformToWorld(vLocal);
		m_CenterofMassTransform.RotateAround(vLocal, chassis.GetUpGlobal(), fAngle);
		if (bRotateVelocity)
		{
			Quaternion quaternion = Quaternion.AngleAxis(fAngle, chassis.GetUpGlobal());
			Vector3 vecGlobal = quaternion * m_vVecGlobal;
			SetVecGlobal(vecGlobal);
		}
	}

	public override void RotateAroundRearWheelsWorldUp(float fAngle, bool bRotateVelocity)
	{
		Vector3 vLocal = chassis.vLocalMidRearWheels;
		vLocal = chassis.TransformToWorld(vLocal);
		m_CenterofMassTransform.RotateAround(vLocal, Vector3.up, fAngle);
		if (bRotateVelocity)
		{
			Quaternion quaternion = Quaternion.AngleAxis(fAngle, Vector3.up);
			Vector3 vecGlobal = quaternion * m_vVecGlobal;
			SetVecGlobal(vecGlobal);
		}
	}

	public override void RotateAroundPoint(Vector3 vOrigo, float fAngle, bool bRotateVelocity)
	{
		m_CenterofMassTransform.RotateAround(vOrigo, chassis.GetUpGlobal(), fAngle);
		if (bRotateVelocity)
		{
			Quaternion quaternion = Quaternion.AngleAxis(fAngle, chassis.GetUpGlobal());
			Vector3 vecGlobal = quaternion * m_vVecGlobal;
			SetVecGlobal(vecGlobal);
		}
	}

	public override void RotateCarLocal(float fAngleX, float fAngleY, float fAngleZ)
	{
		Quaternion quaternion = Quaternion.Euler(fAngleX, fAngleY, fAngleZ);
		quaternion = chassis.GetRootRotate() * quaternion;
		m_CenterofMassTransform.localRotation = quaternion;
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

	public void LockPose()
	{
		if (!m_kCarCollisionCenter.IsSimulating())
		{
			m_bLockPose = true;
		}
	}

	public void UnLockPose()
	{
		m_bLockPose = false;
	}

	public bool CheckLock()
	{
		return m_bLockPose;
	}

	public override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		m_kFallingSimulator.OnDrawGizmos();
		m_kCollsionSimulator.OnDrawGizmos();
		Color color = Gizmos.color;
		Gizmos.color = Color.red;
		Gizmos.DrawLine(chassis.GetWorldPosition(), chassis.GetWorldPosition() + curForce * 3f);
		curForce = Vector3.zero;
		Gizmos.color = color;
	}

	public override void OnGUI()
	{
	}

	public override void SetKeepLiveVehicle(btVehicleBase kOther)
	{
		m_kCarCollisionCenter.AddKeepLivePair(kOther);
		kOther.m_kCarCollisionCenter.AddKeepLivePair(this);
	}

	public override Vector3 GetUpGlobal()
	{
		return chassis.GetUpGlobal();
	}

	public override Vector3 GetForwardGlobal()
	{
		return chassis.vForwardDir;
	}

	public override Vector3 GetRightGlobal()
	{
		return chassis.GetRightGlobal();
	}

	public override Vector3 GetGroundNormal()
	{
		return chassis.GetGroundNormal();
	}

	public override Vector3 GetAccelate()
	{
		Vector3 vector = vGroundFriction + vTractiveForce + AirFriction;
		return vector / m_kRigidBody.mass;
	}
}
