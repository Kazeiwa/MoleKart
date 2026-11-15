using UnityEngine;

namespace RaceTrack;

public interface IAgent
{
	Vector2 Pos { get; }

	Vector3 Position { get; }
}
