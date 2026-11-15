using UnityEngine;

namespace RaceTrack;

public interface IObstacle
{
	float Radius { get; }

	Vector2 Pos { get; }

	Vector2 Vel { get; }
}
