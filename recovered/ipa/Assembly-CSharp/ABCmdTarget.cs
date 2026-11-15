using System;
using System.Collections.Generic;

public class ABCmdTarget
{
	private static Dictionary<string, _MessageMap> _msgmapTable;

	public virtual void processMsg(int msg, ref ABMessageData data)
	{
	}

	~ABCmdTarget()
	{
	}

	public _MessageMap GetMessageMap(Type t)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void MessageFun<T>(Type type, OnMsg delMsgFun) where T : ABCmdTarget
	{
	}

	public void sendMessage(object target, int msg, ref ABMessageData data)
	{
	}
}
