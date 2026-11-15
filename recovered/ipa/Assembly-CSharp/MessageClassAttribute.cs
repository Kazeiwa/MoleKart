using System;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MessageClassAttribute : Attribute
{
	private static Dictionary<string, _MessageMap> _msgmapTable;

	public MessageClassAttribute(Type t)
	{
	}

	public _MessageMap MessageMap(Type t)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
