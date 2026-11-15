using System;
using UnityEngine;

[Serializable]
public class UVAnimation_Multi
{
	public string name;

	public int loopCycles;

	public bool loopReverse;

	public float framerate;

	public UVAnimation.ANIM_END_ACTION onAnimEnd;

	public UVAnimation_Auto[] clips;

	[HideInInspector]
	public int index;

	protected int curClip;

	protected int stepDir;

	protected int numLoops;

	protected float duration;

	protected bool ret;

	protected int i;

	protected int framePos;

	public UVAnimation_Multi()
	{
	}

	public UVAnimation_Multi(UVAnimation_Multi anim)
	{
	}

	public UVAnimation_Multi Clone()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public UVAnimation_Auto GetCurrentClip()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public UVAnimation_Auto[] BuildUVAnim(SpriteRoot s)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool GetNextFrame(ref SPRITE_FRAME nextFrame)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SPRITE_FRAME GetCurrentFrame()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void AppendAnim(int index, SPRITE_FRAME[] anim)
	{
	}

	public void AppendClip(UVAnimation clip)
	{
	}

	public void PlayInReverse()
	{
	}

	public void SetAnim(int index, SPRITE_FRAME[] frames)
	{
	}

	public void Reset()
	{
	}

	public void SetPosition(float pos)
	{
	}

	public void SetAnimPosition(float pos)
	{
	}

	protected void CalcDuration()
	{
	}

	public float GetDuration()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetFrameCount()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetCurPosition()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
