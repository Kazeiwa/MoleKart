using System.Collections.Generic;

public class EZLinkedList<T> where T : IEZLinkedListItem<T>
{
	private List<EZLinkedListIterator<T>> iters;

	private List<EZLinkedListIterator<T>> freeIters;

	protected T head;

	protected T cur;

	protected T nextItem;

	protected int count;

	public int Count
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool Empty
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public T Head
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public T Current
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public EZLinkedListIterator<T> Begin()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void End(EZLinkedListIterator<T> it)
	{
	}

	public bool Rewind()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool MoveNext()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Add(T item)
	{
	}

	public void Remove(T item)
	{
	}

	public void Clear()
	{
	}
}
