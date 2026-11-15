using UnityEngine;

public class IronArmBehaviour : SimpleMonsterBehaviour
{
	private const float KnockedOutTimeLength = 7f;

	public const float CarTimerLimit = 2f;

	public float IdleTimeLength;

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

	protected new void StartState()
	{
	}

	protected void OnDrawGizmos()
	{
	}
}
