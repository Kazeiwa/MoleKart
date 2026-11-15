using UnityEngine;

namespace Misc;

public class LapCounter
{
	private CrossOverEdge.AreaIndex currentAreaIndex;

	private int lapCount;

	private CrossOverEdge crossOverEdge;

	public int LapCount => lapCount;

	public CrossOverEdge.AreaIndex CurrentAreaIndex => currentAreaIndex;

	public LapCounter(CrossOverEdge crossOverEdge)
	{
		this.crossOverEdge = crossOverEdge;
		currentAreaIndex = CrossOverEdge.AreaIndex.None;
	}

	public void InputPosition(Vector2 position)
	{
		crossOverEdge.UpdateLapCountAndAreaIndex(position, ref lapCount, ref currentAreaIndex);
	}
}
