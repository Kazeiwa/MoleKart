using UnityEngine;

public class VehicleShift
{
	public const float StiffnessDownSpeed = 0.18f;

	public const float StiffnessUpSpeed = 2f;

	public const float StiffnessMax = 1f;

	public const float StiffnessMin = 0.1f;

	public const float StiffnessMinFront = 0.1f;

	public const float MinShiftSpeed = 15f;

	public const float MinShiftSteer = 0.25f;

	public const float ShiftBreakSpeed = 7f;

	public const float MinAngleStopP1 = 0.25f;

	private const float m_fEffectDotPeriod = 0.5f;

	private const float m_fEffectGapPeriod = 0.2f;

	private const float m_fMinBoostAccum = 0.2f;

	private const float m_fMaxBoostAccum = 1.8f;

	private const float m_fMaxSpeedRot = 20f;

	private const float m_fSpeedRotSpeed = 30f;

	public bool m_bEnableShift = true;

	public bool m_bAutoShift = true;

	private VehicleShiftSound m_kSound = new VehicleShiftSound();

	public float m_fShiftDir;

	public bool m_bShiftCD = true;

	public float[] m_fWheelsStiffness = new float[4] { 1f, 1f, 1f, 1f };

	public float m_fInSpeed;

	public float m_fShiftTime;

	public float m_fShiftBoost;

	public bool m_bShiftMode;

	public int m_iStiffnessChangeFlag;

	public btVehicleBase m_car;

	public Vehicle m_vehicle;

	public Car m_carModel;

	public float m_fRotMaxAng;

	public float m_fAccumRotate;

	public float m_fRotMaxV;

	public float m_fRotateV;

	public float m_fRotateA;

	public float m_fRotateDir;

	public float m_fModelRotAng;

	public float m_fModelRotMaxAng = 25f;

	public float m_fModelRotV = 20f;

	public float m_fShiftBoostMaxSpeed0;

	public float m_fShiftBoostMaxSpeed1;

	public float m_fShiftBoostTime0;

	public float m_fShiftBoostTime1;

	private float[] m_fRotAcc = new float[3];

	private float[] m_fRotAng = new float[3];

	private int m_iCurStage;

	public float m_fP1BaseSpeed = 26f;

	public float m_fP1DownAcc = 4f;

	public float m_fP1UpAcc = 10f;

	public float m_fShiftBoostSmallTime;

	public float m_fShiftBoostBigTime;

	private float m_fShiftEffectTime;

	public float m_fCarMaxSpeed = 30f;

	public float m_fCarMaxSpeedAuto = 30f;

	public float m_fShiftAutoMotorScale = 1f;

	public float m_fStiffnessFactor = 1f;

	public float m_fAutoShiftBeginTime = 0.5f;

	public float m_fAutoShiftMinSteer = 0.95f;

	public float m_fAutoShiftBreakSteer = 0.3f;

	private float m_fAutoShiftHoldTime;

	private float m_fTorqueScale = 1f;

	private float m_fCurSpeedRotAng;

	private int m_iPerformanceLevel = 1;

	private Transform m_carRootTrans;

	private float StiffnessRecover;

	private float StiffnessChange;

	private CONTROL_TYPE curType;

	public float AdjustSteerRange = 2.4f;

	public float AdjustSteerMin = 0.2f;

	public float AutoAdjustSteerRange = 2.4f;

	public float AutoAdjustSteerMin = 0.2f;

	public VehicleShift(btVehicleBase car)
	{
		m_car = car;
		m_vehicle = car as Vehicle;
	}

	public bool IsShiftMode()
	{
		return m_bShiftMode;
	}

	public float GetTorqueScale()
	{
		return m_fTorqueScale;
	}

	public int GetShiftLevel()
	{
		if (m_bShiftMode)
		{
			if (m_bAutoShift)
			{
				return 0;
			}
			if (m_fShiftEffectTime <= 0.01f)
			{
				return 0;
			}
			if (m_fShiftEffectTime < m_fShiftBoostSmallTime - 0.5f - 0.2f)
			{
				return 0;
			}
			if (m_fShiftEffectTime < m_fShiftBoostSmallTime - 0.2f)
			{
				return 1;
			}
			if (m_fShiftEffectTime < m_fShiftBoostSmallTime)
			{
				return -1;
			}
			if (m_fShiftEffectTime < m_fShiftBoostBigTime - 0.5f - 0.2f)
			{
				if (m_iPerformanceLevel > 0)
				{
					m_kSound.PlayShiftSmallFire();
				}
				return 2;
			}
			if (m_fShiftEffectTime < m_fShiftBoostBigTime - 0.2f)
			{
				return 3;
			}
			if (m_fShiftEffectTime < m_fShiftBoostBigTime)
			{
				return -1;
			}
			if (m_iPerformanceLevel > 0)
			{
				m_kSound.PlayShiftBigFire();
			}
			return 4;
		}
		return -1;
	}

