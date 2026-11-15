using GameNetwork;
using Misc;
using UnityEngine;

public class DogBehaviour : ColliderMonsterBehaviour2
{
	public enum StateID
	{
		None,
		Init,
		Walk,
		KnockedOut
	}

	public enum CommandID
	{
		CarHit
	}

	public class WalkState : SuperFSM<StateID, DogBehaviour>.State
	{
		public WalkState(StateID stateID, DogBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void Action()
		{
			if (base.Behaviour.RenderEnabled || !NetworkManager.Instance.IsStandAlone)
			{
				base.Behaviour.UpdateWalk();
			}
		}
	}

	public class KnockedOutState : SuperFSM<StateID, DogBehaviour>.State
	{
		public KnockedOutState(StateID stateID, DogBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.Vel = Vector2.zero;
		}

		public override void ExitAction()
		{
			base.Behaviour.KnockedOut = false;
		}
	}

	public const float BackFlipSpeedThreshold = 15f;

	private const float CarTimerLimit = 2f;

	private const float WalkSpeed = 9f;

	private const float IdleTimeLength = 3f;

	private const float SqareArriveTargetThreshold = 0.5f;

	private const float hitVehicleDistanceThreshold = 2f;

	public GameObject Center;

	public float WalkRadius = 13f;

	private SuperFSM<StateID, DogBehaviour> fsm;

	private StateID stateId;

	public override Transform GrabTransform => base.transform;

	public override GameObject GrabGameObject => base.gameObject;

	public Vector2 Home
	{
		get
		{
			if (Center == null)
			{
				return Vector2.zero;
			}
			return Vector3Ex.To2D(Center.transform.position);
		}
	}

	public DogBehaviour()
		: base(2f)
	{
	}

	public void SendCommand(CommandID commandId)
	{
		if (NetworkManager.Instance.IsServer)
		{
			ServerSendCommand((int)commandId);
		}
	}

	public void OnCollision(Car car)
	{
		if (CheckAndHitCar(car, delegate(Car c)
		{
			c.BackFlipDeputy();
		}))
		{
			SendCommand(CommandID.CarHit);
		}
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
		if (this.stateId == StateID.KnockedOut)
		{
			ColliderControlStandUp();
		}
		this.stateId = (StateID)stateId;
		switch (this.stateId)
		{
		case StateID.None:
		case StateID.Init:
			break;
		case StateID.Walk:
			PlayAnimation("walk");
			break;
		case StateID.KnockedOut:
			PlayAnimation("knock");
			ColliderControlBeKnockedOut();
			break;
		}
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
		if (commandId == 0)
		{
			PlaySound(20702, 1);
		}
	}

	protected override void InitEntity()
	{
		MakeFSM();
		fsm.Update(doStateActionIfNotTriggered: false);
	}

	protected override void UpdateEntity()
	{
		fsm.Update(doStateActionIfNotTriggered: true);
	}

	protected virtual void OnDrawGizmos()
	{
		Color color = Gizmos.color;
		Gizmos.color = Color.cyan;
		GizmosEx.DrawLine(Position, Home);
		Vector3 center = Vector2Ex.To3D(Home, Position.y);
		Gizmos.color = Color.yellow;
		GizmosEx.DrawCircle(center, WalkRadius);
		Gizmos.color = Color.magenta;
		GizmosEx.DrawCircle(Position, base.Radius);
		Gizmos.color = color;
	}

	protected void MakeFSM()
	{
		fsm = new SuperFSM<StateID, DogBehaviour>(this);
		fsm.CreateState(StateID.Init).CreateTransition(StateID.Walk, (DogBehaviour b) => true);
		fsm.CreateState<WalkState>(StateID.Walk).CreateTransition(StateID.KnockedOut, (DogBehaviour b) => b.KnockedOut);
		fsm.CreateTimedState<KnockedOutState>(StateID.KnockedOut).CreateTransition(StateID.KnockedOut, (DogBehaviour b) => b.KnockedOut).CreateTimedTransition(AnimationLength("knock"), StateID.Walk);
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
		MoveAroundPos(9f, Time.deltaTime, WalkRadius, Home);
	}
}
