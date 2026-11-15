using UnityEngine;

namespace RaceTrack;

public class Wall
{
	public Vector2 From { get; set; }

	public Vector2 To { get; set; }

	public Vector2 Normal { get; set; }

	public Vector2 Center => (From + To) * 0.5f;

	public Wall(Vector2 from, Vector2 to)
	{
		From = from;
		To = to;
		CalculateNormal();
	}

	private void CalculateNormal()
	{
		Vector2 vector = To - From;
		vector.Normalize();
		Normal = new Vector2(0f - vector.y, vector.x);
	}
}
