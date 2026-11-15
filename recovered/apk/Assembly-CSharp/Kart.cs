using System;
using UnityEngine;

public class Kart : btRaycastVehicle
{
	private float maxTurnScale = 1f;

	private float motoForceScale = 1f;

	public Vector3 vTractiveForce = Vector3.zero;

	public Vector3 AirFriction = Vector3.zero;

	public MotorControl motorcontrol;

	private Vector3 vForwardDir;

	private bool m_bLockMovement;

	private bool bBrake;

	private float m_fBackwardCoolTime;

	protected float fLastMotorForce;

	protected float fLastBrakeForce;

	public float m_fTireTemperature;

	public float fRollingCoefficient = 0.02f;

	public float fBumpAngle;

	private int m_WheelCount;

	protected float m_fFlyingTime;

	protected Collider m_curPlatformCollider;

	public FlySimulator m_Flysimulator;

	private bool m_bEject;

	private Vector3 ejectSpeed = Vector3.zero;

	private float m_fLastLandingTime;

	private FallingSimulator m_kFallingSimulator;

	public override float MaxTurnScale
	{
		get
		{
			return maxTurnScale;
		}
		set
		{
			maxTurnScale = value;
		}
	}

	public override float MotorForceScale
	{
		get
		{
			return motoForceScale;
		}
		set
		{
			motoForceScale = value;
		}
	}

	public Kart(VehicleData phyData, Transform bodyTransform, Transform lFrontWheel, Transform rFrontWheel, Transform lRearWheel, Transform rRearWheel, EnmCollisionLayer clLayer, CollisionCallback callback)
		: base(phyData, bodyTransform, lFrontWheel, rFrontWheel, lRearWheel, rRearWheel)
	{
		motorcontrol = new MotorControl(m_kPhydata);
		wheels[0] = lFrontWheel;
		wheels[1] = rFrontWheel;
		wheels[3] = lRearWheel;
		wheels[2] = rRearWheel;
		InitChassis(wheels, fFrontTireRadius, fRearTireRadius, fSpring, fsuspensionDistance, bodyTransform.gameObject);
		m_vGravity = m_kRigidBody.mass * Physics.gravity;
		m_kRigidBody.useGravity = false;
		m_kRigidBody.isKinematic = false;
		m_kCompositeCollider = new BodyComponent(bodyTransform.gameObject, this, EnmCollisionLayer.CL_PLAYER);
		m_kCarCollisionCenter = new CarCollisionDetection();
		m_kCarCollisionCenter.Init(m_kCompositeCollider, callback, this);
		m_Flysimulator = new FlySimulator(this);
		m_kFallingSimulator = new FallingSimulator(this);
		m_kCollsionSimulator = new VehicleCollisionSimulator(this);
		m_kBounceSimulator = new BounceSimulator(this);
		Transform transform = bodyTransform.gameObject.transform.FindChild("collision/bodyhead");
		if (transform != null)
		{
			transform.collider.material = m_kCompositeCollider[0].GetCollider().material;
			transform.gameObject.layer = 19;
		}
		transform = bodyTransform.gameObject.transform.FindChild("collision/bodytail");
		if (transform != null)
		{
			transform.collider.material = m_kCompositeCollider[0].GetCollider().material;
			transform.gameObject.layer = 19;
		}
	}

	public override void Uninit()
	{
		m_kCarCollisionCenter.Unint();
		m_kCompositeCollider.Uninit();
		base.Uninit();
	}

	public override bool MoveToGround(Vector3 vPos)
	{
		Vector3 vector = vPos;
		vector.y += 100f;
		if (!CollisionLayer.Instance.VerticalIntersectHitPosition(vector, out var _, out var vHitPoint))
		{
			return false;
		}
		SetPosition(vector);
		Vector3 ray = new Vector3(0f, -10000f, 0f);
		if (!projectVehicleToSurface(ray, btranslate_vehicle: true))
		{
			vHitPoint.y += 0.2f;
			SetPosition(vHitPoint);
			return false;
		}
		return true;
	}

	public override void LockMovement(bool bLock)
	{
		m_bLockMovement = bLock;
	}

	public override bool GetLockMovement()
	{
		return m_bLockMovement;
	}

	public override bool CheckCanDriveNow()
	{
		return m_state != RUNNINGSTATES.RS_INAIR;
	}

