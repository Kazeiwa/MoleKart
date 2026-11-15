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
		return new GameObject("DelayDelegateTimer").AddComponent<DelayDelegateTimer>();
	}

	public void InitTimer(float fTimeLimit, OnCallback fun)
	{
		this.fun = fun;
		this.fTimeLimit = fTimeLimit;
		bStart = false;
	}

	public void StartTimer()
	{
		bStart = true;
		fStartTime = Time.time;
	}

	public void StopTimer()
	{
		bStart = false;
	}

	public void Reset()
	{
		bStart = true;
		fStartTime = Time.time;
	}

	private void Update()
	{
		if (bStart)
		{
			float num = Time.time - fStartTime;
			if (fTimeLimit <= num)
			{
				Debug.Log("Call Fun from DelayDelegateTimer!");
				fun();
				StopTimer();
			}
		}
	}
}
