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

	private bool bAdjustOnce;

	private float fAngularVel;

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
	}

	private void InitializeVariables()
	{
	}

	public void SetVariables(JumpPlatformData data, bool isAccState)
	{
	}

	public Vector3 GetVelocity()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool IsStarted()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void BeginSimulator()
	{
	}

	public void BeginSimulator(Vector3 speed)
	{
	}

	public void StopSimulator()
	{
	}

	public override void Update(float fTickTime)
	{
	}

	private int DoSimulation()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void OnDrawGizmos()
	{
	}

	public bool IsVelocityUp()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetS()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
