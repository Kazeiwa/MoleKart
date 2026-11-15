using System;
using UnityEngine;

public class FlySimulator : ISimulator
{
	private struct TVector
	{
		public float i;

		public float j;

		public float k;
	}

	private TVector v;

	protected bool m_bStart;

	protected Vector3 m_vStartPosition;

	protected Vector3 m_vVelocityDir;

	private float fPitch;

	private bool bAdjustOnce = true;

	private float fAngularVel = -30f;

	private btVehicleBase m_car;

	public float Vm;

	public float Alpha;

	private float Gamma;

	private float L;

	private float Yb;

	private TVector s;

	private float time;

	private float tInc;

	private float g;

	private float m;

	private float Vw;

	private float GammaW;

	private float Cw;

	private float Cd;

	public FlySimulator(btVehicleBase car)
	{
		m_car = car;
		m_fRotX = (m_fRotY = (m_fRotZ = 0f));
		InitializeVariables();
	}

	private void InitializeVariables()
	{
		Vm = 39f;
		Alpha = 75f;
		Gamma = 0f;
		L = 0.01f;
		Yb = 0f;
		s.i = 0f;
		s.j = 0f;
		s.k = 0f;
		time = 0f;
		tInc = 0.05f;
		g = 9.8f;
		m = 100f;
		Vw = 0f;
		GammaW = 90f;
		Cw = 10f;
		Cd = 45f;
	}

	public Vector3 GetVelocity()
	{
		if (m_bStart)
		{
			return m_vVelocityDir * v.i + new Vector3(0f, v.j, 0f) + v.k * m_car.GetRightGlobal();
		}
		return Vector3.zero;
	}

	public bool IsStarted()
	{
		return m_bStart;
	}

	public void BeginSimulator()
	{
		if (!m_bStart)
		{
			m_bStart = true;
			Vm = m_car.GetVecGlobal().magnitude;
			m_vStartPosition = m_car.GetPosition();
			m_vVelocityDir = m_car.GetVecGlobal();
			m_vVelocityDir.y = 0f;
			m_vVelocityDir.Normalize();
			s.i = 0f;
			s.j = 0f;
			s.k = 0f;
			v.i = 0f;
			v.j = 0f;
			v.k = 0f;
			time = 0f;
			bAdjustOnce = true;
			fAngularVel = -20f;
		}
	}

	public void BeginSimulator(Vector3 speed)
	{
		if (!m_bStart)
		{
			m_bStart = true;
			Vm = speed.magnitude;
			m_vStartPosition = m_car.GetPosition();
			m_vVelocityDir = speed;
			m_vVelocityDir.y = 0f;
			m_vVelocityDir.Normalize();
			s.i = 0f;
			s.j = 0f;
			s.k = 0f;
			v.i = 0f;
			v.j = 0f;
			v.k = 0f;
			time = 0f;
			bAdjustOnce = true;
			fAngularVel = -30f;
			fPitch = m_car.GetEulerAngleX();
			if (fPitch > 180f)
			{
				fPitch -= 360f;
			}
			if (fPitch > 0f)
			{
				fPitch = -20f;
			}
		}
	}

	public void StopSimulator()
	{
		m_bStart = false;
	}

	public override void Update(float fTickTime)
	{
		if (!m_bStart)
		{
			return;
		}
		tInc = fTickTime * 1.5f;
		DoSimulation();
		Vector3 position = m_vStartPosition + s.i * m_vVelocityDir + new Vector3(0f, s.j, 0f) + s.k * m_car.GetRightGlobal();
		m_car.SetPosition(position);
		if (bAdjustOnce)
		{
			fAngularVel += 30f * tInc;
			fPitch += fAngularVel * tInc;
			if (fPitch > 0f)
			{
				fPitch = 0f;
				bAdjustOnce = false;
			}
			m_car.SetEulerAngleX(fPitch);
		}
	}

