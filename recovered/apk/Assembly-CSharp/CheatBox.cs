using RaceTrack;
using UnityEngine;

public class CheatBox : Prop
{
	private float m_EftTime;

	private bool m_Throwing;

	private Vector3 m_ThrowDir = Vector3.one;

	private float m_ThrowSpeed = 180f;

	private bool m_EftPlayed;

	private Transform m_Exp1;

	private Transform m_Pub26;

	private float m_FrameTime;

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
		base.gameObject.GetComponentInChildren<Renderer>().enabled = true;
		base.gameObject.GetComponentInChildren<Renderer>().castShadows = false;
		base.gameObject.GetComponentInChildren<Renderer>().receiveShadows = false;
		m_Pub26 = base.transform.FindChild("puc_26");
		m_Exp1 = base.transform.FindChild("expl_01");
		if (null != m_Exp1)
		{
			m_Exp1.gameObject.SetActiveRecursively(state: false);
		}
		InitDistanceToCamera();
	}

	private void Update()
	{
		UpdateDistanceToCamera();
		UpdateVisible();
		if (m_HitTarget)
		{
			PrepareDeleteObject(1.5f);
			return;
		}
		if (m_Throwing)
		{
			if (!CheckTouchGround())
			{
				if (CheckCollide(base.transform.position, m_ThrowDir, 3f, out var rebounceDir))
				{
					m_ThrowDir = rebounceDir;
				}
				else if (CheckCollide(base.transform.position, base.transform.right, 0.35f, out rebounceDir))
				{
					m_ThrowDir = rebounceDir * 2f;
				}
				else if (CheckCollide(base.transform.position, -base.transform.right, 0.35f, out rebounceDir))
				{
					m_ThrowDir = rebounceDir * 2f;
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
			if (base.transform.position.y < HeightMap.m_InvalideHeight)
			{
				m_HitTarget = true;
			}
		}
		CheckCollidWithMonster(5f);
		CheckCollideWithOtherProps(3f);
		m_FrameTime += Time.deltaTime;
		m_CreatedTime += Time.deltaTime;
		if (m_CreatedTime > 60f)
		{
			RaceWorld.Instance.RemoveProp(base.name);
			SoundManager.Instance.RemoveAudio(m_Audio);
			Object.Destroy(base.gameObject);
		}
	}

	private void PlayExplodeEft()
	{
		if (!m_bIsDistanceShow)
		{
			return;
		}
		if (null == m_Pub26)
		{
			m_Pub26 = base.gameObject.transform.FindChild("puc_26");
		}
		if (null != m_Pub26)
		{
			m_Pub26.gameObject.SetActiveRecursively(state: false);
		}
		if (null == m_Exp1)
		{
			m_Exp1 = base.transform.FindChild("expl_01");
		}
		if (null != m_Exp1)
		{
			TurnOn component = m_Exp1.GetComponent<TurnOn>();
			if (null != component)
			{
				component.TurnOnEffect();
			}
		}
	}

	private bool CheckTouchGround()
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
		base.transform.position = new Vector3(base.transform.position.x, num + 1.5f, base.transform.position.z);
		return true;
	}

	protected void PrepareDeleteObject(float time)
	{
		if (!m_EftPlayed)
		{
			RaceWorld.Instance.RemoveProp(base.name);
			TSingleton<PropManager>.Instance.Release(ePropType.CHEAT_BOX, this);
			if (null == m_Exp1)
			{
				m_Exp1 = base.transform.FindChild("expl_01");
			}
			if (null != m_Exp1)
			{
				m_Exp1.gameObject.SetActiveRecursively(state: true);
			}
			m_EftPlayed = true;
			PlayExplodeEft();
			PlaySound(52601, 1);
		}
		m_EftTime += Time.deltaTime;
		if (m_EftTime >= time)
		{
			if (null != m_Audio)
			{
				m_Audio.Stop();
				SoundManager.Instance.RemoveAudio(m_Audio);
			}
			Object.Destroy(base.gameObject);
		}
	}

	public void ThrowFromPos(Vector3 vPos, Vector3 vDir, Vector3 right, float fRelationspeed, bool forward)
	{
		m_ThrowDir = vDir;
		base.transform.position = vPos;
		m_ThrowDir.y = 1.5f;
		m_ThrowDir.Normalize();
		if (forward)
		{
			m_ThrowDir *= m_ThrowSpeed + fRelationspeed * 10f;
		}
		else
		{
			m_ThrowDir *= 20f;
		}
		m_InitRight = right;
		m_Throwing = true;
		if (null != base.animation && null != base.animation.GetClip("toss_ani"))
		{
			base.animation.Play("toss_ani");
		}
		RaceWorld.Instance.AddProp(base.name, this);
	}

	public void DropAtPos(Vector3 pos, Vector3 dir, float speed)
	{
		m_ThrowDir = dir;
		base.transform.position = pos;
		m_ThrowDir.y = 1.5f;
		m_ThrowDir = m_ThrowDir.normalized * speed;
		m_Throwing = true;
		RaceWorld.Instance.AddProp(base.name, this);
	}

	protected override void OnTriggerEnter(Collider other)
	{
		Car car = null;
		if (null != other.transform.parent && null != other.transform.parent.parent && null != (car = other.transform.parent.parent.gameObject.GetComponent<Car>()) && !m_HitTarget && !m_Throwing)
		{
			m_HitTarget = true;
			car.HitByCheatBox();
			SendMessageHandler.Instance.BroadPropDestroy(ePropType.CHEAT_BOX, base.name);
		}
	}
}
