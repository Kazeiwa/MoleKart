using GameNetwork;
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
			: base(destStateID, behaviour)
		{
		}

		public override bool IsTriggered()
		{
			return base.Behaviour.KnockedOut && !base.Behaviour.KnockedOutProcessed;
		}
	}

	public class IdleState : SuperFSM<StateID, QiqiBehaviour>.State
	{
		public IdleState(StateID stateID, QiqiBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void Action()
		{
			base.Behaviour.ProcessVehicleCollision();
		}
	}

	public class WalkState : SuperFSM<StateID, QiqiBehaviour>.State
	{
		public WalkState(StateID stateID, QiqiBehaviour behaviour)
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
				base.Behaviour.UpdateWalk();
				base.Behaviour.ProcessVehicleCollision();
			}
		}
	}

	public class KnockedOutState01 : SuperFSM<StateID, QiqiBehaviour>.State
	{
		public KnockedOutState01(StateID stateID, QiqiBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.KnockedOutProcessed = true;
			base.Behaviour.ColliderControlBeKnockedOut();
		}
	}

	public class KnockedOutState03 : SuperFSM<StateID, QiqiBehaviour>.State
	{
		public KnockedOutState03(StateID stateID, QiqiBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void ExitAction()
		{
			base.Behaviour.KnockedOut = false;
			base.Behaviour.KnockedOutProcessed = false;
			base.Behaviour.ColliderControlStandUp();
		}
	}

	public const float CarTimerLimit = 2f;

	private const float BackFlipSpeedThreshold = 15f;

	public const float WalkSpeed = 2f;

	public const float IdleTimeLength = 1f;

	private const float SqareArriveThreshold = 1f;

	public GameObject[] Points = new GameObject[2];

	private Vector2 home;

	private SuperFSM<StateID, QiqiBehaviour> fsm;

	public override Transform GrabTransform => base.transform;

	public override GameObject GrabGameObject => base.gameObject;

	public int TargetPointIndex { get; set; }

	public Vector2 TargetPos => Vector3Ex.To2D(Points[TargetPointIndex].transform.position);

	public QiqiBehaviour()
		: base(2f)
	{
		TargetPointIndex = 0;
	}

	public override bool CanHitVehicle(Car vehicle)
	{
		return Vector3.Distance(vehicle.Position, Position) < base.Radius + vehicle.Radius && (!NetworkManager.Instance.IsStandAlone || vehicle.Vel.magnitude > 15f);
	}

	public void ProcessVehicleCollision()
	{
		CheckAndHitCar(delegate(Car v)
		{
			v.BackFlip();
		});
	}

	public void UpdateTargetPointIndex()
	{
		TargetPointIndex = (TargetPointIndex + 1) % Points.Length;
	}

	public bool ArrivedAtTargetPoint()
	{
		return Vector2Ex.SqrDistance(Pos, TargetPos) < 1f;
	}

	public void UpdateWalk()
	{
		HeadForPos(2f, Time.deltaTime, TargetPos);
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
		case StateID.KnockedOut01:
			PlayAnimation("knock01");
			break;
		case StateID.KnockedOut02:
			PlayAnimation("knock02");
			break;
		case StateID.KnockedOut03:
			PlayAnimation("knock03");
			break;
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
		fsm = new SuperFSM<StateID, QiqiBehaviour>(this);
		fsm.CreateState(StateID.Init).CreateTransition(StateID.Idle, (QiqiBehaviour b) => true);
		KnockedOutBeginTransition trans = new KnockedOutBeginTransition(StateID.KnockedOut01, this);
		fsm.CreateTimedState<IdleState>(StateID.Idle).AddTransition(trans).CreateTimedTransition(1f, StateID.Walk);
		fsm.CreateState<WalkState>(StateID.Walk).AddTransition(trans).CreateTransition(StateID.Idle, (QiqiBehaviour b) => b.ArrivedAtTargetPoint());
		fsm.CreateTimedState<KnockedOutState01>(StateID.KnockedOut01).AddTransition(trans).CreateTimedTransition(AnimationLength("knock01"), StateID.KnockedOut02);
		fsm.CreateState(StateID.KnockedOut02).AddTransition(trans).CreateTimedTransition(AnimationLength("knock02"), StateID.KnockedOut03);
		fsm.CreateTimedState<KnockedOutState03>(StateID.KnockedOut03).AddTransition(trans).CreateTimedTransition(AnimationLength("knock03"), StateID.Walk);
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
