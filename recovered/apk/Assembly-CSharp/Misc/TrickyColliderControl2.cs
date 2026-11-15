namespace Misc;

public class TrickyColliderControl2
{
	public enum StateID
	{
		None,
		Init,
		NormalAndEnable,
		NormalAndDisable,
		KnockedOut
	}

	public class NormalAndEnableState : SuperFSM<StateID, ColliderMonsterBehaviour2>.State
	{
		public NormalAndEnableState(StateID stateID, ColliderMonsterBehaviour2 behaviour)
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

	private SuperFSM<StateID, ColliderMonsterBehaviour2> fsm;

	private ColliderMonsterBehaviour2 monster;

	public TrickyColliderControl2(ColliderMonsterBehaviour2 monster)
	{
		this.monster = monster;
	}

	public void MakeFSM()
	{
		fsm = new SuperFSM<StateID, ColliderMonsterBehaviour2>(monster);
		fsm.CreateState<NormalAndEnableState>(StateID.NormalAndEnable);
		fsm.CreateState(StateID.NormalAndDisable).CreateTransition(StateID.NormalAndEnable, (ColliderMonsterBehaviour2 b) => b.IsNotBesideAnyVehicle());
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
