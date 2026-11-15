using System;
using System.Collections.Generic;
using GameNetwork;
using Misc;
using RaceProtocol;
using RaceTrack;
using UnityEngine;

public abstract class BaseMonsterBehaviour : MonoBehaviour, IAgent, IMonster
{
	public const float CheckConditionsWeakDistanceThreshold = 3f;

	public bool UseCollisionBody;

	protected Animation animationComponent;

	private Vector2 vel = Vector2.zero;

	private bool knockedOut;

	private bool knockedOutProcessed;

	private Quaternion beginRotation;

	private CCharacterCollisionBody collisionBody;

	private Dictionary<int, AudioSource> audios = new Dictionary<int, AudioSource>();

	private int indexInScene = -1;

	private Collider colliderComponent;

	private Transform cachedTransform;

	private GameObject cachedGameObject;

	private Renderer cachedRenderer;

	private static float m_LastPlaySound;

	public virtual Transform Transform
	{
		get
		{
			if (null == cachedTransform)
			{
				cachedTransform = GrabTransform;
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
				cachedGameObject = GrabGameObject;
			}
			return cachedGameObject;
		}
	}

	public abstract Transform GrabTransform { get; }

	public abstract GameObject GrabGameObject { get; }

	public virtual Animation Animation
	{
		get
		{
			if (animationComponent == null)
			{
				FindAndSetAnimation();
			}
			return animationComponent;
		}
	}

	public Renderer Renderer
	{
		get
		{
			if (null == cachedRenderer)
			{
				cachedRenderer = GetComponentInChildren<Renderer>();
			}
			return cachedRenderer;
		}
	}

	public bool RenderEnabled => Renderer.enabled;

	public virtual string Name => GameObject.name;

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

	public Vector2 Heading => Vector3Ex.To2D(Transform.forward).normalized;

	public Vector2 Vel
	{
		get
		{
			return vel;
		}
		set
		{
			vel = value;
		}
	}

	public bool KnockedOut
	{
		get
		{
			return knockedOut;
		}
		set
		{
			knockedOut = value;
		}
	}

	public bool ForceKnockedOut
	{
		set
		{
			KnockedOut = value;
			KnockedOutProcessed = false;
		}
	}

	public bool KnockedOutProcessed
	{
		get
		{
			return knockedOutProcessed;
		}
		set
		{
			knockedOutProcessed = value;
		}
	}

	public float Radius => -1f;

	public virtual bool ShouldSendMovementInfo => true;

	public virtual Collider Collider
	{
		get
		{
			if (null == colliderComponent)
			{
				colliderComponent = Transform.GetComponentInChildren<Collider>();
			}
			return colliderComponent;
		}
	}

	public ObjTimerManager<Car> CarTimerManager { get; set; }

	public float CarTimerLimitOfMonster { get; set; }

	public virtual bool ShouldBeAvoided => true;

	public BaseMonsterBehaviour(float carTimerLimit)
	{
		CarTimerLimitOfMonster = carTimerLimit;
		if (carTimerLimit >= 0f)
		{
			CarTimerManager = new ObjTimerManager<Car>();
		}
	}

	public abstract void UpdateState();

	public abstract void ClientOnReceiveRemoteMovementInfo(Vector2 position, Vector2 velocity, float rotAngle);

	public abstract void ClientOnReceiveRemoteMovementInfo(Vector3 position, Vector2 velocity, float rotAngle);

	public abstract void ClientOnReceiveStateInfo(int stateId);

	public abstract void ClientOnReceiveCommand(int commandId);

	public void HeadForPos(float speed, float time, Vector2 targetPos)
	{
		Vel = (targetPos - Pos).normalized * speed;
		Move(time);
		LookAt(targetPos);
	}

	public void MoveAroundPos(float speed, float time, float radius, Vector2 centerPos)
	{
		Vector2 pos = Pos;
		Vector2 v = pos - centerPos;
		float num = Vector2Ex.RadianOfVector(v);
		float num2 = speed * time / radius;
		float num3 = num + num2;
		float num4 = num3 + (float)Math.PI / 2f;
		Vector2 pos2 = centerPos + Vector2Ex.FromRadian(num3, radius);
		Vel = speed * Vector2Ex.FromRadian(num4);
		MoveTo(pos2);
		RotAngle = num4 * 57.29578f;
	}

	public bool PlaySound(int id, int priority)
	{
		if (!audios.TryGetValue(id, out var value))
		{
			value = SoundManager.Instance.AddAudio(base.gameObject, id);
			if (null != value)
			{
				audios.Add(id, value);
			}
		}
		if (null != value)
		{
			if (Time.time - m_LastPlaySound < 2f && iPhoneSettings.generation < iPhoneGeneration.iPad2Gen)
			{
				return true;
			}
			m_LastPlaySound = Time.time;
			SoundManager.Instance.PlayAudio(base.gameObject, value, priority);
			return true;
		}
		return false;
	}

	public bool StopSound(int id)
	{
		if (audios.ContainsKey(id))
		{
			SoundManager.Instance.StopAudio(audios[id]);
			return true;
		}
		return false;
	}

	public void Uninit()
	{
		if (collisionBody != null)
		{
			collisionBody.Uninit();
		}
	}

