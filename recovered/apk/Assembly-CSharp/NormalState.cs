public class NormalState : FState
{
	public NormalState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToNormal;
		m_HighPriority.Add(EventType.ToRotate);
		m_HighPriority.Add(EventType.ToUpBound);
		m_HighPriority.Add(EventType.ToBigBound);
		m_HighPriority.Add(EventType.ToRightBound);
		m_HighPriority.Add(EventType.ToLeftBound);
		m_HighPriority.Add(EventType.ToMissile);
		m_HighPriority.Add(EventType.ToDead);
		m_HighPriority.Add(EventType.ToBackFlip);
		m_LowPriority.Add(EventType.ToThunderCloud);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_PlayerCar.CarState |= 1u;
			m_StateFlag = StateFlag.UPDATING;
			m_PlayerCar.LimitSpeed(m_PlayerCar.MaxSpeed);
		}
	}

	public override void Update()
	{
		if (m_PlayerCar.isHero)
		{
			UpdateAction();
			updatePlayerState();
		}
	}

	private void UpdateAction()
	{
		if (InputManager.Instance.CheckUseProp())
		{
			SkillManager.Instance.UseProp(m_PlayerCar, throwForward: true);
		}
		if (InputManager.Instance.CheckBackwardUseProp())
		{
			SkillManager.Instance.UseProp(m_PlayerCar, throwForward: false);
		}
		if (InputManager.Instance.Check0())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.MUSHROOM_ONE);
		}
		else if (InputManager.Instance.Check1())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.MUSHROOM_BIG);
		}
		else if (InputManager.Instance.Check2())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.MISSILE);
		}
		else if (InputManager.Instance.Check3())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.LUCKY_STAR);
		}
		else if (InputManager.Instance.Check4())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.LIGHTNING);
		}
		else if (InputManager.Instance.Check5())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.CUTTLE_FISH);
		}
		else if (InputManager.Instance.Check6())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.CHEAT_BOX);
		}
		else if (InputManager.Instance.Check7())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.BOMB);
		}
		else if (InputManager.Instance.Check8())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.BANANA_THREE);
		}
		else if (InputManager.Instance.Check9())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.BANANA);
		}
		else if (InputManager.Instance.CheckF1())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.QUAKE);
		}
		else if (InputManager.Instance.CheckF2())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.THUNDER_CLOUD);
		}
		else if (InputManager.Instance.CheckF3())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.TORTOISE_BLUE);
		}
		else if (InputManager.Instance.CheckF4())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.TORTOISE_GREEN);
		}
		else if (InputManager.Instance.CheckF5())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.TORTOISE_GREEN_THREE);
		}
		else if (InputManager.Instance.CheckF6())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.TORTOISE_RED);
		}
		else if (InputManager.Instance.CheckF7())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.TORTOISE_RED_THREE);
		}
		else if (InputManager.Instance.CheckF8())
		{
			SceneUIManager.Instance.RollPropUI(ePropType.MUSHROOM_THREE);
		}
		if (InputManager.Instance.CheckEquipProp())
		{
			TSingleton<PropManager>.Instance.EquipTortoise(m_PlayerCar, 3, ePropType.TORTOISE_RED);
		}
	}

	private void updatePlayerState()
	{
		if (m_PlayerCar.GetPlayerState() != PlayerState.STATE_ONLYPLAY)
		{
			if (m_PlayerCar.CarState == 1024)
			{
				m_PlayerCar.SetPlayerState(PlayerState.STATE_ACC);
			}
			else if (InputManager.Instance.bTouchMode_Break && m_PlayerCar.GetAheadSpeed() < 0f)
			{
				m_PlayerCar.SetPlayerState(PlayerState.STATE_TURN_DOWN);
			}
			else if (InputManager.Instance.CurrentSteering < -0.2f)
			{
				m_PlayerCar.SetPlayerState(PlayerState.STATE_LEFT_LEAN);
			}
			else if (InputManager.Instance.CurrentSteering > 0.2f)
			{
				m_PlayerCar.SetPlayerState(PlayerState.STATE_RIGHT_LEAN);
			}
			else
			{
				m_PlayerCar.ResetPlayerState();
			}
		}
	}

	public override void Exited()
	{
		m_PlayerCar.CarState &= 4294967294u;
		m_StateFlag = StateFlag.STOP;
	}
}
