using System;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class MessageFunAttribute : Attribute
{
	public OnMsg delegateMsgFun;

	public _MessageMap _Derivedmsgmap;

	private Type type;

	public MessageFunAttribute(Type _DerivedClass)
	{
		MessageClassAttribute messageClassAttribute = (MessageClassAttribute)_DerivedClass.GetCustomAttributes(inherit: false)[0];
		_Derivedmsgmap = messageClassAttribute.MessageMap(_DerivedClass);
		type = _DerivedClass;
	}

	public void InitMessageMap(OnMsg delegateMsgFun, object _base, object _handle)
	{
		_MessageEntry item = new _MessageEntry
		{
			_MsgID = 0u,
			delegateMsgFun = delegateMsgFun,
			handle = _handle
		};
		_Derivedmsgmap._basemap = _base;
		_Derivedmsgmap._ClassType = type;
		if (_Derivedmsgmap._messages == null)
		{
			_Derivedmsgmap._messages = new List<_MessageEntry>();
		}
		_Derivedmsgmap._messages.Add(item);
	}
}
