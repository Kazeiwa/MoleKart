using UnityEngine;

namespace RaceTrack;

public interface IAutoCar : IAgent
{
	float MaxSpeed { get; }

	float MaxForce { get; }

	float Mass { get; }

	Vector2 Vel { get; }

	Vector2 Heading { get; }

	Vector2 Right { get; }

	float Radius { get; }

	float CurrentMinimalCurveRadius { get; }

	PlayerAIProfile AIProfile { get; }

	bool IsDead { get; }

	bool IsInPowerStatus { get; }

	string Name { get; }

	float GetMaxSteeringAngle(float speed);

	float GetMinimalCurveRadius(float speed);

	void InitSteeringBehavior(RacingLine racingLine);

	void UpdateAISteering();

	void ResetPathFollowing();
}
