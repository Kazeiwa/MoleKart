using RaceTrack;
using UnityEngine;

public class TortoiseBlue : Tortoise
{
	private Car m_LockTarget;

	private bool m_CreatePath;

	private float m_FollowFlyingTime;

	private float m_InreamentHeight = 2f;

	private bool m_FollowOver;

	private void Start()
	{
		m_FlyingSpeed = 80f;
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
			m_TrailRender.enabled = true;
		}
		PlaySound(51601, 1);
		InitDistanceToCamera();
	}

	private void Update()
	{
		UpdateDistanceToCamera();
		UpdateVisible();
		if (m_Throwing && !m_CreatePath)
		{
			m_CurrentWaypoint = FindNearByWaypoint();
			CreatePath();
			m_CreatePath = true;
		}
		if (m_CreatePath)
		{
			Flying();
		}
		m_CreatedTime += Time.deltaTime;
		if (m_CreatedTime > TORTOISE_LIFE_TIME)
		{
			RaceWorld.Instance.RemoveProp(base.name);
			TSingleton<PropManager>.Instance.Release(ePropType.TORTOISE, this);
			SoundManager.Instance.RemoveAudio(m_Audio);
			Object.Destroy(base.gameObject);
		}
	}

	private void Flying()
	{
		if (null == m_LockTarget)
		{
			Car car = TSingleton<CarManager>.Instance.FindFirstCar();
			if (null != car && (base.transform.position - car.transform.position).magnitude <= 25f)
			{
				m_LockTarget = car;
			}
		}
		if (null != m_LockTarget)
		{
			base.transform.position = m_LockTarget.transform.position;
			base.transform.forward = m_LockTarget.transform.forward;
			m_FollowFlyingTime += Time.deltaTime;
			if (null != m_TrailRender && m_TrailRender.enabled)
			{
				m_TrailRender.enabled = false;
			}
		}
		else
		{
			FollowWaypoint();
		}
		if (m_InreamentHeight < 4.5f)
		{
			m_InreamentHeight += 0.2f;
		}
		if (m_FollowFlyingTime >= 2f && !m_FollowOver)
		{
			m_InreamentHeight += 0.2f;
			if (m_InreamentHeight >= 7f)
			{
				m_FollowOver = true;
			}
		}
		if (m_FollowOver && !m_LockTarget.CarBounceOver && !m_LockTarget.CarMushroomBounce && m_LockTarget.vehicle.RunningMode != RUNNING_MDOE.RM_FLYING)
		{
			m_InreamentHeight -= 1.5f;
		}
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + m_InreamentHeight, base.transform.position.z);
		if (m_FollowOver && (base.transform.position - m_LockTarget.transform.position).magnitude < 3f)
		{
			Explode();
		}
	}

	private void Explode()
	{
		RaceWorld.Instance.RemoveProp(base.name);
		m_HitTarget = true;
		TSingleton<PropManager>.Instance.UseProp(null, ePropType.BLUE_TORTOISE_BOMB_EFFECT, out var kcomponent);
		if (null != kcomponent)
		{
			(kcomponent as BombEffect).DropAtPosition(m_LockTarget.transform.position);
		}
		TSingleton<PropManager>.Instance.Release(ePropType.TORTOISE, this);
		SoundManager.Instance.RemoveAudio(m_Audio);
		Object.Destroy(base.gameObject);
	}

	protected override void OnTriggerEnter(Collider other)
	{
	}
}
