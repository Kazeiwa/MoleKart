using Misc;
using UnityEngine;

public class PterosaurBehaviour : MonsterBehaviour
{
	public enum StateID
	{
		None,
		Init,
		Idle,
		ChangeTarget,
		PrepareSpittingEffect,
		Attack
	}

	public class InitState : SuperFSM<StateID, PterosaurBehaviour>.State
	{
		public InitState(StateID stateID, PterosaurBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class ChangeTargetState : SuperFSM<StateID, PterosaurBehaviour>.State
	{
		public ChangeTargetState(StateID stateID, PterosaurBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void Action()
		{
		}
	}

	public class AttackState : SuperFSM<StateID, PterosaurBehaviour>.State
	{
		private bool notFired;

		public AttackState(StateID stateID, PterosaurBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void Action()
		{
		}
	}

	public const float PrepareSpittingEffectTimeLength = 0.2f;

	public const float SpittingWaitTimeLength = 0.1f;

	public const float TargetGridSize = 5f;

	public GameObject FireBallStartPoint;

	public float IdleTimeLength;

	public float ChangeTargetTimeLength;

	public float FireBallExplosionTimeLength;

	public float FireBallSpeed;

	public float TargetTop;

	public float TargetBottom;

	public float TargetLeft;

	public float TargetRight;

	private Vector2 fireBallTargetPos;

	private SuperFSM<StateID, PterosaurBehaviour> fsm;

	private CrudeElapsedTimer changeTargetTimer;

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

	public void ResetFireBallTarget()
	{
	}

	public void UpdateChangeTargetHeading()
	{
	}

	public void SpitFireBall()
	{
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
	}

	protected override void InitEntity()
	{
	}

	protected override void UpdateEntity()
	{
	}

	protected void OnDrawGizmos()
	{
	}

	protected void MakeFSM()
	{
	}

	private void OnStateInfo(StateID stateId)
	{
	}
}
