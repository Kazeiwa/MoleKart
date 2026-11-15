using System;
using UnityEngine;

[Serializable]
public struct SPRITE_FRAME
{
	public Rect uvs;

	public Vector2 scaleFactor;

	public Vector2 topLeftOffset;

	public Vector2 bottomRightOffset;

	public SPRITE_FRAME(float dummy)
	{
	}

	public void Copy(CSpriteFrame f)
	{
	}
}
