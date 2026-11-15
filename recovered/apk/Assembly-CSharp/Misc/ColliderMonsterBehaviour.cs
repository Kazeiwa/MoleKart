using GameNetwork;
using RaceTrack;
using UnityEngine;

namespace Misc;

public abstract class ColliderMonsterBehaviour : MonsterBehaviour
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

	public TrickyColliderControl ColliderControl { get; set; }

	public virtual bool ShouldDisableClientPhysics => true;

	public ColliderMonsterBehaviour(float carTimerLimit)
		: base(carTimerLimit)
	{
		if (!NetworkManager.Instance.IsClient)
		{
			ColliderControl = new TrickyColliderControl(this);
			ColliderControl.MakeFSM();
		}
	}

	protected override void UpdateEntity()
	{
		ColliderControl.Update();
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

	protected void Awake()
	{
		if (NetworkManager.Instance.IsClient && ShouldDisableClientPhysics)
		{
			DisablePhysics();
		}
	}
}
