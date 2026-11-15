using System.Runtime.CompilerServices;
using Misc;
using UnityEngine;

public class RockMonsterBehaviour : ColliderMonsterBehaviour
{
	public enum StateID
	{
		None,
		Init,
		Idle,
		Walk,
		KnockedOut
	}

	public class IdleState : SuperFSM<StateID, RockMonsterBehaviour>.State
	{
		public IdleState(StateID stateID, RockMonsterBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void Action()
		{
		}
	}

	public class WalkState : SuperFSM<StateID, RockMonsterBehaviour>.State
	{
		public WalkState(StateID stateID, RockMonsterBehaviour behaviour)
		{
		}

		public override void Action()
		{
		}

		public override void ExitAction()
		{
		}
	}

	public class KnockedOutState : SuperFSM<StateID, RockMonsterBehaviour>.State
	{
		public KnockedOutState(StateID stateID, RockMonsterBehaviour behaviour)
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

	private const int MoveInc = 1;

	private const int MoveDec = -1;

	private const float IdleTimeLength = 5f;

	private const float KnockedOutTimeLength = 7f;

	private const float WalkSpeed = 20f;

	private const float SqareArriveThreshold = 1f;

	private const float BackFlipSpeedThreshold = 15f;

	public GameObject[] points;

	private SuperFSM<StateID, RockMonsterBehaviour> fsm;

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

	public int MoveOrder
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

	public bool ArrivedAtTargetPoint()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UpdateWalk()
	{
	}

	public override bool CanHitVehicle(Car vehicle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ProcessVehicleCollisionInWalk()
	{
	}

	public void ProcessVehicleCollisionInIdle()
	{
	}

	public void UpdateTargetPointIndex()
	{
	}

	public override void ClientOnReceiveStateInfo(int stateId)
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

	protected void OnDrawGizmosSelected()
	{
	}

	protected void MakeFSM()
	{
	}

	private void OnStateInfo(StateID stateId)
	{
	}
}
