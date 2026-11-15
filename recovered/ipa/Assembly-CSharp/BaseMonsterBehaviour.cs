using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Google.ProtocolBuffers;
using Misc;
using RaceTrack;
using UnityEngine;

public abstract class BaseMonsterBehaviour : MonoBehaviour, IAgent, IMonster
{
	public const float CheckConditionsWeakDistanceThreshold = 3f;

	public bool UseCollisionBody;

	protected Animation animationComponent;

	private Vector2 vel;

	private bool knockedOut;

	private bool knockedOutProcessed;

	private Quaternion beginRotation;

	private CCharacterCollisionBody collisionBody;

	private Dictionary<int, AudioSource> audios;

	private int indexInScene;

	private Collider colliderComponent;

	private Transform cachedTransform;

	private GameObject cachedGameObject;

	private Renderer cachedRenderer;

	protected Dictionary<eEffectType, Effect> m_eftList;

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

	public abstract Transform GrabTransform { get; }

	public abstract GameObject GrabGameObject { get; }

	public virtual Animation Animation
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Renderer Renderer
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool RenderEnabled
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual string Name
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

	public Vector2 Heading
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Vel
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

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

	public bool KnockedOutProcessed
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

	public virtual bool ShouldSendMovementInfo
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

	public virtual Collider Collider
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public ObjTimerManager<Car> CarTimerManager
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public float CarTimerLimitOfMonster
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public virtual bool ShouldBeAvoided
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public BaseMonsterBehaviour(float carTimerLimit)
	{
	}

	public abstract void UpdateState();

	public virtual IMessageLite CreateMovementInfo()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void ClientOnReceiveRemoteMovementInfoCircular1(float t)
	{
	}

	public abstract void ClientOnReceiveRemoteMovementInfo(Vector2 position, Vector2 velocity, float rotAngle);

	public abstract void ClientOnReceiveRemoteMovementInfo(Vector3 position, Vector2 velocity, float rotAngle);

	public abstract void ClientOnReceiveStateInfo(int stateId);

	public abstract void ClientOnReceiveCommand(int commandId);

	public void HeadForPos(float speed, float time, Vector2 targetPos)
	{
	}

	public void MoveAroundPos(float speed, float time, float radius, Vector2 centerPos)
	{
	}

	public bool PlaySound(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool StopSound(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Uninit()
	{
	}

	public void UpdateCarTimerManager()
	{
	}

	public virtual bool CheckAndHitCar(Car car, Action<Car> action)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual bool CanHitVehicle(Car vehicle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Car CheckAndHitCar(Action<Car> action)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool HasVehicleToHit()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void Move(float time)
	{
	}

	protected void Move3(float time)
	{
	}

	protected void LookAt(Vector2 targetPos)
	{
	}

	protected void SaveBeginningRotation()
	{
	}

	protected void LookRotation(Vector2 targetPos, float t)
	{
	}

	protected void PlayAnimation(string name)
	{
	}

	protected void StopAnimation()
	{
	}

	protected float AnimationLength(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void PlayEffect(eEffectType type, Vector3 offset, float timeLength)
	{
	}

	protected void PlayEffect(eEffectType type, float x, float y, float z, float timeLength)
	{
	}

	public virtual void StartState()
	{
	}

	protected void ServerSendStateInfo(int stateId)
	{
	}

	protected void ServerSendCommand(int commandId)
	{
	}

	protected void PeerSendCommand(int commandId)
	{
	}

	protected virtual void FindAndSetAnimation()
	{
	}

	protected void CreateCollisionBody()
	{
	}

	protected void MoveTo(Vector2 pos)
	{
	}

	protected void MoveTo(Vector2 pos, float height)
	{
	}

	private void MoveTo(Vector3 position)
	{
	}

	private void Move(Vector2 velocity, float time)
	{
	}

	private void Move(Vector3 velocity, float time)
	{
	}
}