	private float CaculateSteering(float fVelcoity)
	{
		float num = maxTurnScale * m_kPhydata.GetInterpolatePointFromCurve(fVelcoity);
		return m_fSteering * num;
	}

	public override void Steering(bool bAccelation, bool bBrake, float fSteering)
	{
		fSteering = AdjustAngleAccordingSimulateState(fSteering);
		this.bBrake = bBrake;
		m_fSteering = fSteering;
		PreAccelDir = AccelDir;
		if (PreVelocityDir == ENMMOTIONDIR.ECMD_FORWARD && AccelDir == ENMMOTIONDIR.ECMD_BACK)
		{
			m_fBackwardCoolTime = 0.5f;
		}
		if (bAccelation && !bBrake)
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
		else if (bBrake && !bAccelation)
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
		if (GetVelocity() > base.fLimitSpeed && bAccelation)
		{
			bAccelation = false;
		}
		Steering(bAccelation, bBrake, fSteering);
	}

	private float AdjustAngleAccordingSimulateState(float fSteering)
	{
		return fSteering;
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
		m_vResistance = Vector2.zero;
		vVecGlobal.Normalize();
		Vector3 vForce = CaculateAirFriction(m_vVecGlobal);
		AddForceGlobal(vForce, ENMFORCETYPE.EFT_NORMAL, fTickTime);
		m_vResistance.x += Mathf.Sqrt(vForce.z * vForce.z + vForce.x * vForce.x);
		m_vResistance.y = 0f;
		float num = m_kRigidBody.mass * 9.8f * fRollingCoefficient;
		Vector3 vForce2 = vector * num;
		AddForceGlobal(vForce2, ENMFORCETYPE.EFT_NORMAL, fTickTime);
		Vector3 vForce3 = CalculateGroundFriction(fTickTime, vector);
		AddForceGlobal(vForce3, ENMFORCETYPE.EFT_FRICTROLL, fTickTime);
		m_vResistance.x += Mathf.Sqrt(vForce2.z * vForce2.z + vForce2.x * vForce2.x);
		m_vResistance.x += Mathf.Sqrt(vForce3.z * vForce3.z + vForce3.x * vForce3.x);
		if ((VelocityDir == ENMMOTIONDIR.ECMD_FORWARD || VelocityDir == ENMMOTIONDIR.ECMD_BACK) && AccelDir == ENMMOTIONDIR.ECM_NONE)
		{
			if (m_vVecGlobal.magnitude > 10f)
			{
				Vector3 vForce4 = -vVecGlobal * m_kRigidBody.mass * 18f;
				AddForceGlobal(vForce4, ENMFORCETYPE.EFT_NORMAL, fTickTime);
				m_vResistance.x += Mathf.Sqrt(vForce4.z * vForce4.z + vForce4.x * vForce4.x);
			}
			else
			{
				Vector3 vForce5 = -vVecGlobal * m_kRigidBody.mass * 5f;
				AddForceGlobal(vForce5, ENMFORCETYPE.EFT_NORMAL, fTickTime);
				m_vResistance.x += Mathf.Sqrt(vForce5.z * vForce5.z + vForce5.x * vForce5.x);
			}
		}
		if (!m_kBoostVar.m_bHandleEndOfBoost)
		{
			return;
		}
		if (IsKinematic())
		{
			m_kBoostVar.m_bHandleEndOfBoost = false;
			return;
		}
		float velocity = GetVelocity();
		if (velocity > 30f)
		{
			Vector3 vecGlobal = GetVecGlobal();
			vecGlobal.Normalize();
			velocity -= fTickTime * 10f;
			SetVecGlobal(velocity * vecGlobal);
		}
		else
		{
			m_kBoostVar.m_bHandleEndOfBoost = false;
		}
	}

