using UnityEngine;

public static class DebugEx
{
	private static bool consoleDebug;

	private static bool gameDebug;

	private static string lastDebugMessage;

	static DebugEx()
	{
	}

	public static void Break()
	{
	}

	public static void Log(object message)
	{
	}

	public static void Log(object message, Object context)
	{
	}

	public static void LogError(object message)
	{
	}

	public static void LogError(object message, Object context)
	{
	}

	public static void LogWarning(object message)
	{
	}

	public static void LogWarning(object message, Object context)
	{
	}

	public static void DrawLine(Vector3 start, Vector3 end, Color col)
	{
	}

	public static void DrawRay(Vector3 start, Vector3 dir, Color col)
	{
	}

	public static void UseGUI()
	{
	}

	public static void UseGUI(Rect position, string message)
	{
	}
}
