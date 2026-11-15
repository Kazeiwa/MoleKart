using UnityEngine;

namespace Misc;

public class CrossOverEdge
{
	public enum AreaIndex
	{
		None,
		Left,
		Right
	}

	private Vector2 fromPos;

	private Vector2 toPos;

	private Vector2 forward;

	private Vector2 left;

	private float length;

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

	public CrossOverEdge(Vector2 fromPos, Vector2 toPos)
	{
	}

	public void UpdateLapCountAndAreaIndex(Vector2 position, ref int lapCount, ref AreaIndex areaIndex)
	{
	}

	public void SetEdge(Vector2 fromPos, Vector2 toPos)
	{
	}

	public AreaIndex CalculateAreaIndx(Vector2 position)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
