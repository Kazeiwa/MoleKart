using Google.ProtocolBuffers;
using Misc;
using UnityEngine;

public class VolcanoRockMonsterBehaviour : ColliderMonsterBehaviour
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

	public class WalkState : SuperFSM<StateID, VolcanoRockMonsterBehaviour>.State
	{
		public WalkState(StateID stateID, VolcanoRockMonsterBehaviour behaviour)
		{
		}

		public override void Action()
		{
		}
	}

	public class KnockedOutState : SuperFSM<StateID, VolcanoRockMonsterBehaviour>.State
	{
		public KnockedOutState(StateID stateID, VolcanoRockMonsterBehaviour behaviour)
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

	private const float WalkSpeed = 13.333333f;

	private const float IdleTimeLength = 3f;

	private const float SqareArriveTargetThreshold = 0.5f;

	public GameObject Center;

	public float WalkRadius;

	private SuperFSM<StateID, VolcanoRockMonsterBehaviour> fsm;

	private float walkAroundAngle;

	private float remoteT;

	private bool isWalking;

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

	public override MonsterNetworking.MovementInfoType MovementInfoType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override IMessageLite CreateMovementInfo()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void ClientOnReceiveRemoteMovementInfoCircular1(float t)
	{
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

	protected override void ClientUpdateMovement()
	{
	}

	protected override void InitEntity()
	{
	}

	protected override void UpdateEntity()
	{
	}

	protected void MakeFSM()
	{
	}

	protected virtual void UpdateWalk(float deltaTime)
	{
	}

	private void OnStateInfo(StateID stateId)
	{
	}

	public void InitFromData(MonsterBaseData data)
	{
	}
}
