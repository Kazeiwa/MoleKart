using Google.ProtocolBuffers;
using Misc;
using RaceTrack;
using UnityEngine;

public class DummyMonsterBehaviour : MonoBehaviour, IAgent, IMonster
{
	private int indexInScene;

	private Transform cachedTransform;

	private GameObject cachedGameObject;

	public bool KnockedOut
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public bool ForceKnockedOut
	{
		set
		{
		}
	}

	public Vector3 Position
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Pos
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float RotAngle
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public Vector2 Vel
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual Transform Transform
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual GameObject GameObject
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public string Name
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int IndexInScene
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float Radius
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool ShouldBeAvoided
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool ShouldSendMovementInfo
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual MonsterNetworking.MovementInfoType MovementInfoType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IMessageLite CreateMovementInfo()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ClientOnReceiveRemoteMovementInfoCircular1(float t)
	{
	}

	public void ClientOnReceiveRemoteMovementInfo(Vector2 position, Vector2 velocity, float rotAngle)
	{
	}

	public void ClientOnReceiveRemoteMovementInfo(Vector3 position, Vector2 velocity, float rotAngle)
	{
	}

	public void ClientOnReceiveStateInfo(int stateId)
	{
	}

	public void ClientOnReceiveCommand(int commandId)
	{
	}

	public void StartState()
	{
	}

	public void UpdateState()
	{
	}

	public void Uninit()
	{
	}

	protected void OnDrawGizmos()
	{
	}
}
