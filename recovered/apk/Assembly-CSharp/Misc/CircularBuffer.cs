using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Misc;

public class CircularBuffer<T> : IEnumerable, IEnumerable<T>
{
	private T[] buffer;

	public int Capacity { get; set; }

	public int Size { get; set; }

	private int HeadIndex { get; set; }

	private int TailIndex { get; set; }

	public T this[int n]
	{
		get
		{
			if (n >= Size || n < 0)
			{
				Debug.LogError("Circular Buffer gets element: index " + n + " out of range, Size = " + Size);
			}
			return buffer[(HeadIndex + n) % Capacity];
		}
	}

	public T First => this[0];

	public T Last => this[Size - 1];

	public CircularBuffer(int capacity)
	{
		Capacity = capacity;
		Size = 0;
		HeadIndex = 0;
		TailIndex = 0;
		buffer = new T[capacity];
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Add(T element)
	{
		buffer[TailIndex++] = element;
		if (TailIndex == Capacity)
		{
			TailIndex = 0;
		}
		if (Size < Capacity)
		{
			Size++;
		}
		else
		{
			HeadIndex = (HeadIndex + 1) % Capacity;
		}
	}

	public void Clear()
	{
		Size = 0;
		HeadIndex = 0;
		TailIndex = 0;
	}

	public IEnumerator<T> GetEnumerator()
	{
		for (int i = 0; i < Size; i++)
		{
			yield return buffer[(HeadIndex + i) % Capacity];
		}
	}
}
