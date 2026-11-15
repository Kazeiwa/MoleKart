using UnityEngine;

public class IronArmBehaviour : SimpleMonsterBehaviour
{
	private const float KnockedOutTimeLength = 7f;

	public const float CarTimerLimit = 2f;

	public float IdleTimeLength = 1f;

	public override Transform GrabTransform => base.transform;

	public override GameObject GrabGameObject => base.gameObject;

	public IronArmBehaviour()
		: base(2f)
	{
	}

	protected new void StartState()
	{
		Animation.Play("Take 001");
	}

	protected void OnDrawGizmos()
	{
	}
}
