using UnityEngine;

public abstract class MonsterBehaviour : BaseMonsterBehaviour
{
	private Vector2 remotePos;

	private float remoteH;

	private bool hasRemoteH;

	private bool remoteNeedSync;

	protected bool RemoteNeedSync
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public MonsterBehaviour(float carTimerLimit)
	{
	}

	public override void ClientOnReceiveRemoteMovementInfo(Vector2 position, Vector2 velocity, float rotAngle)
	{
	}

	public override void ClientOnReceiveRemoteMovementInfo(Vector3 position, Vector2 velocity, float rotAngle)
	{
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
	}

	public override void UpdateState()
	{
	}

	protected abstract void InitEntity();

	protected abstract void UpdateEntity();

	protected virtual void ClientUpdateMovement()
	{
	}

	public override void StartState()
	{
	}

	private void ClientSyncPosWithRemoteInfo()
	{
	}
}
