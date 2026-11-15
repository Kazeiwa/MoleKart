using RaceTrack;
using UnityEngine;

public class InvincibleState : FState
{
	private float m_InvincibleTime;

	public static float TIME = 12.9f;

	public InvincibleState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToInvincible;
		m_LowPriority.Add(EventType.ToScaleBigger);
		m_LowPriority.Add(EventType.ToScaleSmaller);
		m_LowPriority.Add(EventType.ToScaleFlat);
		m_LowPriority.Add(EventType.ToRotate);
		m_LowPriority.Add(EventType.ToLeafShelter);
		m_HighPriority.Add(EventType.ToDead);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_StateFlag = StateFlag.UPDATING;
			m_InvincibleTime = 0f;
			m_PlayerCar.PlayInvincibleEffect();
			m_PlayerCar.LimitSpeed(m_PlayerCar.FastSpeed);
			if (m_PlayerCar.isHero)
			{
				SoundManager.Instance.StopBackgroundMusic(isPause: true);
				SoundManager.Instance.PlaySoundEx(52001);
			}
			m_PlayerCar.EnableGroundFriction(bEnable: false);
			m_PlayerCar.CarState |= 64u;
			m_PlayerCar.SetCarTorqueScale(1);
			m_PlayerCar.IgnoreNPC(bIgnore: true);
		}
	}

	public override void Update()
	{
		m_InvincibleTime += Time.deltaTime;
		if (m_InvincibleTime >= TIME)
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
		m_PlayerCar.LimitSpeed(m_PlayerCar.MaxSpeed);
		m_PlayerCar.EnableGroundFriction(bEnable: true);
		if (m_PlayerCar.isHero)
		{
			SoundManager.Instance.StopSound(52001);
			SoundManager.Instance.PlayBackgroundMusic();
		}
		m_PlayerCar.CarState &= 4294967231u;
		m_StateFlag = StateFlag.STOP;
		m_PlayerCar.StopCarEft(eEffectType.INVINCIBLE_EFFECT.ToString());
		m_PlayerCar.SetCarTorqueScale(0);
		m_PlayerCar.IgnoreNPC(bIgnore: false);
	}
}
