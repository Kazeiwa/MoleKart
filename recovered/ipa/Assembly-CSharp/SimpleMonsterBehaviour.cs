using UnityEngine;

public abstract class SimpleMonsterBehaviour : BaseMonsterBehaviour
{
	public SimpleMonsterBehaviour(float carTimerLimit)
	{
	}

	public override void UpdateState()
	{
	}

	public override void ClientOnReceiveRemoteMovementInfo(Vector2 position, Vector2 velocity, float rotAngle)
	{
	}

	public override void ClientOnReceiveRemoteMovementInfo(Vector3 position, Vector2 velocity, float rotAngle)
	{
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
	}
}
