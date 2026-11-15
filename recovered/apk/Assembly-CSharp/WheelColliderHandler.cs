using UnityEngine;

public class WheelColliderHandler : MonoBehaviour
{
	private WheelCollider[] m_kWheels = new WheelCollider[4];

	private Collider[] m_kOther = new Collider[4];

	private float[] m_kIgnoreTime = new float[4];

	public void Init(WheelCollider[] wheels)
	{
		m_kWheels[0] = wheels[0];
		m_kWheels[1] = wheels[1];
		m_kWheels[2] = wheels[2];
		m_kWheels[3] = wheels[3];
		m_kIgnoreTime[0] = (m_kIgnoreTime[1] = (m_kIgnoreTime[2] = (m_kIgnoreTime[3] = 0f)));
		m_kOther[0] = (m_kOther[1] = (m_kOther[2] = (m_kOther[3] = null)));
	}

	public void Init(WheelCollider w1, WheelCollider w2, WheelCollider w3, WheelCollider w4)
	{
		m_kWheels[0] = w1;
		m_kWheels[1] = w2;
		m_kWheels[2] = w3;
		m_kWheels[3] = w4;
		m_kIgnoreTime[0] = (m_kIgnoreTime[1] = (m_kIgnoreTime[2] = (m_kIgnoreTime[3] = 0f)));
		m_kOther[0] = (m_kOther[1] = (m_kOther[2] = (m_kOther[3] = null)));
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.contacts.GetLength(0) > 0)
		{
			ContactPoint contactPoint = collision.contacts[0];
			if (!(contactPoint.thisCollider is WheelCollider))
			{
			}
		}
	}

	public void OnCollisionStay(Collision collision)
	{
		if (collision.contacts.GetLength(0) <= 0)
		{
			return;
		}
		ContactPoint contactPoint = collision.contacts[0];
		if (!(contactPoint.thisCollider is WheelCollider))
		{
			return;
		}
		WheelCollider wheelCollider = contactPoint.thisCollider as WheelCollider;
		Vector3 position = wheelCollider.transform.position;
		Vector3 lhs = wheelCollider.transform.TransformDirection(Vector3.up);
		Vector3 normal = contactPoint.normal;
		float num = Vector3.Dot(lhs, normal);
		wheelCollider.transform.TransformDirection(Vector3.up);
		if (!(num < 0.7f))
		{
			return;
		}
		for (int i = 0; i < 4; i++)
		{
			if (m_kWheels[i] == wheelCollider)
			{
				m_kIgnoreTime[i] = 0.2f;
				if (m_kOther[i] != null && contactPoint.otherCollider != m_kOther[i])
				{
					Physics.IgnoreCollision(wheelCollider, m_kOther[i], ignore: false);
				}
				m_kOther[i] = contactPoint.otherCollider;
				Physics.IgnoreCollision(wheelCollider, contactPoint.otherCollider);
				break;
			}
		}
	}

	public void Update()
	{
		for (int i = 0; i < 4; i++)
		{
			if (!(m_kIgnoreTime[i] > 0f))
			{
				continue;
			}
			m_kIgnoreTime[i] -= Time.deltaTime;
			if (m_kIgnoreTime[i] <= 0f)
			{
				m_kIgnoreTime[i] = 0f;
				if (m_kOther[i] != null)
				{
					Physics.IgnoreCollision(m_kWheels[i], m_kOther[i], ignore: false);
				}
			}
		}
	}
}
