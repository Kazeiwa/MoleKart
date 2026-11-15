using System;
using System.Collections.Generic;
using System.Threading;
using Executors;
using UnityEngine;

[AddComponentMenu("Executors Framework/Execution Manager")]
public class ExecutionManager : MonoBehaviour
{
	private interface IManagedTask
	{
		bool IsDone { get; }

		void CallCallback();
	}

	private class ManagedTask<T> : IManagedTask
	{
		private ICallable<T> callable;

		private Future<T> future;

		private TaskFinishedHandler<T> finishedHandler;

		public bool IsDone => future.IsDone;

		public ManagedTask(ICallable<T> callable, Future<T> future, TaskFinishedHandler<T> finishedHandler)
		{
			this.callable = callable;
			this.future = future;
			this.finishedHandler = finishedHandler;
		}

		public void CallCallback()
		{
			finishedHandler(callable, future);
		}
	}

	public delegate void TaskFinishedHandler<T>(ICallable<T> finishedTask, Future<T> finishedFuture);

	public int threadCount = 1;

	public int taskCountWarningThreshold = 100;

	private IExecutor executor;

	private List<IManagedTask> managedTasks = new List<IManagedTask>();

	private void Awake()
	{
		if (threadCount == 0)
		{
			executor = new ImmediateExecutor();
			return;
		}
		if (threadCount == 1)
		{
			executor = new SingleThreadExecutor();
			return;
		}
		throw new NotImplementedException("Currently only 0-1 thread executors are supported.");
	}

	private void FixedUpdate()
	{
		foreach (IManagedTask managedTask in managedTasks)
		{
			if (managedTask.IsDone)
			{
				managedTask.CallCallback();
			}
		}
		managedTasks.RemoveAll((IManagedTask managedTask) => managedTask.IsDone);
	}

	private void OnApplicationQuit()
	{
		executor.Shutdown();
		while (!executor.IsShutdown())
		{
			Thread.Sleep(10);
		}
	}

	public void SubmitAndManage<T>(ICallable<T> task, TaskFinishedHandler<T> finishedHandler)
	{
		if (managedTasks.Count >= taskCountWarningThreshold)
		{
			Debug.LogWarning("Execution Manager on " + base.gameObject.name + " currently has " + managedTasks.Count + " work tasks in queue.");
		}
		Future<T> future = executor.Submit(task);
		managedTasks.Add(new ManagedTask<T>(task, future, finishedHandler));
	}

	public Future<T> SubmitAndForget<T>(ICallable<T> task)
	{
		return executor.Submit(task);
	}

	public int GetQueueSize()
	{
		return executor.GetQueueSize();
	}
}
