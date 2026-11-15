using System.Runtime.CompilerServices;

namespace Misc;

public abstract class ColliderMonsterBehaviour2 : MonsterBehaviour
{
	private float radius;

	public new float Radius
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public TrickyColliderControl2 ColliderControl
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

	public virtual bool ShouldDisableClientPhysics
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public ColliderMonsterBehaviour2(float carTimerLimit)
	{
	}

	public override void UpdateState()
	{
	}

	public void DisablePhysics()
	{
	}

	public void EnablePhysics()
	{
	}

	public bool IsNotBesideAnyVehicle()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ColliderControlBeKnockedOut()
	{
	}

	public void ColliderControlStandUp()
	{
	}

	public void ColliderControlUpdate()
	{
	}
}
