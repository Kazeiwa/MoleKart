using System;
using UnityEngine;

namespace Misc;

public class GizmosEx
{
	private const float DefaultY = 15f;

	private static readonly Vector2[] arrowHeadPath = new Vector2[5]
	{
		new Vector2(-2f, 1f),
		new Vector2(-2f, 2f),
		new Vector2(0f, 0f),
		new Vector2(-2f, -2f),
		new Vector2(-2f, -1f)
	};

	private static readonly Vector2[] arrowTailPath = new Vector2[2]
	{
		new Vector2(0f, 2f),
		new Vector2(0f, -2f)
	};

	public static void DrawSphere(Vector2 center, float radius)
	{
		Gizmos.DrawSphere(Vector2Ex.To3D(center, 15f), radius);
	}

	public static void DrawCircle(Vector3 center, float radius)
	{
		float num = 0f;
		for (int i = 0; i < 64; i++)
		{
			Vector3 vector = new Vector3(Mathf.Cos(num), 0f, Mathf.Sin(num)) * radius + center;
			num += (float)Math.PI / 32f;
			Vector3 to = new Vector3(Mathf.Cos(num), 0f, Mathf.Sin(num)) * radius + center;
			Gizmos.DrawLine(vector, to);
		}
	}

	public static void DrawCircle(Vector2 center, float radius, float y)
	{
		DrawCircle(new Vector3(center.x, y, center.y), radius);
	}

	public static void DrawCircle(Vector2 center, float radius)
	{
		DrawCircle(center, radius, 15f);
	}

	public static void DrawRay(Vector3 from, Vector2 direction)
	{
		Gizmos.DrawRay(from, new Vector3(direction.x, 0f, direction.y));
	}

	public static void DrawRay(Vector2 from, Vector2 direction, float y)
	{
		DrawRay(new Vector3(from.x, y, from.y), direction);
	}

	public static void DrawRay(Vector2 from, Vector2 direction)
	{
		DrawRay(from, direction, 15f);
	}

	public static void DrawLine(Vector3 from, Vector2 to)
	{
		Gizmos.DrawLine(from, new Vector3(to.x, from.y, to.y));
	}

	public static void DrawLine(Vector2 from, Vector2 to)
	{
		DrawLine(new Vector3(from.x, 15f, from.y), to);
	}

	public static void DrawLine(Vector2 from, Vector2 to, float y)
	{
		Gizmos.DrawLine(Vector2Ex.To3D(from, y), Vector2Ex.To3D(to, y));
	}

	public static void DrawRect(Vector2 center, float r)
	{
		Vector2 vector = new Vector2(center.x - r, center.y - r);
		Vector2 vector2 = new Vector2(center.x + r, center.y - r);
		Vector2 vector3 = new Vector2(center.x - r, center.y + r);
		Vector2 to = new Vector2(center.x + r, center.y + r);
		DrawLine(vector, vector2);
		DrawLine(vector, vector3);
		DrawLine(vector2, to);
		DrawLine(vector3, to);
	}

	public static void DrawQuadrangle(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
	{
		DrawQuadrangle(a, b, c, d, 15f);
	}

	public static void DrawQuadrangle(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float y)
	{
		Vector3 vector = Vector2Ex.To3D(a, y);
		Vector3 vector2 = Vector2Ex.To3D(b, y);
		Vector3 vector3 = Vector2Ex.To3D(c, y);
		Vector3 vector4 = Vector2Ex.To3D(d, y);
		Gizmos.DrawLine(vector, vector2);
		Gizmos.DrawLine(vector2, vector3);
		Gizmos.DrawLine(vector3, vector4);
		Gizmos.DrawLine(vector4, vector);
	}

	public static void DrawText(Vector3 position, string text)
	{
		TextGizmos.Instance.Draw(position, text);
	}

	public static void DrawText(Vector3 position, int line, string text)
	{
		position.z += (float)line * 3f;
		DrawText(position, text);
	}

	public static void DrawText(Vector2 position, string text)
	{
		DrawText(Vector2Ex.To3D(position, 15f), text);
	}

	public static void DrawRect(float top, float bottom, float left, float right)
	{
		DrawRect(top, bottom, left, right, 15f);
	}

	public static void DrawRect(float top, float bottom, float left, float right, float y)
	{
		Vector2 vector = new Vector2(left, top);
		Vector2 vector2 = new Vector2(right, top);
		Vector2 vector3 = new Vector2(left, bottom);
		Vector2 to = new Vector2(right, bottom);
		DrawLine(vector, vector2, y);
		DrawLine(vector3, to, y);
		DrawLine(vector, vector3, y);
		DrawLine(vector2, to, y);
	}

	private static void DrawPath(Vector2[] path)
	{
		for (int i = 1; i < path.Length; i++)
		{
			DrawLine(path[i - 1], path[i]);
		}
	}

	public static void DrawArrow(Vector3 from, Vector3 to)
	{
		DrawArrow(Vector3Ex.To2D(from), Vector3Ex.To2D(to));
	}

	public static void DrawArrow(Vector3 from, Vector2 to)
	{
		DrawArrow(from, Vector2Ex.To3D(to, 15f));
	}

	public static void DrawArrow(Vector2 from, Vector2 to)
	{
		Vector2 normalized = (to - from).normalized;
		Vector2[] array = Vector2Ex.RotateTranslate(arrowHeadPath, normalized, to);
		DrawPath(array);
		Vector2[] array2 = Vector2Ex.RotateTranslate(arrowTailPath, normalized, from);
		DrawPath(array2);
		DrawLine(array2[0], array[0]);
		DrawLine(array2[array2.Length - 1], array[array.Length - 1]);
	}

	public static void DrawLocalOffset(Vector2 from, Vector2 localOffset, Vector2 xDirection)
	{
		Vector2 vector = Vector2Ex.LocalToGlobalVector(localOffset, xDirection);
		Vector2 vector2 = from + vector;
		DrawLine(from, vector2);
		DrawSphere(vector2, 0.6f);
	}
}
