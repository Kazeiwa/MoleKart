using Google.ProtocolBuffers;
using Misc;
using UnityEngine;

namespace RaceTrack;

public interface IMonster : IAgent
{
	bool KnockedOut { get; set; }

	bool ForceKnockedOut { set; }

	float RotAngle { get; set; }

	Vector2 Vel { get; }

	GameObject GameObject { get; }

	string Name { get; }

	int IndexInScene { get; set; }

	float Radius { get; }

	bool ShouldBeAvoided { get; }

	bool ShouldSendMovementInfo { get; }

	MonsterNetworking.MovementInfoType MovementInfoType { get; }

	IMessageLite CreateMovementInfo();

	void ClientOnReceiveRemoteMovementInfoCircular1(float t);

	void ClientOnReceiveRemoteMovementInfo(Vector2 position, Vector2 velocity, float rotAngle);

	void ClientOnReceiveRemoteMovementInfo(Vector3 position, Vector2 velocity, float rotAngle);

	void ClientOnReceiveStateInfo(int stateId);

	void ClientOnReceiveCommand(int commandId);

	void StartState();

	void UpdateState();

	void Uninit();
}
