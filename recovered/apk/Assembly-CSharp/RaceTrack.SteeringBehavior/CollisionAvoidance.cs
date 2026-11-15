using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace RaceTrack.SteeringBehavior;

public class CollisionAvoidance
{
	private const float TimeToCollisionThreshold = 2f;

	private IObstacle firstTarget;

	private IList<IObstacle> targets;

	protected IAutoCar Vehicle { get; set; }

	public void AttachVehicle(IAutoCar vehicle)
	{
		Vehicle = vehicle;
	}

	public void AttachObstacles(IList<IObstacle> obstacles)
	{
		targets = obstacles;
	}

	public void DrawGizmos()
	{
		if (firstTarget != null)
		{
			Color color = Gizmos.color;
			Gizmos.color = Color.magenta;
			GizmosEx.DrawLine(Vehicle.Position, firstTarget.Pos);
			Gizmos.color = color;
		}
	}

	public virtual Vector2 Calculate()
	{
		float num = float.PositiveInfinity;
		IObstacle obstacle = null;
		float num2 = 0f;
		float num3 = 0f;
		Vector2 vector = Vector2.zero;
		Vector2 vector2 = Vector2.zero;
		foreach (IObstacle target in targets)
		{
			Vector2 vector3 = target.Pos - Vehicle.Pos;
			Vector2 vector4 = target.Vel - Vehicle.Vel;
			float sqrMagnitude = vector4.sqrMagnitude;
			float num4 = Mathf.Sqrt(sqrMagnitude);
			float num5 = Vector2.Dot(vector3, vector4) / sqrMagnitude;
			if (!(num5 > 2f))
			{
				float magnitude = vector3.magnitude;
				float num6 = magnitude - num4 * num;
				if (num6 > target.Radius + Vehicle.Radius && num5 < num)
				{
					num = num5;
					obstacle = target;
					num2 = num6;
					num3 = magnitude;
					vector = vector3;
					vector2 = vector4;
				}
			}
		}
		firstTarget = obstacle;
		if (obstacle == null)
		{
			return Vector2.zero;
		}
		return ((!(num2 <= 0f) && !(num3 < Vehicle.Radius + obstacle.Radius)) ? (vector + vector2 * num) : (obstacle.Pos - Vehicle.Pos)).normalized * Vehicle.MaxForce;
	}
}
