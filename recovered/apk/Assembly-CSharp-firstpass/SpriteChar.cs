using System.Collections.Generic;
using UnityEngine;

public struct SpriteChar
{
	public int id;

	public Rect UVs;

	public float xOffset;

	public float yOffset;

	public float xAdvance;

	public Dictionary<char, float> kernings;

	public Dictionary<char, float> origKernings;

	public float GetKerning(char prevChar)
	{
		if (kernings == null)
		{
			return 0f;
		}
		float value = 0f;
		kernings.TryGetValue(prevChar, out value);
		return value;
	}
}
