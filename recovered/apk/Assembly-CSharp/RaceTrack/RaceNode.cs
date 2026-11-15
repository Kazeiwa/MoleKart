using UnityEngine;

namespace RaceTrack;

public class RaceNode
{
	public const int InvalidNodeIndex = -1;

	public int Index { get; set; }

	public Vector2 Pos { get; set; }

	public float MaxSpeed { get; set; }

	public float LeftWidth { get; set; }

	public float RightWidth { get; set; }

	public float DistanceFromGoal { get; set; }

	public RaceNode(int idx, Vector2 pos)
	{
		Index = idx;
		Pos = pos;
		MaxSpeed = float.PositiveInfinity;
		LeftWidth = 2f;
		RightWidth = 2f;
		DistanceFromGoal = float.PositiveInfinity;
	}

	public bool MatchPosition(Vector2 pos)
	{
		return Pos == pos;
	}
}
