using System.Runtime.CompilerServices;

namespace Misc;

public class TollGateColliderControl
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

	public class TriggerState : SuperFSM<StateID, TollGateBehaviour>.State
	{
		public TriggerState(StateID stateID, TollGateBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class BlockerState : SuperFSM<StateID, TollGateBehaviour>.State
	{
		public BlockerState(StateID stateID, TollGateBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class NothingState : SuperFSM<StateID, TollGateBehaviour>.State
	{
		public NothingState(StateID stateID, TollGateBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	private SuperFSM<StateID, TollGateBehaviour> fsm;

	private TrapMovementStateID trapMovement;

	public TollGateBehaviour Behaviour
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

	public TollGateColliderControl(TollGateBehaviour behaviour)
	{
	}

	public void MakeFSM()
	{
	}

	public void Update()
	{
	}

	public void EnterHighState()
	{
	}

	public void EnterGoingDownState()
	{
	}

	public void EnterLowState()
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
