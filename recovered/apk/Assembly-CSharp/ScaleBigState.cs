using RaceTrack;
using UnityEngine;

public class ScaleBigState : FState
{
	private float m_ContinueTime;

	public ScaleBigState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToScaleBigger;
		m_LowPriority.Add(EventType.ToScaleFlat);
		m_LowPriority.Add(EventType.ToScaleSmaller);
		m_LowPriority.Add(EventType.ToInvincible);
		m_HighPriority.Add(EventType.ToDead);
		m_HighPriority.Add(EventType.ToLeftBound);
		m_HighPriority.Add(EventType.ToRightBound);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_StateFlag = StateFlag.UPDATING;
			m_ContinueTime = 0f;
			if (m_ForceEnter)
			{
				m_ForceEnter = false;
			}
			m_PlayerCar.Play("mushroom01");
			m_PlayerCar.CarState |= 128u;
			m_PlayerCar.PlayScaleBigCarEft();
			m_PlayerCar.LimitSpeed(m_PlayerCar.FastSpeed);
			m_PlayerCar.EnableGroundFriction(bEnable: false);
			bool flag = !m_PlayerCar.vehicle.IsKinematic();
			if (flag)
			{
				m_PlayerCar.EnableKinematic();
			}
			m_PlayerCar.SetDynamicScaleFactor(2f);
			if (flag)
			{
				m_PlayerCar.DisableKinematic();
			}
			m_PlayerCar.SetMaxColliderMass(bMax: true);
			if (m_PlayerCar.isHero)
			{
				SoundManager.Instance.PauseBackgroundMusic();
				SoundManager.Instance.PlaySound(52702);
			}
			m_PlayerCar.m_CarRadius = 3f;
			m_PlayerCar.m_FollowDis = 2.5f;
			m_PlayerCar.SetCarTorqueScale(1);
			m_PlayerCar.IgnoreNPC(bIgnore: true);
		}
	}

	public override void Update()
	{
		m_ContinueTime += Time.deltaTime;
		if (m_ContinueTime >= 13.3f)
		{
			FEvent evt = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
		}
		foreach (IMonster monster in RaceWorld.Instance.Monsters)
		{
			float num = ((!(monster.Radius > 0f)) ? m_PlayerCar.Radius : (monster.Radius + m_PlayerCar.Radius));
			num += 1f;
			if ((m_PlayerCar.transform.position - monster.Position).magnitude < num)
			{
				monster.KnockedOut = true;
			}
		}
	}

	public override void Exited()
	{
		m_PlayerCar.Play("mushroom02");
		m_PlayerCar.LimitSpeed(m_PlayerCar.MaxSpeed);
		m_PlayerCar.EnableGroundFriction(bEnable: true);
		bool flag = !m_PlayerCar.vehicle.IsKinematic();
		if (flag)
		{
			m_PlayerCar.EnableKinematic();
		}
		m_PlayerCar.SetDynamicScaleFactor(1f);
		if (flag)
		{
			m_PlayerCar.DisableKinematic();
		}
		m_PlayerCar.SetMaxColliderMass(bMax: false);
		if (m_PlayerCar.isHero)
		{
			SoundManager.Instance.StopSound(52702);
			SoundManager.Instance.PlayBackgroundMusic();
		}
		m_PlayerCar.CarState &= 4294967167u;
		m_StateFlag = StateFlag.STOP;
		m_PlayerCar.m_CarRadius = 1.9f;
		m_PlayerCar.m_FollowDis = 1.5f;
		m_PlayerCar.SetCarTorqueScale(0);
		m_PlayerCar.IgnoreNPC(bIgnore: false);
	}
}
