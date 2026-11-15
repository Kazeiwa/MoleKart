using UnityEngine;

public class LuckyStar : Prop
{
	private Vector3 m_ThrowDir = Vector3.one;

	private bool m_Throwing;

	private void Start()
	{
		m_HitTarget = false;
		Renderer componentInChildren = base.gameObject.GetComponentInChildren<Renderer>();
		if (null != componentInChildren)
		{
			componentInChildren.enabled = true;
			componentInChildren.castShadows = false;
			componentInChildren.receiveShadows = false;
		}
	}

	private void Update()
	{
		if (m_HitTarget)
		{
			PrepareDeleteObject();
		}
		else
		{
			if (!m_Throwing)
			{
				return;
			}
			if (!CheckTouchGround())
			{
				if (CheckCollide(base.transform.position, m_ThrowDir, 2f, out var rebounceDir))
				{
					m_ThrowDir = rebounceDir;
				}
				m_ThrowDir.x -= m_ThrowDir.x * DELTA_TIME * 0.5f;
				m_ThrowDir.z -= m_ThrowDir.z * DELTA_TIME * 0.5f;
				m_ThrowDir.y -= Mathf.Abs(m_ThrowDir.y) * DELTA_TIME * 25f;
				m_ThrowDir.y += -9.8f * DELTA_TIME;
				base.gameObject.transform.position += m_ThrowDir * DELTA_TIME;
			}
			else
			{
				m_Throwing = false;
				if (base.transform.position.y < HeightMap.m_InvalideHeight)
				{
					m_HitTarget = true;
				}
			}
		}
	}

	private bool CheckTouchGround()
	{
		float num = HeightMapManager.Instance.Height(base.transform.position.x, base.transform.position.z);
		if (base.transform.position.y - num > 0.2f)
		{
			return false;
		}
		base.transform.position = new Vector3(base.transform.position.x, num, base.transform.position.z);
		return true;
	}

	protected void PrepareDeleteObject()
	{
		Object.Destroy(base.gameObject);
	}

	public void DropAtPos(Vector3 pos, Vector3 dir, float speed)
	{
		base.transform.position = pos;
		m_ThrowDir = dir;
		m_ThrowDir.y = 1.5f;
		m_ThrowDir = m_ThrowDir.normalized * speed;
		m_Throwing = true;
	}

	protected override void OnTriggerEnter(Collider other)
	{
		Car car = null;
		if (null != other.transform.parent && null != other.transform.parent.parent && null != (car = other.transform.parent.parent.gameObject.GetComponent<Car>()) && !m_HitTarget)
		{
			car.PlaySound(40002, 1);
			car.InvincibleCar();
			m_HitTarget = true;
		}
	}
}
