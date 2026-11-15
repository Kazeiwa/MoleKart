using System;

namespace Executors;

public class Future<T> : IFuture
{
	private T result;

	private Exception exception;

	private volatile bool isDone;

	public bool IsDone
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	internal void SetResult(T result)
	{
	}

	internal void SetException(Exception e)
	{
	}

	internal void SetDone()
	{
	}

	public T GetResult()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
