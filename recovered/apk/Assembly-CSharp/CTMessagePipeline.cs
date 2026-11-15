using System;
using System.Collections;
using System.Collections.Generic;

public class CTMessagePipeline
{
	private static Hashtable cacheTable = new Hashtable();

	public static void postMessage(ABCmdTarget target, int msg, ABMessagePriority ePriority, ref ABMessageData data)
	{
		if (target != null)
		{
			ABMessageQueue instance = ABMessageQueue.getInstance();
			ABMessage msg2 = new ABMessage(target, msg, ref data, 0);
			instance.insert(ref msg2, ePriority);
		}
	}

	public static void postMessageEx(ABCmdTarget target, int msg, ABMessagePriority ePriority, ref ABMessageData data, int expire)
	{
		if (target != null)
		{
			ABMessageQueue instance = ABMessageQueue.getInstance();
			ABMessage msg2 = new ABMessage(target, msg, ref data, expire);
			instance.insert(ref msg2, ePriority);
		}
	}

	public static void RemoveCacheTableObject(object target)
	{
		if (cacheTable.ContainsKey(target))
		{
			cacheTable.Remove(target);
		}
	}

	public static void sendMessage(object target, int msg, ref ABMessageData data)
	{
		if (cacheTable.ContainsKey(target))
		{
			List<_MessageEntry> list = cacheTable[target] as List<_MessageEntry>;
			{
				foreach (_MessageEntry item in list)
				{
					item.delegateMsgFun(msg, ref data);
				}
				return;
			}
		}
		Type type = target.GetType();
		while (type.GetCustomAttributes(inherit: false) == null || type.GetCustomAttributes(inherit: false).Length <= 0)
		{
			type = type.BaseType;
			if (type == null)
			{
				return;
			}
		}
		MessageClassAttribute messageClassAttribute = (MessageClassAttribute)type.GetCustomAttributes(inherit: false)[0];
		if (target == null)
		{
			return;
		}
		_MessageMap messageMap = messageClassAttribute.MessageMap(type);
		messageMap = messageMap._ParentMap(type);
		List<_MessageEntry> list2 = new List<_MessageEntry>();
		do
		{
			if (messageMap._messages != null)
			{
				foreach (_MessageEntry message in messageMap._messages)
				{
					if (message.handle.Equals(target))
					{
						message.delegateMsgFun(msg, ref data);
						list2.Add(message);
					}
				}
			}
			messageMap = (_MessageMap)messageMap._basemap;
		}
		while (messageMap != null);
		if (list2.Count > 0)
		{
			cacheTable.Add(target, list2);
		}
	}

	public static void UpdateMessage()
	{
		ABMessageQueue instance = ABMessageQueue.getInstance();
		ABMessage msg = new ABMessage(null);
		if (instance.getMessage(ref msg) && msg.mReceiverInfo != null)
		{
			msg.mReceiverInfo.processMsg(msg.mMessage, ref msg.mData);
		}
	}
}
