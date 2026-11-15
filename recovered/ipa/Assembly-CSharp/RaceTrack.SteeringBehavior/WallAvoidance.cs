using System.Collections.Generic;
using UnityEngine;

namespace RaceTrack.SteeringBehavior;

public class WallAvoidance
{
	private const float WallDetectionFeelerLength = 12f;

	private const int FeelerCount = 3;

	private IAutoCar vehicle;

	private Vector2[] feelers;

	public void AttachVehicle(IAutoCar vehicle)
	{
	}

	public Vector2 Calculate(IList<Wall> walls)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void DrawGizmos()
	{
	}
}
