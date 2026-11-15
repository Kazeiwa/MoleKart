using System.Collections.Generic;
using UnityEngine;

namespace RaceTrack.SteeringBehavior;

public class Separation
{
	public static Vector2 Calculate(IAutoCar self, List<IAutoCar> neighbors)
	{
		Vector2 result = new Vector2(0f, 0f);
		foreach (IAutoCar neighbor in neighbors)
		{
			if (neighbor != self)
			{
				Vector2 vector = self.Pos - neighbor.Pos;
				result += vector / vector.sqrMagnitude;
			}
		}
		return result;
	}
}
