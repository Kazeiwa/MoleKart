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
		{
		}

		public override void Action()
		{
		}
	}

	public class KnockedOutState : SuperFSM<StateID, BearBehaviour>.State
	{
		public KnockedOutState(StateID stateID, BearBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void ExitAction()
		{
		}
	}

	private const float CarTimerLimit = 2f;

	private const float BackFlipSpeedThreshold = 15f;

	private const float WalkSpeed = 3f;

	private const float IdleTimeLength = 3f;

	private const float SqareArriveTargetThreshold = 0.5f;

	private const float hitVehicleDistanceThreshold = 2f;

	public GameObject Center;

	public float WalkRadius;

	private SuperFSM<StateID, BearBehaviour> fsm;

	private float walkAroundAngle;

	public override Transform GrabTransform
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override GameObject GrabGameObject
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Home
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float WalkAroundAngle
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public void SendCommand(CommandID commandId)
	{
	}

	public override bool CanHitVehicle(Car vehicle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ProcessVehicleCollision()
	{
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
	}

	protected override void InitEntity()
	{
	}

	protected override void UpdateEntity()
	{
	}

	protected virtual void OnDrawGizmos()
	{
	}

	protected void MakeFSM()
	{
	}

	private void OnStateInfo(StateID stateId)
	{
	}

	protected virtual void UpdateWalk(float deltaTime)
	{
	}
}
