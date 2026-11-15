using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ABCmdBehaviour : MonoBehaviour
{
	private static Dictionary<string, _MessageMap> _msgmapTable = new Dictionary<string, _MessageMap>();

	~ABCmdBehaviour()
	{
		CTMessagePipeline.RemoveCacheTableObject(this);
	}

	protected void MessageFun<T>(Type type, OnMsg delMsgFun) where T : ABCmdBehaviour
	{
		T val = this as T;
		Type baseType = type.BaseType;
		_MessageMap messageMap;
		if (typeof(T).Equals(typeof(ABCmdBehaviour)))
		{
			messageMap = val.GetMessageMap(GetType());
		}
		else
		{
			MessageClassAttribute messageClassAttribute = (MessageClassAttribute)baseType.GetCustomAttributes(inherit: false)[0];
			messageMap = messageClassAttribute.MessageMap(baseType);
		}
		MessageClassAttribute messageClassAttribute2 = (MessageClassAttribute)type.GetCustomAttributes(inherit: false)[0];
		messageMap.SetParentMap(type.Name, messageClassAttribute2.MessageMap(type));
		MethodInfo[] methods = type.GetMethods();
		MethodInfo[] array = methods;
		foreach (MethodInfo methodInfo in array)
		{
			if (!methodInfo.DeclaringType.Equals(type))
			{
				continue;
			}
			object[] customAttributes = methodInfo.GetCustomAttributes(inherit: false);
			object[] array2 = customAttributes;
			foreach (object obj in array2)
			{
				if (obj is MessageFunAttribute)
				{
					MessageFunAttribute messageFunAttribute = obj as MessageFunAttribute;
					messageFunAttribute.InitMessageMap(delMsgFun, messageMap, this);
				}
			}
		}
	}

	public _MessageMap GetMessageMap(Type t)
	{
		if (_msgmapTable.ContainsKey(t.Name))
		{
			return _msgmapTable[t.Name];
		}
		_MessageMap messageMap = new _MessageMap();
		messageMap._ClassType = typeof(ABCmdBehaviour);
		_msgmapTable[t.Name] = messageMap;
		return messageMap;
	}

	public void sendMessage(object target, int msg, ref ABMessageData data)
	{
		CTMessagePipeline.sendMessage(target, msg, ref data);
	}

	public virtual void processMsg(int msg, ref ABMessageData data)
	{
	}
}
