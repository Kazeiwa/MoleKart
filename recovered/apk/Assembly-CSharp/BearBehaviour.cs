using GameNetwork;
using Misc;
using UnityEngine;

public class BearBehaviour : ColliderMonsterBehaviour
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

	public class WalkState : SuperFSM<StateID, BearBehaviour>.State
	{
		public WalkState(StateID stateID, BearBehaviour behaviour)
			: base(stateID, behaviour)
		{
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

	public class KnockedOutState : SuperFSM<StateID, BearBehaviour>.State
	{
		public KnockedOutState(StateID stateID, BearBehaviour behaviour)
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

	private const float CarTimerLimit = 2f;

	private const float BackFlipSpeedThreshold = 15f;

	private const float WalkSpeed = 3f;

	private const float IdleTimeLength = 3f;

	private const float SqareArriveTargetThreshold = 0.5f;

	private const float hitVehicleDistanceThreshold = 2f;

	public GameObject Center;

	public float WalkRadius = 13f;

	private SuperFSM<StateID, BearBehaviour> fsm;

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

	public BearBehaviour()
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
			PlaySound(20601, 1);
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
		Gizmos.color = Color.yellow;
		GizmosEx.DrawCircle(center, WalkRadius);
		Gizmos.color = Color.magenta;
		GizmosEx.DrawCircle(Position, base.Radius);
		Gizmos.color = color;
	}

	protected void MakeFSM()
	{
		fsm = new SuperFSM<StateID, BearBehaviour>(this);
		fsm.CreateState(StateID.Init).CreateTransition(StateID.Walk, (BearBehaviour b) => true);
		fsm.CreateState<WalkState>(StateID.Walk).CreateTransition(StateID.KnockedOut, (BearBehaviour b) => b.KnockedOut);
		fsm.CreateTimedState<KnockedOutState>(StateID.KnockedOut).CreateTransition(StateID.KnockedOut, (BearBehaviour b) => b.KnockedOut).CreateTimedTransition(AnimationLength("knock"), StateID.Walk);
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

	protected virtual void UpdateWalk(float deltaTime)
	{
		MoveAroundPos(3f, deltaTime, WalkRadius, Home);
	}
}
