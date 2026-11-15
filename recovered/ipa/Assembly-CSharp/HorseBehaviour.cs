using System.Runtime.CompilerServices;
using UnityEngine;

public class HorseBehaviour : HorseBaseBehaviour
{
	private const int AvailableTargetCount = 8;

	public float WalkRadius;

	public Vector2 Home;

	public int TargetId
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

	public override void SetTargetPos()
	{
	}

	protected virtual void OnDrawGizmos()
	{
	}
}