	public void ChangeAutoShift(bool bAuto)
	{
		if (m_bAutoShift != bAuto)
		{
			m_bAutoShift = bAuto;
			if (m_bShiftMode)
			{
				BreakShift();
			}
		}
	}

	public void BeginShift(int dir)
	{
		if (dir > 0)
		{
			m_iStiffnessChangeFlag = -1;
			m_fShiftDir = 1f;
			m_fRotateDir = 1f;
		}
		else if (dir < 0)
		{
			m_iStiffnessChangeFlag = -1;
			m_fShiftDir = -1f;
			m_fRotateDir = -1f;
		}
		m_bShiftMode = true;
		m_kSound.Reset();
		m_kSound.PlayShiftNormal();
		float fInSpeed = (m_fInSpeed = m_car.GetVelocity());
		m_fRotateV = 1f;
		m_iCurStage = 0;
		m_car.SetAngularVec(Vector3.zero);
		m_fRotMaxAng = 100f;
		m_fShiftTime = 0f;
		m_fShiftEffectTime = 0f;
		m_fCurSpeedRotAng = 0f;
		for (int i = 0; i < 4; i++)
		{
			m_fWheelsStiffness[i] = 0.025f;
		}
		for (int j = 0; j < TSingleton<CarManager>.Instance.GetCarCount(); j++)
		{
			if (m_car == TSingleton<CarManager>.Instance.GetCarByIndex(j).vehicle)
			{
				m_carModel = TSingleton<CarManager>.Instance.GetCarByIndex(j);
				break;
			}
		}
		if (m_carModel != null)
		{
			m_fRotateV = 1f;
			m_fRotAcc[0] = m_carModel._RotateAcc0;
			m_fRotAcc[1] = m_carModel._RotateAcc1;
			m_fRotAcc[2] = m_carModel._RotateAcc2;
			m_fRotAng[0] = m_carModel._RotateAng0;
			m_fRotAng[1] = m_carModel._RotateAng1;
			m_fRotAng[2] = m_carModel._RotateAng2;
			m_fRotMaxAng = m_fRotAng[2];
			m_fInSpeed = fInSpeed;
			m_fP1BaseSpeed = m_carModel._P1Speed;
			m_fP1DownAcc = m_carModel._P1DownAcc;
			m_fP1UpAcc = m_carModel._P1UpAcc;
			m_carModel._XShiftTotalTime = 0f;
			m_carModel._XShiftRecoverTime = 0f;
			m_carRootTrans = m_carModel.m_AnimCar.transform.FindChild("root");
		}
		if (m_bAutoShift)
		{
			m_iCurStage = 3;
			m_fAutoShiftHoldTime = 0f;
			StiffnessChange = 0f;
		}
	}

	public void EndShift()
	{
		m_iStiffnessChangeFlag = 1;
		StiffnessRecover = 0f;
		m_bShiftMode = false;
		m_bShiftCD = false;
		m_fRotMaxAng = 0f;
		m_fAccumRotate = 0f;
		m_fRotMaxV = 0f;
		m_fRotateV = 0f;
		m_fRotateA = 0f;
		if (m_fShiftEffectTime > m_fShiftBoostBigTime)
		{
			m_fShiftBoost = m_fShiftBoostTime1;
			m_carModel.EnableGroundFriction(bEnable: false);
			m_carModel.StartBoost(m_fShiftBoostTime1, m_fShiftBoostMaxSpeed1);
		}
		else if (m_fShiftEffectTime > m_fShiftBoostSmallTime)
		{
			m_fShiftBoost = m_fShiftBoostTime0;
			m_carModel.EnableGroundFriction(bEnable: false);
			m_carModel.StartBoost(m_fShiftBoostTime0, m_fShiftBoostMaxSpeed0);
		}
		m_fShiftTime = 0f;
	}

