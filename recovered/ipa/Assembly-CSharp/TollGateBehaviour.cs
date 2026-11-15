using System.Runtime.CompilerServices;
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

	public override Transform GrabTransform
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override GameObject GrabGameObject
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override bool ShouldBeAvoided
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override bool ShouldSendMovementInfo
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override Collider Collider
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public TollGateColliderControl ColliderControl
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

	public void ColliderBecomeTrigger()
	{
	}

	public void ColliderBecomeBlocker()
	{
	}

	public void ColliderBecomeNothing()
	{
	}

	public void EnterHighState()
	{
	}

	public void EnterGoingDownState()
	{
	}

	public void EnterLowState()
	{
	}

	public void LightBecameRed()
	{
	}

	public void PlayCloseSound()
	{
	}

	public override void StartState()
	{
	}

	public override void UpdateState()
	{
	}

	public void HitCar(Car car)
	{
	}

	public bool IsNotBesideAnyVehicle()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void AddCollider()
	{
	}

	protected void Awake()
	{
	}

	protected override void FindAndSetAnimation()
	{
	}

	private bool VehicleTrapRange(IAutoCar vehicle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private WaitTimerResult TryUpdateWaitTimer()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
