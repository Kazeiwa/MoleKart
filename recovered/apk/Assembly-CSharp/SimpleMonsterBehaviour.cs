using UnityEngine;

public abstract class SimpleMonsterBehaviour : BaseMonsterBehaviour
{
	public SimpleMonsterBehaviour(float carTimerLimit)
		: base(carTimerLimit)
	{
	}

	public override void UpdateState()
	{
	}

	public override void ClientOnReceiveRemoteMovementInfo(Vector2 position, Vector2 velocity, float rotAngle)
	{
		Debug.LogError("SimpleMonsterBehaviour: " + Name + " received 2D remote movement info!");
	}

	public override void ClientOnReceiveRemoteMovementInfo(Vector3 position, Vector2 velocity, float rotAngle)
	{
		Debug.LogError("SimpleMonsterBehaviour: " + Name + " received 3D remote movement info!");
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
		Debug.LogError("SimpleMonsterBehaviour: " + Name + " received state info: ID = " + stateId);
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
		Debug.LogError("SimpleMonsterBehaviour: " + Name + " received state info: ID = " + commandId);
	}
}
