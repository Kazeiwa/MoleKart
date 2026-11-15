using System;
using System.Reflection;
using Google.ProtocolBuffers;

namespace TaomeeNetwork;

public static class MessageUtil
{
	public static IMessageLite GetDefaultMessage(Type type)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type", "No type specified.");
		}
		if (type.IsAbstract || type.IsGenericTypeDefinition)
		{
			throw new ArgumentException("Unable to get a default message for an abstract or generic type (" + type.FullName + ")");
		}
		if (!typeof(IMessageLite).IsAssignableFrom(type))
		{
			throw new ArgumentException("Unable to get a default message for non-message type (" + type.FullName + ")");
		}
		PropertyInfo property = type.GetProperty("DefaultInstance", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		if (property == null)
		{
			throw new ArgumentException(type.FullName + " doesn't have a static DefaultInstance property");
		}
		if (property.PropertyType != type)
		{
			throw new ArgumentException(type.FullName + ".DefaultInstance property is of the wrong type");
		}
		return (IMessageLite)property.GetValue(null, null);
	}

	public static IMessageLite GetDefaultMessage(string typeName)
	{
		if (typeName == null)
		{
			throw new ArgumentNullException("typeName", "No type name specified.");
		}
		Type type = Type.GetType(typeName);
		if (type == null)
		{
			throw new ArgumentException("Unable to load type " + typeName);
		}
		return GetDefaultMessage(type);
	}
}
