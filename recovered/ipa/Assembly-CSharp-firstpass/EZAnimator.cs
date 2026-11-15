using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EZAnimator : MonoBehaviour
{
	private static EZAnimator s_Instance;

	protected static Dictionary<EZAnimation.ANIM_TYPE, EZLinkedList<EZAnimation>> freeAnimPool;

	protected static EZLinkedList<EZAnimation> animations;

	protected static bool pumpIsRunning;

	protected static bool pumpIsDone;

	protected static float startTime;

	protected static float time;

	protected static float elapsed;

	protected static EZAnimation anim;

	private int i;

	public static EZAnimator instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static bool Exists()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void Awake()
	{
	}

	private void OnLevelWasLoaded(int level)
	{
	}

	[DebuggerHidden]
	protected static IEnumerator AnimPump()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
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

	protected EZAnimation CreateNewAnimation(EZAnimation.ANIM_TYPE type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public EZAnimation GetAnimation(EZAnimation.ANIM_TYPE type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected static void ReturnAnimToPool(EZAnimation anim)
	{
	}

	public void AddAnimation(EZAnimation a)
	{
	}

	public void AddTransition(EZTransition t)
	{
	}

	public void StopAnimation(EZAnimation a)
	{
	}

	public void StopAnimation(EZAnimation a, bool end)
	{
	}

	public void Stop(object obj)
	{
	}

	public void Stop(object obj, bool end)
	{
	}

	public void Stop(object obj, EZAnimation.ANIM_TYPE type, bool end)
	{
	}

	public void End(object obj)
	{
	}

	public void EndAll()
	{
	}

	public void StopAll()
	{
	}

	public void PauseAll()
	{
	}

	public void UnpauseAll()
	{
	}

	public static int GetNumAnimations()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
