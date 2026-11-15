using GameNetwork;
using Misc;
using UnityEngine;

public class MouseBehaviour : SimpleMonsterBehaviour
{
	public enum StateID
	{
		None,
		Init,
		Lurk,
		Jump,
		KnockedOut
	}

	public enum CommandID
	{
		CarHit
	}

	public class LurkState : SuperFSM<StateID, MouseBehaviour>.State
	{
		public LurkState(StateID stateID, MouseBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.Vel = Vector2.zero;
			float limit = Random.Range(1f, 2.5f);
			base.Timer.ResetWithLimit(limit);
		}
	}

	public class JumpState : SuperFSM<StateID, MouseBehaviour>.State
	{
		public JumpState(StateID stateID, MouseBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}
	}

	public class KnockedOutState : SuperFSM<StateID, MouseBehaviour>.State
	{
		public KnockedOutState(StateID stateID, MouseBehaviour behaviour)
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

	private const float LurkTimeLength = 3f;

	private const float KnockedOutTimeLength = 3f;

	public const float CarTimerLimit = 2f;

	private SuperFSM<StateID, MouseBehaviour> fsm;

	private Transform cachedRootTransform;

	public override Transform GrabTransform => base.transform;

	public override GameObject GrabGameObject => base.transform.parent.parent.gameObject;

	public Transform RootTransform
	{
		get
		{
			if (null == cachedRootTransform)
			{
				cachedRootTransform = GameObject.transform;
			}
			return cachedRootTransform;
		}
	}

	public Vector3 Home { get; set; }

	public override bool ShouldSendMovementInfo => false;

	public MouseBehaviour()
		: base(2f)
	{
	}

	public void SendCommand(CommandID commandId)
	{
		if (NetworkManager.Instance.IsServer)
		{
			ServerSendCommand((int)commandId);
		}
		ClientOnReceiveCommand((int)commandId);
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
		switch ((StateID)stateId)
		{
		case StateID.None:
		case StateID.Init:
			break;
		case StateID.Lurk:
			RootTransform.position = Home - new Vector3(0f, 10f, 0f);
			break;
		case StateID.Jump:
			RootTransform.position = Home;
			PlayAnimation("jump");
			break;
		case StateID.KnockedOut:
			RootTransform.position = Home;
			PlayAnimation("knock");
			break;
		}
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
		if (commandId == 0)
		{
			PlaySound(20701, 1);
		}
	}

	public override void StartState()
	{
		Home = RootTransform.position;
		base.StartState();
		if (!NetworkManager.Instance.IsClient)
		{
			MakeFSM();
			fsm.Update(doStateActionIfNotTriggered: false);
		}
	}

	public override void UpdateState()
	{
		UpdateCarTimerManager();
		if (!NetworkManager.Instance.IsClient)
		{
			fsm.Update(doStateActionIfNotTriggered: true);
		}
		base.UpdateState();
	}

	protected void OnTriggerEnter(Collider other)
	{
		Car car = CarUtil.FindCarInCollision(other);
		if (null != car && car.isHero && CheckAndHitCar(car, delegate(Car v)
		{
			v.HitByRobb((v.Pos - Pos).normalized);
		}))
		{
			SendCommand(CommandID.CarHit);
		}
	}

	protected void OnDrawGizmos()
	{
		GizmosEx.DrawText(Position, Transform.localPosition.y.ToString());
	}

	protected void MakeFSM()
	{
		fsm = new SuperFSM<StateID, MouseBehaviour>(this);
		fsm.CreateState(StateID.Init).CreateTransition(StateID.Lurk, (MouseBehaviour b) => true);
		fsm.CreateTimedState<LurkState>(StateID.Lurk).CreateTimedTransition(3f, StateID.Jump);
		fsm.CreateTimedState<JumpState>(StateID.Jump).CreateTransition(StateID.KnockedOut, (MouseBehaviour b) => b.KnockedOut).CreateTimedTransition(AnimationLength("jump"), StateID.Lurk);
		fsm.CreateTimedState<KnockedOutState>(StateID.KnockedOut).CreateTimedTransition(3f, StateID.Lurk);
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
