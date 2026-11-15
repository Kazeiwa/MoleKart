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

	public bool m_bEnableShift;

	public bool m_bAutoShift;

	private VehicleShiftSound m_kSound;

	public float m_fShiftDir;

	public bool m_bShiftCD;

	public float[] m_fWheelsStiffness;

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

	public float m_fModelRotMaxAng;

	public float m_fModelRotV;

	public float m_fShiftBoostMaxSpeed0;

	public float m_fShiftBoostMaxSpeed1;

	public float m_fShiftBoostTime0;

	public float m_fShiftBoostTime1;

	private float[] m_fRotAcc;

	private float[] m_fRotAng;

	private int m_iCurStage;

	public float m_fP1BaseSpeed;

	public float m_fP1DownAcc;

	public float m_fP1UpAcc;

	public float m_fShiftBoostSmallTime;

	public float m_fShiftBoostBigTime;

	private float m_fShiftEffectTime;

	public float m_fCarMaxSpeed;

	public float m_fCarMaxSpeedAuto;

	public float m_fShiftAutoMotorScale;

	public float m_fStiffnessFactor;

	public float m_fAutoShiftBeginTime;

	public float m_fAutoShiftMinSteer;

	public float m_fAutoShiftBreakSteer;

	private float m_fAutoShiftHoldTime;

	private float m_fTorqueScale;

	private float m_fCurSpeedRotAng;

	private int m_iPerformanceLevel;

	private Transform m_carRootTrans;

	private int m_iBoostTriggerState;

	private float StiffnessRecover;

	private float StiffnessChange;

	private CONTROL_TYPE curType;

	public float AdjustSteerRange;

	public float AdjustSteerMin;

	public float AutoAdjustSteerRange;

	public float AutoAdjustSteerMin;

	public void InitComponent(btVehicleBase v)
	{
	}

	public bool IsShiftMode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetShiftFirePower()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetBoostTriggerState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool CanDrawTireEffect()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetTorqueScale()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetShiftLevel()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ChangeAutoShift(bool bAuto)
	{
	}

	public void BeginShift(int dir)
	{
	}

	public void EndShift()
	{
	}

	public void ShiftSpeedAdjust2()
	{
	}

	public void ShiftSpeedAdjust1()
	{
	}

	public void BreakShift()
	{
	}

	public void ResetSpeed()
	{
	}

	private bool CheckShiftCondition()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UpdateShift()
	{
	}

	public float AdjustShiftSteering(float fSteer, float fAngle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool RequireModifyMaxSteering()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float ModifyMaxSteering(float fSteer)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetPhyXData(VehicleData phydata)
	{
	}

	private float CalculateTorque(float speed, float motortorque, float friction, float upAcc, float downAcc)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float ModifyTorque(float motortorque, float friction)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
