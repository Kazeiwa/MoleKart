using Misc;
using RaceTrack;
using UnityEngine;

public class DummyMonsterBehaviour : MonoBehaviour, IAgent, IMonster
{
	private int indexInScene = -1;

	private Transform cachedTransform;

	private GameObject cachedGameObject;

	public bool KnockedOut
	{
		get
		{
			return false;
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

	public Vector3 Position => Transform.position;

	public Vector2 Pos => Vector3Ex.To2D(Position);

	public float RotAngle
	{
		get
		{
			return 90f - Transform.eulerAngles.y;
		}
		set
		{
			Transform.eulerAngles = new Vector3(0f, 90f - value, 0f);
		}
	}

	public Vector2 Vel => Vector2.zero;

	public virtual Transform Transform
	{
		get
		{
			if (null == cachedTransform)
			{
				cachedTransform = base.transform;
			}
			return cachedTransform;
		}
	}

	public virtual GameObject GameObject
	{
		get
		{
			if (null == cachedGameObject)
			{
				cachedGameObject = Transform.gameObject;
			}
			return cachedGameObject;
		}
	}

	public string Name => GameObject.name;

	public int IndexInScene
	{
		get
		{
			return indexInScene;
		}
		set
		{
			indexInScene = value;
		}
	}

	public float Radius => 3f;

	public bool ShouldBeAvoided => true;

	public bool ShouldSendMovementInfo => false;

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
		Color color = Gizmos.color;
		Gizmos.color = Color.white;
		Gizmos.DrawSphere(base.transform.position, 1.5f);
		Gizmos.color = Color.yellow;
		GizmosEx.DrawCircle(Position, Radius);
		GizmosEx.DrawText(Position, Name);
		Gizmos.color = color;
	}
}
