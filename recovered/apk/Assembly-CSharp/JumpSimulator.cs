using UnityEngine;

public class JumpSimulator : ISimulator
{
	private Vehicle m_car;

	private bool m_bActive;

	private Vector3 m_vPrePosition;

	private Vector3 m_vVelocity;

	private Vector3 m_vStepPos;

	private float m_fStepLen;

	public bool IsActive => m_bActive;

	public JumpSimulator(Vehicle car)
	{
		m_car = car;
	}

	public bool CanAdjustPicth()
	{
		return true;
	}

	public void BeginJump(Vector3 vVelocity, int iWheelInBump)
	{
		if (!m_bActive)
		{
			m_bActive = true;
			m_vStepPos = m_car.GetPosition();
			m_vPrePosition = m_vStepPos;
			m_fRotX = m_car.GetEulerAngleX();
			m_fRotY = m_car.GetEulerAngleY();
			m_fRotZ = m_car.GetEulerAngleZ();
			m_vVelocity = vVelocity;
			m_vVelocity.Normalize();
		}
	}

	public void EndJump()
	{
		m_bActive = false;
	}

	private bool CheckSideStep(Vector3 v0, Vector3 v1, Vector3 v2)
	{
		float num = Vector3.Dot(Vector3.Cross(v0, v1), Vector3.Cross(v1, v2));
		return num < 0f;
	}

	private void GetStepLengthAndCenter(out float fStepLen, out Vector3 vStepPos)
	{
		fStepLen = 0f;
		vStepPos = m_vPrePosition;
		Vector3[] array = new Vector3[5]
		{
			m_vPrePosition - m_vVelocity * 0.5f,
			m_vPrePosition,
			m_vPrePosition + m_vVelocity * 0.5f,
			m_vPrePosition + m_vVelocity * 1f,
			m_vPrePosition + m_vVelocity * 1.5f
		};
		Vector3[] array2 = new Vector3[5]
		{
			HeightMapManager.Instance.Normal(array[0].x, array[0].y, array[0].z),
			HeightMapManager.Instance.Normal(array[1].x, array[1].y, array[1].z),
			HeightMapManager.Instance.Normal(array[2].x, array[2].y, array[2].z),
			HeightMapManager.Instance.Normal(array[3].x, array[3].y, array[3].z),
			HeightMapManager.Instance.Normal(array[4].x, array[4].y, array[4].z)
		};
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < 3; i++)
		{
			if (CheckSideStep(array2[i], array2[i + 1], array2[i + 2]))
			{
				if (num == -1)
				{
					num = i + 1;
				}
				num2 = i + 1;
			}
		}
		if (num != -1)
		{
			vStepPos = (array[num2] + array[num]) / 2f;
			fStepLen = (array[num2] - array[num]).magnitude / 2f;
		}
	}

	public override void Update(float fTickTime)
	{
		if (m_bActive)
		{
			Vector3 position = m_car.GetPosition();
			if ((position - m_vStepPos).magnitude < 2f && m_car.CollisionSide == VehicleCollisionSide.CS_NONE)
			{
				m_car.LockRotation(bLockX: true, bLockY: false, bLockZ: true);
				return;
			}
			m_car.LockRotation(bLockX: false, bLockY: false, bLockZ: false);
			EndJump();
		}
	}

	public override void OnDrawGizmos()
	{
	}
}
