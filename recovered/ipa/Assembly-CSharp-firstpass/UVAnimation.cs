using System;
using UnityEngine;

[Serializable]
public class UVAnimation
{
	public enum ANIM_END_ACTION
	{
		Do_Nothing,
		Revert_To_Static,
		Play_Default_Anim,
		Hide,
		Deactivate,
		Destroy
	}

	protected SPRITE_FRAME[] frames;

	protected int curFrame;

	protected int stepDir;

	protected int numLoops;

	protected bool playInReverse;

	protected float length;

	public string name;

	public int loopCycles;

	public bool loopReverse;

	[HideInInspector]
	public float framerate;

	[HideInInspector]
	public int index;

	[HideInInspector]
	public ANIM_END_ACTION onAnimEnd;

	public int StepDirection
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public UVAnimation(UVAnimation anim)
	{
	}

	public UVAnimation()
	{
	}

	public UVAnimation Clone()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Reset()
	{
	}

	public void PlayInReverse()
	{
	}

	public void SetStepDir(int dir)
	{
	}

	public bool GetNextFrame(ref SPRITE_FRAME nextFrame)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SPRITE_FRAME GetCurrentFrame()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SPRITE_FRAME GetFrame(int frame)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SPRITE_FRAME[] BuildUVAnim(Vector2 start, Vector2 cellSize, int cols, int rows, int totalCells)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SPRITE_FRAME[] BuildWrappedUVAnim(Vector2 start, Vector2 cellSize, int cols, int rows, int totalCells)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SPRITE_FRAME[] BuildWrappedUVAnim(Vector2 start, Vector2 cellSize, int totalCells)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetAnim(SPRITE_FRAME[] anim)
	{
	}

	public void SetAnim(TextureAnim anim, int idx)
	{
	}

	public void AppendAnim(SPRITE_FRAME[] anim)
	{
	}

	public void SetCurrentFrame(int f)
	{
	}

	public void SetPosition(float pos)
	{
	}

	public void SetClipPosition(float pos)
	{
	}

	protected void CalcLength()
	{
	}

	public float GetLength()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetDuration()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetFrameCount()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetFramesDisplayed()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetCurPosition()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
