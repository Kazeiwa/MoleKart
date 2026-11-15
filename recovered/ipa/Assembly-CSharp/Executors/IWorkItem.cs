namespace Executors;

internal interface IWorkItem
{
	void Execute();

	void Cancel(string reason);
}
