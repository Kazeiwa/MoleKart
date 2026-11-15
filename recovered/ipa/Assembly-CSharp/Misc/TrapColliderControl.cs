using System.Runtime.CompilerServices;

namespace Misc;

public class TrapColliderControl
{
	public enum StateID
	{
		None,
		Init,
		High,
		GoingDown,
		LowWithCar,
		LowWithoutCar
	}

	public enum TrapMovementStateID
	{
		None,
		High,
		GoingDown,
		Low
	}

	public class TriggerState : SuperFSM<StateID, TrapBehaviour>.State
	{
		public TriggerState(StateID stateID, TrapBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class BlockerState : SuperFSM<StateID, TrapBehaviour>.State
	{
		public BlockerState(StateID stateID, TrapBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class NothingState : SuperFSM<StateID, TrapBehaviour>.State
	{
		public NothingState(StateID stateID, TrapBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	private SuperFSM<StateID, TrapBehaviour> fsm;

	private TrapMovementStateID trapMovement;

	public TrapBehaviour Behaviour
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public TrapColliderControl(TrapBehaviour behaviour)
	{
	}

	public void MakeFSM()
	{
	}

	public void Update()
	{
	}

	public void TrapEnterHighState()
	{
	}

	public void TrapEnterGoingDownState()
	{
	}

	public void TrapEnterLowState()
	{
	}

	public bool TrapInHighState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool TrapInGoingDownState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool TrapInLowState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool TrapInLowStateWithoutCar()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
