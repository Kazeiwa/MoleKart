using UnityEngine;

public class ScaleFlatState : FState
{
	private float m_ContinueTime;

	public ScaleFlatState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToScaleFlat;
		m_LowPriority.Add(EventType.ToScaleSmaller);
		m_LowPriority.Add(EventType.ToScaleBigger);
		m_HighPriority.Add(EventType.ToDead);
		m_HighPriority.Add(EventType.ToInvincible);
		m_HighPriority.Add(EventType.ToMissile);
	}

	public override void Entered()
	{
		if (!m_CanRepeat && m_StateFlag == StateFlag.UPDATING)
		{
			return;
		}
		m_StateFlag = StateFlag.UPDATING;
		m_ContinueTime = 0f;
		if (m_ForceEnter)
		{
			m_ForceEnter = false;
			if (m_ForceEnterState.m_Type == EventType.ToScaleSmaller)
			{
				m_PlayerCar.Play("small_squash01");
			}
			else if (m_ForceEnterState.m_Type == EventType.ToScaleBigger)
			{
				m_PlayerCar.Play("squash01");
			}
		}
		else
		{
			m_PlayerCar.Play("squash01");
		}
		m_PlayerCar.BreakVehicle(1f);
		m_PlayerCar.LimitSpeed(m_PlayerCar.SlowSpeed);
		m_PlayerCar.PlaySound(52701, 1);
		m_PlayerCar.PlayHitByMissile();
		m_PlayerCar.DropProp(drop: false);
		m_PlayerCar.CarState |= 16384u;
		SceneUIManager.Instance.RotateCar(m_PlayerCar);
		CheckAndBreakShift();
	}

	public override void Update()
	{
		m_ContinueTime += Time.deltaTime;
		if (m_ContinueTime >= 10f)
		{
			FEvent evt = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
		}
	}

	public override void Exited()
	{
		if (m_ForceExit)
		{
			m_ForceExit = false;
			if (m_ForceExitState.m_Type == EventType.ToScaleBigger || m_ForceExitState.m_Type == EventType.ToInvincible || m_ForceExitState.m_Type == EventType.ToMissile || m_ForceExitState.m_Type == EventType.ToDead)
			{
				m_PlayerCar.Play("largen");
			}
		}
		else
		{
			m_PlayerCar.Play("largen");
		}
		m_PlayerCar.LimitSpeed(m_PlayerCar.MaxSpeed);
		m_PlayerCar.CarState &= 4294950911u;
		m_StateFlag = StateFlag.STOP;
	}
}
