using System.Collections.Generic;

internal class ABMessageQueue
{
	private static ABMessageQueue instance;

	private List<ABMessage> mHighPriority;

	private List<ABMessage> mNormalPriority;

	private List<ABMessage> mLowPriority;

	~ABMessageQueue()
	{
	}

	public void insert(ref ABMessage msg, ABMessagePriority msgP)
	{
	}

	public bool getMessage(ref ABMessage msg)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static ABMessageQueue getInstance()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void insert(ref ABMessage msg, List<ABMessage> msgQueue)
	{
	}

	private bool getMessage(ref ABMessage msg, List<ABMessage> msgQueue, int tick)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
