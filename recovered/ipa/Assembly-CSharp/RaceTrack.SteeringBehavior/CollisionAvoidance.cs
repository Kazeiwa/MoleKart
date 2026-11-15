using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RaceTrack.SteeringBehavior;

public class CollisionAvoidance
{
	private const float TimeToCollisionThreshold = 2f;

	private IObstacle firstTarget;

	private IList<IObstacle> targets;

	protected IAutoCar Vehicle
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

	public void AttachVehicle(IAutoCar vehicle)
	{
	}

	public void AttachObstacles(IList<IObstacle> obstacles)
	{
	}

	public void DrawGizmos()
	{
	}

	public virtual Vector2 Calculate()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
