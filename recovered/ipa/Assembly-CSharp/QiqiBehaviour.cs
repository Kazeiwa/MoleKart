using System.Runtime.CompilerServices;
using Misc;
using UnityEngine;

public class QiqiBehaviour : ColliderMonsterBehaviour
{
	public enum StateID
	{
		None,
		Init,
		Idle,
		Walk,
		KnockedOut01,
		KnockedOut02,
		KnockedOut03
	}

	public class KnockedOutBeginTransition : SuperFSM<StateID, QiqiBehaviour>.Transition
	{
		public KnockedOutBeginTransition(StateID destStateID, QiqiBehaviour behaviour)
		{
		}

		public override bool IsTriggered()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public class IdleState : SuperFSM<StateID, QiqiBehaviour>.State
	{
		public IdleState(StateID stateID, QiqiBehaviour behaviour)
		{
		}

		public override void Action()
		{
		}
	}

	public class WalkState : SuperFSM<StateID, QiqiBehaviour>.State
	{
		public WalkState(StateID stateID, QiqiBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void Action()
		{
		}
	}

	public class KnockedOutState01 : SuperFSM<StateID, QiqiBehaviour>.State
	{
		public KnockedOutState01(StateID stateID, QiqiBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class KnockedOutState03 : SuperFSM<StateID, QiqiBehaviour>.State
	{
		public KnockedOutState03(StateID stateID, QiqiBehaviour behaviour)
		{
		}

		public override void ExitAction()
		{
		}
	}

	public const float CarTimerLimit = 2f;

	private const float BackFlipSpeedThreshold = 15f;

	public const float WalkSpeed = 2f;

	public const float IdleTimeLength = 1f;

	private const float SqareArriveThreshold = 1f;

	public GameObject[] Points;

	private Vector2 home;

	private SuperFSM<StateID, QiqiBehaviour> fsm;

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

	public void UpdateWalk()
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

	protected void MakeFSM()
	{
	}

	private void OnStateInfo(StateID stateId)
	{
	}
}
