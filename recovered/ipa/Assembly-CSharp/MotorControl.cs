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
		}
	}

	private class CurveCompare : IComparer
	{
		public int Compare(object x, object y)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private ACCELSTATE m_state;

	private ArrayList kCurve0;

	private ArrayList kCurve1;

	private float m_fextremumRPM;

	private float m_fminimumRPM;

	private float m_fRPM;

	private float m_fMotorForce;

	public float fAccelCoefficient;

	public float fDecAccelCoefficient;

	public MotorControl(VehicleData kData)
	{
	}

	private void LoadConfig(List<CurvePair> kCP0, List<CurvePair> kCP1)
	{
	}

	private float GetInterpolatePointFromCurve(float fRpm)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float GetInterpolatePointFromCurve1(float fRpm)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetMotorForce(float fRPM, float fSteering, ENMMOTIONDIR AccelDir, int nUseAccelItemStatus)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
