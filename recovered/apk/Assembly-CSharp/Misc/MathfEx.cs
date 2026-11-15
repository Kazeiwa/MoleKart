using System;
using System.Collections.Generic;
using UnityEngine;

namespace Misc;

public class MathfEx
{
	public static float Square(float x)
	{
		return x * x;
	}

	public static float RandomBinomial()
	{
		return UnityEngine.Random.value - UnityEngine.Random.value;
	}

	public static bool RandomBoolean()
	{
		int num = UnityEngine.Random.Range(0, 2);
		return num == 0;
	}

	public static bool RandomTrue(float threshold)
	{
		return UnityEngine.Random.value < threshold;
	}

	public static Vector2 RandomDisc(float radius)
	{
		float num = UnityEngine.Random.Range(0f, radius);
		float t = UnityEngine.Random.Range(-(float)Math.PI, (float)Math.PI);
		return num * new Vector2(CheapMathf.Cos(t), CheapMathf.Sin(t));
	}

	public static Vector2 RandomRect(float top, float bottom, float left, float right)
	{
		float x = UnityEngine.Random.Range(left, right);
		float y = UnityEngine.Random.Range(top, bottom);
		return new Vector2(x, y);
	}

	public static float Clamp(float value, float maxAbs)
	{
		return Mathf.Clamp(value, 0f - maxAbs, maxAbs);
	}

	public static int Clamp(int value, int maxAbs)
	{
		return Mathf.Clamp(value, -maxAbs, maxAbs);
	}

	public static void Clamp(ref float value, float maxAbs)
	{
		value = Clamp(value, maxAbs);
	}

	public static float SimpleReverse(float q, float maxAbsQ, float maxAbsP)
	{
		return maxAbsP * SimpleReverse(q, maxAbsQ);
	}

	public static float SimpleReverse(float q, float maxAbsQ)
	{
		return (0f - Clamp(q, maxAbsQ)) / maxAbsQ;
	}

	public static bool InRange(float value, float maxAbs)
	{
		return value < maxAbs && 0f - maxAbs < value;
	}

	public static bool OnRange(float value, float maxAbs)
	{
		return value <= maxAbs && 0f - maxAbs <= value;
	}

	public static bool InRange(int value, int maxAbs)
	{
		return value < maxAbs && -maxAbs < value;
	}

	public static bool OnRange(int value, int maxAbs)
	{
		return value <= maxAbs && -maxAbs <= value;
	}

	public static float Min(IEnumerable<float> list)
	{
		float num = float.PositiveInfinity;
		foreach (float item in list)
		{
			float num2 = item;
			if (num2 < num)
			{
				num = num2;
			}
		}
		return num;
	}

	public static float Floor(float value, float errorLength)
	{
		return errorLength * Mathf.Floor(value / errorLength);
	}

	public static Vector2 Floor(Vector2 value, float errorLength)
	{
		return new Vector2(Floor(value.x, errorLength), Floor(value.y, errorLength));
	}

	public static float WeakDistance(Vector2 p1, Vector2 p2)
	{
		Vector2 vector = p1 - p2;
		return Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
	}

	public static float WeakDistance(Vector3 p1, Vector3 p2)
	{
		Vector3 vector = p1 - p2;
		return Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
	}
}
