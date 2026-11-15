using UnityEngine;

public static class DebugEx
{
	private static bool consoleDebug;

	private static bool gameDebug;

	private static string lastDebugMessage;

	static DebugEx()
	{
		consoleDebug = true;
		gameDebug = true;
		if (!Debug.isDebugBuild)
		{
			consoleDebug = false;
			gameDebug = false;
		}
	}

	public static void Break()
	{
		if (consoleDebug)
		{
			Debug.Break();
		}
	}

	public static void Log(object message)
	{
		if (message != null)
		{
			if (consoleDebug)
			{
				Debug.Log(message);
			}
			lastDebugMessage = string.Empty + message;
		}
	}

	public static void Log(object message, Object context)
	{
		if (message != null)
		{
			if (consoleDebug)
			{
				Debug.Log(message, context);
			}
			lastDebugMessage = string.Empty + message;
		}
	}

	public static void LogError(object message)
	{
		if (message != null)
		{
			if (consoleDebug)
			{
				Debug.LogError(message);
			}
			lastDebugMessage = string.Empty + message;
		}
	}

	public static void LogError(object message, Object context)
	{
		if (message != null)
		{
			if (consoleDebug)
			{
				Debug.LogError(message, context);
			}
			lastDebugMessage = string.Empty + message;
		}
	}

	public static void LogWarning(object message)
	{
		if (message != null)
		{
			if (consoleDebug)
			{
				Debug.LogWarning(message);
			}
			lastDebugMessage = string.Empty + message;
		}
	}

	public static void LogWarning(object message, Object context)
	{
		if (message != null)
		{
			if (consoleDebug)
			{
				Debug.LogWarning(message, context);
			}
			lastDebugMessage = string.Empty + message;
		}
	}

	public static void DrawLine(Vector3 start, Vector3 end, Color col)
	{
		if (consoleDebug)
		{
			Debug.DrawLine(start, end, col);
		}
	}

	public static void DrawRay(Vector3 start, Vector3 dir, Color col)
	{
		if (consoleDebug)
		{
			Debug.DrawRay(start, dir, col);
		}
	}

	public static void UseGUI()
	{
		if (gameDebug && lastDebugMessage != null)
		{
			GUI.Label(new Rect(5f, -3f, Screen.width, 20f), lastDebugMessage);
		}
	}

	public static void UseGUI(Rect position, string message)
	{
		if (gameDebug && message != null)
		{
			Log(message);
			GUI.Label(position, message);
		}
	}
}
