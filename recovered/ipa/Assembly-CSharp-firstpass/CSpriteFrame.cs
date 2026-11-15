using System;
using UnityEngine;

[Serializable]
public class CSpriteFrame
{
	public Rect uvs;

	public Vector2 scaleFactor;

	public Vector2 topLeftOffset;

	public Vector2 bottomRightOffset;

	public CSpriteFrame()
	{
	}

	public CSpriteFrame(CSpriteFrame f)
	{
	}

	public CSpriteFrame(SPRITE_FRAME f)
	{
	}

	public void Copy(SPRITE_FRAME f)
	{
	}

	public void Copy(CSpriteFrame f)
	{
	}

	public SPRITE_FRAME ToStruct()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
