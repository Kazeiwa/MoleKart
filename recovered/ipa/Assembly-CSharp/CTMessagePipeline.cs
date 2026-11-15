using System.Collections;

public class CTMessagePipeline
{
	private static Hashtable cacheTable;

	public static void postMessage(ABCmdTarget target, int msg, ABMessagePriority ePriority, ref ABMessageData data)
	{
	}

	public static void postMessageEx(ABCmdTarget target, int msg, ABMessagePriority ePriority, ref ABMessageData data, int expire)
	{
	}

	public static void RemoveCacheTableObject(object target)
	{
	}

	public static void sendMessage(object target, int msg, ref ABMessageData data)
	{
	}

	public static void UpdateMessage()
	{
	}
}