	private Vector3 CalculateGroundFriction(float fTickTime, Vector3 vDragDir)
	{
		Vector3 result = Vector3.zero;
		int num = 0;
		float num2 = 0f;
		for (int i = 0; i < 4; i++)
		{
			if (Wheels(i).m_raycastInfo.m_isInContact)
			{
				if (null == Wheels(i).m_raycastInfo.m_groundObject)
				{
					num2 += 0.5f;
				}
				else
				{
					PhysicMaterial material = Wheels(i).m_raycastInfo.m_groundObject.material;
					num2 = ((!(material != null)) ? (num2 + 0.5f) : (num2 + material.staticFriction));
				}
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

	private void CalculateGravity(float fTickTime)
	{
		AddForceGlobal(m_vGravity, ENMFORCETYPE.EFT_GRAVITY, fTickTime);
	}

	private void GroundTick(bool handBrake, float motorFroce, float braketorque, float timestamp)
	{
		int num = 0;
		Vector3 lhs = Vector3.zero;
		for (int i = 0; i < 4; i++)
		{
			btWheelInfo btWheelInfo2 = Wheels(i);
			if (btWheelInfo2.m_raycastInfo.m_isInContact)
			{
				num++;
				lhs += btWheelInfo2.m_raycastInfo.m_contactNormalWS;
			}
		}
		if (num > 0)
		{
			lhs /= (float)num;
		}
		else
		{
			lhs = Vector3.up;
		}
		vTractiveForce = Vector3.zero;
		if ((m_eContactState == CONTACTSTATE.CS_FOURWHEEL || m_eContactState == CONTACTSTATE.CS_THREEWHEEL) && !m_bLockMovement && motorFroce != 0f)
		{
			Vector3 vector = -Vector3.Cross(lhs, GetRightGlobal());
			vector.Normalize();
			vTractiveForce = vector * motorFroce * 10f;
			AddForceGlobal(vTractiveForce, ENMFORCETYPE.EFT_PULL, timestamp);
			m_vTraction.x = motorFroce * vector.z * 10f;
			m_vTraction.y = motorFroce * vector.x * 10f;
		}
		if (!(m_kBoostVar.m_fBoostTime > 0f) || m_kRigidBody.isKinematic || m_LockSpeed)
		{
			return;
		}
		if (m_kBoostVar.m_bPauseBoost)
		{
			m_kBoostVar.m_bPauseBoost = false;
		}
		else if (m_eContactState == CONTACTSTATE.CS_FOURWHEEL)
		{
			float num2 = m_kBoostVar.m_fBoostMaxSpeed;
			if (num2 < 1f)
			{
				num2 = m_kPhydata.m_fextremumVelocity;
			}
			float num3 = num2 - GetVelocity();
			if (num3 > 0f)
			{
				Vector3 force = GetForwardGlobal() * (num3 * Time.deltaTime * 2.5f);
				m_kRigidBody.AddForce(force, ForceMode.VelocityChange);
			}
		}
	}

	public float GetMotorForce(out float braketorque)
	{
		float num = 0f;
		braketorque = 0f;
		num = motorcontrol.GetMotorForce(m_fRPM, m_fSteering, AccelDir, 0);
		if (bBrake || m_bBreakVehicleOnce)
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
		float fixedDeltaTime = Time.fixedDeltaTime;
		UpdateVehicleState();
		UpdateBoost(fixedDeltaTime);
		switch (m_eRunnigMode)
		{
		case RUNNING_MDOE.RM_GROUNDRUNING:
			fLastMotorForce = 0f;
			fLastBrakeForce = 0f;
			m_fSteeringAngle = CaculateSteering(GetVelocity()) * ((float)Math.PI / 180f);
			CalculateGravity(fixedDeltaTime);
			UpdateRepulsion(fixedDeltaTime);
			CalculateFriction(fixedDeltaTime);
			motortorque = motoForceScale * GetMotorForce(out braketorque);
			GroundTick(bBrake, motortorque, braketorque, fixedDeltaTime);
			fLastMotorForce = motortorque;
			fLastBrakeForce = braketorque;
			TriggerForceTwistPool(fixedDeltaTime);
			doSteering(fixedDeltaTime);
			SynchronizeWheel(m_fSteeringAngle, m_fRPM, fixedDeltaTime, VelocityDir);
			if (m_state == RUNNINGSTATES.RS_ONEARTH)
			{
				m_WheelCount++;
				m_WheelCount %= 4;
				SetWheelTerrainType(m_wheelInfo[m_WheelCount].m_raycastInfo.m_groundObject);
			}
			break;
		case RUNNING_MDOE.RM_FLYING:
			UpdateEjectMode(fixedDeltaTime, VelocityDir);
			break;
		case RUNNING_MDOE.RM_LANDING:
			SynchronizeWheel(0f, m_fRPM, fixedDeltaTime, VelocityDir);
			CalculateGravity(fixedDeltaTime);
			UpdateLandingMode(fixedDeltaTime);
			TriggerForceTwistPool(fixedDeltaTime);
			break;
		case RUNNING_MDOE.RM_FALLINGDOWN:
			SynchronizeWheel(0f, m_fRPM, fixedDeltaTime, VelocityDir);
			CalculateGravity(fixedDeltaTime);
			UpdateFallingMode(fixedDeltaTime, VelocityDir);
			TriggerForceTwistPool(fixedDeltaTime);
			break;
		case RUNNING_MDOE.RM_SIDESLIP:
			CalculateGravity(fixedDeltaTime);
			CalculateFriction(fixedDeltaTime);
			motortorque = 0f;
			braketorque = 0f;
			motortorque = GetMotorForce(out braketorque);
			GroundTick(m_bBrake, motortorque, braketorque, fixedDeltaTime);
			TriggerForceTwistPool(fixedDeltaTime);
			UpdateSideSlipMode(fixedDeltaTime);
			UpdateRepulsion(fixedDeltaTime);
			break;
		case RUNNING_MDOE.RM_BOUNCE:
			CalculateGravity(fixedDeltaTime);
			CalculateFriction(fixedDeltaTime);
			motortorque = 0f;
			braketorque = 0f;
			motortorque = GetMotorForce(out braketorque);
			GroundTick(m_bBrake, motortorque, braketorque, fixedDeltaTime);
			TriggerForceTwistPool(fixedDeltaTime);
			UpdateBounceMode(fixedDeltaTime);
			break;
		case RUNNING_MDOE.RM_GROUNDJUMP:
			break;
		}
	}

	private void UpdateVehicleState()
	{
		PreVelocityDir = VelocityDir;
		m_fBackwardCoolTime -= Time.deltaTime;
		m_vVecGlobal = GetVecGlobal();
		vForwardDir = GetForwardGlobal();
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
		if (m_bBreakVehicleOnce && m_vVecGlobal.magnitude < 5f)
		{
			m_bBreakVehicleOnce = false;
		}
	}

	public override void SetKeepLiveVehicle(btVehicleBase kOther)
	{
		m_kCarCollisionCenter.AddKeepLivePair(kOther);
		kOther.m_kCarCollisionCenter.AddKeepLivePair(this);
	}

	public override void Hide()
	{
		m_bVisible = false;
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
		MeshRenderer[] componentsInChildren = m_CenterofMassTransform.FindChild("car/root").GetComponentsInChildren<MeshRenderer>();
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].enabled = true;
			if (null != componentsInChildren[j].gameObject.transform.parent && null != componentsInChildren[j].gameObject.transform.parent.GetComponent<Effect>())
			{
				componentsInChildren[j].enabled = false;
			}
		}
		m_CenterofMassTransform.FindChild("collision").gameObject.SetActiveRecursively(state: true);
	}

	public override void Update()
	{
		m_vPreVelGlobal = m_vVecGlobal;
		vForwardDir = GetForwardGlobal();
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
		UpdateVehicleState();
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

	public override void EnableKinematic()
	{
		m_kRigidBody.isKinematic = true;
	}

	public override void DisableKinematic()
	{
		m_kRigidBody.isKinematic = false;
	}

	public override bool IsKinematic()
	{
		return m_kRigidBody.isKinematic;
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

	public override void StopBoost()
	{
		m_kBoostVar.m_bBoost = false;
		m_kBoostVar.m_fBoostTime = 0f;
		m_kBoostVar.m_bHandleEndOfBoost = true;
	}

	public override bool GetBoostMode()
	{
		return m_kBoostVar.m_bBoost;
	}

	private void BeginEject(Collider col, float speed)
	{
		if (!m_bEject)
		{
			m_kBoostVar.m_bBoost = true;
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
		}
	}

	private void UpdateEjectMode2(float fTickTime, ENMMOTIONDIR dir)
	{
		SimulateFlying(fTickTime, dir);
		Vector3 vector = new Vector3(0f, 1f, 0f);
		Ray ray = new Ray(GetPosition() + vector, Vector3.down);
		if (m_curPlatformCollider != null)
		{
			if (!m_curPlatformCollider.Raycast(ray, out var hitInfo, 3f))
			{
				if (!m_Flysimulator.IsStarted() && !m_kRigidBody.isKinematic)
				{
					EnableKinematic();
					m_Flysimulator.BeginSimulator(ejectSpeed);
				}
				m_fFlyingTime += Time.deltaTime;
				if (m_state != RUNNINGSTATES.RS_INAIR && !m_Flysimulator.IsVelocityUp())
				{
					Ray ray2 = new Ray(base.vFrontLeft + vector, Vector3.down);
					Ray ray3 = new Ray(base.vFrontRight + vector, Vector3.down);
					Ray ray4 = new Ray(base.vBackLeft + vector, Vector3.down);
					Ray ray5 = new Ray(base.vBackRight + vector, Vector3.down);
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
			}
			m_fFlyingTime += Time.deltaTime;
		}
		else
		{
			ChangeVehicleRunningMode(RUNNING_MDOE.RM_LANDING, null);
		}
		m_Flysimulator.Update(fTickTime);
	}

	private void UpdateEjectMode(float fTickTime, ENMMOTIONDIR dir)
	{
		SimulateFlying(fTickTime, dir);
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
		m_curPlatformCollider = null;
		m_bEject = false;
		m_kBoostVar.m_bBoost = false;
		DisableKinematic();
		Vector3[] array = new Vector3[2] { base.vFrontLeft, base.vFrontRight };
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < 2; i++)
		{
			zero += HeightMapManager.Instance.Normal(array[i].x, array[i].y, array[i].z);
		}
		zero /= 2f;
		Vector3 vector = -Vector3.Cross(zero, GetRightGlobal());
		vector.Normalize();
		vector = m_fMaxVelocity * vector;
		SetVecGlobal(vector);
		m_Flysimulator.StopSimulator();
		NiLog.Log("End Jump !" + m_fFlyingTime);
	}

	private void BeginLanding()
	{
		m_fLastLandingTime = 0f;
		LockRotation(bLockX: false, bLockY: true, bLockZ: false);
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
		Vector3 vWorldPos = GetPosition() + m_kPhydata.fVehicleHeight * GetUpGlobal();
		Vector3 vDir = -GetUpGlobal();
		if (CollisionLayer.Instance.CheckHitDeepWater(vWorldPos, vDir, out var _))
		{
			return ENMTERRAIN_TYPE.ETT_DEEPWATER;
		}
		if (CollisionLayer.Instance.CheckHitShallowWater(vWorldPos, vDir, out m_vCurWaterPoint))
		{
			return ENMTERRAIN_TYPE.ETT_WATER;
		}
		if (tag.Equals("Sand"))
		{
			result = ENMTERRAIN_TYPE.ETT_SAND;
		}
		else if (tag.Equals("Grass"))
		{
			result = ENMTERRAIN_TYPE.ETT_GRASS;
		}
		return result;
	}

	public void SetWheelTerrainType(Collider collider)
	{
		ENMTERRAIN_TYPE groundedType = UpdateTerrainEffect(collider);
		ENMTERRAIN_TYPE groundedType2 = GroundedType;
		GroundedType = groundedType;
		if (GroundedType == ENMTERRAIN_TYPE.ETT_BOOSTPLATFORM && base.TriggerPlatform && !m_bEject)
		{
			base.TriggerPlatform = false;
			ABMessageData data = new COM_DATA();
			((COM_DATA)data).iValue = 0;
			CTMessagePipeline.sendMessage(m_kUserData, 1006, ref data);
			BeginEject(collider, 39f);
		}
		if (groundedType2 != GroundedType)
		{
			ABMessageData data2 = new COM_DATA();
			((COM_DATA)data2).iValue = (int)GroundedType;
			CTMessagePipeline.sendMessage(m_kUserData, 1002, ref data2);
			ENMTERRAIN_TYPE groundedType3 = GroundedType;
			if (groundedType3 != ENMTERRAIN_TYPE.ETT_BOOSTPLATFORM && groundedType3 == ENMTERRAIN_TYPE.ETT_DEEPWATER && !m_kRigidBody.isKinematic)
			{
				((COM_DATA)data2).iValue = (int)GroundedType;
				((COM_DATA)data2).vValue = m_vCurWaterPoint;
				CTMessagePipeline.sendMessage(m_kUserData, 1001, ref data2);
			}
		}
	}

	public override bool GetShiftMode()
	{
		return false;
	}

	public override void ChangeVehicleRunningMode(RUNNING_MDOE eMode, object data)
	{
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

	public override void OnGUI()
	{
	}

	public override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
	}
}
