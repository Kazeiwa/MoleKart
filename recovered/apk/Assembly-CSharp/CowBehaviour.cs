using GameNetwork;
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
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.Vel = Vector2.zero;
		}

		public override void Action()
		{
			base.Behaviour.ProcessVehicleCollision();
		}
	}

	public class WalkState : SuperFSM<StateID, CowBehaviour>.State
	{
		public WalkState(StateID stateID, CowBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.SetTargetPos();
		}

		public override void Action()
		{
			if (base.Behaviour.RenderEnabled || !NetworkManager.Instance.IsStandAlone)
			{
				base.Behaviour.UpdateWalk();
				base.Behaviour.ProcessVehicleCollision();
			}
		}
	}

	public class KnockedOutState : SuperFSM<StateID, CowBehaviour>.State
	{
		public KnockedOutState(StateID stateID, CowBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.Vel = Vector2.zero;
			base.Behaviour.ColliderControlBeKnockedOut();
		}

		public override void ExitAction()
		{
			base.Behaviour.KnockedOut = false;
			base.Behaviour.ColliderControlStandUp();
		}
	}

	public const float CarTimerLimit = 2f;

	public const float BackFlipSpeedThreshold = 15f;

	private const int AvailableTargetCount = 8;

	private const float IdleTimeLength = 3f;

	private const float SqareArriveTargetThreshold = 0.5f;

	private const float hitVehicleDistanceThreshold = 2f;

	public float WalkRadius = 20f;

	public float WalkSpeed = 3f;

	public Vector2 Home;

	private SuperFSM<StateID, CowBehaviour> fsm;

	public override Transform GrabTransform => base.transform;

	public override GameObject GrabGameObject => base.gameObject;

	public Vector2 Target { get; set; }

	public int TargetId { get; set; }

	public CowBehaviour()
		: base(2f)
	{
		TargetId = 0;
	}

	public void SendCommand(CommandID commandId)
	{
		if (NetworkManager.Instance.IsServer)
		{
			ServerSendCommand((int)commandId);
		}
		ClientOnReceiveCommand((int)commandId);
	}

	public virtual void SetTargetPos()
	{
		TargetId++;
		Target = Home + Vector2Ex.FromDegree(45f * (float)TargetId, WalkRadius);
	}

	public bool IsArrivedAtTargetPos()
	{
		return Vector2Ex.SqrDistance(Pos, Target) < 0.5f;
	}

	public override bool CanHitVehicle(Car vehicle)
	{
		return Vector3.Distance(vehicle.Position, Position) < base.Radius + vehicle.Radius && (!NetworkManager.Instance.IsStandAlone || vehicle.Vel.magnitude > 15f);
	}

	public void ProcessVehicleCollision()
	{
		Car car = CheckAndHitCar(delegate(Car v)
		{
			v.BackFlip();
		});
		if (car != null)
		{
			SendCommand(CommandID.CarHit);
		}
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
		switch ((StateID)stateId)
		{
		case StateID.None:
		case StateID.Init:
			break;
		case StateID.Idle:
			PlayAnimation("idle");
			break;
		case StateID.Walk:
			PlayAnimation("walk");
			break;
		case StateID.KnockedOut:
			PlayAnimation("knock");
			break;
		}
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
		if (commandId == 0)
		{
			PlaySound(20202, 1);
		}
	}

	protected override void InitEntity()
	{
		MakeFSM();
		fsm.Update(doStateActionIfNotTriggered: false);
	}

	protected override void UpdateEntity()
	{
		UpdateCarTimerManager();
		fsm.Update(doStateActionIfNotTriggered: true);
		base.UpdateEntity();
	}

	protected virtual void OnDrawGizmos()
	{
		Color color = Gizmos.color;
		Gizmos.color = Color.cyan;
		GizmosEx.DrawLine(Position, Home);
		Vector3 center = Vector2Ex.To3D(Home, Position.y);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(center, 1.5f);
		Gizmos.color = Color.yellow;
		GizmosEx.DrawCircle(center, WalkRadius);
		Gizmos.color = Color.magenta;
		GizmosEx.DrawCircle(Position, base.Radius);
		Gizmos.color = color;
	}

	protected void MakeFSM()
	{
		fsm = new SuperFSM<StateID, CowBehaviour>(this);
		fsm.CreateState(StateID.Init).CreateTransition(StateID.Idle, (CowBehaviour b) => true);
		fsm.CreateTimedState<IdleState>(StateID.Idle).CreateTransition(StateID.KnockedOut, (CowBehaviour b) => b.KnockedOut).CreateTimedTransition(3f, StateID.Walk);
		fsm.CreateState<WalkState>(StateID.Walk).CreateTransition(StateID.KnockedOut, (CowBehaviour b) => b.KnockedOut).CreateTransition(StateID.Idle, (CowBehaviour b) => b.IsArrivedAtTargetPos());
		fsm.CreateTimedState<KnockedOutState>(StateID.KnockedOut).CreateTransition(StateID.KnockedOut, (CowBehaviour b) => b.KnockedOut).CreateTimedTransition(AnimationLength("knock"), StateID.Walk);
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

	protected virtual void UpdateWalk()
	{
		HeadForPos(WalkSpeed, Time.deltaTime, Target);
	}
}
