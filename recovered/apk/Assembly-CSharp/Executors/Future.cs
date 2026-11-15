using System;
using System.Threading;

namespace Executors;

public class Future<T> : IFuture
{
	private T result;

	private Exception exception;

	private volatile bool isDone;

	public bool IsDone => isDone;

	internal void SetResult(T result)
	{
		this.result = result;
	}

	internal void SetException(Exception e)
	{
		exception = e;
	}

	internal void SetDone()
	{
		isDone = true;
	}

	public T GetResult()
	{
		while (!IsDone)
		{
			Thread.Sleep(1);
		}
		if (exception != null)
		{
			throw exception;
		}
		return result;
	}
}
