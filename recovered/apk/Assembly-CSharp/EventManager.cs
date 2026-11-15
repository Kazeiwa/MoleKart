using System.Collections;
using UnityEngine;

public class EventManager
{
	public bool LimitQueueProcesing;

	public float QueueProcessTime;

	private object m_lock = new object();

	private static EventManager s_Instance;

	private Hashtable m_listenerTable = new Hashtable();

	private Queue m_eventQueue = new Queue();

	public static EventManager instance
	{
		get
		{
			if (s_Instance == null)
			{
				s_Instance = new EventManager();
			}
			return s_Instance;
		}
	}

	public bool AddListener(IEventListener listener, string eventName)
	{
		if (listener == null || eventName == null)
		{
			Debug.Log("Event Manager: AddListener failed due to no listener or event name specified.");
			return false;
		}
		if (!m_listenerTable.ContainsKey(eventName))
		{
			m_listenerTable.Add(eventName, new ArrayList());
		}
		ArrayList arrayList = m_listenerTable[eventName] as ArrayList;
		if (arrayList.Contains(listener))
		{
			Debug.Log("Event Manager: Listener: " + listener.GetType().ToString() + " is already in list for event: " + eventName);
			return false;
		}
		arrayList.Add(listener);
		return true;
	}

	public bool DetachListener(IEventListener listener, string eventName)
	{
		if (!m_listenerTable.ContainsKey(eventName))
		{
			return false;
		}
		ArrayList arrayList = m_listenerTable[eventName] as ArrayList;
		if (!arrayList.Contains(listener))
		{
			return false;
		}
		arrayList.Remove(listener);
		return true;
	}

	public bool TriggerEvent(IEvent evt)
	{
		string name = evt.GetName();
		if (!m_listenerTable.ContainsKey(name))
		{
			Debug.Log("Event Manager: Event \"" + name + "\" triggered has no listeners!");
			return false;
		}
		ArrayList arrayList = m_listenerTable[name] as ArrayList;
		foreach (IEventListener item in arrayList)
		{
			if (item.HandleEvent(evt))
			{
				return true;
			}
		}
		return true;
	}

	public bool QueueEvent(IEvent evt)
	{
		if (!m_listenerTable.ContainsKey(evt.GetName()))
		{
			Debug.Log("EventManager: QueueEvent failed due to no listeners for event: " + evt.GetName());
			return false;
		}
		lock (m_lock)
		{
			m_eventQueue.Enqueue(evt);
		}
		return true;
	}

	public void Update()
	{
		float num = 0f;
		while (m_eventQueue.Count > 0 && (!LimitQueueProcesing || !(num > QueueProcessTime)))
		{
			IEvent obj;
			lock (m_lock)
			{
				obj = m_eventQueue.Dequeue() as IEvent;
			}
			if (!TriggerEvent(obj))
			{
				Debug.Log("Error when processing event: " + obj.GetName());
			}
			if (LimitQueueProcesing)
			{
				num += Time.deltaTime;
			}
		}
	}
}
