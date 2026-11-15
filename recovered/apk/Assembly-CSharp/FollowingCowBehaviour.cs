using Misc;
using UnityEngine;

public class FollowingCowBehaviour : CowBehaviour
{
	public GameObject leadCowGameObject;

	public Vector2 OffsetToLead;

	private CowBehaviour leadCow;

	public override void SetTargetPos()
	{
		base.Target = Vector2Ex.RotateTranslate(OffsetToLead, leadCow.Heading, leadCow.Pos);
	}

	protected override void UpdateWalk()
	{
		SetTargetPos();
		base.UpdateWalk();
	}

	public override void StartState()
	{
		leadCow = leadCowGameObject.GetComponentInChildren<CowBehaviour>();
		base.StartState();
	}

	protected override void OnDrawGizmos()
	{
		Color color = Gizmos.color;
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(Position, leadCowGameObject.transform.position);
		Gizmos.color = Color.magenta;
		GizmosEx.DrawCircle(Position, base.Radius);
		Gizmos.color = color;
	}
}
