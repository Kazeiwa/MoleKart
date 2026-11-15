using System;

namespace Executors;

internal class WorkItem<T> : IWorkItem
{
	public readonly ICallable<T> callable;

	public readonly Future<T> future;

	public WorkItem(ICallable<T> callable, Future<T> future)
	{
		this.callable = callable;
		this.future = future;
	}

	public void Execute()
	{
		try
		{
			T result = callable.Call();
			future.SetResult(result);
		}
		catch (Exception delayedException)
		{
			future.SetException(new ExecutionException(delayedException));
		}
		finally
		{
			future.SetDone();
		}
	}

	public void Cancel(string reason)
	{
		if (future.IsDone)
		{
			throw new InvalidOperationException("Can not cancel a future that is done.");
		}
		future.SetException(new ExecutionException(new Exception("Task was cancelled due to: " + reason)));
		future.SetDone();
	}
}
