using UnityEngine;

public class AnimateScale : EZAnimation
{
	protected Vector3 start;

	protected Vector3 delta;

	protected Vector3 end;

	protected GameObject subject;

	protected Transform subTrans;

	protected Vector3 temp;

	public override object GetSubject()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void _end()
	{
	}

	protected override void LoopReset()
	{
	}

	protected override void WaitDone()
	{
	}

	protected override void DoAnim()
	{
	}

	public static AnimateScale Do(GameObject sub, ANIM_MODE mode, Vector3 begin, Vector3 dest, Interpolator interp, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static AnimateScale Do(GameObject sub, ANIM_MODE mode, Vector3 dest, Interpolator interp, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override bool Start(GameObject sub, AnimParams parms)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Start(GameObject sub, ANIM_MODE mode, Vector3 dest, Interpolator interp, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
	}

	public void Start(GameObject sub, ANIM_MODE mode, Vector3 begin, Vector3 dest, Interpolator interp, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
	}

	public void Start()
	{
	}
}
