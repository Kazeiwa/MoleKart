namespace Executors;

public class ImmediateExecutor : IExecutor
{
	private bool shutdown;

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
