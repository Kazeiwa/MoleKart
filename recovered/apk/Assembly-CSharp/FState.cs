public class FState : FAbstractState
{
	public enum ChildMode
	{
		ExclusiveStates,
		ParallelStates
	}

	private ChildMode m_Mode;

	public ChildMode childMode => m_Mode;

	public FState(ChildMode childMode, FState parent)
		: base(parent)
	{
		m_Mode = childMode;
	}

	public void CheckAndBreakShift()
	{
		if (m_PlayerCar.vehicle is Vehicle)
		{
			Vehicle vehicle = m_PlayerCar.vehicle as Vehicle;
			if (vehicle.m_kShift.IsShiftMode())
			{
				vehicle.m_kShift.BreakShift();
			}
		}
	}

	public void SetChildMode(ChildMode mode)
	{
		m_Mode = mode;
	}

	public override void Entered()
	{
	}

	public override void Exited()
	{
	}

	public override void Update()
	{
	}

	public override void UpdatePhysics()
	{
	}

	public override bool Reject()
	{
		foreach (int item in m_HighPriority)
		{
			if (m_Machine.GetState((EventType)item).m_StateFlag == StateFlag.UPDATING)
			{
				m_ForceExit = true;
				m_ForceExitState = m_Machine.GetState((EventType)item);
				m_Machine.GetState((EventType)item).m_ForceEnterState = this;
				return true;
			}
		}
		foreach (int item2 in m_LowPriority)
		{
			if (m_Machine.GetState((EventType)item2).m_StateFlag == StateFlag.UPDATING)
			{
				m_Machine.GetState((EventType)item2).m_StateFlag = StateFlag.OVER;
				m_Machine.GetState((EventType)item2).m_ForceExit = true;
				m_ForceEnterState = m_Machine.GetState((EventType)item2);
				m_Machine.GetState((EventType)item2).m_ForceExitState = this;
				m_ForceEnter = true;
			}
		}
		return false;
	}
}
