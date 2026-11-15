using Misc;
using UnityEngine;

public class CraterBehaviour : MonoBehaviour
{
	public const float IdleTimeLength = 8f;

	public const float FireBallExplosionTimeLength = 5f;

	public const float FireBallSpeed = 200f;

	public RectangleBehaviour[] rects;

	private Transform cachedTransform;

	private CrudeElapsedTimer timer;

	public Transform Transform
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 Position
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void SpitFireBalls()
	{
	}

	protected void Update()
	{
	}
}
