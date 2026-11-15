using System.Collections.Generic;
using UnityEngine;

namespace RaceTrack;

public class Chain
{
	public List<Vector2> Vectors = new List<Vector2>();

	public void Add(Vector2 v, bool reverse)
	{
		if (reverse)
		{
			Vectors.Insert(0, v);
		}
		else
		{
			Vectors.Add(v);
		}
	}
}
