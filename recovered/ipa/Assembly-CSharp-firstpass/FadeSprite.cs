using UnityEngine;

public class FadeSprite : EZAnimation
{
	protected Color start;

	protected Color delta;

	protected Color end;

	protected SpriteRoot sprite;

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

	public static FadeSprite Do(SpriteRoot sprt, ANIM_MODE mode, Color begin, Color dest, Interpolator interp, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static FadeSprite Do(SpriteRoot sprt, ANIM_MODE mode, Color dest, Interpolator interp, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override bool Start(GameObject sub, AnimParams parms)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Start(SpriteRoot sprt, ANIM_MODE mode, Color dest, Interpolator interp, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
	}

	public void Start(SpriteRoot sprt, ANIM_MODE mode, Color begin, Color dest, Interpolator interp, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
	}

	public void Start()
	{
	}
}
