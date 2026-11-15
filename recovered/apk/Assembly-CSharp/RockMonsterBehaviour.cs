using GameNetwork;
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
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.Vel = Vector2.zero;
		}

		public override void Action()
		{
			base.Behaviour.ProcessVehicleCollisionInIdle();
		}
	}

	public class WalkState : SuperFSM<StateID, RockMonsterBehaviour>.State
	{
		public WalkState(StateID stateID, RockMonsterBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void Action()
		{
			if (base.Behaviour.RenderEnabled || !NetworkManager.Instance.IsStandAlone)
			{
				base.Behaviour.UpdateWalk();
				base.Behaviour.ProcessVehicleCollisionInWalk();
			}
		}

		public override void ExitAction()
		{
			base.Behaviour.UpdateTargetPointIndex();
		}
	}

	public class KnockedOutState : SuperFSM<StateID, RockMonsterBehaviour>.State
	{
		public KnockedOutState(StateID stateID, RockMonsterBehaviour behaviour)
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

	private const int MoveInc = 1;

	private const int MoveDec = -1;

	private const float IdleTimeLength = 5f;

	private const float KnockedOutTimeLength = 7f;

	private const float WalkSpeed = 20f;

	private const float SqareArriveThreshold = 1f;

	private const float BackFlipSpeedThreshold = 15f;

	public GameObject[] points = new GameObject[3];

	private SuperFSM<StateID, RockMonsterBehaviour> fsm;

	public override Transform GrabTransform => base.transform;

	public override GameObject GrabGameObject => base.gameObject;

	public int MoveOrder { get; set; }

	public int TargetPointIndex { get; set; }

	public Vector2 TargetPos => Vector3Ex.To2D(points[TargetPointIndex].transform.position);

	public RockMonsterBehaviour()
		: base(2f)
	{
		MoveOrder = 1;
		TargetPointIndex = 0;
	}

	public bool ArrivedAtTargetPoint()
	{
		return Vector2Ex.SqrDistance(Pos, TargetPos) < 1f;
	}

	public void UpdateWalk()
	{
		HeadForPos(20f, Time.deltaTime, TargetPos);
	}

	public override bool CanHitVehicle(Car vehicle)
	{
		return Vector3.Distance(vehicle.Position, Position) < base.Radius + vehicle.Radius && (!NetworkManager.Instance.IsStandAlone || vehicle.Vel.magnitude > 15f);
	}

	public void ProcessVehicleCollisionInWalk()
	{
		CheckAndHitCar(delegate(Car v)
		{
			v.BackFlip();
		});
	}

	public void ProcessVehicleCollisionInIdle()
	{
		CheckAndHitCar(delegate(Car v)
		{
			v.BackFlip();
		});
	}

	public void UpdateTargetPointIndex()
	{
		int num = TargetPointIndex + MoveOrder;
		if (num < 0)
		{
			MoveOrder = 1;
		}
		else if (num >= points.Length)
		{
			MoveOrder = -1;
		}
		TargetPointIndex += MoveOrder;
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
		Gizmos.DrawLine(Position, points[0].transform.position);
		for (int i = 0; i < points.Length - 1; i++)
		{
			Gizmos.DrawLine(points[i].transform.position, points[(i + 1) % points.Length].transform.position);
		}
		Gizmos.color = Color.magenta;
		GizmosEx.DrawCircle(Position, base.Radius);
		GizmosEx.DrawText(Position, "r: " + base.Radius);
		Gizmos.color = color;
	}

	protected void OnDrawGizmosSelected()
	{
	}

	protected void MakeFSM()
	{
		fsm = new SuperFSM<StateID, RockMonsterBehaviour>(this);
		fsm.CreateState(StateID.Init).CreateTransition(StateID.Idle, (RockMonsterBehaviour b) => true);
		fsm.CreateTimedState<IdleState>(StateID.Idle).CreateTransition(StateID.KnockedOut, (RockMonsterBehaviour b) => b.KnockedOut).CreateTimedTransition(5f, StateID.Walk);
		fsm.CreateState<WalkState>(StateID.Walk).CreateTransition(StateID.KnockedOut, (RockMonsterBehaviour b) => b.KnockedOut).CreateTransition(StateID.Idle, (RockMonsterBehaviour b) => b.ArrivedAtTargetPoint());
		fsm.CreateTimedState<KnockedOutState>(StateID.KnockedOut).CreateTimedTransition(7f, StateID.Walk);
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
