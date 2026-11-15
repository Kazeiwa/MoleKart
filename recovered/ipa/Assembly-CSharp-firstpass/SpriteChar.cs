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
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
