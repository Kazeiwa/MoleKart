using GameNetwork;
using Misc;
using RaceTrack;
using UnityEngine;

public abstract class MonsterBehaviour : BaseMonsterBehaviour, IAgent, IMonster
{
	private Vector2 remotePos;

	private float remoteH;

	private bool hasRemoteH;

	private bool remoteNeedSync;

	public MonsterBehaviour(float carTimerLimit)
		: base(carTimerLimit)
	{
	}

	public override void ClientOnReceiveRemoteMovementInfo(Vector2 position, Vector2 velocity, float rotAngle)
	{
		remotePos = position;
		remoteNeedSync = true;
		hasRemoteH = false;
		base.Vel = velocity;
		RotAngle = rotAngle;
	}

	public override void ClientOnReceiveRemoteMovementInfo(Vector3 position, Vector2 velocity, float rotAngle)
	{
		remotePos = Vector3Ex.To2D(position);
		remoteH = position.y;
		hasRemoteH = true;
		remoteNeedSync = true;
		base.Vel = velocity;
		RotAngle = rotAngle;
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
	}

	public override void UpdateState()
	{
		if (NetworkManager.Instance.IsClient)
		{
			ClientUpdateMovement();
		}
		else if (!NetworkManager.Instance.IsClient)
		{
			UpdateEntity();
		}
	}

	protected abstract void InitEntity();

	protected abstract void UpdateEntity();

	protected void ClientUpdateMovement()
	{
		if (remoteNeedSync)
		{
			ClientSyncPosWithRemoteInfo();
		}
		else if (hasRemoteH)
		{
			Move3(Time.deltaTime);
		}
		else
		{
			Move(Time.deltaTime);
		}
	}

	public override void StartState()
	{
		base.StartState();
		if (!NetworkManager.Instance.IsClient)
		{
			InitEntity();
		}
	}

	private void ServerSendMovementInfo()
	{
	}

	private void ClientSyncPosWithRemoteInfo()
	{
		if (hasRemoteH)
		{
			MoveTo(remotePos, remoteH);
		}
		else
		{
			MoveTo(remotePos);
		}
		remoteNeedSync = false;
	}
}
