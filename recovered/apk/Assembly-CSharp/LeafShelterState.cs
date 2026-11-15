using UnityEngine;

public class LeafShelterState : FState
{
	private float m_Time;

	private bool m_OnePlayed;

	private bool m_TwoPlayed;

	public LeafShelterState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToLeafShelter;
		m_HighPriority.Add(EventType.ToDead);
		m_HighPriority.Add(EventType.ToInvincible);
		m_HighPriority.Add(EventType.ToMissile);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_OnePlayed = (m_TwoPlayed = false);
			m_Time = 0f;
			m_StateFlag = StateFlag.UPDATING;
		}
	}

	public override void Update()
	{
		m_Time += Time.deltaTime;
		if (m_PlayerCar.isHero && m_Time >= 2f && !m_OnePlayed)
		{
			m_OnePlayed = true;
			SoundManager.Instance.PlaySound(52401);
		}
		if (m_PlayerCar.isHero && m_Time >= 6.5f && !m_TwoPlayed)
		{
			m_TwoPlayed = true;
			SoundManager.Instance.PlaySound(52402);
		}
		if (m_Time >= 8f)
		{
			FEvent evt = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
		}
	}

	public override void Exited()
	{
		m_StateFlag = StateFlag.STOP;
		m_PlayerCar.StopScreenEft(eEffectType.HIT_BY_LEAF.ToString());
	}
}
