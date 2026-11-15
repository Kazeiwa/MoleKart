using System.Collections;
using System.Collections.Generic;

public class MotorControl
{
	private enum ACCELSTATE
	{
		ACC_NONE = -1,
		ACC_FORWARD,
		ACC_BACKWORD,
		ACC_KEEP
	}

	private struct TorqueRPM
	{
		public float Torque;

		public float RPM;

		public TorqueRPM(float t, float r)
		{
			Torque = t;
			RPM = r;
		}
	}

	private class CurveCompare : IComparer
	{
		public int Compare(object x, object y)
		{
			float num = ((TorqueRPM)x).RPM - ((TorqueRPM)y).RPM;
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

	private ACCELSTATE m_state;

	private ArrayList kCurve0 = new ArrayList();

	private ArrayList kCurve1 = new ArrayList();

	private float m_fextremumRPM = 6000f;

	private float m_fminimumRPM = 1000f;

	private float m_fRPM;

	private float m_fMotorForce;

	public float fAccelCoefficient = 1f;

	public float fDecAccelCoefficient = 1f;

	public MotorControl(VehicleData kData)
	{
		m_state = ACCELSTATE.ACC_NONE;
		LoadConfig(kData.m_lstMotorCurve0, kData.m_lstMotorCurve1);
		m_fRPM = 1000f;
		m_fMotorForce = GetInterpolatePointFromCurve(m_fRPM);
	}

	private void LoadConfig(List<CurvePair> kCP0, List<CurvePair> kCP1)
	{
		foreach (CurvePair item in kCP0)
		{
			TorqueRPM torqueRPM = new TorqueRPM(item.fX, item.fY);
			kCurve0.Add(torqueRPM);
		}
		CurveCompare comparer = new CurveCompare();
		kCurve0.Sort(comparer);
		m_fminimumRPM = ((TorqueRPM)kCurve0[0]).RPM;
		m_fextremumRPM = ((TorqueRPM)kCurve0[kCurve0.Count - 1]).RPM;
		foreach (CurvePair item2 in kCP1)
		{
			TorqueRPM torqueRPM2 = new TorqueRPM(item2.fX, item2.fY);
			kCurve1.Add(torqueRPM2);
		}
		kCurve1.Sort(comparer);
	}

	private float GetInterpolatePointFromCurve(float fRpm)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float result = 0f;
		int num5 = 0;
		TorqueRPM torqueRPM = (TorqueRPM)kCurve0[0];
		if (fRpm < torqueRPM.RPM)
		{
			result = torqueRPM.Torque;
		}
		else
		{
			for (int i = 0; i < kCurve0.Count; i++)
			{
				num5++;
				if (num5 < kCurve0.Count)
				{
					TorqueRPM torqueRPM2 = (TorqueRPM)kCurve0[num5];
					if (torqueRPM2.RPM >= fRpm)
					{
						TorqueRPM torqueRPM3 = (TorqueRPM)kCurve0[i];
						num = torqueRPM3.RPM;
						num2 = torqueRPM3.Torque;
						num3 = torqueRPM2.RPM;
						num4 = torqueRPM2.Torque;
						result = num2 + (fRpm - num) * (num4 - num2) / (num3 - num);
						break;
					}
				}
				else
				{
					result = ((TorqueRPM)kCurve0[i]).Torque;
				}
			}
		}
		return result;
	}

	private float GetInterpolatePointFromCurve1(float fRpm)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float result = 0f;
		int num5 = 0;
		TorqueRPM torqueRPM = (TorqueRPM)kCurve1[0];
		if (fRpm < torqueRPM.RPM)
		{
			result = torqueRPM.Torque;
		}
		else
		{
			for (int i = 0; i < kCurve1.Count; i++)
			{
				num5++;
				if (num5 < kCurve1.Count)
				{
					TorqueRPM torqueRPM2 = (TorqueRPM)kCurve1[num5];
					if (torqueRPM2.RPM >= fRpm)
					{
						TorqueRPM torqueRPM3 = (TorqueRPM)kCurve1[i];
						num = torqueRPM3.RPM;
						num2 = torqueRPM3.Torque;
						num3 = torqueRPM2.RPM;
						num4 = torqueRPM2.Torque;
						result = num2 + (fRpm - num) * (num4 - num2) / (num3 - num);
						break;
					}
				}
				else
				{
					result = ((TorqueRPM)kCurve1[i]).Torque;
				}
			}
		}
		return result;
	}

	public float GetMotorForce(float fRPM, float fSteering, ENMMOTIONDIR AccelDir, int nUseAccelItemStatus)
	{
		if ((AccelDir & ENMMOTIONDIR.ECMD_FORWARD) == ENMMOTIONDIR.ECMD_FORWARD)
		{
			m_state = ACCELSTATE.ACC_FORWARD;
			m_fRPM = fRPM;
		}
		else if ((AccelDir & ENMMOTIONDIR.ECMD_BACK) == ENMMOTIONDIR.ECMD_BACK)
		{
			m_state = ACCELSTATE.ACC_BACKWORD;
			m_fRPM = fRPM;
		}
		else if ((AccelDir & ENMMOTIONDIR.ECMD_KEEPCURRENT) == ENMMOTIONDIR.ECMD_KEEPCURRENT)
		{
			m_state = ACCELSTATE.ACC_KEEP;
		}
		else
		{
			m_state = ACCELSTATE.ACC_NONE;
		}
		switch (m_state)
		{
		case ACCELSTATE.ACC_FORWARD:
		case ACCELSTATE.ACC_KEEP:
			if (m_fRPM < m_fminimumRPM)
			{
				m_fRPM = m_fminimumRPM;
			}
			if (m_fRPM > m_fextremumRPM)
			{
				m_fRPM = m_fextremumRPM;
			}
			m_fMotorForce = GetInterpolatePointFromCurve(m_fRPM) * fAccelCoefficient;
			break;
		case ACCELSTATE.ACC_BACKWORD:
			if (m_fRPM < 200f)
			{
				m_fRPM = 200f;
			}
			if (m_fRPM > 2000f)
			{
				m_fRPM = 2000f;
			}
			m_fMotorForce = GetInterpolatePointFromCurve1(m_fRPM) * fDecAccelCoefficient;
			break;
		default:
			m_fMotorForce = 0f;
			break;
		}
		return m_fMotorForce;
	}
}
