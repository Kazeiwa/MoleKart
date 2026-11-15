using UnityEngine;

public class WheelColliderHandler : MonoBehaviour
{
	private WheelCollider[] m_kWheels;

	private Collider[] m_kOther;

	private float[] m_kIgnoreTime;

	public void Init(WheelCollider[] wheels)
	{
	}

	public void Init(WheelCollider w1, WheelCollider w2, WheelCollider w3, WheelCollider w4)
	{
	}

	public void OnCollisionEnter(Collision collision)
	{
	}

	public void OnCollisionStay(Collision collision)
	{
	}

	public void Update()
	{
	}
}
