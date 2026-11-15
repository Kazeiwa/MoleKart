using UnityEngine;

namespace Misc;

public class SimpleElapsedTimer
{
	protected float limit;

	protected float endTime;

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

	public bool TimeOut { get; set; }

	public SimpleElapsedTimer(float limit)
	{
		this.limit = limit;
		TimeOut = false;
	}

	public void Reset()
	{
		endTime = Time.time + limit;
		TimeOut = false;
	}

	public void Reset(float limit)
	{
		this.limit = limit;
		Reset();
	}

	public void Update()
	{
		if (Time.time > endTime)
		{
			TimeOut = true;
		}
	}
}
