using System;
using UnityEngine;

[Serializable]
public class UVAnimation_Auto : UVAnimation
{
	public Vector2 start;

	public Vector2 pixelsToNextColumnAndRow;

	public int cols;

	public int rows;

	public int totalCells;

	public UVAnimation_Auto()
	{
	}

	public UVAnimation_Auto(UVAnimation_Auto anim)
	{
	}

	public new UVAnimation_Auto Clone()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SPRITE_FRAME[] BuildUVAnim(SpriteRoot s)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
