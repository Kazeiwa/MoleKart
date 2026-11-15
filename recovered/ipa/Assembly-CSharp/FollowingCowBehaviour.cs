using UnityEngine;

public class FollowingCowBehaviour : CowBehaviour
{
	public GameObject leadCowGameObject;

	public Vector2 OffsetToLead;

	private CowBehaviour leadCow;

	public override void SetTargetPos()
	{
	}

	protected override void UpdateWalk()
	{
	}

	public override void StartState()
	{
	}

	protected override void OnDrawGizmos()
	{
	}
}
