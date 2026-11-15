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
		{
		}

		public override void EntryAction()
		{
		}

		public override void ExitAction()
		{
		}
	}

	private SuperFSM<StateID, ColliderMonsterBehaviour> fsm;

	private ColliderMonsterBehaviour monster;

	public TrickyColliderControl(ColliderMonsterBehaviour monster)
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
