using System.Collections.Generic;
using Misc;
using UnityEngine;

public class PlantBehaviour : MonsterBehaviour
{
	public enum StateID
	{
		None,
		Init,
		Idle,
		Attack
	}

	public class IdleState : SuperFSM<StateID, PlantBehaviour>.State
	{
		public IdleState(StateID stateID, PlantBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class AttackState : SuperFSM<StateID, PlantBehaviour>.State
	{
		public AttackState(StateID stateID, PlantBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public const float CarTimerLimit = 2f;

	public const float BackFlipSpeedThreshold = 15f;

	private const int StartAttackCommand = 1;

	private SuperFSM<StateID, PlantBehaviour> fsm;

	private IList<Car> targetVehicles;

	private bool peerIsAskingToHit;

	public override bool ShouldBeAvoided
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

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

	public override bool ShouldSendMovementInfo
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
	}

	public override void StartState()
	{
	}

	protected override void InitEntity()
	{
	}

	public override void UpdateState()
	{
	}

	protected override void UpdateEntity()
	{
	}

	protected void OnCollisionEnter(Collision collision)
	{
	}

	protected void MakeFSM()
	{
	}

	private bool HasTargetVehicle()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void HitTargetVehicles()
	{
	}

	private bool PeerIsAskingToHit()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
