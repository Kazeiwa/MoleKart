namespace Executors;

public class ExecutorTester
{
	private class MultiplyTask : ICallable<double>
	{
		private int a;

		private int b;

		public MultiplyTask(int a, int b)
		{
		}

		public double Call()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private class ExceptionThrowingTask : ICallable<int>
	{
		public int Call()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private bool doLogging;

	public ExecutorTester(bool doLogging)
	{
	}

	private void Log(string msg)
	{
	}

	public void TestAllExecutors()
	{
	}

	private void DoBasicTest(IExecutor executor)
	{
	}

	private void DoExceptionTest(IExecutor executor)
	{
	}

	private void DoShutdownTest(IExecutor executor)
	{
	}

	private void DoShutdownWithPendingTasksTest(IExecutor executor)
	{
	}

	private void AssertTrue(bool condition)
	{
	}

	private void AssertEqual(int i1, int i2)
	{
	}

	private void AssertAlmostEqual(double d1, double d2)
	{
	}
}
