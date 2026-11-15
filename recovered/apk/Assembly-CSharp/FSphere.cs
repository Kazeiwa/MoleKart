using RaceTrack;
using UnityEngine;

public class FSphere : Prop
{
	private bool m_Throwing;

	private float m_ExplodedTime = 3f;

	private Vector3 m_ThrowDir;

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
	}

	private void Update()
	{
		if (m_Throwing)
		{
			if (!CheckTouchGround())
			{
				float num = 0f;
				while (num < Time.deltaTime)
				{
					float num2 = ((!(DELTA_TIME < Time.deltaTime - num)) ? (Time.deltaTime - num) : DELTA_TIME);
					num += num2;
					base.transform.position += m_ThrowDir * num2;
				}
				return;
			}
			m_Throwing = false;
			GameObject gameObject = base.transform.FindChild("Prop_Sphere/sphere_02").gameObject;
			if (null != gameObject)
			{
				Animation animation = gameObject.animation;
				if (null != animation && null != animation.GetClip("A_pterosaur_rock_01"))
				{
					animation.Play("A_pterosaur_rock_01");
				}
			}
			CreateHitEft();
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

	private void CreateHitEft()
	{
		GameObject gameObject = EffectManager.Instance.CreateEffect(eEffectType.SPHEREHITGROUND, isPause: true, isOneShot: false);
		Effect component = gameObject.GetComponent<Effect>();
		component.AttachTo(base.transform, new Vector3(0f, -1.2f, 0f), new Vector3(0f, 0f, 0f));
		component.SetLifeTime(2f);
		component.doReset();
	}

	public void Explode()
	{
		RaceWorld.Instance.RemoveProp(base.name);
		TSingleton<PropManager>.Instance.UseProp(null, ePropType.BOMB_EFFECT, out var kcomponent);
		if (null != kcomponent)
		{
			(kcomponent as BombEffect).DropAtPosition(base.transform.position);
		}
		TSingleton<PropManager>.Instance.Release(ePropType.SPHERE, this);
		SoundManager.Instance.RemoveAudio(m_Audio);
		Object.Destroy(base.gameObject);
	}

	public void ThrowFromPos(Vector3 fromPos, Vector3 destPos, float speed, float time)
	{
		m_ExplodedTime = time;
		base.transform.position = fromPos;
		m_ThrowDir = (destPos - fromPos).normalized;
		base.transform.forward = m_ThrowDir;
		m_ThrowDir *= speed;
		m_Throwing = true;
		PlaySound(20401, 1);
		RaceWorld.Instance.AddProp(base.name, this);
	}

	protected bool CheckTouchGround()
	{
		float num = HeightMapManager.Instance.Height(base.transform.position.x, base.transform.position.z);
		if (base.transform.position.y - num > 0.2f)
		{
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
