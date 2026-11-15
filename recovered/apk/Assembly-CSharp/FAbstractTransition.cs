using System.Collections;

public class FAbstractTransition
{
	private FStateMachine m_Machine;

	private FState m_Sourcestate;

	private FAbstractState m_Targetstate;

	private ArrayList m_Targetlist;

	public FAbstractTransition(FState sourceState)
	{
		m_Sourcestate = sourceState;
		m_Machine = sourceState.m_Machine;
	}

	public FStateMachine Machine()
	{
		return m_Machine;
	}

	public void SetTargetState(FAbstractState target)
	{
		m_Targetstate = target;
	}

	public void SetTargetStates(ArrayList targets)
	{
		m_Targetlist = targets.Clone() as ArrayList;
	}

	public FState SourceState()
	{
		return m_Sourcestate;
	}

	public FAbstractState TargetState()
	{
		return m_Targetstate;
	}

	public ArrayList TargetStates()
	{
		return m_Targetlist;
	}
}
