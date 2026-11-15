using System;

namespace Executors;

public class ImmediateExecutor : IExecutor
{
	private bool shutdown;

	public Future<T> Submit<T>(ICallable<T> callable)
	{
		if (shutdown)
		{
			throw new InvalidOperationException("May not submit tasks after shutting down executor.");
		}
		Future<T> future = new Future<T>();
		WorkItem<T> workItem = new WorkItem<T>(callable, future);
		((IWorkItem)workItem).Execute();
		return future;
	}

	public bool IsShutdown()
	{
		return shutdown;
	}

	public void Shutdown()
	{
		shutdown = true;
	}

	public int GetQueueSize()
	{
		return 0;
	}
}
