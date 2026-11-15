using System.Runtime.CompilerServices;
using Misc;
using UnityEngine;

public class CowBehaviour : ColliderMonsterBehaviour
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

	public class IdleState : SuperFSM<StateID, CowBehaviour>.State
	{
		public IdleState(StateID stateID, CowBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void Action()
		{
		}
	}

	public class WalkState : SuperFSM<StateID, CowBehaviour>.State
	{
		public WalkState(StateID stateID, CowBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void Action()
		{
		}
	}

	public class KnockedOutState : SuperFSM<StateID, CowBehaviour>.State
	{
		public KnockedOutState(StateID stateID, CowBehaviour behaviour)
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

	private const int AvailableTargetCount = 8;

	private const float IdleTimeLength = 3f;

	private const float SqareArriveTargetThreshold = 0.5f;

	private const float hitVehicleDistanceThreshold = 2f;

	public float WalkRadius;

	public float WalkSpeed;

	public Vector2 Home;

	private SuperFSM<StateID, CowBehaviour> fsm;

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

	public int TargetId
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

	public void SendCommand(CommandID commandId)
	{
	}

	public virtual void SetTargetPos()
	{
	}

	public bool IsArrivedAtTargetPos()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override bool CanHitVehicle(Car vehicle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ProcessVehicleCollision()
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

	private void OnStateInfo(StateID stateId)
	{
	}

	protected virtual void UpdateWalk()
	{
	}
}
