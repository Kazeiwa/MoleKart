using System;
using System.Collections.Generic;
using System.Threading;

namespace Executors;

internal class SingleThreadExecutor : IExecutor
{
	private Thread workerThread;

	private readonly Queue<IWorkItem> taskQueue = new Queue<IWorkItem>();

	private readonly object locker = new object();

	private ShutdownMode shutdownMode;

	private volatile bool shutdown;

	private volatile bool shutdownCompleted;

	public SingleThreadExecutor()
		: this(ShutdownMode.FinishAll)
	{
	}

	public SingleThreadExecutor(ShutdownMode shutdownMode)
	{
		this.shutdownMode = shutdownMode;
		ThreadStart start = RunWorker;
		workerThread = new Thread(start);
		workerThread.Start();
	}

	private void RunWorker()
	{
		while (!shutdown)
		{
			lock (locker)
			{
				while (taskQueue.Count == 0 && !shutdown)
				{
					Monitor.Wait(locker);
				}
			}
			while (taskQueue.Count > 0 && (!shutdown || !shutdownMode.Equals(ShutdownMode.CancelQueuedTasks)))
			{
				IWorkItem workItem = null;
				lock (locker)
				{
					if (taskQueue.Count > 0)
					{
						workItem = taskQueue.Dequeue();
					}
				}
				workItem?.Execute();
			}
		}
		foreach (IWorkItem item in taskQueue)
		{
			item.Cancel("Shutdown");
		}
		shutdownCompleted = true;
	}

	public Future<T> Submit<T>(ICallable<T> callable)
	{
		lock (locker)
		{
			if (shutdown)
			{
				throw new InvalidOperationException("May not submit tasks after shutting down executor.");
			}
			Future<T> future = new Future<T>();
			WorkItem<T> item = new WorkItem<T>(callable, future);
			taskQueue.Enqueue(item);
			Monitor.Pulse(locker);
			return future;
		}
	}

	public bool IsShutdown()
	{
		return shutdownCompleted;
	}

	public void Shutdown()
	{
		lock (locker)
		{
			shutdown = true;
			Monitor.Pulse(locker);
		}
	}

	public int GetQueueSize()
	{
		lock (locker)
		{
			return taskQueue.Count;
		}
	}
}
