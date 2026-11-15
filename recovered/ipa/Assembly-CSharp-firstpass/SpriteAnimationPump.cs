using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class SpriteAnimationPump : MonoBehaviour
{
	private static SpriteAnimationPump instance;

	protected static SpriteBase head;

	protected static SpriteBase cur;

	protected static bool pumpIsRunning;

	protected static bool pumpIsDone;

	public static float animationPumpInterval;

	public bool IsRunning
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static SpriteAnimationPump Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private void Awake()
	{
	}

	public void StartAnimationPump()
	{
	}

	[DebuggerHidden]
	protected IEnumerator PumpStarter()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void StopAnimationPump()
	{
	}

	[DebuggerHidden]
	protected static IEnumerator AnimationPump()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void Add(SpriteBase s)
	{
	}

	public static void Remove(SpriteBase s)
	{
	}
}
