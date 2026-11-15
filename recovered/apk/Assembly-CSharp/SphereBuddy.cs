using UnityEngine;

public class SphereBuddy
{
	public Rigidbody m_kRigidbody;

	private SphereCollider m_collider;

	private Transform m_Node;

	private static int iIdx;

	private bool bEnable = true;

	public Vector3 m_vLastForce = Vector3.zero;

	public SphereBuddy(Vector3 vPos, float fRadius)
	{
		m_Node = new GameObject().transform;
		string name = "SphereBuddy_" + iIdx++;
		m_Node.name = name;
		m_collider = m_Node.gameObject.AddComponent<SphereCollider>();
		m_kRigidbody = m_Node.gameObject.AddComponent<Rigidbody>();
		m_kRigidbody.isKinematic = true;
		m_kRigidbody.useGravity = false;
		m_collider.name = name;
		m_collider.radius = fRadius;
		m_collider.center = Vector3.zero;
		m_Node.gameObject.layer = 13;
		SetPosition(vPos);
	}

	public void SetPosition(Vector3 vWorldPos)
	{
		if (bEnable && CollisionLayer.Instance.VerticalIntersectHitPosition(vWorldPos, out var _, out var vHitPoint))
		{
			vWorldPos = vHitPoint;
			vWorldPos.y += m_collider.radius;
		}
		m_Node.position = vWorldPos;
	}

	public Vector3 GetPosition()
	{
		return m_Node.transform.position;
	}

	public void SetRotation(Quaternion rot)
	{
		m_Node.rotation = rot;
	}

	public void Disable()
	{
		m_Node.gameObject.active = false;
		bEnable = false;
		if (!m_kRigidbody.isKinematic)
		{
			m_kRigidbody.velocity = Vector3.zero;
		}
		m_kRigidbody.isKinematic = true;
	}

	public void Enable()
	{
		m_Node.gameObject.active = true;
		bEnable = true;
		m_kRigidbody.isKinematic = false;
	}

	public void AddForce(Vector3 vForce)
	{
		if (!m_kRigidbody.isKinematic)
		{
			m_vLastForce = vForce;
			m_kRigidbody.AddForce(vForce);
		}
		else
		{
			m_vLastForce = Vector3.zero;
		}
	}
}
