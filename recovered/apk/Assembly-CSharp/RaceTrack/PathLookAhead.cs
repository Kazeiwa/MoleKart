using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace RaceTrack;

public class PathLookAhead : PathLookAheadBase
{
	public struct Edge
	{
		public RaceNode From;

		public RaceNode To;

		public float Magnitude => Vector2Ex.Distance(From.Pos, To.Pos);

		public Vector2 Forward => (To.Pos - From.Pos).normalized;

		public Vector2 Right => Vector2Ex.RotateRight(To.Pos - From.Pos).normalized;

		public Edge(RaceNode from, RaceNode to)
		{
			From = from;
			To = to;
		}
	}

	public const float EdgeExtentLength = 4f;

	public int NodeCount => base.NodeIndexQueue.Size;

	public int EdgeCount
	{
		get
		{
			int num = base.NodeIndexQueue.Size - 1;
			return (num >= 0) ? num : 0;
		}
	}

	public RaceNode TargetNode
	{
		get
		{
			if (NodeCount < 2)
			{
				Debug.LogError("PathLookAhead: There is no target node!");
			}
			return GetNodeByLookAheadIndex(1);
		}
	}

	public new void Reset(int index)
	{
		base.Reset(index);
		while (base.NodeIndexQueue.Size < 5)
		{
			Advance();
		}
	}

	public static BareEdge ExtendedEdge(Vector2 from, Vector2 to)
	{
		return BareEdge.Extend(4f, from, to);
	}

	public void Reset(Vector2 position)
	{
		Debug.LogWarning("PathLookAhead.Reset");
		foreach (RaceEdge edge in base.RacingLine.Edges)
		{
			if (PositionInEdgeBlock(position, edge.From, edge.To))
			{
				Reset(edge.From);
				return;
			}
		}
		Reset();
	}

	public void Update(Vector2 position)
	{
		if (!base.Available)
		{
			Reset(position);
			return;
		}
		int lookAheadEdgeIndexOfPoint = GetLookAheadEdgeIndexOfPoint(position);
		if (lookAheadEdgeIndexOfPoint < 0)
		{
			Reset(position);
		}
		else if (lookAheadEdgeIndexOfPoint > 0)
		{
			Advance(lookAheadEdgeIndexOfPoint);
		}
	}

	public void DrawGizmos()
	{
		Color color = Gizmos.color;
		Gizmos.color = Color.cyan;
		foreach (RaceNode item in Nodes())
		{
			GizmosEx.DrawCircle(item.Pos, 4f);
		}
		Gizmos.color = color;
	}

	public IEnumerable<RaceNode> Nodes()
	{
		foreach (int index in base.NodeIndexQueue)
		{
			yield return base.RacingLine.Node(index);
		}
	}

	public IEnumerable<Edge> Edges()
	{
		int count = base.NodeIndexQueue.Size - 1;
		if (count >= 1)
		{
			for (int i = 0; i < count; i++)
			{
				yield return GetEdgeByLookAheadIndex(i);
			}
		}
	}

	public IEnumerable<Edge> EdgesInReverseOrder()
	{
		int count = base.NodeIndexQueue.Size - 1;
		if (count >= 1)
		{
			for (int i = count - 1; i >= 0; i--)
			{
				yield return GetEdgeByLookAheadIndex(i);
			}
		}
	}

	public int GetLookAheadEdgeIndexOfPoint(Vector2 p)
	{
		int num = 1;
		foreach (Edge item in EdgesInReverseOrder())
		{
			if (PositionInEdgeBlock(p, item.From.Index, item.To.Index))
			{
				return EdgeCount - num;
			}
			num++;
		}
		return -1;
	}

	public Edge GetEdgeByLookAheadIndex(int lookAheadEdgeIndex)
	{
		if (lookAheadEdgeIndex < 0 || lookAheadEdgeIndex >= EdgeCount)
		{
			Debug.LogWarning("Invalid look-ahead-edge-index: " + lookAheadEdgeIndex);
			return new Edge(null, null);
		}
		return new Edge(GetNodeByLookAheadIndex(lookAheadEdgeIndex), GetNodeByLookAheadIndex(lookAheadEdgeIndex + 1));
	}

