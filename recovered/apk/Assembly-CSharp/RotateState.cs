using UnityEngine;

public class RotateState : FState
{
	private float m_RotateTime = 3f;

	public RotateState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToRotate;
		m_HighPriority.Add(EventType.ToUpBound);
		m_HighPriority.Add(EventType.ToBigBound);
		m_HighPriority.Add(EventType.ToLeftBound);
		m_HighPriority.Add(EventType.ToRightBound);
		m_HighPriority.Add(EventType.ToDead);
		m_HighPriority.Add(EventType.ToInvincible);
		m_HighPriority.Add(EventType.ToBackFlip);
		m_HighPriority.Add(EventType.ToBounce);
		m_HighPriority.Add(EventType.ToBounceOver);
		m_LowPriority.Add(EventType.ToNormal);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_StateFlag = StateFlag.UPDATING;
			m_PlayerCar.LockSpeed(lockspeed: true);
			if (m_PlayerCar.CarScaleSmall)
			{
				m_PlayerCar.Play("small_rot");
			}
			else if (m_PlayerCar.CarScaleFlat)
			{
				m_PlayerCar.Play("flat_rot");
			}
			else
			{
				m_PlayerCar.Play("rot");
			}
			m_PlayerCar.CarState |= 8u;
			m_RotateTime = 1.5f;
			m_PlayerCar.DropProp(drop: false);
			m_PlayerCar.PlaySound(51003, 1);
			if (m_PlayerCar.GetRole(0) != null)
			{
				m_PlayerCar.GetRole(0).PlaySound(Role.SCREAM_LOW);
			}
			m_PlayerCar.BreakVehicle(1f);
			SceneUIManager.Instance.RotateCar(m_PlayerCar);
			CheckAndBreakShift();
		}
	}

	public override void Update()
	{
		m_RotateTime -= Time.deltaTime;
		if (m_RotateTime < 0f)
		{
			FEvent evt = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
		}
	}

	public override void Exited()
	{
		m_PlayerCar.LockSpeed(lockspeed: false);
		InputManager.Instance.CurrentSteering = 0f;
		m_PlayerCar.CarState &= 4294967287u;
		m_StateFlag = StateFlag.STOP;
	}
}
