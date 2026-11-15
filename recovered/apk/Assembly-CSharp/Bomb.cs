using RaceTrack;
using UnityEngine;

public class Bomb : Prop
{
	private bool m_Throwing;

	private float m_ExplodedTime = 3f;

	private Vector3 m_ThrowDir;

	private float m_ThrowSpeed = 250f;

	private bool m_Explode;

	public override bool IsPickable
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public override bool IsDangerous
	{
		get
		{
			return true;
		}
		set
		{
		}
	}

	public override bool IsMoving
	{
		get
		{
			return m_Throwing;
		}
		set
		{
		}
	}

	private void Start()
	{
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
		if (m_Throwing)
		{
			if (!CheckTouchGround())
			{
				if (CheckCollide(base.transform.position, m_ThrowDir, 2f, out var rebounceDir))
				{
					m_ThrowDir = rebounceDir;
				}
				float num = 0f;
				while (num < Time.deltaTime)
				{
					float num2 = ((!(DELTA_TIME < Time.deltaTime - num)) ? (Time.deltaTime - num) : DELTA_TIME);
					num += num2;
					m_ThrowDir.x -= m_ThrowDir.x * num2 * 2.5f;
					m_ThrowDir.z -= m_ThrowDir.z * num2 * 2.5f;
					m_ThrowDir.y -= Mathf.Abs(m_ThrowDir.y) * num2 * 25f;
					m_ThrowDir.y += -9.8f * num2;
					base.gameObject.transform.position += m_ThrowDir * num2;
				}
				return;
			}
			m_Throwing = false;
			Animation componentInChildren = base.transform.GetComponentInChildren<Animation>();
			if (null != componentInChildren)
			{
				componentInChildren.Play();
			}
			if (base.transform.position.y < HeightMap.m_InvalideHeight)
			{
				m_HitTarget = true;
			}
		}
		if (CheckCollidWithMonster(3f) && !m_Explode)
		{
			m_Explode = true;
			Explode();
		}
		m_ExplodedTime -= Time.deltaTime;
		if ((m_HitTarget || m_ExplodedTime <= 0f) && !m_Explode)
		{
			m_Explode = true;
			Explode();
		}
	}

	public void Explode()
	{
		RaceWorld.Instance.RemoveProp(base.name);
		TSingleton<PropManager>.Instance.UseProp(null, ePropType.BOMB_EFFECT, out var kcomponent);
		if (null != kcomponent)
		{
			(kcomponent as BombEffect).DropAtPosition(base.transform.position);
		}
		TSingleton<PropManager>.Instance.Release(ePropType.BOMB, this);
		SoundManager.Instance.RemoveAudio(m_Audio);
		Object.Destroy(base.gameObject);
	}

	public void ThrowFromPos(Vector3 vPos, Vector3 vDir, Vector3 right, float fRelationspeed, bool forward)
	{
		base.transform.localPosition = vPos;
		m_ThrowDir = vDir;
		m_ThrowDir.y = 0.8f;
		m_ThrowDir.Normalize();
		if (forward)
		{
			m_ThrowDir *= m_ThrowSpeed + fRelationspeed * 5f;
		}
		else
		{
			m_ThrowDir *= 20f;
		}
		m_InitRight = right;
		m_Throwing = true;
		base.transform.forward = vDir;
		RaceWorld.Instance.AddProp(base.name, this);
	}

	protected bool CheckTouchGround()
	{
		float num = HeightMapManager.Instance.Height(base.transform.position.x, base.transform.position.z);
		if (base.transform.position.y - num > 0.2f)
		{
			if (m_DeltaRight < 0.7f)
			{
				m_DeltaRight += 0.05f;
				base.transform.position -= 0.05f * m_InitRight;
			}
			return false;
		}
		base.transform.position = new Vector3(base.transform.position.x, num + 0.5f, base.transform.position.z);
		return true;
	}

	protected override void OnTriggerEnter(Collider other)
	{
		Car car = null;
		if (null != other.transform.parent && null != other.transform.parent.parent && null != (car = other.transform.parent.parent.gameObject.GetComponent<Car>()) && !m_Throwing)
		{
			car.HitByBomb();
			if (!m_Explode)
			{
				m_Explode = true;
				Explode();
			}
		}
	}
}