	public bool GetEdgeByLookAheadIndex(int lookAheadEdgeIndex, out Edge edge)
	{
		if (lookAheadEdgeIndex < 0 || lookAheadEdgeIndex >= EdgeCount)
		{
			edge = new Edge(null, null);
			return false;
		}
		edge = new Edge(base.RacingLine.Node(base.NodeIndexQueue[lookAheadEdgeIndex]), base.RacingLine.Node(base.NodeIndexQueue[lookAheadEdgeIndex + 1]));
		return true;
	}

	public RaceNode GetNodeByLookAheadIndex(int lookAheadNodeIndex)
	{
		return base.RacingLine.Node(base.NodeIndexQueue[lookAheadNodeIndex]);
	}

	public float GetSignedDistanceFromPointToEdge(Vector2 p, int lookAheadEdgeIndex)
	{
		if (!GetEdgeByLookAheadIndex(lookAheadEdgeIndex, out var edge))
		{
			Debug.LogError("GetSignedDistanceFromPointToEdge: Invalid look-ahead-edge-index: " + lookAheadEdgeIndex);
		}
		return Vector2Ex.SignedDistanceFromPointToLine(p, edge.From.Pos, edge.To.Pos);
	}

	public float GetSignedDistanceFromPointToEdge(Vector2 p)
	{
		return GetSignedDistanceFromPointToEdge(p, 0);
	}

	public float GetCosineToEdge(Vector2 direction, int lookAheadEdgeIndex)
	{
		if (!GetEdgeByLookAheadIndex(lookAheadEdgeIndex, out var edge))
		{
			Debug.LogError("GetCosineToEdge: Invalid look-ahead-edge-index: " + lookAheadEdgeIndex);
		}
		return Vector2.Dot(direction, edge.Forward);
	}

	public float GetSignedDegreeToEdge(Vector2 direction, int lookAheadEdgeIndex)
	{
		if (!GetEdgeByLookAheadIndex(lookAheadEdgeIndex, out var edge))
		{
			Debug.LogError("GetCosineToEdge: Invalid look-ahead-edge-index: " + lookAheadEdgeIndex);
		}
		return Vector2Ex.SignedDegreeOfDirections(direction, edge.From.Pos, edge.To.Pos);
	}

	public float GetSignedDegreeToEdge(Vector2 direction)
	{
		return GetSignedDegreeToEdge(direction, 0);
	}

	private static int CheckSquareDistanceFromPositionToLineWithExtent(Vector2 position, Vector2 from, Vector2 to, ref float sqrDistance)
	{
		BareEdge bareEdge = BareEdge.Extend(4f, from, to);
		return Vector2Ex.SquareDistanceFromPointToLine(position, bareEdge.From, bareEdge.To, ref sqrDistance);
	}

	private bool PositionIsAtFirstLookAheadEdge(Vector2 position)
	{
		int lookAheadEdgeIndexOfPoint = GetLookAheadEdgeIndexOfPoint(position);
		return 0 == lookAheadEdgeIndexOfPoint;
	}

	private int CheckSquareDistanceFromPositionToLineWithExtent(Vector2 position, int indexFrom, int indexTo, ref float sqrDistance)
	{
		Vector2 pos = base.RacingLine.Node(indexFrom).Pos;
		Vector2 pos2 = base.RacingLine.Node(indexTo).Pos;
		return CheckSquareDistanceFromPositionToLineWithExtent(position, pos, pos2, ref sqrDistance);
	}

	private bool PositionInEdgeBlock(Vector2 position, int indexFrom, int indexTo)
	{
		RaceNode raceNode = base.RacingLine.Node(indexFrom);
		RaceNode raceNode2 = base.RacingLine.Node(indexTo);
		BareEdge bareEdge = BareEdge.Extend(4f, raceNode.Pos, raceNode2.Pos);
		return Vector2Ex.PointInEdgeBlock(position, bareEdge.From, raceNode.LeftWidth, raceNode.RightWidth, bareEdge.To, raceNode2.LeftWidth, raceNode2.RightWidth);
	}
}
