using System;
using System.Collections.Generic;
using UnityEngine;

public class ABCmdBehaviour : MonoBehaviour
{
	private static Dictionary<string, _MessageMap> _msgmapTable;

	~ABCmdBehaviour()
	{
	}

	protected void MessageFun<T>(Type type, OnMsg delMsgFun) where T : ABCmdBehaviour
	{
	}

	public _MessageMap GetMessageMap(Type t)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void sendMessage(object target, int msg, ref ABMessageData data)
	{
	}

	public virtual void processMsg(int msg, ref ABMessageData data)
	{
	}
}
