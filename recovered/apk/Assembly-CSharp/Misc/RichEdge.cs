using RaceTrack;
using UnityEngine;

namespace Misc;

public class RichEdge
{
	public RaceNode From;

	public RaceNode To;

	private Vector2 fromPos;

	private Vector2 toPos;

	private Vector2 vector;

	private Vector2 forward;

	private Vector2 right;

	private float magnitude;

	private Vector2 extendedFromPos;

	private Vector2 extendedToPos;

	private ClockwiseQuadrangle quadrangle;

	private Bounds2D bounds;

	private float minLeftWidth;

	private float minRightWidth;

	private int identifier;

	public Vector2 FromPos => fromPos;

	public Vector2 ToPos => toPos;

	public Vector2 Vector => vector;

	public Vector2 Forward => forward;

	public Vector2 Right => right;

	public float Magnitude => magnitude;

	public Bounds2D Bounds => bounds;

	public ClockwiseQuadrangle Quadrangle => quadrangle;

	public float MinLeftWidth => minLeftWidth;

	public float MinRightWidth => minRightWidth;

	public RichEdge(RaceNode from, RaceNode to)
	{
		From = from;
		To = to;
		fromPos = from.Pos;
		toPos = to.Pos;
		this.vector = ToPos - FromPos;
		forward = Vector.normalized;
		right = Vector2Ex.RotateRight(Forward);
		magnitude = this.vector.magnitude;
		Vector2 vector = 4f * Forward;
		extendedFromPos = FromPos - vector;
		extendedToPos = ToPos + vector;
		Vector2 a = extendedFromPos - From.LeftWidth * Right;
		Vector2 b = extendedToPos - To.LeftWidth * Right;
		Vector2 c = extendedToPos + To.RightWidth * Right;
		Vector2 d = extendedFromPos + From.RightWidth * Right;
		quadrangle = new ClockwiseQuadrangle(a, b, c, d);
		bounds = quadrangle.Bounds;
		minLeftWidth = Mathf.Min(From.LeftWidth, To.LeftWidth);
		minRightWidth = Mathf.Min(From.RightWidth, To.RightWidth);
		identifier = from.Index | (to.Index << 16);
	}

	public override int GetHashCode()
	{
		return identifier;
	}

	public bool PositionInEdgeBlock(Vector2 position)
	{
		return Bounds.Contains(position) && Quadrangle.Contains(position);
	}

	public Vector2 GlobalToLocalVector(Vector2 globalPos)
	{
		return Vector2Ex.GlobalToLocalVector(globalPos - FromPos, Forward);
	}

	public float SignedDistanceFromPointToLine(Vector2 p)
	{
		return Vector2.Dot(p - FromPos, Right);
	}

	public Vector2 GlobalToLocalLookAheadPos(Vector2 globalPos, out float leftWidth, out float rightWidth)
	{
		leftWidth = To.LeftWidth;
		rightWidth = To.RightWidth;
		return GlobalToLocalVector(globalPos);
	}

	public LookAheadInfo GlobalToLocalLookAheadInfo(Vector2 globalPos)
	{
		Vector2 lookAheadPos = GlobalToLocalVector(globalPos);
		return new LookAheadInfo(lookAheadPos, To.LeftWidth, To.RightWidth);
	}

	public void OnDrawGizmos()
	{
		Color color = Gizmos.color;
		Gizmos.color = Color.cyan;
		foreach (Vector2 vertex in Quadrangle.Vertices)
		{
			GizmosEx.DrawSphere(vertex, 0.5f);
		}
		Gizmos.color = color;
	}
}