	public void ShiftSpeedAdjust2()
	{
	}

	public void ShiftSpeedAdjust1()
	{
		if (m_bAutoShift)
		{
			if (InputManager.Instance.CheckHandbrake())
			{
				m_fInSpeed -= Time.deltaTime * 20f;
				if (m_fInSpeed < 1f)
				{
					m_fInSpeed = 1f;
				}
			}
			else if (InputManager.Instance.IsAccelerate())
			{
				m_fInSpeed += Time.deltaTime * 5f;
				if (m_fInSpeed > m_fCarMaxSpeed)
				{
					m_fInSpeed = m_fCarMaxSpeed;
				}
			}
			else
			{
				m_fInSpeed -= Time.deltaTime * 20f;
				if (m_fInSpeed < 1f)
				{
					m_fInSpeed = 1f;
				}
			}
		}
		else if (InputManager.Instance.CheckHandbrake())
		{
			m_fInSpeed -= Time.deltaTime * 20f;
			if (m_fInSpeed < 1f)
			{
				m_fInSpeed = 1f;
			}
		}
		else if (InputManager.Instance.IsAccelerate() && m_iCurStage == 3)
		{
			m_fInSpeed += Time.deltaTime * 5f;
			if (m_fInSpeed > m_fCarMaxSpeed)
			{
				m_fInSpeed = m_fCarMaxSpeed;
			}
		}
		else if (InputManager.Instance.IsAccelerate() && m_iCurStage < 3)
		{
			if (m_fInSpeed > m_fP1BaseSpeed)
			{
				m_fInSpeed -= Time.deltaTime * m_fP1DownAcc;
				if (m_fInSpeed <= m_fP1BaseSpeed)
				{
					m_fInSpeed = m_fP1BaseSpeed;
				}
			}
			else if (m_fInSpeed < m_fP1BaseSpeed)
			{
				m_fInSpeed += Time.deltaTime * m_fP1UpAcc;
				if (m_fInSpeed >= m_fP1BaseSpeed)
				{
					m_fInSpeed = m_fP1BaseSpeed;
				}
			}
		}
		else
		{
			m_fInSpeed -= Time.deltaTime * 20f;
			if (m_fInSpeed < 1f)
			{
				m_fInSpeed = 1f;
			}
		}
		Vector3 vecGlobal = m_car.GetVecGlobal();
		vecGlobal.Normalize();
		m_car.SetVecGlobal(m_fInSpeed * vecGlobal);
	}

	public void BreakShift()
	{
		m_iStiffnessChangeFlag = 1;
		StiffnessRecover = 0f;
		m_bShiftMode = false;
		m_fShiftDir = 0f;
		m_bShiftCD = false;
		m_fRotMaxAng = 0f;
		m_fAccumRotate = 0f;
		m_fRotMaxV = 0f;
		m_fRotateV = 0f;
		m_fRotateA = 0f;
		m_fShiftTime = 0f;
	}

	public void ResetSpeed()
	{
		float magnitude = m_car.GetVecGlobal().magnitude;
		m_car.SetVecGlobal(m_car.GetForwardGlobal() * magnitude);
	}

	private bool CheckShiftCondition()
	{
		if (m_car.GroundedType != ENMTERRAIN_TYPE.ETT_NONE)
		{
			if ((m_carModel.CarState & 0x100) != 0)
			{
				return true;
			}
			if ((m_carModel.CarState & 0x80) != 0)
			{
				return true;
			}
			if ((m_carModel.CarState & 0x40) != 0)
			{
				return true;
			}
			if (m_vehicle != null && !m_vehicle.bGroundFriction)
			{
				return true;
			}
			return false;
		}
		if (m_car.m_state == RUNNINGSTATES.RS_INAIR)
		{
			return false;
		}
		return true;
	}

