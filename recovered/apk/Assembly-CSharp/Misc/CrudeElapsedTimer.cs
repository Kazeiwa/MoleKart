using UnityEngine;

namespace Misc;

public class CrudeElapsedTimer
{
	private float limit;

	private float elapsedTime;

	private float wrappedElapsedTime;

	private int timeOutCount;

	public float Limit
	{
		get
		{
			return limit;
		}
		set
		{
			limit = value;
		}
	}

	public float ElapsedTime => elapsedTime;

	public float WrappedElapsedTime => wrappedElapsedTime;

	public float SaturatedElapsedTime => Mathf.Min(ElapsedTime, Limit);

	public float SaturatedElapsedRate => SaturatedElapsedTime / Limit;

	public int TimeOutCount
	{
		get
		{
			return timeOutCount;
		}
		set
		{
			timeOutCount = value;
		}
	}

	public CrudeElapsedTimer(float limit)
	{
		this.limit = limit;
	}

	public void Reset()
	{
		elapsedTime = 0f;
		wrappedElapsedTime = 0f;
		ResetTimeOutCount();
	}

	public void ResetWithLimit(float limit)
	{
		Limit = limit;
		Reset();
	}

	public void ResetTimeOutCount()
	{
		timeOutCount = 0;
	}

	public int Advance(float deltaTime)
	{
		if (Limit == 0f)
		{
			return ++timeOutCount;
		}
		elapsedTime += deltaTime;
		float num = wrappedElapsedTime + deltaTime;
		int num2 = 0;
		while (num >= limit)
		{
			num -= limit;
			num2++;
		}
		wrappedElapsedTime = num;
		timeOutCount += num2;
		return num2;
	}

	public int DoAllAfter()
	{
		int num = Advance(Time.deltaTime);
		if (num > 0)
		{
			ResetTimeOutCount();
		}
		return num;
	}

	public int DoAllAfter(float newLimit)
	{
		int num = Advance(Time.deltaTime);
		if (num > 0)
		{
			ResetTimeOutCount();
			Limit = newLimit;
		}
		return num;
	}
}
