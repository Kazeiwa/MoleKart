using UnityEngine;

namespace Misc;

public struct BareEdge
{
	public Vector2 From;

	public Vector2 To;

	public Vector2 Vector => To - From;

	public BareEdge(Vector2 from, Vector2 to)
	{
		From = from;
		To = to;
	}

	public static BareEdge Extend(float length, Vector2 from, Vector2 to)
	{
		Vector2 vector = length * (to - from).normalized;
		return new BareEdge(from - vector, to + vector);
	}
}
