using System.Collections.Generic;
using System.Threading;

namespace Executors;

internal class SingleThreadExecutor : IExecutor
{
	private Thread workerThread;

	private readonly Queue<IWorkItem> taskQueue;

	private readonly object locker;

	private ShutdownMode shutdownMode;

	private volatile bool shutdown;

	private volatile bool shutdownCompleted;

	public SingleThreadExecutor()
	{
	}

	public SingleThreadExecutor(ShutdownMode shutdownMode)
	{
	}

	private void RunWorker()
	{
	}

	public Future<T> Submit<T>(ICallable<T> callable)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool IsShutdown()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Shutdown()
	{
	}

	public int GetQueueSize()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
