using Misc;
using UnityEngine;

namespace RaceTrack;

public class PathLookAheadBase
{
	protected const int Capacity = 5;

	private RacingLine racingLine = RaceWorld.Instance.RacingLine;

	public bool Available => NodeIndexQueue.Size > 1;

	public CircularBuffer<int> NodeIndexQueue { get; set; }

	protected RacingLine RacingLine => racingLine;

	public PathLookAheadBase()
	{
		NodeIndexQueue = new CircularBuffer<int>(5);
	}

	public void Reset(int index)
	{
		Reset();
		NodeIndexQueue.Add(index);
	}

	public void Reset()
	{
		NodeIndexQueue.Clear();
	}

	protected void Advance()
	{
		int last = NodeIndexQueue.Last;
		int max = RacingLine.NextNodeCount(last);
		int nth = Random.Range(0, max);
		int element = RacingLine.NextNodeIndexOfNode(last, nth);
		NodeIndexQueue.Add(element);
	}

	protected void Advance(int count)
	{
		while (count-- > 0)
		{
			Advance();
		}
	}
}
