using System;
using System.Collections.Generic;

public class _MessageMap
{
	public Type _ClassType;

	public List<_MessageEntry> _messages;

	public object _basemap;

	private Dictionary<string, object> _parentmap;

	public void SetParentMap(string _typename, object map)
	{
		if (_parentmap == null)
		{
			_parentmap = new Dictionary<string, object>();
		}
		if (!_parentmap.ContainsKey(_typename))
		{
			_parentmap[_typename] = map;
		}
	}

	public _MessageMap _ParentMap(Type type)
	{
		return TraverseParent(this, type);
	}

	private _MessageMap TraverseParent(_MessageMap e, Type type)
	{
		if (e._ClassType.Equals(type))
		{
			return e;
		}
		if (e._parentmap != null)
		{
			if (e._parentmap.ContainsKey(type.Name))
			{
				return e._parentmap[type.Name] as _MessageMap;
			}
			foreach (KeyValuePair<string, object> item in _parentmap)
			{
				_MessageMap messageMap = TraverseParent(item.Value as _MessageMap, type);
				if (messageMap != null)
				{
					return messageMap;
				}
			}
		}
		return null;
	}
}
