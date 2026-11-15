using UnityEngine;

public class FlySimulatorComponent
{
	protected bool m_bEject;

	protected float m_fFlyingTime;

	protected Collider m_curPlatformCollider;

	protected FlySimulator m_Flysimulator;

	protected Vector3 ejectSpeed;

	protected Collider cHead;

	protected Collider cTail;

	protected Collider cBody;

	private btVehicleBase vehicle;

	public bool IsEject
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private void Start()
	{
	}

	public Vector3 GetVelocity()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetFlySimulatorVariables(JumpPlatformData data, bool isAccelerate)
	{
	}

	public void InitComponent(btVehicleBase v)
	{
	}

	public void EnableWallCollider(bool enable)
	{
	}

	public void BeginEject(Collider col)
	{
	}

	public void UpdateEject(float fTickTime, ENMMOTIONDIR dir)
	{
	}

	private void Update()
	{
	}

	public void EndEject()
	{
	}
}
