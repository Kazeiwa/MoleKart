using System.Collections.Generic;

public abstract class FAbstractState
{
	public bool m_ForceEnter;

	public FAbstractState m_ForceEnterState;

	public bool m_ForceExit;

	public FAbstractState m_ForceExitState;

	public FStateMachine m_Machine;

	public StateFlag m_StateFlag;

	public EventType m_Type;

	protected List<EventType> m_HighPriority;

	protected List<EventType> m_LowPriority;

	protected FState m_Parent;

	protected Car m_PlayerCar;

	protected bool m_CanRepeat;

	public bool CanRepeat
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Car PlayerCar
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	protected FAbstractState(FState parent)
	{
	}

	public abstract void Entered();

	public abstract void Exited();

	public abstract void Update();

	public abstract void UpdatePhysics();

	public abstract bool Reject();
}
