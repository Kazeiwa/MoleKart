using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Misc;
using UnityEngine;

namespace RaceTrack.SteeringBehavior;

public class AdvancedSteeringBehavior
{
	public enum StateID
	{
		None,
		Init,
		StartOut,
		StartRun,
		PathFollowing,
		PathLookAhead,
		LeaveSticking,
		AntiStuck,
		FrontVehicleAvoidanceLA,
		MonsterAvoidanceLA,
		PropBoxAcquiringLA,
		SimplePathFollowing
	}

	public interface ISteeringState
	{
		SteeringArgument Steering { get; }
	}

	public class State : SuperFSM<StateID, AdvancedSteeringBehavior>.State, ISteeringState
	{
		public virtual SteeringArgument Steering
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		protected State(StateID stateID, AdvancedSteeringBehavior sb)
		{
		}
	}

	public class InitState : State
	{
		public InitState(StateID stateID, AdvancedSteeringBehavior sb)
		{
		}
	}

	public class StartOutState : State
	{
		public StartOutState(StateID stateID, AdvancedSteeringBehavior sb)
		{
		}

		public override void EntryAction()
		{
		}

		public override void Action()
		{
		}
	}

	public class StartRunState : State
	{
		public override SteeringArgument Steering
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public StartRunState(StateID stateID, AdvancedSteeringBehavior sb)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class PathFollowingState : State
	{
		public override SteeringArgument Steering
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public PathFollowingState(StateID stateID, AdvancedSteeringBehavior sb)
		{
		}

		public override void Action()
		{
		}
	}

	public class PathLookAheadState : State
	{
		public override SteeringArgument Steering
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public PathLookAheadState(StateID stateID, AdvancedSteeringBehavior sb)
		{
		}

		public override void Action()
		{
		}
	}

	public class AntiStuckState : State
	{
		public override SteeringArgument Steering
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public AntiStuckState(StateID stateID, AdvancedSteeringBehavior sb)
		{
		}

		public override void ExitAction()
		{
		}
	}

	public class FrontVehicleAvoidanceLAState : State
	{
		public override SteeringArgument Steering
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public FrontVehicleAvoidanceLAState(StateID stateID, AdvancedSteeringBehavior sb)
		{
		}

		public override void Action()
		{
		}

		public override void ExitAction()
		{
		}
	}

	public class MonsterAvoidanceLAState : State
	{
		public override SteeringArgument Steering
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public MonsterAvoidanceLAState(StateID stateID, AdvancedSteeringBehavior sb)
		{
		}

		public override void Action()
		{
		}

		public override void ExitAction()
		{
		}
	}

	public class PropBoxAcquiringLAState : State
	{
		public override SteeringArgument Steering
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public PropBoxAcquiringLAState(StateID stateID, AdvancedSteeringBehavior sb)
		{
		}

		public override void Action()
		{
		}

		public override void ExitAction()
		{
		}
	}

	public class SimplePathFollowingState : State
	{
		public override SteeringArgument Steering
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public SimplePathFollowingState(StateID stateID, AdvancedSteeringBehavior sb)
		{
		}

		public override void Action()
		{
		}
	}

	public struct VehicleLookAheadToAvoid
	{
		public IAutoCar Target;

		public LookAheadInfo LookAheadInfo;

		public static VehicleLookAheadToAvoid Invalid
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public bool IsInvalid
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public VehicleLookAheadToAvoid(IAutoCar target, Vector2 lookAheadPos, float leftWidth, float rightWidth)
		{
		}

		public VehicleLookAheadToAvoid(IAutoCar target, LookAheadInfo lookAheadInfo)
		{
		}
	}

	public struct MonsterLookAheadToAvoid
	{
		public IMonster Target;

		public LookAheadInfo LookAheadInfo;

		public static MonsterLookAheadToAvoid Invalid
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public bool IsInvalid
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public MonsterLookAheadToAvoid(IMonster target, Vector2 lookAheadPos, float leftWidth, float rightWidth)
		{
		}

		public MonsterLookAheadToAvoid(IMonster target, LookAheadInfo lookAheadInfo)
		{
		}
	}

	public struct PowerUpLookAheadToAcquire
	{
		public Prop Target;

		public LookAheadInfo LookAheadInfo;

		public static PowerUpLookAheadToAcquire Invalid
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public bool IsInvalid
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public PowerUpLookAheadToAcquire(Prop target, Vector2 lookAheadPos, float leftWidth, float rightWidth)
		{
		}

		public PowerUpLookAheadToAcquire(Prop target, LookAheadInfo lookAheadInfo)
		{
		}
	}

	private const float WayPointSeekDist = 7f;

	private const float WallDetectionFeelerLength = 12f;

	private const int FeelerCount = 3;

	private const float TimeToCollisionThreshold = 2f;

	private const float TimeToWaitAtStartOut1 = 0.5f;

	private const float TimeToWaitAtStartOut2 = 1f;

	private const float CheckConditionsWeakDistanceThreshold = 10f;

	private const float StickingTimeLength = 3f;

	private const float LeaveStickingTimeLength = 4f;

	private const float FrontVehicleAvoidanceLookAheadXDistance = 30f;

