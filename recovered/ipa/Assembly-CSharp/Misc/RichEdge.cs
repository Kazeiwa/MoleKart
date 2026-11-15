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

	public Vector2 FromPos
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 ToPos
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Vector
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Forward
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Right
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float Magnitude
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Bounds2D Bounds
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public ClockwiseQuadrangle Quadrangle
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float MinLeftWidth
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float MinRightWidth
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public RichEdge(RaceNode from, RaceNode to)
	{
	}

	public override int GetHashCode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool PositionInEdgeBlock(Vector2 position)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector2 GlobalToLocalVector(Vector2 globalPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float SignedDistanceFromPointToLine(Vector2 p)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector2 GlobalToLocalLookAheadPos(Vector2 globalPos, out float leftWidth, out float rightWidth)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public LookAheadInfo GlobalToLocalLookAheadInfo(Vector2 globalPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void OnDrawGizmos()
	{
	}
}
