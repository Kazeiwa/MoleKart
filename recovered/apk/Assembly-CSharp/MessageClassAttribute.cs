using System;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MessageClassAttribute : Attribute
{
	private static Dictionary<string, _MessageMap> _msgmapTable = new Dictionary<string, _MessageMap>();

	public MessageClassAttribute(Type t)
	{
		if (!_msgmapTable.ContainsKey(t.Name))
		{
			_MessageMap value = new _MessageMap
			{
				_ClassType = t
			};
			_msgmapTable[t.Name] = value;
		}
	}

	public _MessageMap MessageMap(Type t)
	{
		if (_msgmapTable.ContainsKey(t.Name))
		{
			return _msgmapTable[t.Name];
		}
		return null;
	}
}
