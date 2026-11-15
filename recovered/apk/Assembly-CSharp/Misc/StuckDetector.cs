using UnityEngine;

namespace Misc;

public class StuckDetector
{
	private class CirBuf<T>
	{
		private T[] buffer;

		public int Capacity { get; set; }

		public int Size { get; set; }

		public int Tail { get; set; }

		public T[] Buffer => buffer;

		public CirBuf(int capacity)
		{
			Capacity = capacity;
			Size = 0;
			Tail = 0;
			buffer = new T[capacity];
		}

		public void Add(T item)
		{
			Buffer[Tail++] = item;
			if (Tail == Capacity)
			{
				Tail = 0;
			}
			if (Size < Capacity)
			{
				Size++;
			}
		}

		public void Clear()
		{
			Size = 0;
			Tail = 0;
		}
	}

	private const int MaxSampleCount = 5;

	private const float MaxSampleTime = 3f;

	private CirBuf<Vector2> samplePositions = new CirBuf<Vector2>(5);

	private CrudeElapsedTimer timer = new CrudeElapsedTimer(0.6f);

	private bool HasNoMove()
	{
		if (samplePositions.Size < 5)
		{
			return false;
		}
		Vector2 a = samplePositions.Buffer[0];
		Vector2[] buffer = samplePositions.Buffer;
		foreach (Vector2 b in buffer)
		{
			if (Vector2.Distance(a, b) > 0.1f)
			{
				return false;
			}
		}
		return true;
	}

	public bool DectectStuck(Vector2 position, float deltaTime)
	{
		timer.Advance(deltaTime);
		if (timer.TimeOutCount > 0)
		{
			timer.ResetTimeOutCount();
			samplePositions.Add(position);
			return HasNoMove();
		}
		return false;
	}

	public void Reset()
	{
		timer.Reset();
		samplePositions.Clear();
	}
}
