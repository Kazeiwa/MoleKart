using System.Collections;
using System.Collections.Generic;

namespace Google.ProtocolBuffers.Collections;

public sealed class PopsicleList<T> : IEnumerable, IPopsicleList<T>, IList<T>, ICollection<T>, IEnumerable<T>
{
	private readonly List<T> items;

	private bool readOnly;

	public T this[int index]
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public int Count
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsReadOnly
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void MakeReadOnly()
	{
	}

	public int IndexOf(T item)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Insert(int index, T item)
	{
	}

	public void RemoveAt(int index)
	{
	}

	public void Add(T item)
	{
	}

	public void Clear()
	{
	}

	public bool Contains(T item)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
	}

	public bool Remove(T item)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Add(IEnumerable<T> collection)
	{
	}

	public IEnumerator<T> GetEnumerator()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void ValidateModification()
	{
	}
}
