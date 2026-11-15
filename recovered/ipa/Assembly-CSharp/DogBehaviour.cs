using Misc;
using UnityEngine;

public class DogBehaviour : ColliderMonsterBehaviour2
{
	public enum StateID
	{
		None,
		Init,
		Walk,
		KnockedOut
	}

	public enum CommandID
	{
		CarHit
	}

	public class WalkState : SuperFSM<StateID, DogBehaviour>.State
	{
		public WalkState(StateID stateID, DogBehaviour behaviour)
		{
		}

		public override void Action()
		{
		}
	}

	public class KnockedOutState : SuperFSM<StateID, DogBehaviour>.State
	{
		public KnockedOutState(StateID stateID, DogBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void ExitAction()
		{
		}
	}

	public const float BackFlipSpeedThreshold = 15f;

	private const float CarTimerLimit = 2f;

	private const float IdleTimeLength = 3f;

	private const float SqareArriveTargetThreshold = 0.5f;

	public GameObject Center;

	public float WalkRadius;

	public float WalkSpeed;

	private SuperFSM<StateID, DogBehaviour> fsm;

	private StateID stateId;

	private float walkAroundAngle;

	public override Transform GrabTransform
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override GameObject GrabGameObject
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Home
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float WalkAroundAngle
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public void SendCommand(CommandID commandId)
	{
	}

	public void OnCollision(Car car)
	{
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
	}

	protected override void InitEntity()
	{
	}

	protected override void UpdateEntity()
	{
	}

	protected virtual void OnDrawGizmos()
	{
	}

	protected void MakeFSM()
	{
	}

	protected virtual void UpdateWalk(float deltaTime)
	{
	}

	private void OnStateInfo(StateID stateId)
	{
	}
}
