using UnityEngine;

public class AccelerateState : FState
{
	private float m_AccelerateTime;

	public AccelerateState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_CanRepeat = true;
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToAccelerate;
		m_HighPriority.Add(EventType.ToDead);
		m_HighPriority.Add(EventType.ToBounce);
		m_HighPriority.Add(EventType.ToBounceOver);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_StateFlag = StateFlag.UPDATING;
			m_AccelerateTime = 0f;
			m_PlayerCar.CarState |= 1024u;
			m_PlayerCar.StartBoost(m_PlayerCar.m_AccelerateTime);
			m_PlayerCar.EnableGroundFriction(bEnable: false);
		}
	}

	public override void Update()
	{
		m_AccelerateTime += Time.deltaTime;
		if (m_AccelerateTime >= m_PlayerCar.m_AccelerateTime)
		{
			FEvent evt = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
		}
	}

	public override void Exited()
	{
		m_PlayerCar.CarState &= 4294966271u;
		m_StateFlag = StateFlag.STOP;
		m_PlayerCar.EnableJet(enable: false);
		m_PlayerCar.EnableGroundFriction(bEnable: true);
		m_PlayerCar.m_AccelerateTime = 0f;
	}
}
