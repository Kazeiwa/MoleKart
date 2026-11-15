using Misc;
using UnityEngine;

public class PointBehaviour : MonoBehaviour
{
	public Vector3 Position => base.transform.position;

	public Vector2 Pos => Vector3Ex.To2D(Position);

	protected void OnDrawGizmos()
	{
		Color color = Gizmos.color;
		Gizmos.color = Color.magenta;
		Gizmos.DrawSphere(base.transform.position, 1.5f);
		GizmosEx.DrawText(Position, base.name);
		Gizmos.color = color;
	}
}
