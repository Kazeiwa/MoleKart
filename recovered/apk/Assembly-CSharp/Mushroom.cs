using RaceTrack;
using UnityEngine;

public class Mushroom : Prop
{
	private Vector3 m_ThrowDir = Vector3.one;

	private bool m_Throwing;

	public override bool IsPickable
	{
		get
		{
			return true;
		}
		set
		{
		}
	}

	public override bool IsDangerous
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public override bool IsMoving
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

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
		InitDistanceToCamera();
	}

	private void Update()
	{
		UpdateDistanceToCamera();
		UpdateVisible();
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
				base.transform.localScale *= 2f;
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
		RaceWorld.Instance.RemoveProp(base.name);
		SoundManager.Instance.RemoveAudio(m_Audio);
		Object.Destroy(base.gameObject);
	}

	public void DropAtPos(Vector3 pos, Vector3 dir, float speed)
	{
		base.transform.position = pos;
		m_ThrowDir = dir;
		m_ThrowDir.y = 1.5f;
		m_ThrowDir = m_ThrowDir.normalized * speed;
		m_Throwing = true;
		RaceWorld.Instance.AddProp(base.name, this);
	}

	protected override void OnTriggerEnter(Collider other)
	{
		Car car = null;
		if (null != other.transform.parent && null != other.transform.parent.parent && null != (car = other.transform.parent.parent.gameObject.GetComponent<Car>()) && !m_HitTarget)
		{
			car.AccelerateCar(2f);
			m_HitTarget = true;
		}
	}
}
