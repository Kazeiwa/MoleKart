using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class MessageFunAttribute : Attribute
{
	public OnMsg delegateMsgFun;

	public _MessageMap _Derivedmsgmap;

	private Type type;

	public MessageFunAttribute(Type _DerivedClass)
	{
	}

	public void InitMessageMap(OnMsg delegateMsgFun, object _base, object _handle)
	{
	}
}
