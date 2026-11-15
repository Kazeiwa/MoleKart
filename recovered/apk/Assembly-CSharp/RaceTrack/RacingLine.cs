using Misc;
using UnityEngine;

namespace RaceTrack;

public class RacingLine : RaceGraph
{
	private int endIndex = -1;

	private CrossOverEdge crossOverEdge;

	public int EndIndex
	{
		get
		{
			return endIndex;
		}
		set
		{
			endIndex = value;
		}
	}

	public RaceNode EndNode => Node(EndIndex);

	public CrossOverEdge CrossOverEdge => crossOverEdge;

	public float LapLength => EndNode.DistanceFromGoal;

	public void SetDistanceFromGoalOnNode(int nodeIndex)
	{
		SetDistanceFromGoalOnNode(nodeIndex, EndIndex);
	}

	public bool IsGoingReversely(Vector2 position, Vector2 direction)
	{
		bool result = false;
		foreach (RichEdge item in RichEdgesNearPos(position))
		{
			if (item.PositionInEdgeBlock(position))
			{
				result = true;
				if (Vector2.Dot(direction, item.Vector) >= 0f)
				{
					return false;
				}
			}
		}
		return result;
	}

	public float GetDistanceFromGoal(Vector2 position)
	{
		int index = QueryClosestNode(position).Index;
		return Node(index).DistanceFromGoal;
	}

	public float GetDistanceFromBeginning(Vector2 position, int laps, CrossOverEdge.AreaIndex index, out float distanceInLapFromGoal)
	{
		if (laps <= 0)
		{
			distanceInLapFromGoal = 0f;
			return 0f;
		}
		int index2 = QueryClosestNode(position).Index;
		Vector2 pos = Node(index2).Pos;
		int to = QueryEdgesByFromNode(index2)[0].To;
		RaceNode raceNode = Node(to);
		Vector2 pos2 = raceNode.Pos;
		Vector2 normalized = (pos2 - pos).normalized;
		float num = ((index != CrossOverEdge.AreaIndex.Left || (to != EndIndex && index2 != EndIndex)) ? raceNode.DistanceFromGoal : 0f);
		distanceInLapFromGoal = num + Vector2.Dot(normalized, pos2 - position);
		float b = (float)laps * LapLength - distanceInLapFromGoal;
		return Mathf.Max(0f, b);
	}

	public int RandomGetNextNodeIndexOfNode(int index)
	{
		int num = NextNodeCount(index);
		int nth = ((num != 1) ? Random.Range(0, num) : 0);
		return NextNodeIndexOfNode(index, nth);
	}

	public RaceNode RandomGetNextNodeOfNode(int index)
	{
		int index2 = RandomGetNextNodeIndexOfNode(index);
		return Node(index2);
	}

	public void CreateCrossOverEdge(Vector2 fromPos, Vector2 toPos)
	{
		crossOverEdge = new CrossOverEdge(fromPos, toPos);
	}

	private float SetDistanceFromGoalOnNode(int nodeIndex, int goalIndex)
	{
		RaceNode raceNode = Node(nodeIndex);
		if (raceNode.DistanceFromGoal < float.PositiveInfinity)
		{
			return raceNode.DistanceFromGoal;
		}
		Vector2 pos = raceNode.Pos;
		float num = 0f;
		foreach (RaceNode item in NextNodes(nodeIndex))
		{
			float num2 = ((item.Index != goalIndex) ? SetDistanceFromGoalOnNode(item.Index, goalIndex) : 0f);
			num = Mathf.Max(num, num2 + Vector2Ex.Distance(pos, item.Pos));
		}
		raceNode.DistanceFromGoal = num;
		return num;
	}
}
