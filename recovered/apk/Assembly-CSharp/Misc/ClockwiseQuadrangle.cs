using System.Collections.Generic;
using UnityEngine;

namespace Misc;

public class ClockwiseQuadrangle
{
	public struct Edge
	{
		public Vector2 From;

		public Vector2 To;

		public Edge(Vector2 from, Vector2 to)
		{
			From = from;
			To = to;
		}
	}

	public Vector2 A;

	public Vector2 B;

	public Vector2 C;

	public Vector2 D;

	public IEnumerable<Vector2> Vertices
	{
		get
		{
			yield return A;
			yield return B;
			yield return C;
			yield return D;
		}
	}

	public IEnumerable<Edge> Edges
	{
		get
		{
			yield return new Edge(A, B);
			yield return new Edge(B, C);
			yield return new Edge(C, D);
			yield return new Edge(D, A);
		}
	}

	public bool Convex => Vector2Ex.PerpDot(A, B, C) > 0f && Vector2Ex.PerpDot(B, C, D) > 0f && Vector2Ex.PerpDot(C, D, A) > 0f && Vector2Ex.PerpDot(D, A, B) > 0f;

	public Bounds2D Bounds
	{
		get
		{
			Bounds2D result = new Bounds2D(A, Vector2.zero);
			result.Encapsulate(B);
			result.Encapsulate(C);
			result.Encapsulate(D);
			return result;
		}
	}

	public ClockwiseQuadrangle(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
	{
		A = a;
		B = b;
		C = c;
		D = d;
	}

	public bool Contains(Vector2 position)
	{
		Vector2 vector = A - position;
		Vector2 vector2 = B - position;
		Vector2 vector3 = C - position;
		Vector2 vector4 = D - position;
		return Vector2Ex.PerpDot(vector, vector2) > 0f && Vector2Ex.PerpDot(vector2, vector3) > 0f && Vector2Ex.PerpDot(vector3, vector4) > 0f && Vector2Ex.PerpDot(vector4, vector) > 0f;
	}
}
