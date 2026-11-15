using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private RestorePolicy m_GlobalRestorePolicy;

	private bool m_Runing;

	private FAbstractState m_Initialstate;

	private FAbstractState m_CurrentState;

	private Dictionary<EventType, FState> m_FStates = new Dictionary<EventType, FState>();

	private ArrayList m_Normalevents = new ArrayList();

	private ArrayList m_Highevents = new ArrayList();

	private GroupState m_GroupState = new GroupState();

	private FEvent tempitem = new FEvent();

	public FStateMachine()
	{
		m_GlobalRestorePolicy = RestorePolicy.DontRestoreProperties;
	}

	public RestorePolicy GlobalRestorePolicy()
	{
		return m_GlobalRestorePolicy;
	}

	public void AddState(EventType type, FState state)
	{
		m_FStates.Add(type, state);
		state.m_Machine = this;
	}

	public FAbstractState GetState(EventType type)
	{
		if (m_FStates.ContainsKey(type))
		{
			return m_FStates[type];
		}
		return null;
	}

	public bool IsRunning()
	{
		return m_Runing;
	}

	public void PostEvent(FEvent evt, EventPriority priority)
	{
		switch (priority)
		{
		case EventPriority.NormalPriority:
			m_Normalevents.Add(evt);
			break;
		case EventPriority.HighPriority:
			m_Highevents.Add(evt);
			break;
		default:
			m_Normalevents.Add(evt);
			break;
		}
	}

	public void SetInitialState(FState state)
	{
		m_Initialstate = state;
	}

	public FAbstractState InitialState()
	{
		return m_Initialstate;
	}

	public FAbstractState CurrentState()
	{
		return m_CurrentState;
	}

	public bool CanDrive()
	{
		return m_GroupState.CanDrive();
	}

	public void Start()
	{
		if (m_Initialstate != null)
		{
			m_CurrentState = m_Initialstate;
			m_GroupState.Init(m_CurrentState);
			m_Runing = true;
		}
		else
		{
			Debug.LogError("No initial state!");
		}
	}

	public void Stop()
	{
		if (m_Initialstate != null)
		{
			m_Initialstate.Exited();
			m_CurrentState = null;
			m_Runing = false;
		}
	}

	private bool PopOneEvent(ref FEvent item)
	{
		if (m_Highevents.Count > 0)
		{
			item = m_Highevents[0] as FEvent;
			m_Highevents.RemoveAt(0);
			return true;
		}
		if (m_Normalevents.Count > 0)
		{
			item = m_Normalevents[0] as FEvent;
			m_Normalevents.RemoveAt(0);
			return true;
		}
		return false;
	}

	public void Update()
	{
		if (PopOneEvent(ref tempitem))
		{
			if (m_CurrentState != null)
			{
				FState fState = null;
				if (m_FStates.ContainsKey(tempitem.type()))
				{
					fState = m_FStates[tempitem.type()];
				}
				if (fState != null)
				{
					m_GroupState.PushState(fState);
				}
			}
			else
			{
				m_CurrentState = m_FStates[tempitem.type()];
				m_GroupState.PushState(m_CurrentState);
			}
		}
		m_GroupState.Update();
	}

	public void UpdatePhysics()
	{
	}
}
