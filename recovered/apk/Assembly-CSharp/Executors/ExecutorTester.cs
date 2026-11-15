using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Executors;

public class ExecutorTester
{
	private class MultiplyTask : ICallable<double>
	{
		private int a;

		private int b;

		public MultiplyTask(int a, int b)
		{
			this.a = a;
			this.b = b;
		}

		public double Call()
		{
			Thread.Sleep(10);
			return (double)a * (double)b;
		}
	}

	private class ExceptionThrowingTask : ICallable<int>
	{
		public int Call()
		{
			throw new ExecutionException(new Exception("Task thrown exception."));
		}
	}

	private bool doLogging;

	public ExecutorTester(bool doLogging)
	{
		this.doLogging = doLogging;
	}

	private void Log(string msg)
	{
		if (doLogging)
		{
			Debug.Log("ExecutorTester: " + msg);
		}
	}

	public void TestAllExecutors()
	{
		List<IExecutor> list = new List<IExecutor>();
		list.Add(new ImmediateExecutor());
		list.Add(new SingleThreadExecutor());
		foreach (IExecutor item in list)
		{
			DoBasicTest(item);
		}
		foreach (IExecutor item2 in list)
		{
			DoExceptionTest(item2);
		}
		foreach (IExecutor item3 in list)
		{
			DoShutdownTest(item3);
		}
		DoShutdownWithPendingTasksTest(new SingleThreadExecutor());
		DoShutdownWithPendingTasksTest(new SingleThreadExecutor(ShutdownMode.CancelQueuedTasks));
	}

	private void DoBasicTest(IExecutor executor)
	{
		List<Future<double>> list = new List<Future<double>>();
		List<double> list2 = new List<double>();
		for (int i = 1; i < 10; i++)
		{
			list.Add(executor.Submit(new MultiplyTask(i, i)));
			list2.Add(i * i);
		}
		for (int j = 0; j < list.Count; j++)
		{
			AssertAlmostEqual(list[j].GetResult(), list2[j]);
			AssertTrue(list[j].IsDone);
			Log("Basic test " + j + " with executor type " + executor.GetType().Name + " passed.");
		}
	}

	private void DoExceptionTest(IExecutor executor)
	{
		List<Future<int>> list = new List<Future<int>>();
		for (int i = 0; i < 10; i++)
		{
			list.Add(executor.Submit(new ExceptionThrowingTask()));
		}
		for (int j = 0; j < list.Count; j++)
		{
			try
			{
				list[j].GetResult();
				throw new Exception("Shouldn't be here...");
			}
			catch (ExecutionException)
			{
			}
			AssertTrue(list[j].IsDone);
			Log("Exception test " + j + " with executor type " + executor.GetType().Name + " passed.");
		}
	}

	private void DoShutdownTest(IExecutor executor)
	{
		executor.Shutdown();
		for (int i = 0; i < 20; i++)
		{
			if (executor.IsShutdown())
			{
				Log("Shutdown test with executor type " + executor.GetType().Name + " passed.");
				return;
			}
			Thread.Sleep(100);
		}
		throw new Exception("Executor " + executor.GetType().Name + " failed to shutdown in a timely manner.");
	}

	private void DoShutdownWithPendingTasksTest(IExecutor executor)
	{
		List<Future<double>> list = new List<Future<double>>();
		for (int i = 0; i < 20; i++)
		{
			list.Add(executor.Submit(new MultiplyTask(i, i)));
		}
		Thread.Sleep(100);
		DoShutdownTest(executor);
		int queueSize = executor.GetQueueSize();
		Log("Items in queue after shutdown: " + queueSize);
		int num = 0;
		int num2 = 0;
		for (int j = 0; j < list.Count; j++)
		{
			try
			{
				if (!list[j].IsDone)
				{
					throw new Exception("All queued tasks should have been set to done during shutdown.");
				}
				num++;
			}
			catch (ExecutionException)
			{
				num2++;
			}
		}
		AssertEqual(queueSize, num2);
		Log("Shutdown with pending tasks: " + num + " completed, " + num2 + " cancelled.");
	}

	private void AssertTrue(bool condition)
	{
		if (!condition)
		{
			throw new Exception("Condition not true.");
		}
	}

	private void AssertEqual(int i1, int i2)
	{
		if (i1 != i2)
		{
			throw new Exception("Numbers are not equal: " + i1 + " , " + i2);
		}
	}

	private void AssertAlmostEqual(double d1, double d2)
	{
		if (Math.Abs(d1 - d2) > 1.0000000116860974E-07)
		{
			throw new Exception("Numbers are not equal: " + d1 + " , " + d2);
		}
	}
}
