using System.Collections.Generic;
using System.Diagnostics;
using Misc;
using UnityEngine;

namespace RaceTrack;

public class PathLookAhead : PathLookAheadBase
{
	public const float EdgeExtentLength = 4f;

	public int NodeCount
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int EdgeCount
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public RaceNode TargetNode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public RaceNode NextTargetNode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 TargetDirectionY
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 NextTargetDirectionY
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void Reset(Vector2 position)
	{
	}

	public void DrawGizmos()
	{
	}

	[DebuggerHidden]
	public IEnumerable<RaceNode> Nodes()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DebuggerHidden]
	public IEnumerable<RichEdge> Edges()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DebuggerHidden]
	public IEnumerable<RichEdge> EdgesInReverseOrder()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetLookAheadEdgeIndexOfPoint(Vector2 p)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public RichEdge GetEdgeByLookAheadIndex(int lookAheadEdgeIndex)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetSignedDistanceFromPointToEdge(Vector2 p, int lookAheadEdgeIndex)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetSignedDistanceFromPointToEdge(Vector2 p)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool GetCosineAndSineToEdge(Vector2 direction, int lookAheadEdgeIndex, out float c, out float s)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool GetCosineAndSineToEdge(Vector2 direction, out float c, out float s)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool GlobalToLookAheadPos(Vector2 globalPos, out Vector2 localPos, out float leftWidth, out float rightWidth)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool GlobalToLookAheadPos(Vector2 globalPos, out LookAheadInfo lookAheadInfo)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
