using Misc;
using UnityEngine;

public class RaceEdgeTool : MonoBehaviour
{
	public const float DefaultY = 150f;

	public RaceNodeTool From;

	public RaceNodeTool To;

	public Vector2 Direction => (To.Pos - From.Pos).normalized;

	public Vector2 Left => Vector2Ex.RotateLeft(Direction);

	public Vector2 Right => -Left;

	protected void OnDrawGizmos()
	{
		if (GlobalOptions.Instance.ShouldDrawRaceLine)
		{
			DrawRaceBoundary(Color.cyan);
		}
	}

	private void DrawRaceBoundary(Color color)
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		BareEdge bareEdge = BareEdge.Extend(4f, From.Pos, To.Pos);
		Vector2 a;
		Vector2 b;
		if (From.IsSlim)
		{
			RaceEdgeTool inEdge = From.GetInEdge(0);
			Vector2 vector = Vector2Ex.MeanDirection(inEdge.Right, Right);
			a = From.Pos - From.leftWidth * vector;
			b = From.Pos + From.rightWidth * vector;
		}
		else
		{
			a = From.Pos + From.leftWidth * Left;
			b = From.Pos + From.rightWidth * Right;
		}
		Vector2 d;
		Vector2 c;
		if (To.IsSlim)
		{
			RaceEdgeTool outEdge = To.GetOutEdge(0);
			Vector2 vector2 = Vector2Ex.MeanDirection(Right, outEdge.Right);
			d = To.Pos - To.leftWidth * vector2;
			c = To.Pos + To.rightWidth * vector2;
		}
		else
		{
			d = To.Pos + To.leftWidth * Left;
			c = To.Pos + To.rightWidth * Right;
		}
		GizmosEx.DrawQuadrangle(a, b, c, d, base.transform.position.y);
		Gizmos.color = color2;
	}
}
