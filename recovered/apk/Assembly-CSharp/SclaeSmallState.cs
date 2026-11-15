using UnityEngine;

public class SclaeSmallState : FState
{
	private float m_ContinueTime;

	private float m_fPreMass;

	public SclaeSmallState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToScaleSmaller;
		m_LowPriority.Add(EventType.ToScaleFlat);
		m_LowPriority.Add(EventType.ToScaleBigger);
		m_HighPriority.Add(EventType.ToDead);
		m_HighPriority.Add(EventType.ToInvincible);
		m_HighPriority.Add(EventType.ToMissile);
		m_HighPriority.Add(EventType.ToUpBound);
		m_HighPriority.Add(EventType.ToLeftBound);
		m_HighPriority.Add(EventType.ToRightBound);
		m_HighPriority.Add(EventType.ToBigBound);
	}

	public override void Entered()
	{
		if (!m_CanRepeat && m_StateFlag == StateFlag.UPDATING)
		{
			return;
		}
		m_StateFlag = StateFlag.UPDATING;
		m_ContinueTime = 0f;
		m_PlayerCar.DropProp(drop: true);
		if (m_ForceEnter)
		{
			m_ForceEnter = false;
			if (m_ForceEnterState.m_Type == EventType.ToScaleBigger)
			{
				m_PlayerCar.Play("Lightning01");
			}
			else if (m_ForceEnterState.m_Type == EventType.ToScaleFlat)
			{
				m_PlayerCar.Play("flat_Lightning01");
			}
		}
		else
		{
			m_PlayerCar.Play("Lightning01");
		}
		m_PlayerCar.CarState |= 2048u;
		m_PlayerCar.PlayHitByLightningEffect();
		m_PlayerCar.BreakVehicle(1f);
		m_PlayerCar.LimitSpeed(m_PlayerCar.SlowSpeed);
		m_PlayerCar.EnableGroundFriction(bEnable: false);
		bool flag = !m_PlayerCar.vehicle.IsKinematic();
		if (flag)
		{
			m_PlayerCar.EnableKinematic();
		}
		m_PlayerCar.SetDynamicScaleFactor(0.5f);
		if (flag)
		{
			m_PlayerCar.DisableKinematic();
		}
		m_fPreMass = m_PlayerCar.GetMass();
		m_PlayerCar.SetMass(m_fPreMass * 0.25f);
		SceneUIManager.Instance.RotateCar(m_PlayerCar);
		m_PlayerCar.m_CarRadius = 1.3f;
		m_PlayerCar.m_FollowDis = 0.6f;
		CheckAndBreakShift();
		m_PlayerCar.SetCarTorqueScale(2);
	}

	public override void Update()
	{
		m_ContinueTime += Time.deltaTime;
		if (m_ContinueTime >= 10f)
		{
			m_StateFlag = StateFlag.OVER;
			FEvent evt = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	public override void Exited()
	{
		if (m_ForceExit)
		{
			m_ForceExit = false;
			if (m_ForceExitState.m_Type == EventType.ToScaleBigger || m_ForceExitState.m_Type == EventType.ToInvincible || m_ForceExitState.m_Type == EventType.ToMissile || m_ForceExitState.m_Type == EventType.ToDead)
			{
				m_PlayerCar.Play("Lightning02");
			}
		}
		else
		{
			m_PlayerCar.Play("Lightning02");
		}
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
		m_PlayerCar.SetMass(m_fPreMass);
		m_PlayerCar.CarState &= 4294965247u;
		m_StateFlag = StateFlag.STOP;
		m_PlayerCar.m_CarRadius = 1.9f;
		m_PlayerCar.m_FollowDis = 1.5f;
		m_PlayerCar.SetCarTorqueScale(0);
	}
}
