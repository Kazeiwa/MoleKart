using UnityEngine;

namespace Misc;

public struct Vector3Ex
{
	public static float SqrDistance(Vector3 from, Vector3 to)
	{
		return (to - from).sqrMagnitude;
	}

	public static Vector3 Add(Vector3 v1, Vector2 v2)
	{
		return new Vector3(v1.x + v2.x, v1.y, v1.z + v2.y);
	}

	public static Vector3 Add(Vector2 v1, Vector3 v2)
	{
		return new Vector3(v1.x + v2.x, v2.y, v1.y + v2.z);
	}

	public static Vector2 To2D(Vector3 v)
	{
		return new Vector2(v.x, v.z);
	}
}
