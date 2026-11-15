using RaceTrack;
using UnityEngine;

public class TortoiseRed : Tortoise
{
	private float m_SearchCarRadius = 15f;

	private bool m_EffectPlayed;

	protected GameObject m_TraceTarget;

	protected float m_UpdateTargetTime;

	private void Start()
	{
		m_FlyingSpeed = 40f;
		m_Type = ePropType.TORTOISE_RED;
		Renderer componentInChildren = base.gameObject.GetComponentInChildren<Renderer>();
		if (null != componentInChildren)
		{
			componentInChildren.enabled = true;
			componentInChildren.castShadows = false;
			componentInChildren.receiveShadows = false;
		}
		m_Render = base.transform.GetComponentsInChildren<Renderer>();
		m_TrailRender = base.transform.GetComponentInChildren<TrailRenderer>();
		if (null != m_TrailRender)
		{
			m_TrailRender.enabled = false;
		}
		InitDistanceToCamera();
	}

	private void Update()
	{
		UpdateDistanceToCamera();
		UpdateVisible();
		if (m_HitTarget)
		{
			if (!m_EffectPlayed)
			{
				if (null != m_Audio)
				{
					m_Audio.Stop();
					SoundManager.Instance.StopAudio(m_Audio);
				}
				PlaySound(51204, 1);
				SetVisible(isActive: false);
				TSingleton<PropManager>.Instance.Release(ePropType.TORTOISE, this);
				PlayExplodeEft(0.5f, eEffectType.RED_TORTOISE_EXPLODE);
				StopSurroundSound();
				m_EffectPlayed = true;
			}
			PrepareDeleteObject(0.6f);
		}
		else
		{
			if (m_Throwing && ThrowingToGround())
			{
				return;
			}
			if (null != m_SurroundCar)
			{
				RotateSurroundCar();
				if (null != m_TrailRender)
				{
					m_TrailRender.enabled = false;
				}
			}
			else
			{
				if (!m_Droped)
				{
					Flying();
				}
				CheckCollideWithOtherProps(1f);
				m_CreatedTime += Time.deltaTime;
				if (m_CreatedTime > TORTOISE_LIFE_TIME)
				{
					RaceWorld.Instance.RemoveProp(base.name);
					TSingleton<PropManager>.Instance.Release(ePropType.TORTOISE, this);
					SoundManager.Instance.RemoveAudio(m_Audio);
					Object.Destroy(base.gameObject);
				}
			}
			CheckCollidWithMonster(2f);
		}
	}

	private bool ThrowingToGround()
	{
		m_HitTarget = CheckCollide(base.transform.position, base.transform.forward, 1.5f, out var _);
		if (!CheckTouchGround())
		{
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
			return true;
		}
		m_Throwing = false;
		base.transform.forward = m_InitForward;
		if (m_Forward)
		{
			CreatePath();
		}
		if (null != m_TrailRender)
		{
			m_TrailRender.enabled = true;
		}
		base.gameObject.transform.localScale *= 2f;
		if (!m_Droped)
		{
			Animation componentInChildren = base.transform.GetComponentInChildren<Animation>();
			if (null != componentInChildren)
			{
				componentInChildren.Play();
			}
		}
		if (base.transform.position.y < HeightMap.m_InvalideHeight)
		{
			m_HitTarget = true;
		}
		return false;
	}

	private void Flying()
	{
		if (HeightMapManager.Instance.Height(base.transform.position.x, base.transform.position.z) < HeightMap.m_InvalideHeight)
		{
			m_HitTarget = true;
			return;
		}
		if (m_Forward)
		{
			FlyingForward();
			return;
		}
		base.transform.position += base.transform.forward * DELTA_TIME * m_FlyingSpeed;
		base.transform.position = new Vector3(base.transform.position.x, HeightMapManager.Instance.Height(base.transform.position.x, base.transform.position.z) + 0.5f, base.transform.position.z);
	}

	protected void FlyingForward()
	{
		m_UpdateTargetTime += Time.deltaTime;
		if (m_UpdateTargetTime >= 0.05f)
		{
			m_TraceTarget = TSingleton<CarManager>.Instance.FindNearestCar(m_OwnerCar, base.gameObject, m_SearchCarRadius);
			m_UpdateTargetTime = 0f;
		}
		if (null != m_TraceTarget)
		{
			base.transform.forward = m_TraceTarget.transform.position - base.transform.position;
			base.transform.position += base.transform.forward * Time.deltaTime * m_FlyingSpeed * 3f;
		}
		else
		{
			FollowWaypoint();
		}
	}

	protected override void OnTriggerEnter(Collider other)
	{
		Car car = null;
		if (null != other.transform.parent && null != other.transform.parent.parent && null != (car = other.transform.parent.parent.gameObject.GetComponent<Car>()) && car != m_SurroundCar && !m_HitTarget && !m_Throwing)
		{
			if (!m_Defended)
			{
				car.HitByTortoise();
			}
			m_HitTarget = true;
			SendMessageHandler.Instance.BroadPropDestroy(ePropType.TORTOISE_RED, base.name);
		}
	}
}
