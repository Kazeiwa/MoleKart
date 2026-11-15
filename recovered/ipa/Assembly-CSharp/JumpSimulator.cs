using UnityEngine;

public class JumpSimulator : ISimulator
{
	private Vehicle m_car;

	private bool m_bActive;

	private Vector3 m_vPrePosition;

	private Vector3 m_vVelocity;

	private Vector3 m_vStepPos;

	private float m_fStepLen;

	public bool IsActive
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public JumpSimulator(Vehicle car)
	{
	}

	public bool CanAdjustPicth()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void BeginJump(Vector3 vVelocity, int iWheelInBump)
	{
	}

	public void EndJump()
	{
	}

	private bool CheckSideStep(Vector3 v0, Vector3 v1, Vector3 v2)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void GetStepLengthAndCenter(out float fStepLen, out Vector3 vStepPos)
	{
	}

	public override void Update(float fTickTime)
	{
	}

	public override void OnDrawGizmos()
	{
	}
}
