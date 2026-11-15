using GameNetwork;
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
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.Vel = Vector2.zero;
		}
	}

	public class ChangeTargetState : SuperFSM<StateID, PterosaurBehaviour>.State
	{
		public ChangeTargetState(StateID stateID, PterosaurBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.ResetFireBallTarget();
			base.Behaviour.SaveBeginningRotation();
		}

		public override void Action()
		{
			base.Behaviour.UpdateChangeTargetHeading();
		}
	}

	public class AttackState : SuperFSM<StateID, PterosaurBehaviour>.State
	{
		private bool notFired;

		public AttackState(StateID stateID, PterosaurBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			notFired = true;
		}

		public override void Action()
		{
			if (notFired && base.Timer.SaturatedElapsedTime > 0.1f)
			{
				notFired = false;
				base.Behaviour.SpitFireBall();
			}
		}
	}

	public const float PrepareSpittingEffectTimeLength = 0.2f;

	public const float SpittingWaitTimeLength = 0.1f;

	public const float TargetGridSize = 5f;

	public GameObject FireBallStartPoint;

	public float IdleTimeLength = 1f;

	public float ChangeTargetTimeLength = 1f;

	public float FireBallExplosionTimeLength = 5f;

	public float FireBallSpeed = 200f;

	public float TargetTop;

	public float TargetBottom;

	public float TargetLeft;

	public float TargetRight;

	private Vector2 home;

	private Vector2 fireBallTargetPos;

	private SuperFSM<StateID, PterosaurBehaviour> fsm;

	private CrudeElapsedTimer changeTargetTimer;

	public override Transform GrabTransform => base.transform;

	public override GameObject GrabGameObject => base.gameObject;

	public PterosaurBehaviour()
		: base(-1f)
	{
	}

	public void ResetFireBallTarget()
	{
		fireBallTargetPos = MathfEx.Floor(MathfEx.RandomRect(TargetTop, TargetBottom, TargetLeft, TargetRight), 5f);
	}

	public void UpdateChangeTargetHeading()
	{
		float t = changeTargetTimer.SaturatedElapsedTime / changeTargetTimer.Limit;
		LookRotation(fireBallTargetPos, t);
	}

	public void SpitFireBall()
	{
		float y = HeightMapManager.Instance.Height(fireBallTargetPos.x, fireBallTargetPos.y);
		Vector3 destPos = Vector2Ex.To3D(fireBallTargetPos, y);
		SkillManager.Instance.FireSphere(FireBallStartPoint.transform.position, destPos, FireBallSpeed, FireBallExplosionTimeLength);
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
		case StateID.ChangeTarget:
			break;
		case StateID.PrepareSpittingEffect:
			PlayAnimation("atk");
			break;
		case StateID.Attack:
			if (FireBallStartPoint != null)
			{
				PlayEffect(eEffectType.FIRESPHERE, FireBallStartPoint.transform.localPosition, 3f);
			}
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
		fsm.Update(doStateActionIfNotTriggered: true);
	}

	protected void OnDrawGizmos()
	{
		GizmosEx.DrawRect(TargetTop, TargetBottom, TargetLeft, TargetRight);
	}

	protected void MakeFSM()
	{
		fsm = new SuperFSM<StateID, PterosaurBehaviour>(this);
		fsm.CreateState<InitState>(StateID.Init).CreateTransition(StateID.Idle, (PterosaurBehaviour b) => true);
		fsm.CreateState(StateID.Idle).CreateTimedTransition(IdleTimeLength, StateID.ChangeTarget);
		SuperFSM<StateID, PterosaurBehaviour>.State state = fsm.CreateTimedState<ChangeTargetState>(StateID.ChangeTarget).CreateTimedTransition(ChangeTargetTimeLength, StateID.PrepareSpittingEffect);
		changeTargetTimer = state.Timer;
		fsm.CreateState(StateID.PrepareSpittingEffect).CreateTimedTransition(0.2f, StateID.Attack);
		float num = Animation["atk"].length - 0.2f;
		if (num <= 0f)
		{
			Debug.LogError("PterosaurBehaviour.MakeFSM: Error: PrepareSpittingEffectTimeLength should be greater than the time length of the attack animation.");
		}
		if (num <= 0.1f)
		{
			Debug.LogError("PterosaurBehaviour.MakeFSM: Error: The time length of the attack state should be greater than SpittingWaitTimeLength");
		}
		fsm.CreateTimedState<AttackState>(StateID.Attack).CreateTimedTransition(num, StateID.Idle);
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
