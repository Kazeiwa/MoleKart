using Misc;
using RaceTrack;
using UnityEngine;

public class RobberBehaviour : ColliderMonsterBehaviour
{
	public enum StateID
	{
		None,
		Init,
		Patral,
		Lock,
		Pursue,
		Attack,
		Rest,
		Fall,
		Return
	}

	public class PatrolState : SuperFSM<StateID, RobberBehaviour>.State
	{
		public PatrolState(StateID stateID, RobberBehaviour behaviour)
		{
		}

		public override void Action()
		{
		}
	}

	public class LockState : SuperFSM<StateID, RobberBehaviour>.State
	{
		public LockState(StateID stateID, RobberBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void Action()
		{
		}
	}

	public class PursueState : SuperFSM<StateID, RobberBehaviour>.State
	{
		public PursueState(StateID stateID, RobberBehaviour behaviour)
		{
		}

		public override void Action()
		{
		}
	}

	public class AttackState : SuperFSM<StateID, RobberBehaviour>.State
	{
		public AttackState(StateID stateID, RobberBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void ExitAction()
		{
		}
	}

	public class RestState : SuperFSM<StateID, RobberBehaviour>.State
	{
		public RestState(StateID stateID, RobberBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class FallState : SuperFSM<StateID, RobberBehaviour>.State
	{
		public FallState(StateID stateID, RobberBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void ExitAction()
		{
		}
	}

	public class ReturnState : SuperFSM<StateID, RobberBehaviour>.State
	{
		public ReturnState(StateID stateID, RobberBehaviour behaviour)
		{
		}

		public override void Action()
		{
		}
	}

	public const float HeadingAngleDelta = 16f;

	public const float PatrolRadius = 10f;

	public const float GuardRadius = 20f;

	public const float ReachRadius = 15f;

	public const float AttackDistance = 2f;

	public const float HomeRadius = 1f;

	public const float PatrolSpeed = 6f;

	public const float PursueSpeed = 20f;

	public const float ReturnSpeed = 11f;

	public const float LockOnTimeLength = 0.5f;

	public const float LockOnEffectTimeLength = 1.5f;

	public const float AttackEffectTimeLength = 1.5f;

	public const float KnockedOutEffectTimeLength = 0.5f;

	public const float CarTimerLimit = 2f;

	public const float RestTimeLength = 5f;

	protected const float StopDistance = 0.5f;

	private Vector2 home;

	private IAutoCar pursueTargetCar;

	private SuperFSM<StateID, RobberBehaviour> fsm;

	public override bool ShouldBeAvoided
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

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

	public IAutoCar PursueTargetCar
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void PlayLockOnEffect()
	{
	}

	public void PlayAttackEffect()
	{
	}

	public void PlayKnockedOutEffect()
	{
	}

	public void UpdatePatrolMovement()
	{
	}

	public void UpdateLockHeading(CrudeElapsedTimer timer)
	{
	}

	public void UpdatePursueMovement()
	{
	}

	public void UpdateReturnMovement()
	{
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
	}

	public bool E1()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool E3()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool E4()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool E5()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool E6()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool E7()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool E8()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool E12()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override bool CanHitVehicle(Car vehicle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected override void InitEntity()
	{
	}

	protected override void UpdateEntity()
	{
	}

	protected void Start()
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

	private Vector2 CalculateVelocityWander()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private Vector2 CalculateVelocityPursue()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
