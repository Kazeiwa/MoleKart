using UnityEngine;

public class ThunderCloudState : FState
{
	private float m_ThunderCloudTime;

	public ThunderCloudState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToThunderCloud;
		m_HighPriority.Add(EventType.ToDead);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_StateFlag = StateFlag.UPDATING;
			m_ThunderCloudTime = 0f;
			m_PlayerCar.CarState |= 256u;
			m_PlayerCar.LimitSpeed(m_PlayerCar.FastSpeed);
			m_PlayerCar.EnableGroundFriction(bEnable: false);
			m_PlayerCar.SetCarTorqueScale(1);
		}
	}

	public override void Update()
	{
		m_ThunderCloudTime += Time.deltaTime;
		if (m_ThunderCloudTime >= ThunderCloud.LIFE_TIME)
		{
			FEvent evt = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
		}
	}

	public override void Exited()
	{
		m_PlayerCar.LimitSpeed(m_PlayerCar.MaxSpeed);
		m_PlayerCar.EnableGroundFriction(bEnable: true);
		m_PlayerCar.CarState &= 4294967039u;
		m_StateFlag = StateFlag.STOP;
		m_PlayerCar.SetCarTorqueScale(0);
	}
}
