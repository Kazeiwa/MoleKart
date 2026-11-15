using System;
using System.Collections.Generic;

public static class Messenger
{
	private static Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

	public static void AddListener(string eventType, Callback handler)
	{
		lock (eventTable)
		{
			if (!eventTable.ContainsKey(eventType))
			{
				eventTable.Add(eventType, null);
			}
			eventTable[eventType] = (Callback)Delegate.Combine((Callback)eventTable[eventType], handler);
		}
	}

	public static void RemoveListener(string eventType, Callback handler)
	{
		lock (eventTable)
		{
			if (eventTable.ContainsKey(eventType))
			{
				eventTable[eventType] = (Callback)Delegate.Remove((Callback)eventTable[eventType], handler);
				if (eventTable[eventType] == null)
				{
					eventTable.Remove(eventType);
				}
			}
		}
	}

	public static void Invoke(string eventType)
	{
		if (eventTable.TryGetValue(eventType, out var value))
		{
			((Callback)value)?.Invoke();
		}
	}
}
public static class Messenger<T>
{
	private static Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

	public static void AddListener(string eventType, Callback<T> handler)
	{
		lock (eventTable)
		{
			if (!eventTable.ContainsKey(eventType))
			{
				eventTable.Add(eventType, null);
			}
			eventTable[eventType] = (Callback<T>)Delegate.Combine((Callback<T>)eventTable[eventType], handler);
		}
	}

	public static void RemoveListener(string eventType, Callback<T> handler)
	{
		lock (eventTable)
		{
			if (eventTable.ContainsKey(eventType))
			{
				eventTable[eventType] = (Callback<T>)Delegate.Remove((Callback<T>)eventTable[eventType], handler);
				if (eventTable[eventType] == null)
				{
					eventTable.Remove(eventType);
				}
			}
		}
	}

	public static void Invoke(string eventType, T arg1)
	{
		if (eventTable.TryGetValue(eventType, out var value))
		{
			((Callback<T>)value)?.Invoke(arg1);
		}
	}
}
public static class Messenger<T, U>
{
	private static Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

	public static void AddListener(string eventType, Callback<T, U> handler)
	{
		lock (eventTable)
		{
			if (!eventTable.ContainsKey(eventType))
			{
				eventTable.Add(eventType, null);
			}
			eventTable[eventType] = (Callback<T, U>)Delegate.Combine((Callback<T, U>)eventTable[eventType], handler);
		}
	}

	public static void RemoveListener(string eventType, Callback<T, U> handler)
	{
		lock (eventTable)
		{
			if (eventTable.ContainsKey(eventType))
			{
				eventTable[eventType] = (Callback<T, U>)Delegate.Remove((Callback<T, U>)eventTable[eventType], handler);
				if (eventTable[eventType] == null)
				{
					eventTable.Remove(eventType);
				}
			}
		}
	}

	public static void Invoke(string eventType, T arg1, U arg2)
	{
		if (eventTable.TryGetValue(eventType, out var value))
		{
			((Callback<T, U>)value)?.Invoke(arg1, arg2);
		}
	}
}
public static class Messenger<T, U, V>
{
	private static Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

	public static void AddListener(string eventType, Callback<T, U, V> handler)
	{
		lock (eventTable)
		{
			if (!eventTable.ContainsKey(eventType))
			{
				eventTable.Add(eventType, null);
			}
			eventTable[eventType] = (Callback<T, U, V>)Delegate.Combine((Callback<T, U, V>)eventTable[eventType], handler);
		}
	}

	public static void RemoveListener(string eventType, Callback<T, U, V> handler)
	{
		lock (eventTable)
		{
			if (eventTable.ContainsKey(eventType))
			{
				eventTable[eventType] = (Callback<T, U, V>)Delegate.Remove((Callback<T, U, V>)eventTable[eventType], handler);
				if (eventTable[eventType] == null)
				{
					eventTable.Remove(eventType);
				}
			}
		}
	}

	public static void Invoke(string eventType, T arg1, U arg2, V arg3)
	{
		if (eventTable.TryGetValue(eventType, out var value))
		{
			((Callback<T, U, V>)value)?.Invoke(arg1, arg2, arg3);
		}
	}
}
