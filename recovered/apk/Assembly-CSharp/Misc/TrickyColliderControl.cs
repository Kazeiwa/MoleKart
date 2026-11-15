namespace Misc;

public class TrickyColliderControl
{
	public enum StateID
	{
		None,
		Init,
		NormalAndEnable,
		NormalAndDisable,
		KnockedOut
	}

	public class NormalAndEnableState : SuperFSM<StateID, ColliderMonsterBehaviour>.State
	{
		public NormalAndEnableState(StateID stateID, ColliderMonsterBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.EnablePhysics();
		}

		public override void ExitAction()
		{
			base.Behaviour.DisablePhysics();
		}
	}

	private SuperFSM<StateID, ColliderMonsterBehaviour> fsm;

	private ColliderMonsterBehaviour monster;

	public TrickyColliderControl(ColliderMonsterBehaviour monster)
	{
		this.monster = monster;
	}

	public void MakeFSM()
	{
		fsm = new SuperFSM<StateID, ColliderMonsterBehaviour>(monster);
		fsm.CreateState<NormalAndEnableState>(StateID.NormalAndEnable);
		fsm.CreateState(StateID.NormalAndDisable).CreateTransition(StateID.NormalAndEnable, (ColliderMonsterBehaviour b) => b.IsNotBesideAnyVehicle());
		fsm.CreateState(StateID.KnockedOut);
	}

	public void BeKnockedOut()
	{
		fsm.GotoState(StateID.KnockedOut);
	}

	public void StandUp()
	{
		fsm.GotoState(StateID.NormalAndDisable);
	}

	public void Update()
	{
		fsm.Update(doStateActionIfNotTriggered: true);
	}
}
