using System;
using UnityEngine;

namespace Misc;

public class CheapMathf
{
	public const int SineSampleCount = 1440;

	public const float Radian2SineSampleIndex = 720f / (float)Math.PI;

	private static float[] SineTable;

	public static float Sin(float t)
	{
		return Sin((int)(t * (720f / (float)Math.PI)));
	}

	public static float Cos(float t)
	{
		return Sin(t + (float)Math.PI / 2f);
	}

	public static float SinOfDegree(float t)
	{
		return Sin(t * ((float)Math.PI / 180f));
	}

	public static float CosOfDegree(float t)
	{
		return Cos(t * ((float)Math.PI / 180f));
	}

	public static void Init()
	{
		SineTable = new float[1440];
		float num = 0.004363323f;
		for (int i = 0; i < 1440; i++)
		{
			float f = num * (float)i;
			SineTable[i] = Mathf.Sin(f);
		}
	}

	private static float Sin(int sineSampleIndex)
	{
		int num = sineSampleIndex % 1440;
		if (num < 0)
		{
			num += 1440;
		}
		return SineTable[num];
	}
}