	public void UpdateCarTimerManager()
	{
		if (CarTimerManager.Mapping.Count == 0)
		{
			return;
		}
		foreach (IAutoCar vehicle in RaceWorld.Instance.Vehicles)
		{
			if (vehicle.IsDead)
			{
				CarTimerManager.Remove((Car)vehicle);
			}
		}
		CarTimerManager.Update(Time.deltaTime);
	}

	public virtual bool CheckAndHitCar(Car car, Action<Car> action)
	{
		if (car.IsDead)
		{
			return false;
		}
		if (!CarTimerManager.HasObject(car))
		{
			action(car);
			CarTimerManager.Add(car, CarTimerLimitOfMonster);
			return true;
		}
		return false;
	}

	public virtual bool CanHitVehicle(Car vehicle)
	{
		return false;
	}

	public Car CheckAndHitCar(Action<Car> action)
	{
		Car result = null;
		foreach (IAutoCar item in RaceWorld.Instance.VehiclesWeakNear(Pos, 3f))
		{
			if (!item.IsDead && !CarTimerManager.HasObject((Car)item) && CanHitVehicle((Car)item))
			{
				action((Car)item);
				CarTimerManager.Add((Car)item, CarTimerLimitOfMonster);
				result = (Car)item;
			}
		}
		return result;
	}

	public bool HasVehicleToHit()
	{
		foreach (IAutoCar item in RaceWorld.Instance.VehiclesWeakNear(Pos, 3f))
		{
			if (item.IsDead || CarTimerManager.HasObject((Car)item) || !CanHitVehicle((Car)item))
			{
				continue;
			}
			return true;
		}
		return false;
	}

	protected void Move(float time)
	{
		Move(Vel, time);
	}

	protected void Move3(float time)
	{
		Move(Vector2Ex.To3D(Vel, 0f), time);
	}

	protected void LookAt(Vector2 targetPos)
	{
		Vector3 worldPosition = Vector2Ex.To3D(targetPos, Position.y);
		Transform.LookAt(worldPosition);
	}

	protected void SaveBeginningRotation()
	{
		beginRotation = Transform.rotation;
	}

	protected void LookRotation(Vector2 targetPos, float t)
	{
		Vector2 vector = targetPos - Pos;
		Vector3 forward = new Vector3(vector.x, 0f, vector.y);
		Quaternion to = Quaternion.LookRotation(forward, Vector3.up);
		Transform.rotation = Quaternion.Slerp(beginRotation, to, t);
	}

	protected void PlayAnimation(string name)
	{
		Animation.CrossFade(name);
	}

	protected void StopAnimation()
	{
		Animation.Stop();
	}

	protected float AnimationLength(string name)
	{
		return Animation[name].length;
	}

	protected void PlayEffect(eEffectType type, Vector3 offset, float timeLength)
	{
		GameObject gameObject = EffectManager.Instance.CreateEffect(type, isPause: true, isOneShot: false);
		Effect component = gameObject.GetComponent<Effect>();
		component.AttachTo(Transform, offset, Vector3.zero);
		component.SetLifeTime(timeLength);
	}

	protected void PlayEffect(eEffectType type, float x, float y, float z, float timeLength)
	{
		PlayEffect(type, new Vector3(x, y, z), timeLength);
	}

	public virtual void StartState()
	{
		if (!NetworkManager.Instance.IsClient && UseCollisionBody)
		{
			CreateCollisionBody();
		}
	}

	protected void ServerSendStateInfo(int stateId)
	{
		MsgMonsterStateInfo message = MsgMonsterStateInfo.CreateBuilder().SetStateId(stateId).SetIndex(IndexInScene)
			.Build();
		NetworkManager.Instance.BroadCastMessage(message, reliable: true);
	}

	protected void ServerSendCommand(int commandId)
	{
		MsgMonsterCommand message = MsgMonsterCommand.CreateBuilder().SetCommandId(commandId).SetIndex(IndexInScene)
			.Build();
		NetworkManager.Instance.BroadCastMessage(message, reliable: true);
	}

	protected virtual void FindAndSetAnimation()
	{
		animationComponent = GameObject.GetComponentInChildren<Animation>();
		if (animationComponent == null)
		{
			Debug.LogError("BaseMonsterBehaviour.FindAndSetAnimation: Can't found the Animation component.");
		}
	}

	protected void CreateCollisionBody()
	{
		if (collisionBody != null)
		{
			Debug.LogWarning("BaseMonsterBehaviour.CreateCollisionBody: Collosion body exists!");
		}
		collisionBody = new CCharacterCollisionBody(GameObject, EnmCollisionLayer.CL_NPC);
	}

	protected void MoveTo(Vector2 pos)
	{
		float height = HeightMapManager.Instance.Height(pos.x, pos.y);
		MoveTo(pos, height);
	}

	protected void MoveTo(Vector2 pos, float height)
	{
		MoveTo(Vector2Ex.To3D(pos, height));
	}

	private void MoveTo(Vector3 position)
	{
		Transform.position = position;
	}

	private void Move(Vector2 velocity, float time)
	{
		MoveTo(Pos + velocity * time);
	}

	private void Move(Vector3 velocity, float time)
	{
		MoveTo(Position + velocity * time);
	}
}
