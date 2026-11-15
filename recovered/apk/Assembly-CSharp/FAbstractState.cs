using System.Collections;

public abstract class FAbstractState
{
	protected ArrayList m_HighPriority = new ArrayList();

	protected ArrayList m_LowPriority = new ArrayList();

	public bool m_ForceEnter;

	public FAbstractState m_ForceEnterState;

	public bool m_ForceExit;

	public FAbstractState m_ForceExitState;

	public FStateMachine m_Machine;

	protected FState m_Parent;

	public StateFlag m_StateFlag;

	public EventType m_Type = EventType.None;

	protected Car m_PlayerCar;

	protected bool m_CanRepeat;

	public bool CanRepeat => m_CanRepeat;

	public Car PlayerCar => m_PlayerCar;

	protected FAbstractState(FState parent)
	{
		m_Parent = parent;
	}

	public abstract void Entered();

	public abstract void Exited();

	public abstract void Update();

	public abstract void UpdatePhysics();

	public abstract bool Reject();
}
