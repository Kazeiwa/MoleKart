using System;
using UnityEngine;

namespace Misc;

public struct Vector2Ex
{
	public const float SideOfLineBias = 0.01f;

	public static Vector2 FromRadian(float angle)
	{
		return new Vector2(CheapMathf.Cos(angle), CheapMathf.Sin(angle));
	}

	public static Vector2 FromRadian(float angle, float r)
	{
		return r * FromRadian(angle);
	}

	public static Vector2 FromDegree(float angle)
	{
		return FromRadian(angle * ((float)Math.PI / 180f));
	}

	public static Vector2 FromDegree(float angle, float r)
	{
		return r * FromDegree(angle);
	}

	public static Vector2 Rotate(Vector2 v, float angle)
	{
		float num = CheapMathf.Sin(angle);
		float num2 = CheapMathf.Cos(angle);
		return new Vector2(num2 * v.x + (0f - num * v.y), num * v.x + num2 * v.y);
	}

	public static Vector2 Rotate(Vector2 v, Vector2 rotation)
	{
		return new Vector2(rotation.x * v.x - rotation.y * v.y, rotation.y * v.x + rotation.x * v.y);
	}

	public static Vector2 RotateTranslate(Vector2 v, Vector2 rotation, Vector2 offset)
	{
		return offset + Rotate(v, rotation);
	}

	public static Vector2[] Rotate(Vector2[] vectors, Vector2 rotation)
	{
		return Array.ConvertAll(vectors, (Vector2 v) => Rotate(v, rotation));
	}

	public static Vector2[] Translate(Vector2[] vectors, Vector2 offset)
	{
		return Array.ConvertAll(vectors, (Vector2 v) => v + offset);
	}

	public static Vector2[] RotateTranslate(Vector2[] vectors, Vector2 rotation, Vector2 offset)
	{
		return Translate(Rotate(vectors, rotation), offset);
	}

	public static Vector2 RotateRight(Vector2 v)
	{
		return new Vector2(v.y, 0f - v.x);
	}

	public static Vector2 RotateLeft(Vector2 v)
	{
		return new Vector2(0f - v.y, v.x);
	}

	public static bool IsNaN(Vector2 v)
	{
		return float.IsNaN(v.x) || float.IsNaN(v.y);
	}

	public static float SqrDistance(Vector2 from, Vector2 to)
	{
		return (to - from).sqrMagnitude;
	}

	public static float Distance(Vector2 from, Vector2 to)
	{
		return (to - from).magnitude;
	}

	public static Vector3 To3D(Vector2 v, float y)
	{
		return new Vector3(v.x, y, v.y);
	}

	public static bool LineIntersection(Vector2 a, Vector2 b, Vector2 c, Vector2 d, ref float dist, ref Vector2 point)
	{
		float num = (b.x - a.x) * (d.y - c.y) - (b.y - a.y) * (d.x - c.x);
		if (num == 0f)
		{
			return false;
		}
		float num2 = (a.y - c.y) * (d.x - c.x) - (a.x - c.x) * (d.y - c.y);
		float num3 = (a.y - c.y) * (b.x - a.x) - (a.x - c.x) * (b.y - a.y);
		float num4 = num2 / num;
		float num5 = num3 / num;
		if (num4 > 0f && num4 < 1f && num5 > 0f && num5 < 1f)
		{
			dist = Vector2.Distance(a, b) * num4;
			point = a + num4 * (b - a);
			return true;
		}
		dist = 0f;
		return false;
	}

	public static int ProjectPointOnLine(Vector2 v, Vector2 from, Vector2 to)
	{
		Vector2 lhs = v - from;
		Vector2 rhs = to - from;
		float num = Vector2.Dot(lhs, rhs);
		if (num < 0f)
		{
			return -1;
		}
		float sqrMagnitude = rhs.sqrMagnitude;
		if (num > sqrMagnitude)
		{
			return 1;
		}
		return 0;
	}

	public static int ProjectPointOnLine(Vector2 v, Vector2 from, Vector2 to, ref float r)
	{
		Vector2 lhs = v - from;
		Vector2 rhs = to - from;
		float num = Vector2.Dot(lhs, rhs);
		if (num < 0f)
		{
			return -1;
		}
		float sqrMagnitude = rhs.sqrMagnitude;
		if (num > sqrMagnitude)
		{
			return 1;
		}
		r = num / sqrMagnitude;
		return 0;
	}

	public static int ProjectPointOnLine(Vector2 v, Vector2 from, Vector2 to, ref Vector2 p)
	{
		Vector2 lhs = v - from;
		Vector2 vector = to - from;
		float num = Vector2.Dot(lhs, vector);
		if (num < 0f)
		{
			return -1;
		}
		float sqrMagnitude = vector.sqrMagnitude;
		if (num > sqrMagnitude)
		{
			return 1;
		}
		float num2 = num / sqrMagnitude;
		p = from + num2 * vector;
		return 0;
	}

