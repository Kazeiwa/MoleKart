using RaceTrack;
using UnityEngine;

public class Obstacle2D : IObstacle
{
	private float radius;

	private Transform transform;

	public float Radius => radius;

	public Vector2 Pos
	{
		get
		{
			if (null == transform)
			{
				return Vector2.zero;
			}
			Vector3 position = transform.position;
			return new Vector2(position.x, position.z);
		}
	}

	public Vector2 Vel => Vector2.zero;

	public Obstacle2D(Transform transform, float radius)
	{
		this.transform = transform;
		this.radius = radius;
	}
}
