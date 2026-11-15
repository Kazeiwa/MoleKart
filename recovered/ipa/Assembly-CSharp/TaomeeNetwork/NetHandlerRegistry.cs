using System;
using System.Collections.Generic;
using Google.ProtocolBuffers;

namespace TaomeeNetwork;

public class NetHandlerRegistry
{
	private static NetHandlerRegistry instance;

	private Dictionary<string, Delegate> protoFuncs;

	public static NetHandlerRegistry Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void RegistHandler<T>(Handler<T> handler)
	{
	}

	public void UnregistHandler<T>(Handler<T> handler)
	{
	}

	public Delegate FindHandler(string msgName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ProcessMsg(string peerId, IMessageLite msg)
	{
	}
}
