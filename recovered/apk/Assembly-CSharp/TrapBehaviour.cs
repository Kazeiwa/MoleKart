using Misc;
using RaceTrack;
using UnityEngine;

public class TrapBehaviour : SimpleMonsterBehaviour
{
	public const float CarTimerLimit = 2f;

	private const float CarBesidesIDistance = 5f;

	public Collider trapCollider;

	public override Transform GrabTransform => base.transform;

	public override GameObject GrabGameObject => base.transform.parent.gameObject;

	public override bool ShouldSendMovementInfo => false;

	public override Collider Collider => trapCollider;

	public TrapColliderControl ColliderControl { get; set; }

	public TrapBehaviour()
		: base(2f)
	{
		ColliderControl = new TrapColliderControl(this);
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

	public void TrapEnterHighState()
	{
		ColliderControl.TrapEnterHighState();
	}

	public void TrapEnterGoingDownState()
	{
		ColliderControl.TrapEnterGoingDownState();
	}

	public void TrapEnterLowState()
	{
		PlayHitGroundSound();
		ColliderControl.TrapEnterLowState();
	}

	public override void StartState()
	{
		base.StartState();
		Animation.Stop();
		PlayAnimation("trap_idle");
	}

	public override void UpdateState()
	{
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

	protected void OnDrawGizmos()
	{
		GizmosEx.DrawText(Position, base.transform.localPosition.y.ToString());
	}

	protected void PlayHitGroundSound()
	{
		PlaySound(20502, 1);
	}

	private bool VehicleTrapRange(IAutoCar vehicle)
	{
		Vector3 vector = Transform.InverseTransformPoint(vehicle.Position);
		BoxCollider boxCollider = Collider as BoxCollider;
		Vector2 vector2 = new Vector2(boxCollider.size.x, boxCollider.size.y) / 2f + new Vector2(vehicle.Radius, vehicle.Radius);
		return vector.x < vector2.x && vector.x > 0f - vector2.x && vector.y < vector2.y && vector.y > 0f - vector2.y;
	}
}
