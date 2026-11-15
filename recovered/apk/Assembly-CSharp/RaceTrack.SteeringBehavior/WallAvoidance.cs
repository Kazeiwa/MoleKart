using System;
using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace RaceTrack.SteeringBehavior;

public class WallAvoidance
{
	private const float WallDetectionFeelerLength = 12f;

	private const int FeelerCount = 3;

	private IAutoCar vehicle;

	private Vector2[] feelers = new Vector2[3];

	public void AttachVehicle(IAutoCar vehicle)
	{
		this.vehicle = vehicle;
	}

	public Vector2 Calculate(IList<Wall> walls)
	{
		ref Vector2 reference = ref feelers[0];
		reference = vehicle.Pos + 12f * vehicle.Heading;
		ref Vector2 reference2 = ref feelers[1];
		reference2 = vehicle.Pos + 6f * Vector2Ex.Rotate(vehicle.Heading, 5.4977875f);
		ref Vector2 reference3 = ref feelers[2];
		reference3 = vehicle.Pos + 6f * Vector2Ex.Rotate(vehicle.Heading, (float)Math.PI / 4f);
		float dist = 0f;
		float num = float.PositiveInfinity;
		int num2 = -1;
		Vector2 result = Vector2.zero;
		Vector2 point = Vector2.zero;
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < feelers.Length; i++)
		{
			for (int j = 0; j < walls.Count; j++)
			{
				if (Vector2Ex.LineIntersection(vehicle.Pos, feelers[i], walls[j].From, walls[j].To, ref dist, ref point) && dist < num)
				{
					num = dist;
					num2 = j;
					vector = point;
				}
			}
			if (num2 >= 0)
			{
				Vector2 vector2 = feelers[i] - vector;
				result = walls[num2].Normal * vector2.magnitude;
			}
		}
		return result;
	}

	public void DrawGizmos()
	{
		Color color = Gizmos.color;
		Gizmos.color = Color.cyan;
		Vector2[] array = feelers;
		foreach (Vector2 to in array)
		{
			GizmosEx.DrawLine(vehicle.Position, to);
		}
		Gizmos.color = color;
	}
}
