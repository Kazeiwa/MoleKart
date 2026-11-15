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
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.PlayAnimation("idle");
		}
	}

	public class AttackState : SuperFSM<StateID, PlantBehaviour>.State
	{
		public AttackState(StateID stateID, PlantBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.HitTargetVehicles();
			base.Behaviour.peerIsAskingToHit = false;
			base.Behaviour.PlayAnimation("attack");
			base.Behaviour.PlaySound(20801, 1);
		}
	}

	public const float CarTimerLimit = 2f;

	public const float BackFlipSpeedThreshold = 15f;

	private const int StartAttackCommand = 1;

	private SuperFSM<StateID, PlantBehaviour> fsm;

	private IList<Car> targetVehicles = new List<Car>();

	private bool peerIsAskingToHit;

	public override bool ShouldBeAvoided => false;

	public override Transform GrabTransform => base.transform;

	public override GameObject GrabGameObject => base.gameObject;

	public override bool ShouldSendMovementInfo => false;

	public PlantBehaviour()
		: base(2f)
	{
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
		if (commandId == 1)
		{
			peerIsAskingToHit = true;
		}
	}

	public override void StartState()
	{
		base.StartState();
		MakeFSM();
		fsm.Update(doStateActionIfNotTriggered: false);
	}

	protected override void InitEntity()
	{
	}

	public override void UpdateState()
	{
		fsm.Update(doStateActionIfNotTriggered: true);
		UpdateCarTimerManager();
	}

	protected override void UpdateEntity()
	{
		fsm.Update(doStateActionIfNotTriggered: true);
		UpdateCarTimerManager();
	}

	protected void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.magnitude > 15f)
		{
			Car car = CarUtil.FindCarInCollision(collision.collider);
			if (null != car)
			{
				targetVehicles.Add(car);
			}
		}
	}

	protected void MakeFSM()
	{
		fsm = new SuperFSM<StateID, PlantBehaviour>(this);
		fsm.CreateState(StateID.Init).CreateTransition(StateID.Idle);
		fsm.CreateState<IdleState>(StateID.Idle).CreateTransition(StateID.Attack, PeerIsAskingToHit).CreateTransition(StateID.Attack, HasTargetVehicle);
		fsm.CreateState<AttackState>(StateID.Attack).CreateTransition(StateID.Attack, HasTargetVehicle).CreateTransition(StateID.Attack, PeerIsAskingToHit)
			.CreateTimedTransition(AnimationLength("attack"), StateID.Idle);
	}

	private bool HasTargetVehicle()
	{
		return targetVehicles.Count > 0;
	}

	private void HitTargetVehicles()
	{
		foreach (Car targetVehicle in targetVehicles)
		{
			CheckAndHitCar(targetVehicle, delegate(Car c)
			{
				c.BackFlipDeputy();
			});
		}
		targetVehicles.Clear();
	}

	private bool PeerIsAskingToHit()
	{
		return peerIsAskingToHit;
	}
}
