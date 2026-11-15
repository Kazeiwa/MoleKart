using System.Collections;
using System.Collections.Generic;

public class VehicleData
{
	public struct Streering
	{
		public float Angle;

		public float Velocity;

		public Streering(float t, float r)
		{
		}
	}

	private class CurveCompare : IComparer
	{
		public int Compare(object x, object y)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int m_iVehicleType;

	public float fWheelBase;

	public float b;

	public float c;

	public float inertia;

	public float fVehicleHeight;

	public float m_fMass;

	public float fChassisHeight;

	public float fMaxSpeed;

	public float fShiftMinSteer;

	public float fShiftMaxSteer;

	public float fShiftP1Speed;

	public float fShiftP1DownAcc;

	public float fShiftP1UpAcc;

	public float fShiftAng0;

	public float fShiftAng1;

	public float fShiftAng2;

	public float fShiftAcc0;

	public float fShiftAcc1;

	public float fShiftAcc2;

	public float fShiftBoostSpeed0;

	public float fShiftBoostSpeed1;

	public float fShiftBoostTime0;

	public float fShiftBoostTime1;

	public float fShiftBoostSmallTime;

	public float fShiftBoostBigTime;

	public float fShiftStiffnessFactor;

	public float fShiftMaxSpeed;

	public float fModelRotV;

	public float fModelRotManualScale;

	public float fMinTimeStopP1;

	public float fShiftAutoMotorScale;

	public float fShiftAutoMaxSpeed;

	public float fShiftAutoMaxSteer;

	public float fSmallTorqueScale;

	public float fBigTorqueScale;

	public List<CurvePair> m_lstMotorCurve0;

	public List<CurvePair> m_lstMotorCurve1;

	public float m_fextremumVelocity;

	private float m_fminimumVelocity;

	public ArrayList m_kSteeringCurve;

	public bool Load(int ID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void LoadMassData()
	{
	}

	private void LoadStandardSteeringConfig()
	{
	}

	private void LoadStandardMotorConfig()
	{
	}

	private void LoadNPCSteeringConfig()
	{
	}

	private void LoadNPCMotorConfig()
	{
	}

	public float GetInterpolatePointFromCurve(float fVelocity)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetMotorFactor(float fV)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
