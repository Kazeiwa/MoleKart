using System.Collections;

public class EventManager
{
	public bool LimitQueueProcesing;

	public float QueueProcessTime;

	private object m_lock;

	private static EventManager s_Instance;

	private Hashtable m_listenerTable;

	private Queue m_eventQueue;

	public static EventManager instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool AddListener(IEventListener listener, string eventName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool DetachListener(IEventListener listener, string eventName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool TriggerEvent(IEvent evt)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool QueueEvent(IEvent evt)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Update()
	{
	}
}