	private const float FrontVehicleAvoidanceLookAheadYDistance = 5f;

	private const float FrontMonsterAvoidanceLookAheadXDistance = 30f;

	private const float FrontMonsterAvoidanceLookAheadYDistance = 5f;

	private const float FrontPropBoxAcquiringLookAheadXDistance = 25f;

	private const float FrontPropBoxAcquiringLookAheadYDistance = 18f;

	private const float AngleDegreeThresholdOfPathLookAhead = 10f;

	private const float AngleDegreeThresholdOfMonsterAvoidanceLA = 15f;

	private static float AngleCosineThresholdOfPathLookAhead;

	private static float AngleCosineThresholdOfMonsterAvoidanceLA;

	private StuckDetector stuckDetector;

	private static Dictionary<StateID, string> stateToTextMapping;

	private RacingLine racingLine;

	private IObstacle firstTarget;

	private SimpleElapsedTimer startOutTimer;

	private SuperFSM<StateID, AdvancedSteeringBehavior> fsm;

	public IAutoCar Vehicle
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

	public string Name
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public RaceNode TargetNode
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

	public bool NeedPropAcquiring
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsStuck
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

	public bool StickingMonitorEnable
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

	private IList<VehicleLookAheadToAvoid> FrontVehiclesLookAheadToAvoid
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

	public VehicleLookAheadToAvoid FrontVehicleLookAheadToAvoid
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

	private IList<MonsterLookAheadToAvoid> FrontMonstersLookAheadToAvoid
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

	public MonsterLookAheadToAvoid FrontMonsterLookAheadToAvoid
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

	public PowerUpLookAheadToAcquire FrontPropBoxLookAheadToAcquire
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

	private StatedPathLookAhead LookAhead
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

	public float TargetLookAheadY
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

	public Vector2 TargetLookAheadPos
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 NextTargetLookAheadPos
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void AttachVehicle(IAutoCar vehicle)
	{
	}

	public void AttachRacingLine(RacingLine racingLine)
	{
	}

	public void AttachWalls(IList<Wall> walls)
	{
	}

	public void AttachObstacles(IList<IObstacle> obstacles)
	{
	}

	public bool IsLookAheadInvailable()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool IsLookAheadAvailable()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool HasFrontVehicleLookAheadToAvoid()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool HasNoFrontVehicleLookAheadToAvoid()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool HasFrontMonsterLookAheadToAvoid()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool HasNoFrontMonsterLookAheadToAvoid()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void CheckFrontVehicleShouldBeAvoided(IAutoCar target)
	{
	}

	public void CheckFrontVehiclesShouldBeAvoided()
	{
	}

	private void CheckFrontMonsterShouldBeAvoided(IMonster target)
	{
	}

	public void CheckMonstersShouldBeAvoided()
	{
	}

	private bool HasTargetPowerUp(Prop powerUp)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool HasFrontPropBoxLookAheadToAcquire()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool HasNoFrontPropBoxLookAheadToAcquire()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool CanPropBoxBeAcquiredInFront(Prop target, out LookAheadInfo relativeLookAheadInfo)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void UnlockFrontPropBoxLookAheadToAcquire()
	{
	}

	private void LockFrontPropBoxLookAheadToAcquire(Prop target, LookAheadInfo lookAheadInfo)
	{
	}

	public void CheckPropBoxsShouldBeAcquired()
	{
	}

	public void MakeFSM()
	{
	}

	public void OnDrawGizmos()
	{
	}

	public void OnDrawGizmosSelected()
	{
	}

	public virtual SteeringArgument CalculateSteering()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UpdateState(bool doit)
	{
	}

	public SteeringArgument Force2Steering(Vector2 direction)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector2 PathFollowingForce()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector2 PathLookAheadForce()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector2 PathLookAheadForceNextTarget()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SteeringArgument CalculateSteeringPathLookAhead(float targetY)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SteeringArgument CalculateSteeringPathLookAheadNextTarget(float targetY)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SteeringArgument CalculateSteeringMonsterAvoidanceLA(float targetY)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UpdatePathFollowing()
	{
	}

	public void UpdatePathLookAhead()
	{
	}

	public bool CheckStartOutDone()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void InitStartOut()
	{
	}

	public void UpdateStartOut()
	{
	}

	public void UninitStartOut()
	{
	}

	public void ResetStickingMonitor()
	{
	}

	private static float CalculateVehicleAvoidanceTargetY(LookAheadInfo lookAheadInfo, float y)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static float CalculateMonsterAvoidanceTargetY(LookAheadInfo lookAheadInfo, float y)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static bool InTheWayOf(IAutoCar front, IAutoCar back)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void CheckConditions(bool doit)
	{
	}

	private bool MonsterIsInMyWay(IMonster monster)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool PropBoxIsInMyWay(Prop propBox)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool PropIsInMyWay(IProp prop)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool FrontVehicleShouldBeAvoided(IAutoCar vehicle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool BackVehicleShouldBeAvoided(IAutoCar vehicle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool IsFarFromCamera()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool IsNotFarFromCamera()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private Vector2 SeekForce(Vector2 target)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void UpdateStuckDetection()
	{
	}
}
