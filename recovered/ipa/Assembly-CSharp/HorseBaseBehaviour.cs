using System.Runtime.CompilerServices;
using Misc;
using UnityEngine;

public class HorseBaseBehaviour : ColliderMonsterBehaviour
{
	public enum StateID
	{
		None,
		Init,
		Idle,
		Walk,
		KnockedOut
	}

	public enum CommandID
	{
		CarHit
	}

	public class IdleState : SuperFSM<StateID, HorseBaseBehaviour>.State
	{
		public IdleState(StateID stateID, HorseBaseBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void Action()
		{
		}
	}

	public class WalkState : SuperFSM<StateID, HorseBaseBehaviour>.State
	{
		public WalkState(StateID stateID, HorseBaseBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void Action()
		{
		}
	}

	public class KnockedOutState : SuperFSM<StateID, HorseBaseBehaviour>.State
	{
		public KnockedOutState(StateID stateID, HorseBaseBehaviour behaviour)
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

	public const float BackFlipSpeedThreshold = 15f;

	private const float IdleTimeLength = 3f;

	private const float SqareArriveTargetThreshold = 0.5f;

	public float WalkSpeed;

	private SuperFSM<StateID, HorseBaseBehaviour> fsm;

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

	public Vector2 Target
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

	public virtual void SetTargetPos()
	{
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

	public bool IsArrivedAtTargetPos()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
	}

	protected virtual void UpdateWalk()
	{
	}

	protected override void InitEntity()
	{
	}

	protected override void UpdateEntity()
	{
	}

	protected void MakeFSM()
	{
	}

	private void OnStateInfo(StateID stateId)
	{
	}
}
