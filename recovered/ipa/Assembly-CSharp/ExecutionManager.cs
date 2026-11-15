using System.Collections.Generic;
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

		public bool IsDone
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public ManagedTask(ICallable<T> callable, Future<T> future, TaskFinishedHandler<T> finishedHandler)
		{
		}

		public void CallCallback()
		{
		}
	}

	public delegate void TaskFinishedHandler<T>(ICallable<T> finishedTask, Future<T> finishedFuture);

	public int threadCount;

	public int taskCountWarningThreshold;

	private IExecutor executor;

	private List<IManagedTask> managedTasks;

	private void Awake()
	{
	}

	private void FixedUpdate()
	{
	}

	private void OnApplicationQuit()
	{
	}

	public void SubmitAndManage<T>(ICallable<T> task, TaskFinishedHandler<T> finishedHandler)
	{
	}

	public Future<T> SubmitAndForget<T>(ICallable<T> task)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetQueueSize()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