	private int DoSimulation2()
	{
		time += tInc;
		float num = L * Mathf.Cos((90f - Alpha) * 3.14f / 180f);
		float num2 = num * Mathf.Cos(Gamma * 3.14f / 180f);
		float num3 = L * Mathf.Cos(Alpha * 3.14f / 180f);
		float num4 = num * Mathf.Sin(Gamma * 3.14f / 180f);
		float num5 = num2 / L;
		float num6 = num3 / L;
		float num7 = num4 / L;
		float num8 = L * Mathf.Cos((90f - Alpha) * 3.14f / 180f) * Mathf.Cos(Gamma * 3.14f / 180f);
		float num9 = L * Mathf.Cos((90f - Alpha) * 3.14f / 180f) * Mathf.Sin(Gamma * 3.14f / 180f);
		float num10 = num8;
		float num11 = Vm * num5;
		float num12 = Yb + L * Mathf.Cos(Alpha * 3.14f / 180f);
		float num13 = Vm * num6;
		float num14 = num9;
		float num15 = Vm * num7;
		TVector tVector = s;
		s.i = m / Cd * Mathf.Exp((0f - Cd * time) / m) * ((0f - Cw) * Vw * Mathf.Cos(GammaW * 3.14f / 180f) / Cd - num11) - Cw * Vw * Mathf.Cos(GammaW * 3.14f / 180f) * time / Cd - m / Cd * ((0f - Cw) * Vw * Mathf.Cos(GammaW * 3.14f / 180f) / Cd - num11) + num10;
		s.j = num12 + ((0f - (num13 + m * g / Cd)) * (m / Cd) * Mathf.Exp((0f - Cd * time) / m) - m * g * time / Cd) + m / Cd * (num13 + m * g / Cd);
		s.k = m / Cd * Mathf.Exp((0f - Cd * time) / m) * ((0f - Cw) * Vw * Mathf.Sin(GammaW * 3.14f / 180f) / Cd - num15) - Cw * Vw * Mathf.Sin(GammaW * 3.14f / 180f) * time / Cd - m / Cd * ((0f - Cw) * Vw * Mathf.Sin(GammaW * 3.14f / 180f) / Cd - num15) + num14;
		v.i = (s.i - tVector.i) / tInc;
		v.j = (s.j - tVector.j) / tInc;
		v.k = (s.k - tVector.k) / tInc;
		if (s.j <= 0f)
		{
			return 2;
		}
		if (time > 30f)
		{
			return 3;
		}
		return 0;
	}

	private int DoSimulation()
	{
		time += tInc;
		float f = Alpha * ((float)Math.PI / 180f);
		float f2 = (90f - Alpha) * ((float)Math.PI / 180f);
		float f3 = Gamma * ((float)Math.PI / 180f);
		float f4 = GammaW * ((float)Math.PI / 180f);
		float num = Mathf.Cos(f);
		float num2 = Mathf.Sin(f3);
		float num3 = Mathf.Cos(f3);
		float num4 = Mathf.Cos(f2);
		float num5 = Mathf.Sin(f4);
		float num6 = Mathf.Cos(f4);
		float num7 = L * num4;
		float num8 = num7 * num3;
		float num9 = L * num;
		float num10 = num7 * num2;
		float num11 = num8 / L;
		float num12 = num9 / L;
		float num13 = num10 / L;
		float num14 = L * num4 * num3;
		float num15 = L * num4 * num2;
		float num16 = num14;
		float num17 = Vm * num11;
		float num18 = Yb + L * num;
		float num19 = Vm * num12;
		float num20 = num15;
		float num21 = Vm * num13;
		TVector tVector = s;
		float num22 = Mathf.Exp((0f - Cd * time) / m);
		float num23 = m / Cd;
		float num24 = Cw * Vw / Cd;
		s.i = num23 * num22 * ((0f - num24) * num6 - num17) - num24 * num6 * time - num23 * ((0f - num24) * num6 - num17) + num16;
		s.j = num18 + ((0f - (num19 + num23 * g)) * num23 * num22 - num23 * g * time) + num23 * (num19 + num23 * g);
		s.k = num23 * num22 * ((0f - num24) * num5 - num21) - num24 * num5 * time - num23 * ((0f - num24) * num5 - num21) + num20;
		v.i = (s.i - tVector.i) / tInc;
		v.j = (s.j - tVector.j) / tInc;
		v.k = (s.k - tVector.k) / tInc;
		if (s.j <= 0f)
		{
			return 2;
		}
		if (time > 30f)
		{
			return 3;
		}
		return 0;
	}

	public override void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawRay(m_car.GetPosition(), GetVelocity());
	}

	public bool IsVelocityUp()
	{
		if (v.j >= -0.05f && m_bStart)
		{
			return true;
		}
		return false;
	}

	public float GetS()
	{
		return s.j;
	}
}
