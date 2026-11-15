using System.Runtime.CompilerServices;
using Misc;
using RaceTrack;
using UnityEngine;

public class TrapBehaviour : SimpleMonsterBehaviour
{
	public const float CarTimerLimit = 2f;

	public Collider trapCollider;

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

	public TrapColliderControl ColliderControl
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

	public void TrapEnterHighState()
	{
	}

	public void TrapEnterGoingDownState()
	{
	}

	public void TrapEnterLowState()
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

	protected void OnDrawGizmos()
	{
	}

	protected void PlayHitGroundSound()
	{
	}

	protected override void FindAndSetAnimation()
	{
	}

	private bool VehicleTrapRange(IAutoCar vehicle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