	public static int SquareDistanceFromPointToLine(Vector2 v, Vector2 from, Vector2 to, ref float sqrDistance)
	{
		Vector2 p = Vector2.zero;
		int num = ProjectPointOnLine(v, from, to, ref p);
		if (num != 0)
		{
			return num;
		}
		sqrDistance = SqrDistance(v, p);
		return 0;
	}

	public static int DistanceFromPointToLine(Vector2 v, Vector2 from, Vector2 to, ref float distance)
	{
		Vector2 p = Vector2.zero;
		int num = ProjectPointOnLine(v, from, to, ref p);
		if (num != 0)
		{
			return num;
		}
		distance = Distance(v, p);
		return 0;
	}

	public static float SignedDistanceFromPointToLine(Vector2 v, Vector2 from, Vector2 to)
	{
		Vector2 lhs = v - from;
		Vector2 normalized = (to - from).normalized;
		Vector3 vector = RotateRight(normalized);
		return Vector2.Dot(lhs, vector);
	}

	public static int SideOfLine(Vector2 v, Vector2 from, Vector2 to)
	{
		float num = SignedDistanceFromPointToLine(v, from, to);
		if (num > 0.01f)
		{
			return 1;
		}
		if (0f - num > 0.01f)
		{
			return -1;
		}
		return 0;
	}

	public static float CosineOfAngle(Vector2 direction1, Vector2 direction2)
	{
		return Vector2.Dot(direction1, direction2);
	}

	public static float CosineOfAngle(Vector2 direction, Vector2 from, Vector2 to)
	{
		return CosineOfAngle(direction, (to - from).normalized);
	}

	public static float SignedRadianOfDirections(Vector2 direction, Vector2 referenceDirection)
	{
		float f = CosineOfAngle(direction, RotateRight(referenceDirection));
		return (float)Math.PI / 2f - Mathf.Acos(f);
	}

	public static float SignedRadianOfDirections(Vector2 direction, Vector2 from, Vector2 to)
	{
		return SignedRadianOfDirections(direction, (to - from).normalized);
	}

	public static float SignedDegreeOfDirections(Vector2 direction, Vector2 referenceDirection)
	{
		return 57.29578f * SignedRadianOfDirections(direction, referenceDirection);
	}

	public static float SignedDegreeOfDirections(Vector2 direction, Vector2 from, Vector2 to)
	{
		return 57.29578f * SignedRadianOfDirections(direction, from, to);
	}

	public static bool PointInEdgeBlock(Vector2 p, Vector2 from, float fromLeftWidth, float fromRightWidth, Vector2 to, float toLeftWidth, float toRightWidth)
	{
		if (ProjectPointOnLine(p, from, to) != 0)
		{
			return false;
		}
		Vector2 normalized = RotateRight(to - from).normalized;
		int num = SideOfLine(p, from + fromRightWidth * normalized, to + toRightWidth * normalized);
		if (num != -1)
		{
			return false;
		}
		int num2 = SideOfLine(p, from - fromLeftWidth * normalized, to - toLeftWidth * normalized);
		if (num2 != 1)
		{
			return false;
		}
		return true;
	}

	public static float RadianOfVector(Vector2 v)
	{
		Vector2 normalized = v.normalized;
		if (normalized.y >= 0f)
		{
			return Mathf.Acos(normalized.x);
		}
		return (float)Math.PI * 2f - Mathf.Acos(normalized.x);
	}

	public static float DegreeOfVector(Vector2 v)
	{
		return 57.29578f * RadianOfVector(v);
	}

	public static float PerpDot(Vector2 a, Vector2 b)
	{
		return a.y * b.x - a.x * b.y;
	}

	public static float PerpDot(Vector2 a, Vector2 b, Vector2 x)
	{
		return PerpDot(a - x, b - x);
	}

	public static Vector2 MeanDirection(Vector2 s, Vector2 t)
	{
		Vector2 normalized = (s + t).normalized;
		if (normalized.magnitude > 0.5f)
		{
			return normalized;
		}
		return t;
	}

	public static Vector2 GlobalToLocalVector(Vector2 globalVector, Vector2 direction)
	{
		return new Vector2(Vector2.Dot(globalVector, direction), PerpDot(globalVector, direction));
	}

	public static Vector2 GlobalToLocalVector(Vector2 globalPos, Vector2 from, Vector2 to)
	{
		Vector2 normalized = (to - from).normalized;
		Vector2 globalVector = globalPos - from;
		return GlobalToLocalVector(globalVector, normalized);
	}

	public static Vector2 LocalToGlobalVector(Vector2 localVector, Vector2 xDirection)
	{
		return localVector.x * xDirection + localVector.y * RotateLeft(xDirection);
	}
}
