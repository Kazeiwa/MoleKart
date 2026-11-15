using Misc;
using RaceTrack;
using UnityEngine;

public class TollGateBehaviour : SimpleMonsterBehaviour
{
	private enum WaitTimerResult
	{
		Wait,
		Start,
		NoWait
	}

	public const float CarTimerLimit = 2f;

	public Collider TollGateCollider;

	public TrafficLightBehaviour TrafficLight;

	public float WaitTimeLength;

	private CrudeElapsedTimer waitTimer;

	public override Transform GrabTransform => base.transform;

	public override GameObject GrabGameObject => base.transform.parent.gameObject;

	public override bool ShouldBeAvoided => false;

	public override bool ShouldSendMovementInfo => false;

	public override Collider Collider => TollGateCollider;

	public TollGateColliderControl ColliderControl { get; set; }

	public TollGateBehaviour()
		: base(2f)
	{
		ColliderControl = new TollGateColliderControl(this);
		ColliderControl.MakeFSM();
	}

	public void ColliderBecomeTrigger()
	{
		Collider.enabled = true;
		Collider.isTrigger = true;
	}

	public void ColliderBecomeBlocker()
	{
		Collider.enabled = true;
		Collider.isTrigger = false;
	}

	public void ColliderBecomeNothing()
	{
		Collider.enabled = false;
	}

	public void EnterHighState()
	{
		ColliderControl.EnterHighState();
		TrafficLight.BecomeGreen();
	}

	public void EnterGoingDownState()
	{
		ColliderControl.EnterGoingDownState();
	}

	public void EnterLowState()
	{
		ColliderControl.EnterLowState();
	}

	public void LightBecameRed()
	{
		TrafficLight.BecomeRed();
	}

	public void PlayCloseSound()
	{
		PlaySound(20802, 1);
	}

	public override void StartState()
	{
		base.StartState();
		waitTimer = new CrudeElapsedTimer(WaitTimeLength);
	}

	public override void UpdateState()
	{
		switch (TryUpdateWaitTimer())
		{
		case WaitTimerResult.Wait:
			return;
		case WaitTimerResult.Start:
			Animation.Stop();
			PlayAnimation("tollgate_idle");
			break;
		}
		ColliderControl.Update();
		base.UpdateState();
		UpdateCarTimerManager();
	}

	public void HitCar(Car car)
	{
		CheckAndHitCar(car, delegate(Car c)
		{
			c.PressByTrap();
		});
	}

	public bool IsNotBesideAnyVehicle()
	{
		bool result = true;
		foreach (IAutoCar item in RaceWorld.Instance.VehiclesWeakNear(Pos, 7f))
		{
			if (VehicleTrapRange(item))
			{
				result = false;
				break;
			}
		}
		return result;
	}

	public void AddCollider()
	{
		Transform transform = base.transform;
		Transform parent = transform.parent;
		BoxCollider boxCollider = GameObjectUtil.InsertGameObject<BoxCollider>(transform.localPosition, transform.localRotation, transform.name + "_collider", parent);
		boxCollider.isTrigger = true;
		boxCollider.center = new Vector3(2f, 0f, 0f);
		boxCollider.size = new Vector3(4.2f, 4f, 0.2f);
		Rigidbody rigidbody = boxCollider.gameObject.AddComponent<Rigidbody>();
		rigidbody.mass = 1E+09f;
		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;
		TollGateTriggerForwarder tollGateTriggerForwarder = boxCollider.gameObject.AddComponent<TollGateTriggerForwarder>();
		TollGateCollider = boxCollider;
		tollGateTriggerForwarder.Behaviour = this;
	}

	protected void Awake()
	{
		AddCollider();
	}

	protected override void FindAndSetAnimation()
	{
		animationComponent = base.animation;
		if (animationComponent == null)
		{
			Debug.LogError("TrapBehaviour.FindAndSetAnimation: Can't found the Animation component.");
		}
	}

	private bool VehicleTrapRange(IAutoCar vehicle)
	{
		Vector3 vector = Transform.InverseTransformPoint(vehicle.Position);
		BoxCollider boxCollider = Collider as BoxCollider;
		Vector2 vector2 = new Vector2(boxCollider.size.x, boxCollider.size.y) / 2f + new Vector2(vehicle.Radius, vehicle.Radius);
		return vector.x < vector2.x && vector.x > 0f - vector2.x && vector.y < vector2.y && vector.y > 0f - vector2.y;
	}

	private WaitTimerResult TryUpdateWaitTimer()
	{
		if (waitTimer != null)
		{
			waitTimer.Advance(Time.deltaTime);
			if (waitTimer.TimeOutCount > 0)
			{
				waitTimer = null;
				return WaitTimerResult.Start;
			}
			return WaitTimerResult.Wait;
		}
		return WaitTimerResult.NoWait;
	}
}
