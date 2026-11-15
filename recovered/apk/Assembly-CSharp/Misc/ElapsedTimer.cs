using UnityEngine;

namespace Misc;

public class ElapsedTimer : SimpleElapsedTimer
{
	private float startTime;

	public float ElapsedTime { get; set; }

	public float WrappedElapsedTime => Mathf.Min(ElapsedTime, base.Limit);

	public ElapsedTimer(float limit)
		: base(limit)
	{
		ElapsedTime = 0f;
	}

	public new void Reset()
	{
		startTime = Time.time;
		endTime = startTime + limit;
		base.TimeOut = false;
	}

	public new void Reset(float limit)
	{
		base.limit = limit;
		Reset();
	}

	public new void Update()
	{
		float time = Time.time;
		if (time > endTime)
		{
			base.TimeOut = true;
		}
		ElapsedTime = time - startTime;
	}
}
