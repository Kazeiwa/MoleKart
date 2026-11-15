namespace Executors;

internal class WorkItem<T> : IWorkItem
{
	public readonly ICallable<T> callable;

	public readonly Future<T> future;

	public WorkItem(ICallable<T> callable, Future<T> future)
	{
	}

	public void Execute()
	{
	}

	public void Cancel(string reason)
	{
	}
}
