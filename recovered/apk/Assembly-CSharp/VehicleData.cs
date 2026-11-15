using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleData
{
	public struct Streering
	{
		public float Angle;

		public float Velocity;

		public Streering(float t, float r)
		{
			Angle = t;
			Velocity = r;
		}
	}

	private class CurveCompare : IComparer
	{
		public int Compare(object x, object y)
		{
			float num = ((Streering)x).Velocity - ((Streering)y).Velocity;
			if (num < 0f)
			{
				return -1;
			}
			if (num > 0f)
			{
				return 1;
			}
			return 0;
		}
	}

	public int m_iVehicleType;

	public float fWheelBase = 1f;

	public float b = 0.5f;

	public float c = 0.5f;

	public float inertia = 1500f;

	public float fVehicleHeight = 1f;

	public float m_fMass = 2000f;

	public float fChassisHeight;

	public float fMaxSpeed = 30f;

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

	public float fShiftStiffnessFactor = 1f;

	public float fShiftMaxSpeed = 30f;

	public float fShiftAutoMotorScale = 1f;

	public float fShiftAutoMaxSpeed = 29f;

	public float fShiftAutoMaxSteer = 3.5f;

	public float fSmallTorqueScale = 1f;

	public float fBigTorqueScale = 1f;

	public List<CurvePair> m_lstMotorCurve0 = new List<CurvePair>();

	public List<CurvePair> m_lstMotorCurve1 = new List<CurvePair>();

	public float m_fextremumVelocity = 38f;

	private float m_fminimumVelocity = 3f;

	public ArrayList m_kSteeringCurve = new ArrayList();

	public bool Load(int ID)
	{
		if (ID == -1)
		{
			LoadStandardSteeringConfig();
			LoadStandardMotorConfig();
			LoadMassData();
		}
		else
		{
			LoadNPCSteeringConfig();
			LoadNPCMotorConfig();
		}
		return true;
	}

	private void LoadMassData()
	{
		m_fMass = 2000f;
	}

	private void LoadStandardSteeringConfig()
	{
		float t = Mathf.Asin(fWheelBase / 3f) * 57.2957f;
		Streering streering = new Streering(t, 3f);
		m_kSteeringCurve.Add(streering);
		t = Mathf.Asin(fWheelBase / 12f) * 57.2957f;
		Streering streering2 = new Streering(t, 15f);
		m_kSteeringCurve.Add(streering2);
		t = Mathf.Asin(fWheelBase / 20f) * 57.2957f;
		Streering streering3 = new Streering(t, 18f);
		m_kSteeringCurve.Add(streering3);
		t = Mathf.Asin(fWheelBase / 25f) * 57.2957f;
		Streering streering4 = new Streering(t, 30f);
		m_kSteeringCurve.Add(streering4);
		CurveCompare comparer = new CurveCompare();
		m_kSteeringCurve.Sort(comparer);
		m_fminimumVelocity = ((Streering)m_kSteeringCurve[0]).Velocity;
		m_fextremumVelocity = ((Streering)m_kSteeringCurve[m_kSteeringCurve.Count - 1]).Velocity;
	}

	private void LoadStandardMotorConfig()
	{
		CurvePair item = new CurvePair(1600f, 1000f);
		m_lstMotorCurve0.Add(item);
		CurvePair item2 = new CurvePair(867.5f, 2300f);
		m_lstMotorCurve0.Add(item2);
		CurvePair item3 = new CurvePair(600f, 3000f);
		m_lstMotorCurve0.Add(item3);
		CurvePair item4 = new CurvePair(500f, 4000f);
		m_lstMotorCurve0.Add(item4);
		CurvePair item5 = new CurvePair(350f, 4300f);
		m_lstMotorCurve0.Add(item5);
		CurvePair item6 = new CurvePair(150f, 5000f);
		m_lstMotorCurve0.Add(item6);
		CurvePair item7 = new CurvePair(1600f, 200f);
		m_lstMotorCurve1.Add(item7);
		CurvePair item8 = new CurvePair(1200f, 700f);
		m_lstMotorCurve1.Add(item8);
		CurvePair item9 = new CurvePair(200f, 1200f);
		m_lstMotorCurve1.Add(item9);
		CurvePair item10 = new CurvePair(10f, 2000f);
		m_lstMotorCurve1.Add(item10);
	}

	private void LoadNPCSteeringConfig()
	{
		float t = Mathf.Asin(fWheelBase / 3f) * 57.2957f;
		Streering streering = new Streering(t, 3f);
		m_kSteeringCurve.Add(streering);
		t = Mathf.Asin(fWheelBase / 12f) * 57.2957f;
		Streering streering2 = new Streering(t, 15f);
		m_kSteeringCurve.Add(streering2);
		t = Mathf.Asin(fWheelBase / 20f) * 57.2957f;
		Streering streering3 = new Streering(t, 18f);
		m_kSteeringCurve.Add(streering3);
		t = Mathf.Asin(fWheelBase / 25f) * 57.2957f;
		Streering streering4 = new Streering(t, 30f);
		m_kSteeringCurve.Add(streering4);
		CurveCompare comparer = new CurveCompare();
		m_kSteeringCurve.Sort(comparer);
		m_fminimumVelocity = ((Streering)m_kSteeringCurve[0]).Velocity;
		m_fextremumVelocity = ((Streering)m_kSteeringCurve[m_kSteeringCurve.Count - 1]).Velocity;
	}

	private void LoadNPCMotorConfig()
	{
		CurvePair item = new CurvePair(1600f, 1000f);
		m_lstMotorCurve0.Add(item);
		CurvePair item2 = new CurvePair(867.5f, 2300f);
		m_lstMotorCurve0.Add(item2);
		CurvePair item3 = new CurvePair(600f, 2700f);
		m_lstMotorCurve0.Add(item3);
		CurvePair item4 = new CurvePair(350f, 3200f);
		m_lstMotorCurve0.Add(item4);
		CurvePair item5 = new CurvePair(250f, 3500f);
		m_lstMotorCurve0.Add(item5);
		CurvePair item6 = new CurvePair(100f, 4000f);
		m_lstMotorCurve0.Add(item6);
		CurvePair item7 = new CurvePair(1600f, 200f);
		m_lstMotorCurve1.Add(item7);
		CurvePair item8 = new CurvePair(1200f, 700f);
		m_lstMotorCurve1.Add(item8);
		CurvePair item9 = new CurvePair(200f, 1200f);
		m_lstMotorCurve1.Add(item9);
		CurvePair item10 = new CurvePair(10f, 2000f);
		m_lstMotorCurve1.Add(item10);
	}

	public float GetInterpolatePointFromCurve(float fVelocity)
	{
		if (fVelocity < m_fminimumVelocity)
		{
			fVelocity = m_fminimumVelocity;
		}
		if (fVelocity > m_fextremumVelocity)
		{
			fVelocity = m_fextremumVelocity;
		}
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = fVelocity;
		float result = 0f;
		int num6 = 0;
		Streering streering = (Streering)m_kSteeringCurve[0];
		if (fVelocity < streering.Velocity)
		{
			result = streering.Angle;
		}
		else
		{
			for (int i = 0; i < m_kSteeringCurve.Count; i++)
			{
				num6++;
				if (num6 < m_kSteeringCurve.Count)
				{
					Streering streering2 = (Streering)m_kSteeringCurve[num6];
					if (streering2.Velocity >= fVelocity)
					{
						Streering streering3 = (Streering)m_kSteeringCurve[i];
						num = streering3.Velocity;
						num2 = streering3.Angle;
						num3 = streering2.Velocity;
						num4 = streering2.Angle;
						result = num2 + (num5 - num) * (num4 - num2) / (num3 - num);
						break;
					}
				}
				else
				{
					result = ((Streering)m_kSteeringCurve[i]).Angle;
				}
			}
		}
		return result;
	}

	public float GetMotorFactor(float fV)
	{
		float num = 1f;
		if (fV < 25f)
		{
			return 1f;
		}
		return fV / 25f;
	}
}
