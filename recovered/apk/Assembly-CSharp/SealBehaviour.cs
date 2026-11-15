using GameNetwork;
using Misc;
using UnityEngine;

public class SealBehaviour : ColliderMonsterBehaviour
{
	public enum StateID
	{
		None,
		Init,
		TurnAround,
		Walk,
		KnockedOut
	}

	public enum CommandID
	{
		CarHit
	}

	public class KnockedOutBeginTransition : SuperFSM<StateID, SealBehaviour>.Transition
	{
		public KnockedOutBeginTransition(StateID destStateID, SealBehaviour behaviour)
			: base(destStateID, behaviour)
		{
		}

		public override bool IsTriggered()
		{
			return base.Behaviour.KnockedOut && !base.Behaviour.KnockedOutProcessed;
		}
	}

	public class MoveState : SuperFSM<StateID, SealBehaviour>.State
	{
		public MoveState(StateID stateID, SealBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void Action()
		{
			base.Behaviour.ProcessVehicleCollision();
		}
	}

	public class WalkState : SuperFSM<StateID, SealBehaviour>.State
	{
		public WalkState(StateID stateID, SealBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.UpdateTargetPointIndex();
		}

		public override void Action()
		{
			if (base.Behaviour.RenderEnabled || !NetworkManager.Instance.IsStandAlone)
			{
				base.Behaviour.UpdateWalk(Time.deltaTime);
				base.Behaviour.ProcessVehicleCollision();
			}
		}
	}

	public class KnockedOutState : SuperFSM<StateID, SealBehaviour>.State
	{
		public KnockedOutState(StateID stateID, SealBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.KnockedOutProcessed = true;
			base.Behaviour.ColliderControlBeKnockedOut();
		}

		public override void ExitAction()
		{
			base.Behaviour.KnockedOut = false;
			base.Behaviour.KnockedOutProcessed = false;
			base.Behaviour.ColliderControlStandUp();
		}
	}

	public const float CarTimerLimit = 2f;

	public const float WalkSpeed = 2f;

	public const float IdleTimeLength = 1f;

	private const float SqareArriveThreshold = 1f;

	private const float SqareVehicleCollisionThreshold = 4f;

	public const float BackFlipSpeedThreshold = 20f;

	public GameObject[] Points = new GameObject[2];

	private Vector2 home;

	private SuperFSM<StateID, SealBehaviour> fsm;

	public override Transform GrabTransform => base.transform;

	public override GameObject GrabGameObject => base.gameObject;

	public int TargetPointIndex { get; set; }

	public Vector2 TargetPos => Vector3Ex.To2D(Points[TargetPointIndex].transform.position);

	public SealBehaviour()
		: base(2f)
	{
		TargetPointIndex = 0;
	}

	public void SendCommand(CommandID commandId)
	{
		if (NetworkManager.Instance.IsServer)
		{
			ServerSendCommand((int)commandId);
		}
		ClientOnReceiveCommand((int)commandId);
	}

	public override bool CanHitVehicle(Car vehicle)
	{
		return Vector3Ex.SqrDistance(vehicle.Position, Position) < 4f && (!NetworkManager.Instance.IsStandAlone || vehicle.Vel.magnitude > 20f);
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

	public void UpdateTargetPointIndex()
	{
		TargetPointIndex = (TargetPointIndex + 1) % Points.Length;
	}

	public bool ArrivedAtTargetPoint()
	{
		return Vector2Ex.SqrDistance(Pos, TargetPos) < 1f;
	}

	public void UpdateWalk(float deltaTime)
	{
		HeadForPos(2f, deltaTime, TargetPos);
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
		switch ((StateID)stateId)
		{
		case StateID.None:
		case StateID.Init:
			break;
		case StateID.TurnAround:
			PlayAnimation("iseal_move");
			break;
		case StateID.Walk:
			PlayAnimation("seal_walk");
			break;
		case StateID.KnockedOut:
			PlayAnimation("seal_knock");
			break;
		}
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
		if (commandId == 0)
		{
			PlaySound(20602, 1);
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

	protected void OnDrawGizmos()
	{
		Color color = Gizmos.color;
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(Position, Points[0].transform.position);
		for (int i = 0; i < Points.Length - 1; i++)
		{
			Gizmos.DrawLine(Points[i].transform.position, Points[(i + 1) % Points.Length].transform.position);
		}
		Gizmos.color = color;
	}

	protected void MakeFSM()
	{
		fsm = new SuperFSM<StateID, SealBehaviour>(this);
		fsm.CreateState(StateID.Init).CreateTransition(StateID.Walk, (SealBehaviour b) => true);
		KnockedOutBeginTransition trans = new KnockedOutBeginTransition(StateID.KnockedOut, this);
		fsm.CreateTimedState<MoveState>(StateID.TurnAround).AddTransition(trans).CreateTimedTransition(1f, StateID.Walk);
		fsm.CreateState<WalkState>(StateID.Walk).AddTransition(trans).CreateTransition(StateID.TurnAround, (SealBehaviour b) => b.ArrivedAtTargetPoint());
		fsm.CreateTimedState<KnockedOutState>(StateID.KnockedOut).AddTransition(trans).CreateTimedTransition(AnimationLength("seal_knock"), StateID.Walk);
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
}