	public void UpdateShift()
	{
		if (!m_bEnableShift)
		{
			return;
		}
		CONTROL_TYPE cONTROL_TYPE = InputManager.Instance.LoadControlType();
		if (cONTROL_TYPE != curType)
		{
			curType = cONTROL_TYPE;
			if (curType == CONTROL_TYPE.CONTROLTYPE_BABY || curType == CONTROL_TYPE.CONTROLTYPE_DAXIA)
			{
				ChangeAutoShift(bAuto: true);
			}
			else if (curType == CONTROL_TYPE.CONTROLTYPE_CHILD || curType == CONTROL_TYPE.CONTROLTYPE_MASTER)
			{
				ChangeAutoShift(bAuto: false);
			}
		}
		if (m_bAutoShift)
		{
			if (!m_bShiftMode && m_car.GetVelocity() > 15f && CheckShiftCondition())
			{
				if (InputManager.Instance.CurrentSteering > m_fAutoShiftMinSteer)
				{
					m_fAutoShiftHoldTime += Time.deltaTime;
					if (m_fAutoShiftHoldTime > m_fAutoShiftBeginTime)
					{
						BeginShift(1);
					}
				}
				else if (InputManager.Instance.CurrentSteering < 0f - m_fAutoShiftMinSteer)
				{
					m_fAutoShiftHoldTime += Time.deltaTime;
					if (m_fAutoShiftHoldTime > m_fAutoShiftBeginTime)
					{
						BeginShift(-1);
					}
				}
				else
				{
					m_fAutoShiftHoldTime = 0f;
				}
			}
			if (m_bShiftMode)
			{
				if (m_fShiftDir > 0f && InputManager.Instance.CurrentSteering < m_fAutoShiftBreakSteer)
				{
					BreakShift();
				}
				if (m_fShiftDir < 0f && InputManager.Instance.CurrentSteering > 0f - m_fAutoShiftBreakSteer)
				{
					BreakShift();
				}
				if (m_car.GetVelocity() <= 7f)
				{
					BreakShift();
				}
			}
		}
		else if (InputManager.Instance.CheckShift())
		{
			if (!m_bShiftMode && m_car.GetVelocity() > 15f && m_bShiftCD && CheckShiftCondition())
			{
				if (InputManager.Instance.CurrentSteering > 0.25f)
				{
					BeginShift(1);
				}
				else if (InputManager.Instance.CurrentSteering < -0.25f)
				{
					BeginShift(-1);
				}
			}
			if (!m_bShiftMode)
			{
			}
			if (m_car.GetVelocity() < 7f && m_bShiftMode)
			{
				BreakShift();
			}
		}
		else if (m_bShiftMode)
		{
			EndShift();
		}
		else
		{
			m_bShiftCD = true;
		}
		if (m_bShiftMode)
		{
			if (!m_bAutoShift && m_fRotateV >= 0f)
			{
				Vehicle vehicle = m_vehicle;
				if (vehicle != null && m_iCurStage <= 2)
				{
					if (Mathf.Abs(m_fAccumRotate) < m_fRotMaxAng)
					{
						float num = m_fRotateDir * Time.deltaTime * m_fRotateV;
						m_car.m_CenterofMassTransform.RotateAround(vehicle.chassis.vFrontMid, vehicle.chassis.GetUpGlobal(), num);
						m_fAccumRotate += num;
					}
					m_fRotateV += Time.deltaTime * m_fRotAcc[m_iCurStage];
					if (m_fRotateV < 0f)
					{
						m_fRotateV = 0f;
					}
					if (m_iCurStage <= 2 && Mathf.Abs(m_fAccumRotate) > m_fRotAng[m_iCurStage])
					{
						m_iCurStage++;
					}
					if (Mathf.Abs(m_fAccumRotate) / m_fRotMaxAng > 0.25f && InputManager.Instance.CurrentSteering * m_fRotateDir < -0.1f)
					{
						m_iCurStage = 3;
					}
					m_carModel._XShiftTotalTime += Time.deltaTime;
				}
			}
			m_fShiftTime += Time.deltaTime;
			if (m_iCurStage == 3)
			{
				if (m_vehicle.m_state != RUNNINGSTATES.RS_INAIR)
				{
					float num2 = 1f;
					num2 = Mathf.Abs(m_fShiftDir + InputManager.Instance.CurrentSteering) * 0.5f;
					num2 = Mathf.Lerp(0.2f, 1.8f, num2);
					m_fShiftEffectTime += Time.deltaTime * num2;
				}
				if (m_fShiftDir > 0f)
				{
					if (InputManager.Instance.CurrentSteering < -0.85f)
					{
						float fCurSpeedRotAng = m_fCurSpeedRotAng;
						float num3 = Mathf.Clamp01((20f - m_fCurSpeedRotAng) / 20f * 2f);
						m_fCurSpeedRotAng += 30f * Time.deltaTime * num3;
						if (m_fCurSpeedRotAng > 20f)
						{
							m_fCurSpeedRotAng = 20f;
						}
						float num4 = m_fCurSpeedRotAng - fCurSpeedRotAng;
						if (num4 > 0f)
						{
							m_car.m_CenterofMassTransform.RotateAround(m_vehicle.chassis.vFrontMid, m_vehicle.chassis.GetUpGlobal(), 0f - num4);
						}
					}
					else if (InputManager.Instance.CurrentSteering > 0f)
					{
						m_fCurSpeedRotAng = 0f;
					}
				}
				else if (m_fShiftDir < 0f)
				{
					if (InputManager.Instance.CurrentSteering > 0.85f)
					{
						float fCurSpeedRotAng2 = m_fCurSpeedRotAng;
						float num5 = Mathf.Clamp01((20f - m_fCurSpeedRotAng) / 20f * 2f);
						m_fCurSpeedRotAng += 30f * Time.deltaTime * num5;
						if (m_fCurSpeedRotAng > 20f)
						{
							m_fCurSpeedRotAng = 20f;
						}
						float num6 = m_fCurSpeedRotAng - fCurSpeedRotAng2;
						if (num6 > 0f)
						{
							m_car.m_CenterofMassTransform.RotateAround(m_vehicle.chassis.vFrontMid, m_vehicle.chassis.GetUpGlobal(), num6);
						}
					}
					else if (InputManager.Instance.CurrentSteering < 0f)
					{
						m_fCurSpeedRotAng = 0f;
					}
				}
			}
		}
		if (m_iStiffnessChangeFlag > 0)
		{
			Vehicle vehicle2 = m_vehicle;
			if (vehicle2 != null)
			{
				int num7 = 0;
				NxWheel[] nxwheels = vehicle2.chassis.Nxwheels;
				if (m_bAutoShift)
				{
					num7 = nxwheels.Length;
				}
				else
				{
					for (int i = 0; i < nxwheels.Length; i++)
					{
						NxWheel nxWheel = nxwheels[i];
						float num8 = m_fWheelsStiffness[i];
						num8 += 2f * Time.deltaTime;
						if (num8 > 1f)
						{
							num8 = 1f;
							num7++;
						}
						m_fWheelsStiffness[i] = num8;
						nxWheel.AdjustSideStiffness(num8);
					}
				}
				bool flag = false;
				if (null != m_carModel.m_AnimCar)
				{
					if (Mathf.Abs(m_fModelRotAng) != 0f)
					{
						float num9 = Time.deltaTime * 3f * m_fModelRotV * (0f - Mathf.Sign(m_fModelRotAng));
						if (Mathf.Abs(num9) > Mathf.Abs(m_fModelRotAng))
						{
							num9 = 0f - m_fModelRotAng;
							m_fModelRotAng = 0f;
						}
						else
						{
							m_fModelRotAng += num9;
						}
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (num7 == nxwheels.Length && flag)
				{
					m_iStiffnessChangeFlag = 0;
				}
			}
		}
		else if (m_iStiffnessChangeFlag < 0)
		{
			Vehicle vehicle3 = m_vehicle;
			if (vehicle3 != null)
			{
				int num10 = 0;
				NxWheel[] nxwheels2 = vehicle3.chassis.Nxwheels;
				if (m_bAutoShift)
				{
					num10 = nxwheels2.Length;
				}
				else
				{
					for (int j = 0; j < nxwheels2.Length; j++)
					{
						NxWheel nxWheel2 = nxwheels2[j];
						if (m_iCurStage <= 2)
						{
							nxWheel2.AdjustSideStiffness(0.025f);
							m_fWheelsStiffness[j] = 0.025f;
							continue;
						}
						float num11 = m_fWheelsStiffness[j];
						num11 += 0.18f * m_fStiffnessFactor * Time.deltaTime;
						if (j == 0 || j == 1)
						{
							if (num11 > 0.1f)
							{
								num11 = 0.1f;
								num10++;
							}
						}
						else if (num11 > 0.1f)
						{
							num11 = 0.1f;
							num10++;
						}
						m_fWheelsStiffness[j] = num11;
						nxWheel2.AdjustSideStiffness(num11);
					}
				}
				bool flag2 = false;
				if (null != m_carModel.m_AnimCar)
				{
					if (Mathf.Abs(m_fModelRotAng) < m_fModelRotMaxAng && (m_bAutoShift || m_iCurStage >= 2))
					{
						float num12 = m_fRotateDir * Time.deltaTime * m_fModelRotV;
						m_fModelRotAng += num12;
						if (Mathf.Abs(m_fModelRotAng) >= m_fModelRotMaxAng)
						{
							num12 -= m_fRotateDir * (Mathf.Abs(m_fModelRotAng) - m_fModelRotMaxAng);
							m_fModelRotAng = m_fRotateDir * m_fModelRotMaxAng;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag2 = true;
				}
				if (num10 == nxwheels2.Length && flag2)
				{
					m_iStiffnessChangeFlag = 0;
				}
				if (num10 != nxwheels2.Length)
				{
					m_carModel._XShiftRecoverTime += Time.deltaTime;
				}
			}
		}
		if (null != m_carRootTrans)
		{
			Vector3 localEulerAngles = m_carRootTrans.localEulerAngles;
			localEulerAngles.z = m_fModelRotAng;
			m_carRootTrans.localEulerAngles = localEulerAngles;
		}
		if (!m_bAutoShift && m_fShiftBoost > 0f)
		{
			m_fShiftBoost -= Time.deltaTime;
			FollowCamera.Instance.ShiftScaleAdjust(enable: true, 0.75f);
			if (m_fShiftBoost <= 0f)
			{
				m_carModel.StopBoost();
				m_fShiftBoost = 0f;
				FollowCamera.Instance.ShiftScaleAdjust(enable: false, 0.75f);
				m_carModel.EnableGroundFriction(bEnable: true);
			}
		}
	}

	public float AdjustShiftSteering(float fSteer, float fAngle)
	{
		float num = fAngle;
		if (m_bAutoShift)
		{
			if (m_fShiftDir > 0f)
			{
				num = Mathf.Abs(Mathf.Clamp(fSteer * 2f, -1f, 1f) + 1f) * AutoAdjustSteerRange + AutoAdjustSteerMin;
				if (num > fAngle)
				{
					num = fAngle;
				}
			}
			else if (m_fShiftDir < 0f)
			{
				num = (0f - Mathf.Abs(Mathf.Clamp(fSteer * 2f, -1f, 1f) - 1f)) * AutoAdjustSteerRange - AutoAdjustSteerMin;
				if (num < fAngle)
				{
					num = fAngle;
				}
			}
		}
		else
		{
			if (m_fShiftDir > 0f)
			{
				num = Mathf.Abs(Mathf.Clamp(fSteer * 2f, -1f, 1f) + 1f) * AdjustSteerRange + AdjustSteerMin;
			}
			else if (m_fShiftDir < 0f)
			{
				num = (0f - Mathf.Abs(Mathf.Clamp(fSteer * 2f, -1f, 1f) - 1f)) * AdjustSteerRange - AdjustSteerMin;
			}
			if (m_iCurStage <= 2)
			{
				num = 0f;
			}
		}
		return num;
	}

	public bool RequireModifyMaxSteering()
	{
		if (m_bAutoShift)
		{
			return m_bShiftMode;
		}
		return m_iStiffnessChangeFlag > 0;
	}

	public float ModifyMaxSteering(float fSteer)
	{
		if (m_bAutoShift)
		{
			if (m_bShiftMode)
			{
				if (m_iStiffnessChangeFlag > 0)
				{
					float num = AutoAdjustSteerRange * 2f + AutoAdjustSteerMin;
					return fSteer;
				}
				if (m_iStiffnessChangeFlag < 0)
				{
					return AutoAdjustSteerRange * 2f + AutoAdjustSteerMin;
				}
				return AutoAdjustSteerRange * 2f + AutoAdjustSteerMin;
			}
			return fSteer;
		}
		if (m_iStiffnessChangeFlag > 0)
		{
			float num2 = AdjustSteerRange * 2f + AdjustSteerMin;
			StiffnessRecover += Time.deltaTime;
			if (StiffnessRecover >= 0.5f)
			{
				StiffnessRecover = 0.5f;
			}
			return Mathf.Lerp(num2, fSteer, StiffnessRecover / 0.5f);
		}
		return fSteer;
	}

	public void SetPhyXData(VehicleData phydata)
	{
		AdjustSteerMin = phydata.fShiftMinSteer;
		AdjustSteerRange = (phydata.fShiftMaxSteer - phydata.fShiftMinSteer) * 0.5f;
		AutoAdjustSteerMin = phydata.fShiftMinSteer;
		AutoAdjustSteerRange = (phydata.fShiftAutoMaxSteer - phydata.fShiftMinSteer) * 0.5f;
		if (m_carModel != null)
		{
			m_carModel._P1Speed = phydata.fShiftP1Speed;
			m_carModel._P1DownAcc = phydata.fShiftP1DownAcc;
			m_carModel._P1UpAcc = phydata.fShiftP1UpAcc;
			m_carModel._RotateAng0 = phydata.fShiftAng0;
			m_carModel._RotateAng1 = phydata.fShiftAng1;
			m_carModel._RotateAng2 = phydata.fShiftAng2;
			m_carModel._RotateAcc0 = phydata.fShiftAcc0;
			m_carModel._RotateAcc1 = phydata.fShiftAcc1;
			m_carModel._RotateAcc2 = phydata.fShiftAcc2;
		}
		else
		{
			m_fP1BaseSpeed = phydata.fShiftP1Speed;
			m_fP1DownAcc = phydata.fShiftP1DownAcc;
			m_fP1UpAcc = phydata.fShiftP1UpAcc;
			m_fRotAcc[0] = phydata.fShiftAcc0;
			m_fRotAcc[1] = phydata.fShiftAcc1;
			m_fRotAcc[2] = phydata.fShiftAcc2;
			m_fRotAng[0] = phydata.fShiftAng0;
			m_fRotAng[1] = phydata.fShiftAng1;
			m_fRotAng[2] = phydata.fShiftAng2;
		}
		m_fShiftBoostMaxSpeed0 = phydata.fShiftBoostSpeed0;
		m_fShiftBoostMaxSpeed1 = phydata.fShiftBoostSpeed1;
		m_fShiftBoostTime0 = phydata.fShiftBoostTime0;
		m_fShiftBoostTime1 = phydata.fShiftBoostTime1;
		m_fShiftBoostSmallTime = phydata.fShiftBoostSmallTime;
		m_fShiftBoostBigTime = phydata.fShiftBoostBigTime;
		m_fCarMaxSpeed = phydata.fShiftMaxSpeed;
		m_fCarMaxSpeedAuto = phydata.fShiftAutoMaxSpeed;
		m_fShiftAutoMotorScale = phydata.fShiftAutoMotorScale;
		m_fStiffnessFactor = phydata.fShiftStiffnessFactor;
	}

	private float CalculateTorque(float speed, float motortorque, float friction, float upAcc, float downAcc)
	{
		if (m_fInSpeed < speed - 0.25f)
		{
			return Mathf.Max(motortorque, friction * 0.1f + 2500f * upAcc * Mathf.Min((speed - m_fInSpeed) * 0.75f, 1f));
		}
		if ((double)m_fInSpeed > (double)speed + 0.25)
		{
			return Mathf.Min(motortorque, friction * 0.1f - 2500f * downAcc * Mathf.Min((m_fInSpeed - speed) * 0.75f, 1f));
		}
		return friction * 0.1f;
	}

	public float ModifyTorque(float motortorque, float friction)
	{
		m_fInSpeed = m_car.GetVelocity();
		if (m_car.GetBoostMode())
		{
			return motortorque;
		}
		float num = 1f;
		if (m_car.fLimitSpeed == m_carModel.SlowSpeed)
		{
			num = 0.5f;
		}
		if (m_bAutoShift)
		{
			num *= m_fShiftAutoMotorScale;
		}
		if (m_bAutoShift)
		{
			return CalculateTorque(m_fCarMaxSpeedAuto, motortorque, friction, 1f * num, 1f * num);
		}
		if (m_iCurStage == 3)
		{
			return CalculateTorque(m_fCarMaxSpeed, motortorque, friction, 1f * num, 1f * num);
		}
		if (m_iCurStage < 3)
		{
			return CalculateTorque(m_fP1BaseSpeed, motortorque, friction, m_fP1UpAcc * num, m_fP1DownAcc * num);
		}
		return motortorque;
	}
}
