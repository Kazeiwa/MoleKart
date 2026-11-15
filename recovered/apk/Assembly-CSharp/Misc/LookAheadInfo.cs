using UnityEngine;

namespace Misc;

public struct LookAheadInfo
{
	public Vector2 LookAheadPos;

	public float LeftWidth;

	public float RightWidth;

	public static LookAheadInfo Default => new LookAheadInfo(Vector2.zero, 0f, 0f);

	public LookAheadInfo(Vector2 lookAheadPos, float leftWidth, float rightWidth)
	{
		LookAheadPos = lookAheadPos;
		LeftWidth = leftWidth;
		RightWidth = rightWidth;
	}

	public LookAheadInfo NewLookAheadPos(Vector2 lookAheadPos)
	{
		return new LookAheadInfo(lookAheadPos, LeftWidth, RightWidth);
	}
}
