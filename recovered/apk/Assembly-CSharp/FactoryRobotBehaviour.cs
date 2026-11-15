using System;
using GameNetwork;
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
			: base(stateID, behaviour)
		{
		}

		public override void Action()
		{
			if (base.Behaviour.RenderEnabled || !NetworkManager.Instance.IsStandAlone)
			{
				base.Behaviour.UpdatePatrolMovement();
			}
		}
	}

	public class LockState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public LockState(StateID stateID, FactoryRobotBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.SaveBeginningRotation();
		}

		public override void Action()
		{
			base.Behaviour.UpdateLockHeading(base.Timer);
		}
	}

	public class PursueState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public PursueState(StateID stateID, FactoryRobotBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void Action()
		{
			base.Behaviour.UpdatePursueMovement();
		}
	}

	public class AttackState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public AttackState(StateID stateID, FactoryRobotBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			Car car = base.Behaviour.CheckAndHitCar(delegate(Car v)
			{
				v.HitByRobb((base.Behaviour.Pos - v.Pos).normalized);
			});
			if (null != car)
			{
				base.Behaviour.LookAt(car.Pos);
			}
			base.Behaviour.Vel = Vector2.zero;
		}

		public override void ExitAction()
		{
			base.Behaviour.pursueTargetCar = null;
		}
	}

	public class RestState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public RestState(StateID stateID, FactoryRobotBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.Vel = Vector2.zero;
		}
	}

	public class FallState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public FallState(StateID stateID, FactoryRobotBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.KnockedOutProcessed = true;
			base.Behaviour.Vel = Vector2.zero;
			base.Behaviour.ColliderControlBeKnockedOut();
		}

		public override void ExitAction()
		{
			base.Behaviour.KnockedOut = false;
			base.Behaviour.KnockedOutProcessed = false;
			base.Behaviour.ColliderControlStandUp();
		}
	}

	public class ReturnState : SuperFSM<StateID, FactoryRobotBehaviour>.State
	{
		public ReturnState(StateID stateID, FactoryRobotBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void Action()
		{
			base.Behaviour.UpdateReturnMovement();
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

	public float PatrolRadius = 10f;

	public float GuardRadius = 25f;

	public float ReachRadius = 20f;

	private Vector2 cachedHomePos;

	private bool hasCachedHomePos;

	private IAutoCar pursueTargetCar;

	private SuperFSM<StateID, FactoryRobotBehaviour> fsm;

	public override bool ShouldBeAvoided => false;

	public override Transform GrabTransform => base.transform;

	public override GameObject GrabGameObject => base.gameObject;

	public Vector2 Home
	{
		get
		{
			if (!hasCachedHomePos)
			{
				InitHomePos();
			}
			return cachedHomePos;
		}
	}

	public IAutoCar PursueTargetCar => pursueTargetCar;

	public FactoryRobotBehaviour()
		: base(3f)
	{
	}

	public void PlayLockOnEffect()
	{
		PlayEffect(eEffectType.Ghost_Lock, 0f, 6f, 0f, 1f);
	}

	public void PlayAttackEffect()
	{
		PlayEffect(eEffectType.GeGeLa_Attack, 0f, 0f, 0.5f, 1.5f);
	}

	public void PlayKnockedOutEffect()
	{
		PlayEffect(eEffectType.HIT_BY_MISSILE, 0f, 0.4f, 0f, 0.5f);
	}

	public void UpdatePatrolMovement()
	{
		base.Vel = CalculateVelocityWander();
		Move(Time.deltaTime);
		LookAt(Pos + Vel);
	}

	public void UpdateLockHeading(CrudeElapsedTimer timer)
	{
		LookRotation(pursueTargetCar.Pos, timer.SaturatedElapsedRate);
	}

	public void UpdatePursueMovement()
	{
		base.Vel = CalculateVelocityPursue();
		Move(Time.deltaTime);
		LookAt(pursueTargetCar.Pos);
	}

	public void UpdateReturnMovement()
	{
		Vector2 vector = Home - Pos;
		if (vector.sqrMagnitude < float.Epsilon)
		{
			base.Vel = Vector2.zero;
			return;
		}
		vector.Normalize();
		vector *= 11f;
		base.Vel = vector;
		Move(Time.deltaTime);
		LookAt(Home);
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
		switch ((StateID)stateId)
		{
		case StateID.None:
		case StateID.Init:
			break;
		case StateID.Patral:
			PlayAnimation("walk");
			break;
		case StateID.Lock:
			PlayLockOnEffect();
			PlayAnimation("transformation");
			PlaySound(20904, 1);
			break;
		case StateID.Pursue:
			PlayAnimation("walk01");
			break;
		case StateID.Attack:
			PlayAnimation("attack");
			PlayAttackEffect();
			PlaySound(20901, 1);
			break;
		case StateID.Rest:
			PlayAnimation("idle");
			break;
		case StateID.Fall:
			PlayAnimation("knock");
			PlayKnockedOutEffect();
			break;
		case StateID.Return:
			PlayAnimation("walk");
			break;
		}
	}

	public bool E1()
	{
		IAutoCar vehicleNotDeadInRange = RaceWorld.Instance.GetVehicleNotDeadInRange(Pos, MathfEx.Square(GuardRadius));
		if (vehicleNotDeadInRange == null)
		{
			return false;
		}
		pursueTargetCar = vehicleNotDeadInRange;
		return true;
	}

	public bool E3()
	{
		return (Pos - pursueTargetCar.Pos).sqrMagnitude > MathfEx.Square(GuardRadius) || pursueTargetCar.IsDead;
	}

	public bool E4()
	{
		return (Pos - Home).sqrMagnitude <= MathfEx.Square(PatrolRadius);
	}

	public bool E5()
	{
		return !E4();
	}

	public bool E6()
	{
		return (Pos - Home).sqrMagnitude > MathfEx.Square(ReachRadius);
	}

	public bool E7()
	{
		return HasVehicleToHit();
	}

	public bool E8()
	{
		return KnockedOut && !base.KnockedOutProcessed;
	}

	public bool E12()
	{
		return (Pos - Home).sqrMagnitude < MathfEx.Square(1f);
	}

	public override bool CanHitVehicle(Car vehicle)
	{
		float num = Vector3Ex.SqrDistance(Position, vehicle.Position);
		return num < MathfEx.Square(2.2f);
	}

	protected override void InitEntity()
	{
		InitHomePos();
		MakeFSM();
		fsm.Update(doStateActionIfNotTriggered: false);
	}

	protected override void UpdateEntity()
	{
		UpdateCarTimerManager();
		fsm.Update(doStateActionIfNotTriggered: true);
		base.UpdateEntity();
	}

	protected void OnDrawGizmos()
	{
		if (SystemConfig.Instance.GizmosMonsterName)
		{
			GizmosEx.DrawText(Position, base.transform.parent.name);
		}
		Color color = Gizmos.color;
		Vector3 vector = new Vector3(Home.x, Position.y, Home.y);
		Gizmos.color = Color.cyan;
		GizmosEx.DrawCircle(vector, PatrolRadius);
		Gizmos.color = Color.magenta;
		GizmosEx.DrawCircle(vector, ReachRadius);
		Gizmos.color = Color.yellow;
		GizmosEx.DrawCircle(Position, GuardRadius);
		Gizmos.color = Color.white;
		Gizmos.DrawLine(Position, vector);
		Gizmos.color = color;
	}

	protected void MakeFSM()
	{
		fsm = new SuperFSM<StateID, FactoryRobotBehaviour>(this);
		fsm.CreateState(StateID.Init).CreateTransition(StateID.Patral, (FactoryRobotBehaviour b) => true);
		fsm.CreateState<PatrolState>(StateID.Patral).CreateTransition(StateID.Fall, (FactoryRobotBehaviour x) => x.E8()).CreateTransition(StateID.Attack, (FactoryRobotBehaviour x) => x.E7())
			.CreateTransition(StateID.Lock, (FactoryRobotBehaviour x) => x.E1());
		fsm.CreateState<LockState>(StateID.Lock).CreateTransition(StateID.Fall, (FactoryRobotBehaviour x) => x.E8()).CreateTransition(StateID.Attack, (FactoryRobotBehaviour x) => x.E7())
			.CreateTimedTransition(0.5f, StateID.Pursue);
		fsm.CreateState<PursueState>(StateID.Pursue).CreateTransition(StateID.Fall, (FactoryRobotBehaviour x) => x.E8()).CreateTransition(StateID.Attack, (FactoryRobotBehaviour x) => x.E7())
			.CreateTransition(StateID.Patral, (FactoryRobotBehaviour x) => x.E3() && x.E4())
			.CreateTransition(StateID.Return, (FactoryRobotBehaviour x) => (x.E3() && x.E5()) || x.E6());
		fsm.CreateState<AttackState>(StateID.Attack).CreateTransition(StateID.Fall, (FactoryRobotBehaviour x) => x.E8()).CreateTransition(StateID.Attack, (FactoryRobotBehaviour x) => x.E7())
			.CreateTimedTransition(AnimationLength("attack"), StateID.Rest);
		fsm.CreateState<RestState>(StateID.Rest).CreateTransition(StateID.Fall, (FactoryRobotBehaviour x) => x.E8()).CreateTransition(StateID.Attack, (FactoryRobotBehaviour x) => x.E7())
			.CreateTimedTransition(5f, StateID.Patral);
		fsm.CreateState<FallState>(StateID.Fall).CreateTransition(StateID.Fall, (FactoryRobotBehaviour x) => x.E8()).CreateTimer(AnimationLength("knock"))
			.CreateTimedTransition(StateID.Patral, (FactoryRobotBehaviour b, CrudeElapsedTimer t) => t.TimeOutCount > 0 && b.E4())
			.CreateTimedTransition(StateID.Return, (FactoryRobotBehaviour b, CrudeElapsedTimer t) => t.TimeOutCount > 0 && b.E5());
		fsm.CreateState<ReturnState>(StateID.Return).CreateTransition(StateID.Fall, (FactoryRobotBehaviour x) => x.E8()).CreateTransition(StateID.Attack, (FactoryRobotBehaviour x) => x.E7())
			.CreateTransition(StateID.Patral, (FactoryRobotBehaviour x) => x.E4());
		if (!NetworkManager.Instance.IsClient)
		{
			fsm.OnTransition = OnStateInfo;
		}
	}

	private void OnStateInfo(StateID stateId)
	{
		if (NetworkManager.Instance.IsServer)
		{
			ServerSendStateInfo((int)stateId);
		}
		ClientOnReceiveStateInfo((int)stateId);
	}

	private Vector2 CalculateVelocityWander()
	{
		float angle = 8f * MathfEx.RandomBinomial() * ((float)Math.PI / 180f);
		Vector2 vector = Vector2Ex.Rotate(base.Heading, angle) * 6f;
		Vector2 vector2 = Pos + vector * Time.deltaTime;
		if (Vector2Ex.SqrDistance(vector2, Home) > MathfEx.Square(PatrolRadius))
		{
			vector = (Home - Pos).normalized * 6f;
		}
		return vector;
	}

	private Vector2 CalculateVelocityPursue()
	{
		Vector2 vector = PursueTargetCar.Pos - Pos;
		return (!(vector.sqrMagnitude < MathfEx.Square(0.5f))) ? (vector.normalized * 11f) : Vector2.zero;
	}

	private void InitHomePos()
	{
		cachedHomePos = Pos;
		hasCachedHomePos = true;
	}
}
