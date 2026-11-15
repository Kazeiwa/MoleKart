using System.Runtime.CompilerServices;
using Misc;
using UnityEngine;

public class SealBehaviour : ColliderMonsterBehaviour
{
	public enum StateID
	{
		None,
		Init,
		TurnAround,
		Walk,
		KnockedOut
	}

	public enum CommandID
	{
		CarHit
	}

	public class KnockedOutBeginTransition : SuperFSM<StateID, SealBehaviour>.Transition
	{
		public KnockedOutBeginTransition(StateID destStateID, SealBehaviour behaviour)
		{
		}

		public override bool IsTriggered()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public class MoveState : SuperFSM<StateID, SealBehaviour>.State
	{
		public MoveState(StateID stateID, SealBehaviour behaviour)
		{
		}

		public override void Action()
		{
		}
	}

	public class WalkState : SuperFSM<StateID, SealBehaviour>.State
	{
		public WalkState(StateID stateID, SealBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void Action()
		{
		}
	}

	public class KnockedOutState : SuperFSM<StateID, SealBehaviour>.State
	{
		public KnockedOutState(StateID stateID, SealBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void ExitAction()
		{
		}
	}

	public const float CarTimerLimit = 2f;

	public const float WalkSpeed = 2f;

	public const float IdleTimeLength = 1f;

	private const float SqareArriveThreshold = 1f;

	private const float SqareVehicleCollisionThreshold = 4f;

	public const float BackFlipSpeedThreshold = 20f;

	public GameObject[] Points;

	private Vector2 home;

	private SuperFSM<StateID, SealBehaviour> fsm;

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

	public int TargetPointIndex
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

	public Vector2 TargetPos
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void SendCommand(CommandID commandId)
	{
	}

	public override bool CanHitVehicle(Car vehicle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ProcessVehicleCollision()
	{
	}

	public void UpdateTargetPointIndex()
	{
	}

	public bool ArrivedAtTargetPoint()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UpdateWalk(float deltaTime)
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

	protected void OnDrawGizmos()
	{
	}

	protected void MakeFSM()
	{
	}

	private void OnStateInfo(StateID stateId)
	{
	}
}
