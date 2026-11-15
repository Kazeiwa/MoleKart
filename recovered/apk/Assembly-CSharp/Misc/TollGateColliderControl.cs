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
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.ColliderBecomeTrigger();
		}
	}

	public class BlockerState : SuperFSM<StateID, TollGateBehaviour>.State
	{
		public BlockerState(StateID stateID, TollGateBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.ColliderBecomeBlocker();
		}
	}

	public class NothingState : SuperFSM<StateID, TollGateBehaviour>.State
	{
		public NothingState(StateID stateID, TollGateBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.ColliderBecomeNothing();
		}
	}

	private SuperFSM<StateID, TollGateBehaviour> fsm;

	private TrapMovementStateID trapMovement;

	public TollGateBehaviour Behaviour { get; set; }

	public TollGateColliderControl(TollGateBehaviour behaviour)
	{
		Behaviour = behaviour;
	}

	public void MakeFSM()
	{
		fsm = new SuperFSM<StateID, TollGateBehaviour>(Behaviour);
		fsm.CreateState<BlockerState>(StateID.LowWithoutCar).CreateTransition(StateID.High, TrapInHighState);
		fsm.CreateState<TriggerState>(StateID.LowWithCar).CreateTransition(StateID.High, TrapInHighState).CreateTransition(StateID.LowWithCar, (TollGateBehaviour b) => b.IsNotBesideAnyVehicle());
		fsm.CreateState<NothingState>(StateID.High).CreateTransition(StateID.GoingDown, TrapInGoingDownState);
		fsm.CreateState<TriggerState>(StateID.GoingDown).CreateTransition(StateID.High, TrapInHighState).CreateTransition(StateID.LowWithoutCar, TrapInLowStateWithoutCar)
			.CreateTransition(StateID.LowWithCar, TrapInLowState);
	}

	public void Update()
	{
		fsm.Update(doStateActionIfNotTriggered: true);
	}

	public void EnterHighState()
	{
		trapMovement = TrapMovementStateID.High;
		fsm.Update(doStateActionIfNotTriggered: true);
	}

	public void EnterGoingDownState()
	{
		trapMovement = TrapMovementStateID.GoingDown;
		fsm.Update(doStateActionIfNotTriggered: true);
	}

	public void EnterLowState()
	{
		trapMovement = TrapMovementStateID.Low;
		fsm.Update(doStateActionIfNotTriggered: true);
	}

	public bool TrapInHighState()
	{
		return trapMovement == TrapMovementStateID.High;
	}

	public bool TrapInGoingDownState()
	{
		return trapMovement == TrapMovementStateID.GoingDown;
	}

	public bool TrapInLowState()
	{
		return trapMovement == TrapMovementStateID.Low;
	}

	public bool TrapInLowStateWithoutCar()
	{
		return TrapInLowState() && Behaviour.IsNotBesideAnyVehicle();
	}
}
