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
		{
		}

		public override void EntryAction()
		{
		}

		public override void ExitAction()
		{
		}
	}

	private SuperFSM<StateID, ColliderMonsterBehaviour2> fsm;

	private ColliderMonsterBehaviour2 monster;

	public TrickyColliderControl2(ColliderMonsterBehaviour2 monster)
	{
	}

	public void MakeFSM()
	{
	}

	public void BeKnockedOut()
	{
	}

	public void StandUp()
	{
	}

	public void Update()
	{
	}
}
