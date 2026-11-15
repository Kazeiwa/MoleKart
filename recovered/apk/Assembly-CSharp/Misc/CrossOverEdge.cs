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

	public Vector2 FromPos => fromPos;

	public Vector2 ToPos => toPos;

	public CrossOverEdge(Vector2 fromPos, Vector2 toPos)
	{
		SetEdge(fromPos, toPos);
	}

	public void UpdateLapCountAndAreaIndex(Vector2 position, ref int lapCount, ref AreaIndex areaIndex)
	{
		AreaIndex areaIndex2 = CalculateAreaIndx(position);
		switch (areaIndex)
		{
		case AreaIndex.Left:
			if (areaIndex2 == AreaIndex.Right)
			{
				lapCount++;
			}
			break;
		case AreaIndex.Right:
			if (areaIndex2 == AreaIndex.Left)
			{
				lapCount--;
			}
			break;
		}
		areaIndex = areaIndex2;
	}

	public void SetEdge(Vector2 fromPos, Vector2 toPos)
	{
		this.fromPos = fromPos;
		this.toPos = toPos;
		forward = (toPos - fromPos).normalized;
		left = Vector2Ex.RotateLeft(forward);
		length = (toPos - fromPos).magnitude;
	}

	public AreaIndex CalculateAreaIndx(Vector2 position)
	{
		Vector2 lhs = position - fromPos;
		float num = Vector2.Dot(lhs, forward);
		if (num < 0f || num > length)
		{
			return AreaIndex.None;
		}
		float num2 = Vector2.Dot(lhs, left);
		return (num2 > 0f) ? AreaIndex.Left : AreaIndex.Right;
	}
}
