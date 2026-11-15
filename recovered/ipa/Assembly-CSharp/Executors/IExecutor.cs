namespace Executors;

public interface IExecutor
{
	Future<T> Submit<T>(ICallable<T> callable);

	bool IsShutdown();

	void Shutdown();

	int GetQueueSize();
}
