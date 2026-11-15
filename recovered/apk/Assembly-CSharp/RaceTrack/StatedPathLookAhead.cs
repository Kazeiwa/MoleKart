using UnityEngine;

namespace RaceTrack;

public class StatedPathLookAhead : PathLookAhead
{
	public Vector2 Pos { get; set; }

	public new void Update(Vector2 position)
	{
		Pos = position;
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
}
