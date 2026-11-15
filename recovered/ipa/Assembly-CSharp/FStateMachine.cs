using System.Collections.Generic;

public class FStateMachine
{
	public enum EventPriority
	{
		NormalPriority,
		HighPriority
	}

	public enum RestorePolicy
	{
		DontRestoreProperties,
		RestoreProperties
	}

	private FEvent tempItem;

	private RestorePolicy m_GlobalRestorePolicy;

	private bool m_Runing;

	private FAbstractState m_Initialstate;

	private FAbstractState m_CurrentState;

	private Dictionary<EventType, FState> m_FStates;

	private List<FEvent> m_Normalevents;

	private List<FEvent> m_Highevents;

	private GroupState m_GroupState;

	public RestorePolicy GlobalRestorePolicy()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void AddState(EventType type, FState state)
	{
	}

	public FAbstractState GetState(EventType type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool IsRunning()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void PostEvent(FEvent evt, EventPriority priority)
	{
	}

	public void SetInitialState(FState state)
	{
	}

	public FAbstractState InitialState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public FAbstractState CurrentState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool CanDrive()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Start()
	{
	}

	public void Stop()
	{
	}

	public void Update()
	{
	}

	public void UpdatePhysics()
	{
	}

	private bool PopOneEvent(ref FEvent item)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
