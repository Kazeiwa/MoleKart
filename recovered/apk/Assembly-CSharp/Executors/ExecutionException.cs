using System;

namespace Executors;

public class ExecutionException : Exception
{
	public readonly Exception delayedException;

	public ExecutionException(Exception delayedException)
	{
		this.delayedException = delayedException;
	}
}
