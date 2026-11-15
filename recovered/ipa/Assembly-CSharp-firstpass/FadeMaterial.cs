using UnityEngine;

public class FadeMaterial : EZAnimation
{
	protected Color start;

	protected Color delta;

	protected Color end;

	protected Material mat;

	protected Color temp;

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

	public static FadeMaterial Do(Material material, ANIM_MODE mode, Color begin, Color dest, Interpolator interp, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static FadeMaterial Do(Material material, ANIM_MODE mode, Color dest, Interpolator interp, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override bool Start(GameObject sub, AnimParams parms)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Start(Material material, ANIM_MODE mode, Color dest, Interpolator interp, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
	}

	public void Start(Material material, ANIM_MODE mode, Color begin, Color dest, Interpolator interp, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
	}

	public void Start()
	{
	}
}
