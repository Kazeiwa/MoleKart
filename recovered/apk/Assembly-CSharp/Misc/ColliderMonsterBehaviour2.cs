using RaceTrack;
using UnityEngine;

namespace Misc;

public abstract class ColliderMonsterBehaviour2 : MonsterBehaviour
{
	private float radius = -1f;

	public new float Radius
	{
		get
		{
			if (radius < 0f)
			{
				radius = ColliderUtil.Radius(Collider);
			}
			return radius;
		}
	}

	public TrickyColliderControl2 ColliderControl { get; set; }

	public virtual bool ShouldDisableClientPhysics => true;

	public ColliderMonsterBehaviour2(float carTimerLimit)
		: base(carTimerLimit)
	{
		ColliderControl = new TrickyColliderControl2(this);
		ColliderControl.MakeFSM();
	}

	public override void UpdateState()
	{
		UpdateCarTimerManager();
		ColliderControl.Update();
		base.UpdateState();
	}

	public void DisablePhysics()
	{
		Collider.enabled = false;
	}

	public void EnablePhysics()
	{
		Collider.enabled = true;
	}

	public bool IsNotBesideAnyVehicle()
	{
		bool result = true;
		foreach (IAutoCar item in RaceWorld.Instance.VehiclesWeakNear(Pos, 3f))
		{
			if (Vector3.Distance(item.Position, Position) < Radius + item.Radius)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	public void ColliderControlBeKnockedOut()
	{
		ColliderControl.BeKnockedOut();
	}

	public void ColliderControlStandUp()
	{
		ColliderControl.StandUp();
	}

	public void ColliderControlUpdate()
	{
		ColliderControl.Update();
	}
}
