using System;
using System.Collections.Generic;
using Google.ProtocolBuffers;

namespace TaomeeNetwork;

public class NetHandlerRegistry
{
	private static NetHandlerRegistry instance;

	private Dictionary<string, Delegate> protoFuncs = new Dictionary<string, Delegate>();

	public static NetHandlerRegistry Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new NetHandlerRegistry();
			}
			return instance;
		}
	}

	public void RegistHandler<T>(Handler<T> handler)
	{
		Type parameterType = handler.Method.GetParameters()[1].ParameterType;
		if (protoFuncs.ContainsKey(parameterType.Name))
		{
			protoFuncs[parameterType.Name] = Delegate.Remove(protoFuncs[parameterType.Name], handler);
			protoFuncs[parameterType.Name] = Delegate.Combine(protoFuncs[parameterType.Name], handler);
		}
		else
		{
			protoFuncs.Add(parameterType.Name, handler);
		}
	}

	public void UnregistHandler<T>(Handler<T> handler)
	{
		Type parameterType = handler.Method.GetParameters()[1].ParameterType;
		if (protoFuncs.ContainsKey(parameterType.Name))
		{
			protoFuncs[parameterType.Name] = Delegate.Remove(protoFuncs[parameterType.Name], handler);
		}
	}

	public Delegate FindHandler(string msgName)
	{
		if (protoFuncs.ContainsKey(msgName))
		{
			return protoFuncs[msgName];
		}
		return null;
	}

	public void ProcessMsg(string peerId, IMessageLite msg)
	{
		string name = msg.GetType().Name;
		Delegate obj = Instance.FindHandler(name);
		if (obj == null)
		{
			return;
		}
		Delegate[] invocationList = obj.GetInvocationList();
		if (invocationList != null && invocationList.Length >= 1)
		{
			Delegate[] array = invocationList;
			foreach (Delegate obj2 in array)
			{
				obj2.DynamicInvoke(peerId, msg);
			}
		}
	}
}
