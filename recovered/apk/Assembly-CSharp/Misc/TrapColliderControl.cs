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
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.ColliderBecomeTrigger();
		}
	}

	public class BlockerState : SuperFSM<StateID, TrapBehaviour>.State
	{
		public BlockerState(StateID stateID, TrapBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.ColliderBecomeBlocker();
		}
	}

	public class NothingState : SuperFSM<StateID, TrapBehaviour>.State
	{
		public NothingState(StateID stateID, TrapBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.ColliderBecomeNothing();
		}
	}

	private SuperFSM<StateID, TrapBehaviour> fsm;

	private TrapMovementStateID trapMovement;

	public TrapBehaviour Behaviour { get; set; }

	public TrapColliderControl(TrapBehaviour behaviour)
	{
		Behaviour = behaviour;
	}

	public void MakeFSM()
	{
		fsm = new SuperFSM<StateID, TrapBehaviour>(Behaviour);
		fsm.CreateState<BlockerState>(StateID.LowWithoutCar).CreateTransition(StateID.High, TrapInHighState);
		fsm.CreateState<TriggerState>(StateID.LowWithCar).CreateTransition(StateID.High, TrapInHighState).CreateTransition(StateID.LowWithCar, (TrapBehaviour b) => b.IsNotBesideAnyVehicle());
		fsm.CreateState<NothingState>(StateID.High).CreateTransition(StateID.GoingDown, TrapInGoingDownState);
		fsm.CreateState<TriggerState>(StateID.GoingDown).CreateTransition(StateID.High, TrapInHighState).CreateTransition(StateID.LowWithoutCar, TrapInLowStateWithoutCar)
			.CreateTransition(StateID.LowWithCar, TrapInLowState);
	}

	public void Update()
	{
		fsm.Update(doStateActionIfNotTriggered: true);
	}

	public void TrapEnterHighState()
	{
		trapMovement = TrapMovementStateID.High;
		fsm.Update(doStateActionIfNotTriggered: true);
	}

	public void TrapEnterGoingDownState()
	{
		trapMovement = TrapMovementStateID.GoingDown;
		fsm.Update(doStateActionIfNotTriggered: true);
	}

	public void TrapEnterLowState()
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
