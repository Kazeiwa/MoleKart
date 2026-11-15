using System;
using System.Collections.Generic;
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
		Braking,
		TurningAround,
		MonsterAvoidance,
		FrontVehicleAvoidance,
		BackVehicleAvoidance,
		FrontPropAvoidance,
		BackPropAvoidance,
		PropAcquiring,
		LeaveSticking
	}

	public interface ISteeringState
	{
		SteeringArgument CalculateSteering();
	}

	public class State : SuperFSM<StateID, AdvancedSteeringBehavior>.State, ISteeringState
	{
		protected State(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public virtual SteeringArgument CalculateSteering()
		{
			return SteeringArgument.Default;
		}
	}

	public class TimedState : SuperFSM<StateID, AdvancedSteeringBehavior>.State, ISteeringState
	{
		protected TimedState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public virtual SteeringArgument CalculateSteering()
		{
			return SteeringArgument.Default;
		}
	}

	public class InitState : State
	{
		public InitState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}
	}

	public class StartOutState : State
	{
		public StartOutState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.InitStartOut();
		}

		public override void Action()
		{
			base.Behaviour.UpdateStartOut();
		}
	}

	public class StartRunState : TimedState
	{
		public StartRunState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override void EntryAction()
		{
			((Car)base.Behaviour.Vehicle).StartBoost(0.5f);
		}

		public override SteeringArgument CalculateSteering()
		{
			return SteeringArgument.Forth;
		}
	}

	public class PathFollowingState : State
	{
		public PathFollowingState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override void Action()
		{
			base.Behaviour.UpdatePathFollowing();
		}

		public override SteeringArgument CalculateSteering()
		{
			Vector2 direction = base.Behaviour.PathFollowingForce();
			SteeringArgument result = base.Behaviour.Force2Steering(direction);
			if (result.Direction == SteeringArgument.SteeringDirection.Back)
			{
				result.Rate = ((!(result.Rate > 0f)) ? (-1f) : 1f);
				result.Direction = ((base.Behaviour.Vehicle.Vel.magnitude > 2f) ? SteeringArgument.SteeringDirection.Still : SteeringArgument.SteeringDirection.Forth);
			}
			if (float.IsNaN(result.Rate))
			{
				Debug.LogError("PathFollowingState.CalculateSteering: steering rate is NaN");
			}
			return result;
		}
	}

	public class PathLookAheadState : State
	{
		public PathLookAheadState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override void Action()
		{
			base.Behaviour.UpdatePathLookAhead();
		}

		public override SteeringArgument CalculateSteering()
		{
			return base.Behaviour.CalculateSteeringPathLookAhead();
		}
	}

	public class BrakingState : State
	{
		public BrakingState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override SteeringArgument CalculateSteering()
		{
			return SteeringArgument.Brake;
		}
	}

	public class TurningAroundState : State
	{
		public TurningAroundState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override SteeringArgument CalculateSteering()
		{
			return SteeringArgument.Default;
		}
	}

	public class MonsterAvoidanceState : State
	{
		public MonsterAvoidanceState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override SteeringArgument CalculateSteering()
		{
			IMonster monster = base.Behaviour.MonstersToAvoid[0];
			Vector2 lhs = monster.Pos - base.Behaviour.Vehicle.Pos;
			Vector2 right = base.Behaviour.Vehicle.Right;
			float rate = ((!(Vector2.Dot(lhs, right) > 0f)) ? 1f : (-1f));
			return new SteeringArgument(rate, SteeringArgument.SteeringDirection.Forth, braking: false);
		}
	}

	public class FrontVehicleAvoidanceState : State
	{
		public FrontVehicleAvoidanceState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override SteeringArgument CalculateSteering()
		{
			IAutoCar autoCar = base.Behaviour.FrontVehiclesToAvoid[0];
			Vector2 lhs = autoCar.Pos - base.Behaviour.Vehicle.Pos;
			Vector2 right = base.Behaviour.Vehicle.Right;
			float rate = ((!(Vector2.Dot(lhs, right) > 0f)) ? 0.2f : (-0.2f));
			return new SteeringArgument(rate, SteeringArgument.SteeringDirection.Forth, braking: false);
		}
	}

	public class BackVehicleAvoidanceState : State
	{
		public BackVehicleAvoidanceState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override SteeringArgument CalculateSteering()
		{
			IAutoCar autoCar = base.Behaviour.BackVehiclesToAvoid[0];
			Vector2 lhs = autoCar.Pos - base.Behaviour.Vehicle.Pos;
			Vector2 right = base.Behaviour.Vehicle.Right;
			float rate = ((!(Vector2.Dot(lhs, right) > 0f)) ? 0.2f : (-0.2f));
			return new SteeringArgument(rate, SteeringArgument.SteeringDirection.Forth, braking: false);
		}
	}

	public class FrontPropAvoidanceState : State
	{
		public FrontPropAvoidanceState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override SteeringArgument CalculateSteering()
		{
			Vector2 vector = base.Behaviour.FrontPropsToAvoid[0];
			Vector2 lhs = vector - base.Behaviour.Vehicle.Pos;
			Vector2 right = base.Behaviour.Vehicle.Right;
			float rate = ((!(Vector2.Dot(lhs, right) > 0f)) ? 0.2f : (-0.2f));
			return new SteeringArgument(rate, SteeringArgument.SteeringDirection.Forth, braking: false);
		}
	}

	public class BackPropAvoidanceState : State
	{
		public BackPropAvoidanceState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override SteeringArgument CalculateSteering()
		{
			IProp prop = base.Behaviour.BackPropsToAvoid[0];
			Vector2 lhs = prop.Pos - base.Behaviour.Vehicle.Pos;
			Vector2 right = base.Behaviour.Vehicle.Right;
			float rate = ((!(Vector2.Dot(lhs, right) > 0f)) ? 0.5f : (-0.5f));
			return new SteeringArgument(rate, SteeringArgument.SteeringDirection.Forth, braking: false);
		}
	}

	public class PropAcquiringState : State
	{
		public PropAcquiringState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override SteeringArgument CalculateSteering()
		{
			Prop targetPropBox = base.Behaviour.TargetPropBox;
			Vector2 lhs = targetPropBox.Pos - base.Behaviour.Vehicle.Pos;
			Vector2 right = base.Behaviour.Vehicle.Right;
			float rate = ((!(Vector2.Dot(lhs, right) > 0f)) ? (-0.8f) : 0.8f);
			return new SteeringArgument(rate, SteeringArgument.SteeringDirection.Forth, braking: false);
		}
	}

	public class LeaveStickingState : TimedState
	{
		public LeaveStickingState(StateID stateID, AdvancedSteeringBehavior sb)
			: base(stateID, sb)
		{
		}

		public override void ExitAction()
		{
			base.Behaviour.ResetStickingMonitor();
		}

		public override SteeringArgument CalculateSteering()
		{
			return SteeringArgument.Back;
		}
	}

	private const float WayPointSeekDist = 7f;

	private const float WallDetectionFeelerLength = 12f;

	private const int FeelerCount = 3;

	private const float TimeToCollisionThreshold = 2f;

	private const float TimeToWaitAtStartOut1 = 0.5f;

	private const float TimeToWaitAtStartOut2 = 1f;

	private const float CheckConditionsWeakDistanceThreshold = 5f;

	private const float StickingTimeLength = 3f;

	private const float LeaveStickingTimeLength = 4f;

	private static Dictionary<StateID, string> stateToTextMapping = new Dictionary<StateID, string>
	{
		{
			StateID.None,
			"None"
		},
		{
			StateID.Init,
			"Init"
		},
		{
			StateID.StartOut,
			"StOt"
		},
		{
			StateID.StartRun,
			"StRn"
		},
		{
			StateID.PathFollowing,
			"PaFo"
		},
		{
			StateID.PathLookAhead,
			"PaLA"
		},
		{
			StateID.MonsterAvoidance,
			"MnAv"
		},
		{
			StateID.FrontVehicleAvoidance,
			"FtVA"
		},
		{
			StateID.BackVehicleAvoidance,
			"BkVA"
		},
		{
			StateID.PropAcquiring,
			"PrAc"
		}
	};

	private RacingLine racingLine;

	private IObstacle firstTarget;

	private SimpleElapsedTimer startOutTimer;

	private SuperFSM<StateID, AdvancedSteeringBehavior> fsm;

	public IAutoCar Vehicle { get; set; }

	public RaceNode TargetNode { get; set; }

	public bool HasMonsterInMyWay => MonstersToAvoid.Count > 0;

	public bool HasFrontVehicleToAvoid => FrontVehiclesToAvoid.Count > 0;

	public bool HasBackVehicleToAvoid => BackVehiclesToAvoid.Count > 0;

	public bool HasFrontPropToAvoid => FrontPropsToAvoid.Count > 0;

	public bool HasBackPropToAvoid => BackPropsToAvoid.Count > 0;

	public bool NeedPropAcquiring => !SkillManager.Instance.HasProp(Vehicle as Car);

	public bool HasTargetPropBox => null != TargetPropBox;

	public bool IsInSticking => StickingMonitorEnable && StickingMonitorTimer.TimeOutCount > 0;

	public bool StickingMonitorEnable { get; set; }

	public CrudeElapsedTimer StickingMonitorTimer { get; set; }

	private IList<IMonster> MonstersToAvoid { get; set; }

	private IList<IAutoCar> FrontVehiclesToAvoid { get; set; }

	private IList<IAutoCar> BackVehiclesToAvoid { get; set; }

	private IList<Vector2> FrontPropsToAvoid { get; set; }

	private IList<IProp> BackPropsToAvoid { get; set; }

	private Prop TargetPropBox { get; set; }

	private StatedPathLookAhead LookAhead { get; set; }

	public AdvancedSteeringBehavior()
	{
		TargetNode = null;
		MonstersToAvoid = new List<IMonster>();
		FrontVehiclesToAvoid = new List<IAutoCar>();
		BackVehiclesToAvoid = new List<IAutoCar>();
		FrontPropsToAvoid = new List<Vector2>();
		BackPropsToAvoid = new List<IProp>();
		TargetPropBox = null;
		StickingMonitorTimer = new CrudeElapsedTimer(3f);
	}

	public void AttachVehicle(IAutoCar vehicle)
	{
		Vehicle = vehicle;
		LookAhead = new StatedPathLookAhead();
	}

	public void AttachRacingLine(RacingLine racingLine)
	{
		this.racingLine = racingLine;
	}

	public void AttachWalls(IList<Wall> walls)
	{
	}

	public void AttachObstacles(IList<IObstacle> obstacles)
	{
	}

	public void MakeFSM()
	{
		fsm = new SuperFSM<StateID, AdvancedSteeringBehavior>(this);
		SuperFSM<StateID, AdvancedSteeringBehavior>.DelegateTransition trans = new SuperFSM<StateID, AdvancedSteeringBehavior>.DelegateTransition(StateID.LeaveSticking, this, (AdvancedSteeringBehavior b) => b.IsInSticking);
		fsm.CreateState<InitState>(StateID.Init).CreateTransition(StateID.StartRun, (AdvancedSteeringBehavior b) => b.Vehicle.AIProfile.StartTrait == PlayerAIProfile.Trait.T0).CreateTransition(StateID.PathFollowing, (AdvancedSteeringBehavior b) => b.Vehicle.AIProfile.StartTrait == PlayerAIProfile.Trait.T1)
			.CreateTransition(StateID.StartOut, (AdvancedSteeringBehavior b) => true);
		fsm.CreateTimedState<StartRunState>(StateID.StartRun).CreateTimedTransition(0.3f, StateID.PathFollowing).AddTransition(trans);
		fsm.CreateState<StartOutState>(StateID.StartOut).AddTransition(trans).CreateTransition(StateID.PathFollowing, (AdvancedSteeringBehavior b) => b.CheckStartOutDone());
		fsm.CreateState<PathLookAheadState>(StateID.PathLookAhead).AddTransition(trans);
		fsm.CreateState<PathFollowingState>(StateID.PathFollowing).AddTransition(trans).CreateTransition(StateID.MonsterAvoidance, (AdvancedSteeringBehavior b) => b.HasMonsterInMyWay)
			.CreateTransition(StateID.FrontVehicleAvoidance, (AdvancedSteeringBehavior b) => b.HasFrontVehicleToAvoid)
			.CreateTransition(StateID.FrontPropAvoidance, (AdvancedSteeringBehavior b) => b.HasFrontPropToAvoid)
			.CreateTransition(StateID.PropAcquiring, (AdvancedSteeringBehavior b) => b.HasTargetPropBox);
		fsm.CreateState<MonsterAvoidanceState>(StateID.MonsterAvoidance).AddTransition(trans).CreateTransition(StateID.PathFollowing, (AdvancedSteeringBehavior b) => !b.HasMonsterInMyWay);
		fsm.CreateState<FrontVehicleAvoidanceState>(StateID.FrontVehicleAvoidance).AddTransition(trans).CreateTransition(StateID.PathFollowing, (AdvancedSteeringBehavior b) => !b.HasFrontVehicleToAvoid);
		fsm.CreateState<FrontPropAvoidanceState>(StateID.FrontPropAvoidance).AddTransition(trans).CreateTransition(StateID.PathFollowing, (AdvancedSteeringBehavior b) => !b.HasFrontPropToAvoid);
		fsm.CreateState<PropAcquiringState>(StateID.PropAcquiring).AddTransition(trans).CreateTransition(StateID.PathFollowing, (AdvancedSteeringBehavior b) => !b.HasTargetPropBox);
		fsm.CreateTimedState<LeaveStickingState>(StateID.LeaveSticking).CreateTimedTransition(4f, StateID.PathFollowing).CreateTransition(StateID.PathFollowing, (AdvancedSteeringBehavior b) => !b.StickingMonitorEnable);
	}

	public void OnDrawGizmos()
	{
		Color color = Gizmos.color;
		if (SystemConfig.Instance.GizmosVehicleSteeringState)
		{
			GizmosEx.DrawText(Vehicle.Position, 1, Vehicle.AIProfile.PathTrait.ToString() + " " + Vehicle.AIProfile.CollisionTrait.ToString() + " " + stateToTextMapping[fsm.CurrentStateID]);
		}
		if (SystemConfig.Instance.GizmosPathFollowing)
		{
			Vector3 position = Vehicle.Position;
			if (TargetNode != null)
			{
				Gizmos.color = Color.magenta;
				Vector3 to = new Vector3(TargetNode.Pos.x, 15f, TargetNode.Pos.y);
				Gizmos.DrawLine(position, to);
			}
			Gizmos.color = Color.yellow;
			GizmosEx.DrawCircle(position, 7f);
		}
		if (SystemConfig.Instance.GizmosWallAvoidance)
		{
		}
		if (SystemConfig.Instance.GizmosCollisionAvoidance && firstTarget != null)
		{
			Gizmos.color = Color.magenta;
			GizmosEx.DrawLine(Vehicle.Position, firstTarget.Pos);
		}
		if (SystemConfig.Instance.GizmosMonsterAvoidance)
		{
			foreach (IMonster item in MonstersToAvoid)
			{
				Gizmos.color = Color.blue;
				GizmosEx.DrawLine(Vehicle.Position, item.Pos);
			}
		}
		if (SystemConfig.Instance.GizmosFrontVehicleAvoidance)
		{
			foreach (IAutoCar item2 in FrontVehiclesToAvoid)
			{
				Gizmos.color = Color.yellow;
				GizmosEx.DrawLine(Vehicle.Position, item2.Pos);
			}
		}
		if (SystemConfig.Instance.GizmosBackVehicleAvoidance)
		{
			foreach (IAutoCar item3 in BackVehiclesToAvoid)
			{
				Gizmos.color = Color.black;
				GizmosEx.DrawLine(Vehicle.Position, item3.Pos);
			}
		}
		Gizmos.color = Color.yellow;
		GizmosEx.DrawRect(Vehicle.Pos, 5f);
		if (SystemConfig.Instance.GizmosPropAcquiring && null != TargetPropBox)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(Vehicle.Position, TargetPropBox.Position);
		}
		LookAhead.DrawGizmos();
		if (LookAhead.GetEdgeByLookAheadIndex(0, out var edge))
		{
			Gizmos.color = Color.cyan;
			GizmosEx.DrawLine(edge.From.Pos, edge.To.Pos);
		}
		Gizmos.color = color;
	}

	public void OnDrawGizmosSelected()
	{
		Color color = Gizmos.color;
		LookAhead.DrawGizmos();
		Gizmos.color = color;
	}

	public virtual SteeringArgument CalculateSteering()
	{
		if (fsm != null)
		{
			ISteeringState steeringState = fsm.CurrentState as ISteeringState;
			return steeringState.CalculateSteering();
		}
		return SteeringArgument.Default;
	}

	public void UpdateState(bool doit)
	{
		CheckConditions(doit);
		if (fsm != null)
		{
			fsm.Update(doStateActionIfNotTriggered: true);
		}
	}

	public SteeringArgument Force2Steering(Vector2 direction)
	{
		float magnitude = Vehicle.Vel.magnitude;
		float maxSteeringAngle = Vehicle.GetMaxSteeringAngle(magnitude);
		if (maxSteeringAngle < float.Epsilon)
		{
			return SteeringArgument.Default;
		}
		Vector2 heading = Vehicle.Heading;
		Vector2 lhs = Vector2Ex.RotateRight(heading);
		SteeringArgument.SteeringDirection direction2 = ((!(Vector2.Dot(direction, heading) > 0f)) ? SteeringArgument.SteeringDirection.Back : SteeringArgument.SteeringDirection.Forth);
		float num = Vector2.Dot(lhs, direction.normalized);
		if (Mathf.Abs(num) < 0.5f)
		{
			num = num * num * num;
		}
		num = Mathf.Clamp(num, -1f, 1f);
		if (num > 1f || num < -1f)
		{
			Debug.LogError("AdvancedSteeringBehavior.Force2Steering: innerProduct out of range: " + num);
		}
		float num2 = (float)Math.PI / 2f - Mathf.Acos(num);
		if (float.IsNaN(num2))
		{
			Debug.LogError("AdvancedSteeringBehavior.Force2Steering: innerProduct: " + num + ", direction: " + direction.ToString() + ", right: " + lhs.ToString() + ", angle is NaN");
		}
		float num3 = Mathf.Clamp(num2 / maxSteeringAngle, -1f, 1f);
		if (float.IsNaN(num3))
		{
			Debug.LogError("AdvancedSteeringBehavior.Force2Steering: maxSteeringAngle: " + maxSteeringAngle + ", rate is NaN");
		}
		return new SteeringArgument(num3, direction2, braking: false);
	}

	public Vector2 PathFollowingForce()
	{
		if (TargetNode == null)
		{
			return Vector2.zero;
		}
		return SeekForce(TargetNode.Pos);
	}

	public SteeringArgument CalculateSteeringPathFollowing()
	{
		if (!LookAhead.Available)
		{
			return SteeringArgument.Default;
		}
		float num = 0f;
		float signedDegreeToEdge = LookAhead.GetSignedDegreeToEdge(Vehicle.Heading);
		if (MathfEx.InRange(signedDegreeToEdge, 10f))
		{
			float signedDistanceFromPointToEdge = LookAhead.GetSignedDistanceFromPointToEdge(Vehicle.Pos);
			num = MathfEx.SimpleReverse(signedDistanceFromPointToEdge, 2f);
		}
		else
		{
			num = MathfEx.SimpleReverse(signedDegreeToEdge, 10f);
		}
		return new SteeringArgument(num, SteeringArgument.SteeringDirection.Forth, braking: false);
	}

	public SteeringArgument CalculateSteeringPathLookAhead()
	{
		if (!LookAhead.Available)
		{
			return SteeringArgument.Default;
		}
		float num = 0f;
		float signedDegreeToEdge = LookAhead.GetSignedDegreeToEdge(Vehicle.Heading);
		if (MathfEx.InRange(signedDegreeToEdge, 10f))
		{
			float signedDistanceFromPointToEdge = LookAhead.GetSignedDistanceFromPointToEdge(Vehicle.Pos);
			num = MathfEx.SimpleReverse(signedDistanceFromPointToEdge, 2f);
		}
		else
		{
			num = MathfEx.SimpleReverse(signedDegreeToEdge, 10f);
		}
		return new SteeringArgument(num, SteeringArgument.SteeringDirection.Forth, braking: false);
	}

	public void UpdatePathFollowing()
	{
		if (racingLine != null)
		{
			RaceNode raceNode = racingLine.QueryClosestNode(Vehicle.Pos);
			if (TargetNode == null || !racingLine.IsFromToNodePair(raceNode.Index, TargetNode.Index))
			{
				int max = racingLine.NextNodeCount(raceNode.Index);
				int nth = UnityEngine.Random.Range(0, max);
				TargetNode = racingLine.NextNodeOfNode(raceNode.Index, nth);
				LookAhead.Reset(TargetNode.Index);
			}
		}
	}

	public void UpdatePathLookAhead()
	{
		if (racingLine != null)
		{
			LookAhead.Update(Vehicle.Pos);
		}
	}

	public bool CheckStartOutDone()
	{
		return startOutTimer == null || startOutTimer.TimeOut;
	}

	public void InitStartOut()
	{
		switch (Vehicle.AIProfile.StartTrait)
		{
		case PlayerAIProfile.Trait.T0:
			break;
		case PlayerAIProfile.Trait.T1:
			break;
		case PlayerAIProfile.Trait.T2:
			startOutTimer = new SimpleElapsedTimer(0.5f);
			startOutTimer.Reset();
			break;
		case PlayerAIProfile.Trait.T3:
			startOutTimer = new SimpleElapsedTimer(1f);
			startOutTimer.Reset();
			break;
		default:
			Debug.LogError("Unknown Start Trait: " + Vehicle.AIProfile.StartTrait);
			break;
		}
	}

	public void UpdateStartOut()
	{
		startOutTimer.Update();
	}

	public void UninitStartOut()
	{
		startOutTimer = null;
	}

	public void ResetStickingMonitor()
	{
		StickingMonitorTimer.Reset();
		StickingMonitorEnable = false;
	}

	private static bool InTheWayOf(IAutoCar front, IAutoCar back)
	{
		Vector2 vector = front.Pos - back.Pos;
		float magnitude = vector.magnitude;
		vector.Normalize();
		if (Vector2Ex.IsNaN(vector))
		{
			return false;
		}
		Vector2 heading = back.Heading;
		float num = Vector2.Dot(vector, heading);
		if (num < 0.8f)
		{
			return false;
		}
		float magnitude2 = (back.Vel - front.Vel).magnitude;
		return magnitude / magnitude2 < 1f;
	}

	private void CheckConditions(bool doit)
	{
		if (fsm.CurrentStateID != StateID.LeaveSticking && StickingMonitorEnable)
		{
			StickingMonitorTimer.Advance(Time.deltaTime);
			if (StickingMonitorTimer.TimeOutCount > 0)
			{
				return;
			}
		}
		if (!doit)
		{
			return;
		}
		Vector2 pos = Vehicle.Pos;
		MonstersToAvoid.Clear();
		switch (Vehicle.AIProfile.PathTrait)
		{
		case PlayerAIProfile.Trait.T0:
		case PlayerAIProfile.Trait.T1:
			foreach (IMonster item in RaceWorld.Instance.MonstersWeakNear(pos, 5f))
			{
				if (item.ShouldBeAvoided && MonsterIsInMyWay(item))
				{
					MonstersToAvoid.Add(item);
				}
			}
			break;
		}
		FrontVehiclesToAvoid.Clear();
		BackVehiclesToAvoid.Clear();
		switch (Vehicle.AIProfile.CollisionTrait)
		{
		case PlayerAIProfile.Trait.T0:
			if (Vehicle.IsInPowerStatus)
			{
				break;
			}
			foreach (IAutoCar item2 in RaceWorld.Instance.VehiclesWeakNear(pos, 5f))
			{
				if (FrontVehicleShouldBeAvoided(item2))
				{
					FrontVehiclesToAvoid.Add(item2);
				}
				if (BackVehicleShouldBeAvoided(item2))
				{
					BackVehiclesToAvoid.Add(item2);
				}
			}
			break;
		case PlayerAIProfile.Trait.T1:
			foreach (IAutoCar item3 in RaceWorld.Instance.VehiclesWeakNear(pos, 5f))
			{
				if (Vehicle.Mass <= item3.Mass)
				{
					if (InTheWayOf(item3, Vehicle))
					{
						FrontVehiclesToAvoid.Add(item3);
					}
					if (InTheWayOf(Vehicle, item3))
					{
						BackVehiclesToAvoid.Add(item3);
					}
				}
			}
			break;
		case PlayerAIProfile.Trait.T2:
			foreach (IAutoCar item4 in RaceWorld.Instance.VehiclesWeakNear(pos, 5f))
			{
				if (InTheWayOf(item4, Vehicle))
				{
					FrontVehiclesToAvoid.Add(item4);
				}
				if (InTheWayOf(Vehicle, item4))
				{
					BackVehiclesToAvoid.Add(item4);
				}
			}
			break;
		}
		FrontPropsToAvoid.Clear();
		BackPropsToAvoid.Clear();
		if (!Vehicle.IsInPowerStatus)
		{
			switch (Vehicle.AIProfile.CollisionTrait)
			{
			case PlayerAIProfile.Trait.T0:
			case PlayerAIProfile.Trait.T1:
				foreach (IProp item5 in RaceWorld.Instance.PropsWeakNear(pos, 5f))
				{
					if (item5 != null && PropIsInMyWay(item5))
					{
						FrontPropsToAvoid.Add(item5.Pos);
					}
				}
				break;
			}
		}
		Prop prop = null;
		if (NeedPropAcquiring)
		{
			if (TargetPropBox == null || TargetPropBox.IsPicked || !PropBoxIsInMyWay(TargetPropBox))
			{
				foreach (Prop item6 in TSingleton<PropManager>.Instance.PropBoxesWeakNear(pos, 5f))
				{
					if (!item6.IsPicked && PropBoxIsInMyWay(item6) && (item6.VehicleLockCount == 0 || UnityEngine.Random.Range(0f, 1f) < 0.4f))
					{
						prop = item6;
						break;
					}
				}
			}
			else
			{
				prop = TargetPropBox;
			}
		}
		if (TargetPropBox != prop)
		{
			if (TargetPropBox != null)
			{
				TargetPropBox.DecVehicleLockCount();
			}
			if (prop != null)
			{
				prop.IncVehicleLockCount();
			}
			TargetPropBox = prop;
		}
	}

	private bool MonsterIsInMyWay(IMonster monster)
	{
		Vector2 vector = monster.Pos - Vehicle.Pos;
		float magnitude = vector.magnitude;
		vector.Normalize();
		if (Vector2Ex.IsNaN(vector))
		{
			return false;
		}
		Vector2 heading = Vehicle.Heading;
		float num = Vector2.Dot(vector, heading);
		if (num < 0.7071f)
		{
			return false;
		}
		float magnitude2 = (Vehicle.Vel - monster.Vel).magnitude;
		return magnitude / magnitude2 < 2f;
	}

	private bool PropBoxIsInMyWay(Prop propBox)
	{
		Vector2 vector = propBox.Pos - Vehicle.Pos;
		float magnitude = vector.magnitude;
		vector.Normalize();
		if (Vector2Ex.IsNaN(vector))
		{
			return false;
		}
		Vector2 heading = Vehicle.Heading;
		float num = Vector2.Dot(vector, heading);
		if (num < 0.7f)
		{
			return false;
		}
		Vector3 position = propBox.Position;
		float num2 = HeightMapManager.Instance.Height(position.x, position.z);
		float num3 = position.y - num2;
		if (num3 > 2f)
		{
			return false;
		}
		float magnitude2 = Vehicle.Vel.magnitude;
		return magnitude / magnitude2 < 1.5f;
	}

	private bool PropIsInMyWay(IProp prop)
	{
		Vector2 vector = prop.Pos - Vehicle.Pos;
		float magnitude = vector.magnitude;
		vector.Normalize();
		if (Vector2Ex.IsNaN(vector))
		{
			return false;
		}
		Vector2 heading = Vehicle.Heading;
		float num = Vector2.Dot(vector, heading);
		if (num < 0.7f)
		{
			return false;
		}
		float magnitude2 = Vehicle.Vel.magnitude;
		return magnitude / magnitude2 < 1.5f;
	}

	private bool FrontVehicleShouldBeAvoided(IAutoCar vehicle)
	{
		if (!InTheWayOf(vehicle, Vehicle))
		{
			return false;
		}
		if (TSingleton<PropManager>.Instance.Equiped(vehicle.Name) != ePropType.NONE)
		{
			return true;
		}
		if (TSingleton<PropManager>.Instance.Equiped(Vehicle.Name) == ePropType.THUNDER_CLOUD)
		{
			return false;
		}
		return Vehicle.Mass <= vehicle.Mass;
	}

	private bool BackVehicleShouldBeAvoided(IAutoCar vehicle)
	{
		if (!InTheWayOf(Vehicle, vehicle))
		{
			return false;
		}
		if (TSingleton<PropManager>.Instance.Equiped(vehicle.Name) != ePropType.NONE)
		{
			return true;
		}
		if (TSingleton<PropManager>.Instance.Equiped(Vehicle.Name) == ePropType.THUNDER_CLOUD)
		{
			return false;
		}
		return Vehicle.Mass <= vehicle.Mass;
	}

	private Vector2 SeekForce(Vector2 target)
	{
		return (target - Vehicle.Pos).normalized;
	}
}
