using Misc;
using RaceTrack;
using UnityEngine;

public class FactoryRobotBehaviour : ColliderMonsterBehaviour
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

	public class PatrolState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public PatrolState(StateID stateID, FactoryRobotBehaviour behaviour)
		{
		}

		public override void Action()
		{
		}
	}

	public class LockState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public LockState(StateID stateID, FactoryRobotBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void Action()
		{
		}
	}

	public class PursueState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public PursueState(StateID stateID, FactoryRobotBehaviour behaviour)
		{
		}

		public override void Action()
		{
		}
	}

	public class AttackState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public AttackState(StateID stateID, FactoryRobotBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void ExitAction()
		{
		}
	}

	public class RestState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public RestState(StateID stateID, FactoryRobotBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class FallState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public FallState(StateID stateID, FactoryRobotBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void ExitAction()
		{
		}
	}

	public class ReturnState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public ReturnState(StateID stateID, FactoryRobotBehaviour behaviour)
		{
		}

		public override void Action()
		{
		}
	}

	public const float HeadingAngleDelta = 8f;

	public const float AttackDistance = 2.2f;

	public const float HomeRadius = 1f;

	public const float PatrolSpeed = 6f;

	public const float PursueSpeed = 20f;

	public const float ReturnSpeed = 11f;

	public const float LockOnTimeLength = 0.5f;

	public const float LockOnEffectTimeLength = 1f;

	public const float AttackEffectTimeLength = 1.5f;

	public const float KnockedOutEffectTimeLength = 0.5f;

	public const float CarTimerLimit = 3f;

	public const float RestTimeLength = 5f;

	protected const float StopDistance = 0.5f;

	public float PatrolRadius;

	public float GuardRadius;

	public float ReachRadius;

	private Vector2 cachedHomePos;

	private bool hasCachedHomePos;

	private IAutoCar pursueTargetCar;

	private SuperFSM<StateID, FactoryRobotBehaviour> fsm;

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

	protected void OnDrawGizmos()
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

	private void InitHomePos()
	{
	}
}
