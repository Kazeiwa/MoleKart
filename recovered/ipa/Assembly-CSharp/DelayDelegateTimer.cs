using UnityEngine;

public class DelayDelegateTimer : MonoBehaviour
{
	public delegate void OnCallback();

	private OnCallback fun;

	private float fTimeLimit;

	private bool bStart;

	private float fStartTime;

	public static DelayDelegateTimer Create()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void InitTimer(float fTimeLimit, OnCallback fun)
	{
	}

	public void StartTimer()
	{
	}

	public void StopTimer()
	{
	}

	public void Reset()
	{
	}

	private void Update()
	{
	}
}
